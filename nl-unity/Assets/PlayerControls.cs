using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    private float horizontalMovement;
    private float verticalMovement;

    // A field editable from inside Unity with a default value of 5
    public float speed = 2.0f;

    public float turnSpeed = 4.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;

    private float rotX;

    public Camera cam;

    // How much will the player slide on the ground
    // The lower the value, the greater distance the user will slide
    //public float drag;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;
        // clamp the vertical rotation
        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
        // rotate the camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);

        // This will detect forward and backward movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This will detect sideways movement
        verticalMovement = Input.GetAxisRaw("Vertical");

        Debug.Log(cam.transform.forward);

        // Calculate the direction to move the player
        Vector3 movementDirection = cam.transform.forward * verticalMovement + cam.transform.right * horizontalMovement;
        
        // Move the player
        rb.transform.Translate(movementDirection * speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(new Vector3(0, 5f, 0), ForceMode.VelocityChange);

        // Apply drag
        //rb.drag = drag;
    }
}
