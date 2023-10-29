using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI buttonText;
    public GameObject starPrefab;  // The prefab reference

    private GameObject leftStarInstance;  // Stores the instantiated left star
    private GameObject rightStarInstance;  // Stores the instantiated right star
    private bool isHovered = false;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        // Flashing text effect
        if (isHovered)
        {
            float alpha = Mathf.PingPong(Time.time * 2, 1);
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, alpha);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        ActivateHoverEffect();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DeactivateHoverEffect();
    }

    private void ActivateHoverEffect()
    {
        isHovered = true;

        // Use the width of the button's RectTransform to correctly offset the stars
        float xOffset = rectTransform.rect.width * 0.5f + 10;  // 10 is a small padding, adjust as needed
        leftStarInstance = Instantiate(starPrefab, transform.position + new Vector3(-xOffset, 0, 0), Quaternion.identity, transform.parent);
        rightStarInstance = Instantiate(starPrefab, transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity, transform.parent);
    }

    private void DeactivateHoverEffect()
    {
        isHovered = false;
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1);
        
        if (leftStarInstance != null)
        {
            Destroy(leftStarInstance);
        }
        if (rightStarInstance != null)
        {
            Destroy(rightStarInstance);
        }
    }
}
