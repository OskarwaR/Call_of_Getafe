using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiespawn : MonoBehaviour
{
    public GameObject[] Zombies;
    [SerializeField] int max=10;
    private int n=0;
    [SerializeField] float radius=10;
    [SerializeField] float time = 10;
    [SerializeField] GameObject parent;

    void Start()
    {
        InvokeRepeating("invocar", time, time);
    }

    void invocar()
    {
        if (n == max)
        {
            Destroy(this.gameObject);
            return;
        }
            
        Vector3 rVector = new Vector3(Random.Range(0, radius), transform.position.y, Random.Range(0, radius));   
        GameObject tZombie=Instantiate(Zombies[Random.Range(0,Zombies.Length)], transform.position + (Random.insideUnitSphere* radius), transform.rotation);
        tZombie.SetActive(true);
        tZombie.transform.SetParent(parent.transform);
        n++;
    }
}
