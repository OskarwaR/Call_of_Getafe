using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public class ControlVida : MonoBehaviour
{
    PostProcessVolume volume;
    Vignette vignette;
    Health vida;

    [SerializeField] GameObject muerteUI;
    [SerializeField] ShootController shootController;
    [SerializeField] Inventario inventario;
    [SerializeField] CharacterController characterController;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    [SerializeField] Animator animator;
    [SerializeField] GameObject mira;


    bool muerto=false;

    // Start is called before the first frame update
    void Awake()
    {
        volume = gameObject.GetComponentInChildren<PostProcessVolume>();
        vida = GetComponent<Health>();
        volume.profile.TryGetSettings(out vignette);
        muerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        float tVida = 1 - ((float)vida.getSalud() / 100);
        vignette.intensity.value = tVida/1.5f;
        if (vida.getSalud() <= 0 && !muerto)
        {
            vida.salud = 0;
            StartCoroutine(Muerte());
        }

        if (muerto)
            if (Input.anyKey)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //print(vida.getSalud());
    }

    IEnumerator Muerte()
    {
        GameObject.FindGameObjectWithTag("Brazos").SetActive(false);
        muerteUI.SetActive(true);
        mira.SetActive(false);
        shootController.enabled = false;
        inventario.enabled = false;
        characterController.enabled = false;
        firstPersonController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        animator.enabled = true;
        animator.SetBool("Muerte", true);
        yield return new WaitForSeconds(2);
        muerto = true;
    }
}
