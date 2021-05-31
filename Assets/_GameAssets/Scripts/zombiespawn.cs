using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiespawn : MonoBehaviour
{
    public GameObject Zombie;
    private int n=0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("invocar", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void invocar()
    {
        if (n >= 10) return;
        Instantiate(Zombie, transform.position, transform.rotation);
        n++;
    }
}
