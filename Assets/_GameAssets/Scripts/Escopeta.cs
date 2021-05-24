using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escopeta: MonoBehaviour
{
    public GameObject escopeta;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            escopeta.SetActive(!escopeta.activeSelf);
        }
    }
}

