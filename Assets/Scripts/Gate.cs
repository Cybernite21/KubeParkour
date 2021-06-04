using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : activatedByButtons
{

    public static event System.Action wonLevel;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && activated)
        {
            if(wonLevel != null)
            {
                wonLevel();
            }
        }
    }
}
