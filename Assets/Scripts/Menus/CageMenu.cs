using UnityEngine;
using UnityEngine.SceneManagement;

public class CageMenu : MonoBehaviour
{
    private Session session;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    public void SetCage(bool cage)
    {
        session.cage = cage;
        SceneManager.LoadScene("WrestlerMenu");
    }
}
