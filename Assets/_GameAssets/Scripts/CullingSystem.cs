using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingSystem : MonoBehaviour
{
    private float distanceToPlayer;
    private Transform playerPosition;
    public float cullingDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //distanceToPlayer = Vector3.Distance(GameObject.Find("LODAccidente").transform.position, playerPosition.position);
        //distanceToPlayer = (playerPosition.position - child.transform.position).sqrMagnitude;
        //distanceToPlayer = Vector2.Distance(playerPosition.position, child.transform.position);
        //print(distanceToPlayer);

        foreach (Transform child in transform)
        {
            //print(child.name);
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            distanceToPlayer = Vector3.Distance(child.transform.position, playerPosition.position);

            //distanceToPlayer = (playerPosition.position - child.transform.position).sqrMagnitude;
            //distanceToPlayer = Vector2.Distance(playerPosition.position, child.transform.position);
            //print(distanceToPlayer);
            //print(distanceToPlayer);
            if (distanceToPlayer >= 50f)
            {
                GameObject.Find(child.name).SetActive(false);
            }
            else
            {
                GameObject.Find(child.name).SetActive(true);
            }
        }
    }
}
