using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WrestlerMenu : MonoBehaviour
{
    private Session session;

    public Image p1Portrait;
    public Image p1Name;
    public Image p2Portrait;
    public Image p2Name;

    private int p1Selection = -1;
    private int p2Selection = -1; 

    IEnumerator LoadMatch()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Match");
    }

    private void UpdateP1Selection()
    {
        p1Portrait.sprite = session.wrestlers[p1Selection].portrait;
        p1Name.sprite = session.wrestlers[p1Selection].nameSprite;
        p1Name.gameObject.SetActive(true);
    }

    private void UpdateP2Selection()
    {
        p2Portrait.sprite = session.wrestlers[p2Selection].portrait;
        p2Name.sprite = session.wrestlers[p2Selection].nameSprite;
        p2Name.gameObject.SetActive(true);
    }

    void IncrementP1Selection()
    {
        do
        {
            if (p1Selection + 1 < session.wrestlers.Count)
                ++p1Selection;
            else
                p1Selection = 0;
        } while (p1Selection == p2Selection);
        UpdateP1Selection();
    }

    void DecrementP1Selection()
    {
        do
        {
            if (p1Selection - 1 >= 0)
                --p1Selection;
            else
                p1Selection = session.wrestlers.Count - 1;
        } while (p1Selection == p2Selection);
        UpdateP1Selection();
    }

    void IncrementP2Selection()
    {
        do
        {
            if (p2Selection + 1 < session.wrestlers.Count)
                ++p2Selection;
            else
                p2Selection = 0;
        } while (p2Selection == p1Selection);
        UpdateP2Selection();
    }

    void DecrementP2Selection()
    {
        do
        {
            if (p2Selection - 1 >= 0)
                --p2Selection;
            else
                p2Selection = session.wrestlers.Count - 1;
        } while (p2Selection == p1Selection);
        UpdateP2Selection();
    }

    void Awake()
    {
        session = FindObjectOfType<Session>();
        p1Name.gameObject.SetActive(false);
        p2Name.gameObject.SetActive(false);
    }

    void Update()
    {
        bool p1Prev = Input.GetKeyDown(KeyCode.A);
        bool p1Next = Input.GetKeyDown(KeyCode.D);
        bool p2Prev = Input.GetKeyDown(KeyCode.LeftArrow);
        bool p2Next = Input.GetKeyDown(KeyCode.RightArrow);
        bool cont = Input.GetKeyDown(KeyCode.Return);

        if (session.option1 == null)
        {
            if (p1Next)
                IncrementP1Selection();
            else if (p1Prev)
                DecrementP1Selection();
        }

        if (session.option1 != null)
        {
            if (p2Next)
                IncrementP2Selection();
            else if (p2Prev)
                DecrementP2Selection();
        }

        if (cont)
        {
            if (session.option1 == null)
            {
                if (p1Selection >= 0)
                {
                    session.option1 = p1Selection;
                }
            }
            else if (p2Selection >= 0)
            {
                session.option2 = p2Selection;
                StartCoroutine(LoadMatch());
            }
        }
    }
}
