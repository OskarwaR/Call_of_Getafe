using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioReverbZone))]
public class ReverbZoneActivator : MonoBehaviour
{
    
    private AudioReverbZone arz;
    private void Start() {
        arz = GetComponent<AudioReverbZone>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            arz.enabled=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            arz.enabled=false;
        }
    }
}
