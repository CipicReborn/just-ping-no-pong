using UnityEngine;

public class PadMove : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public float MaxRotation; // around 5-15
    public PadController Controller;

    private int direction;
    private GUIStyle style = new GUIStyle();
    public void OnGUI()
    {
        style.fontSize = 30;
        GUI.contentColor = Color.black;
        
        GUILayout.Label("Direction : " + direction, style);
        GUILayout.Label("Up Vector X-Component : " + transform.up.x, style);

        style.fontSize = 25;
        GUILayout.Label("CanRotateLeft : " + CanRotateLeftFromMove(direction), style);
        GUILayout.Label("CanComeBackLeft : " + CanComeBackWithLeft(direction), style);
        GUILayout.Label("CanRotateRight : " + CanRotateRightFromMove(direction), style);
        GUILayout.Label("CanComeBackRight : " + CanComeBackWithRight(direction), style);

        GUILayout.Label("DeltaPosition : " + Controller.DeltaPosition, style);
        GUILayout.Label("GoLeft : " + Controller.GoLeft, style);
        GUILayout.Label("GoRight : " + Controller.GoRight, style);
    }

    void Update()
    {
        if (Controller.GoLeft)
        {
            direction = -1;
        }
        else if (Controller.GoRight)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }
        ApplyMove(direction);
        ApplyRotate(direction);
    }

    private void ApplyMove(int direction)
    {
        if (Controller.IsTouch)
        {
            direction = Controller.Position.x > transform.position.x ? 1 : -1;
            if (Controller.Position.x < transform.position.x)
            {
                direction = -1;
            }
            else if (Controller.Position.x > transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = 0;
            }
            transform.position = Controller.Position;
        }
        else
        {
            transform.position += Vector3.right * Speed * Time.deltaTime * direction;
        }
    }

    private void ApplyRotate(int direction)
    {
        if (CanRotateLeftFromMove(direction) || CanComeBackWithLeft(direction))
        {
            transform.rotation *= Quaternion.Euler(new Vector3(0, 0, RotationSpeed) * -1 * Time.deltaTime);
            //Debug.Log("Moving Left (" + transform.up.x + ")");
        }
        else if (CanRotateRightFromMove(direction) || CanComeBackWithRight(direction))
        {
            transform.rotation *= Quaternion.Euler(new Vector3(0, 0, RotationSpeed) * 1 * Time.deltaTime);
            //Debug.Log("Moving Right");
        }

        if (transform.rotation.z < -MaxRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -MaxRotation));
            //Debug.Log("Limited Left");
        }
        if (transform.rotation.z > MaxRotation)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, MaxRotation));
            //Debug.Log("Limited Right");
        }
        if(direction == 0 && (transform.up.x != 0f && transform.up.x < 0.01f && transform.up.x > -0.01f))
        {
            transform.rotation = Quaternion.identity;
            //Debug.Log("Centered : " + transform.up.x);
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(transform.up.x, 0, 0));
    }

    private bool CanRotateLeftFromMove(int direction)
    {
        return direction < 0 && transform.up.x < 0.2f;
    }
    private bool CanRotateRightFromMove(int direction)
    {
        return direction > 0 && transform.up.x > -0.2f;
    }
    private bool CanComeBackWithLeft (int direction)
    {
        return direction == 0 && transform.up.x < 0;
    }
    private bool CanComeBackWithRight(int direction)
    {
        return direction == 0 && transform.up.x > 0;
    }

}
