using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public GameObject startTower;
    public GameObject towers;
    public GameObject houses;
    public GameObject circles;
    public Material lineMat;
    public UnityEngine.UI.Text scoreText;
    public LineRenderer buildLine;
    public Collider bgCollider;
    public Texture2D circle;
    public Sprite greenCircle;

    //class variables
    private List<GameObject> activeTowers = new List<GameObject>();
    private GameObject lastClicked;
    private List<towerLink> links = new List<towerLink>();
    private GameObject lineRenderers;
    private int points;
    private float money;
    private AnimationCurve buildCurve = new AnimationCurve();
    private Sprite circleSprite;

    const int radius = 3;

    // Use this for initialization
    void Start () {
        points = 0;
        money = 100.0f;
        //Debug.Log("Start");
        activeTowers.Add(startTower);
        addCircle(startTower.transform.position, radius);
        CountNearbyHouses(startTower);
        setUpLineRenderers();
        Sprite circleSprite = Sprite.Create(circle, new Rect(0, 0, circle.width, circle.height), new Vector2(0.5f, 0.5f));
    }

    void setUpLineRenderers()
    {
        lineRenderers = new GameObject();
        lineRenderers.name = "lineRenderers";

        buildCurve.AddKey(0, 0.2f);
        buildCurve.AddKey(1, 0.2f);
        buildLine.widthCurve = buildCurve;
        buildLine.SetPosition(0, new Vector3(0, 0, 0));
        buildLine.SetPosition(1, new Vector3(0, 0, 0));
    }

    void addCircle(Vector3 location, float r)
    {
        GameObject newSpriteObject = new GameObject();
        newSpriteObject.name = "spriterendererobject";
        newSpriteObject.transform.parent = circles.transform;

        SpriteRenderer spriteR = newSpriteObject.AddComponent<SpriteRenderer>();
        spriteR.sprite = greenCircle;
        spriteR.sortingOrder = 3;
        spriteR.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        spriteR.transform.localScale = new Vector3(r / 2.5f, r / 2.5f, 1.0f);
        spriteR.transform.position = location;
    }

    public void towerhit(GameObject clickedTower)
    {
        if(!activeTowers.Contains(clickedTower) && lastClicked)
        {
            //Debug.Log("Yhdistä kaksi tornia: " + clickedTower + " yhdistettiin torniin " + lastClicked);
            float dist = Vector3.Distance(lastClicked.transform.position, clickedTower.transform.position);
            if (money - dist >= 0.0f)
            {
                ActivateNewTower(clickedTower);
                lastClicked = null;
            }
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
        money -= Vector3.Distance(clickedTower.transform.position, lastClicked.transform.position);
        addCircle(clickedTower.transform.position, radius);
    }

    void updateScoreText()
    {
        if (lastClicked)
        {
            float dist = Vector3.Distance(lastClicked.transform.position, getMousePos());
            scoreText.text = "Score: " + points + " Money: " + money.ToString("n1") + " (Cost: " + dist.ToString("n1") + ")";
        }
        else
        {
            scoreText.text = "Score: " + points + " Money: " + money.ToString("n1");
        }
    }

    void CountNearbyHouses(GameObject activeTower)
    {
        foreach (Transform h in houses.transform)
        {
            var dist = Vector3.Distance(activeTower.transform.position, h.transform.position);
            if (dist < 3)
            {
                points++;
            }
        }
    }

    Vector3 getMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (bgCollider.Raycast(ray, out hit, 100.0f))
        {
            return hit.point;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    void drawBuildLine()
    {
        if (lastClicked)
        {
            buildLine.SetPosition(0, lastClicked.transform.position);
            buildLine.SetPosition(1, getMousePos());
        }
        else
        {
            buildLine.SetPosition(0, new Vector3(0, 0, 0));
            buildLine.SetPosition(1, new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetMouseButtonDown(1))
        {
            lastClicked = null;
            //Debug.Log("REMOVED ALL ACTIVE SELECTIONS.");
        }
        drawBuildLine();
        updateScoreText();
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
        //Debug.Log(lineRenderer);
        lineRenderer.widthCurve = curve;
        lineRenderer.material = new Material(lineMat);
        lineRenderer.SetPosition(0, start.transform.position);
        lineRenderer.SetPosition(1, end.transform.position);
        lineRenderer.sortingOrder = 2;
    }
}
