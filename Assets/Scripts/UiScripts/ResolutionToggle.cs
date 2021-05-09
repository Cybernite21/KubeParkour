using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionToggle : MonoBehaviour
{
    public Slider resolutionSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleResolution()
    {
        if(resolutionSlider.value == 0)
        {
            resolutionSlider.value = 1;
        }
        else
        {
            resolutionSlider.value = 0;
        }

        //Set Resolution
        if (resolutionSlider.value == 0)
        {
            Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        }
        else
        {
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        }
    }
}
