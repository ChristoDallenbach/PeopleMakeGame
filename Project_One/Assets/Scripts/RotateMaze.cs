﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// to add: lower FOV or add motion blur while turning (if you can, do this incrementally to get an ease in/ease out effect)
/// </summary>
public class RotateMaze : MonoBehaviour
{
    public int size;
    private int[,] mazeMat;
    private bool isRotate;
    private float destination;
    private float prevSpeed;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    // Start is called before the first frame update
    void Start()
    {
        mazeMat = new int[size, size];
        isRotate = false;
        prevSpeed = 0;
        controller = this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRotate)
            {
                prevSpeed = controller.m_WalkSpeed;
                controller.m_WalkSpeed = controller.m_StartSpeed/2;
                isRotate = true;
                destination = transform.rotation.eulerAngles.y + 90;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRotate)
            {
                prevSpeed = controller.m_WalkSpeed;
                controller.m_WalkSpeed = controller.m_StartSpeed/2;
                isRotate = true;
                destination = transform.rotation.eulerAngles.y - 90;
            }
        }
        RotateModel();
    }

    void RotateModel()
    {
        if (isRotate)
        {
            Time.timeScale = 0.2f;
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(transform.rotation.x, destination, transform.rotation.z)) > 1e-1)//Compare the two rotations, with a small buffer
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, destination, transform.rotation.z), Time.deltaTime * 800);
            else
            {
                controller.m_WalkSpeed = prevSpeed;
                isRotate = false;
                Time.timeScale = 1.0f;
            }
        }
    }

    void RotateMatrix(bool left)
    {
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
        }
        else
        {
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