using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
    public PadData Data;
    public IPadInput Input { get; private set; }
    public float ScreenRightLimit { get { return gameWorldBoundaries.ScreenRightLimit; } }

    //charac
    private Transform startTransform;

    //gameworld
    private GameWorldBoundaries gameWorldBoundaries;
    [SerializeField][Tooltip("This is not to be confused with the speed of the pad")]
    private readonly float LerpFactor = 0.4f;

    //component
    private PositionController positionController;
    private RotationController rotationController;
    private ReboundForce reboundBehaviour;

    public void Init(GameManager gm, IPadInput input, GameWorldBoundaries gwb, Transform startTransform)
    {
        Input = input;
        this.startTransform = startTransform;

        gameWorldBoundaries = gwb;

        positionController = GetComponent<PositionController>();
        positionController.Init(this);

        rotationController = GetComponent<RotationController>();
        rotationController.Init(this);

        reboundBehaviour = GetComponentInChildren<ReboundForce>();
        reboundBehaviour.Init(gm, Data);
        
    }

    public void Reset()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
    }

    public void Tick(Single deltaTime)
    {
        if (Input.InputPressed)
        {
            GoToTargetPosition();
        }
        GoToTargetRotation();
    }

    public float GetNormalisedXPosition()
    {
        return (transform.position.x + ScreenRightLimit) / (ScreenRightLimit * 2.0f);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(positionController.GetPosition(), transform.position.y, transform.position.z);
    }

    private Quaternion GetTargetRotation()
    {
        return Quaternion.Euler(0, positionController.GetYAngle(), rotationController.GetZAngle());
    }

    private void GoToTargetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), LerpFactor);
    }

    private void GoToTargetRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, GetTargetRotation(), LerpFactor);
    }

}