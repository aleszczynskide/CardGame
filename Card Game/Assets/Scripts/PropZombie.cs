using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropZombie : MonoBehaviour
{
    public GameObject Plane;
    void Start()
    {

    }
    void Update()
    {

    }
    public void StartGame()
    {
        Plane.SetActive(true);
        Plane.GetComponent<Zombie>().StartDialogue();
    }
}
