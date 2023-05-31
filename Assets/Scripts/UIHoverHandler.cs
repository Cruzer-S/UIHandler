using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

public enum HoverType
{
    Begin, Hover, End
}

public class UIHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private float hoverTime;

    public Action<HoverType, Vector2> callback;

    private Coroutine hoverTimer;
    private bool hovering;
    private Vector2 lastPoint;

    private void Awake()
    {
        hoverTimer = null;
        hovering = false;

        lastPoint = Vector2.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = false;
        hoverTimer = StartCoroutine(HoverWaitAndAction());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hovering)
            callback(HoverType.End, eventData.position);

        if (hoverTimer != null) {
            StopCoroutine(hoverTimer);
            hoverTimer = null;
        }

        hovering = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        lastPoint = eventData.position;

        if (hovering)
            callback(HoverType.Hover, eventData.position);
    }

    private IEnumerator HoverWaitAndAction()
    {
        yield return new WaitForSeconds(hoverTime);

        callback(HoverType.Begin, lastPoint);

        hovering = true;
    }
}
