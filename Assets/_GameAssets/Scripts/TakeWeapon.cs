using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    [SerializeField] Inventario inventario;
    [SerializeField] bool cuchillo=false;
    [SerializeField] bool pistola = false;
    [SerializeField] bool escopeta = false;
    [SerializeField] bool m16 = false;
    [SerializeField] int armaActual=0;
    [SerializeField] int numArmas = 0;
    [SerializeField] GameObject armaPF;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cuchillo) inventario.equipoCuchillo = true;
            if (pistola) inventario.equipoPistola = true;
            if (escopeta) inventario.equipoEscopeta = true;
            if (m16) inventario.equipoM16 = true;

            inventario.armaActual = armaActual;
            inventario.armasOptenidas += numArmas;

            Destroy(armaPF);
        }
    }
}
