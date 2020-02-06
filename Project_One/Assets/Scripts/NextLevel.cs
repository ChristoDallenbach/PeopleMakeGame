using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private GameController controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void OnTriggerEnter(Collider other){
        controller.NextLevel();
    }
}
