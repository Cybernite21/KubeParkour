using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IDamageable
{
    //Public Events Player Will Call
    public static event Action playerDeath; 

    //IDamageable Variables
    private int health = 100;
    private int airInTank = 25;
    
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

    //Health and AirInTank Sliders
    public Slider AirInTankSlider;
    public Slider HealthSlider;

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
        AirInTankSlider.maxValue = AirInTank;
        HealthSlider.maxValue = Health;
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

        //Update Bars
        if(AirInTankSlider.value != AirInTank)
        {
            AirInTankSlider.value = AirInTank;
        }
        if (HealthSlider.value != Health)
        {
            HealthSlider.value = Health;
        }

        //Check For Death
        if(Health <= 0)
        {
            if(playerDeath != null)
            {
                playerDeath();
            }
        }
    }

    void FixedUpdate()
    {
        //Scaling
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button4))
        {
            if (transform.localScale.x - Vector3.one.x * scaleSpeed * Time.deltaTime >= scaleClamp[0].x)
            {
                transform.localScale = transform.localScale - Vector3.one * scaleSpeed * Time.fixedDeltaTime;
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
            if (transform.localScale.x + Vector3.one.x * scaleSpeed * Time.fixedDeltaTime <= scaleClamp[1].x)
            {
                transform.localScale = transform.localScale + Vector3.one * scaleSpeed * Time.fixedDeltaTime;
                rb.mass = Mathf.Clamp(rb.mass - scaleSpeed * Time.fixedDeltaTime, rbMassMinMax.x, rbMassMinMax.y);
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
        rb.MoveRotation(Quaternion.Euler((Vector3.up * turn * rotSpeed * Time.fixedDeltaTime) + transform.eulerAngles));

        //movement
        rb.MovePosition(transform.position + -movement.normalized * moveSpeed * Time.fixedDeltaTime);

        //clamp rotation
        //rb.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(rb.rotation.x, -rotationClamp.x, rotationClamp.x), Mathf.Clamp(rb.rotation.y, -rotationClamp.y, rotationClamp.y), Mathf.Clamp(rb.rotation.z, -rotationClamp.z, rotationClamp.z)));
    }

    //IDamageable Take Damage Function
    public void takeDamage(int damage)
    {
        int _dmg = damage;
        //Subract airInTank first, then health when in water;
        if(AirInTank > 0)
        {
            damage -= airInTank;
            AirInTank = Mathf.Clamp(AirInTank - _dmg, 0, AirInTank);

            if(damage > 0)
            {
                Health = Mathf.Clamp(Health - damage, 0, Health);
            }
        }

        else
        {
            Health = Mathf.Clamp(Health - damage, 0, Health);
        }
    }

    //IDamageable Variables
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int AirInTank
    {
        get
        {
            return airInTank;
        }

        set
        {
            airInTank = value;
        }
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
