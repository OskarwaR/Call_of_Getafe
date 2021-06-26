using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;

public class UIscript : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] ShootController shootController;
    [SerializeField] Inventario inventario;
    [SerializeField] CharacterController characterController;
    [SerializeField] UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    // Start is called before the first frame update
    
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
                shootController.enabled = false;
                inventario.enabled = false;
                characterController.enabled = false;
                firstPersonController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0.2f;
                
            }
            else
            {
                Continue();               
            }
        }
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        menu.SetActive(false);
        shootController.enabled = true;
        inventario.enabled = true;
        characterController.enabled = true;
        firstPersonController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
