using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientatorVida : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private void Start()
    {
        
    }
    void Update()
    {
        transform.LookAt(target);
    }
}
