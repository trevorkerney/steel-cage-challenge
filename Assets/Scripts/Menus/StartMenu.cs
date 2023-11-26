using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private Session session;

    public GameObject noPlayersInput;
    public GameObject onePlayersInput;
    public GameObject twoPlayersInput;

    void Awake()
    {
        session = FindObjectOfType<Session>();
        session.Refresh();
    }

    void Update()
    {
        if (session.player2 != null)
        {
            noPlayersInput.SetActive(false);
            onePlayersInput.SetActive(false);
            twoPlayersInput.SetActive(true);
        }
        else if (session.player1 != null)
        {
            noPlayersInput.SetActive(false);
            onePlayersInput.SetActive(true);
            twoPlayersInput.SetActive(false);
        }
        else
        {
            noPlayersInput.SetActive(true);
            onePlayersInput.SetActive(false);
            twoPlayersInput.SetActive(false);
        }
    }

    public void LoadLoginMenu()
    {
        SceneManager.LoadScene("LoginMenu"); 
    }

    public void LoadRegisterMenu()
    {
        SceneManager.LoadScene("RegisterMenu"); 
    }

    public void LoadGame()
    {
        if (session.player2 != null)
        {
            SceneManager.LoadScene("CategoryMenu");
        }
        else if (session.player1 != null)
        {
            Debug.Log("Single player disabled: AI Controller not yet implemented.");
        }
        else
        {
            Debug.Log("Somehow you tried to proceed to the category menu while no one was logged in.");
        }
    }

    public void LogoutPlayer()
    {
        if (session.player2 != null)
            session.player2 = null;
        else if (session.player1 != null)
            session.player1 = null;
        else
            Debug.Log("Somehow you tried to log out when no one was logged in.");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
