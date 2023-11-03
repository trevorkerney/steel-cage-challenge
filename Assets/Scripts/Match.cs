using UnityEngine;

public class Match : MonoBehaviour
{
    private Session session;

    public WASDController WASDInputPrefab;
    public ArrowController ArrowInputPrefab;
    public AIController AIInputPrefab;

    public Wrestler WrestlerPrefab;
    public Transform Wrestler1Spawn;
    public Transform Wrestler2Spawn;

    public BoxCollider2D boundary;

    public StrengthBar strength1;
    public StrengthBar strength2;

    private Wrestler wrestler1;
    private Wrestler wrestler2;
    
    void Awake()
    {
        session = FindObjectOfType<Session>();

        wrestler1 = Instantiate(WrestlerPrefab, Wrestler1Spawn);
        wrestler2 = Instantiate(WrestlerPrefab, Wrestler2Spawn);

        wrestler1.animator.runtimeAnimatorController = session.wrestlers[session.option1].animator;
        wrestler2.animator.runtimeAnimatorController = session.wrestlers[session.option2].animator;

        wrestler1.opponent = wrestler2;
        wrestler2.opponent = wrestler1;

        wrestler1.boundary = boundary;
        wrestler2.boundary = boundary;

        wrestler1.controller = Instantiate(WASDInputPrefab, Wrestler1Spawn);
        if (session.player2 != null)
        {
            wrestler2.controller = Instantiate(ArrowInputPrefab, Wrestler2Spawn);
        }
        else
        {
            wrestler2.controller = Instantiate(AIInputPrefab, Wrestler2Spawn);
        }
    }
}
