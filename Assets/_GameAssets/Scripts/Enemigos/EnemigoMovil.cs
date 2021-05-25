using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemigoMovil : Enemigo
{
    [Range(0, 360)]
    public float minAngle;
    [Range(0, 360)]
    public float maxAngle;

    public float speed;
    public float timeToRotation;
    public float distanceToExplosion;
    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("Rotate", timeToRotation, timeToRotation);
    }

    public override void Update()
    {
        base.Update();
        Attack();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public virtual void Rotate()
    {
        int determinante = Random.Range(0, 100);
        int signo = determinante > 50 ? 1 : -1; //Expresion ternaria
        transform.Rotate(0, Random.Range(minAngle, maxAngle) * signo, 0);
    }
    public override void Attack()
    {
        /*if (distanceToPlayer<=distanceToExplosion)
        {
            print("EXPLOSION");
            Instantiate(prefabPSDeath, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/
    }

    //public abstract void Move();

}
