using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(IsEnemy(collision.gameObject))
        {
            Destroy(gameObject);
            
            collision.gameObject.GetComponentInParent<Enemigo>().ReceiveDamage(damage, collision.GetContact(0).point, collision.GetContact(0).normal);
        }
    }

    private bool IsEnemy(GameObject candidate)
    {
        return (candidate.CompareTag("Enemigo"));
    }

}
