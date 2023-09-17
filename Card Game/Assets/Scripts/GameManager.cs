using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Card[,] BattleCards = new Card[3, 4];
    public GameObject[,] CardsOnTheTable = new GameObject[3, 4];
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
        SpawnPlayerMana(1);
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
    }
    public void BoardMove()
    {
        Camera.GetComponent<CameraMovement>().Camera = 2;

        //Instancja walki
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
        if (BattleCards[0, 0] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.663f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "First";
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0, 1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Second";
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.328f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawnPoint.name = "Third";
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0, 3] == null)
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
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.663f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    BattleCards[0, 0] = CurrentCardCard;
                    CardsOnTheTable[0, 0] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                } 
                break;
            case "Second":
                {
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.494f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    BattleCards[0, 1] = CurrentCardCard;
                    CardsOnTheTable[0, 1] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Third":
                {
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.328f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    BattleCards[0, 2] = CurrentCardCard;
                    CardsOnTheTable[0, 2] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
            case "Fourth":
                {
                    CurrentCardGameObject.GetComponent<BoxCollider>().enabled = false;
                    CurrentCardGameObject.transform.position = new Vector3(-0.159f, 1.16f, 0.614f);
                    CurrentCardGameObject.transform.rotation = Quaternion.Euler(0f, -90f, -90f);
                    BattleCards[0, 3] = CurrentCardCard;
                    CardsOnTheTable[0, 3] = CurrentCardGameObject;
                    CurrentCardCard = null;
                    CurrentCardGameObject = null;
                }
                break;
        }
        for (int i = 0; SpawningPoints.Count > i; i++)
        {
            Destroy(SpawningPoints[i]);
        }
    }
    private void OnMouseDown()
    {
        BoardMove();
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