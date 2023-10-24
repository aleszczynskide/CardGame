using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject Light1, Light2,Camera,Zombie;
    public void SinglePLayer()
    {
        Zombie.GetComponent<Animator>().enabled = true;
        Camera.GetComponent<CameraMovement>().Camera = 0;
        Light2.SetActive(true);
        Animator Anim;
        Anim = GetComponent<Animator>();
        Anim.enabled = false;
        Light1.SetActive(false);
    }
    public void MultiPlayer()
    {

    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void LightOn()
    {
        Light1.SetActive(true);
    }
    public void LightOff() 
    {
        Light1.SetActive(false);
    }
}
