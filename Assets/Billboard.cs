using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.forward = transform.position.normalized;
        //LookAt(Vector3.zero, Vector3.up);
        //Debug.Log(Camera.main); 
    }
}