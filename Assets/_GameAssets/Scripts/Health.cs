using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int salud=100;
    private string zona="";

    public void setSalud(int daño,string impacto=null)
    {
        salud += daño;
        zona = impacto;
    }

    public int getSalud()
    {
        return salud;
    }

    public string getZona()
    {
        return zona;
    }

}
