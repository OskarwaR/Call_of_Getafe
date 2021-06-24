using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToClean;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
            foreach(GameObject obj in objectsToClean) 
                Destroy(obj);
    }
}
