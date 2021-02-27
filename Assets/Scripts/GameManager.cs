using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Gate.wonLevel += nextLevel;
        PlayerController.playerDeath += playerDied;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        Gate.wonLevel -= nextLevel;
        //next level code
        print("Won");
    }

    public void playerDied()
    {
        PlayerController.playerDeath -= playerDied;
        //code when player dies
        print("Death");
    }
}
