using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardCreator : MonoBehaviour
{
    public List<Card> Card;
    Renderer Renderer;
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        int x = Random.Range(0, 2);
        Renderer.material = Card[x].CardMaterial;
        Debug.Log(x + " " + Card[x].Health);
    }

    public void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
