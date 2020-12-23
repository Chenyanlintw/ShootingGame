using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float speed;
    public float turnSpeed;

    public GameObject bullet;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 虛擬搖桿方向
        Vector3 inputVec = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        // 鍵盤方向鍵
        if (inputVec.magnitude == 0)
            inputVec = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // 移動
        rb.AddForce(inputVec * speed * Time.deltaTime * 100);

        // 調整面對方向
        if (inputVec.magnitude > 0)
        {
            Quaternion r = Quaternion.LookRotation(inputVec);
            transform.rotation = Quaternion.Lerp(transform.rotation, r, Time.deltaTime * turnSpeed);
        }

        // 鍵盤
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    // 開火
    public void Fire()
    {
        // 複製子彈到面前
        GameObject b = Instantiate(bullet);
        b.transform.position = transform.position + transform.forward * 1;

        // 發射
        b.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }

    
}
