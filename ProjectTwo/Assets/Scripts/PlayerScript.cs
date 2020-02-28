using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float health;
    [SerializeField] private float damage;

    //shooting
    private Ray shotRay;
    private Vector3 shootDirection;


    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;

        shootDirection = Input.mousePosition;
    }

    void Update() {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                shootDirection = touch.position;
                shotRay = Camera.main.ScreenPointToRay(shootDirection);

                ShootRay(shotRay);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootDirection = Input.mousePosition;
            shotRay = Camera.main.ScreenPointToRay(shootDirection);

            ShootRay(shotRay);
        }
    }

    private void ShootRay(Ray shotRay)
    {
        Debug.DrawRay(shotRay.origin, shotRay.direction, Color.red, 5);
        RaycastHit hit;//Info on the object the ray hit
        if(Physics.Raycast(shotRay.origin, shotRay.direction, out hit))
        {
            hit.transform.gameObject.GetComponent<BaseEnemy>().GetHit(damage);
        }
    }

    // checks if the player is dead 
    public void CheckHealth()
    {
        if(health <= 0.0f)
        {
            Die();
        }
    }

    // called when player has no health and ends game
    private void Die()
    {
        // go to the game over scene or just destroy player & pause scene w/ restart button

    }
}
