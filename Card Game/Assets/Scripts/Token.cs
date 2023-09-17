using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    Rigidbody Rb;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        Rb.AddForce(new Vector3 (0,2,0), ForceMode.Impulse);
    }
}
