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
        int x = Random.Range(0, 6);
        Renderer.material = Card[x].CardMaterial;
        Debug.Log(x + " " + Card[x].Health);
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
        transform.position = new Vector3(-0.327f, 1.15f, 0.7635601f);
        transform.rotation = Quaternion.Euler(0f, -90f, -90f);
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