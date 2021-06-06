using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] GameObject[] armas;

    [SerializeField] bool equipoLinterna;
    [SerializeField] bool equipoCuchillo;
    [SerializeField] bool equipoPistola;
    [SerializeField] bool equipoEscopeta;
    [SerializeField] bool equipoM16;

    [SerializeField] int municionPistola;
    [SerializeField] int municionEscopeta;
    [SerializeField] int municionM16;

    [SerializeField] int cargadorPistola;
    [SerializeField] int cargadorEscopeta;
    [SerializeField] int cargadorM16;

    [SerializeField] int maxMunicionPistola;
    [SerializeField] int maxMunicionEscopeta;
    [SerializeField] int maxMunicionM16;

    [SerializeField] int armaActual=4;
    [SerializeField] int arma = 4;

    private bool scrool=false;

    private void Awake()
    {
        arma = armaActual;
    }
    private void Update()
    {
        Controles();
        if (armaActual != arma) CambiarArma();
    }

    private void Controles()
    {
        if (GetComponentInParent<ShootController>().getDisparando()) return; //No se puede cambiar de arma mientras disparas

        if (Input.GetKeyDown(KeyCode.Alpha1) && equipoCuchillo)
        {
            armaActual = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && equipoPistola)
        {
            armaActual = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && equipoEscopeta)
        {
            armaActual = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && equipoM16)
        {
            armaActual = 4;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && !scrool) // forward
        {
            armaActual++;
            scrool = true;
            Invoke("setScrool", 0.5f);

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && !scrool) // backwards
        {
            armaActual--;
            scrool = true;
            Invoke("setScrool", 0.5f);
        }

        if (armaActual > 4) armaActual = 1;
        if (armaActual < 1) armaActual = 4;
    }

    private void CambiarArma()
    {
        arma = armaActual;
        foreach(GameObject arma in armas)
        {
            arma.SetActive(false);
        }
        armas[armaActual].SetActive(true);
    }

    public int getArmaActual()
    {
        return armaActual;
    }

    private void setScrool()
    {
        scrool = false;
    }
}
