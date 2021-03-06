using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] GameObject[] armas;
    [SerializeField] ShootController shootController;
    [SerializeField] GameObject[] cross;

    public bool equipoLinterna;
    public bool equipoCuchillo;
    public bool equipoPistola;
    public bool equipoEscopeta;
    public bool equipoM16;

    public int municionPistola;
    public int municionEscopeta;
    public int municionM16;

    [SerializeField] int capacidadCargadorPistola;
    [SerializeField] int capacidadCargadorEscopeta;
    [SerializeField] int capacidadCargadorM16;

    public int cargadorPistola;
    public int cargadorEscopeta;
    public int cargadorM16;

    [SerializeField] int maxMunicionPistola;
    [SerializeField] int maxMunicionEscopeta;
    [SerializeField] int maxMunicionM16;

    public int municion;
    public int cargador;
    public int capacidadCargador;

    public int armaActual=4;
    [SerializeField] int arma = 4;
    public int armasOptenidas = 4;

    private bool scrool=false;
    [SerializeField] Text UIMunicionText;
    [SerializeField] GameObject UIMunicion;

    private void Awake()
    {
        cargadorPistola = capacidadCargadorPistola;
        cargadorEscopeta = capacidadCargadorEscopeta;
        cargadorM16 = capacidadCargadorM16;
    }
    private void Update()
    {
        Controles();
        if (armaActual != arma && armaActual>=0) CambiarArma();
        Municion();
    }

    private void Municion()
    {
        switch (armaActual)
        {
            case 1:
                municion = 1;
                cargador = 1;
                capacidadCargador = 1;
                break;
            case 2:
                municion = municionPistola;
                cargador = cargadorPistola;
                capacidadCargador = capacidadCargadorPistola;
                break;
            case 3:
                municion = municionEscopeta;
                cargador = cargadorEscopeta;
                capacidadCargador = capacidadCargadorEscopeta;
                break;
            case 4:
                municion = municionM16;
                cargador = cargadorM16;
                capacidadCargador = capacidadCargadorM16;
                break;
        }
        UIMunicionText.text = cargador + "/" + municion;
    }

    private void Controles()
    {
        if (shootController.getDisparando()) return; //No se puede cambiar de arma mientras disparas
        if (shootController.recarga) return; //No se puede cambiar de arma mientras recargas
        if (Input.GetKeyDown(KeyCode.F) && armasOptenidas == 0) armaActual = 0; //La linterna solo se puede sacar si no tenemos armas

        if (armasOptenidas == 0) return; //No se puede cambiar de arma si no tenemos armas
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

        if (armaActual > armasOptenidas) armaActual++;
        if (armaActual > 4) armaActual = 1;
        if (armaActual < 1) armaActual = 4;
        if (armaActual > armasOptenidas) armaActual--;

        foreach (GameObject mira in cross) mira.SetActive(false);
        if (armaActual==2 || armaActual == 4)
        {
            cross[1].SetActive(true);
        }
        else if(armaActual == 3)
        {
            cross[0].SetActive(true);
        }
    }

    private void CambiarArma()
    {
        foreach(GameObject arma in armas)
        {
            arma.SetActive(false);
        }
        armas[armaActual].SetActive(true);

        if (armaActual > 1) UIMunicion.SetActive(true);
        else UIMunicion.SetActive(false);
        arma = armaActual;
    }

    public int getArmaActual()
    {
        return armaActual;
    }

    private void setScrool()
    {
        scrool = false;
    }

    public void setMunicion(int n=-1)
    {
        switch (armaActual)
        {
            case 2:
                cargadorPistola += n;
                break;
            case 3:
                cargadorEscopeta += n;
                break;
            case 4:
                cargadorM16 += n;
                break;
        }
    }

    public int getMunicion()
    {
        return cargador;
    }

    public void Recargar()
    {
        int tempCarga = capacidadCargador - cargador; //comprobamos cuantas balas recargar del global
        if (tempCarga > municion) tempCarga = municion; //si tenemos que recargar mas balas del global que nos queda, recargamos el resto

        switch (armaActual)
        {
            case 2:
                municionPistola -= tempCarga;
                cargadorPistola += tempCarga;
                break;
            case 3:
                municionEscopeta -= tempCarga;
                cargadorEscopeta += tempCarga;
                break;
            case 4:
                municionM16 -= tempCarga;
                cargadorM16 += tempCarga;
                break;
        }
    }
    public void TakeMunicion(int mPistola, int mEscopeta, int mM16)
    {
        municionPistola += mPistola;
        if (municionPistola > maxMunicionPistola) municionPistola = maxMunicionPistola;
        municionEscopeta += mEscopeta;
        if (municionEscopeta > maxMunicionEscopeta) municionEscopeta = maxMunicionEscopeta;
        municionM16 += mM16;
        if (municionM16 > maxMunicionM16) municionM16 = maxMunicionM16;
    }
}
