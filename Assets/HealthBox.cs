using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public Health health;
    public int vida;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            health.setSalud(vida);
            Destroy(this.gameObject);
            //print("player coge vida");
        }
    }
}
