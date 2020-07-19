using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GameSpeedChanger : MonoBehaviour
{    
    public Slider slider;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeGameSpeed);    
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeGameSpeed);    
    }

    public void ChangeGameSpeed(float value)
    {
        Time.timeScale = value;
    }
}
