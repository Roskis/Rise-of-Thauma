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
        Debug.Log(Buildings);
        foreach (Tile t in Buildings.GetTilesBlock(Buildings.cellBounds))
        {
            if (t && t.name.Contains("Base"))
            {
                Debug.Log(t);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

public List<Vector3Int> getActiveTowers()
    {
        List<Vector3Int> list = new List<Vector3Int>();
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
}

public class Link {
    public Vector3Int start;
    public Vector3Int end;
    public Link(Vector3Int s, Vector3Int e)
    {
        start = s;
        end = e;
    }
}


// links.Add(new Link(startpos, endpos));
// foreach(Link link in links) {}
