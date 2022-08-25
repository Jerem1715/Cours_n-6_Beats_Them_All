using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip punchClip;

    [SerializeField] AudioSource ambientSound;
    [SerializeField] AudioSource battleSound;
    [SerializeField] AnimationCurve animCurve;

    [SerializeField] float fadeDuration = 2f;


    #region ---AmbientSound----
    public void ChangerMusique(bool battle)
    {
        StartCoroutine(battle ? FadeSound(ambientSound, battleSound) : FadeSound(battleSound, ambientSound));
    }

    IEnumerator FadeSound(AudioSource toStop, AudioSource toPlay)
    {
        toPlay.Play();

        float volume1 = toStop.volume;
        float volume2 = toPlay.volume;
        
        float t = 0;

        while (t < fadeDuration)
        {
            //Lerp : on prend le son à arreter pour l'amener progressivement à 0, et l'inverse pour le son à lancer
            toStop.volume = Mathf.Lerp(volume1, 0,animCurve.Evaluate(t / fadeDuration));
            toPlay.volume = Mathf.Lerp(volume2, 1, animCurve.Evaluate(t / fadeDuration));

            t += Time.deltaTime;
            yield return null;
        }

        //Permet d'être sur que les sons sont bien complètement arretés et lancé
        toStop.volume = 0;
        toPlay.volume = 1;

        toStop.Stop();
    }

    #endregion ---AmbientSound----

    public void PlayPunchSound()
    {
        GameObject punchSound = new GameObject("PunchSound");
        AudioSource audioSource = punchSound.AddComponent<AudioSource>();
        audioSource.clip = punchClip;
        audioSource.Play();

        Destroy(punchSound, punchClip.length); 
    }

}
