using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject Target;

    public GameObject bloodPool;

    private int arma;

    [SerializeField] int damagePistola;
    [SerializeField] int damageEscopeta;
    [SerializeField] int damageCuchillo;
    [SerializeField] int damageM16;
    private int damage;

    [SerializeField] float distanciaPistola = 50;
    [SerializeField] float distanciaEscopeta = 25;
    [SerializeField] float distanciaCuchillo = 1;
    [SerializeField] float distanciaM16 = 75;
    private float distancia;

    [SerializeField] GameObject sangreImpacto;
    [SerializeField] float sangreImpactoSize;

    [SerializeField] GameObject player;
    private GameObject brazos;

    private GameObject explosionPoint;
    [SerializeField] GameObject explosionPointEscopeta;
    [SerializeField] GameObject explosionPointM16;
    [SerializeField] GameObject explosionPointPistola;
    private GameObject explosionPointPF;
    [SerializeField] GameObject explosionPointPFEscopeta;
    [SerializeField] GameObject explosionPointPFM16;
    [SerializeField] GameObject explosionPointPFPistola;

    [SerializeField] LayerMask layerMask;
    [SerializeField] SoundManager soundManager;

    public bool disparando = false;
    public bool gatillo = false;
    public bool recarga = false;
    private Inventario inventario;

    void Start()
    {
        //Inicializamos el rayo
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        inventario = GetComponentInParent<Inventario>();
        brazos = GameObject.FindGameObjectWithTag("Brazos");

        //Comprobamos el arma actual
        arma = inventario.getArmaActual();
    }

    void Update()
    {
        Disparo();
        Recarga();
    }

    private void Recarga()
    {
        if (Input.GetKeyDown(KeyCode.R) && !recarga)
        {
            if (inventario.cargador == inventario.capacidadCargador) return; //no se puede recargar con el cagador lleno
            if (inventario.municion<=0) return; //no se puede recargar sin balas
            if (inventario.getArmaActual() == 1 || inventario.getArmaActual() == 0) return; //no se puede recargar con el cuchillo o la linterna
            recarga = true;
            brazos = GameObject.FindGameObjectWithTag("Brazos");
            brazos.GetComponent<Animator>().SetTrigger("Recarga");
            inventario.Recargar();
        }
    }

    private void Disparo()
    {
        if (inventario.getArmaActual() <= 0) return; //No se puede disparar sin arma
        if (recarga) return; //No se puede disparar mientras recargas

        if (Input.GetMouseButtonDown(0) || gatillo)
        {
            if (!disparando && inventario.getMunicion() > 0)
            {
                arma = inventario.getArmaActual();
                //Debug.Log("MouseDown: " + arma);

                //Comprobamos el arma equipada
                brazos = GameObject.FindGameObjectWithTag("Brazos");
                brazos.GetComponent<Animator>().SetTrigger("Disparo");
                switch (arma)
                {
                    //cuchillo
                    case 1:
                        soundManager.PlaySound(3);
                        distancia = distanciaCuchillo;
                        explosionPoint = null;
                        explosionPointPF = null;
                        damage = damageCuchillo;
                        break;
                    //pistola
                    case 2:
                        soundManager.PlaySound(2, true, 0.7f, 1.1f);
                        distancia = distanciaPistola;
                        explosionPoint = explosionPointPistola;
                        explosionPointPF = explosionPointPFPistola;
                        damage = damageM16;
                        break;
                    //escopeta
                    case 3:
                        soundManager.PlaySound(0);
                        distancia = distanciaEscopeta;
                        explosionPoint = explosionPointEscopeta;
                        explosionPointPF = explosionPointPFEscopeta;
                        damage = damageEscopeta;
                        break;
                    //m16
                    case 4:
                        soundManager.PlaySound(1);
                        distancia = distanciaM16;
                        explosionPoint = explosionPointM16;
                        explosionPointPF = explosionPointPFM16;
                        damage = damageM16;
                        break;
                }

                if (arma==3)
                {
                    float variance = 150.0f;  // This much variance 
                    float distance = 10.0f; // at this distance

                    for (var i = 0; i < 30; i++)
                    {
                        Vector3 v3Offset = transform.up * Random.Range(0.0f, variance);
                        v3Offset = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), transform.forward) * v3Offset;
                        Vector3 v3Hit = transform.forward * distance + v3Offset;

                        ray = Camera.main.ScreenPointToRay(Input.mousePosition + v3Hit);
                        //Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
                        RayImpact();
                    }
                }
                else
                {
                    // Reset ray with new mouse position
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
                    RayImpact();
                }

                //fogonazo
                if (explosionPoint)
                {
                    GameObject tempExplosion = Instantiate(explosionPointPF, explosionPoint.transform.position, explosionPoint.transform.rotation);
                    tempExplosion.transform.parent = explosionPoint.transform;
                }

                //Movimiento de camara por retroceso
                switch (arma)
                {
                    //cuchillo
                    case 1:
                        player.transform.Rotate(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                        break;
                    //pistola
                    case 2:
                        player.transform.Rotate(Random.Range(-6f, 6f), Random.Range(-6f, 6f), 0);
                        break;
                    //escopeta
                    case 3:
                        player.transform.Rotate(Random.Range(-11f, -9f), Random.Range(-2f, 2f), 0);
                        break;
                    //m16
                    case 4:
                        player.transform.Rotate(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
                        break;
                }

                //restamos municion si no es el cuchillo
                if (arma>1) inventario.setMunicion();

                if (arma != 0) disparando = true;

                if (arma == 4)
                {
                    gatillo = true;
                }
                else
                {
                    gatillo = false;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            gatillo = false;
        }
    }

    private void RayImpact()
    {
        if (Physics.Raycast(ray, out hit, distancia, layerMask))
        {
            Target = hit.collider.gameObject;
            //Debug.Log("Hit: " + Target.tag + " (" + Target.name + ") - " + hit.distance + " metros");
            if (hit.collider.gameObject.tag == "Enemigo" || hit.collider.gameObject.tag == "Head")
            {

                Health enemigo = Target.GetComponentInParent<Health>();
                if (hit.collider.gameObject.tag == "Head")
                {
                    enemigo.setSalud(-damage * 2, Target.tag, Target);
                    //print("daño: " + damage * 2);
                }
                else
                {
                    enemigo.setSalud(-damage, Target.tag);
                    //print("daño: " + damage);
                }

                if (Target.GetComponentInParent<PatrolManager>()) Target.GetComponentInParent<PatrolManager>().Hit();
                if (Target.GetComponentInParent<SwatPatrolManager>()) Target.GetComponentInParent<SwatPatrolManager>().Hit();

                //GameObject sangre = Instantiate(sangreImpacto, hit.point, Quaternion.LookRotation(hit.normal));
                bloodPool.GetComponent<BloodPool>().InstantiatePoolObject(hit.point, Quaternion.LookRotation(hit.normal));
                if (arma==3)
                {
                    //sangre.transform.localScale = new Vector3(sangreImpactoSize-1.5f, sangreImpactoSize - 1.5f, sangreImpactoSize - 1.5f);
                }
                else
                {
                    //sangre.transform.localScale = new Vector3(sangreImpactoSize, sangreImpactoSize, sangreImpactoSize);
                }
                
                //sangre.transform.SetParent(Target.transform);
            }

        }
    }

    public void setDisparando(bool estado)
    {
        disparando = estado;
    }

    public void setRecargando(bool estado)
    {
        recarga = estado;
    }

    public bool getDisparando()
    {
        return disparando;
    }

    //doble comprobacion del gatillo, para evitar el bug de que se quede pulsado
    void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gatillo = false;
        }
    }
}



//multiples hits
/*
RaycastHit[] hits = Physics.RaycastAll(ray);
foreach (RaycastHit hit in hits)
{
    if (hit.collider.gameObject.tag == "Target")
    {
        Target = hit.collider.gameObject;
        Debug.Log("Hit");
    }
}*/
