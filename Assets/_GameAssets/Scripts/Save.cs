using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Inventario inventario;
    [SerializeField] Health health;
    private void Awake()
    {
        LoadData();
    }

    public void LoadData()
    {
        //Posicion
        if (!PlayerPrefs.HasKey("posX")) PlayerPrefs.SetFloat("posX", 298.6539f);
        if (!PlayerPrefs.HasKey("posY")) PlayerPrefs.SetFloat("posY", 35.21f);
        if (!PlayerPrefs.HasKey("posZ")) PlayerPrefs.SetFloat("posZ", 161.0408f);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), PlayerPrefs.GetFloat("posZ"));

        //MUNICION
        //Total
        if (!PlayerPrefs.HasKey("municionPistola")) PlayerPrefs.SetInt("municionPistola", inventario.municionPistola);
        else inventario.municionPistola = PlayerPrefs.GetInt("municionPistola");

        if (!PlayerPrefs.HasKey("municionEscopeta")) PlayerPrefs.SetInt("municionEscopeta", inventario.municionEscopeta);
        else inventario.municionEscopeta = PlayerPrefs.GetInt("municionEscopeta");

        if (!PlayerPrefs.HasKey("municionM16")) PlayerPrefs.SetInt("municionM16", inventario.municionM16);
        else inventario.municionM16 = PlayerPrefs.GetInt("municionM16");

        //Cargador
        if (!PlayerPrefs.HasKey("cargadorPistola")) PlayerPrefs.SetInt("cargadorPistola", inventario.cargadorPistola);
        else inventario.cargadorPistola = PlayerPrefs.GetInt("cargadorPistola");

        if (!PlayerPrefs.HasKey("cargadorEscopeta")) PlayerPrefs.SetInt("cargadorEscopeta", inventario.cargadorEscopeta);
        else inventario.cargadorEscopeta = PlayerPrefs.GetInt("cargadorEscopeta");

        if (!PlayerPrefs.HasKey("cargadorM16")) PlayerPrefs.SetInt("cargadorM16", inventario.cargadorM16);
        else inventario.cargadorM16 = PlayerPrefs.GetInt("cargadorM16");

        //ARMAS
        //Disponibles
        if(!PlayerPrefs.HasKey("armasOptenidas")) PlayerPrefs.SetInt("armasOptenidas", 0);

        if (!PlayerPrefs.HasKey("equipoCuchillo")) PlayerPrefs.SetInt("equipoCuchillo", 0);
        if (!PlayerPrefs.HasKey("equipoPistola")) PlayerPrefs.SetInt("equipoPistola", 0);
        if (!PlayerPrefs.HasKey("equipoEscopeta")) PlayerPrefs.SetInt("equipoEscopeta", 0);
        if (!PlayerPrefs.HasKey("equipoM16")) PlayerPrefs.SetInt("equipoM16", 0);

        if (PlayerPrefs.GetInt("equipoCuchillo") == 0) inventario.equipoCuchillo = false;
        else inventario.equipoCuchillo = true;
        if (PlayerPrefs.GetInt("equipoPistola") == 0) inventario.equipoPistola = false;
        else inventario.equipoPistola = true;
        if (PlayerPrefs.GetInt("equipoEscopeta") == 0) inventario.equipoEscopeta = false;
        else inventario.equipoEscopeta = true;
        if (PlayerPrefs.GetInt("equipoM16") == 0) inventario.equipoM16 = false;
        else inventario.equipoM16 = true;
        //Equipada
        if (!PlayerPrefs.HasKey("armaActual")) PlayerPrefs.SetInt("armaActual", -1);
        
        //Vida
        if (!PlayerPrefs.HasKey("health")) PlayerPrefs.SetInt("health", 100);
        else health.salud = PlayerPrefs.GetInt("health");

        //Entorno
        if (!PlayerPrefs.HasKey("fog")) PlayerPrefs.SetFloat("fog", 0.035f);
        else RenderSettings.fogDensity = PlayerPrefs.GetFloat("fog");
    }

    public void SaveData()
    {
        //Posicion
        PlayerPrefs.SetFloat("posX", player.transform.position.x);
        PlayerPrefs.SetFloat("posY", player.transform.position.y);
        PlayerPrefs.SetFloat("posZ", player.transform.position.z);

        //MUNICION
        //Total
        PlayerPrefs.SetInt("municionPistola", inventario.municionPistola);
        PlayerPrefs.SetInt("municionEscopeta", inventario.municionEscopeta);
        PlayerPrefs.SetInt("municionM16", inventario.municionM16);

        //Cargador
        PlayerPrefs.SetInt("cargadorPistola", inventario.cargadorPistola);
        PlayerPrefs.SetInt("cargadorEscopeta", inventario.cargadorEscopeta);
        PlayerPrefs.SetInt("cargadorM16", inventario.cargadorM16);

        //ARMAS
        //Disponibles
        PlayerPrefs.SetInt("armasOptenidas", inventario.armasOptenidas);

        if (inventario.equipoCuchillo) PlayerPrefs.SetInt("equipoCuchillo", 1);
        if (inventario.equipoPistola) PlayerPrefs.SetInt("equipoPistola", 1);
        if (inventario.equipoEscopeta) PlayerPrefs.SetInt("equipoEscopeta", 1);
        if (inventario.equipoM16) PlayerPrefs.SetInt("equipoM16", 1);
        //Equipada
        PlayerPrefs.SetInt("armaActual", inventario.armaActual);

        //Vida
        PlayerPrefs.SetInt("health", health.salud);

        //Entorno
        PlayerPrefs.SetFloat("fog", RenderSettings.fogDensity);
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
