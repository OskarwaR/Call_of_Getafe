using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight: MonoBehaviour
{
    public GameObject torchLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            torchLight.SetActive(!torchLight.activeSelf);
        }
    }
}

