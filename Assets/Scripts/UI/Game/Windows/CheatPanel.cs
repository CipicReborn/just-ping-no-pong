using JustPingNoPong.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatPanel : Window
{
    public Image BallA;
    public Image BallB;
    public Image BallC;

    public Image PadA;
    public Image PadB;
    public Image PadC;

    private int selectedBall;
    private int selectedPad;

    public override void Hide()
    {
        SetActive(false);
    }

    public override void Show()
    {
        SetActive(true);
    }

    public void OnClickOnBallAmateur()
    {
        selectedBall = 0;
        UIManager.FocusBall(0);
        BallA.color = Color.green;
        BallB.color = Color.white;
        BallC.color = Color.white;
    }
    public void OnClickOnBallPro()
    {
        selectedBall = 1;
        UIManager.FocusBall(1);
        BallA.color = Color.white;
        BallB.color = Color.green;
        BallC.color = Color.white;
    }
    public void OnClickOnBallMaster()
    {
        selectedBall = 2;
        UIManager.FocusBall(2);
        BallA.color = Color.white;
        BallB.color = Color.white;
        BallC.color = Color.green;
    }

    public void OnClickOnPadAmateur()
    {
        selectedPad = 0;
        UIManager.FocusPad(0);
        PadA.color = Color.green;
        PadB.color = Color.white;
        PadC.color = Color.white;
    }
    public void OnClickOnPadPro()
    {
        selectedPad = 1;
        UIManager.FocusPad(1);
        PadA.color = Color.white;
        PadB.color = Color.green;
        PadC.color = Color.white;
    }
    public void OnClickOnPadMaster()
    {
        selectedPad = 2;
        UIManager.FocusPad(2);
        PadA.color = Color.white;
        PadB.color = Color.white;
        PadC.color = Color.green;
    }

    public void OnClickOnEquip()
    {
        Hide();
        UIManager.EquipBall(selectedBall);
        UIManager.EquipPad(selectedPad);
    }
}
