using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleButton : MonoBehaviour
{
    public Material on;
    public Material off;

    GameManager gManager;

    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if(isOn)
        {
            Material[] mats = GetComponent<Renderer>().materials;
            mats[1] = off;
            GetComponent<Renderer>().materials = mats;
            isOn = false;
        }
        else
        {
            Material[] mats = GetComponent<Renderer>().materials;
            mats[1] = on;
            GetComponent<Renderer>().materials = mats;
            isOn = true;
        }
    }
}
