using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class PVPWrestler
{
    public string wrestlerName;
    public Sprite wrestlerImage;
}

public class WrestlerSelectionLogicPVP : MonoBehaviour
{
    public List<PVPWrestler> availableWrestlers;
    public Image playerOneImageDisplay;
    public TextMeshProUGUI playerOneTitleDisplay;
    public Image playerTwoImageDisplay;
    public TextMeshProUGUI playerTwoTitleDisplay;

    private int currentP1Selection = 0;
    private int currentP2Selection = 1; 

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
            if (currentIndex >= availableWrestlers.Count)
                currentIndex = 0;
        }
        else
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = availableWrestlers.Count - 1;
        }
        return currentIndex;
    }

    private void UpdatePlayerOneSelection()
    {
        playerOneImageDisplay.sprite = availableWrestlers[currentP1Selection].wrestlerImage;
        playerOneTitleDisplay.text = availableWrestlers[currentP1Selection].wrestlerName;
    }

    private void UpdatePlayerTwoSelection()
    {
        playerTwoImageDisplay.sprite = availableWrestlers[currentP2Selection].wrestlerImage;
        playerTwoTitleDisplay.text = availableWrestlers[currentP2Selection].wrestlerName;
    }

    IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(3f);
        // Load the next scene here using SceneManager.LoadScene()
    }
}
