using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int salud=100;

    public void setSalud(int n)
    {
        salud += n;
        if(salud<=0)
        {
            //muerte
        }
    }
}
