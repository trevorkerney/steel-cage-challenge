using UnityEngine;
using UnityEngine.InputSystem;

public class WASDController : MonoBehaviour, IWrestlerController
{
    public InputAction moveInput;
    public InputAction AInput;
    public bool isAInputHeld = false;
    public InputAction BInput;
    public bool isBInputHeld = false;

    private void OnEnable()
    {
        moveInput.Enable();
        AInput.Enable();
        BInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        AInput.Disable();
        BInput.Disable();
    }

    public void Delegate(Wrestler wrestler, Wrestler opponent, Bounds boundary)
    {
        if (moveInput != null)
        {
            wrestler.moveDir = moveInput.ReadValue<Vector2>();
        }
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
        if (AInput.ReadValue<float>() == 1f && !isAInputHeld)
        {
            isAInputHeld = true;
            wrestler.Punch();
        }
        else if (AInput.ReadValue<float>() == 0f)
        {
            isAInputHeld = false;
        }
        if (BInput.ReadValue<float>() == 1f && !isAInputHeld)
        {
            isBInputHeld = true;
            wrestler.Kick();
        }
        else if (BInput.ReadValue<float>() == 0f)
        {
            isBInputHeld = false;
        }
    }
}
