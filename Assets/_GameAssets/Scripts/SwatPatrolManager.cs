using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwatPatrolManager : MonoBehaviour
{
    //vision
    private float distanceToPlayer;
    [SerializeField] GameObject player;
    private Vector3 playerPosition;
    [SerializeField] float viewDistance = 30;
    [SerializeField] float shootDistance = 30;
    private float view;
    bool detect = false;

    //disparo
    Ray ray;
    RaycastHit hit;
    private GameObject target;
    [SerializeField] Transform shootTarget;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask layerMask;
    private bool gatillo = false;
    private Health playerSalud;
    [SerializeField] int rifleDMG = 10;
    [SerializeField] int impactChance = 80;
    [SerializeField] GameObject DisparoPF;
    private AudioSource audioM16;
    [SerializeField]  int municion=30;
    int maxMunicion;
    bool recargando = false;


    //retirada
    [SerializeField] float distanciaRetirada=15;
    private NavMeshAgent nma;
    private Animator agentAnimator;
    bool retirada = false;
    private Transform PuntoRetirada;

    //vida
    private Health salud;
    bool alive = true;

    //GameObject shootTarget;
    private void Awake()
    {
        PuntoRetirada = this.gameObject.transform.GetChild(2);
        nma = GetComponentInParent<NavMeshAgent>();
        agentAnimator = GetComponentInChildren<Animator>();
        playerSalud = player.GetComponent<Health>();
        salud = GetComponentInParent<Health>();
        AudioSource[] audios = GetComponentsInParent<AudioSource>();
        audioM16 = audios[0];
        maxMunicion = municion;
    }
    private void Update()
    {
        

        Vida();
        if (!alive) return;

        playerPosition = player.transform.position;
        distanceToPlayer = Vector3.Distance(playerPosition, transform.position);

        Vista();
        Retirada();

    }

    private void Vida()
    {
        if (!alive) return;
        //print(zombieImpacto);
        if (salud.getSalud() <= 0)
        {
            agentAnimator.SetBool("Death", true);
            nma.ResetPath();
            nma.enabled = false;
            //audioLoop.Stop();
            alive = false;
            GetComponentInParent<CapsuleCollider>().enabled = false;
            //GetComponentInChildren<CapsuleCollider>().enabled = false;
            //print(zombieImpacto);
            /*if (zombieImpacto == "Head")
            {

                string path = "/" + cabezaPath.name;
                while (cabezaPath.transform.parent != null)
                {
                    cabezaPath = cabezaPath.transform.parent.gameObject;
                    path = "/" + cabezaPath.name + path;
                }

                GameObject cabeza = transform.Find(path).gameObject;
                GameObject gore = Instantiate(pfExplosionCabeza, cabeza.transform.position, cabeza.transform.rotation);
                gore.transform.localScale = new Vector3(3, 3, 3);
                cabeza.transform.localScale = new Vector3(0, 0, 0);
                //print(path);
            }*/
        }
    }

    private void Retirada()
    {
        //if (recargando) return;
        if (distanceToPlayer <= distanciaRetirada)
        {
            if (!retirada)
            {
                nma.SetDestination(RandomNavmeshLocation(10));
                retirada = true;
                agentAnimator.SetBool("Retirada", true);
            }
        }

        if (!nma.pathPending)
        {
            if (nma.remainingDistance <= nma.stoppingDistance)
            {
                if (!nma.hasPath || nma.velocity.sqrMagnitude == 0f)
                {
                    retirada = false;
                    nma.ResetPath();
                    agentAnimator.SetBool("Retirada", false);
                    //Invoke("Andar", Random.Range(2, 10));
                }
            }
        }

        if(retirada&&recargando)
        {
            //retirada = false;
            retirada = false;
            nma.ResetPath();
            //agentAnimator.SetBool("Retirada", false);
        }
            
    }

    private void Vista()
    {
        if (distanceToPlayer <= viewDistance)
        {
            viewDistance = 1000;
            var targetPosition = player.transform.position;
            targetPosition.y = transform.position.y;
            transform.parent.gameObject.transform.LookAt(targetPosition);
            //transform.LookAt(player.transform.position.y);
            
            //Debug.DrawRay(shootPoint.position, shootTarget.transform.position - shootPoint.position, Color.red);
            //Debug.DrawRay(shootPoint.position, shootTarget.position - shootPoint.position, Color.red);
            //print("te ve y dispara");
            if (distanceToPlayer <=  shootDistance)
            {
                ray = new Ray(shootPoint.position, shootTarget.transform.position - shootPoint.position);
                agentAnimator.SetBool("Detect", true);
                if (Physics.Raycast(ray, out hit, viewDistance, layerMask))
                {
                    target = hit.collider.gameObject;
                    if (target.CompareTag("Player"))
                    {
                        detect = true;
                        if (!gatillo)
                        {
                            //print("dispara");
                            float shoot = Random.Range(0, 100);
                            if (shoot > impactChance)
                            {
                                //print("impacto");
                                playerSalud.setSalud(-rifleDMG);
                            }
                            Instantiate(DisparoPF, shootPoint.position, shootPoint.rotation).transform.localScale=new Vector3(1.5f, 1.5f, 1.5f);
                            audioM16.pitch = Random.Range(0.8f, 1.2f);
                            audioM16.PlayOneShot(audioM16.clip);
                            gatillo = true;
                            Invoke("Cadencia", Random.Range(0.10f, 0.30f));
                            municion -= 1;
                            if (municion <= 0) Recargar();
                        }

                    }
                    else
                    {
                        if (!gatillo && detect)
                        {
                            float rRandomDisparo = Random.Range(0,100);
                            if (rRandomDisparo>=98)
                            {
                                Instantiate(DisparoPF, shootPoint.position, shootPoint.rotation).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                                audioM16.pitch = Random.Range(0.8f, 1.2f);
                                audioM16.PlayOneShot(audioM16.clip);
                                gatillo = true;
                                Invoke("Cadencia", Random.Range(0.10f, 0.30f));
                                municion -= 1;
                                if (municion <= 0) Recargar();
                            }
                        }
                    }

                }
            }
            
        }
    }

    void Cadencia()
    {
        if(!recargando) gatillo = false;
    }

    void Recargar()
    {
        gatillo = true;
        recargando = true;
        agentAnimator.SetBool("Reload", true);
    }

    public void EndRecargar()
    {
        gatillo = false;
        recargando = false;
        municion = maxMunicion;
        agentAnimator.SetBool("Reload", false);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += PuntoRetirada.position;
        NavMeshHit hit;
        //Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            //finalPosition = hit.position;
        }
        return randomDirection;
    }

    public void Hit()
    {
        /*alerta = true;
        audioHitZombie.pitch = Random.Range(0.8f, 1.2f);
        audioHitZombie.PlayOneShot(audioHitZombie.clip);
        Andar();*/
    }
}
