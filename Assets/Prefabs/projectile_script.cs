using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
    public double damage=50;
    public float speed=20f;
    Vector3 projectile_vector;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setting_up(Vector3 projectile_vector){
        this.projectile_vector=projectile_vector;
    }
    void OnCollisionEnter(Collision col)
    {
     
         if(col.gameObject.tag == "enemy"){
            col.gameObject.SendMessage("hit", damage);
            Destroy(gameObject);
         } 
    }
    
    // Update is called once per frame
    void Update()
    {
    
        transform.position+=transform.forward*speed*Time.deltaTime;
    }
}
