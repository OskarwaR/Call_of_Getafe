using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int salud=100;

    public void setSalud(int daño)
    {
        salud += daño;
    }

    public int getSalud()
    {
        return salud;
    }
}
