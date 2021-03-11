using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController plr;
    public Water water;

    public int frameRate = 60;

    public float restartLevelDelay = 2f;

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

        plr = FindObjectOfType<PlayerController>();
        water = FindObjectOfType<Water>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel()
    {
        //Gate.wonLevel -= nextLevel;
        //next level code
        print("Won");
        if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1) != null)
        {
            Gate.wonLevel -= nextLevel;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void playerDied()
    {
        PlayerController.playerDeath -= playerDied;
        //code when player dies
        print("Death");
        plr.enabled = false;
        Invoke("restartLevel", restartLevelDelay);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
