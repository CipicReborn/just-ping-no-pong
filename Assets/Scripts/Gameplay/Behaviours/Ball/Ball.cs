using UnityEngine;

public class Ball : MonoBehaviour
{
    private WallsRebound reboundBehaviour;
    private Rigidbody rb;
    private Transform startTransform;
    private BallData data;

    public void Init(GameManager gm, BallData data, GameWorldBoundaries gameWorldBoundaries, Transform startTransform)
    {
        this.startTransform = startTransform;
        this.data = data;
        rb = GetComponent<Rigidbody>();
        rb.mass = data.Mass;
        transform.localScale = new Vector3(data.Radius, data.Radius, data.Radius);
        reboundBehaviour = GetComponent<WallsRebound>();
        reboundBehaviour.Init(gm, gameWorldBoundaries.ScreenRightLimit);
    }

    public void Tick(float deltaTime)
    {
        rb.mass = data.Mass; // for tuning
        reboundBehaviour.Tick();
    }

    public void Reset()
    {
        ResetTransform();
        ResetPhysics();
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
    }
}
