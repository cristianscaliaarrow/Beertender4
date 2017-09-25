using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeOwnerManager : MonoBehaviour {

    public GameObject popupHistory;

    private void OnEnable()
    {
        SendAlgo.instance.LoadScreen("Home MANAGER OWNER");
    }

    public void BTN_ShowPopUpScore(bool b)
    {
        popupHistory.SetActive(b);
    }

}
