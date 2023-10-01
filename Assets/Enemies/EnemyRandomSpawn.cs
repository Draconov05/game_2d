using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomSpawn : MonoBehaviour
{
    private GameObject WorldSettings;

    private WorldSettings WorldSettingsScript;

    private Vector3 Min;
    private  Vector3 Max;

    private bool esperaConcluida = true;

    private float tempoDecorrido = 0.0f;

    private float tempoDeEspera = 2.0f;

    private float maxTime = 6.0f;
    private float minTime = 3.0f;

    private  float _xAxis;
    private  float _yAxis;
    private  float _zAxis; //If you need this, use it

    private Vector3 _randomPosition ;
    public bool _canInstantiate = true;

    public GameObject Enemy;
    // Start is called before the first frame update
    private void Start()
    {
        SetRanges();

        WorldSettings = GameObject.FindGameObjectsWithTag("world")[0];

        if(WorldSettings != null){
            WorldSettingsScript = WorldSettings.GetComponent<WorldSettings>();
            setStatsByDiff();
        }        
    }
    private void Update()
    {

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

        _xAxis = UnityEngine.Random.Range(Min.x, Max.x);
        _yAxis = UnityEngine.Random.Range(Min.y, Max.y);
        _zAxis = UnityEngine.Random.Range(Min.z, Max.z);

        _randomPosition = new Vector3(_xAxis, _yAxis, _zAxis );

        if(esperaConcluida){
            InstantiateRandomObjects();
            esperaConcluida = false;
            tempoDecorrido = 0.0f;
            tempoDeEspera = UnityEngine.Random.Range(minTime, maxTime);
        }
    }

    private void SetRanges()
    {
        Min = new Vector3(-11, -5, 0); //Random value.
        Max = new Vector3(11, 5, 0); //Another ramdon value, just for the example.
    }

    private void setStatsByDiff(){
        switch(WorldSettingsScript.Difficulty){
            case "easy":
                minTime = 4.0f;
                maxTime = 6.5f;
                break;

            case "normal":
                minTime = 3.0f;
                maxTime = 6.0f;
                break;
            
            case "hard":
                minTime = 2.0f;
                maxTime = 5.0f;
                break;

            case "nightmare":
                minTime = 1.0f;
                maxTime = 4.5f;
                break;
            default:
                minTime = 4.0f;
                maxTime = 6.5f;
                break;
        }
    }

    private void InstantiateRandomObjects()
    {
        if (_canInstantiate)
        {
            Instantiate(Enemy, _randomPosition , Quaternion.identity);
        }
       
    }
}
