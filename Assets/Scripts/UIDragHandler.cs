using System;

using UnityEngine.EventSystems;
using UnityEngine;

public enum DragType {
    Begin, Drag, End
}

[RequireComponent(typeof(RectTransform))]
public class UIDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform RT;

    public Action<DragType, Vector2> callback;

    private void Awake()
    {
        RT = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RT.position = eventData.position;
        callback?.Invoke(DragType.Drag, eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        callback?.Invoke(DragType.Begin, eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        callback?.Invoke(DragType.End, eventData.position);
    }
}