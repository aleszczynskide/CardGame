using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAttackTitle : MonoBehaviour
{
    Animator Anim;
    public GameObject GameManager;
    public GameObject FlyingTitle;
    public int CurrentCardX;
    public int CurrentCardY;
    public int CurrentCardAttackSpree;
    private bool CoolDown;
    void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void Update()
    {

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
            case "FlyingRight":
                {
                    Anim.SetInteger("Attack", 6);
                }
                break;
        }
    }
    public void CheckAttackSpree()
    {
        this.transform.DetachChildren();
        Idle();  
    }
    public void Idle()
    {
        Anim.SetInteger("Attack", 9);
    }
    public void NextMove()
    {
        if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 1)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY + 1, -1, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 2)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY + 1, -1, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 3)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY, 0, 0, "Front");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 4)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 5;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY, 0, 0, "Front");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 5)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttack(CurrentCardX, CurrentCardY + 1, -1, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 0)
        {
            Anim.SetInteger("Attack", 0);
            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX,CurrentCardY + 1);
        }

    }
}
