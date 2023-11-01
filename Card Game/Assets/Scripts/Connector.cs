using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Connector : MonoBehaviour
{
    GameObject Victim;
    public int YourNumber;
    public List<GameObject> ConnectedNode;
    private GameObject MapTile;
    public List<Sprite> Sprites;
    SpriteRenderer Sprite;
    [HideInInspector] public int State;
    GameObject Statue;
    GameObject GameManager;
    void Start()
    {
        Victim = GameObject.Find("Stroke Shaking Head(1)");
        Sprite = GetComponent<SpriteRenderer>();
        MapTile = GameObject.Find("MapTile");
        if (this.name == "Fight")
        {
            Sprite.sprite = Sprites[0];
            State = 0;
        }
        else if (this.name == "Chance")
        {
            int x = Random.Range(1, 3);
            Sprite.sprite = Sprites[x];
            State = x;
        }
        if (this.name == "Boss")
        {
            Sprite.sprite = Sprites[0];
            State = 3;
        }
        if (this.name == "Tutorial")
        {
            switch (YourNumber)
            {
                case 0:
                    {
                        State = 1;
                        Sprite.sprite = Sprites[1];
                      
                    }
                    break;
                case 1:
                    {
                        State = 2;
                        Sprite.sprite = Sprites[2];
                    }
                    break;
                case 2:
                    {
                        State = 0;
                        Sprite.sprite = Sprites[0];
                    }
                    break;
            }
        }
    }
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (this.name == "Start")
        {
            MapTile.GetComponent<Map>().StatueSpawner(this.transform);
            MapTile.GetComponent<Map>().CurrentPointer = this.gameObject;
            MapTile.GetComponent<Map>().UpdateMap();
        }
        else
        {
            Statue = GameObject.Find("Statue(Clone)");
            MapTile.GetComponent<Map>().CurrentPointer = this.gameObject;
            MapTile.GetComponent<Map>().UpdateMap();
            Statue.GetComponent<Statue>().Pointer = MapTile.GetComponent<Map>().CurrentPointer;
            Statue.GetComponent<Statue>().NextMove();
        }
    }
    public void StartConnector()
    {
        MapTile.GetComponent<Animator>().SetBool("Up", false);
        GameManager = GameObject.Find("brain_jar");
        switch (State)
        {
            case 0:
                {
                    GameManager.GetComponent<GameManager>().StartBattle();
                }
                break;
            case 1:
                {
                    GameManager.GetComponent<GameManager>().PickCard();
                }
                break;
            case 2:
                {
                    GameManager.GetComponent<GameManager>().DeleteCard();
                }
                break;
            case 3:
                {

                }
                break;
        }
        switch (YourNumber)
        {
            case 0:
                {
                    Victim.GetComponent<Victim>().PickCard();
                }
                break;
            case 1:
                {
                    Victim.GetComponent<Victim>().DestroyCard();
                }
                break;
            case 2:
                {
                    Victim.GetComponent<Victim>().Battle();
                }
                break;
            case 3:
                {
                    Application.Quit();
                    Debug.Log("Kalafior");
                }
                break;
        }
    }
}
