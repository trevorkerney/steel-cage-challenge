using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class Wrestler : MonoBehaviour, ILossSubject
{
    public IWrestlerController controller;
    
    // defined at runtime
    [HideInInspector]
    public Match match;
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
    public int strength = 100;
    [SerializeField]
    private float moveSpeed = 0.02f;
    [HideInInspector]
    public Vector2 moveDir = Vector2.zero;

    float maxHitX = .2f;
    float maxHitY = .1f;

    bool stunned = false;
    bool pinned = false;

    int punchDamage = 5;
    int kickDamage = 10;

    void Update()
    {
        // handle wrestlers' depth
        if (opponent.transform.position.y - phys.position.y < 0)
            rend.sortingOrder = 0;
        else
            rend.sortingOrder = 2;
        
        // disable most animations if player is stunned or pinned
        if (!stunned && !pinned)
        {
            // flip up or down animations depending on opponent location
            animator.SetFloat("Ydiff", opponent.transform.position.y - transform.position.y);
            
            // trigger walk animation if movement vector > 0
            if (moveDir.x != 0 || moveDir.y != 0)
                animator.SetBool("IsWalking", true);
            else
                animator.SetBool("IsWalking", false);

            // flip left or right animations depending on opponent location
            if (opponent.transform.position.x - phys.position.x < 0)
                rend.flipX = true;
            else
                rend.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        // delegate input to controller
        controller.Delegate(this);

        // if not stunned or pinned, apply motion vector
        if (!stunned && !pinned)
        {
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
    }

    IEnumerator IStunned()
    {
        Debug.Log("stunned");
        stunned = true;
        yield return new WaitForSeconds(.5f);
        stunned = false;
        Debug.Log("not stunned");
    }

    public void Stunned()
    {
        if (!stunned)
            StartCoroutine(IStunned());
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
        if (!stunned)
        {
            animator.SetTrigger("Punch");
            float xDiff = Mathf.Abs(phys.position.x - opponent.phys.position.x);
            float yDiff = Mathf.Abs(phys.position.y - opponent.phys.position.y);
            if (xDiff <= maxHitX && yDiff <= maxHitY)
                opponent.Hit(punchDamage);
        }
    }

    public void Kick()
    {
        if (!stunned)
        {
            animator.SetTrigger("Kick");
            if (phys.position.x - opponent.phys.position.x <= maxHitX)
                opponent.Hit(kickDamage);
        }
    }

    public void Hit(int damage)
    {
        // animator.SetTrigger("Hit");
        Stunned();
        LoseStrength(damage);
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
