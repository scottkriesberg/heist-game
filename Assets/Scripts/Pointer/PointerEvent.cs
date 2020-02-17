using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color enterColor = Color.cyan;
    [SerializeField] private Color downColor = Color.blue;
    [SerializeField] private UnityEvent OnClick = new UnityEvent();

    private MeshRenderer mesh = null;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter Button");
        mesh.material.color = enterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit Button");
        mesh.material.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down Button");
        mesh.material.color = downColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up Button");
        mesh.material.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Button");
        OnClick.Invoke();
    }
}
