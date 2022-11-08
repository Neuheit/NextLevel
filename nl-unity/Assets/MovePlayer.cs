using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Vector3 moveDir;
    float speed = 15;
    float jumpSpeed = 6000f;
    float gravity = 200f;

    float height;

    Rigidbody rb;

    bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void OnCollisionStay(Collision other) 
    {
        //isGrounded = true;
    }

    // Update is called once per frame
    void Move()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, 1f) && !isGrounded)
        {
            height = hit.distance;
            isGrounded = true;
        }

        if(isGrounded)
            transform.position = new Vector3(transform.position.x, height, transform.position.z);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        transform.Translate(dir * speed * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
            isGrounded = false;
        }
        /*
        var movX = Input.GetAxis("Horizontal");
        var movZ = Input.GetAxis("Vertical");

        moveDir = new Vector3(movX, 0, movZ);
        moveDir = transform.TransformDirection(moveDir);

        moveDir *= speed;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = jumpSpeed;
        }

        moveDir.y -= gravity * speed * Time.deltaTime;

        charCon.Move(moveDir * Time.deltaTime);
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BuffBox"))
        {
            rb.AddForce(new Vector3(0, 45f, 0), ForceMode.VelocityChange);
            Destroy(other.gameObject);
        }
    }
}
