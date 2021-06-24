using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niebla : MonoBehaviour
{
    [SerializeField] float intensidad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) InvokeRepeating("Fog", 0.1f, 0.1f);
    }

    void Fog()
    {
        if (RenderSettings.fogDensity >= intensidad) RenderSettings.fogDensity -= 0.01f*Time.deltaTime;
        else Destroy(this.gameObject);
    }
}
