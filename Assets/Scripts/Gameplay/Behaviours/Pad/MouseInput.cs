using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : IPadInput
{
    public bool InputPressed { get; private set; }

    public float Position { get { return GetPosition(); } }
    public float Rotation { get { return GetRotation(); } }
    public float RawVerticalDelta { get { return mousePosition.y - originMousePosition.y; } }

    public void Refresh()
    {
        prevPressed = InputPressed;
        InputPressed = Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject();
        mousePosition = Input.mousePosition;
        if (InputPressed && !prevPressed)
        {
            originMousePosition = mousePosition;
            Debug.Log("Origin reset");
        }
    }

    private float GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 20f))
        {
            return hitInfo.point.x;
        }
        return 0;
    }

    private Vector3 mousePosition;
    private Vector3 originMousePosition;
    private bool prevPressed;

    private float GetRotation()
    {
        return mousePosition.y - originMousePosition.y;
    }
}
