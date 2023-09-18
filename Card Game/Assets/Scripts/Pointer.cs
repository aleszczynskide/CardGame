using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
   Animator Anim;
    void Start()
    {
        Anim = GetComponent<Animator>();    
    }

    void Update()
    {
        
    }

    public void AttackCard()
    {
        Anim.SetInteger("Attack", 1);
    }
    public void AttackOpponent()
    {
        Anim.SetInteger("Attack", 2);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
