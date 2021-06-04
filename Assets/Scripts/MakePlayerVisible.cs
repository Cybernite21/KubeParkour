using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerVisible : MonoBehaviour
{

    GameObject objHidingPlr = null;
    GameObject plr;
    float origAlpha;

    public float newAlpha = 0.5f;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlr;
        distToPlr = Vector3.Distance(transform.position, plr.transform.position);
        Ray r = new Ray(transform.position, (plr.transform.position - transform.position).normalized);
        RaycastHit rHit;
        if(Physics.Raycast(r, out rHit, distToPlr + 0.5f, mask, QueryTriggerInteraction.Ignore))
        {
            if(objHidingPlr != rHit.collider.gameObject)
            {
                if(objHidingPlr != null)
                {
                    objHidingPlr.GetComponent<Renderer>().material.SetFloat("Vector1_b5cafce7c6a14629bf2593b4fa434c6b", origAlpha);
                }
            }
            objHidingPlr = rHit.collider.gameObject;
            origAlpha = objHidingPlr.GetComponent<Renderer>().material.GetFloat("Vector1_b5cafce7c6a14629bf2593b4fa434c6b");
            objHidingPlr.GetComponent<Renderer>().material.SetFloat("Vector1_b5cafce7c6a14629bf2593b4fa434c6b", newAlpha);
            //objHidingPlr.GetComponent<Renderer>().material.color = new Color(origColor.r, origColor.g, origColor.b, newAlpha);

        }
        else if(objHidingPlr != null)
        {
            objHidingPlr.GetComponent<Renderer>().material.SetFloat("Vector1_b5cafce7c6a14629bf2593b4fa434c6b", origAlpha);
        }
    }
}
