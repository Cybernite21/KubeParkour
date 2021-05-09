using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour
{
    public GameManager gManager;

    private List<IDamageable> objectsToDamage = new List<IDamageable>();
    public int screenRippleMatStrengthName;

    public int damage = 10;
    public int damageRateInSeconds = 2;

    private float _timer;

    public float destinationHeight = 5;
    public float floodSpeed = .5f;
    public float delayToStartFlood = 5;
    public float maxScreenRippleStrength = .005f;
    public float screenRippleFadeSpeed = 5;

    public Material screenRippleMatOriginal;
    public Material screenRippleMatCopy;

    public Transform plr;

    [HideInInspector]
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
        if (gManager.gameSettings.useTheseSettings)
        {
            useGManagerSettings();
        }

        plr = GameObject.FindGameObjectWithTag("Player").transform;
        waterBox = GetComponent<BoxCollider>();
        Blit rippleBlit = forwardRendererData.rendererFeatures[forwardRendererData.rendererFeatures.Count-1] as Blit;
        screenRippleMatCopy = new Material(screenRippleMatOriginal);
        rippleBlit.settings.blitMaterial = screenRippleMatCopy;
        screenRippleMatCopy.SetFloat(screenRippleMatStrengthName, 0f);
        //print(rippleBlit.settings.blitMaterial.GetFloat(screenRippleMatStrengthName) + "" + rippleBlit.settings.blitMaterial.Equals(screenRippleMatCopy));
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

        screenRippleMatOriginal = gManager.gameSettings.screenRippleMat;
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
                /*if(obj.AirInTank > 0)
                {
                    print(obj.AirInTank);
                }
                else
                {
                    print(obj.Health);
                } */
            }
            _timer = Time.unscaledTime + damageRateInSeconds;
        }

        if(waterBox.bounds.Contains(plr.position))
        {
            float t = screenRippleMatCopy.GetFloat(screenRippleMatStrengthName);
            screenRippleMatCopy.SetFloat(screenRippleMatStrengthName, Mathf.Clamp(t + (maxScreenRippleStrength/screenRippleFadeSpeed * Time.deltaTime), 0, maxScreenRippleStrength));
            forwardRendererData.SetDirty();
        }
        else
        {
            float t = screenRippleMatCopy.GetFloat(screenRippleMatStrengthName);
            screenRippleMatCopy.SetFloat(screenRippleMatStrengthName, Mathf.Clamp(t - (maxScreenRippleStrength / screenRippleFadeSpeed * Time.deltaTime), 0, maxScreenRippleStrength));
            forwardRendererData.SetDirty();
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
