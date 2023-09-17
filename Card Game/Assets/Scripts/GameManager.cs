using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[,] BattleCards = new GameObject[3,4];
    public List<GameObject> SpawningPoints;
    public List<GameObject> PlayerTokens;
    public GameObject Camera;
    private int BoardHealth = 5;
    public int PlayerMana;
    public GameObject PlayerManaPoints;
    public GameObject SpawnPoints;
    public GameObject CurrentCardPicked;

    void Start()
    {
        SpawnPlayerMana(1);
    }
    void Update()
    {
        
    }
    public void BoardMove()
    {
        Camera.GetComponent<CameraMovement>().Camera = 2;
    }
    public void CardPlace(Card CurrentCardCard,GameObject CurrentPickedCard)
    {
        if (CurrentCardCard.Cost <= PlayerMana)
        {
            Camera.GetComponent<CameraMovement>().Camera = 2;
            CurrentCardPicked = CurrentPickedCard;
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
        if (BattleCards[0,0] == null)
        {
           GameObject SpawnPoint =  Instantiate(SpawnPoints, new Vector3(-0.663f, 1.16f, 0.614f),Quaternion.Euler(0f,-90f,-90f));
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0,1] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.494f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0, 2] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.328f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawningPoints.Add(SpawnPoint);
        }
        if (BattleCards[0, 3] == null)
        {
            GameObject SpawnPoint = Instantiate(SpawnPoints, new Vector3(-0.159f, 1.16f, 0.614f), Quaternion.Euler(0f, -90f, -90f));
            SpawningPoints.Add(SpawnPoint);
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