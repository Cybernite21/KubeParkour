using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Color[] scaleColors = new Color[2];

    public float scaleSpeed = 2f;
    public float jumpPower = 2f;
    public float moveSpeed = 2f;
    public float rotSpeed = 2f;
    public float scaleFactor;
    public float turnSensitivity = .9f;

    public Vector3[] scaleClamp = new Vector3[2] {new Vector3(.25f, .25f, .25f), new Vector3(4, 4, 4)};
    public Vector3 rotationClamp = new Vector3(20, 360, 20);
    Vector3 movement;


    float turn;

    public Vector2 rbMassMinMax;

    private Rigidbody rb;

    public Transform top;

    public bool isGrounded;
    public bool jump;

    [HideInInspector]
    public GameObject orien;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        orien = new GameObject("Temp");
        //orien = (GameObject)Instantiate(new GameObject("Temp"), transform.position, transform.rotation);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement input
        orien.transform.position = transform.position;
        orien.transform.rotation = transform.rotation;
        orien.transform.rotation = Quaternion.Euler(new Vector3(0, orien.transform.eulerAngles.y, 0));
        movement = orien.transform.forward * Input.GetAxisRaw("Vertical") + orien.transform.right * Input.GetAxisRaw("Horizontal");

        //look input
        turn = Input.GetAxis("Debug Horizontal") * turnSensitivity;

        //Jump Input
        if(Input.GetKeyDown(KeyCode.E) && isGrounded || Input.GetKeyDown(KeyCode.Joystick1Button0) && isGrounded || Input.GetKeyDown(KeyCode.Joystick1Button5) && isGrounded)
        {
            jump = true;
        }

        //Change color based on scale
        scaleFactor = Remap(transform.localScale.x, scaleClamp[0].x, scaleClamp[1].x, 0f, 1f);
        GetComponent<Renderer>().material.color = Color.Lerp(scaleColors[0], scaleColors[1], scaleFactor);
    }

    void FixedUpdate()
    {
        //Scaling
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button4))
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
        if(jump && isGrounded)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }

        //turning
        rb.MoveRotation(Quaternion.Euler((Vector3.up * turn * rotSpeed) + transform.eulerAngles));

        //movement
        rb.MovePosition(transform.position + -movement.normalized * moveSpeed * Time.deltaTime);

        //clamp rotation
        //rb.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(rb.rotation.x, -rotationClamp.x, rotationClamp.x), Mathf.Clamp(rb.rotation.y, -rotationClamp.y, rotationClamp.y), Mathf.Clamp(rb.rotation.z, -rotationClamp.z, rotationClamp.z)));
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

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
