using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] GameObject objectToClean;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(objectToClean);
    }
}
