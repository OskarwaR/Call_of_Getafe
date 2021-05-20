using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{
    public int Salud;
    public GameObject prefabPSDamage;
    public GameObject prefabPSDeath;
    public float distanceToPlayer;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    public void ReceiveDamage()
    {
        //TODO sistema de particulas, emitir un sonido, quitar salud, comprobar si ha muerto
    }

    /// <summary>
    /// Mata al enemigo
    /// </summary>
    public void death()
    {
        //TODO sistema de particulas, emitir sonido, destruir objeto
    }

    /// <summary>
    /// Ataque del enmigo
    /// </summary>
    public abstract void Attack();

}
