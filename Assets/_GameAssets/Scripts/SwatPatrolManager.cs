using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatPatrolManager : MonoBehaviour
{
    //vision
    private float distanceToPlayer;
    [SerializeField] GameObject player;
    private Vector3 playerPosition;
    [SerializeField] float viewDistance = 30;
    private float view;

    //disparo
    Ray ray;
    RaycastHit hit;
    private GameObject target;
    [SerializeField] Transform shootTarget;
    [SerializeField] Transform shootPoint;

    private void Update()
    {
        Vista();
    }

    private void Vista()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        if (distanceToPlayer <= viewDistance)
        {
            var targetPosition = player.transform.position;
            targetPosition.y = transform.position.y;
            transform.parent.gameObject.transform.LookAt(targetPosition);
            //transform.LookAt(player.transform.position.y);
            ray = new Ray(shootPoint.position, playerPosition);
            Debug.DrawRay(shootPoint.position, playerPosition - shootPoint.position, Color.red);
            //Debug.DrawRay(shootPoint.position, shootTarget.position - shootPoint.position, Color.red);
            //print("te ve y dispara");
            if (Physics.Raycast(ray, out hit, viewDistance))
            {

            }
        }
    }
}
