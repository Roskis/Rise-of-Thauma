using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePoint : MonoBehaviour
{
    void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);
        if (hit.collider != null)
        {
            Debug.Log("mouse : " + hit.point);
        }
    }
}
