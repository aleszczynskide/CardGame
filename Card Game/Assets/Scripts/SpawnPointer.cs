using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointer : MonoBehaviour
{
    public GameObject Manager;
    private void Start()
    {
        Manager = GameObject.Find("brain_jar");
    }
    private void OnMouseDown()
    {
        Manager.GetComponent<GameManager>().SpawnPointerActivated(this.name);
    }
}
