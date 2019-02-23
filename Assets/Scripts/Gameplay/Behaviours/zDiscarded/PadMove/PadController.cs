using System;
using UnityEngine;

public class PadController
{
    public bool GoLeft;
    public bool GoRight;
    public float DeltaPosition { get; private set; }
    public bool IsTouch = false;

    private Action GetInput;
    private float lastMousePosition;
    public Vector3 Position;

    public void Init()
    {
#if UNITY_EDITOR
        lastMousePosition = Input.mousePosition.x;
        GetInput = GetMouse;
#else
        InitTouch();
#endif
    }


    public void Tick()
    {
        GetInput();

        GoLeft = DeltaPosition < 0;
        GoRight = DeltaPosition > 0;
    }


    private void InitTouch()
    {
        IsTouch = true;
        GetInput = GetTouch;
        Position = Vector3.zero;
        Position.y = 1;
    }

    private void GetTouch()
    {
        //ByDeltaPos();
        if (Input.touchCount == 0)
        {
            
        }
        else
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 20f))
            {
                Position.x = hitInfo.point.x;
            }
            
        }
    }

    private void ByDeltaPos()
    {
        if (Input.touchCount == 0)
        {
            DeltaPosition = 0;
        }
        else
        {
            DeltaPosition = Input.GetTouch(0).deltaPosition.x / 2.0f;
        }
    }

    private void GetMouse()
    {
        DeltaPosition = Input.mousePosition.x - lastMousePosition;
        lastMousePosition = Input.mousePosition.x;
    }
}
