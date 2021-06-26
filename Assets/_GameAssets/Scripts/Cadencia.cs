using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadencia : MonoBehaviour
{
    private ShootController shootController;
    [SerializeField] SoundManager soundManager;

    private void setDisparando()
    {
        shootController = GetComponentInParent<ShootController>();
        shootController.setDisparando(false);
    }

    private void setRecargando()
    {
        shootController = GetComponentInParent<ShootController>();
        shootController.setRecargando(false);
    }

    public void SoundRecarga(int n)
    {
        soundManager.PlaySound(n);
    }

}
