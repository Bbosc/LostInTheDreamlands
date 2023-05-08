using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    GameObject target;
    public Vector3[] SpawnPos;
    private float StopDist = 1f;

    public bool Spawnable = false;
    public bool Spawned = false;
    public bool EnemyDead = false;


    List<GameObject> list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        // for (int i = 0; i < spawners.Length; i++)
        // {
        //     spawners[i] = transform.GetChild(i).gameObject;
        // }

        SpawnPos = new Vector3[4];

        SpawnPos[0] = new Vector3(-125f, -52f, 135f);
        SpawnPos[1] = new Vector3(-116, -52f, 121f);
        SpawnPos[2] = new Vector3(-130, -52f, 100f);
        SpawnPos[3] = new Vector3(-145, -52f, 107f);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     SpawnEnemy();
        // }
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if ((dist <= StopDist) & (Spawned == false))
        {
            Spawned = true;
            Spawnable = true;
        }



        if (Spawnable)
        {
            for(int i = 0; i < SpawnPos.Length; ++i)
            {
                SpawnEnemy(i);
            }
            Spawnable = false;
        }
        
        // if (enemy_array[0].GetComponent<Dead> == true)
        // {
        //     Debug.Log("VICTORY");
        // }

        
        for(int i = 0; i < list.Count; ++i)
        {
            EnemyDead = true;
                if (list[i].GetComponentInChildren<Enemy>().Dead != true)
                {  
                    EnemyDead = false;
                    break;
                }
        }

        if (EnemyDead == true)
        {
            Debug.Log("Victory");
        }

    }

    void SpawnEnemy(int i)
    {
        Vector3 position = SpawnPos[i];
        list.Add(Instantiate(enemy, position, new Quaternion(0, 0, 0, 0)));


        // enemy_array[i] = (GameObject)GameObject.Instantiate;
        
    }



}
