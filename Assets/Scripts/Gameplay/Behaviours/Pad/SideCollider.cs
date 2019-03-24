using UnityEngine;

public class SideCollider : MonoBehaviour
{
    public bool Collided { get; private set; }

    public void TickPhysics(float deltaTime)
    {
        Collided = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        Collided = true;
    }
}
