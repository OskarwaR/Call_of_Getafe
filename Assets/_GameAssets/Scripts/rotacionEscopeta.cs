using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacionEscopeta : MonoBehaviour
{
    // Start is called before the first frame update
    private float x;
    private float y;
    private float z;
    private float cx;
    private float cy;
    private float cz;
    private object player;
    void Start()
    {
        x = transform.localPosition.x;
        y = transform.localPosition.y;
        z = transform.localPosition.z;
        player= GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame

    void Update()
    {
        cx = Camera.main.transform.localPosition.x;
        cy = Camera.main.transform.localPosition.x;
        cz = GameObject.FindGameObjectWithTag("Player").transform.position.z;
        transform.localPosition = new Vector3((cx/1.7f)+0.7f, (cy/3.5f)-0.5f, z);

    }
}
