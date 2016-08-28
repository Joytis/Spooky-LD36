using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Collider2D))]
public class Collider2DRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    Collider2D myCollider;
    RectTransform rectTransform;
    private Vector3 screenPoint, offset, defaultLocalScale, snapToPos;
    bool inInventory;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        rectTransform = GetComponent<RectTransform>();
        defaultLocalScale = transform.localScale;
        inInventory = false;
    }

    #region ICanvasRaycastFilter implementation
    public bool IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera)
    {
        var worldPoint = Vector3.zero;
        var isInside = RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            screenPos,
            eventCamera,
            out worldPoint
        );
        if (isInside)
            isInside = myCollider.OverlapPoint(worldPoint);
        return isInside;
    }
    #endregion

    void OnMouseDown()
    {
        if(IsRaycastLocationValid(Input.mousePosition, Camera.main))
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
        Debug.Log("transform.position: " + transform.position);
    }

    void OnMouseUp()
    {
        if(inInventory)
        {
            SnapToInventory(snapToPos);
        }
    }

    void OnMouseOver()
    {
        if (inInventory)
        {
            transform.localScale = defaultLocalScale / .5f;
        }
    }

    void OnMouseExit()
    {
        if (inInventory)
        {
            transform.localScale = .5f * defaultLocalScale;
        }
    }
    
    void SnapToInventory(Vector3 pos)
    {
        inInventory = true;
        transform.position = pos;
        if (transform.localScale.x >= defaultLocalScale.x)
        {
            transform.localScale = .5f * defaultLocalScale;
        }

        snapToPos = pos;
    }

    void ExitInventory()
    {
        inInventory = false;
        if (transform.localScale.x <= defaultLocalScale.x)
        {
            transform.localScale = defaultLocalScale;
        }
    }

    void Update()
    {
        if(!inInventory)
        {
            transform.localScale = defaultLocalScale;
        }
    }
}