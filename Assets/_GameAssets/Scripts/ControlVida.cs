using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ControlVida : MonoBehaviour
{
    PostProcessVolume volume;
    Vignette vignette;
    Health vida;

    // Start is called before the first frame update
    void Awake()
    {
        volume = gameObject.GetComponentInChildren<PostProcessVolume>();
        vida = GetComponent<Health>();
        volume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        float tVida = 1 - ((float)vida.getSalud() / 100);
        //vignette.intensity.value = tVida/2;

        //print(vida.getSalud());
    }
}
