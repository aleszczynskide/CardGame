using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardsOnTheTable : MonoBehaviour
{
    private GameObject GameManager;
    public List<GameObject> CardsOnTable;
    void Start()
    {
        GameManager = GameObject.Find("brain_jar");
    }
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (CardsOnTable[CardsOnTable.Count-1] != null)
        {
            GameManager.GetComponent<GameManager>().GenerateCard();
            Destroy(CardsOnTable[CardsOnTable.Count-1]);
            CardsOnTable.RemoveAt(CardsOnTable.Count-1);
        }
        else
        {
            Debug.Log("Nie masz kart");
        }
    }
}
