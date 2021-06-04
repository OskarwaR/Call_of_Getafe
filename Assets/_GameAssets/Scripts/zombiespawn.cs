using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiespawn : MonoBehaviour
{
    public GameObject[] Zombies;
    private int n=0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("invocar",0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void invocar()
    {
        if (n >= 10) return;
        Instantiate(Zombies[Random.Range(0,Zombies.Length)], transform.position, transform.rotation);
        n++;
    }
}
