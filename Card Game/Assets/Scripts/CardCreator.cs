using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;

public class CardCreator : MonoBehaviour
{
    Animator Anim;
    public List<Card> Card;
    Renderer Renderer;
    [HideInInspector] public int CurrentCardIndex;
    public int CardInQueue;
    public GameObject GameManager;
    public GameObject CurrentCard;
    public int Health, Attack, AttackRange;
    public bool Flying, AntiFlying, Stealth, AntiStealth, Shield, Move, MoveLeft, Push, PushLeft,Escape,Spikes,TwoTokens,Barricade,Guard,LevelUp,Immortal,Skipper; //Create an list of it
    public bool BoxActivator = true;
    public GameObject HealthBar;
    public GameObject AttackBar;
    private TextMeshProUGUI TextMeshPro;
    public bool CurrentCardUp = false;
    public bool InHand;
    void Start()
    {
        Anim = GetComponent<Animator>();
        GameManager = GameObject.Find("brain_jar");
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
            x = Random.Range(9, 17);
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
            Spikes = Card[x].Spikes;
            TwoTokens = Card[x].TwoTokens;
            Barricade = Card[x].Barricade;
            Guard = Card[x].Guard;
            LevelUp = Card[x].LeveUp;
            Immortal = Card[x].Immortal;
            Skipper = Card[x].OpponentSkipper;
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
            Spikes = Card[x].Spikes;
            TwoTokens = Card[x].TwoTokens;
            Barricade = Card[x].Barricade;
            Guard = Card[x].Guard;
            LevelUp = Card[x].LeveUp;
            Immortal = Card[x].Immortal;
            Skipper = Card[x].OpponentSkipper;
            CurrentCard = this.gameObject;
            CurrentCardIndex = x;
        }
    
    }
    private void OnMouseUp()
    {
        if (!InHand)
        {
            GameManager.GetComponent<GameManager>().CardPlace(Card[CurrentCardIndex], CurrentCard);
        }  
        else if (InHand)
        {
            GameManager.GetComponent<GameManager>().TokenTotem(Card[CurrentCardIndex].Cost);
            GameManager.GetComponent<GameManager>().DeleteCardFinish();
            GameManager.GetComponent<GameManager>().CardCollection.RemoveAt(CardInQueue);
        }
    }
    private void OnMouseEnter()
    {
        if (BoxActivator == true)
        {
            transform.position = new Vector3(transform.position.x ,transform.position.y , transform.position.z - 0.08f);
        }

    }
    private void OnMouseExit()
    {

        if (BoxActivator == true)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y , transform.position.z + 0.08f);
            CurrentCardUp = false;
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void GameCardReady()
    {
        Anim.SetInteger("Position", 1);
    }
    public void CardPosition(int x)
    {
        Anim.SetInteger("Position", x);
    }
    public void TurnOffAnimator()
    {
        Anim.enabled = false;
    }
}