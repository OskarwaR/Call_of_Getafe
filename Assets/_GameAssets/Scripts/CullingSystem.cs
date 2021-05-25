using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingSystem : MonoBehaviour
{
    private float distanceToPlayer;
    private Transform playerPosition;
    public float cullingDistance;
    private GameObject o;


    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            //print(child.name);
            o=GameObject.Find(child.name);
            //print(o);
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            distanceToPlayer = Vector3.Distance(playerPosition.position, o.transform.position);
            //distanceToPlayer = (playerPosition.position - o.transform.position).sqrMagnitude;

            if (distanceToPlayer >= cullingDistance)
            {
                GameObject hijo = o.transform.GetChild(0).gameObject;
                hijo.SetActive(false);
            }
            else
            {
                GameObject hijo = o.transform.GetChild(0).gameObject;
                hijo.SetActive(true);
            }
         }

    }
}
