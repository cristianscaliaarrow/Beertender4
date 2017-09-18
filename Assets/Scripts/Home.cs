using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {
    public static Home instance;
    public Image background;
    public Sprite ownerManagerBackgroundImage;
    public Sprite StaffBackgroundImage;
    public Sprite commonBackgroundImage;

    public HomeStaff panelStaff;
    public HomeOwnerManager panelOwnerManager;

    public GameObject home;

    private void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        home.SetActive(false);
    }

    private void OnDisable()
    {
        background.sprite = commonBackgroundImage;
        home.SetActive(true);
    }

    public void BTN_ShowHome()
    {
        switch (User.instance.rol)
        {
            case (int)Rol.STAFF:
                panelStaff.gameObject.SetActive(true);
                panelOwnerManager.gameObject.SetActive(false);
                background.sprite = StaffBackgroundImage;
                panelStaff.userName.text = User.instance.firstName + " " + User.instance.lastName;
                panelStaff.barName.text = Shop.instance.name;
                break;
            case (int)Rol.MANAGER:
            case (int)Rol.OWNER:
                panelStaff.gameObject.SetActive(false);
                panelOwnerManager.gameObject.SetActive(true);
                background.sprite = ownerManagerBackgroundImage;
                break;
        }
    }

    public void BTN_ShowHitoryPuntos()
    {
        //gameObject.SetActive(false);
    }
}
