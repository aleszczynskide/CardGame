using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CardCreator : MonoBehaviour
{
    public List<Card> Card;
    Renderer Renderer;
    private int CurrentCardIndex;
    public GameObject GameManager;
    public GameObject CurrentCard;
    public int Health, Attack, AttackRange;
    public bool Flying, AntiFlying, Stealth, AntiStealth, Shield, Move, MoveLeft, Push, PushLeft,Escape;
    public bool BoxActivator = true;
    public GameObject HealthBar;
    public GameObject AttackBar;
    private TextMeshProUGUI TextMeshPro;
    void Start()
    {
       
    }
    public void Update()
    {
        HealthBar.GetComponent<TextMeshPro>().text = "" + Health;
        AttackBar.GetComponent<TextMeshPro>().text = "" + Attack;
    }
    public void CreateCard(int x)
    {
        GameManager = GameObject.Find("brain_jar");
        Renderer = GetComponent<Renderer>();
        if (x == -1)
        {
            x = Random.Range(6, 10);
            Renderer.material = Card[x].CardMaterial;
            Health = Card[x].Health;
            Attack = Card[x].Attack;
            Flying = Card[x].Flying;
            AntiFlying = Card[x].AntiFlying;
            Stealth = Card[x].Stealth;
            Shield = Card[x].Shield;
            AntiStealth = Card[x].AntiStealth;
            AttackRange = Card[x].AttackRange;
            Move = Card[x].Move;
            MoveLeft = Card[x].MoveLeft;
            Push = Card[x].Push;
            PushLeft = Card[x].PushLeft;
            Escape = Card[x].Escape;
            CurrentCard = this.gameObject;
            CurrentCardIndex = x;
        }
        else if (x >= 0)
        {
            Renderer.material = Card[x].CardMaterial;
            Health = Card[x].Health;
            Attack = Card[x].Attack;
            Flying = Card[x].Flying;
            AntiFlying = Card[x].AntiFlying;
            Stealth = Card[x].Stealth;
            Shield = Card[x].Shield;
            AntiStealth = Card[x].AntiStealth;
            AttackRange = Card[x].AttackRange;
            Move = Card[x].Move;
            MoveLeft = Card[x].MoveLeft;
            Push = Card[x].Push;
            PushLeft = Card[x].PushLeft;
            Escape = Card[x].Escape;
            CurrentCard = this.gameObject;
            CurrentCardIndex = x;
        }
    
    }
    private void OnMouseUp()
    {
        GameManager.GetComponent<GameManager>().CardPlace(Card[CurrentCardIndex], CurrentCard);
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