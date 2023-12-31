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
public struct WrestlerOption
{
    public string name;
    public Sprite portrait;
    public Sprite nameSprite;
    public Sprite gameNameLeft;
    public Sprite gameNameRight;
    public RuntimeAnimatorController animator;
    public AudioClip theme;
}

public class Session : MonoBehaviour
{
    private static Session Instance;

    public Account player1;
    public Account player2;

    [HideInInspector]
    public Category? category;

    [HideInInspector]
    public Gamemode? gamemode;

    public List<WrestlerOption> wrestlers;
    [HideInInspector]
    public int? option1;
    [HideInInspector]
    public int? option2;

    [HideInInspector]
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

    public void Refresh()
    {
        category = null;
        gamemode = null;
        option1 = null;
        option2 = null;
        cage = false;
    }
}
