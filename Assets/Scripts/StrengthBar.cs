using UnityEngine;
using UnityEngine.UI;

public class StrengthBar : MonoBehaviour
{
    public Slider slider;

    public void SetStrength(int strength)
    {
        slider.value = strength;
    }
}
