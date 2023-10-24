using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ZombieText;
    public GameObject Light1;
    public GameObject ZombieHand;
    [HideInInspector] public bool CardPicked;
    public GameObject Zombie;
    public GameObject PlayerManager;
    [Header("Attacking Titles")]
    public GameObject AttackTitle;
    public GameObject FlyingTitle;
    public GameObject OpponentAttackTitle;
    [Header("CardsManagement")]
    public List<GameObject> CardsInHand;
    public List<int> CardCollection;
    public List<int> CardTemporaryBin;
    public GameObject CardStack;
    public List<GameObject> CardsToPickAndestroy;
    [Header("Prefabs")]
    public GameObject CardPrefab;
    public GameObject CardMannequinn;
    public GameObject CardPicker;
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
    private int CurrentTokenSpawner = 1;
    public GameObject Map;
    private int CurrentTurn = 0;
    private int BattleType = 0;
    void Start()
    {
        StartingHand();
        for (int i = 0; i < CardsInHand.Count; i++)
        {
            CardsInHand[i].GetComponent<CardCreator>().CreateCard(-1);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Debug.Log(BattleType);
        }
        if (Input.GetKeyDown("p"))
        {
            StartBattle();
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
            if (BoardHealth <= 0)
            {
                ZombieText.SetActive(true);
                ZombieText.GetComponent<Zombie>().StartDialogue(5, 8,2);
            }
            else if (BoardHealth >= 10)
            {
                EndBattle();
            }
            else
            {
                Camera.GetComponent<CameraMovement>().Camera = 0;
                Debug.Log(BoardHealth);
                NextTurn();
            }
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
            CurrentCardGameObject.GetComponent<Animator>().enabled = true;
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
        CurrentCardGameObject.transform.position = new Vector3(-0.859f, 1.349f, 0.421f);
        CurrentCardGameObject.GetComponent<CardCreator>().GameCardReady();
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
                    CurrentCardGameObject.GetComponent<CardCreator>().CardPosition(2);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 0] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[0, 1] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 1] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[1, 0] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[1, i] != null && GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[1, i];
                                GameObjectCardsOnTheTable[1, i].transform.position = new Vector3(-0.663f, 1.166f, 0.827f);
                                GameObjectCardsOnTheTable[1, 0] = ObjectToTransform;
                                GameObjectCardsOnTheTable[1, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                break;
            case "Second":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.GetComponent<CardCreator>().CardPosition(3);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 1] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[0, 0] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.663f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 0] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                        if (GameObjectCardsOnTheTable[0, 2] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.325f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 2] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[1, 1] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[1, i] != null && GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[1, i];
                                GameObjectCardsOnTheTable[1, i].transform.position = new Vector3(-0.494f, 1.166f, 0.827f);
                                GameObjectCardsOnTheTable[1, 1] = ObjectToTransform;
                                GameObjectCardsOnTheTable[1, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                break;
            case "Third":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.GetComponent<CardCreator>().CardPosition(4);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 2] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[0, 1] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 1] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                        if (GameObjectCardsOnTheTable[0, 3] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.156f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 3] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[1, 2] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[1, i] != null && GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[1, i];
                                GameObjectCardsOnTheTable[1, i].transform.position = new Vector3(-0.325f, 1.166f, 0.827f);
                                GameObjectCardsOnTheTable[1, 2] = ObjectToTransform;
                                GameObjectCardsOnTheTable[1, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                break;
            case "Fourth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera--;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.GetComponent<CardCreator>().CardPosition(5);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 3] = CurrentCardGameObject;
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Flying)
                    {
                        FlyingTitle.GetComponent<Animator>().Play("Flying", 0, 0f);
                        CurrentCardGameObject.transform.parent = FlyingTitle.transform;
                    }
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[0, 2] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.325f, 1.16f, 0.614f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[0, 2] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[1, 3] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[1, i] != null && GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[1, i];
                                GameObjectCardsOnTheTable[1, i].transform.position = new Vector3(-0.156f, 1.166f, 0.827f);
                                GameObjectCardsOnTheTable[1, 3] = ObjectToTransform;
                                GameObjectCardsOnTheTable[1, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
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
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[1, 1] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.494f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 1] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[0, 0] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[0, i] != null && GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[0, i];
                                GameObjectCardsOnTheTable[0, i].transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                                GameObjectCardsOnTheTable[0, 0] = ObjectToTransform;
                                GameObjectCardsOnTheTable[0, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
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
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[1, 0] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.663f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 0] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                        if (GameObjectCardsOnTheTable[1, 2] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.325f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 2] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[0, 1] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[0, i] != null && GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[0, i];
                                GameObjectCardsOnTheTable[0, i].transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                                GameObjectCardsOnTheTable[0, 1] = ObjectToTransform;
                                GameObjectCardsOnTheTable[0, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
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
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[1, 1] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.494f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 1] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                        if (GameObjectCardsOnTheTable[1, 3] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.156f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 3] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[0, 2] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[0, i] != null && GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[0, i];
                                GameObjectCardsOnTheTable[0, i].transform.position = new Vector3(-0.325f, 1.16f, 0.614f);
                                GameObjectCardsOnTheTable[0, 2] = ObjectToTransform;
                                GameObjectCardsOnTheTable[0, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
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
                    if (CurrentCardGameObject.GetComponent<CardCreator>().Barricade == true)
                    {
                        if (GameObjectCardsOnTheTable[1, 2] == null)
                        {
                            GameObject TailCard = Instantiate(CardPrefab, new Vector3(-0.325f, 1.166f, 0.827f), Quaternion.identity);
                            TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                            TailCard.GetComponent<CardCreator>().CreateCard(16);
                            GameObjectCardsOnTheTable[1, 2] = TailCard;
                            CardsToPickAndestroy.Add(TailCard);
                        }
                    }
                    CardsInHand.Remove(CurrentCardGameObject);
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                    HandCardSorter();
                    if (GameObjectCardsOnTheTable[0, 3] == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (GameObjectCardsOnTheTable[0, i] != null && GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Guard == true)
                            {
                                GameObject ObjectToTransform = GameObjectCardsOnTheTable[0, i];
                                GameObjectCardsOnTheTable[0, i].transform.position = new Vector3(-0.156f, 1.16f, 0.614f);
                                GameObjectCardsOnTheTable[0, 3] = ObjectToTransform;
                                GameObjectCardsOnTheTable[0, i] = null;
                                break;
                            }
                            else
                            {

                            }
                        }
                    }
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
                    if (GameObjectCardsOnTheTable[x, y + 1].GetComponent<CardCreator>().AntiStealth == true)
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
                    if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().AntiStealth == true)
                    {
                        CheckPlayerFrontAttacking(x, y, "", AttackDriection, -1);
                    }
                    else if (GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().AntiStealth == false)
                    {
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardX = x;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardY = y - 1;
                        AttackTitle.GetComponent<AttackingTtile>().CurrentCardAttackSpree = AttackSpree;
                        GameObjectCardsOnTheTable[x, y - 1].transform.parent = AttackTitle.transform;
                        AttackTitle.GetComponent<AttackingTtile>().Animation(AttackDriection);
                        BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y - 1].GetComponent<CardCreator>().Attack;
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
                    else if (GameObjectCardsOnTheTable[x, y - AttackSpree].GetComponent<CardCreator>().AntiStealth == false)
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
                    else if (GameObjectCardsOnTheTable[x, y - AttackSpree].GetComponent<CardCreator>().AntiStealth == false)
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
                TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                TailCard.GetComponent<CardCreator>().CreateCard(17);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                CardsToPickAndestroy.Add(TailCard);
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x + 1, y];
                GameObjectCardsOnTheTable[x + 1, y + 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x + 1, y] = TailCard;
            }
            else if (y > 0 && GameObjectCardsOnTheTable[x + 1, y - 1] == null)
            {
                GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x + 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x + 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x + 1, y].transform.position.y, GameObjectCardsOnTheTable[x + 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                TailCard.GetComponent<CardCreator>().CreateCard(17);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                CardsToPickAndestroy.Add(TailCard);
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
                GameObjectCardsOnTheTable[x + 1, y].GetComponentInChildren<Image>().DeathAnimation();
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Spikes == true)
                {
                    GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                    if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                    {
                        GameObjectCardsOnTheTable[x + 1, y] = null;
                        GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponentInChildren<Image>().DeathAnimation();
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
                        GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponentInChildren<Image>().DeathAnimation();
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
                TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                TailCard.GetComponent<CardCreator>().CreateCard(17);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                CardsToPickAndestroy.Add(TailCard);
                GameObject ObjectTransform = GameObjectCardsOnTheTable[x - 1, y];
                GameObjectCardsOnTheTable[x - 1, y + 1] = ObjectTransform;
                GameObjectCardsOnTheTable[x - 1, y] = TailCard;
            }
            else if (y > 0 && GameObjectCardsOnTheTable[x - 1, y - 1] == null)
            {
                GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Escape = false;
                GameObjectCardsOnTheTable[x - 1, y].transform.position = new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x - 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z);
                GameObject TailCard = Instantiate(CardPrefab, new Vector3(GameObjectCardsOnTheTable[x - 1, y].transform.position.x + 0.169f, GameObjectCardsOnTheTable[x - 1, y].transform.position.y, GameObjectCardsOnTheTable[x - 1, y].transform.position.z), quaternion.identity);
                TailCard.transform.rotation = Quaternion.Euler(180f, 90f, 90f);
                TailCard.GetComponent<CardCreator>().CreateCard(17);
                TailCard.GetComponent<BoxCollider>().enabled = false;
                CardsToPickAndestroy.Add(TailCard);
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
            GameObjectCardsOnTheTable[x - 1, y].GetComponentInChildren<Image>().DeathAnimation();
            int DeathSigil = GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().CurrentCardIndex;
            if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Immortal == true)
            {
                Immortal(DeathSigil);

            }
            if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Spikes == true)
            {
                GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health--;
                if (GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponent<CardCreator>().Health <= 0)
                {
                    GameObjectCardsOnTheTable[x - 1, y] = null;
                    GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponentInChildren<Image>().DeathAnimation();
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
                    GameObjectCardsOnTheTable[x, y + PowerChanger].GetComponentInChildren<Image>().DeathAnimation();
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
        if (CardsInHand.Count != 0)
        {
            Vector3 ZeroPosition = new Vector3(-0.332f, 1.442f, 0.255f);
            GameObject FirstCard = CardsInHand[0];
            FirstCard.transform.position = new Vector3(-0.332f - CardsInHand.Count * 0.001f, 1.442f, 0.255f);
            for (int i = 1; i < CardsInHand.Count; i++)
            {
                if (i % 2 == 0)
                {
                    CardsInHand[i].transform.position = FirstCard.transform.position + new Vector3(-i * 0.02f, 0f, +i * 0.006f);
                    CardsInHand[i].transform.Rotate(0.7f * i, 0f, 0f);
                }
                else
                {
                    CardsInHand[i].transform.position = FirstCard.transform.position + new Vector3(i * 0.02f, 0f, +i * 0.006f);
                    CardsInHand[i].transform.Rotate(-0.7f * i, 0f, 0f);
                }
            }
            CardsInHand[CardsInHand.Count - 1].transform.position = new Vector3(CardsInHand[CardsInHand.Count - 1].transform.position.x, CardsInHand[CardsInHand.Count - 1].transform.position.y + 2f, CardsInHand[CardsInHand.Count - 1].transform.position.z);
            StartCoroutine(CardDropDown());
        }
        else
        {

        }
    }
    IEnumerator CardDropDown()
    {
        Vector3 InitialPosition = CardsInHand[CardsInHand.Count - 1].transform.position;
        Vector3 TargetPosition = new Vector3(InitialPosition.x, InitialPosition.y - 2.0f, InitialPosition.z);
        float Duration = 1.0f;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < Duration)
        {
            CardsInHand[CardsInHand.Count - 1].transform.position = Vector3.Lerp(InitialPosition, TargetPosition, ElapsedTime / Duration);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        CardsInHand[CardsInHand.Count - 1].transform.position = TargetPosition;
    }
    public void StartingHand()
    {
        CardCollection.Add(11);
        CardCollection.Add(7);
        CardCollection.Add(3);
        CardCollection.Add(4);

        //StartCoroutine(CardDrop());
    }
    public void CardDisplay()
    {
        Camera.GetComponent<CameraMovement>().Camera = 4;
    }
    public void GenerateCard()
    {
        int x = UnityEngine.Random.Range(0, CardCollection.Count);
        GameObject NewCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), quaternion.identity);
        NewCard.transform.Rotate(0f, -90f, 0f);
        NewCard.GetComponent<CardCreator>().CreateCard(CardCollection[x]);
        CardsInHand.Add(NewCard);
        HandCardSorter();
        CardsToPickAndestroy.Add(NewCard);
        CardTemporaryBin.Add(CardCollection[x]);
        CardCollection.Remove(CardCollection[x]);
    }
    IEnumerator CardDrop()
    {
        for (int i = 0; i < CardCollection.Count; i++)
        {
            GameObject Mannequine = Instantiate(CardMannequinn, new Vector3(0.059f, 1.755f, 0.396f), quaternion.Euler(0f, -90f, 90f));
            CardStack.GetComponent<CardsOnTheTable>().CardsOnTable.Add(Mannequine);
            CardsToPickAndestroy.Add(Mannequine);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Gameplay Part Methods Here:

    public void PickCard()
    {
        PlayerManager.GetComponent<Player>().Anim.SetInteger("CardPick", 1);
        Camera.GetComponent<CameraMovement>().Camera = 2;
        for (int i = 0; i <= 2; i++)
        {
            GameObject CardPickerBlock = Instantiate(CardPicker, new Vector3(-0.663f + i * 0.2f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            CardPickerBlock.name = "CardPicker" + i;
            int x = UnityEngine.Random.Range(0, 13);
            CardPickerBlock.GetComponent<CardPicker>().CardPickerInt = x;
            CardsToPickAndestroy.Add(CardPickerBlock);
            GameObject CardToPick = Instantiate(CardPrefab, new Vector3(-0.663f + i * 0.2f, 1.16f, 0.814f), Quaternion.Euler(0f, -90f, -90f));
            CardToPick.GetComponent<BoxCollider>().enabled = false;
            CardToPick.GetComponent<CardCreator>().CreateCard(x);
            CardsToPickAndestroy.Add(CardToPick);
            StartCoroutine(CardDropOnZ(CardToPick));
            CardPickerBlock.GetComponent<CardPicker>().CurrentCard = CardToPick;
        }
    }
    IEnumerator CardDropOnZ(GameObject Card)
    {
        Vector3 InitialPosition = Card.transform.position;
        Vector3 TargetPosition = new Vector3(InitialPosition.x, InitialPosition.y, InitialPosition.z - 0.200f);
        float Duration = 1f;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < Duration)
        {
            Card.transform.position = Vector3.Lerp(InitialPosition, TargetPosition, ElapsedTime / Duration);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        Card.transform.position = TargetPosition;
    }
    public void PickCardPicked(int x)
    {
        StartCoroutine(Wait(x));
    }
    IEnumerator Wait(int x)
    {
        yield return new WaitForSeconds(2);
        PlayerManager.GetComponent<Player>().Anim.SetInteger("CardPick", 0);
        CardCollection.Add(x);
        for (int i = 0; i < CardsToPickAndestroy.Count; i++)
        {
            Destroy(CardsToPickAndestroy[i]);
        }
        CardsToPickAndestroy.Clear();
        Camera.GetComponent<CameraMovement>().Camera = 0;
        Map.GetComponent<Animator>().SetBool("Up", true);
    }
    public void DeleteCard()
    {
        Zombie.GetComponent<Animator>().SetInteger("Destroy", 1);
        Camera.GetComponent<CameraMovement>().Camera = 2;
        float StartX = -0.663f;
        float StartY = 1.4f;
        float StartZ = 0.614f;
        float CardSpacingX = 0.15f;
        float CardSpacingZ = 0.15f;
        int CardsPerRow = 5;

        for (int i = 0; i < CardCollection.Count; i++)
        {
            int Row = i / CardsPerRow;
            int Column = i % CardsPerRow;

            float x = StartX + Column * CardSpacingX;
            float z = StartZ - Row * CardSpacingZ;

            GameObject CardToBurn = Instantiate(CardPrefab, new Vector3(x, StartY, z), Quaternion.Euler(0f, -90f, -90f));
            CardToBurn.GetComponent<CardCreator>().CreateCard(CardCollection[i]);
            CardToBurn.GetComponent<CardCreator>().InHand = true;
            CardToBurn.GetComponent<CardCreator>().CardInQueue = i;
            CardsToPickAndestroy.Add(CardToBurn);
        }
    }
    public void DeleteCardFinish()
    {
        for (int i = 0; i < CardsToPickAndestroy.Count; i++)
        {
            Destroy(CardsToPickAndestroy[i]);
        }
        CardsToPickAndestroy.Clear();
        Camera.GetComponent<CameraMovement>().Camera = 0;
    }
    public IEnumerator CardToDestroy(GameObject Card)
    {
        Vector3 InitialPosition = Card.transform.position;
        Vector3 TargetPosition = new Vector3(-0.669f, 1.226f, 1.232f);
        float Duration = 2f;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < Duration)
        {
            Card.transform.position = Vector3.Lerp(InitialPosition, TargetPosition, ElapsedTime / Duration);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        Card.transform.position = TargetPosition;
        Card.transform.SetParent(ZombieHand.transform);
        Card.GetComponentInChildren<Image>().DeathAnimation();
        Zombie.GetComponent<Animator>().SetInteger("Destroy", 2);
    }

    public IEnumerator CardInZombieHand(GameObject Card)
    {
        Vector3 InitialPosition = Card.transform.position;
        Vector3 TargetPosition = new Vector3(-0.669f, 1.226f, 1.232f);
        float Duration = 1f;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < Duration)
        {
            Card.transform.position = Vector3.Lerp(InitialPosition, TargetPosition, ElapsedTime / Duration);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        Card.transform.position = TargetPosition;
        Card.GetComponentInChildren<Image>().DeathAnimation();
    }
    public void TokenTotem(int x)
    {
        CurrentTokenSpawner += x;
    }
    public void StartBattle()
    {
        int x = UnityEngine.Random.Range(0, 6);
        BattleType = x;
        Battle(BattleType, CurrentTurn);
        CardPicked = true;
        SpawnPlayerMana(CurrentTokenSpawner);
        StartCoroutine(CardDrop());
        PlayerManager.GetComponent<Player>().Anim.SetTrigger("PlayTrigger");
    }
    public void EndBattle()
    {
        PlayerManager.GetComponent<Player>().Anim.SetTrigger("IdleTrigger");
        for (int i = 0; i < CardsToPickAndestroy.Count; i++)
        {
            Destroy(CardsToPickAndestroy[i]);
        }
        CardsToPickAndestroy.Clear();
        CardStack.GetComponent<CardsOnTheTable>().CardsOnTable.Clear();
        Camera.GetComponent<CameraMovement>().Camera = 0;
        Map.GetComponent<Animator>().SetBool("Up", true);
        BoardHealth = 5;
        for (int i = 0; i < CardTemporaryBin.Count; i++)
        {
            CardCollection.Add(CardTemporaryBin[i]);
        }
        CardTemporaryBin.Clear();
        PlayerManager.GetComponent<Player>().Anim.SetTrigger("IdleTrigger");
        for (int i = 0; i < PlayerTokens.Count; i++)
        {
            Destroy(PlayerTokens[i]);
            PlayerMana--;
        }
        PlayerTokens.Clear();
        CardsInHand.Clear();
        CurrentTurn = 0;
    }
    public void NextTurn()
    {
        OpponentCardMove();
        CurrentTurn = CurrentTurn + 1;
        Battle(BattleType, CurrentTurn);
        CardPicked = true;
        SpawnPlayerMana(CurrentTokenSpawner);
        for (int i = 0; i < 4; i++)
        {
            if (GameObjectCardsOnTheTable[0, i] != null && GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().LevelUp == true)
            {
                GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().LevelUp = false;
                GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Health += 2;
                GameObjectCardsOnTheTable[0, i].GetComponent<CardCreator>().Attack += 2;
                //TurnAnimation
            }
            else
            {
                continue;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (GameObjectCardsOnTheTable[1, i] != null && GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().LevelUp == true)
            {
                GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().LevelUp = false;
                GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Health += 2;
                GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Attack += 2;
                //TurnAnimation
            }
            else
            {
                continue;
            }
        }
    }

    //Opponnts AI Cards

    public void Battle(int CurrentBattle, int CurrentTur)
    {
        // -0.663f, 1.166f, 0.827f
        // - 0.494f, 1.166f, 0.827f 
        // -0.325f, 1.166f, 0.827f 
        // -0.156f, 1.166f, 0.827f

        // Transform integer = 213
        switch (CurrentBattle)
        {
            case 0:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, -1, 0);
                            }
                            break;
                        case 1:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, -1, 0);
                            }
                            break;
                        case 2:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, -1, 1);
                                SpawnEnemyCard(-0.156f, 1.166f, 1.04f, -1, 3);
                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }
                }
                break;
            case 1:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, 2, 0);
                                SpawnEnemyCard(-0.156f, 1.166f, 1.04f, 2, 3);
                            }
                            break;
                        case 1:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, 2, 1);
                            }
                            break;
                        case 2:
                            {

                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, 2, 2);
                            }
                            break;
                    }
                }
                break;
            case 2:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, 2, 1);
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, 2, 2);
                            }
                            break;
                        case 1:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, 2, 0);
                            }
                            break;
                        case 2:
                            {

                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }
                }
                break;
            case 3:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, 3, 1);
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, 3, 2);
                            }
                            break;
                        case 1:
                            {

                            }
                            break;
                        case 2:
                            {

                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }
                }
                break;
            case 4:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, 16, 0);
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, 16, 1);
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, 16, 2);
                                SpawnEnemyCard(-0.156f, 1.166f, 1.04f, 16, 3);
                            }
                            break;
                        case 1:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, -1, 0);
                            }
                            break;
                        case 2:
                            {
                                SpawnEnemyCard(-0.156f, 1.166f, 1.04f, -1, 3);
                            }
                            break;
                        case 3:
                            {
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, -1, 2);
                            }
                            break;
                        case 4:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, -1, 1);
                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }
                }
                break;
            case 5:
                {
                    switch (CurrentTurn)
                    {
                        case 0:
                            {
                                SpawnEnemyCard(-0.494f, 1.166f, 1.04f, 7, 1);
                                SpawnEnemyCard(-0.325f, 1.166f, 1.04f, 7, 2);
                            }
                            break;
                        case 1:
                            {

                            }
                            break;
                        case 2:
                            {
                                SpawnEnemyCard(-0.663f, 1.166f, 1.04f, 6, 0);
                                SpawnEnemyCard(-0.156f, 1.166f, 1.04f, 6, 3);
                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }
                }
                break;
        }
    }
    public void Immortal(int x)
    {
        GameObject NewCard = Instantiate(CardPrefab, new Vector3(0f, 0f, 0f), quaternion.identity);
        NewCard.transform.Rotate(0f, -90f, 0f);
        NewCard.GetComponent<CardCreator>().CreateCard(x);
        CardsInHand.Add(NewCard);
        HandCardSorter();
        CardsToPickAndestroy.Add(NewCard);
    }
    public void OpponentCardMove()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameObjectCardsOnTheTable[2, i] != null && GameObjectCardsOnTheTable[2, i].GetComponent<CardCreator>().Skipper == true)
            {
                if (GameObjectCardsOnTheTable[1, i] == null)
                {
                    GameObjectCardsOnTheTable[2, i].transform.position = new Vector3(GameObjectCardsOnTheTable[2, i].transform.position.x, GameObjectCardsOnTheTable[2, i].transform.position.y, GameObjectCardsOnTheTable[2, i].transform.position.z - 0.213f);
                    GameObject CardToTransform = GameObjectCardsOnTheTable[2, i];
                    GameObjectCardsOnTheTable[1, i] = CardToTransform;
                    GameObjectCardsOnTheTable[2, i] = null;
                }
            }
        }
    }
    public void SpawnEnemyCard(float x, float y, float z, int CardNumber, int Position)
    {
        GameObject NewCardAdded = (Instantiate(CardPrefab));
        NewCardAdded.transform.position = new Vector3(x, y, z);
        NewCardAdded.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
        NewCardAdded.GetComponent<CardCreator>().CreateCard(CardNumber);
        NewCardAdded.GetComponent<CardCreator>().Skipper = true;
        NewCardAdded.GetComponent<BoxCollider>().enabled = false;
        CardsToPickAndestroy.Add(NewCardAdded);
        GameObjectCardsOnTheTable[2, Position] = NewCardAdded;
    }
    public void Death()
    {
        for (int i = 0; i < CardsToPickAndestroy.Count; i++)
        {
            Destroy(CardsToPickAndestroy[i]);
        }
        Light1.SetActive(false);
        ZombieText.SetActive(false);
        Application.Quit();
    }
}