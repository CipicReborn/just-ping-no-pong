using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboundSound : MonoBehaviour
{
    public AudioSource Audio;

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
    public void OnCollisionEnter()
    {
        Audio.Play();
    }
}
