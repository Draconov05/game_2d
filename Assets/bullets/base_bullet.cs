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
    private bool fired = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fired)
        {
            float speed = (bullet_speed + bullet_speed_mod) * (float) 0.25;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        
    }

    public void fire()
    {
        this.fired = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

}
