using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryMenu : MonoBehaviour
{
    private Session session;

    public GameObject PvCButton;
    public GameObject PsvCButton;
    public GameObject PvPButton;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    void Update()
    {
        // if there are 2 authenticated players
        if (session.player2 != null)
        {
            PvCButton.SetActive(false);
            PsvCButton.SetActive(true);
            PvPButton.SetActive(true);
        }
        // if there is 1 authenticated player
        else if (session.player1 != null)
        {
            PvCButton.SetActive(true);
            PsvCButton.SetActive(false);
            PvPButton.SetActive(false);
        }
        // if there are no authenticated players
        else
        {
            Debug.Log("You somehow got to the category selection screen with no one logged in.");
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void SetCategory(string category)
    {
        switch (category)
        {
            case "PvC":
                // session.category = Category.PvC;
                // break;
                Debug.Log("Computer player disabled: AI Controller not yet implemented.");
                return;
            case "PsvC":
                // session.category = Category.PsvC;
                // break;
                Debug.Log("Computer player disabled: AI Controller not yet implemented.");
                return;
            case "PvP":
                session.category = Category.PvP;
                break;
            default:
                Debug.Log(category + " is not a category.");
                return;
        }
        SceneManager.LoadScene("ModeMenu");
    }
}
