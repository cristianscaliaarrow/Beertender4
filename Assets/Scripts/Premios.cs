using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premios : MonoBehaviour {

    public GameObject panelStaff;
    public GameObject panelManager;
    public GameObject panelOwner;

    
    private void OnDisable()
    {
        panelStaff.SetActive(false);
        panelManager.SetActive(false);
        panelOwner.SetActive(false);
    }

    public void BTN_ShowPremios()
    {
        switch (User.instance.rol)
        {
            case (int)Rol.STAFF:
                panelStaff.SetActive(true);
                break;
            case (int)Rol.MANAGER:
                panelManager.SetActive(true);
                break;
            case (int)Rol.OWNER:
                panelOwner.SetActive(true);
                break;
        }
    }

    public void BTN_ShowPremiosStaff()
    {
        gameObject.SetActive(true);
        panelStaff.SetActive(true);
    }

    public void BTN_VerPremiosDeManager()
    {
        gameObject.SetActive(true);
        panelStaff.SetActive(true);
        panelManager.SetActive(false);
    }

}

