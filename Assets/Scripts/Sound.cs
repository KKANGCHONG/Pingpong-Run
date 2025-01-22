using UnityEngine;

public class AnimationSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    public AudioClip jumpClip;
    public AudioClip hitClip;

    public void PlayJumpSound()
    {
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }

    public void PlayHitSound()
    {
        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }
    }
}