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

    private float direction = 0;

    private float speed;

    void Start()
    {
        speed = (bullet_speed + bullet_speed_mod) * (float) 0.25;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction != 0){
            if(!collided){
                transform.position += new Vector3((speed * direction) * Time.deltaTime, 0, 0);
            }
        }
        
    }

    public void fire(float dir){
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        collided = true;
        Destroy(gameObject);
    }

}
