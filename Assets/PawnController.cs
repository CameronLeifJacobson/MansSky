using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour {

    public GameObject myCamera;
    public GameObject myCursor;

    public float cursorDistanceFromHead = 40.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            float mouseXDelta = 6.0f * Input.GetAxisRaw("Mouse X");
            float mouseYDelta = 6.0f * Input.GetAxisRaw("Mouse Y");
            myCamera.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), mouseXDelta, Space.World);
            myCamera.transform.Rotate(myCamera.transform.right, -mouseYDelta, Space.World);
            Debug.Log(mouseXDelta);
        }

        // Move cursor to be in front of the camera!
        //myCursor.transform.transform.forward = myCamera.transform.forward;
        //myCursor.transform.transform.right = myCamera.transform.right;
        //myCursor.transform.transform.up = myCamera.transform.up;
        myCursor.transform.position = myCamera.transform.position + myCamera.transform.forward * cursorDistanceFromHead;
    }
}
