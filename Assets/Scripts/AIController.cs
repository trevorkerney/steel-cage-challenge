using UnityEngine;

public class AIController : MonoBehaviour, IWrestlerController
{
    private bool called = false;

    public void Delegate(Wrestler wrestler, Wrestler opponent, Bounds boundary)
    {
        if (!called)
        {
            Debug.Log("AI Controller not yet implemented");
            called = true;
        }
    }
}
