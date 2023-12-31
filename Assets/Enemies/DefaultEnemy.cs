using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultEnemy : MonoBehaviour
{
    public GameObject moansObj;

    private GameObject WorldSettings;

    private WorldSettings WorldSettingsScript;

    private GameObject Player;

    private Vector3 distance;

    private Animator anim;

    private Soldier player_script;

    private AudioSource[] moans;

    private AudioSource moan;

    private float selectedMoan = 0;

    private float life = 150;
    
    public GameObject EnemyHealthObj;

    private Text EnemyHealth;

    private GameObject canva;

    private bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        if(moansObj){
            moans = moansObj.GetComponents<AudioSource>();
        }

        selectedMoan = UnityEngine.Random.Range(0, 1);

        EnemyHealthObj = Instantiate(EnemyHealthObj, transform.position, transform.rotation);

        canva = GameObject.FindGameObjectsWithTag("canvaCamera")[0];

        Player = GameObject.FindGameObjectsWithTag("player")[0];

        if(Player != null){
            player_script = Player.GetComponent<Soldier>();
        }

        EnemyHealthObj.transform.parent = canva.transform;

        EnemyHealthObj.transform.localScale = new Vector3(1,1,1);

        if(EnemyHealthObj){
            EnemyHealthObj.transform.position = new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z);
        }

        EnemyHealth = EnemyHealthObj.GetComponent<Text>();

        if (EnemyHealth != null){
            EnemyHealth.text = "" + Math.Round(life, 0) + "%";
        }

        anim = GetComponent<Animator>();
        if(anim != null){
            anim.Play("zombie_walk");
        }

        GameObject[] WorldSettingsLocal = GameObject.FindGameObjectsWithTag("world");

        if(WorldSettingsLocal.Length > 0){
            WorldSettings = WorldSettingsLocal[0];
            if(WorldSettings != null){
                WorldSettingsScript = WorldSettings.GetComponent<WorldSettings>();
                setStatsByDiff();
            }
        }

        Debug.Log(moans);

        if(moans != null){
            moan = moans[(int) selectedMoan];
        }
        
        moan.Play(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!died){
            if(EnemyHealthObj != null){
                EnemyHealthObj.transform.position = new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z);
                if (EnemyHealth != null){
                    EnemyHealth.text = "" + Math.Round(life, 0) + "%";  
                }
            }

            if(life <= 0){
                if(anim != null){
                    anim.Play("death_01");
                    if(player_script != null){
                        player_script.scored();
                    } 
                    Destroy(EnemyHealthObj, 1.30f);
                    Destroy(gameObject, 1.30f);
                    died = true;
                    
                }   
            }else if(anim != null && ( anim.GetCurrentAnimatorClipInfo(0).Length > 0 ) && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "attack" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "death_01" && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "hurt"){

                distance = Player.transform.position - transform.position;

                Vector3 walk = new Vector3();

                if((distance.x < 1.0 && distance.x > -1.0) && (distance.y < 1.0 && distance.y > -1.0)){
                    if(anim != null){
                        anim.Play("idle");
                    }
                    attack();

                }else{
                    if(anim != null){
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("zombie_walk")){
                            anim.Play("zombie_walk");
                        }
                    }
                    if(distance.x > 0){
                        transform.localScale = new Vector3(0.2f, 0.2f, 1);
                        walk.x = transform.position.x + 0.005f;
                    }

                    if(distance.x < 0){
                        transform.localScale = new Vector3(-0.2f, 0.2f, 1);
                        walk.x = transform.position.x - 0.005f;
                    }

                    if(distance.y > 0){
                        walk.y = transform.position.y + 0.005f;
                    }

                    if(distance.y < 0){
                        walk.y = transform.position.y - 0.005f;
                    }

                    transform.position = walk;
                }
            }
        }
        
    }

    void attack()
    {
        anim.Play("attack");
        if(anim != null){
            if(player_script != null){
                player_script.getHited(20);
            }
            anim.Play("walk");
        }
    }

    private void setStatsByDiff(){
        switch(WorldSettingsScript.Difficulty){
            case "easy":
                life = 100;
                break;

            case "normal":
                life = 150;
                break;
            
            case "hard":
                life = 200;
                break;

            case "nightmare":
                life = 300;
                break;
            default:
                life = 100;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(life > 0){
            if(col.gameObject.tag == "556"){
                life -= 25;
                anim.Play("hurt");
            }
        }
    }
}
