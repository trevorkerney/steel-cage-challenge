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
        wrestler1 = Instantiate(WrestlerPrefab, Wrestler1Spawn);
        wrestler2 = Instantiate(WrestlerPrefab, Wrestler2Spawn);

        // assign animator controllers
        wrestler1.animator.runtimeAnimatorController = session.wrestlers[session.option1].animator;
        wrestler2.animator.runtimeAnimatorController = session.wrestlers[session.option2].animator;

        // assign opponent references
        wrestler1.opponent = wrestler2;
        wrestler2.opponent = wrestler1;

        // assign boundary references
        wrestler1.boundary = boundary;
        wrestler2.boundary = boundary;

        // assign controllers depending on how many players are logged in
        wrestler1.controller = Instantiate(WASDInputPrefab, Wrestler1Spawn);
        if (session.player2 != null)
        {
            // arrow input controller if player 2 logged in
            wrestler2.controller = Instantiate(ArrowInputPrefab, Wrestler2Spawn);
        }
        else
        {
            // AI controller if player 2 not logged in
            wrestler2.controller = Instantiate(AIInputPrefab, Wrestler2Spawn);
        }
    }

    void OnDestroy()
    {
        Destroy(wrestler1.controller as Object);
        Destroy(wrestler2.controller as Object);
        Destroy(wrestler1);
        Destroy(wrestler2);
    }
}
