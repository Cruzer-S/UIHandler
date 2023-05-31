using UnityEngine;
using UnityEngine.UI;

using TMPro;

[RequireComponent(typeof(UIDragHandler), typeof(UIClickHandler))]
[RequireComponent(typeof(UIHoldHandler), typeof(UIHoverHandler))]
public class TestButton : MonoBehaviour
{
    [SerializeField] private GameObject holdPanel;
    private GameObject holdPanelInstance;

    private UIDragHandler dragHandler;
    private UIHoldHandler holdHandler;
    private UIClickHandler clickHandler;
    private UIHoverHandler hoverHandler;

    private TMP_Text Title;
    private string origin;
    private int count;

    private void Awake()
    {
        dragHandler = GetComponent<UIDragHandler>();
        holdHandler = GetComponent<UIHoldHandler>();
        clickHandler = GetComponent<UIClickHandler>();
        hoverHandler = GetComponent<UIHoverHandler>();


        holdPanelInstance = null;
    }

    private void Start()
    {
        Title = GetComponentInChildren<TMP_Text>();
        origin = Title.text;

        count = 0;
    }

    private void OnEnable()
    {
        holdHandler.callback += OnHold;
        dragHandler.callback += OnDrag;
        clickHandler.callback += OnClick;
        hoverHandler.callback += OnHover;
    }

    private void OnDisable()
    {
        holdHandler.callback -= OnHold;
        dragHandler.callback -= OnDrag;
        clickHandler.callback -= OnClick;
        hoverHandler.callback -= OnHover;
    }

    private void OnHover(HoverType type, Vector2 point)
    {
        switch (type)
        {
        case HoverType.Begin:
            GetComponent<Image>().color = Color.grey;
            break;

        case HoverType.End:
            GetComponent<Image>().color = Color.white;
            break;
        }
    }

    private void OnClick(Vector2 point)
    {
        Title.text = $"{origin} {++count}";
    }
    
    private void OnHold(HoldType type, Vector2 point)
    {
        switch (type) {
        case HoldType.Begin:
            if ( !holdPanelInstance )
                holdPanelInstance = Instantiate(holdPanel, point, Quaternion.identity, transform.parent);

            break;

        case HoldType.End:
            if (holdPanelInstance) {
                GameObject.Destroy(holdPanelInstance);
                holdPanelInstance = null;
            }
            break;
        }
    }

    private void OnDrag(DragType type, Vector2 point)
    {
        if ( type == DragType.Begin && !holdPanelInstance ) {
            GameObject.Destroy(holdPanelInstance);
            holdPanelInstance = null;
        }
    }
}