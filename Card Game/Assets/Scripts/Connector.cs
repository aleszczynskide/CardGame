using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public List<GameObject> ConnectedNode;
    private GameObject MapTile;
    void Start()
    {
        MapTile = GameObject.Find("MapTile");
    }
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("Trafiony");
        MapTile.GetComponent<Map>().CurrentPointer = this.gameObject;
        MapTile.GetComponent<Map>().UpdateMap();
    }
}
