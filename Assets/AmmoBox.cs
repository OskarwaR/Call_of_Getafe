using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public Inventario inventario;
    public int mPistola;
    public int mEscopeta;
    public int mM16;

    private void Awake()
    {
        //inventario = GetComponentInParent<Inventario>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //print("cojo municion");
            inventario.TakeMunicion(mPistola,mEscopeta,mM16);
        }
    }
}
