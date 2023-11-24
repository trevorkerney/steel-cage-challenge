using TreeEditor;
using UnityEngine;

public class Match : MonoBehaviour
{
    private Session session;

    public GameObject RingPrefab;
    public GameObject CagePrefab;
    public Transform RingSpawn;

    [HideInInspector]
    public GameObject ring;
    [HideInInspector]
    public BoxCollider2D boundary;

    public WASDController WASDInputPrefab;
    public ArrowController ArrowInputPrefab;
    public AIController AIInputPrefab;

    public Wrestler WrestlerPrefab;
    public Transform Wrestler1Spawn;
    public Transform Wrestler2Spawn;

    public StrengthBar strength1;
    public StrengthBar strength2;
    private Wrestler wrestler1;
    private Wrestler wrestler2;
    
    void Awake()
    {
        session = FindObjectOfType<Session>();

        if (session.cage)
        {
            ring = Instantiate(CagePrefab, RingSpawn);
        }
        else
        {
            ring = Instantiate(RingPrefab, RingSpawn);
        }

        boundary = ring.transform.Find("Background").GetComponent<BoxCollider2D>();

        // instantiate wrestlers at their spawn points
        wrestler1 = Instantiate(WrestlerPrefab, transform);
        wrestler2 = Instantiate(WrestlerPrefab, transform);

        // assign animator controllers
        wrestler1.animator.runtimeAnimatorController = session.wrestlers[session.option1].animator;
        wrestler2.animator.runtimeAnimatorController = session.wrestlers[session.option2].animator;

        // assign opponent references
        wrestler1.opponent = wrestler2;
        wrestler2.opponent = wrestler1;

        // assign boundary references
        wrestler1.boundary = boundary;
        wrestler2.boundary = boundary;

        // assign match references
        wrestler1.match = this;
        wrestler2.match = this;

        // assign controllers depending on how many players are logged in
        wrestler1.controller = Instantiate(WASDInputPrefab, transform);
        if (session.player2 != null)
        {
            // arrow input controller if player 2 logged in
            wrestler2.controller = Instantiate(ArrowInputPrefab, transform);
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

    void OnDestroy()
    {
        Destroy(wrestler1.controller as Object);
        Destroy(wrestler2.controller as Object);
        Destroy(wrestler1);
        Destroy(wrestler2);
    }
}
