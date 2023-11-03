using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WrestlerSelectionLogic : MonoBehaviour
{
    private Session session;

    public Image player1Portrait;
    public Image player1Name;
    public Image player2Portrait;
    public Image player2Name;

    private int currentP1Selection = 0;
    private int currentP2Selection = 1; 

    void Awake()
    {
        session = FindObjectOfType<Session>();
        player1Name.gameObject.SetActive(false);
        player2Name.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Player One selection
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            currentP1Selection = CycleSelection(currentP1Selection, Input.GetKeyDown(KeyCode.D));
            UpdatePlayerOneSelection();
        }

        // Player Two selection
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentP2Selection = CycleSelection(currentP2Selection, Input.GetKeyDown(KeyCode.RightArrow));
            
            while (currentP2Selection == currentP1Selection)
            {
                currentP2Selection = CycleSelection(currentP2Selection, Input.GetKeyDown(KeyCode.RightArrow));
            }
            UpdatePlayerTwoSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    int CycleSelection(int currentIndex, bool isIncrement)
    {
        if (isIncrement)
        {
            currentIndex++;
            if (currentIndex >= session.wrestlers.Count)
                currentIndex = 0;
        }
        else
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = session.wrestlers.Count - 1;
        }
        return currentIndex;
    }

    private void UpdatePlayerOneSelection()
    {
        player1Portrait.sprite = session.wrestlers[currentP1Selection].portrait;
        player1Name.sprite = session.wrestlers[currentP1Selection].nameSprite;
        player1Name.gameObject.SetActive(true);
    }

    private void UpdatePlayerTwoSelection()
    {
        player2Portrait.sprite = session.wrestlers[currentP2Selection].portrait;
        player2Name.sprite = session.wrestlers[currentP2Selection].nameSprite;
        player2Name.gameObject.SetActive(true);
    }

    IEnumerator TransitionToNextScene()
    {
        session.option1 = currentP1Selection;
        session.option2 = currentP2Selection;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Match");
    }
}
