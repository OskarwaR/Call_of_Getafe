using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    [SerializeField] int damage;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("Dash impacta");
            other.GetComponentInParent<Health>().setSalud(-damage);
            this.enabled = false;
        }
    }
}
