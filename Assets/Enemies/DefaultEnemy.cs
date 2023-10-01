using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultEnemy : MonoBehaviour
{
    private GameObject Player;

    private Vector3 distance;

    private Animator anim;

    private Soldier player_script;

    private bool esperaConcluida = true;

    private float tempoDecorrido = 0.0f;

    private float tempoDeEspera = 2.0f;

    private float life = 150;
    
    public GameObject EnemyHealthObj;

    private Text EnemyHealth;

    private GameObject canva;
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyHealthObj = Instantiate(EnemyHealthObj, transform.position, transform.rotation);

        canva = GameObject.FindGameObjectsWithTag("canvaCamera")[0];

        Player = GameObject.FindGameObjectsWithTag("player")[0];

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

        anim.Play("zombie_walk");
    }

    // Update is called once per frame
    void Update()
    {

        if(EnemyHealthObj != null){
            EnemyHealthObj.transform.position = new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z);
            if (EnemyHealth != null){
                EnemyHealth.text = "" + Math.Round(life, 0) + "%";  
            }
        }
        
        if (!esperaConcluida)
        {
            // Incrementa o tempo decorrido
            tempoDecorrido += Time.deltaTime;

            // Verifica se o tempo decorrido atingiu o tempo de espera desejado
            if (tempoDecorrido >= tempoDeEspera)
            {
                esperaConcluida = true;
            }
        }

        if(life <= 0){
            if(anim != null){
                anim.Play("death_01");
                Destroy(EnemyHealthObj, 1.30f);
                Destroy(gameObject, 1.30f);
            }   
        }else if(esperaConcluida && anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "attack"){

            distance = Player.transform.position - transform.position;

            Vector3 walk = new Vector3();

            if((distance.x < 1.5 && distance.x > -1.5) && (distance.y < 1.5 && distance.y > -1.5)){
                if(anim != null){
                    anim.Play("idle");
                }
                attack();

                tempoDeEspera = 1.5f;
                tempoDecorrido = 0.0f;
                esperaConcluida = false;

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

    void attack()
    {
        anim.Play("attack");
        if(anim != null){
            if(Player != null){
                player_script = Player.GetComponent<Soldier>();
                if(player_script != null){
                    player_script.getHited(20);
                } 
            }
            anim.Play("walk");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(life > 0){
            if(col.gameObject.tag == "556"){
                life -= 25;
            }
        }
    }
}
