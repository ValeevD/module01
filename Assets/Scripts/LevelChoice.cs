using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelChoice: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Color enterColor;

    private Color exitColor;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        exitColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeButtonColor(enterColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeButtonColor(exitColor);
    }

    private void ChangeButtonColor(Color curColor)
    {
        image.color = curColor;
    }

}
