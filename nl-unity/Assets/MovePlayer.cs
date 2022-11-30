using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public AudioSource jumpingAudio;
    public AudioSource walkingAudio;

    Vector3 moveDir;
    float speed = 15;
    float jumpSpeed = 6000f;
    float gravity = 200f;

    float height;

    Rigidbody rb;

    bool isGrounded;
    bool isWalking;

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
        /*
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out var hit, 1f) && !isGrounded)
        {
            height = hit.distance;
            isGrounded = true;
        }

        if(isGrounded)
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        */

        var grounded = Physics.Raycast(transform.position, Vector3.down, 3);

        Vector3 dir = new Vector3(0, 0, 0);
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        //Debug.Log(dir);

        if(dir != Vector3.zero)
        {
            if(!isWalking && grounded)
            {
                isWalking = true;
                walkingAudio.Play();
            }

            if(!grounded)
            {
                walkingAudio.Stop();
                isWalking = false;
            }

        }
        else
        {
            if(isWalking)
            {
                isWalking = false;
                walkingAudio.Stop();
            }
                
        }

        transform.Translate(dir * speed * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * speed * 1.5f, ForceMode.Impulse);
            jumpingAudio.Play();
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
        if(other.gameObject.CompareTag("BuffBox"))//High Jump Buff
        {
            rb.AddForce(new Vector3(0, 45f, 0), ForceMode.VelocityChange);
            jumpingAudio.Play();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("BuffBox+"))//Super High Jump Buff
        {
            rb.AddForce(new Vector3(0, 60f, 0), ForceMode.VelocityChange);
            jumpingAudio.Play();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("DeBuff"))//Displacement Debuff
        {
            rb.transform.position += new Vector3(20f,15f,0);
            Destroy(other.gameObject);
        }
    }
}
