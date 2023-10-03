using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public int Camera = 0;
    private Animator Anim;
    public GameObject Light; 
    private void Start()
    {
        Anim=GetComponent<Animator>();  
    }
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            if (Camera < 4)
            {
                Camera++;
            }
            else
            {
                
            }
        }
        if (Input.GetKeyDown("s"))
        {
            if (Camera > -1)
            {
                Camera--;
            }
            else
            {
               
            }
        }

        switch (Camera)
        {
            case 0:
                {
                    Anim.SetInteger("Camera", 0);
                    transform.position = new Vector3(-0.4999999f, 1.55f, -0.248f);
                    transform.rotation = Quaternion.Euler(8.619f, 0f,0f) ;
                }
                break;
            case 2:
                {
                    Anim.SetInteger("Camera", 2);
                    transform.position = new Vector3(-0.4999999f, 1.973f, 0.349f);
                    transform.rotation = Quaternion.Euler(68.045f, 0f, 0f);
                }
                break;
            case 1:
                {
                    Anim.SetInteger("Camera", 1);
                    transform.position = new Vector3(-0.32f, 1.55f, -0.139f);
                    transform.rotation = Quaternion.Euler(8.398f, 0f, 0f);
                }
                break;
            case -1:
                {
                    Anim.SetInteger("Camera", -1);
                    transform.position = new Vector3(-0.4999999f, 1.55f, -1.275f);
                    transform.rotation = Quaternion.Euler(8.619f, 0f, 0f);
                }
                break;
            case 3:
                {
                    Anim.SetInteger("Camera", 3);
                    transform.position = new Vector3(-0.343f, 1.878f, 1.493f);
                    transform.rotation = Quaternion.Euler(8.619f, 0f, 0f);
                }
                break;
            case 4:
                {
                    Anim.SetInteger("Camera", 4);
                    transform.position = new Vector3(-0.343f, 1.878f, 1.493f);
                    transform.rotation = Quaternion.Euler(8.619f, 0f, 0f);
                }
                break;
        }
    }
    public void FourLightOn()
    {
        Light.SetActive(true);
    }
    public void FourLightOff()
    {
        Light.SetActive(false);
    }
}
