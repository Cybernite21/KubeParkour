using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatedByButtons : MonoBehaviour
{

    public simpleButton[] buttonsToActivate;
    protected List<simpleButton> btnsTemp = new List<simpleButton>();
    protected bool activated = false;
    protected int buttonsOnNum = 0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        checkButtons();
    }

    public virtual void checkButtons()
    {
        if (buttonsToActivate.Length > 0)
        {
            foreach (simpleButton btn in buttonsToActivate)
            {
                if (btn.isOn)
                {
                    if (!btnsTemp.Contains(btn))
                    {
                        btnsTemp.Add(btn);
                    }
                }
                else
                {
                    if (btnsTemp.Contains(btn))
                    {
                        btnsTemp.Remove(btn);
                    }
                }
            }
            buttonsOnNum = btnsTemp.Count;
            if (buttonsOnNum == buttonsToActivate.Length)
            {
                activated = true;
                GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                activated = false;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
