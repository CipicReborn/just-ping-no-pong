using UnityEngine;

public class WallsRebound : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody rb;
    float xMax;

    public void Init(GameManager gm, float screenRightLimit)
    {
        gameManager = gm;
        xMax = screenRightLimit - (transform.localScale.x / 2.0f);
        rb = GetComponent<Rigidbody>();
        //Debug.Log("Walls Rebound Initialised");
        //Debug.Log("xMax :" + xMax);
        //Debug.Log("Rigidbody : " + rb);
    }

    public void Tick()
    {
        if (transform.position.y < 0)
        {
            gameManager.TriggerGameOver();
            return;
        }

        //Debug.Log(transform.position.x + ", " + rb.velocity.x);
        if (transform.position.x < -xMax && rb.velocity.x < 0 ||
            transform.position.x > xMax && rb.velocity.x > 0)
        {
            //Debug.Log("Wall Hit");
            rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
            gameManager.AddScoreForWalls(transform.position);
        }
    }
}
