using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterMenu : MonoBehaviour
{
    private Database db;
    private Session session;

    public GameObject usernameInput;
    public GameObject passwordInput;
    public GameObject repeatInput;

    void Awake()
    {
        db = FindObjectOfType<Database>();
        session = FindObjectOfType<Session>();
    }

    private bool IsAlphanumeric(string val) {
        for (int c = 0; c < val.Length; c++)
            if (!char.IsLetterOrDigit(val[c]))
                return false;
        return true;
    }

    public void RegisterPlayer()
    {
        string username = usernameInput.GetComponent<TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMP_InputField>().text;
        string repeat = repeatInput.GetComponent<TMP_InputField>().text;

        if (username.Length > 16)
        {
            Debug.Log("Username must be 16 or less characters.");
            return;
        }
        if (!IsAlphanumeric(username) || !IsAlphanumeric(password))
        {
            Debug.Log("Username and password must be alphanumeric.");
            return;
        }
        if (!db.IsUsernameUnique(username))
        {
            Debug.Log("Username '" + username + "' is already taken.");
            return;
        }
        if (password != repeat)
        {
            Debug.Log("Passwords do not match.");
            return;
        }
        if (!db.Register(username, password))
        {
            Debug.Log("There was an unexpected error registering this account.");
            return;
        }
        if (session.player1 == null)
        {
            session.player1 = new Account(username);
        }
        else if (session.player2 == null)
        {
            if (username == session.player1.username)
            {
                Debug.Log(username + "is already logged in.");
                return;
            }
            session.player2 = new Account(username);
        }
        else
        {
            Debug.Log("Somehow you got to the register menu when all players are logged in");
        }
        SceneManager.LoadScene("StartMenu");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
