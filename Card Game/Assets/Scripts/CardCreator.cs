using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardCreator : MonoBehaviour
{
    public List<Card> Card;
    public int CurrentCardIndex;
    Renderer Renderer;
    public GameObject GameManager;
    public GameObject CurrentCard;
    void Start()
    {
        GameManager = GameObject.Find("brain_jar");
        Renderer = GetComponent<Renderer>();
        CurrentCardIndex = Random.Range(0, 6);
        Renderer.material = Card[CurrentCardIndex].CardMaterial;
        CurrentCard = this.gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnMouseUp()
    {
        GameManager.GetComponent<GameManager>().CardPlace(Card[CurrentCardIndex],CurrentCard);
    }

   private void OnMouseEnter()
    {
        transform.position = new Vector3(transform.position.x, 1.511f, transform.position.z);
    }
    private void OnMouseExit()
    {
        transform.position = new Vector3(transform.position.x, 1.42f, transform.position.z);
    }
}