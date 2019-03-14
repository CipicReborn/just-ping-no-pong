using JustPingNoPong.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private PadMover Pad;
    [SerializeField]
    private PadData PadData;
    [SerializeField]
    private Ball Ball;
    [SerializeField]
    private BallData BallData;
    [SerializeField]
    private Transform PadStartTransform;
    [SerializeField]
    private Transform BallStartTransform;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private UIManager UIManager;

#pragma warning restore IDE0044
#pragma warning restore CS0649
    #endregion


    #region UNITY INITIALISATION

    void Awake()
    {
        Application.targetFrameRate = 60;

#if UNITY_EDITOR
        padInput = new MousePadLateralInput();
#else
        padInput = new TouchPadLateralInput();
#endif
        gameWorldBoundaries = new GameWorldBoundaries(Camera);
    }

    void Start()
    {
        Pad.Init(this, padInput, PadData, gameWorldBoundaries, PadStartTransform);
        Ball.Init(this, BallData, gameWorldBoundaries, BallStartTransform);
        UIManager.Init(this);
        CurrentMission = GameDesignData.Missions[0];
        ShowTips();
    }

    #endregion


    #region API

    public Mission CurrentMission { get; private set; }

    public void AddScoreForRebound(Vector3 worldPosition)
    {
        score += scoreForRebounds;
        UIManager.UpdateScoreGUI(scoreForRebounds, score, worldPosition);
    }

    public void AddScoreForWalls(Vector3 worldPosition)
    {
        score += scoreForWalls;
        UIManager.UpdateScoreGUI(scoreForWalls, score, worldPosition);
    }

    public void PauseGame()
    {
        if (gameIsOver) return;
        PauseGameplay();
        UIManager.ShowPause();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        ResumeGameplay();
        UIManager.ClosePopups();
        Debug.Log("Game Resumed");
    }

    public void TriggerGameOver()
    {
        UIManager.GameOver();
        Ball.Disable();
        gameIsOver = true;
        Debug.Log("Triggered Game Over");
    }

    public void ProcessResults()
    {
        UIManager.ShowResults(CurrentMission, score, score >= CurrentMission.ScoreTarget);
        Debug.Log("Results Processed");
    }

    public void ResetGame()
    {
        ResetGameWorld();
        UIManager.Reset();
        ResumeGameplay();
        Debug.Log("Game Started");
    }
    
    #endregion



    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }



    // GAME LOGIC, IMPLEM
    private void Update()
    {
        if (gameIsPaused) return;
        
        // INPUT
        padInput.Refresh();
        
        // GAME OBJECTS
        Pad.Tick(Time.deltaTime); // let the player manipulate the pad even after game over

        if (!gameIsOver)
        {
            Ball.Tick(Time.deltaTime);
        }

        // GAME RULES
        if (!targetReached && score >= CurrentMission.ScoreTarget)
        {
            Debug.Log("Success " + score + " of " + CurrentMission.ScoreTarget + "!");
            targetReached = true;
            UIManager.ShowTargetReachedFeedback();
        }


        // UI
        if (padInput.InputPressed)
        {
            UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
        }
    }


    private GameWorldBoundaries gameWorldBoundaries;
    private IPadInput padInput;
    private int score;

    private bool gameIsPaused;
    private bool gameIsOver = true;
    private int scoreForWalls = 0;
    private int scoreForRebounds = 1;
    private bool targetReached;


    private void ResetGameWorld()
    {
        score = 0;
        targetReached = false;
        gameIsOver = false;
        Pad.Reset();
        Ball.Reset();
    }

    private void PauseGameplay()
    {
        gameIsPaused = true;
        Ball.DisablePhysics();
    }

    private void ResumeGameplay()
    {
        gameIsPaused = false;
        Ball.EnablePhysics();
    }

    private void ShowTips()
    {
        UIManager.ShowTips();
    }
}
