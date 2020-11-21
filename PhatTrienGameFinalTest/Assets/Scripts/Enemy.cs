using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target; //the enemy's target
    public float moveSpeed = 1.5f; //move speed
    public bool isDeath;
    public float timeRespawn;

    private Vector3 positionSpawnLeft;
    private Vector3 positionSpawnRight;
    private Vector3 positionSpawnTop;
    private Vector3 positionSpawnBottom;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("MainCharacter").gameObject;
        isDeath = false;
        timeRespawn = 2f;

        positionSpawnLeft = new Vector3(-8.5f, Random.Range(-4.5f, 4.5f), 0);
        positionSpawnRight = new Vector3(8f, Random.Range(-4.5f, 4.5f), 0);
        positionSpawnTop = new Vector3(Random.Range(-8.5f, 8.5f), 5f, 0);
        positionSpawnBottom = new Vector3(Random.Range(-8.5f, 8.5f), -5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        FollowMainCharacter();
        DeathAndReSpawn();
    }

    private void FollowMainCharacter()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        Vector3 difference = target.transform.position - transform.position;  //tính vị trí giữa con trỏ và nhân vật
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //Mathf.Atan2: trả về số đo góc được tính bằng đơn vị radians sao cho tan của góc đó chính bằng thương số của hai tham số truyền vào        // Mathf.Rad2Deg = 360/(pi*2)
        this.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MysticShot")
        {
            isDeath = true;
            target.GetComponent<MainCharacter>().AddScore();
        }

    }

    private void DeathAndReSpawn()
    {
        if (isDeath)
        {
            this.transform.position = new Vector3(0, 50, 0);     //đưa nhân vật ra khỏi màn hình không thể nhìn
            timeRespawn -= Time.deltaTime;
            if (timeRespawn < 0)        //khi thời gian hồi sinh hết
            {
                int x = (int)Random.Range(1, 4);
                switch (x)
                {
                    case 1:
                        this.transform.position = positionSpawnTop;
                        break;
                    case 2:
                        this.transform.position = positionSpawnRight;
                        break;
                    case 3:
                        this.transform.position = positionSpawnBottom;
                        break;
                    case 4:
                        this.transform.position = positionSpawnLeft;
                        break;
                }
                timeRespawn = 2f;               //đặt lại thời gian hồi sinh 
                isDeath = false;
            }
        }
    }
}
