using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;



[System.Serializable]
public class WrestlerOption
{
    public string name;
    public Sprite image;
}

public class WrestlerSelectionLogic : MonoBehaviour
{
    public List<WrestlerOption> availableWrestlers;
    public Sprite playerImage;
    public TextMeshProUGUI playerTitle;
    public Sprite computerImage;
    public TextMeshProUGUI computerTitle;
    private int currentPlayerSelection = 0;

    public Image playerImageUI;
    public Image computerImageUI;

    
    private void Update()
    {
        // Player selection
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentPlayerSelection--;
            if (currentPlayerSelection < 0)
                currentPlayerSelection = availableWrestlers.Count - 1;
            UpdatePlayerSelection();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentPlayerSelection++;
            if (currentPlayerSelection >= availableWrestlers.Count)
                currentPlayerSelection = 0;
            UpdatePlayerSelection();
        }
        
        // Confirm player's selection and select for computer
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectForComputer();
        }
    }

    private void UpdatePlayerSelection()
    {
        playerImage = availableWrestlers[currentPlayerSelection].image;
        playerTitle.text = availableWrestlers[currentPlayerSelection].name;
        playerImageUI.sprite = playerImage;  // Update the UI Image component with the new sprite
    }

   private void SelectForComputer()
    {
        // Temporarily remove the player's choice from the list
        WrestlerOption playerChoice = availableWrestlers[currentPlayerSelection];
        availableWrestlers.RemoveAt(currentPlayerSelection);

        // Pick a random wrestler for the computer
        int randomIndex = Random.Range(0, availableWrestlers.Count);
        computerImage = availableWrestlers[randomIndex].image;
        computerTitle.text = availableWrestlers[randomIndex].name;
        computerImageUI.sprite = computerImage;

        // Re-add the player's choice back into the list
        availableWrestlers.Insert(currentPlayerSelection, playerChoice);

        // Use StartCoroutine for a delay before transitioning to the next scene
        StartCoroutine(TransitionToNextScene());
    }



    IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        // Load the next scene here using SceneManager.LoadScene()
    }
}
