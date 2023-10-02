using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public Text ZombieText;
    public string[] ZombieDialogue;
    public int TextSpeed;
    public int Index;
    public GameObject Opponent;
    void Start()
    {
        ZombieText.text = string.Empty;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ZombieText.text == ZombieDialogue[Index]) 
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                ZombieText.text = ZombieDialogue[Index];
            }
        }
    }
    public void StartDialogue()
    {
        Index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in ZombieDialogue[Index].ToCharArray()) 
        {
            ZombieText.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
    public void NextLine()
    {
        if (Index < ZombieDialogue.Length - 1)
        {
            Index++;
            ZombieText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Opponent.GetComponent<PropZombie>().StartPlayer();
            gameObject.SetActive(false);
        }
    }
}
