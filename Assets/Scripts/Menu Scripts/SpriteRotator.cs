using UnityEngine;
using UnityEngine.UI; // Necessary for the Image component

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites; // An array of sprites you want to cycle through
    private Image imageComponent; // Reference to the Image component
    private int currentSpriteIndex = 0; // Current index in the sprite array
    public float changeInterval = 0.2f; // Interval between sprite changes

    private void Start()
    {
        // Get the Image component on this object
        imageComponent = GetComponent<Image>();

        // Check if we have a valid Image component and sprites
        if (imageComponent != null && sprites.Length > 0)
        {
            // Start the sprite changing loop
            InvokeRepeating("ChangeSprite", 0f, changeInterval);
        }
    }

    void ChangeSprite()
    {
        // Set the sprite to the next one in the array
        imageComponent.sprite = sprites[currentSpriteIndex];

        // Increment the sprite index and loop back to 0 if at the end
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
    }
}
