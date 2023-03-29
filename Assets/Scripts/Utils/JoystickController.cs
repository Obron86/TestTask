using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private GameObject joystick;
    private TouchJoystick _touchJoystick;

    private void Start()
    {
        joystick.SetActive(false);
        _touchJoystick = joystick.GetComponent<TouchJoystick>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform canvasRectTransform = joystick.transform.parent.GetComponent<RectTransform>();
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPosition);

        joystick.transform.localPosition = localPosition;
        joystick.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _touchJoystick.OnPointerUp(eventData);
        joystick.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _touchJoystick.OnDrag(eventData);
    }
}