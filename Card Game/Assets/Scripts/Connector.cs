using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public List<GameObject> ConnectedNode;
    private GameObject MapTile;
    public List<Sprite> Sprites;
    SpriteRenderer Sprite;
    [HideInInspector] public int State;
    GameObject Statue;
    GameObject GameManager;
    void Start()
    {
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
            switch (this.name)
            {
                case "tutorial1":
                    {
                        State = 2;
                    }
                    break;
                case "tutorial2":
                    {
                        State = 1;
                    }
                    break;
                case "tutorial3":
                    {
                        State = 0;
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
                    Debug.Log("3");
                }
                break;
        }
    }
}
