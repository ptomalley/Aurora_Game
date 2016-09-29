using UnityEngine;
using System.Collections;
using System;

public class autoShotScript : MonoBehaviour {
    private long currentTime;
    private long startTime;
   
	// Use this for initialization
	void Start () {
       startTime =DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
	
	// Update is called once per frame
	void Update () {
         currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        if ((currentTime-startTime) > 3000)
            DestroyObject(this.gameObject);

    }
}
