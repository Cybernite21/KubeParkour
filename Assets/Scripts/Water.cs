using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour
{
    public GameManager gManager;

    private List<IDamageable> objectsToDamage = new List<IDamageable>();
    private int screenRippleMatStrengthName;

    public int damage = 10;
    public int damageRateInSeconds = 2;

    private float _timer;

    public float destinationHeight = 5;
    public float floodSpeed = .5f;
    public float delayToStartFlood = 5;
    public float maxScreenRippleStrength = .005f;
    public float screenRippleFadeSpeed = 5;

    public Material screenRippleMat;

    public Transform plr;

    public ForwardRendererData forwardRendererData;

    [HideInInspector]
    public BoxCollider waterBox;

    private void Awake()
    {
        screenRippleMatStrengthName = Shader.PropertyToID("Vector1_4631a28c3df1447e9e4f219f032d345c");
    }

    // Start is called before the first frame update
    void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
        if(gManager.gameSettings.useTheseSettings)
        {
            useGManagerSettings();
        }

        plr = GameObject.FindGameObjectWithTag("Player").transform;
        waterBox = GetComponent<BoxCollider>();
        Blit rippleBlit = forwardRendererData.rendererFeatures[0] as Blit;
        screenRippleMat = new Material(screenRippleMat);
        rippleBlit.settings.blitMaterial = screenRippleMat;
        screenRippleMat.SetFloat(screenRippleMatStrengthName, 0f);
    }

    void useGManagerSettings()
    {
        damage = gManager.gameSettings.damage;
        damageRateInSeconds = gManager.gameSettings.damageRateInSeconds;
        destinationHeight = gManager.gameSettings.destinationHeight;
        floodSpeed = gManager.gameSettings.floodSpeed;
        delayToStartFlood = gManager.gameSettings.delayToStartFlood;
        maxScreenRippleStrength = gManager.gameSettings.maxScreenRippleStrength;
        screenRippleFadeSpeed = gManager.gameSettings.screenRippleFadeSpeed;

        screenRippleMat = gManager.gameSettings.screenRippleMat;
        forwardRendererData = gManager.gameSettings.forwardRendererData;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > delayToStartFlood && transform.position.y < destinationHeight)
        {
            if((transform.position + Vector3.up.normalized * floodSpeed * Time.deltaTime).y <= destinationHeight)
                transform.position = transform.position + Vector3.up.normalized * floodSpeed * Time.deltaTime;
            else
            {
                transform.position = new Vector3(transform.position.x, destinationHeight, transform.position.z);
            }
        }

        if(Time.unscaledTime > _timer && objectsToDamage.Count > 0)
        {
            //Damage Objects in List
            foreach(IDamageable obj in objectsToDamage)
            {
                obj.takeDamageFromWater(damage);
                //Debugging
                if(obj.AirInTank > 0)
                {
                    print(obj.AirInTank);
                }
                else
                {
                    print(obj.Health);
                } 
            }
            _timer = Time.unscaledTime + damageRateInSeconds;
        }

        if(waterBox.bounds.Contains(plr.position))
        {
            float t = screenRippleMat.GetFloat(screenRippleMatStrengthName);
            screenRippleMat.SetFloat(screenRippleMatStrengthName, Mathf.Clamp(t + (maxScreenRippleStrength/screenRippleFadeSpeed * Time.deltaTime), 0, maxScreenRippleStrength));
        }
        else
        {
            float t = screenRippleMat.GetFloat(screenRippleMatStrengthName);
            screenRippleMat.SetFloat(screenRippleMatStrengthName, Mathf.Clamp(t - (maxScreenRippleStrength / screenRippleFadeSpeed * Time.deltaTime), 0, maxScreenRippleStrength));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && !objectsToDamage.Contains(other.GetComponent<IDamageable>()))
        {
            if(waterBox.bounds.Contains(other.gameObject.transform.position))
                objectsToDamage.Add(other.GetComponent<IDamageable>());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null && !objectsToDamage.Contains(other.GetComponent<IDamageable>()))
        {
            if (waterBox.bounds.Contains(other.gameObject.transform.position))
                objectsToDamage.Add(other.GetComponent<IDamageable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null && objectsToDamage.Contains(other.GetComponent<IDamageable>()))
        {
            objectsToDamage.Remove(other.GetComponent<IDamageable>());
        }
    }
}
