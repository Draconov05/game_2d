using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBarril : MonoBehaviour
{

    private GameObject Player;

    public GameObject bullet;
    private base_bullet bulletScript;

    // Start is called before the first frame update
    public void fire(float angle){

        Player = GameObject.FindGameObjectsWithTag("player")[0];

        GameObject bullet_inner = Instantiate(bullet, new Vector3(transform.position.x,Player.transform.position.y,transform.position.z),transform.rotation);
        bullet_inner.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bulletScript = bullet_inner.GetComponent<base_bullet>();
        bulletScript.fire(Player.transform.localScale.x);
    }
}
