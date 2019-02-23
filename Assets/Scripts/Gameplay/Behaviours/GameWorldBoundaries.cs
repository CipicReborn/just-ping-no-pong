using UnityEngine;

public class GameWorldBoundaries
{
    public float ScreenRightLimit { get; private set; }
    public Camera Camera;

    public GameWorldBoundaries (Camera camera)
    {
        Camera = camera;
        CalculateScreenRightLimit();
    }

    private void CalculateScreenRightLimit()
    {
        var frustumHeight = 2.0f * -Camera.transform.position.z * Mathf.Tan(Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * Camera.aspect;
        ScreenRightLimit = frustumWidth / 2.0f;
    }
}
