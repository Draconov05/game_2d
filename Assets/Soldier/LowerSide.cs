using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerSide : MonoBehaviour
{
    private Animator anim;

    public Animator Anim   // property
    {
        get { return anim; }
        set { anim = value; }
    }
    
    private GameObject player;

    private Soldier soldierScript;

    private float walkSpeed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform.parent.gameObject;
        soldierScript = player.GetComponent<Soldier>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("d") || Input.GetKeyDown("right") || Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("s") || Input.GetKeyDown("down") || Input.GetKeyDown("a") || Input.GetKeyDown("left") ){ //walk animation
            if (anim != null){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }
        }else
        if(Input.GetKeyUp("d") || Input.GetKeyUp("right") || Input.GetKeyUp("w") || Input.GetKeyUp("up") || Input.GetKeyUp("s") || Input.GetKeyUp("down") || Input.GetKeyUp("a") || Input.GetKeyUp("left") ){ //idle animation
            if (anim != null){
                anim.Play("idle");
            }
        }else
        if((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("d") || Input.GetKey("right"))){ // top right

            if(player.transform.localScale != new Vector3(1, 1, 1)){
                player.transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x + walkSpeed, player.transform.position.y + walkSpeed);

        }else
        if((Input.GetKey("w") || Input.GetKey("up")) && (Input.GetKey("a") || Input.GetKey("left"))){ // top left

            if(player.transform.localScale != new Vector3(-1, 1, 1)){
                player.transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x - walkSpeed, player.transform.position.y + walkSpeed);

        }else
        if(Input.GetKey("w") || Input.GetKey("up")){ //top

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + walkSpeed);

        }else
        if((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("d") || Input.GetKey("right"))){ // down right

            if(player.transform.localScale != new Vector3(1, 1, 1)){
                player.transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x + walkSpeed, player.transform.position.y - walkSpeed);

        }else
        if((Input.GetKey("s") || Input.GetKey("down")) && (Input.GetKey("a") || Input.GetKey("left"))){ // down left

            if(player.transform.localScale != new Vector3(-1, 1, 1)){
                player.transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x - walkSpeed, player.transform.position.y - walkSpeed);

        }else
        if(Input.GetKey("s") || Input.GetKey("down")){

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - walkSpeed);

        }else
        if(Input.GetKey("d") || Input.GetKey("right")){

            if(player.transform.localScale != new Vector3(1, 1, 1)){
                player.transform.localScale = new Vector3(1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x + walkSpeed, player.transform.position.y);
        }else
        if(Input.GetKey("a") || Input.GetKey("left")){

            if(player.transform.localScale != new Vector3(-1, 1, 1)){
                player.transform.localScale = new Vector3(-1, 1, 1);
            }

            if(anim != null && !anim.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                soldierScript.playAnim("walk");
                anim.Play("walk");
            }

            player.transform.position = new Vector3(player.transform.position.x - walkSpeed, player.transform.position.y);
            
        }
        
        Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
    }
}
