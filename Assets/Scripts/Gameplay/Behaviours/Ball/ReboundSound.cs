
using UnityEngine;

public class ReboundSound : MonoBehaviour
{
    public AudioSource Audio;

    public void OnCollisionEnter()
    {
        Audio.Play();
    }
}
