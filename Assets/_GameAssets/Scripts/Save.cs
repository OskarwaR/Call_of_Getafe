using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Score")==false)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
