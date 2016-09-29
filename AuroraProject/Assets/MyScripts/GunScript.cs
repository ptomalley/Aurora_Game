using UnityEngine;
using System.Collections;
using System;

public class GunScript : MonoBehaviour {

   
    [SerializeField]private float ammo;
    [SerializeField]private Rigidbody autoBullet;
    [SerializeField]private Rigidbody rocketBullet;
    [SerializeField]private GameObject player;
   
    [SerializeField]private Camera cam;

    private long lastShot;
    private long lastShot1;
    // Use this for initialization
    void Start () {
        lastShot = 0;
        lastShot1 = 0;
        ammo = 100;
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("r"))
        {
            StartCoroutine(reload());
        }
        if (Input.GetMouseButton(0))
        {
            autoShot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            rocketShot();
        }
    }
    //Shoot automatic
    //Can be shot once very 2s
    void autoShot()
    {
        if (ammo>0)
        {
            long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (currentTime - lastShot >200)
            {
                Rigidbody bulletClone=(Rigidbody) Instantiate( autoBullet, transform.position, transform.rotation);
                //Speed of bullet
                bulletClone.GetComponent<Rigidbody>().AddForce(4000*cam.transform.forward, ForceMode.Force);
                //ammo drain
                ammo -= 1;
                lastShot = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }
        }
        
    }
    //Shoot rocket shots
    //Can be shot once every 1s
    void rocketShot()
    {
        long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        if (ammo>=25&(currentTime-lastShot1>1000))
        {
            Rigidbody rocketClone = (Rigidbody)Instantiate(rocketBullet, transform.position, transform.rotation);
            //Speed of rocket
            rocketClone.GetComponent<Rigidbody>().AddForce(2000 * cam.transform.forward, ForceMode.Force);
            //ammo drain
            ammo -= 25;
            lastShot1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
    //Reload
    IEnumerator reload()
    {
        ammo = 0;
        yield return new WaitForSeconds(2);
        ammo = 100;

    }


}
