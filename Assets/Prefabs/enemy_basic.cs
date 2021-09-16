using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_basic : MonoBehaviour
{
    public double health=100;
    public float speed=5f;
    public double contact_damage=20;
    GameObject p;
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
    GameObject tracking_player(){
        player= GameObject.Find("Player");
        //GameObject t=player;
        return player;

    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.SendMessage("enemy_contact_damage", contact_damage);
            //enemy_body.isKinematic=true;
           // Debug.Log("yes");
            Vector3 knock;
            
        } 
    }
    void OnCollisionStay(Collision col){
        if(col.gameObject.tag == "Player"){
            col.gameObject.SendMessage("enemy_contact_damage", contact_damage);
            //Debug.Log("yes");
            //enemy_body.isKinematic=true;
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
        transform.LookAt(p.transform.position);
        transform.rotation*=Quaternion.Euler(0, 30, 0);
        float t_speed=speed*Time.deltaTime;
        enemy_body.AddForce((p.transform.position-transform.position)*speed);
        //transform.position=Vector3.MoveTowards(transform.position, p.position, t_speed);
        if(health<=0){Destroy(gameObject);}
    }
}
