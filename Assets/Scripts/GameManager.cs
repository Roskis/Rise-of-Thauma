using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject startTower;
    public GameObject towers;
    public GameObject houses;
    public Sprite tower;
    public Sprite house;

    //class variables
    private List<GameObject> activeTowers = new List<GameObject>();
    private GameObject lastClicked;
    private List<towerLink> links = new List<towerLink>();

    // Use this for initialization
    void Start () {
        Debug.Log("Start");
        //activeTowers = new List<GameObject>;
        activeTowers.Add(startTower);
        foreach (Transform towerTransform in towers.transform)
        {
                //Debug.Log(towerTransform.GameObject.name);
        }
	}

    public void towerhit(GameObject clickedTower)
    {
        if(!activeTowers.Contains(clickedTower) && lastClicked)
        {
            Debug.Log("Yhdistä kaksi tornia: " + clickedTower + " yhdistettiin torniin " + lastClicked);
            activeTowers.Add(clickedTower);
            AddNodes(clickedTower, lastClicked);
            lastClicked = null;
        }
        else if (activeTowers.Contains(clickedTower))
        {
            lastClicked = clickedTower;
            Debug.Log("this " + clickedTower + " is now selected");
        }
        else
        {
            lastClicked = null;
            Debug.Log("no active tower has been selected.");
        }

    }

    void Awake()
    {


    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(1))
        {
            lastClicked = null;
            Debug.Log("REMOVED ALL ACTIVE SELECTIONS.");
        }
	}

    public void AddNodes(GameObject start, GameObject end)
    {
        links.Add(new towerLink(start, end));
    }
}

public class towerLink
{
    public GameObject start;
    public GameObject end;
    public towerLink(GameObject s, GameObject e)
    {
        start = s;
        end = e;
    }

}
