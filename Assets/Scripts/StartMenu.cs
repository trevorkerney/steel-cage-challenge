using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private Database db;
    private Session session;

    public GameObject noPlayersInput;
    public GameObject onePlayersInput;
    public GameObject twoPlayersInput;

    void Awake()
    {
        db = FindObjectOfType<Database>();
        session = FindObjectOfType<Session>();
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
        SceneManager.LoadScene("");
    }

    public void LogoutPlayer()
    {
        if (session.player2 != null)
            session.player2 = null;
        else if (session.player1 != null)
            session.player1 = null;
        else
            Debug.Log("Somehow you clicked logout when no one was logged in.");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
