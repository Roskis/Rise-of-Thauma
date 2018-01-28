using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public GameObject startTower;
    public GameObject towers;
    public GameObject houses;
    public Sprite tower;
    public Sprite house;
    public Material lineMat;
    public UnityEngine.UI.Text scoreText;

    //class variables
    private List<GameObject> activeTowers = new List<GameObject>();
    private GameObject lastClicked;
    private List<towerLink> links = new List<towerLink>();
    private GameObject lineRenderers;// = new GameObject();
    private int points;

    const int radius = 3;

    // Use this for initialization
    void Start () {
        points = 0;
        Debug.Log("Start");
        //activeTowers = new List<GameObject>;
        activeTowers.Add(startTower);
        CountNearbyHouses(startTower);
        foreach (Transform towerTransform in towers.transform)
        {
                //Debug.Log(towerTransform.GameObject.name);
        }
        lineRenderers = new GameObject();
        lineRenderers.name = "lineRenderers";


    }

    public void towerhit(GameObject clickedTower)
    {
        if(!activeTowers.Contains(clickedTower) && lastClicked)
        {
            //Debug.Log("Yhdistä kaksi tornia: " + clickedTower + " yhdistettiin torniin " + lastClicked);
            ActivateNewTower(clickedTower);
            lastClicked = null;

        }
        else if (activeTowers.Contains(clickedTower))
        {
            lastClicked = clickedTower;
            //Debug.Log("this " + clickedTower + " is now selected");
        }
        else
        {
            lastClicked = null;
            //Debug.Log("no active tower has been selected.");
        }

    }

    void ActivateNewTower(GameObject clickedTower)
    {
        activeTowers.Add(clickedTower);
        AddNodes(clickedTower, lastClicked);
        CountNearbyHouses(clickedTower);

    }

    void CountNearbyHouses(GameObject activeTower)
    {
        foreach (Transform h in houses.transform)
        {
            var dist = Vector3.Distance(activeTower.transform.position, h.transform.position);
            if (dist < 3)
            {
                points++;
                scoreText.text = "Score: " + points; 
            }
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
            //Debug.Log("REMOVED ALL ACTIVE SELECTIONS.");
        }
        
	}

    public void AddNodes(GameObject start, GameObject end)
    {
        GameObject newLRObject = new GameObject();
        newLRObject.name = "linerendererobject";
        newLRObject.transform.parent = lineRenderers.transform;

        LineRenderer lineR = newLRObject.AddComponent<LineRenderer>();
        links.Add(new towerLink(start, end, lineR, lineMat));
    }
}

public class towerLink
{
    public GameObject start;
    public GameObject end;

    private LineRenderer lineRenderer;
    private AnimationCurve curve = new AnimationCurve();

    public towerLink(GameObject s, GameObject e, LineRenderer lr, Material lineMat)
    {
        start = s;
        end = e;
        lineRenderer = lr;
        curve.AddKey(0, 0.2f);
        curve.AddKey(1, 0.2f);
        Debug.Log(lineRenderer);
        lineRenderer.widthCurve = curve;
        lineRenderer.material = new Material(lineMat);
        lineRenderer.SetPosition(0, start.transform.position);
        lineRenderer.SetPosition(1, end.transform.position);
        lineRenderer.sortingOrder = 2;
    }
}
