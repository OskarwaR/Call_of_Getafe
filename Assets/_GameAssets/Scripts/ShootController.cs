using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private GameObject Target;

    public int da�oPistola=30;
    public int da�oEscopeta=50;
    public int da�oCuchillo=10;
    public int da�oRifle=20;

    void Start()
    {
        // Initialise ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            // Reset ray with new mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Target = hit.collider.gameObject;
                Debug.Log("Hit: " + Target.tag + " (" + Target.name + ")");
                if (hit.collider.gameObject.tag == "Enemigo")
                {
                    Health enemigo = Target.GetComponentInParent<Health>();
                    if (hit.collider.gameObject.name == "Head")
                    {
                        enemigo.setSalud(-da�oEscopeta*2);
                    }
                    else
                    {
                        enemigo.setSalud(-da�oEscopeta);
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

        }
    }
}
