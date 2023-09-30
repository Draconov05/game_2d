using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    private Animator anim;

    public Text CharHealth;
    public Text CharArmor;

    public GameObject CrossHair; 

    public float CrossHairDistance;

    private Rigidbody2D body;

    private float life = 100;
    private float armor = 100;

    private float walkSpeed = 0.02f;

    public GameObject bullet;

    private base_bullet bulletScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CharHealth != null){
            CharHealth.text = "Life: " + Math.Round(life, 0) + "%";
        }
        
        if (CharArmor != null){
            CharArmor.text = "Armor: " + Math.Round(armor, 0) + "%";
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

        CrossHair.transform.position = moduloMouse(mousePosition);
        
        if(CrossHair.transform.position.x - transform.position.x < 0){

            if(transform.localScale != new Vector3(-1, 1, 1)){
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }else if(CrossHair.transform.position.x - transform.position.x > 0){

            if(transform.localScale != new Vector3(1, 1, 1)){
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if(armor < 100){
            armor += 0.02f;
        }

        if(Input.GetKeyDown("d") || Input.GetKeyDown("right") || Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("s") || Input.GetKeyDown("down") || Input.GetKeyDown("a") || Input.GetKeyDown("left") ){ //walk animation
            if (anim != null){
                anim.Play("walk");
            }
        }else
        if(Input.GetKeyUp("d") || Input.GetKeyUp("right") || Input.GetKeyUp("w") || Input.GetKeyUp("up") || Input.GetKeyUp("s") || Input.GetKeyUp("down") || Input.GetKeyUp("a") || Input.GetKeyUp("left") ){ //idle animation
            if (anim != null){
                anim.Play("idle");
            }
        }else
        if((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("d") || Input.GetKey("right"))){ // top right

            if(transform.localScale != new Vector3(1, 1, 1)){
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y + walkSpeed);

        }else
        if((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("a") || Input.GetKey("left"))){ // top left

            if(transform.localScale != new Vector3(-1, 1, 1)){
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x - walkSpeed, transform.position.y + walkSpeed);

        }else
        if(Input.GetKey("w") || Input.GetKey("up")){ //top

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x, transform.position.y + walkSpeed);

        }else
        if((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("d") || Input.GetKey("right"))){ // down right

            if(transform.localScale != new Vector3(1, 1, 1)){
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y - walkSpeed);

        }else
        if((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("a") || Input.GetKey("left"))){ // down left

            if(transform.localScale != new Vector3(-1, 1, 1)){
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x - walkSpeed, transform.position.y - walkSpeed);

        }else
        if(Input.GetKey("s") || Input.GetKey("down")){

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x, transform.position.y - walkSpeed);

        }else
        if(Input.GetKey("d") || Input.GetKey("right")){

            if(transform.localScale != new Vector3(1, 1, 1)){
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x + walkSpeed, transform.position.y);
        }else
        if(Input.GetKey("a") || Input.GetKey("left")){

            if(transform.localScale != new Vector3(-1, 1, 1)){
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x - walkSpeed, transform.position.y);
        }else
        if(Input.GetMouseButton(0)){
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "fire"){
                shoot();
            }
        }
    }

    private void shoot(){
        GameObject bullet_inner = Instantiate(bullet, transform.position, CrossHair.transform.rotation);
        bulletScript = bullet_inner.GetComponent<base_bullet>();
        bulletScript.fire(transform.localScale.x);
        anim.Play("fire");
    }

    Vector3 moduloMouse(Vector3 mousePosition){

        Vector3 mousePositionLocal = mousePosition;
        Vector2 mouseRelativo = mousePositionLocal - transform.position;

        float modulo = mouseRelativo.magnitude;
        
        if (modulo > CrossHairDistance)
            mousePositionLocal = transform.position + (Vector3)(CrossHairDistance * mouseRelativo / modulo);
        
        return mousePositionLocal;
    }

    public void getHited(float val){

        float exced;

        if(armor > 0){
            armor -= val;
            if(val > armor) {
                armor = 0;
                exced = val - armor;
                life -= exced;
            }
        }else{
            life -= val;
        }
    }

}
