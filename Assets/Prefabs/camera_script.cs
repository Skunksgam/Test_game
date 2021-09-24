using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    //x=5.9 y=5.5 z=2.6 rox=52
    public GameObject player;
    public Vector3 cam_pos= new Vector3(0f, 9f, -4.5f);
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 static_pos=player.transform.position;static_pos.y=0;
        transform.position=static_pos+cam_pos;
    }
}
