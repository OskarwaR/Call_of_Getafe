using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smart : EnemigoMovil
{
    [Range(1,20)]
    public float followDistance;


    // Update is called once per frame
    public override void Update()
    {
        base.Update(); //ejecuta el update del padre
        if(distanceToPlayer <= followDistance)
        {
            transform.LookAt(player.transform.position);
        }
        Move();
    }

    public override void Rotate()
    {
        if (distanceToPlayer <= followDistance) return;
        base.Rotate();
    }
}
