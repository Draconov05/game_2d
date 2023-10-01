using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class BaseWeapon : MonoBehaviour
{

    // public float effective_range;
    // public string caliber;
    // public int recuo;
    public List<string> fire_modes = new List<string>() { "single" };
    // public int fire_rate;
    public int mag_capacity = 30;
    // private GameObject bullet;
    // private Animation anim;
    // public List<GameObject> mag_bullets = new List<GameObject>() {};

    public Text Mag;

    private bool esperaConcluida = true;

    private float tempoDecorrido = 0.0f;

    private float tempoDeEspera = 0.5f;

    private int mag_bullets = 30;

    private GameObject Weapon;

    private WeaponBarril weaponScript;

    private GameObject CrossHair;

    private GameObject Player;

    private Soldier player_script;

    private bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        CrossHair = GameObject.FindGameObjectsWithTag("crosshair")[0];

        Player = GameObject.FindGameObjectsWithTag("player")[0];

        if(Player != null){
            player_script = Player.GetComponent<Soldier>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dir = CrossHair.transform.position - Player.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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

        if (Input.GetMouseButtonDown(0) && player_script.Anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "fire")
        {
            fire();
        }

        if (Input.GetMouseButton(0))
        {
            if(fire_modes.Contains("auto")){
                fire();
            }
            
        }else if(Input.GetKeyDown("r")){ //walk animation
            reload();
        }

        if(esperaConcluida && reloading){
            mag_bullets += 1;

            if(mag_bullets >= mag_capacity){
                reloading = false;
                esperaConcluida = true;
            }else{
                esperaConcluida = false;
                tempoDecorrido = 0.0f;
                tempoDeEspera = 0.5f;
            }
        }
        if(Mag != null){
            Mag.text = "" + mag_bullets + "/" + mag_capacity + " bullets";
        }
    }

    public void reload()
    {
        reloading = true;
    }

    public void fire()
    {   
        if(mag_bullets > 0){
            reloading = false;
            player_script.shoot();
            mag_bullets -= 1;
        }else{
            reloading = true;
        }
    }

}