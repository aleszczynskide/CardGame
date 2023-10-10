using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu (fileName = "Card",menuName = "Card/NewCard")]
public class Card : ScriptableObject
{
    public Material CardMaterial;

    public int Cost;
    public int Attack;
    public int Health;

    [Header("Special Powers")]
    public bool Flying;
    public bool AntiFlying;
    public bool Stealth;
    public bool AntiStealth;
    public int AttackRange;
    public bool Shield;
    public bool Move;
    public bool MoveLeft;
    public bool Push;
    public bool PushLeft;
    public bool Escape;
    public bool Spikes;
    public bool TwoTokens;
    public bool Barricade;
    public bool Guard;
}
