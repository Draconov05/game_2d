using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Soldier : MonoBehaviour
{
    private Animator anim;

    public Animator Anim   // property
    {
        get { return anim; }
        set { anim = value; }
    }

    public GameObject CharHealthObj;
    private Text CharHealth;
    public GameObject CharArmorObj;
    private Text CharArmor;

    public Text scorePanel;

    public float CrossHairDistance;

    private Rigidbody2D body;

    private float life = 100;
    private float armor = 100;

    private float walkSpeed = 0.02f;

    public GameObject Weapon;

    private WeaponBarril weaponScript;

    private GameObject UpperSide;

    private GameObject LowerSide;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        // Get Child lower and upper sides player
        Transform[] ObjectChildrens = gameObject.GetComponentsInChildren<Transform>();
        UpperSide = System.Array.Find(ObjectChildrens, p => p.gameObject.name == "UpperSide").gameObject;
        LowerSide = System.Array.Find(ObjectChildrens, p => p.gameObject.name == "LowerSide").gameObject;

        CharHealth = CharHealthObj.GetComponent<Text>();
        CharArmor = CharArmorObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CharHealthObj.transform.position = new Vector3(CharHealthObj.transform.position.x,CharHealthObj.transform.position.y,transform.position.z);

        CharArmorObj.transform.position = new Vector3(CharArmorObj.transform.position.x,CharArmorObj.transform.position.y,transform.position.z);

        if (CharHealth != null){
            CharHealth.text = "Life: " + Math.Round(life, 0) + "%";
        }
        
        if (CharArmor != null){
            CharArmor.text = "Armor: " + Math.Round(armor, 0) + "%";
        }

        if (scorePanel != null){
            scorePanel.text = "" + score + " kills";
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
        }
    }

    public void playAnim(string value){
        Debug.Log("Set animation "+value);
        anim.Play(value);
    }

    public string getAnim(){
        Debug.Log("Get animation name");
        return anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    public void scored(){
        score += 1;
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

        if(life < 0){
            SceneManager.LoadSceneAsync("GameOver"); 
            SceneManager.UnloadSceneAsync("Game");  
        }
    }

}
