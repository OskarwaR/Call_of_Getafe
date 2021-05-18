using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FootStep : MonoBehaviour
{
    FirstPersonController firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("colison");
        //print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("colison");
        //print(other.gameObject.name);
    }
}
