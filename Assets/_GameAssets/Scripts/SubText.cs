using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubText : MonoBehaviour
{
    [SerializeField] SubSystem manager;
    [SerializeField] string texto;
    [SerializeField] bool alerta = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            manager.ShowSub(texto);
            Destroy(this.gameObject);
        }
    }
}
