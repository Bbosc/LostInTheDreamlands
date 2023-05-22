using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyboss;
    public GameObject ball;
    GameObject target;
    public Vector3[] SpawnPos;
    private float StopDist = 10f;

    public bool Spawnable = false;
    public bool Spawned = false;
    public bool EnemyDead = false;
    public bool SpawnBoss = false;
    public bool SpawnedBoss = false;
    public bool BossDead = false;

    List<GameObject> list = new List<GameObject>();
    GameObject Boss;
    GameObject Ball;
    Vector3 PosBall;
    GameObject Fragment;
    // Start is called before the first frame update

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        // for (int i = 0; i < spawners.Length; i++)
        // {
        //     spawners[i] = transform.GetChild(i).gameObject;
        // }

        SpawnPos = new Vector3[4];
        Vector3 position = new Vector3(-140f, -45f, 140f);
        SpawnPos[0] = new Vector3(-10f, 1f, -10f) + position;
        SpawnPos[1] = new Vector3(-10f, 1f, 10f) + position;
        SpawnPos[2] = new Vector3(10f, 1f, -10f) + position;
        SpawnPos[3] = new Vector3(10f, 1f, 10f) + position;
        PosBall = transform.position;
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
            SpawnBoss = true;
        }

        if ((SpawnBoss == true) & (SpawnedBoss == false))
        {
            SpawnedBoss = true;
            Vector3 Pos = new Vector3(-263, -234f, 277f);
           

            Boss = (Instantiate(enemyboss, Pos, new Quaternion(0, 0, 0, 0)));
            Ball = (Instantiate(ball, PosBall, new Quaternion(0, 0, 0, 0)));
        }

        // if (SpawnedBoss == true) CheckContact();

        if (SpawnedBoss == true)
        {
            CheckContact();
            if (Boss.GetComponentInChildren<EnemyBoss>().Dead == true)
            {
                BossDead = true;
            }

            if (BossDead == true)
            {
                Debug.Log("Victory");
            }
        }
    }

    void SpawnEnemy(int i)
    {
        Vector3 position = SpawnPos[i];
        list.Add(Instantiate(enemy, position, new Quaternion(0, 0, 0, 0)));  
    }

    void CheckContact()
    {
        // float dist = Vector3.Distance(Boss.transform.GetChild(0).transform.position, Ball.transform.GetChild(0).transform.position);
        
        // if (dist < 10f)
        // {
        //     Boss.GetComponentInChildren<EnemyBoss>().Dead = true;
        // }

        float distplayer = Vector3.Distance(target.transform.position, Ball.transform.GetChild(0).transform.position);
        // Debug.Log('Positions ');
        // Debug.Log(Ball.transform.GetChild(0).transform.position);
        // Debug.Log(Boss.transform.GetChild(0).transform.position);
        
        if (distplayer > 20f) 
        {
            Destroy(Ball);
            Ball = (Instantiate(ball, PosBall, new Quaternion(0, 0, 0, 0)));
        }

        if (Ball.GetComponentInChildren<Projectile>().HitRock == true)
        {
            Destroy(Ball);
            Ball = (Instantiate(ball, PosBall, new Quaternion(0, 0, 0, 0)));
        }
    }
    

}
