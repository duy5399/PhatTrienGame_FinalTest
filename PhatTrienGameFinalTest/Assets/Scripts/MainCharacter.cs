using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    Rigidbody2D rb2D;
    Animator anim;
    public float moveSpeed;         //tốc độ di chuyển
    
    public GameObject mysticShot;   //Mystic Shot
    public Transform mysticShotSpawn;   //vị trí Mystic Shot xuất hiện

    public long score;
    public GameObject scoreTXT;
    public GameObject gameOverIMG;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        moveSpeed = 2f;
        score = 0;
        scoreTXT = GameObject.Find("Canvas").transform.Find("ScoreTXT").gameObject;
        gameOverIMG = GameObject.Find("Canvas").transform.Find("GameOverImg").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        ShowScore();
    }

    //di chuyển nhân vật
    private void Movement()
    {
        var moveHorizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0);    //di chuyển theo chiều ngang //GetAxisRaw: trả về -1 0 1
        this.transform.position += moveHorizontal * moveSpeed * Time.deltaTime;      //Time.delta là khoảng thời gian giữa 2 frame
        var moveVertical = new Vector3(0, Input.GetAxisRaw("Vertical"));        //di chuyển theo chiều dọc
        this.transform.position += moveVertical * moveSpeed * Time.deltaTime;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;  //tính vị trí giữa con trỏ và nhân vật
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //Mathf.Atan2: trả về số đo góc được tính bằng đơn vị radians sao cho tan của góc đó chính bằng thương số của hai tham số truyền vào        // Mathf.Rad2Deg = 360/(pi*2)
        this.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ );  //Trả về một phép quay xoay các độ z quanh trục z, độ x quanh trục x và độ y quanh trục y; áp dụng theo thứ tự đó.
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("isMovement", true);
        }
        else
        {
            anim.SetBool("isMovement", false);
        }
    }

    //bắn
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))    //click chuột trái thì gọi hàm khởi tạo Bullet
        {
            GameObject shot = Instantiate(mysticShot, mysticShotSpawn.position, mysticShotSpawn.rotation);   //Instantiate: khởi tạo Mystic Shot
            anim.SetBool("isShoot", true);
        }
        else
        {
            anim.SetBool("isShoot", false);
        }
    }

    public void AddScore()
    {
        score+= 10;
    }

    private void ShowScore()
    {
        scoreTXT.GetComponent<Text>().text = "Score: " + score;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameOverIMG.SetActive(true);
            Time.timeScale = 0;
            //Destroy(gameObject);
        }

    }
}
