using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityToggleCode : MonoBehaviour
{
    private bool isGravity = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            GameObject[] physicsObject = GameObject.FindGameObjectsWithTag("Physics object");
        
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i].GetComponent<Rigidbody>().useGravity = isGravity;
            }

            for (int i = 0; i < physicsObject.Length; i++)
            {
                physicsObject[i].GetComponent<Rigidbody>().useGravity = isGravity;
            }

    }

    public void ToggleGravity()
    {
        isGravity = !isGravity;

    }
}
