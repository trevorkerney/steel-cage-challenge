using UnityEngine;

public class Wrestler : MonoBehaviour, ILossSubject
{
    public IWrestlerController controller;
    
    // defined at runtime
    [HideInInspector]
    public BoxCollider2D boundary;
    [HideInInspector]
    public Wrestler opponent;
    [HideInInspector]
    public Animator animator;

    // defined at compile
    [SerializeField]
    private SpriteRenderer rend;
    [SerializeField]
    private Rigidbody2D phys;
    [SerializeField]
    private BoxCollider2D body;

    [SerializeField]
    private int strength = 100;
    [SerializeField]
    private float moveSpeed = 0.02f;
    [HideInInspector]
    public Vector2 moveDir = Vector2.zero;

    void Update()
    {
        // flip up or down animations depending on opponent location
        animator.SetFloat("Ydiff", opponent.transform.position.y - phys.position.y);
        
        // trigger walk animation in movement vector > 0
        if (moveDir.x != 0 || moveDir.y != 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        // handle wrestlers' depth
        if (opponent.transform.position.y - phys.position.y < 0)
            rend.sortingOrder = 0;
        else
            rend.sortingOrder = 2;

        // flip left or right animations depending on opponent location
        if (opponent.transform.position.x - phys.position.x < 0)
            rend.flipX = true;
        else
            rend.flipX = false;
    }

    private void FixedUpdate()
    {
        // delegate input to controller
        controller.Delegate(this);

        // apply motion vector
        moveDir.Normalize();
        Vector2 newPos = moveSpeed * moveDir + phys.position;
        newPos.x = Mathf.Clamp(
            newPos.x,
            boundary.bounds.min.x + body.bounds.size.x / 2,
            boundary.bounds.max.x - body.bounds.size.x / 2
        );
        newPos.y = Mathf.Clamp(
            newPos.y,
            boundary.bounds.min.y + body.bounds.size.y / 2,
            boundary.bounds.max.y - body.bounds.size.y / 2
        );
        phys.MovePosition(newPos);
    }

    public void LoseStrength(int loss)
    {
        if (strength <= loss)
        {
            strength = 0;
        }
        else
        {
            strength -= loss;
        }
    }

    public void Punch()
    {
        animator.SetTrigger("Punch");
    }

    public void Kick()
    {
        animator.SetTrigger("Kick");
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void Pin()
    {

    }

    public void AddObserver(ILossObserver observer)
    {

    }

    public void RemoveObserver(ILossObserver observer)
    {
        
    }

    public void Notify(int player)
    {

    }
}
