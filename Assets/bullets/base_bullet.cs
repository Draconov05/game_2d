using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class base_bullet : MonoBehaviour
{
    public float bullet_speed;
    public float bullet_speed_mod;
    public string bullet_caliber;
    public float flesh_damage;
    public float armor_damage;
    public float max_class_perfuration;
    private bool collided = false;

    private float lifeTime = 0.0f;

    private float direction = 0;

    private float speed;

    void Start()
    {
        speed = (bullet_speed + bullet_speed_mod) * (float) 0.025;
        transform.position = new Vector3(transform.position.x, transform.position.y,0);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;

        if(direction != 0){
            if(!collided){
                transform.position += transform.right * speed;
            }
        }

        if(lifeTime >= 5.0f){
            Destroy(gameObject);
        }
        
    }

    public void fire(float dir){
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "enemy_1"){
            collided = true;
            Destroy(gameObject);
        }
    }

}
