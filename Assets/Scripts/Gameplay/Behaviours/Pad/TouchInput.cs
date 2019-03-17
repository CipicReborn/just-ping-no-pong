using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : IPadInput
{
    public bool InputPressed { get; private set; }

    /// <summary>
    /// The x coordinate of the touch in world position
    /// </summary>
    public float Position { get { return GetPosition(); } }
    /// <summary>
    /// The amount of z rotation confered to the pad
    /// </summary>
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



    private Vector2 touchPosition;
    private Vector2 originTouchPosition;
    private bool prevPressed;
    private bool isOverGUI;
    private bool wasOverGUI;
    private int collisionLayer = LayerMask.GetMask("GameInput");

    private bool JustLeftGUI()
    {
        return wasOverGUI && Input.GetTouch(0).phase == TouchPhase.Ended;
    }

    private float GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 20f, collisionLayer))
        {
            return hitInfo.point.x;
        }
        return 0;
    }


    private float GetRotation()
    {
        if (InputPressed && !prevPressed)
        {
            originTouchPosition = touchPosition;
        }

        return (touchPosition.y - originTouchPosition.y) / 10.0f;
    }
}
