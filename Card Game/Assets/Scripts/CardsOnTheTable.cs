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
        GameManager.GetComponent<GameManager>().GenerateCard();
    }
}
