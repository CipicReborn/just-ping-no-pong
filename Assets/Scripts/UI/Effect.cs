using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Effect : MonoBehaviour
{
#region INJECTION VIA UNITY INSPECTOR
#pragma warning disable CS0649 // Disable warning "variable never initialized"
#pragma warning disable IDE0044 // Disable recommendation "Add readonly modifier"

    [SerializeField]
    private string AnimationName;

#pragma warning restore IDE0044
#pragma warning restore CS0649
#endregion


    private Animator animator;
    private AudioSource audioSource;
    private int animHash;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animHash = Animator.StringToHash(AnimationName);
    }

    public void Play()
    {
        animator.Play(animHash);
        if (audioSource.clip != null)
            audioSource.Play();
    }
}
