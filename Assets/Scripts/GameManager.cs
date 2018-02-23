using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public GameObject startTower;
    public GameObject inactiveTowers;
    public GameObject houses;
    public Material lineMat;
    public UnityEngine.UI.Text scoreText;
    public LineRenderer buildLine;
    public Collider bgCollider;
    public Texture2D circle;
    public Sprite greenCircle;

    //class variables
    private GameObject lastClicked;
    private int points;
    private float money;
    private AnimationCurve buildCurve = new AnimationCurve();
    private Sprite circleSprite;

    const int radius = 3;

    bool onIncreaseMoney = false;    

    // Use this for initialization
    void Start () {
        points = 0;
        money = 100.0f;
        //Debug.Log("Start");
        //activeTowers.Add(startTower);
        addCircle(startTower.transform.position, radius, startTower);
        CountNearbyHouses(startTower);
        SetUpBuildLine();
        Sprite circleSprite = Sprite.Create(circle, new Rect(0, 0, circle.width, circle.height), new Vector2(0.5f, 0.5f));
        
    }


    void SetUpBuildLine()
    {
   
        buildCurve.AddKey(0, 0.2f);
        buildCurve.AddKey(1, 0.2f);
        buildLine.widthCurve = buildCurve;
        buildLine.SetPosition(0, new Vector3(0, 0, 0));
        buildLine.SetPosition(1, new Vector3(0, 0, 0));

    }


    void addCircle(Vector3 location, float r, GameObject addTower)
    {
        GameObject newSpriteObject = new GameObject();
        newSpriteObject.name = "circle";
        newSpriteObject.transform.parent = addTower.transform;

        SpriteRenderer spriteR = newSpriteObject.AddComponent<SpriteRenderer>();
        spriteR.sprite = greenCircle;
        spriteR.sortingOrder = 3;
        spriteR.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        spriteR.transform.localScale = new Vector3(r / 2.5f, r / 2.5f, 1.0f);
        spriteR.transform.position = location;
    }


    public void towerhit(GameObject clickedTower)
    {

        if (IsActive(lastClicked) && !IsActive(clickedTower))
        {
            if (money > Vector3.Distance(clickedTower.transform.position, lastClicked.transform.position))
            {
                //Debug.Log("Yhdistä kaksi tornia: " + clickedTower + " yhdistettiin torniin " + lastClicked);
                ActivateTower(clickedTower);
                lastClicked = null;
            } else
            {
                Debug.Log("sorry you're too poor.");
            }
        } else if (IsActive(clickedTower))
        {
            //Debug.Log("this " + clickedTower + " is now selected");
            lastClicked = clickedTower;
        } else
        {
            //Debug.Log("no active tower has been selected.");
            lastClicked = null;
        }

    }
    

    bool IsActive(GameObject tower)
    {

        if (tower == null) return false;
        if (tower == startTower) return true;
        return (tower.transform.root.gameObject == startTower);

    }

    void ActivateTower(GameObject clickedTower)
    {

        clickedTower.transform.parent = lastClicked.transform;
        DrawLine(lastClicked, clickedTower);
        points += CountNearbyHouses(clickedTower);
        money -= Vector3.Distance(clickedTower.transform.position, lastClicked.transform.position);
        addCircle(clickedTower.transform.position, radius, clickedTower);

    }


    public void DeactivateTower(GameObject tower)
    {
        if (tower == startTower)
            return;

        List<GameObject> toBeDeleted = new List<GameObject>();
        foreach (Transform child in tower.gameObject.transform)
        {
            if (child.gameObject.name.Contains("tower"))
            {
                toBeDeleted.Add(child.gameObject);
            }
        }

        foreach (GameObject d in toBeDeleted) {
            DeactivateTower(d);
        }

        tower.transform.parent = inactiveTowers.transform;
        Destroy(tower.gameObject.GetComponent<LineRenderer>());
        Destroy(tower.gameObject.transform.Find("circle").gameObject);

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


    int CountNearbyHouses(GameObject activeTower)
    {
        int numHouses = 0;
        foreach (Transform h in houses.transform)
        {
            var dist = Vector3.Distance(activeTower.transform.position, h.transform.position);
            if (dist < 3)
            {
                numHouses++;
            }
        }
        return numHouses;
    }


    Vector3 getMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (bgCollider.Raycast(ray, out hit, 300.0f))
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


    IEnumerator IncreaseMoney()
    {

        money += points * 0.25f;
        yield return new WaitForSeconds(1);
        onIncreaseMoney = false;

    }


    // Update is called once per frame
    void Update () {
	    if(Input.GetMouseButtonDown(1))
        {
            lastClicked = null;
        }

        drawBuildLine();
        updateScoreText();

        if (!onIncreaseMoney) {
            onIncreaseMoney = true;
            StartCoroutine("IncreaseMoney");
        }

    }



    public void DrawLine(GameObject start, GameObject end)
    {
        LineRenderer lineR = end.AddComponent<LineRenderer>();
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0, 0.2f);
        curve.AddKey(1, 0.2f);
        lineR.widthCurve = curve;
        lineR.material = new Material(lineMat);
        lineR.SetPosition(0, start.transform.position);
        lineR.SetPosition(1, end.transform.position);
        lineR.sortingOrder = 2;
    }
}