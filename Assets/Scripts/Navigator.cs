using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour {

    public ToggleButton ranking;
    public ToggleButton premios;
    public ToggleButton beertender;
    public ToggleButton contacto;

    public GameObject panelRanking;
    public GameObject panelPremios;
    public GameObject panelBeertender;
    public GameObject panelContacto;
    public GameObject panelHome;

    public static bool block;

    public void TurnOffAll()
    {
        if (block) return;
        ranking.Toggle(false);
        premios.Toggle(false);
        beertender.Toggle(false);
        contacto.Toggle(false);
        panelRanking.SetActive(false);
        panelPremios.SetActive(false);
        panelBeertender.SetActive(false);
        panelContacto.SetActive(false);
        panelHome.SetActive(false);
    }

    public void BTN_Ranking()
    {
        if (block) return;
        ranking.Toggle(true);
        panelRanking.SetActive(true);
        
    }

    public void BTN_Premios()
    {
        if (block) return;
        premios.Toggle(true);
        panelPremios.SetActive(true);
    }

    public void BTN_Beertender()
    {
        if (block) return;
        beertender.Toggle(true);
        panelBeertender.SetActive(true);
    }

    public void BTN_Contacto()
    {
        if (block) return;
        contacto.Toggle(true);
        panelContacto.SetActive(true);
    }

    public void BTN_Home()
    {
        if (block) return;
        panelHome.SetActive(true);
    }


}

[System.Serializable]
public class ToggleButton
{
    public Button button;
    public Sprite on;
    public Sprite off;

    public void Toggle(bool active)
    {
        button.image.sprite = (active) ? on : off;
    }

}