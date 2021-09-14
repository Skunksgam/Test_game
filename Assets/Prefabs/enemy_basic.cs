using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_basic : MonoBehaviour
{
    public double health=100;
    public float speed=2;
    public double contact_damage=20;
    Transform p;
    private GameObject player;
    private Transform player_t;
    Rigidbody enemy_body;

    // Start is called before the first frame update
    void Start()
    {
        enemy_body=GetComponent<Rigidbody>();
        p = tracking_player();
    }
    //bu simdilik hem bulma hem de takip etme, sonra bulma ayri bi fonksiyon olabilir
    Transform tracking_player(){
        player= GameObject.Find("Player");
        Transform t=player.transform;
        return t;

    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "player"){
            col.gameObject.SendMessage("enemy_contact_damage", contact_damage);
            Debug.Log("yes");
            Vector3 knock;
            
        } 
    }
    // Update is called once per frame
     void hit(double damage){
         health-=damage;
         
     }
    void Update()
    {
        //p = tracking_player();
        transform.LookAt(p);
        float t_speed=speed*Time.deltaTime;
        transform.position=Vector3.MoveTowards(transform.position, p.position, t_speed);
        if(health<=0){Destroy(gameObject);}
    }
}
