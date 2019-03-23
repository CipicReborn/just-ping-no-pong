﻿using System;
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
    [SerializeField]
    private PadModel PadModel;
    private PositionController positionController;
    private RotationController rotationController;
    private ReboundForce reboundBehaviour;

    //other
    private GameManager gameManager;
    [SerializeField]
    private float contactFeedbackDuration;
    private bool triggerContactFeedback;
    private bool contactFeedbackIsOn;

    public void Init(GameManager gm, IPadInput input, GameWorldBoundaries gwb, Transform startTransform)
    {
        Input = input;
        this.startTransform = startTransform;

        gameManager = gm;
        gameWorldBoundaries = gwb;

        positionController = GetComponent<PositionController>();
        positionController.Init(this);

        rotationController = GetComponent<RotationController>();
        rotationController.Init(this);

        reboundBehaviour = GetComponentInChildren<ReboundForce>();
        reboundBehaviour.Init(this, Data);
        
    }

    public void Reset()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
    }

    public void Tick(float deltaTime)
    {
        if (Input.InputPressed)
        {
            GoToTargetPosition();
        }
        GoToTargetRotation();
        ContactFeedback(deltaTime);
    }

    public float GetNormalisedXPosition()
    {
        return (transform.position.x + ScreenRightLimit) / (ScreenRightLimit * 2.0f);
    }

    public void BallContact(Rigidbody ball, Vector3 worldPosition)
    {
        triggerContactFeedback = true;
        gameManager.AddScoreForRebound(worldPosition);
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
    float elapsedTimeContactFeedback;

    private void ContactFeedback(float deltaTime)
    {
        if (triggerContactFeedback)
        {
            PadModel.SetContactZoneColor(Color.white);
            triggerContactFeedback = false;
            contactFeedbackIsOn = true;
            elapsedTimeContactFeedback = 0;
            return;
        }
        if (contactFeedbackIsOn)
        {
            elapsedTimeContactFeedback += deltaTime;
            if (elapsedTimeContactFeedback > contactFeedbackDuration)
            {
                PadModel.ResetContactZoneColor();
                contactFeedbackIsOn = false;
            }
        }
    }
}