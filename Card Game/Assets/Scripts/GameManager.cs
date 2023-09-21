using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Attacking Titles")]
    public GameObject AttackTitle;
    public GameObject FlyingTitle;
    public GameObject OpponentAttackTitle;
    [Header("CardsManagement")]
    public List<GameObject> CardsInHand;
    [Header("Unsorted")]
    public GameObject[,] GameObjectCardsOnTheTable = new GameObject[3, 4];
    public List<GameObject> SpawningPoints;
    public List<GameObject> PlayerTokens;
    public GameObject Camera;
    private int BoardHealth = 5;
    public int PlayerMana;
    public GameObject PlayerManaPoints;
    public GameObject SpawnPoints;
    public GameObject CurrentCardGameObject;
    public Card CurrentCardCard;
    public GameObject Pointer;
    private bool CancellMovement = false;
    public int CurrentCardAttackRange;
    void Start()
    {
        SpawnPlayerMana(8);
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("p"))
        {
            for (int i = 0; i < 3; i++)
            {
                if (GameObjectCardsOnTheTable[1, i] != null)
                {
                    Debug.Log("Karta :" + 1 + " " + i + " flying; " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Flying + " Antiflying" + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiFlying + "Karta :" + 1 + " " + i + " Stelth: " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Stealth + " AntiStealth : " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiStealth);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (GameObjectCardsOnTheTable[0, i] != null)
                {
                    Debug.Log("Karta :" + 1 + " " + i + " flying; " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Flying + " Antiflying" + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiFlying + "Karta :" + 1 + " " + i + " Stelth: " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Stealth + " AntiStealth : " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiStealth);
                }
            }
        }
        if (Input.GetKeyDown("s") && CancellMovement == true)
        {
            int y = SpawningPoints.Count;
            for (int i = 0; y > i; i++)
            {
                Destroy(SpawningPoints[i]);
            }
            SpawningPoints.Clear();
            CurrentCardCard = null;
            CurrentCardGameObject = null;
        }
    }
    public void BoardMove(int CardPosition, int CardNumber)
    {
        if (CancellMovement == true)
        {
            int y = SpawningPoints.Count;
            for (int i = 0; y > i; i++)
            {
                Destroy(SpawningPoints[i]);
            }
            SpawningPoints.Clear();
            CurrentCardCard = null;
            CurrentCardGameObject = null;
        }
        Camera.GetComponent<CameraMovement>().Camera = 2;
        if (CardNumber < 4 && CardPosition == 0)
        {

            CardPlayerAttack(0, CardNumber);

        }
        else if (CardNumber < 4 && CardPosition == 1)
        {
            CardOpponentAttack(1, CardNumber);
        }
        else if (CardNumber > 3 && CardPosition == 1)
        {
            Camera.GetComponent<CameraMovement>().Camera = 0;
            Debug.Log(BoardHealth);
        }
    }
    public void CardPlace(Card CurrentCardCard, GameObject CurrentPickedCard)
    {
        if (CurrentCardCard.Cost <= PlayerMana)
        {
            CancellMovement = true;
            Camera.GetComponent<CameraMovement>().Camera = 2;
            CurrentCardGameObject = CurrentPickedCard;
            this.CurrentCardCard = CurrentCardCard;
            SpawnSpawningPoints();
        }
    }
    public void SpawnPlayerMana(int x)
    {
        for (int i = 0; i < x; i++)
        {
            GameObject NewToken = Instantiate(PlayerManaPoints, new Vector3(0.2455593f, 1.335f, 0.7793094f), Quaternion.identity);
            PlayerTokens.Add(NewToken);
            PlayerMana++;
        }
    }
    public void SpawnSpawningPoints()
    {
        if (GameObjectCardsOnTheTable[0, 0] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.663f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "First";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[0, 1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Second";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[0, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.328f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Third";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[0, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.159f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Fourth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 0] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.668f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Fifth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.497f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Sixth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.33f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Seventh";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.165f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Eight";
            SpawningPoints.Add(SpawnPoint);
        }
    }

    public void SpawnPointerActivated(string SpawnerName)
    {

        switch (SpawnerName)
        {
            case "First":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 0] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Second":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 1] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Third":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 2] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Fourth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.159f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 3] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Fifth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.668f, 1.166f, 0.827f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[1, 0] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Sixth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.497f, 1.166f, 0.827f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[1, 1] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Seventh":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.33f, 1.166f, 0.827f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[1, 2] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Eight":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.165f, 1.166f, 0.827f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[1, 3] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
        }
        int y = SpawningPoints.Count;
        for (int i = 0; y > i; i++)
        {
            Destroy(SpawningPoints[i]);
        }
        SpawningPoints.Clear();
    }
    private void OnMouseDown()
    {
        BoardMove(0, 0);
    }

    public void ManaMinus(int x)
    {
        for (int i = 0; i < x; i++)
        {
            Destroy(PlayerTokens[PlayerTokens.Count - 1]);
            PlayerTokens.RemoveAt(PlayerTokens.Count - 1);
            PlayerMana--;
        }
    }
    public void CardPlayerAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y] != null)
        {
            if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 1)
            {
                PlayerAttack(x, y, 0, 0, "Front");
            }
            else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 2)
            {
                switch (y)
                {
                    case 0:
                        {
                            PlayerAttack(x, y + 1, -1, 0, "Right");
                        }
                        break;
                    case 3:
                        {
                            PlayerAttack(x, y - 1, 1, 0, "Left");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 1;
                            PlayerAttack(x, y - 1, 1, 1, "Left");
                        }
                        break;
                }
            }
            else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 3)
            {
                switch (y)
                {
                    case 0:
                        {
                            CurrentCardAttackRange = 2;
                            PlayerAttack(x, y, 0, 0, "Front");
                        }
                        break;
                    case 3:
                        {
                            CurrentCardAttackRange = 3;
                            PlayerAttack(x, y - 1, 1, 0, "Left");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 4;
                            PlayerAttack(x, y - 1, 1, 1, "Left");
                        }
                        break;
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y] == null)
        {
            if (y < 3)
            {
                BoardMove(0, y + 1);
            }
            else if (y >= 3)
            {
                BoardMove(1, 0);
            }
        }
    }

    public void PlayerAttack(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    //x and y are positions on board,AttackSpree is to show the current card position on board
    //attackrange is to specify if the card can attack left or right for the next animation nad directon is the direction of the animation 
    {
        if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                    AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                    BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                }
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == false)
                {
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                    AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                    AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                    BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                }

            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                            Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                            GameObjectCardsOnTheTable[x + 1, y] = null;
                        }
                        else
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                            GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                    else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                            GameObjectCardsOnTheTable[x + 1, y] = null;
                        }
                        else
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                    else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                        GameObjectCardsOnTheTable[x + 1, y] = null;
                    }
                    else
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
            }
        }
       else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            Debug.Log("tu jestem");
            BoardMove(x, y + AttackSpree + 1);
        }
    }
    public void CardOpponentAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y] != null)
        {
            if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 1)
            {
                OpponentAttack(x, y, 0, 0, "Front");
            }
            else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 2)
            {
                switch (y)
                {
                    case 0:
                        {
                            OpponentAttack(x, y + 1, -1, 0, "Left");
                        }
                        break;
                    case 3:
                        {
                            OpponentAttack(x, y - 1, 1, 0, "Right");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 1;
                            OpponentAttack(x, y + 1, -1, 1, "Left");
                        }
                        break;
                }
            }
            else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 3)
            {
                switch (y)
                {
                    case 0:
                        {
                            CurrentCardAttackRange = 2;
                            OpponentAttack(x, y + 1, -1, 0, "Left");
                        }
                        break;
                    case 3:
                        {
                            CurrentCardAttackRange = 3;
                            OpponentAttack(x, y - 1, 1, 0, "Right");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 4;
                            OpponentAttack(x, y + 1, -1, 1, "Left");
                        }
                        break;
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y] == null)
        {
            BoardMove(1, y + 1);
        }
    }

    public void OpponentAttack(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    //x and y are positions on board,AttackSpree is to show the current card position on board
    //attackrange is to specify if the card can attack left or right for the next animation nad directon is the direction of the animation 
    {
        if (GameObjectCardsOnTheTable[x - 1, y] == null)
        {
            if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
            {
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
            }
            if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == false)
            {
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
            }

        }
        else if (GameObjectCardsOnTheTable[x - 1, y] != null)
        {
            if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
            {
                if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                        Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                        GameObjectCardsOnTheTable[x - 1, y] = null;
                    }
                    else
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                        GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x + 1, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == false)
                {
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                    BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x + 1, y + AttackSpree].GetComponent<CardCreator>().Attack;
                }
            }
            else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Stealth == true)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                        Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                        GameObjectCardsOnTheTable[x - 1, y] = null;
                    }
                    else
                    {
                        AttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        AttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                        AttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                        GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == false)
                {
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                    BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                }
            }
            else
            {
                if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack)
                {
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                    Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                    GameObjectCardsOnTheTable[x + 1, y] = null;
                }
                else
                {
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                    GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform.parent;
                    OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                    GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                }
            }
        }
    }
}