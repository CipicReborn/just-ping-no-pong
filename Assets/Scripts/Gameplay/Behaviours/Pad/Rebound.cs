using UnityEngine;

public class Rebound : MonoBehaviour
{
    [SerializeField]
    private SideCollider TopFace;
    [SerializeField]
    private SideCollider DownFace;
    [SerializeField]
    private SideCollider LeftFace;
    [SerializeField]
    private SideCollider RightFace;

    private PadData data;
    private Pad pad;

    public void Init(Pad pad, PadData data)
    {
        this.pad = pad;
        this.data = data;
    }

    public Transform GetSideTransform(Ball ball)
    {
        Vector3 ballLocalPosition = transform.InverseTransformDirection(ball.transform.position) - transform.localPosition;
        //Debug.Log("Transform Pos : " + transform.position);
        //Debug.Log("Transform Local Pos : " + transform.localPosition);
        //Debug.Log("Ball Local Pos : " + ballLocalPosition);
        //Debug.Log("Ball World Pos : " + ball.transform.position);
        var d = ball.transform.position - transform.position;
        var d2 = Vector3.Dot(d, transform.right);
        //Debug.Log("d2 : " + d2);
        //if (LeftFace.Collided && d2 < (-0.5f - (ball.Radius * 0.707f)))
        //{
        //    Debug.Log("Left"); return LeftFace.transform;
        //}
        //if (RightFace.Collided && d2 > (0.5f + (ball.Radius * 0.707f)))
        //{
        //    Debug.Log("Right"); return RightFace.transform;
        //}
        //if (TopFace.Collided) { Debug.Log("Top"); return TopFace.transform; }
        //if (DownFace.Collided) { Debug.Log("Down"); return DownFace.transform; }
        return TopFace.transform;
    }

    public void TickPhysics(float deltaTime)
    {
        TopFace.TickPhysics(deltaTime);
        DownFace.TickPhysics(deltaTime);
        LeftFace.TickPhysics(deltaTime);
        RightFace.TickPhysics(deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            pad.BallContact(col.gameObject.GetComponent<Ball>(), col.GetContact(0).point);
        }
    }
}
