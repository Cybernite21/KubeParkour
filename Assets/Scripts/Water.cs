using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Water : MonoBehaviour
{
    private List<IDamageable> objectsToDamage = new List<IDamageable>();

    public int damage = 10;
    public int damageRateInSeconds = 2;

    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.unscaledTime > _timer && objectsToDamage.Count > 0)
        {
            //Damage Objects in List
            foreach(IDamageable obj in objectsToDamage)
            {
                obj.takeDamage(damage);
                //Debugging
                /*if(obj.AirInTank > 0)
                {
                    print(obj.AirInTank);
                }
                else
                {
                    print(obj.Health);
                }*/
            }
            _timer = Time.unscaledTime + damageRateInSeconds;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            objectsToDamage.Add(other.GetComponent<IDamageable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            objectsToDamage.Remove(other.GetComponent<IDamageable>());
        }
    }
}
