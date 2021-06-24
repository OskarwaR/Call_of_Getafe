using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public Animator animator;
    public bool estado;

    private void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        //print("Abrir");

        if (other.tag == "Player")
        {
            animator.GetComponent<Animator>().SetBool("Abrir", estado);
        }
    }
}
