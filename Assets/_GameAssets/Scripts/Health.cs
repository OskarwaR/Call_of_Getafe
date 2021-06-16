using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int salud=100;
    [SerializeField] int maxSalud = 100;
    private string zona="";
    GameObject target;

    public void setSalud(int daño,string impacto=null,GameObject cabeza=null)
    {
        salud += daño;
        zona = impacto;
        target = cabeza;
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

    public GameObject getPath()
    {
        return target;
    }

}
