using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Victim : MonoBehaviour
{
    private GameObject GameManager;
    public GameObject PropZombie;
    public GameObject Map;
    private void Start()
    {
        GameManager = GameObject.Find("brain_jar");
    }
    public void StartConversation()
    {
        Map.GetComponent<Map>().TutorialMap();
        PropZombie.SetActive(true);
        PropZombie.GetComponent<Zombie>().StartDialogue(9,12,3);
    }
    public void DestroyCard()
    {

    }
    public void PickCard()
    {
        PropZombie.SetActive(true);
        PropZombie.GetComponent<Zombie>().StartDialogue(20, 21, 4);
    }
    public void StartTutorial()
    {
        PropZombie.GetComponent<Zombie>().StartDialogue(13, 19, 1);
    }
}
