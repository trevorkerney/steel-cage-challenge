using UnityEngine;

public class AIController : MonoBehaviour, IWrestlerController
{
    private bool called = false;

    public void Delegate(Wrestler wrestler)
    {
        if (!called)
        {
            Debug.Log("AI Controller not yet implemented");
            called = true;
        }
    }
}
