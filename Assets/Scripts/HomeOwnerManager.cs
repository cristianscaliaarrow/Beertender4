using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeOwnerManager : MonoBehaviour {

    public GameObject popupHistory;

    public void BTN_ShowPopUpScore(bool b)
    {
        popupHistory.SetActive(b);
    }

}
