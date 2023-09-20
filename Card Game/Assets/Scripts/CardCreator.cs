using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardCreator : MonoBehaviour
{
    public List<Card> Card;
    public int CurrentCardIndex;
    Renderer Renderer;
    public GameObject GameManager;
    public GameObject CurrentCard;
    public int Health,Attack,AttackRange;
    public bool Flying, AntiFlying, Stealth, AntiStealth;
    public bool BoxActivator = true;
    void Start()
    {
        CreateCard();
    }
    public void Update()
    {
     
    }
    public void CreateCard()
    {
        GameManager = GameObject.Find("brain_jar");
        Renderer = GetComponent<Renderer>();
        CurrentCardIndex = Random.Range(0, 2);
        Renderer.material = Card[CurrentCardIndex].CardMaterial;
        Health = Card[CurrentCardIndex].Health;
        Attack = Card[CurrentCardIndex].Attack;
        Flying = Card[CurrentCardIndex].Flying;
        AntiFlying = Card[CurrentCardIndex].AntiFlying;
        Stealth = Card[CurrentCardIndex].Stealth;
        AntiStealth = Card[CurrentCardIndex].AntiStealth;
        AttackRange = Card[CurrentCardIndex].AttackRange;
        CurrentCard = this.gameObject;
    }

    private void OnMouseUp()
    {
        GameManager.GetComponent<GameManager>().CardPlace(Card[CurrentCardIndex],CurrentCard);
    }

   private void OnMouseEnter()
    {
        if (BoxActivator == true)
        {
            transform.position = new Vector3(transform.position.x, 1.511f, transform.position.z);
        }
       
    }
    private void OnMouseExit()
    {

        if (BoxActivator == true)
        {
            transform.position = new Vector3(transform.position.x, 1.42f, transform.position.z);

        }
    }
}