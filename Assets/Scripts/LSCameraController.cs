using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCameraController : MonoBehaviour
{
    public Vector2 minPos, maxPos;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x); //stavljamo za xpos da ima ove vrednosti
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y); //stavljamo za ypos da ima ove vrednosti

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
