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
        if(col.gameObject.name == "Ball")
        {
            col.rigidbody.AddForce(Vector3.up * data.ReboundForce);
            gameManager.AddOneToScore();
        }
    }
}
