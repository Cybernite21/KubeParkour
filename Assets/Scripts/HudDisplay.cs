using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudDisplay : MonoBehaviour
{
    public GameManager gManager;
    public PlayerController plr;

    //Health and AirInTank Sliders
    public Slider AirInTankSlider;
    public Slider HealthSlider;

    [SerializeField] private Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;

    void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();

        AirInTankSlider = gManager.AirInTankSlider;
        HealthSlider = gManager.HealthSlider;

        AirInTankSlider.maxValue = gManager.gameSettings.airInTank;
        HealthSlider.maxValue = gManager.gameSettings.health;

        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }

        if(AirInTankSlider.value != plr.AirInTank)
        {
            AirInTankSlider.value = plr.AirInTank;
        }
        if(HealthSlider.value != plr.Health)
        {
            HealthSlider.value = plr.Health;
        }
    }
}
