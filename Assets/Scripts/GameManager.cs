using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region INJECTION VIA UNITY INSPECTOR

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
        isGameOver = true;
    }

    // API USED BY INPUT EVENTS
    public void ResetGame()
    {
        ResetGameWorld();
        ResetUI();
        Debug.Log("Game Started");
    }

    // GAME LOGIC, IMPLEM
    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        Pad.Tick(Time.deltaTime);
        Ball.Tick(Time.deltaTime);

        if (padInput.InputPressed)
        {
            UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
        }
    }

    #endregion



    private GameWorldBoundaries gameWorldBoundaries;
    private IPadInput padInput;
    private int score;
    private bool isGameOver;

    private void ResetGameWorld()
    {
        score = 0;
        isGameOver = false;
        Pad.Reset();
        Ball.Reset();
    }

    private void ResetUI()
    {
        UIManager.UpdateScoreGUI(0, score, Vector3.zero);
        UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
    }
}
