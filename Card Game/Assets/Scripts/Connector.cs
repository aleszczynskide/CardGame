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
            Statue = GameObject.Find ("Statue(Clone)");
            MapTile.GetComponent<Map>().CurrentPointer = this.gameObject;
            MapTile.GetComponent<Map>().UpdateMap();
            Statue.GetComponent<Statue>().Pointer = MapTile.GetComponent<Map>().CurrentPointer;
            Statue.GetComponent<Statue>().NextMove();
        }
    }
}
