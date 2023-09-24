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

    private Rigidbody2D body;

    private float life = 100;
    private float armor = 100;

    private float walkSpeed = 0.02f;

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
            anim.Play("fire");
        }else
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            anim.Play("idle");
        }
    }

    Vector3 moduloMouse(Vector3 mousePosition){

        Vector3 mousePositionLocal = mousePosition;

        double modulo = Math.Sqrt(Math.Pow(transform.position.x - mousePosition.x,2) + Math.Pow(transform.position.y - mousePosition.y,2));

        if(modulo > 3.20d || modulo < 3.15d){
            float proporcao =  3.11f / (float) modulo;
            mousePositionLocal = new Vector3(transform.position.x - ((transform.position.x - mousePosition.x) * proporcao),transform.position.y - ((transform.position.y - mousePosition.y) * proporcao));
        }
            
        return mousePositionLocal;
    }

    public void getHited(float val){

        float exced;

        Debug.Log("attack");

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
