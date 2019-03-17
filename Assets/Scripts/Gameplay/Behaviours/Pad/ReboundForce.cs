using UnityEngine;

public class ReboundForce : MonoBehaviour
{
    private PadData data;
    private GameManager gameManager;

    public void Init(GameManager gm, PadData data)
    {
        gameManager = gm;
        this.data = data;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            col.rigidbody.AddForce(transform.up * data.ReboundForce);
            gameManager.AddScoreForRebound(transform.position);
        }
    }
}
