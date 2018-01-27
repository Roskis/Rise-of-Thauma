using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildingBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 256);
        if (hit.collider != null)
        {
            Debug.Log("hit");
            Tilemap tm = hit.collider.gameObject.GetComponent<Tilemap>();
            Debug.Log(tm);
            Debug.Log("point: " + hit.point);
            Vector3Int cellPosition = tm.LocalToCell(hit.point);
            var pos = tm.GetCellCenterLocal(cellPosition);
            Debug.Log("pos: " + pos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
