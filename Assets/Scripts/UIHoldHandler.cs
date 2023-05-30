using System;
using System.Collections;

using UnityEngine.EventSystems;
using UnityEngine;

public enum HoldType {
    Begin, End
}

[RequireComponent(typeof(RectTransform))]
public class UIHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    [SerializeField] private float holdTime;
    [SerializeField] private float holdRange;

    private Vector2 holdStartPoint;
    private Vector2 holdPoint;

    private Coroutine holdTimer;
    private bool holdOn;

    public Action<HoldType, Vector2> callback;

    private void Awake()
    {
        holdStartPoint = Vector2.zero;

        holdOn = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdStartPoint = eventData.pointerCurrentRaycast.screenPosition;

        holdOn = false;
        holdTimer = StartCoroutine(HoldWaitAndAction());
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        holdPoint = eventData.pointerCurrentRaycast.screenPosition;

        if (Vector2.Distance(holdStartPoint, holdPoint) > holdRange) {
            if (holdOn)
                callback?.Invoke(HoldType.End, holdPoint);

            if (holdTimer != null)
                StopCoroutine(holdTimer);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(holdTimer);
        holdTimer = null;

        if (holdOn)
            callback?.Invoke(HoldType.End, holdPoint);
    }

    private IEnumerator HoldWaitAndAction()
    {
        yield return new WaitForSeconds(holdTime);
        
        holdOn = true;
        callback?.Invoke(HoldType.Begin, holdPoint);
    }
}