using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Soldier");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player!= null){
            transform.position=new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        }
        

    }
}
