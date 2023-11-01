using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    private int Changer;
    public GameObject Mapa;
    public Text ZombieText;
    public string[] ZombieDialogue;
    public int TextSpeed;
    public int Index;
    public int EndingNode;
    public GameObject Opponent;
    public GameObject GameManager;
    void Start()
    {
        Mapa = GameObject.Find("MapTile");
        ZombieText.text = string.Empty;
        GameManager = GameObject.Find("brain_jar");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ZombieText.text == ZombieDialogue[Index]) 
            {
                NextLine(Changer);
            }
            else
            {
                StopAllCoroutines();
                ZombieText.text = ZombieDialogue[Index];
            }
        }
    }
    public void StartDialogue(int StartingNote,int y,int Kill)
    {
        EndingNode = y;
        Index = StartingNote;
        StartCoroutine(TypeLine(Kill));
        Changer = Kill;
    }
    IEnumerator TypeLine(int x)
    {
        foreach(char c in ZombieDialogue[Index].ToCharArray()) 
        {
            ZombieText.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
    public void NextLine(int Over)
    {
        if (Index < EndingNode)
        {
            Index++;
            ZombieText.text = string.Empty;
            StartCoroutine(TypeLine(0));
        }
        else if (Index == EndingNode)
        {
            if (Over == 0)
            {
                StopAllCoroutines();
                Index = 40;
                ZombieText.text = ZombieDialogue[Index];
                Mapa.GetComponent<Animator>().SetBool("up", true);
            }
            if (Over == 1)
            {
                Opponent.GetComponent<PropZombie>().StartPlayer();
                gameObject.SetActive(false);
            }
            if (Over == 2)
            {
                GameManager.GetComponent<GameManager>().Death();
            }
            if (Over == 3)
            {
                GameManager.GetComponent<GameManager>().StartTutorial();
            }
            if (Over == 4)
            {
                Index = 40;
                this.gameObject.SetActive(false);
            }
        }
    }
}
