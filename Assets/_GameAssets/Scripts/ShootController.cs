using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject Target;

    private string arma="escopeta";
    public int dañoPistola=30;
    public int dañoEscopeta=0;
    public int dañoCuchillo=10;
    public int dañoRifle=20;

    public GameObject sangreImpacto;
    public float sangreImpactoSize;

    public GameObject player;
    [SerializeField] private LayerMask layerMask;


    void Start()
    {
        // Initialise ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        player = this.gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown: "+arma);
            // Reset ray with new mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,1000, layerMask))
            {
                Target = hit.collider.gameObject;
                Debug.Log("Hit: " + Target.tag + " (" + Target.name + ") - " + hit.distance + " metros");
                if (hit.collider.gameObject.tag == "Enemigo")
                {
                    Health enemigo = Target.GetComponentInParent<Health>();
                    if (hit.collider.gameObject.name == "Head")
                    {
                        enemigo.setSalud(-dañoEscopeta*2, Target.name);
                    }
                    else
                    {
                        enemigo.setSalud(-dañoEscopeta, Target.name);
                    }

                    GameObject sangre = Instantiate(sangreImpacto, hit.point, Quaternion.LookRotation(hit.normal));
                    sangre.transform.localScale = new Vector3(sangreImpactoSize, sangreImpactoSize, sangreImpactoSize);
                    //sangre.transform.SetParent(Target.transform);
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

        }
    }
}
