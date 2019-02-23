using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPadLateralInput : IPadInput
{
    public bool InputPressed { get; private set; }

    public float Position { get { return GetPosition(); } }
    public float Rotation { get { return GetRotation(); } }

    public void Refresh()
    {
        prevPressed = InputPressed;
        wasOverGUI = isOverGUI;

        if (Input.touchCount > 0)
        {
            isOverGUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            InputPressed = !(isOverGUI || JustLeftGUI());

            if (InputPressed) {
                touchPosition = Input.GetTouch(0).position;
            }
        }
        else
        {
            isOverGUI = false;
            InputPressed = false;
        }
    }

    private bool JustLeftGUI()
    {
        return wasOverGUI && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private float GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 20f))
        {
            return hitInfo.point.x;
        }
        return 0;
    }

    private Vector2 touchPosition;
    private Vector2 originTouchPosition;
    private bool prevPressed;
    private bool isOverGUI;
    private bool wasOverGUI;

    private float GetRotation()
    {
        if (InputPressed && !prevPressed)
        {
            originTouchPosition = touchPosition;
        }
        var deltaPos = touchPosition.y - originTouchPosition.y;
        return deltaPos/10.0f;
    }
}
