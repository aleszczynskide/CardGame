using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Card",menuName = "Card/NewCard")]
public class Card : ScriptableObject
{
    public Material CardMaterial;

    public int Cost;
    public int Attack;
    public int Health;
}
 