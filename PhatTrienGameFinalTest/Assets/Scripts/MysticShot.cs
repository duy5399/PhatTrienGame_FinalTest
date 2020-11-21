using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticShot : MonoBehaviour
{
    public float shootSpeed = 20f;
    public float destroyTime = 30f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime * Time.deltaTime);    //bất cứ khi nào đạn được khởi tạo nó sẽ chờ 2 giây sau đó hủy các MysticShot
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * shootSpeed * Time.deltaTime); //Translation là sự di chuyển object trong trục tọa độ X,Y hoặc Z
        //hàm trên giúp viên đạn bay đi liên tục, bỏ đi thì đạn chỉ xuất hiện hình ảnh ở đầu súng
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
}
