using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManagerInstance;

    [SerializeField] AudioClip[] hurt;
    private AudioSource reproductor;
    private AudioSource[] audios;
    private float pitchInincial;

    /* Armas
        0-Escopeta
        1-M16
        2-Pistola
        3-Cuchillo
    */

    //4 - Hurt

    /*Boss
        0 - Rugido
        1-4 - Pisadas
        5 - Dash
    */ 

    private void Awake()
    {
        soundManagerInstance = this;
        audios = GetComponentsInParent<AudioSource>();
    }

    /// <summary>
    /// Control de sonidos
    /// </summary>
    /// <param name="nSound">Numero del sonido en orden de aparicion en el objeto</param>
    /// <param name="pitch">Modificar el pitch, true por defecto</param>
    /// <param name="pitchMin">Min pitch</param>
    /// <param name="pitchMax">Max pitch</param>
    public void PlaySound(int nSound,bool pitch=true,float pitchMin=0.8f, float pitchMax=1.2f)
    {
        reproductor = audios[nSound];
        pitchInincial = reproductor.pitch;
        if (pitch) reproductor.pitch = Random.Range(pitchMin, pitchMax);
        reproductor.PlayOneShot(reproductor.clip);
    }

    public void PlaySoundHurt()
    {
        reproductor = audios[4];
        pitchInincial = reproductor.pitch;
        reproductor.pitch = Random.Range(0.9f, 1.1f);
        reproductor.PlayOneShot(hurt[Random.Range(0,hurt.Length)]);
    }
}
