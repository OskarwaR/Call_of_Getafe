using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludUpdater : MonoBehaviour
{
    Slider slider;
    Enemigo enemigo;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        enemigo = GetComponentInParent<Enemigo>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)enemigo.Salud / (float)enemigo.maxSalud;
    }
}
