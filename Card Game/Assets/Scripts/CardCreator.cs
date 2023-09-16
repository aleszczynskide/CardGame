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
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("W³¹czy³em");
        transform.position = new Vector3(-0.327f, 1.16f, 0.918f);
        transform.rotation = Quaternion.Euler(0f, -90f, -90f);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Jestem");
        transform.localScale = new Vector3(0.007957266f, 0.25f, 0.25f);
    }
    private void OnMouseExit()
    {
        transform.localScale = new Vector3(0.007957266f, 0.1894263f, 0.1408374f);
    }
}