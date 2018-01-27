using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BuildingBehaviour : MonoBehaviour
{
    private LineRenderer line;
    // Use this for initialization
    void Start()
    {

    }

    void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 256);
        if (hit.collider != null)
        {
            Tilemap tm = hit.collider.gameObject.GetComponent<Tilemap>();
            Debug.Log(tm);
            //Debug.Log("point: " + hit.point);
            Vector3Int cellPosition = tm.LocalToCell(hit.point);
            Debug.Log("cellPos" + cellPosition);
            //var pos = tm.GetCellCenterLocal(cellPosition);
            var clickedTile = tm.GetTile(cellPosition);
            var type = tm.GetSprite(cellPosition);
            //Debug.Log("Sprite: " + type);
            //Debug.Log("clickedTile: " + clickedTile);
            //Debug.Log("pos: " + pos);
            //line.SetPosition(0, transform.position);
            //line.SetPosition(0, pos);
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, pos);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
