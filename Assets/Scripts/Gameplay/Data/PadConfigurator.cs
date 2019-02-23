using System;
using UnityEngine;
using UnityEngine.UI;

public class PadConfigurator : MonoBehaviour
{
    public struct SingleMinMax
    {
        public Single Min;
        public Single Max;
        public Single Distance {  get { return Max - Min; } }
    }

    [SerializeField]
    private PadMover Pad;
    [Header("Gauge Bounds")]
    [SerializeField]
    private SingleMinMax Gauge1Bounds;
    [SerializeField]
    private SingleMinMax Gauge2Bounds;
    [SerializeField]
    private SingleMinMax Gauge3Bounds;
    [Header("Gauge GUI Labels")]
    [SerializeField]
    private Text Gauge1GUI;
    [SerializeField]
    private Text Gauge2GUI;
    [SerializeField]
    private Text Gauge3GUI;
    [Header("Gauge Labels Text")]
    [SerializeField]
    private string Gauge1Label;
    [SerializeField]
    private string Gauge2Label;
    [SerializeField]
    private string Gauge3Label;

    public void ApplyGauge(Single normalisedValue)
    {
        //Pad.SetMaxSpeed(Gauge1Bounds.Min + normalisedValue * Gauge1Bounds.Distance);
        //MaxSpeedGaugeLabel.text = string.Format("MaxSpeed : {0:0.0}", normalisedValue * MaxGaugeSpeedValue);
    }

    public void SetPadAcceleration(float normalisedAcceleration)
    {
        //Pad2.SetAcceleration(normalisedAcceleration * MaxGaugeAccelerationValue);
        //AccelerationGaugeLabel.text = string.Format("Acceleration : {0:0.0}", normalisedAcceleration * MaxGaugeAccelerationValue);
    }

    public void SetPadRotationSpeed(float normalisedRotationSpeed)
    {
        //Pad.RotationSpeed = normalisedRotationSpeed * MaxRotationSpeed;
    }
}
