using JustPingNoPong.UI;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    #region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044 // Disable recommendation "Add readonly modifier"

    [SerializeField]
    private GameData PlayerData;
    [SerializeField]
    private GameDesignData GameDesignData;
    [SerializeField]
    private Pad Pad;
    [SerializeField]
    private Ball Ball;
    [SerializeField]
    private Transform PadStartTransform;
    [SerializeField]
    private Transform BallStartTransform;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private UIManager UIManager;
    [SerializeField]
    private HUD HUD;

#pragma warning restore IDE0044
#pragma warning restore CS0649
    #endregion


    #region UNITY INITIALISATION

    void Awake()
    {
        Application.targetFrameRate = 60;

#if UNITY_EDITOR
        padInput = new MouseInput();
#else
        padInput = new TouchInput();
#endif
        gameWorldBoundaries = new GameWorldBoundaries(Camera);
    }

    void Start()
    {
        Equip();
        SelectMission();
        UIManager.Init(this);
    }

    public void Equip()
    {
        EquipPad(Pad);
        EquipBall(Ball);
    }

    public void SelectMission()
    {
        CurrentMission = GameDesignData.Missions[0];
    }

    #endregion


    #region API

    public bool GameIsOver { get; private set; } = true;
    public Mission CurrentMission { get; private set; }

    public void AddScoreForRebound(Vector3 worldPosition)
    {
        score += scoreForRebounds;
        HUD.UpdateScoreGUI(scoreForRebounds, score, worldPosition);
    }

    public void AddScoreForWalls(Vector3 worldPosition)
    {
        score += scoreForWalls;
        HUD.UpdateScoreGUI(scoreForWalls, score, worldPosition);
    }

    public void AddScoreForSpinning()
    {
        var scoreForSpinning = 2;
        score += scoreForSpinning;
        HUD.UpdateScoreGUI(scoreForSpinning, score, Pad.transform.position);
    }

    public void PauseGame()
    {
        if (GameIsOver) return;
        PauseGameplay();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        ResumeGameplay();
        Debug.Log("Game Resumed");
    }

    public void TriggerGameOver()
    {
        HUD.GameOver();
        Ball.Disable();
        GameIsOver = true;
        Debug.Log("Triggered Game Over");
    }

    public void ProcessResults()
    {
        UIManager.ShowResults(CurrentMission, score, score >= CurrentMission.ScoreTarget);
        Debug.Log("Results Processed");
    }

    public void ResetGame()
    {
        RandomiseBallStartPosition();
        ResetGameWorld();
        ResumeGameplay();
        Debug.Log("Game Started");
    }

    public void EquipPad(Pad pad)
    {
        Pad = pad;
        Pad.Init(this, padInput, gameWorldBoundaries, PadStartTransform);
    }

    public void EquipBall(Ball ball)
    {
        Ball = ball;
        Ball.Init(this, gameWorldBoundaries, BallStartTransform);
    }

    #endregion


    #region GAME LOGIC IMPLEM

    private void Update()
    {
        if (gameIsPaused) return;

        // INPUT
        padInput.Refresh();
        
        // GAME OBJECTS
        Pad.Tick(Time.deltaTime); // let the player manipulate the pad even after game over

        if (!GameIsOver)
        {
            Ball.Tick(Time.deltaTime);
        }

        // GAME RULES
        if (!targetReached && score >= CurrentMission.ScoreTarget)
        {
            Debug.Log("Success " + score + " of " + CurrentMission.ScoreTarget + "!");
            targetReached = true;
            HUD.ShowMissionSuccessFeedback();
        }


        // UI
        if (padInput.InputPressed)
        {
            HUD.UpdatePadGUI(Pad.GetNormalisedXPosition());
        }
    }

    private void FixedUpdate()
    {
        if (gameIsPaused || GameIsOver) return;
        Pad.TickPhysics(Time.deltaTime);
        Ball.TickPhysics(Time.deltaTime);
    }

    private GameWorldBoundaries gameWorldBoundaries;
    private IPadInput padInput;
    private int score;

    private bool gameIsPaused;
    private int scoreForWalls = 0;
    private int scoreForRebounds = 1;
    private bool targetReached;


    private void ResetGameWorld()
    {
        score = 0;
        targetReached = false;
        GameIsOver = false;
        Pad.Reset();
        Ball.Reset();
    }

    private void PauseGameplay()
    {
        gameIsPaused = true;
        Ball.PausePhysics();
    }

    private void ResumeGameplay()
    {
        gameIsPaused = false;
        Ball.ResumePhysics();
    }

    private void RandomiseBallStartPosition()
    {
        var pos = BallStartTransform.position;
        pos.x = Random.Range(0f, 1f) > 0.5f ? +1 : -1;
        BallStartTransform.position = pos;
    }
    #endregion
}
