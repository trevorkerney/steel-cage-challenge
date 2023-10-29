using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IWrestlerController
{
    public InputAction playerControls;

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
        
    }

    public void Delegate(Wrestler wrestler, Bounds boundary)
    {
        wrestler.moveDir = playerControls.ReadValue<Vector2>();
        wrestler.moveDir.Normalize();
        Vector2 newPos = wrestler.moveSpeed * wrestler.moveDir + wrestler.rb.position;
        newPos.x = Mathf.Clamp(
            newPos.x,
            boundary.min.x + wrestler.cl.bounds.size.x / 2,
            boundary.max.x - wrestler.cl.bounds.size.x / 2
        );
        newPos.y = Mathf.Clamp(
            newPos.y,
            boundary.min.y + wrestler.cl.bounds.size.y / 2,
            boundary.max.y - wrestler.cl.bounds.size.y / 2
        );
        wrestler.rb.MovePosition(newPos);
    }
}
