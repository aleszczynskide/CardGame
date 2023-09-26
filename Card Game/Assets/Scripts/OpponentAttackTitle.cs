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
            GameManager.GetComponent<GameManager>().OpponentAttackRight(CurrentCardX, CurrentCardY - 1, 0, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 2)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().OpponentAttack(CurrentCardX, CurrentCardY, 0, 0, "Front");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 3)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().OpponentAttack(CurrentCardX, CurrentCardY, 0, 0, "Front");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 4)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 5;
            GameManager.GetComponent<GameManager>().OpponentAttack(CurrentCardX, CurrentCardY, 0, 0, "Front");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 5)
        {
            GameManager.GetComponent<GameManager>().CurrentCardAttackRange = 0;
            GameManager.GetComponent<GameManager>().OpponentAttackRight(CurrentCardX, CurrentCardY - 1, 0, 0, "Right");
        }
        else if (GameManager.GetComponent<GameManager>().CurrentCardAttackRange == 0)
        {
            if (CurrentCardY < 3)
            {
                Anim.SetInteger("Attack", 0);
                Escape();
            }
            else if (CurrentCardY == 3)
            {
                Anim.SetInteger("Attack", 0);
                if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Move == true)
                {
                    Escape();
                }
                else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().MoveLeft == true)
                {
                    Escape();
                }
                else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push == true)
                {
                    Escape();
                }
                else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft == true)
                {
                    Escape();
                }
                else
                {
                    Anim.SetInteger("Attack", 0);
                    GameManager.GetComponent<GameManager>().BoardMove(1, 4);
                }
            }
        }
    }
    public void Escape()
    {
        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] != null)
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

                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.166f, 0.827f);
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
                    case 1:
                        {
                            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                            {
                                Anim.SetInteger("Attack", 0);

                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.325f, 1.166f, 0.827f);
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

                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.156f, 1.166f, 0.827f);
                                GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                                GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);

                            }
                            else
                            {
                                Anim.SetInteger("Attack", 0);
                                GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
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
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.325f, 1.166f, 0.827f);
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
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().MoveLeft = false;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Move = true;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.166f, 0.827f);
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
                    case 1:
                        {
                            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                            {
                                Anim.SetInteger("Attack", 0);

                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.663f, 1.166f, 0.827f);
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
                    case 2:
                        {
                            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                            {
                                Anim.SetInteger("Attack", 0);

                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.494f, 1.166f, 0.827f);
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
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(-0.325f, 1.166f, 0.827f);
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
                }
            }
            else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push == true)
            {
                if (CurrentCardY == 0)
                {

                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] != null && GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform3 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] = ObjectToTransform3;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = null;
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] != null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                }
                else if (CurrentCardY == 1)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] != null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                }
                else if (CurrentCardY == 2)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(1, 4);
                    }
                    else
                    {
                        Anim.SetInteger("Attack", 0);
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                    }
                }
                else if (CurrentCardY == 3)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = true;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = false;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = true;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = false;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] != null)
                        {
                            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 3] == null)
                            {
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = true;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = false;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.z);
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.z);
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                                GameObject ObjectToTransform3 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 3] = ObjectToTransform3;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = null;
                                GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = ObjectToTransform2;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = null;
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

                    }
                }
            }
            else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft == true)
            {
                if (CurrentCardY == 0)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = false;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = true;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = false;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = true;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] != null && GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().PushLeft = false;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().Push = true;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x + 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform3 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] = ObjectToTransform3;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = null;
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 2);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY + 3] != null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }
                }
                else if (CurrentCardY == 1)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
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
                else if (CurrentCardY == 2)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);

                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.z);
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = ObjectToTransform2;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = null;
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] != null)
                        {
                            Anim.SetInteger("Attack", 0);
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                    }

                }
                else if (CurrentCardY == 3)
                {
                    if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] == null)
                    {
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                        GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                        GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                        GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                    }
                    else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] != null)
                    {
                        if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] == null)
                        {
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                            GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                            GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                            GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                        }
                        else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] != null)
                        {
                            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 3] == null)
                            {
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2].transform.position.z);
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1].transform.position.z);
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position = new Vector3(GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.x - 0.169f, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.y, GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].transform.position.z);
                                GameObject ObjectToTransform3 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform3;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = null;
                                GameObject ObjectToTransform2 = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform2;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 2] = null;
                                GameObject ObjectToTransform = GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY];
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 1] = ObjectToTransform;
                                GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY] = null;
                                GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                            }
                            else if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY - 3] != null)
                            {
                                Anim.SetInteger("Attack", 0);
                                GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
                            }
                        }
                    }
                }
            }
            else
            {
                Anim.SetInteger("Attack", 0);
                GameManager.GetComponent<GameManager>().BoardMove(CurrentCardX, CurrentCardY + 1);
            }
            if (GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[CurrentCardX, CurrentCardY].GetComponent<CardCreator>().TwoTokens == true)
            {
                GameManager.GetComponent<GameManager>().SpawnPlayerMana(2);
            }
        }
    }
}
