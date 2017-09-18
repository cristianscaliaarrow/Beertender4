using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResult : MonoBehaviour {

    public static PanelResult instance;

    public Text message;

	void Start () {
        instance = this;
        message = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
	}

    public static void ShowMsg(string msg , float time, Action callback = null)
    {
        instance.message.text = msg;
        instance.gameObject.SetActive(true);
        instance.StartCoroutine(instance.StartMsg(time,callback));
    }

    public static void ShowMsg(string msg)
    {
        instance.message.text = msg;
        instance.gameObject.SetActive(true);
    }

    public static void HideMsg()
    {
        instance.gameObject.SetActive(true);
    }

    private IEnumerator StartMsg(float time, Action callback = null)
    {
        
        yield return new WaitForSeconds(time);
        instance.gameObject.SetActive(false);
        if (callback != null) callback();
    }
}
