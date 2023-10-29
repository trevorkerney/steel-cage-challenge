using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wrestler : MonoBehaviour
{
    public IWrestlerController controller;
    public InputAction playerControls;
    public Animator animator;
    public SpriteRenderer rndr;

    public BoxCollider2D ringCollider;

    public Rigidbody2D rb;
    public BoxCollider2D cl;

    [SerializeField]
    public float moveSpeed = 0.02f;
    [SerializeField]
    public int strength = 100;

    public Vector2 moveDir = Vector2.zero;

    public Transform opponent;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        ringCollider = transform.parent.Find("Ring").Find("RingBG").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rndr = GetComponent<SpriteRenderer>();
        opponent = transform.parent.Find("W2");
    }

    void Update()
    {
        animator.SetFloat("Ydiff", opponent.position.y - rb.position.y);
        
        if (moveDir.x != 0 || moveDir.y != 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (opponent.position.y - rb.position.y < 0)
            rndr.sortingOrder = 0;
        else
            rndr.sortingOrder = 2;

        if (opponent.position.x - rb.position.x < 0)
            rndr.flipX = true;
        else
            rndr.flipX = false;
    }

    private void FixedUpdate()
    {
        controller.Delegate(this, ringCollider.bounds);
    }

    public void Punch()
    {

    }
}
