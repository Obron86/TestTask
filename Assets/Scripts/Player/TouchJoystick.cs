using UnityEngine;
using UnityEngine.EventSystems;

public class TouchJoystick : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform handleRectTransform;
    private Vector3 inputVector;
    private RectTransform backgroundRectTransform;
    private Vector3 originalHandlePosition;

    private void Start()
    {
        backgroundRectTransform = GetComponent<RectTransform>();
        originalHandlePosition = handleRectTransform.localPosition;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                backgroundRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out position))
        {
            position.x /= backgroundRectTransform.sizeDelta.x;
            position.y /= backgroundRectTransform.sizeDelta.y;

            inputVector = new Vector3(position.x * 2, 0, position.y * 2);

            if (inputVector.magnitude > 1.0f)
            {
                inputVector.Normalize();
            }

            handleRectTransform.localPosition = new Vector3(
                inputVector.x * (backgroundRectTransform.sizeDelta.x * 0.5f),
                inputVector.z * (backgroundRectTransform.sizeDelta.y * 0.5f),
                0);
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        handleRectTransform.localPosition = originalHandlePosition;
    }

    public Vector3 GetInputDirection()
    {
        return inputVector.normalized;
    }

}