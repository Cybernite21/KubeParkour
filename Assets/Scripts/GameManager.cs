using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int frameRate = 60;

    public GameSettings gameSettings;

    //Health and AirInTank Sliders
    public Slider AirInTankSlider;
    public Slider HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
