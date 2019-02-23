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
        Ball.Init(ballData, gameWorldBoundaries, BallStartTransform);
        ResetGame();
    }
#endregion

    #region UNITY LOOP
    // API USED BY PHYSICS COMPONENTS
    public void AddOneToScore()
    {
        score++;
        UIManager.UpdateScoreGUI(score);
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

    private void ResetGameWorld()
    {
        score = 0;
        Pad.Reset();
        Ball.Reset();
    }

    private void ResetUI()
    {
        UIManager.UpdateScoreGUI(score);
        UIManager.UpdatePadGUI(Pad.GetNormalisedXPosition());
    }
}
