using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawner : MonoBehaviour
{
    public List<Card> CardsList;
    public GameObject CardPrefab;
    public GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("brain_jar");
    }
    private void OnMouseDown()
    {
       // DemoScript();
    }
    public void DemoScript()
    {
        int x = Random.Range(0, 4);
        switch (x)
        {
            case 0:
                {
                    GameObject OppositeCard = Instantiate(CardPrefab, new Vector3(-0.668f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
                    OppositeCard.GetComponent<CardCreator>().CreateCard(-1);
                    OppositeCard.GetComponent<BoxCollider>().enabled = false;
                    GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[1, 0] = OppositeCard;
                }
                break;
            case 1:
                {
                    GameObject OppositeCard = Instantiate(CardPrefab, new Vector3(-0.497f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
                    OppositeCard.GetComponent<CardCreator>().CreateCard(-1);
                    OppositeCard.GetComponent<BoxCollider>().enabled = false;
                    GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[1, 1] = OppositeCard;
                }
                break;
            case 2:
                {
                    GameObject OppositeCard = Instantiate(CardPrefab, new Vector3(-0.33f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
                    OppositeCard.GetComponent<CardCreator>().CreateCard(-1);
                    OppositeCard.GetComponent<BoxCollider>().enabled = false;
                    GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[1, 2] = OppositeCard;
                }
                break;
            case 3:
                {
                    GameObject OppositeCard = Instantiate(CardPrefab, new Vector3(-0.165f, 1.166f, 0.827f), Quaternion.Euler(0f, -90f, -90f));
                    OppositeCard.GetComponent<CardCreator>().CreateCard(-1);
                    OppositeCard.GetComponent<BoxCollider>().enabled = false;
                    GameManager.GetComponent<GameManager>().GameObjectCardsOnTheTable[1,3] = OppositeCard;
                }
                break;
        }
    }
}
