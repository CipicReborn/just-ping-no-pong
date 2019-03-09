using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR
    #pragma warning disable CS0649

    [SerializeField]
    private PadMover Pad;
    [SerializeField]
    private PadData PadData;
    [SerializeField]
    private Ball Ball;
    [SerializeField]
    private BallData ballData;
    [SerializeField]
    private Transform PadStartTransform;
    [SerializeField]
    private Transform BallStartTransform;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private UIManager UIManager;

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
        gameWorldBoundaries = new GameWorldBoundaries(camera);
    }

    void Start()
    {
        Pad.Init(this, padInput, PadData, gameWorldBoundaries, PadStartTransform);
        Ball.Init(this, ballData, gameWorldBoundaries, BallStartTransform);
        ResetGame();
    }
#endregion

    #region UNITY LOOP
    // API USED BY PHYSICS COMPONENTS
    public void AddScore(int scored, Vector3 worldPosition)
    {
        score += scored;
        UIManager.UpdateScoreGUI(scored, score, worldPosition);
    }

    public void TriggerGameOver()
    {
        Debug.Log("Triggered");
        UIManager.GameOver();
        Ball.Disable();
        gameIsOver = true;
    }

    // API USED BY INPUT EVENTS
    public void PauseGame()
    {
        PauseGameplay();
        UIManager.ShowPause();
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        ResumeGameplay();
        UIManager.HidePause();
        Debug.Log("Game Resumed");
    }

    public void ResetGame()
    {
        ResetGameWorld();
        ResetUI();
        ResumeGameplay();
        Debug.Log("Game Started");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    // GAME LOGIC, IMPLEM
    private void Update()
    {
        if (gameIsPaused)
        {
            return;
        }

        Pad.Tick(Time.deltaTime);

        if (!gameIsOver)
        {
            Ball.Tick(Time.deltaTime);
        }

        if (padInput.InputPressed)
        {
            UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
        }
    }

    #endregion



    private GameWorldBoundaries gameWorldBoundaries;
    private IPadInput padInput;
    private int score;

    private bool gameIsPaused;
    private bool gameIsOver;

    private void ResetGameWorld()
    {
        score = 0;
        gameIsOver = false;
        Pad.Reset();
        Ball.Reset();
    }

    private void ResetUI()
    {
        UIManager.UpdateScoreGUI(0, score, Vector3.zero);
        UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
        UIManager.ClosePopups();
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
}
