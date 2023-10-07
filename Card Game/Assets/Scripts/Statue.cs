using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameObject Pointer;
    public void Update()
    {
        if (Pointer != null)
        {
            StartCoroutine(MoveToTarget());
        }
        else if (Pointer == null)
        {

        }
        if (Pointer != null)
        {
            if (this.transform.position == Pointer.transform.position)
            {
                Pointer = null;
                Debug.Log("Walenie");
            }
        }
    }
    public void NextMove()
    {

    }
    IEnumerator MoveToTarget()
    {
        Vector3 StatuePosition = transform.position;
        Vector3 TargetPosition = this.Pointer.transform.position;
        float Length = Vector3.Distance(StatuePosition, TargetPosition);
        float StartTime = Time.time;

        float DistanceCovered = 0;

        while (DistanceCovered < Length)
        {
            float Distance = (Time.time - StartTime) * 0.2f;
            float fractionOfJourney = Distance / Length;

            transform.position = Vector3.Lerp(StatuePosition, TargetPosition, fractionOfJourney);
            DistanceCovered = Vector3.Distance(StatuePosition, transform.position);

            yield return null;
        }
    }
}
