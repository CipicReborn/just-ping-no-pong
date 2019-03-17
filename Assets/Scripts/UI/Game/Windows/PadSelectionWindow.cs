using JustPingNoPong.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PadSelectionWindow : Window
{
    public TextMeshProUGUI PadName;
    public TextMeshProUGUI Description;

    public Button NextPadButton;
    public Button PrevPadButton;

    public AttributeLine Line1;
    public AttributeLine Line2;
    public AttributeLine Line3;
    
    private void Awake()
    {
        Show();
    }

    int currentPadId = 0;

    public void OnClickOnNextPad()
    {
        currentPadId = UIManager.GetNextPadId();
        Refresh();
    }

    public void OnClickOnPrevPad()
    {
        currentPadId = UIManager.GetPrevPadId();
        Refresh();
    }

    private void Refresh()
    {
        PrevPadButton.interactable = currentPadId > 0;
        NextPadButton.interactable = currentPadId < 2;
        Debug.Log("Displaying pad " + currentPadId);
        UIManager.FocusPad(currentPadId);
        ShowInfo(UIManager.GetPadInfo(currentPadId));
    }

    public void OnClickOnEquip()
    {
        UIManager.EquipPad(currentPadId);
    }

    public void ShowInfo(PadInfo info)
    {
        PadName.text = info.PadName;
        Line1.Setup(info.Attributes[0]);
        Line2.Setup(info.Attributes[1]);
        Line3.Setup(info.Attributes[2]);
        Description.text = info.Description;
    }

    public override void Show()
    {
        Refresh();
        SetActive(true);
    }

    public override void Hide()
    {
        SetActive(false);
    }
}
