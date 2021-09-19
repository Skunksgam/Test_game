using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    //x=5.9 y=5.5 z=2.6 rox=52
    public GameObject player;
    public Vector3 cam_pos= new Vector3(0f, 7f, -4.5f);
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=player.transform.position+cam_pos;
    }
}
