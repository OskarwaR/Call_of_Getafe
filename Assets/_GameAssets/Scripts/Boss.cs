using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private NavMeshAgent nma;
    private float distanceToPlayer;
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    private Vector3 playerPosition;

    //acciones
    public bool jump = false;
    public bool embestida = false;
    public bool walk = false;
    public bool spawn = false;
    public bool punch = false;
    public bool rotate = false;

    private Vector3 targetPoint;
    private Quaternion targetRotation;


    private void Awake()
    {
        nma = GetComponentInParent<NavMeshAgent>();
    }
    void Start()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
    }
    private void Update()
    {
        if (jump) Jump();
        if (embestida) Embestida();
        if (spawn) Spawn();
        if (walk) Walk();
        if (punch) Punch();
        if (rotate) Rotate();
    }

    public void Position()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        //nma.ResetPath();
    }
    void Jump()
    {
        nma.speed = distanceToPlayer;
        nma.acceleration = 200;
        animator.SetTrigger("Jump");
        nma.SetDestination(playerPosition);
        jump = false;
    }

    void Walk()
    {
        animator.SetBool("Walk", true);
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        nma.speed = 9;
        nma.acceleration = 1000;
        nma.SetDestination(playerPosition);
        if(distanceToPlayer<=20)
        {
            nma.ResetPath();
            walk = false;
            animator.SetBool("Walk", false);
            punch = true;
        }
    }

    void Punch()
    {
        animator.SetTrigger("Punch");
        punch = false;
    }

    void Embestida()
    {
        animator.SetBool("Embestida", true);
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        nma.acceleration = 1000;
        nma.speed = 35;
        nma.SetDestination(playerPosition);
        if (!nma.pathPending)
        {
            if (nma.remainingDistance <= nma.stoppingDistance)
            {
                if (!nma.hasPath || nma.velocity.sqrMagnitude == 0f)
                {
                    embestida = false;
                    nma.ResetPath();
                    animator.SetBool("Embestida", false);
                }
            }
        }
    }

    void Spawn()
    {

    }

    void Rotate()
    {
        playerPosition = player.transform.position;
        targetPoint = new Vector3(playerPosition.x, transform.position.y, playerPosition.z) - transform.position;
        targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
    }
}
