using JustPingNoPong.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionsUIController : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044 // Disable recommendation "Add readonly modifier"   [SerializeField]

    [SerializeField]
    private GameObject NextButton;

    [Header("Missions")]
    [SerializeField]
    private GameObject MissionPanel;
    [SerializeField]
    private TextMeshProUGUI MissionText;
    [SerializeField]
    private Image RewardImage;
    [SerializeField]
    private TextMeshProUGUI RewardNameText;


    [Header("Results")]
    [SerializeField]
    private GameObject ResultsPanel;
    [SerializeField]
    private TextMeshProUGUI MissionReminderText;
    [SerializeField]
    private TextMeshProUGUI ResultText;
    [SerializeField]
    private GameObject RewardBlock;
    [SerializeField]
    private GameObject TryAgainBlock;
    [SerializeField]
    private TextMeshProUGUI UnlockedItemName;
    [SerializeField]
    private Image UnlockedItemImage;

#pragma warning restore IDE0044
#pragma warning restore CS0649
    #endregion

    private enum Mode {
        Mission,
        Result
    }
    private Mode mode;
    private GameManager gameManager;
    private UIManager UIManager;
    private AudioSource winSound;

    private void Awake()
    {
        ClosePanels();
        gameObject.SetActive(true);
        winSound = GetComponent<AudioSource>();
    }

    public void Init(GameManager gm, UIManager um)
    {
        gameManager = gm;
        UIManager = um;
    }
    public void ShowMission(Mission mission)
    {
        mode = Mode.Mission;
        MissionText.text = mission.DescriptionText;
        RewardImage.sprite = mission.Reward.Image;
        RewardNameText.text = mission.Reward.Name;
        NextButton.SetActive(true);
        MissionPanel.SetActive(true);        
    }

    public void ShowResults(Mission mission, int finalScore, bool success)
    {
        mode = Mode.Result;
        MissionReminderText.text = "Target: " + mission.ReminderText;
        ResultText.text = "Your score: <color=#" + (success ? "00ff" : "ff00") + "00>" + finalScore.ToString() + "</color>";

        if (success)
        {
            //RewardBlock update
            UnlockedItemName.text = "You unlocked " + mission.Reward.Name;
            UnlockedItemImage.sprite = mission.Reward.Image;
            winSound.Play();
        }

        RewardBlock.SetActive(success);
        TryAgainBlock.SetActive(!success);
        NextButton.SetActive(true);
        ResultsPanel.SetActive(true);
    }

    public void OnClickOnNext()
    {
        ClosePanels();
        if (mode == Mode.Mission)
        {
            Debug.Log("Calling ResetGame");
            gameManager.ResetGame();

        }
        else if (mode == Mode.Result)
        {
            Debug.Log("Calling Menu");
            UIManager.ShowMenu();
        }
    }

    public void ClosePanels()
    {
        MissionPanel.SetActive(false);
        ResultsPanel.SetActive(false);
        NextButton.SetActive(false);
    }
}
