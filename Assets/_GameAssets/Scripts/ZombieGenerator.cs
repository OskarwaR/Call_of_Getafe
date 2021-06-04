using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] zombies;

    private void Awake()
    {
        GameObject newZombie = Instantiate(zombies[Random.Range(0, zombies.Length)],transform.position,transform.rotation);
        float randomSize = Random.Range(-0.13f, 0.13f);
        newZombie.transform.localScale=new Vector3(newZombie.transform.localScale.x+randomSize, newZombie.transform.localScale.y + randomSize, newZombie.transform.localScale.z + randomSize);
        newZombie.transform.parent = gameObject.transform;
    }

}
