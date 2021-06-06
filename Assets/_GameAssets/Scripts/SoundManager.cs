using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource reproductor;
    private AudioSource[] audios;
    private float pitchInincial;

    /*
        0-Escopeta
        1-M16
        2-Pistola    
    */

    private void Awake()
    {
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
}
