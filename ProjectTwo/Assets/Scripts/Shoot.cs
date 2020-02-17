using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Vector3 shootDirection;
    private Vector3 startPosition;
    [SerializeField] private GameObject cubert;
    [SerializeField] private Camera camera;
    private EnemyMove move;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = Input.mousePosition;
        shootDirection = this.transform.forward;
        move = cubert.GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        startPosition = Input.mousePosition;
        startPosition = camera.ScreenToWorldPoint(startPosition);
        startPosition.z = -10;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay((startPosition), shootDirection, Color.red, 5);
            if (Physics.Raycast((startPosition), shootDirection))
            {
                move.direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            }
        }
    }

}
