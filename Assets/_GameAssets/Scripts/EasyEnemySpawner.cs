using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemySpawner : MonoBehaviour
{
    public GameObject[] Enemigo;
    public Transform SpawnPoint;
    [Range(1,60)]
    public float Tiempo = 1;
    [Range(1, 1000)]
    public int Max=1;

    private int n=0;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, Tiempo);
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            SpawnEnemy():
        }*/
    }

    public void SpawnEnemy()
    {
        if (n >= Max) return;
        GameObject enemigo = Instantiate(Enemigo[Random.Range(0,Enemigo.Length)], SpawnPoint.position, SpawnPoint.rotation);
        n++;
    }
}
