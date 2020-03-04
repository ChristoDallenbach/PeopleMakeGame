using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TapType { 
    shortTap,
    longTap,
    doubleTap //probably won't be used, but just in case
}

public class PlayerScript : MonoBehaviour
{
    private float health;
    [SerializeField] private float damage;

    //shooting
    private Ray shotRay;
    private Vector3 shootDirection;
    private float tapTime;


    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;

        shootDirection = Input.mousePosition;
    }

    void Update() {
        foreach (Touch touch in Input.touches)
        {
            //checks which kind of tap the player made
            if (touch.phase == TouchPhase.Began)
            {
                tapTime += Time.deltaTime;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                shootDirection = touch.position;
                shotRay = Camera.main.ScreenPointToRay(shootDirection);
                TapType type;
                if (tapTime > 1)
                    type = TapType.longTap;
                else
                    type = TapType.shortTap;
                ShootRay(shotRay, type);
                tapTime = 0;
            }
        }
        if (Input.GetMouseButton(0))
        {
            tapTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shootDirection = Input.mousePosition;
            shotRay = Camera.main.ScreenPointToRay(shootDirection);

            TapType type;
            if (tapTime > 1)
            {
                type = TapType.longTap;
                Debug.Log("Long");
            }
            else
            {
                type = TapType.shortTap;
                Debug.Log("short");
            }
            ShootRay(shotRay, type);
            tapTime = 0;
        }
    }

    private void ShootRay(Ray shotRay, TapType type)
    {
        Debug.DrawRay(shotRay.origin, shotRay.direction, Color.red, 5);
        RaycastHit hit;//Info on the object the ray hit
        if(Physics.Raycast(shotRay.origin, shotRay.direction, out hit))
        {
            hit.transform.gameObject.GetComponent<BaseEnemy>().GetHit(damage, type);
        }
    }

    //// player checking collision with enemies
    //public List<GameObject> CheckCollision(List<GameObject> enemies)
    //{
    //    int length = enemies.Count;
    //    // loop through the enemies to check if there is collision with the player 
    //    for(int i = length-1; i >= 0; i--)
    //    {
    //        // if it is colliding take damage based of the enemies damage
    //        if (true)
    //        {
    //
    //            // destroy the enemy
    //            enemies
    //
    //            // if it did take damage check if the player is now dead
    //            CheckHealth();
    //        }
    //    }
    //
    //    return enemies;
    //}

    void OnTriggerEnter(Collider collider)
    {
        BaseEnemy normal = collider.transform.gameObject.GetComponent<BaseEnemy>();

        normal.DestroyOnColl();

        health -= normal.Damage();

        CheckHealth();
    }

    // checks if the player is dead 
    private void CheckHealth()
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
