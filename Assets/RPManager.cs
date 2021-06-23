using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPManager : MonoBehaviour
{
    public ReflectionProbe[] reflectionProbe;

    private void Start()
    {
        foreach(ReflectionProbe rp in reflectionProbe)
        {
            rp.RenderProbe();
        }
    }

}
