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
        PropZombie.GetComponent<Zombie>().StartDialogue(9,11 ,0 );
    }
}
