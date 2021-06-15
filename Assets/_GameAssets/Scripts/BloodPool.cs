using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
    public int PoolSize; //Tama�o de la array de objetos a reciclar

    public GameObject bloodPF;  //Referencia temporal para gesti�n de llenado del array con objetos prefabs
    private GameObject[] blood; //Array de objetos a reciclar
    public int poolNumber = -1; //N�mero entero que lleva la cuenta de que
                                 //posici�n del array toca activar y gestionar

    public GameObject spawnPoint; //punto de generacion si es necesario

    void Start()
    {
        //Creamos la array con un tama�o igual al de la variable int primera
        blood = new GameObject[PoolSize];

        //De forma secuencia gracias a un bucle for creamos todos los objetos
        for (int i = 0; i < PoolSize; i++)
        {
            blood[i] = Instantiate(bloodPF, transform.position, Quaternion.identity);
        }
    }

    public void InstantiatePoolObject(Vector3 position, Quaternion rotation)
    {
        //Cada vez que disparemos el �puntero� del array aumenta en uno para que
        //en el siguiente objeto se�ale al siguiente objeto del array.
        poolNumber++;
        //En el caso de que el puntero supere el n�mero de posiciones del array
        //vuelve a 0 para seguir con el proceso.
        if (poolNumber > PoolSize - 1)
        {
            poolNumber = 0;
        }
        //Ponemos el objeto, desactivado a�n, en la posici�n del objeto �manager�
        blood[poolNumber].transform.position = position;
        blood[poolNumber].transform.rotation = rotation;
        blood[poolNumber].transform.localScale = new Vector3(3, 3, 3);
        //�Activamos el objeto!
        blood[poolNumber].SetActive(true);
        blood[poolNumber].GetComponent<ParticleSystem>().Clear();
        blood[poolNumber].GetComponent<ParticleSystem>().time = 0;
        blood[poolNumber].GetComponent<ParticleSystem>().Play();
    }
}
