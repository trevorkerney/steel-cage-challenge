using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Match : MonoBehaviour, ILossObserver
{
    private Database db;
    private Session session;

    // prefabs of objects spawned at runtime (rings, wrestlers, controllers)
    public WASDController WASDInputPrefab;
    public ArrowController arrowsInputPrefab;
    public AIController AIInputPrefab;
    public GameObject ringPrefab;
    public GameObject cagePrefab;
    public Wrestler wrestlerPrefab;

    // spawn points for objects spawned at runtime
    public Transform ringSpawn;
    public Transform wrestler1Spawn;
    public Transform wrestler2Spawn;

    // UI elements configured at runtime
    public Image portrait1;
    public Image portrait2;
    public Image name1;
    public Image name2;
    public StrengthBar strength1;
    public StrengthBar strength2;

    [HideInInspector]
    public GameObject ring;
    [HideInInspector]
    public BoxCollider2D boundary;
    private Wrestler wrestler1;
    private Wrestler wrestler2;
    
    void Awake()
    {
        db = FindObjectOfType<Database>();
        session = FindObjectOfType<Session>();

        if (session.cage)
        {
            ring = Instantiate(cagePrefab, ringSpawn);
        }
        else
        {
            ring = Instantiate(ringPrefab, ringSpawn);
        }

        boundary = ring.transform.Find("Background").GetComponent<BoxCollider2D>();

        if (!session.option1.HasValue || !session.option2.HasValue)
        {
            return;
        }

        portrait1.sprite = session.wrestlers[session.option1.Value].portrait;
        portrait1.SetNativeSize();
        portrait2.sprite = session.wrestlers[session.option2.Value].portrait;
        portrait2.SetNativeSize();

        name1.sprite = session.wrestlers[session.option1.Value].gameNameLeft;
        name1.SetNativeSize();
        name2.sprite = session.wrestlers[session.option2.Value].gameNameRight;
        name2.SetNativeSize();

        // instantiate wrestlers at their spawn points
        wrestler1 = Instantiate(wrestlerPrefab, wrestler1Spawn);
        wrestler2 = Instantiate(wrestlerPrefab, wrestler2Spawn);

        // match observes the wrestlers for a win condition
        wrestler1.AddObserver(this);
        wrestler2.AddObserver(this);

        // assign username of each player to wrestler
        wrestler1.username = session.player1.username;
        wrestler2.username = session.player2.username;

        // assign animator controllers based on selected wrestlers
        wrestler1.animator.runtimeAnimatorController = session.wrestlers[session.option1.Value].animator;
        wrestler2.animator.runtimeAnimatorController = session.wrestlers[session.option2.Value].animator;

        // assign opponent references
        wrestler1.opponent = wrestler2;
        wrestler2.opponent = wrestler1;

        // assign boundary references
        wrestler1.boundary = boundary;
        wrestler2.boundary = boundary;

        // assign controllers depending on how many players are logged in
        wrestler1.controller = Instantiate(WASDInputPrefab, transform);
        if (session.player2 != null)
        {
            // arrow input controller if player 2 logged in
            wrestler2.controller = Instantiate(arrowsInputPrefab, transform);
        }
        else
        {
            // AI controller if player 2 not logged in
            wrestler2.controller = Instantiate(AIInputPrefab, transform);
        }
    }

    void Update()
    {
        strength1.slider.value = wrestler1.strength;
        strength2.slider.value = wrestler2.strength;
    }

    IEnumerator EndMatch()
    {
        yield return new WaitForSeconds(3f);
        // fade to black?
        SceneManager.LoadScene("StartMenu");
    }

    public void Acknowledge(string winner, string loser)
    {
        Destroy(wrestler1.controller as Object);
        Destroy(wrestler2.controller as Object);
        db.RecordWin(winner);
        db.RecordLoss(loser);
        StartCoroutine(EndMatch());
    }

    void OnDestroy()
    {
        Destroy(wrestler1);
        Destroy(wrestler2);
    }
}
