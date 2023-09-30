using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{

    public float effective_range;
    public string caliber;
    public int recuo;
    public List<string> fire_modes = new List<string>() { "single" };
    public int fire_rate;
    public int mag_capacity = 30;
    private GameObject bullet;
    private Animation anim;
    public List<GameObject> mag_bullets = new List<GameObject>() {  };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            this.fire();
        }
    }

    // Update is called once per frame
    public void reload()
    {
        anim.Play("reload");
    }

    public void fire()
    {
        // this.bullet = mag_bullets[0];
        // Debug.Log(this.bullet);
        // base_bullet s2 = this.bullet.GetComponent<base_bullet>();
        // s2.fire();
        // mag_bullets.RemoveAt(0);
        // //anim.Play("fire");
    }

}