using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrestler : MonoBehaviour, ILossSubject
{
    public IWrestlerController controller;

    private List<ILossObserver> lossObservers = new();
    
    // defined at runtime
    [HideInInspector]
    public string username;
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
    private AudioSource hitSound;
    [SerializeField]
    private float maxHitXDist = .2f;
    [SerializeField]
    private float maxHitYDist = .1f;
    [SerializeField]
    private int punchDamage = 3;
    [SerializeField]
    private int kickDamage = 6;

    [HideInInspector]
    public int strength = 100;
    [SerializeField]
    private float moveSpeed = 0.02f;
    [HideInInspector]
    public Vector2 moveDir = Vector2.zero;

    bool stunned = false;
    bool punching = false;
    bool kicking = false;
    public int numDrops = 0;
    int numPins = 0;
    int getUp = 0;

    void Update()
    {
        // handle wrestlers' depth
        if (opponent.transform.position.y - transform.position.y < 0)
            rend.sortingOrder = 0;
        else
            rend.sortingOrder = 2;
        
        // disable most animations if player is stunned, laying, or pinning
        if (!stunned && !animator.GetBool("IsLaying") && !animator.GetBool("IsPinning"))
        {
            // flip up or down animations depending on opponent location
            animator.SetFloat("Ydiff", opponent.transform.position.y - transform.position.y);
            
            // trigger walk animation if movement vector > 0
            if (moveDir.x != 0 || moveDir.y != 0)
                animator.SetBool("IsWalking", true);
            else
                animator.SetBool("IsWalking", false);

            // flip left or right animations depending on opponent location
            if (opponent.transform.position.x - transform.position.x < 0)
                rend.flipX = true;
            else
                rend.flipX = false;
        }
    }

    void FixedUpdate()
    {
        // delegate input to controller
        controller.Delegate(this);

        // if not stunned, laying, or pinning, apply motion vector
        if (!stunned && !animator.GetBool("IsLaying") && !animator.GetBool("IsPinning"))
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
        stunned = true;
        yield return new WaitForSeconds(.3f);
        stunned = false;
    }
    
    IEnumerator IPunching()
    {
        punching = true;
        yield return new WaitForSeconds(.5f);
        punching = false;
    }

    IEnumerator IKicking()
    {
        kicking = true;
        yield return new WaitForSeconds(.6f);
        kicking = false;
    }

    IEnumerator IPinning(int pinNum)
    {
        yield return new WaitForSeconds(3f);
        if (animator.GetBool("IsPinning") && numPins == pinNum)
        {
            Notify(username, opponent.username);
            animator.SetBool("IsWinner", true);
            animator.SetBool("IsPinning", false);
            opponent.numDrops = int.MaxValue;
        }
    }

    public void Stunned()
    {
        if (!stunned && !animator.GetBool("IsLaying"))
        {
            animator.SetTrigger("Stun");
            StartCoroutine(IStunned());
        }
    }

    public void LoseStrength(int loss)
    {
        if (strength <= loss)
            strength = 0;
        else
            strength -= loss;
    }

    public void Pin()
    {
        if (!stunned && !animator.GetBool("IsLaying") && !animator.GetBool("IsPinning") && !punching && !kicking)
        {
            float xDiff = Mathf.Abs(transform.position.x - opponent.transform.position.x);
            float yDiff = Mathf.Abs(transform.position.y - opponent.transform.position.y);
            if (xDiff <= maxHitXDist && yDiff <= maxHitYDist)
            {
                animator.SetBool("IsPinning", true);
                Vector2 newPos = opponent.transform.position;
                newPos.y -= .001f;
                transform.position = newPos;
                rend.flipX = !opponent.rend.flipX;
                StartCoroutine(IPinning(++numPins));
            }
        }
    }

    public void Laid()
    {
        animator.SetBool("IsLaying", true);
        if (numDrops == 0)
            strength = 50;
        numDrops++;
    }

    public void GetUp()
    {
        if (++getUp >= 18 + 3 * numDrops)
        {
            animator.SetBool("IsLaying", false);
            opponent.animator.SetBool("IsPinning", false);
            getUp = 0;
        }
    }

    public void ActionA()
    {
        if (animator.GetBool("IsLaying"))
            GetUp();
        else if (opponent.animator.GetBool("IsLaying"))
            Pin();
        else
            Punch();
    }

    public void ActionB()
    {
        Kick();
    }

    public void Punch()
    {
        if (!stunned && !animator.GetBool("IsLaying") && !punching && !kicking)
        {
            animator.SetTrigger("Punch");
            StartCoroutine(IPunching());
            opponent.Hit(punchDamage);
        }
    }

    public void Kick()
    {
        if (!stunned && !animator.GetBool("IsLaying") && !animator.GetBool("IsPinning") && !punching && !kicking)
        {
            animator.SetTrigger("Kick");
            StartCoroutine(IKicking());
            opponent.Hit(kickDamage);
        }
    }

    public void Hit(int damage)
    {
        float xDiff = Mathf.Abs(transform.position.x - opponent.transform.position.x);
        float yDiff = Mathf.Abs(transform.position.y - opponent.transform.position.y);
        if (xDiff <= maxHitXDist && yDiff <= maxHitYDist)
        {
            hitSound.Play();
            if (strength > 0)
            {
                Stunned();
                LoseStrength(damage);
            }
            else if (!animator.GetBool("IsLaying"))
            {
                Laid();
                return;
            }
        }
    }

    public void AddObserver(ILossObserver observer)
    {
        lossObservers.Add(observer);
    }

    public void RemoveObserver(ILossObserver observer)
    {
        lossObservers.Remove(observer);
    }

    public void Notify(string winner, string loser)
    {
        foreach (var obs in lossObservers)
        {
            obs.Acknowledge(username, opponent.username);
        }
    }
}
