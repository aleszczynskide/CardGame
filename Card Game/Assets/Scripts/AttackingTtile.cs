using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingTtile : MonoBehaviour
{
    Animator Anim;
    public GameObject GameManager;
    public int CurrentCardNumber;
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    public void Animation(string AnimatorString)
    {
        switch (AnimatorString)
        {
            case "Left":
                {
                    Anim.SetInteger("Attack",5);
                }break;
            case "Front":
                {
                    Anim.SetInteger("Attack", 4);
                }
                break;
            case "Right":
                {
                    Anim.SetInteger("Attack", 6);
                }
                break;
            case "FlyingLeft":
                {
                    Anim.SetInteger("Attack", 2);
                }
                break;
            case "FlyingFront":
                {
                    Anim.SetInteger("Attack", 1);
                }
                break;
            case "FlyingRight":
                {
                    Anim.SetInteger("Attack", 3);
                }
                break;
        }
    }
    public void AnimatioIdle()
    {
        Anim.SetInteger("Attack",0);
        this.transform.parent = null;   
    }
    public void NextCard()
    {
            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardNumber + 1);
    }
}
