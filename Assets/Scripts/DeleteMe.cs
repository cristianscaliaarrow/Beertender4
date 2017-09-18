using System;
using UnityEngine;

public class DeleteMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print(UnixTimeNow());
    }

    public long UnixTimeNow()
    {
        var timeSpan = (new DateTime(2017,9,7,0,0,0) - new DateTime(1970, 1, 1, 0, 0, 0));
        return (long)timeSpan.TotalSeconds;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
