using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //At any time, you can adjust the default X and default Y value to move the camera to that spot
    //You can decide whether or not the camera follows "focus" in both x and y. 
    //Please note that if you disable the follow variables, the default value will be the new position.
    //min and max variables are the "walls" that the camera will not go past.

    //Public Variables for Customization
    public bool followX, followY;
    public GameObject Focus;
    public float defaultX, defaultY;
    public float minX, minY, maxX, maxY;
    //Private Variables
    private Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = Focus.transform.position;
        if (!followX)
        {
            targetPosition.x = defaultX;
        }
        if(!followY)
        {
            targetPosition.y = defaultY;
        }
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
    }

}
