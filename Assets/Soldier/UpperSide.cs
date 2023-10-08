using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSide : MonoBehaviour
{
    private Animator anim;

    public Animator Anim   // property
    {
        get { return anim; }
        set { anim = value; }
    }

    private GameObject player;

    private float CrossHairDistance;

    private GameObject Weapon;

    private GameObject CrossHair; 

    private WeaponBarril weaponScript;

    private Soldier soldierScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        Transform[] ObjectChildrens = gameObject.GetComponentsInChildren<Transform>();
        Weapon = System.Array.Find(ObjectChildrens, p => p.gameObject.name == "Barril").gameObject;
        CrossHair = GameObject.FindGameObjectsWithTag("crosshair")[0];
        player = transform.parent.gameObject;
        soldierScript = player.GetComponent<Soldier>();
        CrossHairDistance = soldierScript.CrossHairDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;

        CrossHair.transform.position = moduloMouse(mousePosition);

        var dir = CrossHair.transform.position - player.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if(soldierScript.getAnim() != "walk"){

            if(player.transform.localScale == new Vector3(-1, 1, 1)){

                transform.localScale = new Vector3(-1, -1, 1);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }else if(player.transform.localScale == new Vector3(1, 1, 1)){

                transform.localScale = new Vector3(1, 1, 1);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            }

        }

        

        
        
        if(CrossHair.transform.position.x - player.transform.position.x < 0){

            if(player.transform.localScale != new Vector3(-1, 1, 1)){
                player.transform.localScale = new Vector3(-1, 1, 1);
            }

        }else if(CrossHair.transform.position.x - player.transform.position.x > 0){

            if(player.transform.localScale != new Vector3(1, 1, 1)){
                player.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if(Input.GetMouseButton(0)){
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "fire"){
                shoot();
            }
        }
        
    }

    public void shoot(){
        WeaponBarril weaponScript = Weapon.GetComponent<WeaponBarril>();

        var dir = CrossHair.transform.position - player.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        anim.Play("fire");
    }

    Vector3 moduloMouse(Vector3 mousePosition){

        Vector3 mousePositionLocal = mousePosition;
        Vector2 mouseRelativo = mousePositionLocal - player.transform.position;

        float modulo = mouseRelativo.magnitude;
        
        if (modulo > CrossHairDistance)
            mousePositionLocal = player.transform.position + (Vector3)(CrossHairDistance * mouseRelativo / modulo);
        
        return mousePositionLocal;
    }
}
