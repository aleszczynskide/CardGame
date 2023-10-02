using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropZombie : MonoBehaviour
{
    public GameObject Plane;
    public GameObject CameraManager;
    public GameObject GameManager;
    public GameObject PlayerManager;
    public void StartGame()
    {
        Plane.SetActive(true);
        Plane.GetComponent<Zombie>().StartDialogue(0,5);
        CameraManager.GetComponent<CameraMovement>().Camera = 3;
       
    }
    public void StartPlayer()
    {
        CameraManager.GetComponent<CameraMovement>().Camera = 0;
        PlayerManager.GetComponent<Player>().Anim.SetTrigger("PlayTrigger");
        GameManager.GetComponent<GameManager>().StartingHand();
    }
}
