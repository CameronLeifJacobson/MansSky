using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadableCanvas : MonoBehaviour {

    public float childAlphas = 0;
    public bool POIControlsFade = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if (POIControlsFade)
        {
            UnityEngine.UI.Image[] images = GetComponentsInChildren<UnityEngine.UI.Image>();

            Debug.Log(images.Length);
            foreach (UnityEngine.UI.Image image in images)
            {
                
                Color childColor = image.color;
                childColor.a = childAlphas;
                image.color = childColor;
            }
        }
	}
}
