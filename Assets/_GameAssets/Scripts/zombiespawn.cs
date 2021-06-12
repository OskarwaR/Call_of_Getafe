using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiespawn : MonoBehaviour
{
    public GameObject[] Zombies;
    private int n=0;
    [SerializeField] float radius=15;
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
        Vector3 rVector = new Vector3(Random.Range(0, radius), transform.position.y, Random.Range(0, radius));
        if (n >= 10) return;
        Instantiate(Zombies[Random.Range(0,Zombies.Length)], transform.position + (Random.insideUnitSphere* radius), transform.rotation);
        n++;
    }
}
