using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CameraControl : MonoBehaviour {
    const float cameraSpeed = 10.0f;
    const float sensitivity = 10.0f;
    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 5.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    // Use this for initialization
    void Start () {
        targetOrtho = Camera.main.orthographicSize;

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -cameraSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, cameraSpeed * Time.deltaTime, 0));
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);


    }
}
