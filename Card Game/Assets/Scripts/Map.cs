using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> Maps;
    public List<GameObject> Pointers;
    public List<GameObject> ActivePointers;
    public GameObject CurrentPointer;
    public GameObject Statue;
    [SerializeField] private Animator Anim;
    private int x;
    void Start()
    {
        Anim = GetComponent<Animator>();
        x = UnityEngine.Random.Range(0, Maps.Count);
        foreach (Transform child in Maps[x].transform)
        {
            Pointers.Add(child.gameObject);
        }
        Maps[x] = Instantiate(Maps[x],new Vector3 (-0.311f, -0.189f, 0.804f),Quaternion.identity);
        Maps[x].transform.SetParent(transform);
        if (CurrentPointer == null)
        {
            CurrentPointer = Pointers[0];
        }
        for (int i = 0; i < CurrentPointer.GetComponent<Connector>().ConnectedNode.Count; i++)
        {
            ActivePointers.Add(CurrentPointer.GetComponent<Connector>().ConnectedNode[i]);
        }
        for (int i = 0; i < ActivePointers.Count; i++)
        {
            ActivePointers[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void UpdateMap()
    {
        for (int i = 0; i < ActivePointers.Count; i++)
        {
            ActivePointers[i].GetComponent<BoxCollider2D>().enabled = false;
        }
        ActivePointers.Clear();
        for (int i = 0; i < CurrentPointer.GetComponent<Connector>().ConnectedNode.Count; i++)
        {
            ActivePointers.Add(CurrentPointer.GetComponent<Connector>().ConnectedNode[i]);
        }
        for (int i = 0; i < ActivePointers.Count; i++)
        {
            ActivePointers[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void Update()
    {
        
    }
    public void StatueSpawner(Transform StatuePosition)
    {
       GameObject Statuetka =  Instantiate(Statue, new Vector3(StatuePosition.transform.position.x, StatuePosition.transform.position.y, StatuePosition.transform.position.z), quaternion.identity);
        Statuetka.transform.Rotate(0f, 180f, 0f);
    }
}
