using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPicker : MonoBehaviour
{
    public int CardPickerInt;
    public GameObject GameManager;
    void Start()
    {
        GameManager = GameObject.Find("brain_jar");
    }
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        GameManager.GetComponent<GameManager>().PickCardPicked(CardPickerInt);
    }
}
