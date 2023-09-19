using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingTtile : MonoBehaviour
{
    Animator Anim;
    public GameObject GameManager;
    public int CurrentCardX;
    public int CurrentCardY;
    public int CurrentCardAttackSpree;
    private bool CoolDown;
    void Start()
    {
        Anim = GetComponent<Animator>();
    }
    public void Animation(string AnimationName)
    {
        switch (AnimationName)
        {
            case "Left":
                {
                    Anim.SetInteger("Attack", 1);
                }
                break;
            case "Front":
                {
                    Anim.SetInteger("Attack", 2);
                }
                break;
            case "Right":
                {
                    Anim.SetInteger("Attack", 3);
                }
                break;
            case "FlyingLeft":
                {
                    Anim.SetInteger("Attack", 4);
                }
                break;
            case "FlyingFront":
                {
                    Anim.SetInteger("Attack", 5);
                }
                break;
            case "FlyingRIght":
                {
                    Anim.SetInteger("Attack", 6);
                }
                break;
        }
    }
    public void CheckAttackSpree()
    {
        if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 1)
        {
            
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY, CurrentCardAttackSpree,0,"Right");
           
        }
        else
        {
            Idle();
            this.transform.DetachChildren();    
        }
    }
    public void Idle()
    {
        Anim.SetInteger("Attack", 9);
    }
    public void NextMove()
    {
        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardY + 1);
    }
}
