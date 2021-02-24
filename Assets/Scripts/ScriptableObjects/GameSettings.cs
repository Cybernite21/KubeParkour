using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Info:")]
    [Space(5)]
    public bool useTheseSettings;
    [Space(10)]
    [Header("Player:")]
    public int airInTank = 25;
    public int health = 100;
    public Color[] scaleColors = new Color[2];
    public float scaleSpeed = 2f;
    public float jumpPower = 2f;
    public Vector2 moveSpeeds = new Vector2(5, 10);
    public float rotSpeed = 2f;
    public float turnSensitivity = .9f;
    public Vector3[] scaleClamp = new Vector3[2] { new Vector3(.25f, .25f, .25f), new Vector3(4, 4, 4) };
    public Vector3 rotationClamp = new Vector3(20, 360, 20);
    public Vector2 rbMassMinMax;
    [Space(10)]
    [Header("Damage:")]
    [Space(5)]
    [Header("Water:")] 
    public int damage = 10;
    public int damageRateInSeconds = 2;
    [Space(5)]
    [Header("Behavior:")]
    public float destinationHeight = 5;
    public float floodSpeed = .5f;
    public float delayToStartFlood = 5;
    [Space(5)]
    [Header("Screen Ripples:")]
    public float maxScreenRippleStrength = .005f;
    public float screenRippleFadeSpeed = 5;
    public Material screenRippleMat;
    public ForwardRendererData forwardRendererData;
}
