using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMaze : MonoBehaviour
{
    public int size;
    public GameObject player;
    private int[,] mazeMat;

    // Start is called before the first frame update
    void Start(){
        mazeMat = new int[size, size];
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.A)) {
            Rotate(true);
            transform.Rotate(transform.forward, 90.0f);
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            Rotate(false);
            transform.Rotate(transform.forward, -90);
        }
    }

    void Rotate(bool left){
        if (left)
        {
            for (int x = 0; x < size / 2; x++)
            {
                for (int y = x; y < size - x - 1; y++)
                {
                    int temp = mazeMat[x, y];

                    mazeMat[x, y] = mazeMat[y, size - 1 - x];
                    mazeMat[y, size - 1 - x] = mazeMat[size - 1 - x, size - 1 - y];
                    mazeMat[size - 1 - x, size - 1 - y] = mazeMat[size - 1 - y, x];
                    mazeMat[size - 1 - y, x] = temp;
                }
            }
        }else{
            for (int x = 0; x < size / 2; x++)
            {
                for (int y = x; y < size - x - 1; y++)
                {
                    int temp = mazeMat[x, y];

                    mazeMat[x, y] = mazeMat[size - 1 - x, y];
                    mazeMat[size - 1 - x, y] = mazeMat[size - 1 - x, size - 1 - y];
                    mazeMat[size - 1 - x, size - 1 - y] = mazeMat[x, size - 1 - y];
                    mazeMat[x, size - 1 - y] = temp;
                }
            }
        }
    }
}
