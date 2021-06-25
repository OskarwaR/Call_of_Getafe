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
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }
    public void setSalud(int da�o,string impacto=null,GameObject cabeza=null)
    {
        salud += da�o;
        zona = impacto;
        target = cabeza;
        if (salud > maxSalud) salud = maxSalud;
        if (da�o<0)
            if (CompareTag("Player")) soundManager.PlaySoundHurt();
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
