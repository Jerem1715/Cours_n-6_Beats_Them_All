using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Solo SOund")]
    [SerializeField] AudioClip punchClip;
    [SerializeField] AudioClip jumpClip;

    [Header("Global SOund")]
    [SerializeField] AudioClip ambientClip;



    public void PlayPunchSound()
    {
        GameObject punchSound = new GameObject("PunchSound");
        AudioSource audioSource = punchSound.AddComponent<AudioSource>();
        audioSource.clip = punchClip;
        audioSource.Play();

        Destroy(punchSound, punchClip.length);
    }

    public void PlayJumpSound()
    {
        GameObject jumpSound = new GameObject("JumpSound");
        AudioSource audioSource = jumpSound.AddComponent<AudioSource>();
        audioSource.clip = jumpClip;
        audioSource.Play();

        Destroy(jumpSound, jumpClip.length);
    }

    public void PlayAmbientSound()
    {
        GameObject ambientSound = new GameObject("AmbientSound");
        AudioSource audioSource = ambientSound.AddComponent<AudioSource>();
        audioSource.clip = ambientClip;
        audioSource.loop = true;
        audioSource.Play();

        Destroy(ambientSound, ambientClip.length);
    }
}
    