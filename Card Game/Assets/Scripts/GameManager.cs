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
            for (int i = 0;i < 4;i++) 
            { 
               if (GameObjectCardsOnTheTable[1,i] != null)
                {
                    Debug.Log("Karta :" + 1 + " " + i + " flying; " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Flying + " Antiflying" + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiFlying + "Karta :" + 1 + " " + i + " Stelth: " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Stealth + " AntiStealth : " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiStealth);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (GameObjectCardsOnTheTable[0, i] != null)
                {
                    Debug.Log("Karta :" + 1 + " " + i + " flying; " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Flying + " Antiflying" + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiFlying + "Karta :" + 1 + " " + i + " Stelth: " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().Stealth + " AntiStealth : " + GameObjectCardsOnTheTable[1, i].GetComponent<CardCreator>().AntiStealth);
                }
            }
        }
    }
    public void BoardMove()
    {
        Camera.GetComponent<CameraMovement>().Camera = 2;

        for (int i = 0; i <= 3; i++)
        {
            if (GameObjectCardsOnTheTable[0, i] != null)
            {
                CardPlayerAttack(0, i);
            }
            else
            {
                continue;
            }
        }
        for (int i = 0; i <= 3; i++)
        {
            if (GameObjectCardsOnTheTable[1, i] != null)
            {
               CardOpponentAttack(1, i);
            }
            else
            {
                continue;
            }
        }
        Camera.GetComponent<CameraMovement>().Camera = 0;
        Debug.Log(BoardHealth);

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
    }

    public void SpawnPointerActivated(string SpawnerName)
    {

        switch (SpawnerName)
        {
            case "First":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera --;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 0] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Second":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera --;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 1] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Third":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera --;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    GameObjectCardsOnTheTable[0, 2] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Fourth":
                {
                    ManaMinus(CurrentCardCard.Cost);
                    Camera.GetComponent<CameraMovement>().Camera --;
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.159f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
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
    public void CheckNormal(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x + 1, y] != null)
        {
            if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
            {
                Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                GameObjectCardsOnTheTable[x + 1, y] = null;
            }
            else
            {
                GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
            }
        }
        if (GameObjectCardsOnTheTable[x + 1, y] == null)
        {
            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
        }
    }
    public void CardPlayerAttack(int x, int y)
    {
        if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 1)
        {
            PlayerAttack(x, y);
        }
        else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 2)
        {
            if (y <= 4 && y >= 0)
            {
                PlayerAttack(x, y - 1);
            }
            if (y <= 4 && y >= 0)
            {
                PlayerAttack(x, y + 1);
            }
            
        }
        else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 3)
        {
            if (y <= 4 && y >= 0)
            {
                PlayerAttack(x, y - 1);
            }
            if (y <= 4 && y >= 0)
            {
                PlayerAttack(x, y + 1);
            }
            PlayerAttack(x, y);
        }
    }

    public void PlayerAttack(int x,int y)
    {
        if (GameObjectCardsOnTheTable[x + 1, y] == null)
        {
            BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
        }
        else if (GameObjectCardsOnTheTable[x + 1, y] != null)
        {
            if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Flying == true)
            {
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                    {
                        Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                        GameObjectCardsOnTheTable[x + 1, y] = null;
                    }
                    else
                    {
                        GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().AntiFlying == false)
                {
                    BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Stealth == true)
            {
                if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AntiStealth == true)
                {
                    if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                    {
                        Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                        GameObjectCardsOnTheTable[x + 1, y] = null;
                    }
                    else
                    {
                        GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AntiStealth == false)
                {
                    BoardHealth = BoardHealth + GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else
            {
                if (GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                {
                    Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                    GameObjectCardsOnTheTable[x + 1, y] = null;
                }
                else
                {
                    GameObjectCardsOnTheTable[x + 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
        }
    }
    public void CardOpponentAttack(int x,int y)
    {
        if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 1)
        {
            OpponentAttack(x, y);
        }
        else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 2)
        {
            if (y <= 4 && y >= 0)
            {
                OpponentAttack(x - 1, y - 1);
            }
            if (y <= 4 && y >= 0)
            {
                OpponentAttack(x - 1, y + 1);
            }

        }
        else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AttackRange == 3)
        {
            if (y <= 4 && y >= 0)
            {
                OpponentAttack(x - 1, y - 1);
            }
            if (y <= 4 && y >= 0)
            {
                OpponentAttack(x - 1, y + 1);
            }
            OpponentAttack(x - 1, y);
        }
    }
    public void OpponentAttack(int x,int y)
    {
        if (GameObjectCardsOnTheTable[x - 1, y] == null)
        {
            BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
        }
        else if (GameObjectCardsOnTheTable[x - 1, y] != null)
        {
            if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Flying == true)
            {
                if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                    {
                        Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                        GameObjectCardsOnTheTable[x - 1, y] = null;
                    }
                    else
                    {
                        GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().AntiFlying == false)
                {
                    BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Stealth == true)
            {
                if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AntiStealth == true)
                {
                    if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                    {
                        Destroy(GameObjectCardsOnTheTable[x + 1, y]);
                        GameObjectCardsOnTheTable[x - 1, y] = null;
                    }
                    else
                    {
                        GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                    }
                }
                else if (GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().AntiStealth == false)
                {
                    BoardHealth = BoardHealth - GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
            }
            else
            {
                if (GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health <= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack)
                {
                    Destroy(GameObjectCardsOnTheTable[x - 1, y]);
                    GameObjectCardsOnTheTable[x - 1, y] = null;
                }
                else
                {
                    GameObjectCardsOnTheTable[x - 1, y].GetComponent<CardCreator>().Health -= GameObjectCardsOnTheTable[x, y].GetComponent<CardCreator>().Attack;
                }
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