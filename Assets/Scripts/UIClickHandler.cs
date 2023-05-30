using System;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<Vector2> callback;

    public void OnPointerClick(PointerEventData eventData)
    {
        callback?.Invoke(eventData.position);
    }
}
