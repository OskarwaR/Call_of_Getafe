using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int salud=100;
    [SerializeField] int maxSalud = 100;
    private string zona="";

    public void setSalud(int daño,string impacto=null)
    {
        salud += daño;
        zona = impacto;
        if (salud > maxSalud) salud = maxSalud;
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
