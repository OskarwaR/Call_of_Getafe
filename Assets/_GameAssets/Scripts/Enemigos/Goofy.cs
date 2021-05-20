using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goofy : EnemigoMovil
{
    
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Move();
    }
}
