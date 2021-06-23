using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VistaEventos : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask layerMask;

    [SerializeField] GameObject[] zombiesZona1;
    [SerializeField] GameObject[] zombiesRestaurante1;
    [SerializeField] GameObject[] zombiesRestaurante2;
    [SerializeField] GameObject[] swatsTunel;
    [SerializeField] GameObject[] preCiudad;
    [SerializeField] GameObject[] ciudad;
    [SerializeField] GameObject[] ciudadSwats;
    void Start()
    {
        
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);
        if (Physics.Raycast(ray, out hit,5,layerMask))
        {
            target = hit.collider.gameObject;
            //print(target.name);
            switch (target.name)
            {
                case "ActivadorZombie1":
                    foreach (GameObject zombie in zombiesZona1)
                    {
                        zombie.SetActive(true);
                    }
                    break;

                case "ActivadorZombie2":
                    foreach (GameObject zombie in zombiesRestaurante1)
                    {
                        zombie.SetActive(true);
                    }
                    break;

                case "ActivadorZombie3":
                    foreach (GameObject zombie in zombiesRestaurante2)
                    {
                        zombie.SetActive(true);
                    }
                    break;
                case "ActivadorSwats":
                    foreach (GameObject swat in swatsTunel)
                    {
                        swat.SetActive(true);
                    }
                    break;
                case "PreCiudad":
                    foreach (GameObject zombie in preCiudad)
                    {
                        zombie.SetActive(true);
                    }
                    break;
                case "Ciudad":
                    foreach (GameObject zombie in ciudad)
                    {
                        zombie.SetActive(true);
                    }
                    break;
                case "CiudadSwats":
                    foreach (GameObject swat in ciudadSwats)
                    {
                        swat.SetActive(true);
                    }
                    break;
            }

            
        }
    }
}
