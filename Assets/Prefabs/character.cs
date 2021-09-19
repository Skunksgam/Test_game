using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public float speed=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xdir= Input.GetAxis("Horizontal");
        float zdir=Input.GetAxis("Vertical");
        Vector3 movingdirection=new Vector3(xdir*speed, 0.0f, zdir*speed);

        transform.position += movingdirection; 
    }
}
