using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class WrestlerSelectionLogicPVP : MonoBehaviour
{
    private Session session;

    public Image playerOneImageDisplay;
    public TextMeshProUGUI playerOneTitleDisplay;
    public Image playerTwoImageDisplay;
    public TextMeshProUGUI playerTwoTitleDisplay;

    private int currentP1Selection = 0;
    private int currentP2Selection = 1; 

    void Awake()
    {
        session = FindObjectOfType<Session>();
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
        playerOneImageDisplay.sprite = session.wrestlers[currentP1Selection].portrait;
        playerOneTitleDisplay.text = session.wrestlers[currentP1Selection].name;
    }

    private void UpdatePlayerTwoSelection()
    {
        playerTwoImageDisplay.sprite = session.wrestlers[currentP2Selection].portrait;
        playerTwoTitleDisplay.text = session.wrestlers[currentP2Selection].name;
    }

    IEnumerator TransitionToNextScene()
    {
        session.option1 = currentP1Selection;
        session.option2 = currentP2Selection;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Match");
    }
}
