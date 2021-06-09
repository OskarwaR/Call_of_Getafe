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
    [SerializeField] LayerMask layerMask;

    bool gatillo=false;
    //GameObject shootTarget;
    private void Awake()
    {
        //shootTarget=GameObject.Find("/FPSController/FirstPersonCharacter/");
    }
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
            ray = new Ray(shootPoint.position, shootTarget.transform.position - shootPoint.position);
            Debug.DrawRay(shootPoint.position, shootTarget.transform.position - shootPoint.position, Color.red);
            //Debug.DrawRay(shootPoint.position, shootTarget.position - shootPoint.position, Color.red);
            //print("te ve y dispara");
            if (Physics.Raycast(ray, out hit, viewDistance, layerMask))
            {
                target = hit.collider.gameObject;
                if (target.CompareTag("Player"))
                {
                    if (!gatillo)
                    {
                        print("dispara");
                        float shoot = Random.Range(0, 100);
                        if (shoot > 50)
                        {
                            print("impacto");
                        }
                        gatillo = true;
                        Invoke("Cadencia", 0.25f);
                    }
                    
                }
                    
            }
        }
    }

    void Cadencia()
    {
        gatillo = false;
    }
}
