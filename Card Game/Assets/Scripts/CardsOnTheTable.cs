using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardsOnTheTable : MonoBehaviour
{
    public GameObject CardSorter;
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
            if (GameManager.GetComponent<GameManager>().CardPicked == true)
            {
                GameManager.GetComponent<GameManager>().CardPicked = false;
                GameManager.GetComponent<GameManager>().GenerateCard();
                CardsOnTable[CardsOnTable.Count - 1].GetComponent<Rigidbody>().useGravity = false;
                CardsOnTable[CardsOnTable.Count - 1].transform.SetParent(CardSorter.transform);
                CardSorter.GetComponent<Animator>().SetBool("Up", true);
                StartCoroutine(Flying());
            }
            else
            {
                Debug.Log("You already picked a card");
            }
        }
        else
        {
            Debug.Log("Nie masz kart");
        }
    }
    IEnumerator Flying()
    {
        yield return new WaitForSeconds(3);
        Destroy(CardsOnTable[CardsOnTable.Count - 1]);
        CardsOnTable.RemoveAt(CardsOnTable.Count - 1);
        CardSorter.GetComponent<Animator>().SetBool("Up", false);
    }
}
