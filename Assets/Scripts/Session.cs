using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public readonly string username;

    public Account(string p_username)
    {
        username = p_username;
    }
}

public enum Category
{
    PvC,
    PsvC,
    PvP
}

public enum Gamemode
{
    OvO,
    TT,
    WWF,
    TTWWF,
}

[System.Serializable]
public class WrestlerOption
{
    public string name;
    public Sprite portrait;
    public Sprite nameSprite;
    public RuntimeAnimatorController animator;
}

public class Session : MonoBehaviour
{
    private static Session Instance;

    public Account player1;
    public Account player2;

    [HideInInspector]
    public Category category;

    [HideInInspector]
    public Gamemode gamemode;

    public List<WrestlerOption> wrestlers;
    [HideInInspector]
    public int option1;
    [HideInInspector]
    public int option2;

    public bool cage = false;

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
