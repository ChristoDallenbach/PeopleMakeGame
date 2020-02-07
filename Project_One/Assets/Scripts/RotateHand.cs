using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour {

	[SerializeField] private GameObject player;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
	private float speed;

	// Use this for initialization
	void Start()
	{
		controller = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}

	// Update is called once per frame
	void Update()
	{
		speed = controller.m_WalkSpeed;

		RotateToMouse();
	}

	public void RotateToMouse()
    {
		float angle = speed / 50f * 180;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
