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
    public int fire_rate;
    public int mag_capacity = 30;
    private Animator anim;
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

    private GameObject UpperSide;

    private UpperSide UpperSide_script;

    private bool reloading = false;

    public AudioSource fireSound;

    public AudioSource reloadSound;

    public AudioSource emptySound;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        if(!fireSound) fireSound = GetComponent<AudioSource>();
        
        if(!reloadSound) reloadSound = GetComponent<AudioSource>();

        if(!emptySound) emptySound = GetComponent<AudioSource>();

        CrossHair = GameObject.FindGameObjectsWithTag("crosshair")[0];

        Player = GameObject.FindGameObjectsWithTag("player")[0];

        Weapon = GameObject.FindGameObjectsWithTag("weapon")[0];

        if(Weapon != null){
            weaponScript = Weapon.GetComponent<WeaponBarril>();
        }

        if(Player != null){
            player_script = Player.GetComponent<Soldier>();
        }

        Transform[] ObjectChildrens = Player.GetComponentsInChildren<Transform>();
        UpperSide = System.Array.Find(ObjectChildrens, p => p.gameObject.name == "UpperSide").gameObject;

        if(UpperSide != null){
            UpperSide_script = UpperSide.GetComponent<UpperSide>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var dir = CrossHair.transform.position - Player.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (!esperaConcluida)
        {
            // Incrementa o tempo decorrido
            tempoDecorrido += Time.deltaTime;

            // Verifica se o tempo decorrido atingiu o tempo de espera desejado
            if (tempoDecorrido >= tempoDeEspera)
            {
                esperaConcluida = true;
            }
        }else{
             if (Input.GetMouseButtonDown(0))
            {
                fire();
                esperaConcluida = false;
                tempoDecorrido = 0.0f;
                tempoDeEspera = 1/((float) fire_rate/60);
            }

            if (Input.GetMouseButton(0))
            {
                if(fire_modes.Contains("auto")){
                    fire();
                    esperaConcluida = false;
                    tempoDecorrido = 0.0f;
                    tempoDeEspera = 1/((float) fire_rate/60);
                }
                
            }else if(Input.GetKeyDown("r")){ //walk animation
                reload();
            }

            if(reloading){
                mag_bullets = 30;
                reloadSound.Play(0);
                if(mag_bullets >= mag_capacity){
                    reloading = false;
                    esperaConcluida = true;
                }else{
                    esperaConcluida = false;
                    tempoDecorrido = 0.0f;
                    tempoDeEspera = 1.5f;
                }
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
            fireSound.Play(0);
            if(UpperSide_script){
                UpperSide_script.shoot();
            }
            anim.Play("fire");
            weaponScript.fire();
            mag_bullets -= 1;
        }else{
            emptySound.Play(0);
            esperaConcluida = false;
            tempoDecorrido = 0.0f;
            tempoDeEspera = 0.3f;
        }
    }

}