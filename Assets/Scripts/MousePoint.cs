using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePoint : MonoBehaviour
{
    public Collider collider;

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (collider.Raycast(ray, out hit, 100.0f))
        {
            //Debug.Log("hit: " + hit.point);
        }
    }
}
