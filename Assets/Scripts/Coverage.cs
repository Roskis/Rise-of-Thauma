using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coverage : MonoBehaviour {

    List<Link> links = new List<Link>();
    public Tilemap Buildings;
    // Use this for initialization
    void Start () {
        if (Buildings == null)
            Buildings = GameObject.FindGameObjectWithTag("Buildings").GetComponent<Tilemap>();

        BoundsInt bounds = Buildings.cellBounds;
        TileBase[] allTiles = Buildings.GetTilesBlock(bounds);
        Debug.Log("x bound size" + bounds.size.x);
        Debug.Log("y bound size" + bounds.size.y);

        for (int y = 0; y < bounds.size.y; y++)
        {
            for (int x = 0; x < bounds.size.x; x++)

            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile && tile.name.Contains("Base"))
                {
                    Debug.Log("x:" + (x - (int)(bounds.size.x/2) + 1) + " y:" + (y - (int)(bounds.size.y / 2)) + " tile:" + tile.name);
                    //THIS IS DONE BECAUSE X IS EVEN AND T IS ODD. PROBABLY.
                    Vector3Int pos = new Vector3Int((x - (int)(bounds.size.x / 2) + 1), (y - (int)(bounds.size.y / 2)), 0);
                    Vector3 worldPos = Buildings.GetCellCenterLocal(pos);
                    //Debug.Log(worldPos);
                    addNodes(worldPos, worldPos);
                }
            }
        }
        foreach(Vector3 pos in getActiveTowers())
        {
            Debug.Log("this is the starting postition:" + pos);
        }
    }

     

	
	// Update is called once per frame
	void Update () {
	}

public List<Vector3> getActiveTowers()
    {
        List<Vector3> list = new List<Vector3>();
        foreach (Link link in links) {
            if(!list.Contains(link.start))
            {
                list.Add(link.start);
            }
            if (!list.Contains(link.end))
            {
                list.Add(link.end);
            }
        }
        return list;
    }

    public void addNodes(Vector3 start, Vector3 end)
    {
        links.Add(new Link(start, end));
    }
}


public class Link {
    public Vector3 start;
    public Vector3 end;
    public Link(Vector3 s, Vector3 e)
    {
        start = s;
        end = e;
    }

}


// links.Add(new Link(startpos, endpos));
// foreach(Link link in links) {}
