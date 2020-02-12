using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.cyan;
    public Color32 m_DownColor = Color.blue;

    private Image m_Image = null;
    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Button");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Button");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down Button");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up Button");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Button");
    }

}
