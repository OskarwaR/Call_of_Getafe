using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int salud=100;
    [SerializeField] int maxSalud = 100;
    private string zona="";

    public void setSalud(int da�o,string impacto=null)
    {
        salud += da�o;
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
