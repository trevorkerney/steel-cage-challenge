using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct PvCLeaderboardEntry
{
    public string username;
    public float time;
}

[System.Serializable]
public struct PsvCLeaderboardEntry
{
    public string username1;
    public string username2;
    public float time;
}

[System.Serializable]
public struct PvPLeaderboardEntry
{
    public string username;
    public int wins;
    public int losses;
    public float wl;
}

public class LeaderboardMenu : MonoBehaviour
{
    private Session session;
    private Database db;

    public Image title;

    [SerializeField]
    private List<TextMeshProUGUI> usernames = new();
    [SerializeField]
    private List<TextMeshProUGUI> wins;
    [SerializeField]
    private List<TextMeshProUGUI> losses;
    [SerializeField]
    private List<TextMeshProUGUI> wls;

    [SerializeField]
    private TextMeshProUGUI username1;
    [SerializeField]
    private TextMeshProUGUI username2;
    [SerializeField]
    private TextMeshProUGUI username3;
    [SerializeField]
    private TextMeshProUGUI username4;
    [SerializeField]
    private TextMeshProUGUI username5;

    [SerializeField]
    private TextMeshProUGUI wins1;
    [SerializeField]
    private TextMeshProUGUI wins2;
    [SerializeField]
    private TextMeshProUGUI wins3;
    [SerializeField]
    private TextMeshProUGUI wins4;
    [SerializeField]
    private TextMeshProUGUI wins5;

    [SerializeField]
    private TextMeshProUGUI losses1;
    [SerializeField]
    private TextMeshProUGUI losses2;
    [SerializeField]
    private TextMeshProUGUI losses3;
    [SerializeField]
    private TextMeshProUGUI losses4;
    [SerializeField]
    private TextMeshProUGUI losses5;

    [SerializeField]
    private TextMeshProUGUI wl1;
    [SerializeField]
    private TextMeshProUGUI wl2;
    [SerializeField]
    private TextMeshProUGUI wl3;
    [SerializeField]
    private TextMeshProUGUI wl4;
    [SerializeField]
    private TextMeshProUGUI wl5;

    void Awake()
    {
        session = FindObjectOfType<Session>();
        db = FindObjectOfType<Database>();
    }

    void Start()
    {
        usernames.Add(username1);
        usernames.Add(username2);
        usernames.Add(username3);
        usernames.Add(username4);
        usernames.Add(username5);
        wins.Add(wins1);
        wins.Add(wins2);
        wins.Add(wins3);
        wins.Add(wins4);
        wins.Add(wins5);
        losses.Add(losses1);
        losses.Add(losses2);
        losses.Add(losses3);
        losses.Add(losses4);
        losses.Add(losses5);
        wls.Add(wl1);
        wls.Add(wl2);
        wls.Add(wl3);
        wls.Add(wl4);
        wls.Add(wl5);
        List<PvPLeaderboardEntry> entries = db.GetPvPLB();
        for (int entry = 0; entry < entries.Count; entry++)
        {
            usernames[entry].text = entries[entry].username;
            wins[entry].text = entries[entry].wins.ToString();
            losses[entry].text = entries[entry].losses.ToString();
            wls[entry].text = entries[entry].wl.ToString();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("CageMenu");
    }
}

// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// [System.Serializable]
// public struct PvCLeaderboardEntry
// {
//     public string username;
//     public float time;
// }

// [System.Serializable]
// public struct PsvCLeaderboardEntry
// {
//     public string username1;
//     public string username2;
//     public float time;
// }

// [System.Serializable]
// public struct PvPLeaderboardEntry
// {
//     public string username;
//     public int wins;
//     public int losses;
//     public float wl;
// }

// public class LeaderboardMenu : MonoBehaviour
// {
//     private Session session;
//     private Database db;

//     public Image title;

//     [SerializeField]
//     private TextMeshProUGUI username1;
//     [SerializeField]
//     private TextMeshProUGUI username2;
//     [SerializeField]
//     private TextMeshProUGUI username3;
//     [SerializeField]
//     private TextMeshProUGUI username4;
//     [SerializeField]
//     private TextMeshProUGUI username5;

//     [SerializeField]
//     private TextMeshProUGUI wins1;
//     [SerializeField]
//     private TextMeshProUGUI wins2;
//     [SerializeField]
//     private TextMeshProUGUI wins3;
//     [SerializeField]
//     private TextMeshProUGUI wins4;
//     [SerializeField]
//     private TextMeshProUGUI wins5;

//     [SerializeField]
//     private TextMeshProUGUI losses1;
//     [SerializeField]
//     private TextMeshProUGUI losses2;
//     [SerializeField]
//     private TextMeshProUGUI losses3;
//     [SerializeField]
//     private TextMeshProUGUI losses4;
//     [SerializeField]
//     private TextMeshProUGUI losses5;

//     [SerializeField]
//     private TextMeshProUGUI wl1;
//     [SerializeField]
//     private TextMeshProUGUI wl2;
//     [SerializeField]
//     private TextMeshProUGUI wl3;
//     [SerializeField]
//     private TextMeshProUGUI wl4;
//     [SerializeField]
//     private TextMeshProUGUI wl5;

//     void Awake()
//     {
//         session = FindObjectOfType<Session>();
//         db = FindObjectOfType<Database>();
//     }

//     void Start()
//     {
//         List<PvPLeaderboardEntry> entries = db.GetPvPLB();

//         username1.text = entries[0].username;
//         username2.text = entries[1].username;
//         username3.text = entries[2].username;
//         username4.text = entries[3].username;
//         username5.text = entries[4].username;

//         wins1.text = entries[0].wins.ToString();
//         wins2.text = entries[1].wins.ToString();
//         wins3.text = entries[2].wins.ToString();
//         wins4.text = entries[3].wins.ToString();
//         wins5.text = entries[4].wins.ToString();

//         losses1.text = entries[0].losses.ToString();
//         losses2.text = entries[1].losses.ToString();
//         losses3.text = entries[2].losses.ToString();
//         losses4.text = entries[3].losses.ToString();
//         losses5.text = entries[4].losses.ToString();

//         wl1.text = entries[0].wl.ToString();
//         wl2.text = entries[1].wl.ToString();
//         wl3.text = entries[2].wl.ToString();
//         wl4.text = entries[3].wl.ToString();
//         wl5.text = entries[4].wl.ToString();
//     }

//     public void Back()
//     {
//         SceneManager.LoadScene("CageMenu");
//     }
// }

