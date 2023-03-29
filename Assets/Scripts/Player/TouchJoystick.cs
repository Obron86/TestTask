using UnityEngine;
using UnityEngine.EventSystems;

public class TouchJoystick : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform handleRectTransform;
    private Vector3 _inputVector;
    private RectTransform _backgroundRectTransform;
    private Vector3 _originalHandlePosition;

    private void Start()
    {
        _backgroundRectTransform = GetComponent<RectTransform>();
        _originalHandlePosition = handleRectTransform.localPosition;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _backgroundRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var position))
        {
            var sizeDelta = _backgroundRectTransform.sizeDelta;
            position.x /= sizeDelta.x;
            position.y /= sizeDelta.y;

            _inputVector = new Vector3(position.x * 2, 0, position.y * 2);

            if (_inputVector.magnitude > 1.0f)
            {
                _inputVector.Normalize();
            }

            var delta = _backgroundRectTransform.sizeDelta;
            handleRectTransform.localPosition = new Vector3(
                _inputVector.x * (delta.x * 0.5f),
                _inputVector.z * (delta.y * 0.5f),
                0);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector3.zero;
        handleRectTransform.localPosition = _originalHandlePosition;
    }

    public Vector3 GetInputDirection()
    {
        return _inputVector.normalized;
    }

}