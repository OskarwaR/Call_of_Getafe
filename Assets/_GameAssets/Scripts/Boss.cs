using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] NavMeshAgent nma;
    public float distanceToPlayer;
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    private Vector3 playerPosition;
    private Vector3 forward;

    //acciones
    public bool jump = false;
    public bool embestida = false;
    public bool walk = false;
    public bool spawn = false;
    public bool punch = false;
    public bool rotate = false;
    public bool dash = false;

    private Vector3 targetPoint;
    private Quaternion targetRotation;


    private void Awake()
    {

    }
    void Start()
    {
        Position();
    }
    private void Update()
    {
        if (jump) Jump();
        if (embestida) Embestida();
        if (spawn) Spawn();
        if (walk) Walk();
        if (punch) Punch();
        if (rotate) Rotate();
        if (dash) Dash();
    }

    public void Position()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        forward = transform.forward * 20;
        nma.ResetPath();
    }
    void Jump()
    {
        nma.speed = distanceToPlayer;
        nma.acceleration = 200;
        nma.SetDestination(playerPosition);
        animator.SetTrigger("Jump");
        jump = false;
    }

    void Walk()
    {
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        nma.speed = 9;
        nma.acceleration = 1000;
        nma.SetDestination(playerPosition);
        animator.SetBool("Walk", true);
        if (distanceToPlayer<=25)
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
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        nma.acceleration = 1000;
        nma.speed = 35;
        nma.SetDestination(playerPosition + forward);
        animator.SetBool("Embestida", true);
        //print(distanceToPlayer);
        if (distanceToPlayer<=30)
        {
            //print("dash");
            dash = true;
            embestida = false;
            animator.SetBool("Embestida", false);
        }
    }

    void Dash()
    {
        animator.SetBool("Dash", true);
        if (!nma.pathPending)
        {
            if (nma.remainingDistance <= nma.stoppingDistance)
            {
                if (!nma.hasPath || nma.velocity.sqrMagnitude == 0f)
                {
                    dash = false;
                    nma.ResetPath();
                    animator.SetBool("Dash", false);
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
