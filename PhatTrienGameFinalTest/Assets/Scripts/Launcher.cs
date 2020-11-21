using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeToUpLevel;
    public Vector3 positionSpawnLeft;
    public Vector3 positionSpawnRight;
    public Vector3 positionSpawnTop;
    public Vector3 positionSpawnBottom;
    // Start is called before the first frame update
    void Start()
    {
        timeToUpLevel = 3f;
        positionSpawnLeft = new Vector3(-8.5f, Random.Range(-4.5f, 4.5f), 0);
        positionSpawnRight = new Vector3(8f, Random.Range(-4.5f, 4.5f), 0);
        positionSpawnTop = new Vector3(Random.Range(-8.5f, 8.5f), 5f, 0);
        positionSpawnBottom = new Vector3(Random.Range(-8.5f, 8.5f), -5f, 0);
        GameObject enemy = Instantiate(enemyPrefab, positionSpawnTop, enemyPrefab.transform.rotation);   //Instantiate: khởi tạo Enemy
    }

    // Update is called once per frame
    void Update()
    {
        UpLevel();
    }
    private void UpLevel()
    {
        timeToUpLevel -= Time.deltaTime;
        if (timeToUpLevel < 0)        //khi thời gian hồi sinh hết
        {
            int x = (int)Random.Range(1,4);
            switch(x){
                case 1:
                    Instantiate(enemyPrefab, positionSpawnTop, enemyPrefab.transform.rotation);   //Instantiate: khởi tạo Enemy
                    break;
                case 2:
                    Instantiate(enemyPrefab, positionSpawnRight, enemyPrefab.transform.rotation);   //Instantiate: khởi tạo Enemy
                    break;
                case 3:
                    Instantiate(enemyPrefab, positionSpawnBottom, enemyPrefab.transform.rotation);   //Instantiate: khởi tạo Enemy
                    break;
                case 4:
                    Instantiate(enemyPrefab, positionSpawnLeft, enemyPrefab.transform.rotation);   //Instantiate: khởi tạo Enemy
                    break;
            }
            Debug.Log(x);
            timeToUpLevel = 3f;
        }
    }
}
