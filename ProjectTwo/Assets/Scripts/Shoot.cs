using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Ray shotRay;
    private Vector3 shootDirection;
    public float radius;
    [SerializeField] private GameObject cubert;
    private EnemyMove move;

    // Start is called before the first frame update
    void Start()
    {
        shootDirection = Input.mousePosition;
        move = cubert.GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        { 
            if(touch.phase == TouchPhase.Began)
            {
                shootDirection = touch.position;
                shotRay = Camera.main.ScreenPointToRay(shootDirection);

                ShootRay(shotRay, move);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootDirection = Input.mousePosition;
            shotRay = Camera.main.ScreenPointToRay(shootDirection);

            ShootRay(shotRay, move);
        }
    }


    private void ShootRay(Ray shotRay, EnemyMove move)
    {
        Debug.DrawRay(shotRay.origin, shotRay.direction, Color.red, 5);

        if (Physics.Raycast(shotRay.origin, shotRay.direction))
        {
            Vector3 temp = Random.insideUnitCircle.normalized * radius;
            cubert.transform.position = new Vector3(temp.x, 0, Mathf.Abs(temp.y));
        }
    }
}
