using UnityEngine;
using UnityEngine.EventSystems;
using System;

public sealed class MovementController : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerDownHandler
{
    public Action<PointerEventData> OnDragEvent;
    public Action OnRightClickEvent;
    public Action OnLeftClickEvent;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            OnDragEvent?.Invoke(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            OnRightClickEvent?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            OnLeftClickEvent?.Invoke();
        }
    }
}
