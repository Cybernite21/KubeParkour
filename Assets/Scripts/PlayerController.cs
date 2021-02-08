using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float scaleSpeed = 2f;
    public float jumpPower = 2f;
    public float moveSpeed = 2f;
    public float rotSpeed = 2f;

    public Vector3[] scaleClamp = new Vector3[2] {new Vector3(.25f, .25f, .25f), new Vector3(4, 4, 4)};
    Vector3 movement;
    float turn;

    public Vector2 rbMassMinMax;

    private Rigidbody rb;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        //movement input
        movement = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        //look input
        turn = Input.GetAxis("Debug Horizontal");
    }

    void FixedUpdate()
    {
        //Scaling
        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.localScale.x - Vector3.one.x * scaleSpeed * Time.deltaTime >= scaleClamp[0].x)
            {
                transform.localScale = transform.localScale - Vector3.one * scaleSpeed * Time.deltaTime;
                rb.mass = Mathf.Clamp(rb.mass + scaleSpeed * Time.deltaTime, rbMassMinMax.x, rbMassMinMax.y);
            }
            else
            {
                transform.localScale = scaleClamp[0];
                rb.mass = rbMassMinMax.y;
            }
        }
        else
        {
            if (transform.localScale.x + Vector3.one.x * scaleSpeed * Time.deltaTime <= scaleClamp[1].x)
            {
                transform.localScale = transform.localScale + Vector3.one * scaleSpeed * Time.deltaTime;
                rb.mass = Mathf.Clamp(rb.mass - scaleSpeed * Time.deltaTime, rbMassMinMax.x, rbMassMinMax.y);
            }
            else
            {
                transform.localScale = scaleClamp[1];
                rb.mass = rbMassMinMax.x;
            }
        }

        //Jumping
        if(Input.GetKeyDown(KeyCode.E) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }

        //turning
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.up * turn * rotSpeed));

        //movement
        rb.MovePosition(transform.position + -movement.normalized * moveSpeed * Time.deltaTime);
    }

    //Check if on ground
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    //Check if on ground
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    //Check if not on ground
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
