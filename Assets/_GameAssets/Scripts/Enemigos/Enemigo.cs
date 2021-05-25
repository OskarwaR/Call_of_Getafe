using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    public int maxSalud;
    public int Salud;
    public GameObject prefabPSDamage;
    public GameObject prefabPSDeath;

    public float distanceToPlayer;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Salud = maxSalud;
    }

    public virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        

    }

    /// <summary>
    /// Determina si ve al player
    /// </summary>
    /// <returns></returns>
    public bool PlayerDetected()
    {
        //TODO esta viendo al player
        return true;
    }
    /// <summary>
    /// inflinge un daño al enemigo
    /// </summary>
    public void ReceiveDamage(int damage,Vector3 impacto,Vector3 rotacion)
    {
        //TODO sistema de particulas, emitir un sonido, quitar salud, comprobar si ha muerto
        Salud -= damage;
        GameObject sangre = Instantiate(prefabPSDamage, impacto, Quaternion.LookRotation(rotacion));
        sangre.transform.localScale = new Vector3(5, 5, 5);
        sangre.transform.SetParent(transform);
        
        if (Salud<=0)
        {
            death();
        }
    }

    /// <summary>
    /// Mata al enemigo
    /// </summary>
    public void death()
    {
        //TODO sistema de particulas, emitir sonido, destruir objeto
        Destroy(gameObject);
        Instantiate(prefabPSDeath, transform.position, transform.rotation);
    }

    /// <summary>
    /// Ataque del enmigo
    /// </summary>
    public abstract void Attack();

}
