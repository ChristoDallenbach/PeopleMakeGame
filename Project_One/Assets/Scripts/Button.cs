using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject clockHand;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseDown()
    {
        clockHand.GetComponent<RotateHand>().enabled = !clockHand.GetComponent<RotateHand>().enabled;
    }
}
