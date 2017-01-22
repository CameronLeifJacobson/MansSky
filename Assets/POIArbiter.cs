using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIArbiter : MonoBehaviour {

    public POIController[] POIControllers;
    public DishController[] DishControllers;
    public Camera playerCamera;
    public GameObject sky;

    float timeAccum = 0.0f;
    int POISelectedIndex = 0;

    bool HaveAllPOIsBeenSeen()
    {
        foreach (POIController poi in POIControllers)
        {
            if (!poi.hasBeenSeen)
                return false;
        }
        return true;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Every five seconds, change which POI to focus on
        timeAccum += Time.deltaTime;

        if (timeAccum >= 5.0f)
        {
            timeAccum -= 10.0f;

            POIController newlyActivatedPOI = POIControllers[POISelectedIndex];

            foreach (DishController dish in DishControllers)
            {
                //Debug.Log("Switching Focus");
                dish.SetNewFocus(newlyActivatedPOI.GetComponent<SphereCollider>().transform.position + newlyActivatedPOI.GetComponent<SphereCollider>().transform.right * newlyActivatedPOI.GetComponent<SphereCollider>().center.x);
            }

            newlyActivatedPOI.BeginPulsing();

            POISelectedIndex = (POISelectedIndex + 1) % POIControllers.Length;
        }

        Vector3 start = playerCamera.transform.position;
        Vector3 end = playerCamera.transform.position + 10000.0f * playerCamera.transform.forward;

        Physics.Raycast(start, end, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide);
        Debug.DrawLine(start, end, Color.blue, 4);
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10000.0f))
        {
            
            POIController hitPOIController = hit.collider.GetComponentInParent<POIController>();

            if (hitPOIController)
            {
                //print("Found an object - distance: " + hit.distance);
                hitPOIController.UserRaycastHit(Time.deltaTime);
            }
        }
    }
}
