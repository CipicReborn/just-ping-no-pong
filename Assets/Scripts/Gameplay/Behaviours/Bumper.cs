using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    Ball ball;
    Vector3 tmpVelocity;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Bump");
            ball = col.gameObject.GetComponent<Ball>();
            tmpVelocity = ball.Velocity;
            tmpVelocity.x *= -1;
            tmpVelocity.y *= -1;
            ball.AddForce(tmpVelocity);
        }
    }
}
