using UnityEngine;
using UnityEngine.InputSystem;

public class WASDController : MonoBehaviour, IWrestlerController
{
    private InputAction moveInput;
    private InputAction AInput;
    private bool isAInputHeld = false;
    private InputAction BInput;
    private bool isBInputHeld = false;

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

    void Awake()
    {
        moveInput = new InputAction();
        moveInput.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        AInput = new InputAction();
        AInput.AddBinding("<Keyboard>/r");
        BInput = new InputAction();
        BInput.AddBinding("<Keyboard>/t");
    }

    public void Delegate(Wrestler wrestler, Wrestler opponent, Bounds boundary)
    {
        if (AInput.ReadValue<float>() == 1f && !isAInputHeld)
        {
            isAInputHeld = true;
            wrestler.Punch();
        }
        else if (AInput.ReadValue<float>() == 0f)
        {
            isAInputHeld = false;
        }

        if (BInput.ReadValue<float>() == 1f && !isBInputHeld)
        {
            isBInputHeld = true;
            wrestler.Kick();
        }
        else if (BInput.ReadValue<float>() == 0f)
        {
            isBInputHeld = false;
        }

        wrestler.moveDir = moveInput.ReadValue<Vector2>();
    }
}
