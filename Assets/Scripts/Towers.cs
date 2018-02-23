using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour {
    public GameManager gm;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gm.DeactivateTower(this.gameObject);
        }
        
    }

    void OnMouseDown()
    {
        gm.towerhit(this.gameObject);
    }
}
