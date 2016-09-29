using UnityEngine;
using System.Collections;
using System.Timers;
using System;
using System.Threading;
public class tunnelLightScript : MonoBehaviour {
    public GameObject[] wallArray = new GameObject[10];
    private long startTime;
    private long endTime;
    public int count;
	// Use this for initialization
	void Start () {
        StartCoroutine(doLightStuff(0, 39));
        StartCoroutine(undoLightStuff(0, 39));

    }

    // Update is called once per frame
    void Update () {
	
    }
   //Coroutine to iterate through lights on the hallway
    IEnumerator doLightStuff(int i, int j)
    {
        //col determines what color the hallway will emmit
        int col = 1;
        while (true)
        {
            for (int x = i; x <= j; x += 2)
            {
                Color baseColor=Color.red;
                switch (col)
                {
                     case 1:
                        baseColor = Color.red;
                        break;
                    case 2:
                        baseColor = Color.magenta;
                        break;
                    case 3:
                        baseColor = Color.yellow;
                        break;
                    case 4:
                        baseColor = Color.cyan;
                        break;
                }
                //create a renderer from the component to light up
                //Make material from it, and set the emissive property on it
                //Higher emission value=more light
                Renderer renderer = wallArray[x].GetComponent<Renderer>();
                Material mat = renderer.material;
                float emission = 2.0f;
                Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
                mat.SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(renderer, finalColor);
                Renderer renderer1 = wallArray[x + 1].GetComponent<Renderer>();
                Material mat1 = renderer1.material;
                mat1.SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(renderer1, finalColor);
                yield return new WaitForSeconds(0.08f);
            }
            col++;
            if (col > 4)
                col = 1;
        }
    }
    //Same process as above, just sets emission value to zero, and color to black
   IEnumerator undoLightStuff(int i, int j)
    {
        yield return new WaitForSeconds(.32f);
        while (true)
        {
          
            for (int x = i; x <= j; x+=2)
            {
                Renderer renderer = wallArray[x].GetComponent<Renderer>();
                Material mat = renderer.material;
                float emission = 0.0f;
                Color baseColor = Color.black;
                Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
                mat.SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(renderer, finalColor);
                Renderer renderer1 = wallArray[x + 1].GetComponent<Renderer>();
                Material mat1 = renderer1.material;
                mat1.SetColor("_EmissionColor", finalColor);
                DynamicGI.SetEmissive(renderer1, finalColor);

                yield return new WaitForSeconds(0.08f);
            }
        }
    }
}
