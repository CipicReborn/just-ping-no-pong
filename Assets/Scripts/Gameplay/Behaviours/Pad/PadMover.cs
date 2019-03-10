using System;
using UnityEngine;

public class PadMover : MonoBehaviour
{
    private IPadInput input;

    //charac
    private PadData data;
    private Transform startTransform;

    //gameworld
    private GameWorldBoundaries gameWorldBoundaries;
    private float ScreenRightLimit { get { return gameWorldBoundaries.ScreenRightLimit; } }
    
    //calc
    private float rotationModeSwitchPosition;
    private float maximumYRotation;
    private float rotationFactor;

    //component
    private ReboundForce reboundBehaviour;

    public void Init(GameManager gm, IPadInput input, PadData data, GameWorldBoundaries gwb, Transform startTransform)
    {
        this.input = input;
        this.data = data;
        this.startTransform = startTransform;
        gameWorldBoundaries = gwb;

        reboundBehaviour = GetComponentInChildren<ReboundForce>();
        reboundBehaviour.Init(gm, data);

        PrepareBehaviourVariables();
    }

    public void Reset()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
    }

    public void Tick(Single deltaTime)
    {
        if (input.InputPressed)
        {
            GoToTargetPosition();
            GoToTargetRotation();
        }
    }

    public float GetNormalisedXPosition()
    {
        return (transform.position.x + ScreenRightLimit) / (ScreenRightLimit * 2.0f);
    }

    private void PrepareBehaviourVariables()
    {
        rotationModeSwitchPosition = ScreenRightLimit - 2 * data.PadWidth;
        maximumYRotation = Mathf.Asin(data.PadWidth / data.PadLength) * Mathf.Rad2Deg;
        rotationFactor = maximumYRotation / (ScreenRightLimit - 2 * data.PadWidth);
    }

    private Vector3 CalculateTargetPosition()
    {
        return new Vector3(input.Position, transform.position.y, transform.position.z);
    }

    private Quaternion CalculateTargetRotation()
    {
        float angle;
        if (input.Position < -rotationModeSwitchPosition)
        {
            var x = input.Position + ScreenRightLimit;
            angle = Mathf.Asin((data.PadWidth - x) / data.PadLength) * Mathf.Rad2Deg;
        }
        else if (input.Position > rotationModeSwitchPosition)
        {
            var x = ScreenRightLimit - input.Position;
            angle = Mathf.Asin((x - data.PadWidth) / data.PadLength) * Mathf.Rad2Deg;
        }
        else
        {
            angle = input.Position * rotationFactor;
        }
        //Debug.Log(angle);
        
        var zAngle = Mathf.Min(Mathf.Max(input.Rotation * data.ZRotationSpeed, -data.MaximumZRotation), data.MaximumZRotation);
        return Quaternion.Euler(0, angle, zAngle);
    }

    private void GoToTargetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, CalculateTargetPosition(), data.LerpFactor);
    }

    private void GoToTargetRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, CalculateTargetRotation(), data.LerpFactor);
    }

}
