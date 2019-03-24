using UnityEngine;

public class Ball : MonoBehaviour
{
#pragma warning disable CS0649 // field is never assigned to
    [SerializeField]
    private BallData Data;
#pragma warning restore CS0649 // field is never assigned to

    private GameManager gameManager;
    private Transform startTransform;
    private float xMax;

    // PHYSICS
    public float Radius { get { return Data.Diameter / 2.0f; } }
    public float Mass { get { return Data.Mass; } }
    public Vector3 Acceleration { get; private set; }
    public Vector3 Velocity { get; private set; }
    private Vector3 weightForce = Vector3.zero;
    private Vector3 externalForces = Vector3.zero;
    bool physicsIsEnabled;

    public void Init(GameManager gm, GameWorldBoundaries gameWorldBoundaries, Transform startTransform)
    {
        gameManager = gm;
        this.startTransform = startTransform;
        transform.localScale = new Vector3(Data.Diameter, Data.Diameter, Data.Diameter);
        xMax = gameWorldBoundaries.ScreenRightLimit - (transform.localScale.x / 2.0f);
        PausePhysics();
    }

    public void AddForce(Vector3 force)
    {
        //force.z = 0;
        //externalForces += force;
        Velocity = force;
    }

    public void Tick(float deltaTime)
    {
#if UNITY_EDITOR // for tuning
        transform.localScale = new Vector3(Data.Diameter, Data.Diameter, Data.Diameter);
#endif
    }

    public void TickPhysics(float deltaTime)
    {
        if (!physicsIsEnabled) return;

        weightForce = Data.Mass * Physics.gravity;

        Acceleration = (weightForce + externalForces) / Data.Mass;
        Velocity = ((1 - Data.Drag) * Velocity) + (Acceleration * deltaTime);
        transform.position += Velocity * deltaTime;

        //Debug.Log("Drag vs. Accel: -" + (velocity * 0.1f).ToString("0.0") + "+ " + (acceleration * deltaTime).ToString("0.0"));
        externalForces = Vector3.zero;
        
        if (transform.position.y < 0)
        {
            gameManager.TriggerGameOver();
            return;
        }

        if (transform.position.x < -xMax && Velocity.x < 0 ||
            transform.position.x > xMax && Velocity.x > 0)
        {
            Debug.Log("Wall Hit");
            Velocity = new Vector3(-Velocity.x, Velocity.y, Velocity.z);
            gameManager.AddScoreForWalls(transform.position);
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void ResumePhysics()
    {
        physicsIsEnabled = true;

    }

    public void PausePhysics()
    {
        physicsIsEnabled = false;
    }

    public void Reset()
    {
        ResetPhysics();
        ResetTransform();
        Enable();
    }

    private void ResetTransform()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
    }

    private void ResetPhysics()
    {
        Acceleration = Vector3.zero;
        Velocity = Vector3.zero;
    }
}
