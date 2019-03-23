using UnityEngine;

public class Ball : MonoBehaviour
{
    private WallsRebound reboundBehaviour;
    private Rigidbody rb;
    private Transform startTransform;
    [SerializeField]
    private BallData Data;

    private Vector3 velocityBeforePause;
    private Vector3 angularVelocityBeforePause;

    public void Init(GameManager gm, GameWorldBoundaries gameWorldBoundaries, Transform startTransform)
    {
        this.startTransform = startTransform;
        rb = GetComponent<Rigidbody>();
        rb.mass = Data.Mass;
        transform.localScale = new Vector3(Data.Radius, Data.Radius, Data.Radius);
        reboundBehaviour = GetComponent<WallsRebound>();
        reboundBehaviour.Init(gm, gameWorldBoundaries.ScreenRightLimit);
        DisablePhysics();
    }

    public void Tick(float deltaTime)
    {
#if UNITY_EDITOR // for tuning
        rb.mass = Data.Mass;
        rb.drag = Data.Drag;
        transform.localScale = new Vector3(Data.Radius, Data.Radius, Data.Radius);
#endif
        reboundBehaviour.Tick();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        rb.velocity = velocityBeforePause;
        rb.angularVelocity = angularVelocityBeforePause;
    }
    public void DisablePhysics()
    {
        velocityBeforePause = rb.velocity;
        angularVelocityBeforePause = rb.angularVelocity;
        rb.isKinematic = true;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        ResetPhysics();
        ResetTransform();
    }


    private void ResetTransform()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
    }

    private void ResetPhysics()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        velocityBeforePause = Vector3.zero;
        angularVelocityBeforePause = Vector3.zero;
    }
}
