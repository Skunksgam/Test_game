using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_basic : MonoBehaviour
{
    public double health=100;
    public float speed=20f;
    public float contact_damage=20;
    public float gravity=-10f;
    public float knock_force=100f;
    GameObject p;
    private GameObject player;
    private Transform player_t;
    Rigidbody enemy_body;

    // Start is called before the first frame update
    void Start()
    {
        enemy_body=GetComponent<Rigidbody>();
        enemy_body.constraints = RigidbodyConstraints.FreezePositionY;
        p = tracking_player();
    }
    //bu simdilik hem bulma hem de takip etme, sonra bulma ayri bi fonksiyon olabilir
    GameObject tracking_player(){
        player= GameObject.Find("Player");
        //GameObject t=player;
        return player;

    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            Vector3 push_direction=transform.position;
            push_direction.y=0;
            col.gameObject.SendMessage("enemy_contact_damage", contact_damage);
            //col.gameObject.SendMessage("knockback", push_direction*knock_force);
            //enemy_body.isKinematic=true;
            //Debug.Log("yes");
            
            
        } 
    }
    void OnCollisionStay(Collision col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.SendMessage("enemy_contact_damage", contact_damage);
            //Debug.Log("yes");
            //enemy_body.isKinematic=true;
            
            
        } 
    }
    void OnCollisionLeave(Collision col){
        //enemy_body.isKinematic=false;
    }
    // Update is called once per frame
     void hit(double damage){
         health-=damage;
         
     }
    void Update()
    {
        //p = tracking_player();
        Vector3 newp=p.transform.position;
        newp.y=0;
        transform.LookAt(newp);
        transform.rotation*=Quaternion.Euler(0, 30, 0);
        float t_speed=speed*Time.deltaTime;
        Vector3 filtered=p.transform.position;
        filtered.x-=transform.position.x;filtered.z-=transform.position.z;
        Vector3 move_force=(filtered).normalized;
        //move_force.y=0; daha iyi bi opsiyon olmali
        move_force.y=0;
        enemy_body.AddForce(move_force*speed);
        //transform.position=Vector3.MoveTowards(transform.position, p.position, t_speed);
        if(health<=0){Destroy(gameObject);}
    }
}
