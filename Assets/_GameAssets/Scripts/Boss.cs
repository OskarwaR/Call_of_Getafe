using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class Boss : MonoBehaviour
{
    [SerializeField] NavMeshAgent nma;
    public float distanceToPlayer;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mesh;
    [SerializeField] Animator animator;
    [SerializeField] Health salud;
    [SerializeField] SoundManager soundManager;
    private Vector3 playerPosition;
    private Vector3 forward;
    [SerializeField] GameObject helicoptero;
    [SerializeField] GameObject cartelEnd;
    [SerializeField] ShootController shootController;
    [SerializeField] Inventario inventario;
    [SerializeField] CharacterController characterController;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

    //acciones
    public bool muerto = false;
    public bool jump = false;
    public bool embestida = false;
    public bool walk = false;
    public bool spawn = false;
    public bool punch = false;
    public bool rotate = false;
    public bool dash = false;

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    private NavMeshPath path;

    //Ragdoll
    Collider[] colliders;
    Rigidbody[] rigidbodys;
    Rigidbody rg;


    private void Awake()
    {
        path = new NavMeshPath();
        //ragdoll
        colliders = GetComponentsInChildren<Collider>();
        rigidbodys = GetComponentsInChildren<Rigidbody>();
    }
    void Start()
    {
        //Position();
        mesh.SetActive(true);
    }
    private void Update()
    {
        Vida();
        if (muerto) return;
        if (jump) Jump();
        if (embestida) Embestida();
        if (spawn) Spawn();
        if (walk) Walk();
        if (punch) Punch();
        if (rotate) Rotate();
        if (dash) Dash();
    }

    void Vida()
    {
        if (salud.salud <= 0 && !muerto)
        {
            soundManager.PlaySound(9);
            StartCoroutine(Final());
        }
            

        if (salud.salud<=0) 
        {
            muerto = true;
            nma.enabled = false;
            animator.SetBool("Muerto", true);
            float tvolumen = soundManager.audios[10].volume;
            soundManager.audios[10].volume = Mathf.Lerp(tvolumen, 0, Time.deltaTime/2);
            //audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            //foreach (Rigidbody rig in rigidbodys) rig.isKinematic = false;
            //animator.enabled = false;

        }
    }

    public void Position()
    {
        nma.speed = 0;
        nma.acceleration = 0;
        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);
        forward = transform.forward * 20;
        nma.SetDestination(playerPosition);
    }
    void Jump()
    {
        nma.speed = distanceToPlayer;
        nma.acceleration = 100;
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

    IEnumerator Final()
    {
        yield return new WaitForSeconds(10);
        helicoptero.SetActive(true);

        yield return new WaitForSeconds(45);
        cartelEnd.SetActive(true);
        shootController.enabled = false;
        inventario.enabled = false;
        characterController.enabled = false;
        firstPersonController.enabled = false;

        yield return new WaitForSeconds(25);
        SceneManager.LoadScene(0);
    }
}
