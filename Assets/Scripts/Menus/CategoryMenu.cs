using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryMenu : MonoBehaviour
{
    private Session session;

    public GameObject PVCButton;
    public GameObject PVPButton;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    void Update()
    {
        if (session.player2 != null)
        {
            PVCButton.SetActive(false);
            PVPButton.SetActive(true);
        }
        else if (session.player1 != null)
        {
            PVCButton.SetActive(true);
            PVPButton.SetActive(false);
        }
        else
        {
            Debug.Log("You somehow got to the category selection screen with no one logged in.");
            SceneManager.LoadScene("StartMenu");
        }
    }
}
