using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultEnemy : MonoBehaviour
{
    public GameObject Player;

    private Vector3 distance;

    private Animator anim;

    private Soldier player_script;

    private bool esperaConcluida = true;

    private float tempoDecorrido = 0.0f;

    private float tempoDeEspera = 2.0f;

    private float life = 150;

    public Text EnemyHealth;

    private GameObject ngo;
    
    // Start is called before the first frame update
    void Start()
    {

        ngo = new GameObject("EnemyHealth");

        if(ngo){
            ngo.transform.SetParent(transform);

            ngo.transform.position = new Vector3(transform.position.x,transform.position.y + 2,transform.position.z);

            EnemyHealth = ngo.AddComponent<Text>();
        }

        

        if (EnemyHealth != null){
            EnemyHealth.text = "" + Math.Round(life, 0) + "%";
        }

        anim = GetComponent<Animator>();

        anim.Play("zombie_walk");
    }

    // Update is called once per frame
    void Update()
    {

        if(ngo != null){
            ngo.transform.position = new Vector3(transform.position.x,transform.position.y + 2,transform.position.z);

            if (EnemyHealth != null){
                EnemyHealth.text = "" + Math.Round(life, 0) + "%";  
            }

            Debug.Log(ngo.transform.position);
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

        if(esperaConcluida){

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
                    walk.x = transform.position.x + 0.015f;
                }

                if(distance.x < 0){
                    transform.localScale = new Vector3(-0.2f, 0.2f, 1);
                    walk.x = transform.position.x - 0.015f;
                }

                if(distance.y > 0){
                    walk.y = transform.position.y + 0.015f;
                }

                if(distance.y < 0){
                    walk.y = transform.position.y - 0.015f;
                }

                transform.position = walk;
            }
        }
    }

    void attack()
    {
        anim.Play("attack");

        while(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "attack");

        if(anim != null){
            if(Player != null){
                player_script = Player.GetComponent<Soldier>();
                if(player_script != null){
                    player_script.getHited(25);
                } 
            }
            anim.Play("walk");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if(life <= 0){
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "556"){
            life -= 10;
        }
        
        
    }
}
