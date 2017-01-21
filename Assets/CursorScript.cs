using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    public GameObject OuterCursor;
    public GameObject InnerCursor;
    public float outerRotationSpeed = 5.0f;
    public float innerRotationSpeed = -4.0f;

    public enum CursorMode
    {
        Idle,
        Seek,
        Explode,
        Implode
    };

    public CursorMode cursorMode = CursorMode.Idle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (cursorMode)
        {
            case CursorMode.Idle:
                OuterCursor.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f)/*OuterCursor.transform.forward*/, outerRotationSpeed * Time.deltaTime);
                InnerCursor.transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f)/*InnerCursor.transform.forward*/, innerRotationSpeed * Time.deltaTime);
                break;

            case CursorMode.Seek:
                // Rotate towards currently pinging POI
                // When close to POI, encircle like this: c)
                break;

            case CursorMode.Explode:
                // Expand and fade-out

                // if (Player no longer looking at POI)
                cursorMode = CursorMode.Implode;
                break;

            case CursorMode.Implode:
                // Fade-in and return to Idle

                // if (implode finished)
                cursorMode = CursorMode.Idle;
                break;
        }
        
    }
}
