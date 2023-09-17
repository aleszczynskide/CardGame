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
    }
    public void BoardMove()
    {
        Camera.GetComponent<CameraMovement>().Camera = 2;

        CheckPlayerCard(0, 0);
        CheckPlayerCard(0, 1);
        CheckPlayerCard(0, 2);
        CheckPlayerCard(0, 3);

        CheckRobberCard(1, 0);
        CheckRobberCard(1, 1);
        CheckRobberCard(1, 2);
        CheckRobberCard(1, 3);

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
        CheckPlayerAttack(x, y);
    }

    public void CheckRobberCard(int x, int y)
    {
        CheckRobberAttack(x, y);
    }
    public void CheckPlayerAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y] != null)
        {
            if (GameObjectCardsOnTheTable[x + 1, y] == null)
            {
                //Attack Robber animations and stuff
                BoardHealth = BoardHealth + CardListBattleCards[x, y].Attack;
            }
            else if (GameObjectCardsOnTheTable[x + 1, y] != null)
            {
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
               
            }
        }

    }
    public void CheckPlayerPower()
    {

    }

    public void CheckRobberAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y] != null)
        {
            if (GameObjectCardsOnTheTable[x - 1, y] == null)
            {
                //Attack Robber animations and stuff
                BoardHealth = BoardHealth + CardListBattleCards[x, y].Attack;
            }
            else if (GameObjectCardsOnTheTable[x - 1, y] != null)
            {
                //Attack card animations
                if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                {
                    Debug.Log(GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health);
                    Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                    GameObjectCardsOnTheTable[x - 1, y] = null;
                    CardListBattleCards[x - 1, y] = null;
                }
                else
                {
                    GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else
            {

            }
        }
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