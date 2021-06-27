using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduccion : MonoBehaviour
{
    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource fuego;
    [SerializeField] Animator camara;
    [SerializeField] GameObject splash;
    [SerializeField] ShootController shootController;
    [SerializeField] Inventario inventario;
    [SerializeField] CharacterController characterController;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;


    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("intro");
            fuego.Stop();
            intro.Play();
            splash.SetActive(true);
            shootController.enabled = false;
            inventario.enabled = false;
            characterController.enabled = false;
            firstPersonController.enabled = false;
            StartCoroutine(FadeOff());
        }
    }

    IEnumerator FadeOff()
    {
        while (intro.isPlaying) 
        { 
            //print("esperando");
            yield return null;
        }
        fuego.Play();
        splash.GetComponent<Animator>().SetBool("Fade", true);
        camara.enabled = true;
        camara.SetTrigger("Intro");
        yield return new WaitForSeconds(5.5f);
        camara.enabled = false;
        shootController.enabled = true;
        inventario.enabled = true;
        characterController.enabled = true;
        firstPersonController.enabled = true;
        Destroy(this.gameObject);
       
    }
}
