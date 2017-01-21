using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIController : MonoBehaviour {

    public enum POIState
    {
        Hidden,
        Pinging,
        Revealing,
        Maximized,
        Shrinking,
        Minimized
    }

    public bool bOrbits = false;
    public float DistanceFromOrigin = 400;
    public Vector3 origin;
    public Vector3 orbitDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
