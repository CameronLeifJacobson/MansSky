using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIArbiter : MonoBehaviour {

    public POIController[] POIControllers;
    public DishController[] DishControllers;

    float timeAccum = 0.0f;
    int POISelectedIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Every five seconds, change which POI to focus on
        timeAccum += Time.deltaTime;

        if (timeAccum >= 2.0f)
        {
            
            timeAccum -= 5.0f;
            foreach(DishController dish in DishControllers)
            {
                Debug.Log("Switching Focus");
                dish.SetNewFocus(POIControllers[POISelectedIndex].transform.position);
            }

            POISelectedIndex = (POISelectedIndex + 1) % POIControllers.Length;
        }
	}
}
