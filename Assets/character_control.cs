using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_control : MonoBehaviour
{
    public double health=200;
    public float grav_variable=0f;
    public float jump_stat=0.1f;
    public float speed=10f;
    public bool fire_flag=true;
    public GameObject Projectile;
    public float fire_rate=1;
    public CharacterController bod;
    public bool invulrenable=false;
    public float gravity_pull=-0.3f;
    //public bool airborne=false;
    //public Transform projectile_pos;
    public float projectile_velocity=500f;
    // Start is called before the first frame update
    void Start()
    {
        bod=GetComponent<CharacterController>();
       // Projectile=(GameObject)Resources.Load("Prefabs/Projectile.prefab", typeof(GameObject));
    }
    void enemy_contact_damage(double damage){
        if(!invulrenable){
            health-=damage;
            //contact_damage_reaction();
            StartCoroutine(contact_damage_reaction());
        }
         

    }
    float knockback(float knock_force){
        return 1f;
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "enemy"){
            float forc=col.gameObject.GetComponent<enemy_basic>().knock_force;
            Debug.Log(forc);
            /* Vector3 Addvec=(bod.transform.position-col.gameObject.transform.position).normalized*forc;
            Vector3 trs= Vector3.Lerp(bod.transform.position, (bod.transform.position-col.gameObject.transform.position).normalized*forc, Time.deltaTime*10);*/
            bod.Move((bod.transform.position-col.gameObject.transform.position)*Time.deltaTime*forc);
        } 
    }
    //WaitForSeconds fonksiyonu yield return ve icinde bulundugu fonksiyonun IEnumerator olmasini gerektiriyo
    IEnumerator launch_projectile(){
        if (fire_flag){
            fire_flag=false;
            Vector3 movingdirection2=transform.position;
            GameObject fire=Instantiate(Projectile, transform.position, transform.rotation);
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
    void Jump(){
        Vector3 jump_force=new Vector3( 0, jump_stat, 0);
        if(bod.isGrounded){
            //bod.Move(jump_force*Time.deltaTime);
            grav_variable=jump_stat;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float xdir= Input.GetAxisRaw("Horizontal");
        float zdir=Input.GetAxisRaw("Vertical");
        Vector3 movingdirection=new Vector3(xdir, 0.0f, zdir);
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
        }
        if(Input.GetKey(KeyCode.Space)){
            
            Jump();
        }
        Vector3 grav=new Vector3( 0, gravity_pull, 0);
        if(!bod.isGrounded && grav_variable>-1){
            grav_variable+=gravity_pull*Time.deltaTime;
        }
        
        movingdirection.y=grav_variable;
        
        //transform.position += movingdirection;
        //transform.rotation = Quaternion.LookRotation(movingdirection);
        Vector3 look_rotation=movingdirection;look_rotation.y=0;
        bod.Move(movingdirection);
        if(movingdirection.x==0&&movingdirection.z==0){return;}
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look_rotation), 0.15F);
    }
    void FixedUpdate() {
        //Yercekimi
        
    }
}
