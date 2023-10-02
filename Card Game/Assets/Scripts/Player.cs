using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Anim;

    public void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("b")) 
        {
            Anim.SetBool("Finger",true);
        }
        if (Input.GetKeyDown("n"))
        {
            Anim.SetBool("Blow", true);
        }
        if (Input.GetKeyDown("h"))
        {
            Anim.SetBool("Head", true);
        }
    }

    public void Idle()
    {
        Anim.SetBool("Blow",false);
        Anim.SetBool("Finger", false);
        Anim.SetBool("Head", false);
    }
}
