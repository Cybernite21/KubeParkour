using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public static event System.Action wonLevel;
    public simpleButton[] buttonsToActivate;
    List<simpleButton> btnsTemp = new List<simpleButton>();
    bool activated = false;
    public int buttonsOnNum = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonsToActivate.Length > 0)
        {
            foreach (simpleButton btn in buttonsToActivate)
            {
                if(btn.isOn)
                {
                    if(!btnsTemp.Contains(btn))
                    {
                        btnsTemp.Add(btn);
                    }
                }
                else
                {
                    if(btnsTemp.Contains(btn))
                    {
                        btnsTemp.Remove(btn);
                    }
                }
            }
            buttonsOnNum = btnsTemp.Count;
            if (buttonsOnNum == buttonsToActivate.Length)
            {
                activated = true;
            }
            else
            {
                activated = false;
            }
        }
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
