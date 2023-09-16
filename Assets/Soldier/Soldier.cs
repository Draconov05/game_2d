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

    private int life = 100;
    private int armor = 100;

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
            CharHealth.text = "Life: " + life + "%";
        }
        
        if (CharArmor != null){
            CharArmor.text = "Armor: " + armor + "%";
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

        CrossHair.transform.position = mousePosition;
        
        if(armor < 100){
            armor += 2;
        }

        if(Input.GetKeyUp("w") || Input.GetKeyUp("up")){

            body.AddForce(Vector2.up * 20);

        }else
        if(Input.GetKeyDown("d") || Input.GetKeyDown("right")){
            transform.localScale = new Vector3(1, 1, 1);
            if (anim != null){
                anim.Play("walk");
            }
        }else
        if(Input.GetKey("d") || Input.GetKey("right")){

            if(transform.localScale != new Vector3(1, 1, 1)){
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x + 0.015f, transform.position.y);
        }else
        if(Input.GetKeyUp("d") || Input.GetKeyUp("right")){
            
            if (anim != null){
                anim.Play("idle");
            }
        }else
        if(Input.GetKeyDown("a") || Input.GetKeyDown("left")){
            transform.localScale = new Vector3(-1, 1, 1);
            if (anim != null){
                anim.Play("walk");
            }
        }else
        if(Input.GetKey("a") || Input.GetKey("left")){

            if(transform.localScale != new Vector3(-1, 1, 1)){
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                anim.Play("walk");
            }

            transform.position = new Vector3(transform.position.x - 0.015f, transform.position.y);
        }else
        if(Input.GetKeyUp("a") || Input.GetKeyUp("left")){
            if (anim != null){
                anim.Play("idle");
            }
        }else
        if(Input.GetMouseButton(0)){
            anim.Play("fire");
        }else
        if(Input.GetKeyUp(KeyCode.LeftControl)){
            anim.Play("idle");
        }
    }

}
