using UnityEngine;
using UnityEngine.UI;

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
    private Text ScoreTextGUI;
    [SerializeField]
    private HandleUI handleGUI;
    [SerializeField]
    private Transform PadStartTransform;
    [SerializeField]
    private Transform BallStartTransform;
    [SerializeField]
    private new Camera camera;
    #endregion

    private GameWorldBoundaries gameWorldBoundaries;
    private WallsRebound ball;

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
        UpdateScoreGUI();
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
            UpdatePadGUI();
        }
    }

    #endregion

    private int score;
    private IPadInput padInput;

    private void ResetGameWorld()
    {
        score = 0;
        Pad.Reset();
        Ball.Reset();
    }

    private void ResetUI()
    {
        UpdateScoreGUI();
        UpdatePadGUI();
    }

    private void UpdatePadGUI()
    {
        var padPosition = Pad.GetNormalisedXPosition();
        handleGUI.UpdatePadUIFeedback(padPosition);
    }

    private void UpdateScoreGUI()
    {
        ScoreTextGUI.text = score.ToString();
    }

}
