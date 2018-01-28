using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject startTower;
    public GameObject towers;
    public GameObject houses;
    public Sprite tower;
    public Sprite house;

	// Use this for initialization
	void Start () {
        Debug.Log("Start");
        foreach(Transform towerTransform in towers.transform)
        {
            //Debug.Log(towerTransform.gameObject);
        }
	}

    public void towerhit(GameObject clickedTower)
    {
        Debug.Log(clickedTower);
    }

    void OnMouseDown()
    {
        Debug.Log("haista paska");
    }

    void Awake()
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
