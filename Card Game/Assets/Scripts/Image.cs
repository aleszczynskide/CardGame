using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image : MonoBehaviour
{
    public GameObject Card;
    Animator Anim;
    void Start()
    {
        Anim = GetComponent<Animator>();    
    }
    void Update()
    {
        
    }
    public void Death()
    {
        Card.GetComponent<CardCreator>().Death();
    }
    public void DeathAnimation()
    {
        Anim.SetTrigger("Death");
    }
    
}
