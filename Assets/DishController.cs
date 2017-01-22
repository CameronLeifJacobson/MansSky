using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour {

    public float dishLerp;
    public GameObject pivotObject;
    public GameObject dishObject;
    Vector3 differenceVector;
    float startAngle;
    float targetAngle;
    float lastPitch = 0;
    float lastYaw = 0;
    int whichLerp = 2;

    float VectorToYaw(Vector3 vec)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vec.x, vec.z);
    }

    float VectorToPitch(Vector3 vec)
    {
        return Mathf.Rad2Deg * Mathf.Acos(vec.y);
    }

    // Update is called once per frame
    void Update () {
        if (dishLerp >= 1.0f)
        {
            if (whichLerp == 0)
            {
                startAngle = lastPitch; 
                targetAngle = VectorToPitch(differenceVector.normalized);
                whichLerp = 1;
                dishLerp = 0.0f;
                GetComponent<Animation>().Play();
            }
            else if (whichLerp == 1)
            {
                whichLerp = 2;
            }
        }

        if (whichLerp == 0)
        {
            lastYaw = Mathf.LerpAngle(startAngle, targetAngle, dishLerp);
            Vector3 eulerDangles = new Vector3(0.0f, 0, lastYaw);
            pivotObject.transform.localEulerAngles = eulerDangles;
            //Debug.Log("DERP: " + eulerDangles);
        }
        else if (whichLerp == 1)
        {
            lastPitch = Mathf.LerpAngle(startAngle, targetAngle, dishLerp);
            Vector3 eulerDangles = new Vector3(lastPitch, 0, 0.0f);
            dishObject.transform.localEulerAngles = eulerDangles;
            
        }
    }

    public void SetNewFocus(Vector3 focus)
    {
        Debug.DrawLine(focus, dishObject.transform.position, Color.red, 5.0f, false);
        differenceVector = (focus - dishObject.transform.position);

        whichLerp = 0;
        startAngle = lastYaw;

        Vector3 yawDifference = differenceVector;
        yawDifference.y = 0;
        targetAngle = VectorToYaw(yawDifference.normalized);

        GetComponent<Animation>().Play();
    }
}
