using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [SerializeField] int damage;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("salto impacta");
            other.GetComponentInParent<Health>().setSalud(-damage);
            this.enabled = false;
        }
    }

}
