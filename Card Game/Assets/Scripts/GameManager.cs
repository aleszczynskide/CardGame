using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using TreeEditor;
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
    [Header("Prefabs")]
    public GameObject CardPrefab;
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
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].GetComponent<CardCreator>().CreateCard(-1);
        }
        HandCardSorter();
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("p"))
        {

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
        if (CardPosition == 0 && CardNumber < 4)
        {
            CardPlayerAttack(CardPosition, CardNumber);
        }
        else if (CardPosition == 1 && CardNumber < 4)
        {
            CardOpponentAttack(CardPosition, CardNumber);
        }
        else if (CardPosition == 1 && CardNumber >= 3)
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
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.325f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Third";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[0, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.156f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Fourth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 0] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.663f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Fifth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.494f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Sixth";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.325f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Seventh";
            SpawningPoints.Add(SpawnPoint);
        }
        if (GameObjectCardsOnTheTable[1, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.156f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
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
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
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
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                }
                break;
            case "Third":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.325f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 2] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                }
                break;
            case "Fourth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.156f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 3] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    CardsInHand.Remove(CurrentCardGameObject); 
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    
                    HandCardSorter();
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
                    CurrentCardGameObject.transform.position = new Vector3(-0.494f, 1.166f, 0.827f);
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
                    CurrentCardGameObject.transform.position = new Vector3(-0.325f, 1.166f, 0.827f);
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
                    CurrentCardGameObject.transform.position = new Vector3(-0.156f, 1.166f, 0.827f);
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
                            PlayerAttackRight(x, y + 1, 0, 0, "Right");
                        }
                        break;
                    case 3:
                        {
                            PlayerAttackLeft(x, y - 1, 0, 0, "Left");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 1;
                            PlayerAttackLeft(x, y - 1, 0, 0, "Left");
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
                            PlayerAttackLeft(x, y - 1, 0, 0, "Left");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 4;
                            PlayerAttackLeft(x, y - 1, 0, 0, "Left");
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
    {
        if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.x, GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y + AttackSpree] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "Flying", AttackDriection, 0);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.x, GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y + AttackSpree] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, 0);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        else if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + AttackSpree;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "Flying", AttackDriection, 0);
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
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, 0);
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
                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, 0);
                }
            }
            else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
            {
                if (y < 3)
                {
                    BoardMove(x, y + AttackSpree + 1);
                }
                else if (y == 3)

                {
                    BoardMove(1, 0);
                }
            }

        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            if (y < 3)
            {
                BoardMove(x, y + 1);
            }
            else if (y == 3)
            {
                BoardMove(1, 0);
            }

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
                            OpponentAttackLeft(x, y + 1, 0, 0, "Left");
                        }
                        break;
                    case 3:
                        {
                            OpponentAttackRight(x, y - 1, 0, 0, "Right");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 1;
                            OpponentAttackLeft(x, y + 1, 0, 0, "Left");
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
                            OpponentAttackLeft(x, y + 1, 0, 0, "Left");
                        }
                        break;
                    case 3:
                        {
                            CurrentCardAttackRange = 3;
                            OpponentAttackRight(x, y - 1, 0, 0, "Right");
                        }
                        break;
                    default:
                        {
                            CurrentCardAttackRange = 4;
                            OpponentAttackLeft(x, y + 1, 0, 0, "Left");
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
    {
        if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x - 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.x, GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y + AttackSpree] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, 0);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.x, GameObjectCardsOnTheTable[x, y + AttackSpree].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y + AttackSpree] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, 0);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x - 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, 0);
                    }
                    else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + AttackSpree;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + AttackSpree].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "", AttackDriection, 0);
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
                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, 0);
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            BoardMove(1, y + 1);
        }
    }
    public void PlayerAttackLeft(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    {
        if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + 1].transform.position.x - 0.213f, GameObjectCardsOnTheTable[x, y + 1].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "Flying", AttackDriection, 1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + 1;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + 1].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + 1].transform.position.x - 0.213f, GameObjectCardsOnTheTable[x, y + 1].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, 1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + 1;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + 1].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, 1);
                    }
                    else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + 1;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + 1].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, 1);
                    }
                    else if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + 1;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + 1].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                    }
                }
                else
                {
                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, 1);
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            BoardMove(x, y + AttackSpree + 1);
        }
    }
    public void PlayerAttackRight(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    {
        if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y - 1].transform.position.x + 0.213f, GameObjectCardsOnTheTable[x, y - 1].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "Flying", AttackDriection, -1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y - 1;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y - 1].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                else if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x + 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x + 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x + 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y - 1].transform.position.x + 0.213f, GameObjectCardsOnTheTable[x, y - 1].transform.position.y, 0.827f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x + 1, i];
                                    GameObjectCardsOnTheTable[x + 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x + 1, i] = null;
                                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, -1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x + 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y - 1;
                            AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y - 1].transform.parent = AttackTitle.transform;
                            AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, -1);
                    }
                    else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y - 1;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y - 1].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, -1);
                    }
                    else if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y - 1;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y - 1].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                    }
                }
                else
                {
                    CheckPlayerFrontAttacking(x, y, "", AttackDriection, -1);
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            BoardMove(x, y - 1);
        }
    }
    public void OpponentAttackLeft(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    {
        if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x - 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y - 1].transform.position.x + 0.213f, GameObjectCardsOnTheTable[x, y - 1].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, -1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y - 1;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y - 1].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y - 1].transform.position.x + 0.213f, GameObjectCardsOnTheTable[x, y - 1].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, -1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y - 1;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y - 1].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x - 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, -1);
                    }
                    else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y - 1;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y - 1].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y - AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "", AttackDriection, -1);
                    }
                    else if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y - 1;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y - 1].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                        BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                    }
                }
                else
                {
                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, -1);
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            BoardMove(x, y + AttackSpree + 1);
        }
    }
    public void OpponentAttackRight(int x, int y, int AttackSpree, int AttackRange, string AttackDriection)
    {
        if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack > 0)
        {
            if (GameObjectCardsOnTheTable[x - 1, y] == null)
            {
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true && GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().AntiFlying == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + 1].transform.position.x - 0.213f, GameObjectCardsOnTheTable[x, y + 1].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, 1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + 1;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + 1].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 4)
                        {
                            if (GameObjectCardsOnTheTable[x - 1, i] != null)
                            {
                                if (GameObjectCardsOnTheTable[x - 1, i].GetComponent<CardCreator>().Shield == true)
                                {
                                    GameObjectCardsOnTheTable[x - 1, i].transform.position = new Vector3(GameObjectCardsOnTheTable[x, y + 1].transform.position.x - 0.213f, GameObjectCardsOnTheTable[x, y + 1].transform.position.y, 0.614f);
                                    GameObject ObjectToTransform = GameObjectCardsOnTheTable[x - 1, i];
                                    GameObjectCardsOnTheTable[x - 1, y] = ObjectToTransform;
                                    GameObjectCardsOnTheTable[x - 1, i] = null;
                                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, 1);
                                    break;
                                }
                                else if (GameObjectCardsOnTheTable[x - 1, i] == null)
                                {
                                    continue;
                                }
                            }
                        }
                        if (i == 4)
                        {
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + 1;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                            GameObjectCardsOnTheTable[x, y + 1].transform.parent = OpponentAttackTitle.transform;
                            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Attack;
                        }
                    }
                }
            }
            else if (GameObjectCardsOnTheTable[x - 1, y] != null)
            {
                if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().Flying == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "Flying", AttackDriection, 1);
                    }
                    else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == false)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + 1;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + 1].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation("Flying" + AttackDriection);
                        BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Stealth == true)
                {
                    if (GameObjectCardsOnTheTable[x, y - AttackSpree].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckOpponentFrontAttacking(x, y, "", AttackDriection, 1);
                    }
                    else if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + 1;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y + 1].transform.parent = OpponentAttackTitle.transform;
                        OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(AttackDriection);
                        BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
                    }
                }
                else
                {
                    CheckOpponentFrontAttacking(x, y, "", AttackDriection, 1);
                }
            }
        }
        else if (GameObjectCardsOnTheTable[x, y + AttackSpree].GetComponent<CardCreator>().Attack == 0)
        {
            BoardMove(x, y + AttackSpree + 1);
        }
    }
    public void CheckPlayerFrontAttacking(int x, int y, string Changer, string AttackDriection, int PowerChanger)
    {
        if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Escape == true)
        {
            if (y < 3 && GameObjectCardsOnTheTable[x + 1, y + 1] == null)
            {
                GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x + 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, -90f, -90f);
                TailCard.GetComponent<CardCreator>().CreateCard(14);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x + 1, y];
                GameObjectCardsOnTheTable[x + 1, y + 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x + 1, y] = TailCard;
            }
            else if (y > 0 && GameObjectCardsOnTheTable[x + 1, y - 1] == null)
            {
                GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x + 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, -90f, -90f);
                TailCard.GetComponent<CardCreator>().CreateCard(14);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x + 1, y];
                GameObjectCardsOnTheTable[x + 1, y - 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x + 1, y] = TailCard;
            }
        }
        else
        {

        }
        {
            if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Attack)
            {
                AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + PowerChanger;
                GameObjectCardsOnTheTable[x, y + PowerChanger].transform.parent = AttackTitle.transform;
                AttackTitle.GetComponent<AttackingTtile>().Animation(Changer + AttackDriection);
                Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Spikes == true)
                {
                    GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                    if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                    {
                        GameObjectCardsOnTheTable[x + 1, y] = null;
                        Destroy(GameObjectCardsOnTheTable[x, y + PowerChanger]);
                        if (y <= 3)
                        {
                            CurrentCardAttackRange = 0;
                            GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                            BoardMove(x, y + PowerChanger);
                        }
                        else
                        {
                            CurrentCardAttackRange = 0;
                            GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                            BoardMove(1, 0);
                        }
                    }
                }
                GameObjectCardsOnTheTable[x + 1, y] = null;
            }
            else
            {
                AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y + PowerChanger;
                GameObjectCardsOnTheTable[x, y + PowerChanger].transform.parent = AttackTitle.transform;
                AttackTitle.GetComponent<AttackingTtile>().Animation(Changer + AttackDriection);
                GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Attack;
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Spikes == true)
                {
                    GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                    if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                    {
                        Destroy(GameObjectCardsOnTheTable[x, y + PowerChanger]);
                        if (y < 3)
                        {
                            CurrentCardAttackRange = 0;
                            BoardMove(x, y + PowerChanger + 1);
                        }
                        else if (y == 3)
                        {
                            CurrentCardAttackRange = 0;
                            BoardMove(1, 0);
                        }
                        GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                    }
                }
            }
        }
    }
    public void CheckOpponentFrontAttacking(int x, int y, string Changer, string AttackDriection, int PowerChanger)
    {
        if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Escape == true)
        {
            if (y < 3 && GameObjectCardsOnTheTable[x - 1, y + 1] == null)
            {
                GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x - 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, -90f, -90f);
                TailCard.GetComponent<CardCreator>().CreateCard(14);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x - 1, y];
                GameObjectCardsOnTheTable[x - 1, y + 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x - 1, y] = TailCard;
            }
            else if (y > 0 && GameObjectCardsOnTheTable[x - 1, y - 1] == null)
            {
                GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x - 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, -90f, -90f);
                TailCard.GetComponent<CardCreator>().CreateCard(14);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x - 1, y];
                GameObjectCardsOnTheTable[x - 1, y - 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x - 1, y] = TailCard;
            }
        }
        else
        {

        }
        if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Attack)
        {
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + PowerChanger;
            GameObjectCardsOnTheTable[x, y + PowerChanger].transform.parent = OpponentAttackTitle.transform;
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(Changer + AttackDriection);
            Destroy(GameObjectCardsOnTheTable[x - 1, y]);
            if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Spikes == true)
            {
                GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                {
                    GameObjectCardsOnTheTable[x - 1, y] = null;
                    Destroy(GameObjectCardsOnTheTable[x, y + PowerChanger]);
                    if (y <= 3)
                    {
                        CurrentCardAttackRange = 0;
                        GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                        BoardMove(x, y + PowerChanger);
                    }
                    else
                    {
                        CurrentCardAttackRange = 0;
                        GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                        BoardMove(1, 4);
                    }
                }
            }
            GameObjectCardsOnTheTable[x + 1, y] = null;
        }
        else
        {
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardX = x;
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().CurrentCardY = y + PowerChanger;
            GameObjectCardsOnTheTable[x, y + PowerChanger].transform.parent = OpponentAttackTitle.transform;
            OpponentAttackTitle.GetComponent<OpponentAttackTitle>().Animation(Changer + AttackDriection);
            GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Attack;
            if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Spikes == true)
            {
                GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                {
                    Destroy(GameObjectCardsOnTheTable[x, y + PowerChanger]);
                    if (y < 3)
                    {
                        CurrentCardAttackRange = 0;
                        BoardMove(x, y + PowerChanger + 1);
                    }
                    else if (y == 3)
                    {
                        CurrentCardAttackRange = 0;
                        BoardMove(1, 4);
                    }
                    GameObjectCardsOnTheTable[x, y + PowerChanger] = null;
                }
            }
        }
    }
    public void HandCardSorter()
    {
       for (int i = 0; i < CardsInHand.Count; i++) 
        {

            CardsInHand[i].transform.position = new Vector3(-0.332f - i * 0.06f, 1.442f, 0.255f + i * 0.01f);
            CardsInHand[i].transform.Rotate(0.1f * i, 0f, 0f);
        }
    }
}