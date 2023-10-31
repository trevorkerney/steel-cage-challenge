using UnityEngine;

public class Wrestler : MonoBehaviour, ILossSubject
{
    public WASDController controller;
    
    // [HideInInspector]
    public BoxCollider2D boundary;
    // [HideInInspector]
    public Wrestler opponent;

    public SpriteRenderer rndr;
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D cl;

    public int strength = 100;
    public float moveSpeed = 0.02f;

    public Vector2 moveDir = Vector2.zero;

    void Update()
    {
        animator.SetFloat("Ydiff", opponent.transform.position.y - rb.position.y);
        
        if (moveDir.x != 0 || moveDir.y != 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (opponent.transform.position.y - rb.position.y < 0)
            rndr.sortingOrder = 0;
        else
            rndr.sortingOrder = 2;

        if (opponent.transform.position.x - rb.position.x < 0)
            rndr.flipX = true;
        else
            rndr.flipX = false;
    }

    private void FixedUpdate()
    {
        controller.Delegate(this, opponent, boundary.bounds);
    }

    void OnDestroy()
    {
        if (controller != null)
        {
            Destroy(controller as Object);
        }
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

    public void Pin()
    {

    }
}
