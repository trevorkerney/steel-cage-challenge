using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeMenu : MonoBehaviour
{
    private Session session;

    public GameObject PvCInput;
    public GameObject PsvPInput;
    public GameObject PvPInput;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    void Update()
    {
        if (session.category == Category.PvC)
        {
            PvCInput.SetActive(true);
            PsvPInput.SetActive(false);
            PvPInput.SetActive(false);
        }
        else if (session.category == Category.PsvC)
        {
            PvCInput.SetActive(false);
            PsvPInput.SetActive(true);
            PvPInput.SetActive(false);
        }
        else if (session.category == Category.PvP)
        {
            PvCInput.SetActive(false);
            PsvPInput.SetActive(false);
            PvPInput.SetActive(true);
        }
        else
        {
            Debug.Log("You somehow got to the gamemode selection screen with no category selected.");
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void Back()
    {
        session.gamemode = null;
        SceneManager.LoadScene("CategoryMenu");
    }

    public void SetMode(string mode)
    {
        switch (mode)
        {
            case "1v1":
                session.gamemode = Gamemode.OvO;
                break;
            case "TT":
                // session.gamemode = Gamemode.TT;
                // break;
                Debug.Log("Tag Team not yet implemented.");
                return;
            case "WWF":
                // session.gamemode = Gamemode.WWF;
                // break;
                Debug.Log("Championship not yet implemented.");
                return;
            case "TTWWF":
                // session.gamemode = Gamemode.TTWWF;
                // break;
                Debug.Log("Tag Team Championship not yet implemented.");
                return;
            default:
                Debug.Log(mode + " is not a gamemode.");
                return;
        }
        SceneManager.LoadScene("CageMenu");
    }
}
