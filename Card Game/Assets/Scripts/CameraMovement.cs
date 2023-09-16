using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int Camera;
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            if (Camera == 0)
            {
                Camera++;
            }
            else
            {
                Debug.Log("InvalidCamera");
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (Camera == 1)
            {
                Camera--;
            }
            else
            {
                Debug.Log("InvalidCamera");
            }
        }

        switch (Camera)
        {
            case 0:
                {
                    transform.position = new Vector3(-0.4999999f, 1.55f, -0.248f);
                    transform.rotation = Quaternion.Euler(8.619f, 0f,0f) ;
                }break;
            case 1:
                {
                    transform.position = new Vector3(-0.4999999f, 1.973f, 0.349f);
                    transform.rotation = Quaternion.Euler(68.045f, 0f, 0f);
                }
                break;
        }
    }
}
