using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadencia : MonoBehaviour
{
    private ShootController shootController;

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
    
}
