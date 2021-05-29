using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiespawn : MonoBehaviour
{
    public GameObject Zombie;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("invocar", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void invocar()
    {
        Instantiate(Zombie, transform.position, transform.rotation);
    }
}
