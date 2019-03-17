using UnityEngine;

public class Ball : MonoBehaviour
{
    private WallsRebound reboundBehaviour;
    private Rigidbody rb;
    private Transform startTransform;
    private BallData data;

    private Vector3 velocityBeforePause;
    private Vector3 angularVelocityBeforePause;

    public void Init(GameManager gm, BallData data, GameWorldBoundaries gameWorldBoundaries, Transform startTransform)
    {
        this.startTransform = startTransform;
        this.data = data;
        rb = GetComponent<Rigidbody>();
        rb.mass = data.Mass;
        transform.localScale = new Vector3(data.Radius, data.Radius, data.Radius);
        reboundBehaviour = GetComponent<WallsRebound>();
        reboundBehaviour.Init(gm, gameWorldBoundaries.ScreenRightLimit);
        DisablePhysics();
    }

    public void Tick(float deltaTime)
    {
#if UNITY_EDITOR
        rb.mass = data.Mass; // for tuning
#endif
        reboundBehaviour.Tick();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void RandomisePosition()
    {
        //var pos = transform.position;
        //pos.x += Random.Range(0f, 1f) > 0.5f ? +1 : -1;
        //transform.position = pos;
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
