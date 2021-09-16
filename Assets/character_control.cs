using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_control : MonoBehaviour
{
    public double health=200;
    public float speed=10f;
    public bool fire_flag=true;
    public GameObject projectile;
    public float fire_rate=1;
    public CharacterController bod;
    public bool invulrenable=false;
    //public Transform projectile_pos;
    public float projectile_velocity=500f;
    // Start is called before the first frame update
    void Start()
    {
        bod=GetComponent<CharacterController>();
    }
    void enemy_contact_damage(double damage){
        if(!invulrenable){
            health-=damage;
            //contact_damage_reaction();
            StartCoroutine(contact_damage_reaction());
        }
    }
    void knockback(Vector3 dir, float force){
  
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "player"){
            
        } 
    }
    //WaitForSeconds fonksiyonu yield return ve icinde bulundugu fonksiyonun IEnumerator olmasini gerektiriyo
    IEnumerator launch_projectile(){
        if (fire_flag){
            fire_flag=false;
            Vector3 movingdirection2=transform.position;
            GameObject fire=Instantiate(projectile, transform.position, transform.rotation);
            //fire.GetComponent<projectile_script>().setting_up(movingdirection);
            Destroy(fire, 2.0f);
            yield return new WaitForSeconds((1/fire_rate));
            fire_flag=true;
        }
    }
    IEnumerator contact_damage_reaction(){
        if(!invulrenable){
            invulrenable=true;
            yield return new WaitForSeconds(1);
            invulrenable=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float xdir= Input.GetAxisRaw("Horizontal");
        float zdir=Input.GetAxisRaw("Vertical");
        Vector3 movingdirection=new Vector3(xdir, 0.0f, zdir);
        //transform.TransformDirection(movingdirection);
        //normalize edilme sebebi diagonal giderkenki hiz artimini azaltma
        movingdirection=movingdirection.normalized*Time.deltaTime*speed;
        //transform.rotation=Quaternion.LookRotation(movingdirection);
        //transform.Translate (movingdirection * Time.deltaTime, Space.World);
        /*if (movingdirection != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movingdirection.normalized), 0.2f);
        }*/
        
        
        if(Input.GetKey(KeyCode.F)){
            StartCoroutine(launch_projectile());
           
        }
        if(Input.GetKeyDown(KeyCode.F)){
           StartCoroutine(launch_projectile());
           //Invoke("launch_projectile", fire_rate);
           //InvokeRepeating("launch_projectile", 0.5f, fire_rate); 
        }
       // if(Input.GetKeyUp(KeyCode.F)){
         //   CancelInvoke("launch_projectile");
        //}
        if(movingdirection.magnitude==0){return;}
        //transform.position += movingdirection;
        //transform.rotation = Quaternion.LookRotation(movingdirection);
        bod.Move(movingdirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movingdirection), 0.15F);
    }
}
