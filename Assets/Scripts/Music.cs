using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music Instance;

    public AudioSource theme;
    public AudioSource wrestler;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
