using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Card[,] CardListBattleCards = new Card[3, 4];
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

    void Start()
    {
        SpawnPlayerMana(6);
    }
    void Update()
    {
        if (BoardHealth == 0)
        {
            Debug.Log("Wygrali Bandyci");
        }
        else if (BoardHealth == 10)

        {
            Debug.Log("Wygrali Policjanci");
        }
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("Karta z po³o¿enia :" +0 + " oraz " + 0 + "Równa siê :" + CardListBattleCards[0, 0]);
            Debug.Log("Karta z po³o¿enia :" + 0 + " oraz " + 1 + "Równa siê :" + CardListBattleCards[0, 1]);
            Debug.Log("Karta z po³o¿enia :" + 0 + " oraz " + 2 + "Równa siê :" + CardListBattleCards[0, 2]);
            Debug.Log("Karta z po³o¿enia :" + 0 + " oraz " + 3 + "Równa siê :" + CardListBattleCards[0, 3]);
            Debug.Log("Karta z po³o¿enia :" + 1 + " oraz " + 0 + "Równa siê :" + CardListBattleCards[1, 0]);
            Debug.Log("Karta z po³o¿enia :" + 1 + " oraz " + 1 + "Równa siê :" + CardListBattleCards[1, 1]);
            Debug.Log("Karta z po³o¿enia :" + 1 + " oraz " + 2 + "Równa siê :" + CardListBattleCards[1, 2]);
            Debug.Log("Karta z po³o¿enia :" + 1 + " oraz " + 3 + "Równa siê :" + CardListBattleCards[1, 3]);
            Debug.Log("Karta z po³o¿enia :" + 2 + " oraz " + 0 + "Równa siê :" + CardListBattleCards[2, 0]);
            Debug.Log("Karta z po³o¿enia :" + 2 + " oraz " + 1 + "Równa siê :" + CardListBattleCards[2, 1]);
            Debug.Log("Karta z po³o¿enia :" + 2 + " oraz " + 2 + "Równa siê :" + CardListBattleCards[2, 2]);
            Debug.Log("Karta z po³o¿enia :" + 2 + " oraz " + 3 + "Równa siê :" + CardListBattleCards[2, 3]);



        }

    }
    public void BoardMove()
    {
        Camera.GetComponent<CameraMovement>().Camera = 2;

        CheckPlayerCard(0, 0);
        CheckPlayerCard(0, 1);
        CheckPlayerCard(0, 2);
        CheckPlayerCard(0, 3);

        Camera.GetComponent<CameraMovement>().Camera = 0;

    }
    public void CardPlace(Card CurrentCardCard, GameObject CurrentPickedCard)
    {
        if (CurrentCardCard.Cost <= PlayerMana)
        {
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
        if (CardListBattleCards[0, 0] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.663f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "First";
            SpawningPoints.Add(SpawnPoint);
        }
        if (CardListBattleCards[0, 1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Second";
            SpawningPoints.Add(SpawnPoint);
        }
        if (CardListBattleCards[0, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.328f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Third";
            SpawningPoints.Add(SpawnPoint);
        }
        if (CardListBattleCards[0, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.159f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Fourth";
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
                    Camera.GetComponent<CameraMovement>().Camera = 0;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    CardListBattleCards[0, 0] = CurrentCardCard;
                    GameObjectCardsOnTheTable[0, 0] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Second":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera = 0;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    CardListBattleCards[0, 1] = CurrentCardCard;
                    GameObjectCardsOnTheTable[0, 1] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Third":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera = 0;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    CardListBattleCards[0, 2] = CurrentCardCard;
                    GameObjectCardsOnTheTable[0, 2] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Fourth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera = 0;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.159f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    CardListBattleCards[0, 3] = CurrentCardCard;
                    GameObjectCardsOnTheTable[0, 3] = CurrentCardGameObject;
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
        BoardMove();
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

    public void CheckPlayerCard(int x, int y)
    {
        CheckAttack(x, y);
    }
    public void CheckAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y] != null)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                //Attack Robber animations and stuff
                BoardHealth = BoardHealth + CardListBattleCards[x, y].Attack;
                Debug.Log("Buzie widze w tym teczu");

            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
                Debug.Log("Janiewidze");
                
                //Attack card animations
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                {
                    Debug.Log(GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health);
                    Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                    GameObjectCardsOnTheTable[x + 1, y] = null;
                    CardListBattleCards[x + 1, y] = null;
                }
                else
                {
                    GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else
            {
                Debug.Log("Brak karty");
            }
        }

    }
    public void CheckPower()
    {

    }
}


/*
 * 
 * 
 * 
 * 
 * 
 * 
 */