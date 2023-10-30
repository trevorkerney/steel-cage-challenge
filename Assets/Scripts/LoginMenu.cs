using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginMenu : MonoBehaviour
{
    private Database db;
    private Session session;

    public GameObject usernameInput;
    public GameObject passwordInput;

    void Awake()
    {
        db = FindObjectOfType<Database>();
        session = FindObjectOfType<Session>();
    }
    
    private bool IsAlphanumeric(string val)
    {
        for (int c = 0; c < val.Length; c++)
            if (!char.IsLetterOrDigit(val[c]))
                return false;
        return true;
    }

    public void LoginPlayer()
    {
        string username = usernameInput.GetComponent<TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMP_InputField>().text;

        if (!IsAlphanumeric(username) || !IsAlphanumeric(password))
        {
            Debug.Log("Username and password must be alphanumeric.");
            return;
        }
        if (!db.Login(username, password))
        {
            Debug.Log("There is no match for this username/password combination.");
            return;
        }

        if (session.player1 == null)
        {
            session.player1 = new Account(username);
        }
        else if (session.player2 == null)
        {
            session.player2 = new Account(username);
        }
        else
        {
            Debug.Log("Somehow you got to the login menu when all players are logged in");
        }
        SceneManager.LoadScene("StartMenu");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
