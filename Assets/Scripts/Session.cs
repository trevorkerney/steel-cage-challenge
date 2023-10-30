using UnityEngine;

public class Account
{
    public string username;

    public Account(string name)
    {
        username = name;
    }
}

public class Session : MonoBehaviour
{
    private static Session Instance;
    public Account player1 = null;
    public Account player2 = null;

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
