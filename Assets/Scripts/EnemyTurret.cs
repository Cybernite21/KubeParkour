using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{

    //turret parts
    public GameObject turretNeck;
    public GameObject turretHead;

    //turret parameters
    public float plrDistanceToAcivate = 3f;
    public float neckSmoothSpeed = 4f;
    public float headSmoothSpeed = 2f;

    GameObject plr;

    public bool isPlr;
    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        activeTurret();
    }

    void activeTurret()
    {
        if (active)
        {
            checkForPlr();
            if (isPlr)
            {
                Vector3 posToLook = plr.transform.position;
                Vector3 neckLookPos = new Vector3(posToLook.x, turretNeck.transform.position.y, posToLook.z);
                Vector3 headLookPos = new Vector3(posToLook.x, turretHead.transform.position.y, posToLook.z);

                //turn turret to face plr
                Quaternion newNeckRot = Quaternion.LookRotation(posToLook - turretNeck.transform.position);
                turretNeck.transform.rotation = Quaternion.RotateTowards(turretNeck.transform.rotation, newNeckRot, neckSmoothSpeed * Time.deltaTime);

                Quaternion newHeadRot = Quaternion.LookRotation(posToLook - turretHead.transform.position);
                turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, newHeadRot, headSmoothSpeed * Time.deltaTime);
            }
        }
    }

    void checkForPlr()
    {
        if((plr.transform.position - transform.position).magnitude <= plrDistanceToAcivate)
        {
            isPlr = true;
        }
        isPlr = false;
    }
}
