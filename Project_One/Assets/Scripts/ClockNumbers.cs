using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockNumbers : MonoBehaviour {

    public Text prefab;
    private float angle;
    private float xDis;
    private float yDis;
    private GameObject parent;
    private GameObject canvas;

    // Use this for initialization
    void Start () {
        parent = GameObject.Find("Clock");
        canvas = GameObject.Find("Canvas");
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        angle = -90;
        for (int i = 0; i < 6; i++)
        {
            yDis = (Mathf.Cos(angle * Mathf.Deg2Rad) * 70f) + (canvasRect.rect.height*1/8);
            xDis = (Mathf.Sin(angle * Mathf.Deg2Rad) * 90f) + (canvasRect.rect.width*1.25f);
            Vector3 position = new Vector3(xDis, yDis);
            Text prefabClone = Instantiate(prefab, position, new Quaternion(0,0,0,0));
            prefabClone.transform.parent = parent.transform;
            angle += 35f;
            int j = i * 40;
            prefabClone.text = j.ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
