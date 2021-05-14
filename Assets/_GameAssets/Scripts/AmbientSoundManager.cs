using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(AudioSource))]
public class AmbientSoundManager : MonoBehaviour
{    
    [SerializeField]
    [Range(0,10)]
    [Tooltip("Duración de la atenuación en segundos")]
    float fadeTime;//Tiempo de duración de la atenuación

    private float initialVolume;//Volumen inicial
    private float delta;
    private float fadePause;//Tiempo de espera entre incrementos/decrementos de volumen
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        delta = initialVolume / 100f;
        fadePause = fadeTime / 100f;
        audioSource.volume=0;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            PlaySound();
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            StopSound();
        }
    }
    void PlaySound(){
        StopAllCoroutines();
        if (!audioSource.isPlaying){
            audioSource.Play();
        }
        StartCoroutine("FadeIn");
    }
    void StopSound() {
        StopAllCoroutines();
        StartCoroutine("FadeOut");
    }
    IEnumerator FadeIn() 
    {
        for(int i=0;i<100;i++) {
            audioSource.volume+=delta;
            audioSource.volume=Mathf.Min(audioSource.volume, initialVolume);
            yield return new WaitForSeconds(fadeTime/100);
        }
        audioSource.volume = initialVolume;
    }
    IEnumerator FadeOut() 
    {
        for(int i=0;i<100;i++) {
            audioSource.volume-=delta;
            audioSource.volume=Mathf.Max(audioSource.volume, 0);
            yield return new WaitForSeconds(fadeTime/100);
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}
