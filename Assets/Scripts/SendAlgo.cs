using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendAlgo : MonoBehaviour {
    public static SendAlgo instance;
    GoogleAnalyticsV4 gv4;

	void Awake () {
        instance = this;
        gv4 = GameObject.Find("GAv4").GetComponent<GoogleAnalyticsV4>();
        gv4.StartSession();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SceneManager.LoadScene("a");
        if (Input.GetKeyDown(KeyCode.B))
            SceneManager.LoadScene("b");
    }

    public void LoadScreen(string s)
    {
        gv4.LogScreen(s);
    }
    public void LogEvent(string category,string action)
    {
        gv4.LogEvent(new EventHitBuilder().SetEventCategory(category).SetEventAction(action));
    }
    public void OpenScreen(string s)
    {
        gv4.LogEvent(new EventHitBuilder().SetEventCategory("LoadScreen").SetEventAction(s));
    }

}
