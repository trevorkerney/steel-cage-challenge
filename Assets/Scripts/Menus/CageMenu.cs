using UnityEngine;
using UnityEngine.SceneManagement;

public class CageMenu : MonoBehaviour
{
    private Session session;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    public void Back()
    {
        session.cage = false;
        SceneManager.LoadScene("ModeMenu");
    }

    public void SetCage(bool cage)
    {
        session.cage = cage;
        SceneManager.LoadScene("WrestlerMenu");
    }
}
