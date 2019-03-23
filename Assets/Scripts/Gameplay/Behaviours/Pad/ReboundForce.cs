using UnityEngine;

public class ReboundForce : MonoBehaviour
{
    private PadData data;
    private Pad pad;

    public void Init(Pad pad, PadData data)
    {
        this.pad = pad;
        this.data = data;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            pad.BallContact(col.rigidbody, col.GetContact(0).point);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
    }
}
