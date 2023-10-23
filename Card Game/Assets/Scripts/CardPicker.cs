using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPicker : MonoBehaviour
{
    public int CardPickerInt;
    public GameObject GameManager;
    private GameObject Player;
    public GameObject CurrentCard;
    void Start()
    {
        Player = GameObject.Find("Player");
        GameManager = GameObject.Find("brain_jar");
    }
    void Update()
    {

    }
    private void OnMouseDown()
    {
        GameManager.GetComponent<GameManager>().PickCardPicked(CardPickerInt);
        switch (this.name)
        {
            case ("CardPicker0"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 4);
                    StartCoroutine(CardDrag(CurrentCard));
                }
                break;

            case ("CardPicker1"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 5);
                    StartCoroutine(CardDrag(CurrentCard));
                }
                break;
            case ("CardPicker2"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 6);
                    StartCoroutine(CardDrag(CurrentCard));
                }
                break;
        }
    }
    private void OnMouseEnter()
    {
        switch (this.name)
        {
            case ("CardPicker0"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 1);
                }
                break;

            case ("CardPicker1"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 2);
                }
                break;
            case ("CardPicker2"):
                {
                    Player.GetComponent<Animator>().SetInteger("CardPick", 3);
                }
                break;
        }
    }
    IEnumerator CardDrag(GameObject Card)
    {
        Vector3 InitialPosition = Card.transform.position;
        Vector3 TargetPosition = new Vector3(InitialPosition.x, InitialPosition.y, InitialPosition.z - 0.200f);
        float Duration = 1f;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < Duration)
        {
            Card.transform.position = Vector3.Lerp(InitialPosition, TargetPosition, ElapsedTime / Duration);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }
        Card.transform.position = TargetPosition;
    }
}
