using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float time=5;
    void Start()
    {
        Invoke("Kill", time);
    }

    private void Kill()
    {
        Destroy(this);
    }

}
