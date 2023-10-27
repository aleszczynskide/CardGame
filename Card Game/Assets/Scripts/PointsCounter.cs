using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    Animator Anim;
    private GameObject GameManager;
    public GameObject Camera;
    public int Points;
    void Start()
    {
        Anim = GetComponent<Animator>();
        GameManager = GameObject.Find("brain_jar");
    }
    public void PointsCount()
    {
        if (Points > 0)
        {
            Camera.GetComponent<CameraMovement>().Camera = 5;
            Points--;
            Anim.SetTrigger("Minus");
        }
        else if (Points < 0)
        {
            Camera.GetComponent<CameraMovement>().Camera = 5;
            Points++;
            Anim.SetTrigger("Plus");
        }
        else if (Points == 0)
        {
            Camera.GetComponent<CameraMovement>().Camera = 0;
        }
    }
}
