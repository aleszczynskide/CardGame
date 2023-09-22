using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackingTtile : MonoBehaviour
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
        if (Input.GetKey("t"))
        {
            Debug.Log(" X równa sie : " + CurrentCardX + " Y równa siê : " + CurrentCardY);
        }
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
            GameManager.GetComponent<GameManager>().PlayerAttackRight(CurrentCardX, CurrentCardY + 1, 0, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 2)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().PlayerAttackRight(CurrentCardX, CurrentCardY + 1, 0, 0, "Right");
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
            GameManager.GetComponent<GameManager>().PlayerAttackRight(CurrentCardX, CurrentCardY + 1, 0, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 0)
        {
            if (CurrentCardY <= 3)
            {
                Escape();
            }
            if (CurrentCardY > 3)
            {
                GameManager.GetComponent<GameManager>().BoardMove(1, 0);
            }
        }

    }
    public void Escape()
    {
        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Move == true)
        {
            switch (CurrentCardY)
            {
                case 0:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                           
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX,CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 1:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                            
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 2:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                            
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.159f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(1,0);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 3:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Move = false;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().MoveLeft = true;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(1, 0);
                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(1,0);
                        }
                    }
                    break;
            }
        }
        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().MoveLeft == true)
        {
            switch (CurrentCardY)
            {
                case 0:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Move = true;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().MoveLeft = false;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 1:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);

                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 2:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);

                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);

                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                    break;
                case 3:
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                        else
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(1, 0);
                        }
                    }
                    break;
            }
        }

        else
        {
            Anim.SetInteger("Attack", 0);
            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
        }
    }
}
