using System;
using HeroComponents;
using StaticData;
using UnityEngine;
using Zenject;

public class Hero : MonoBehaviour
{
    [SerializeField] public GameObject Sickle;
    
    [HideInInspector] public VariableJoystick Input;
    [HideInInspector] public float MovementSpeed = 10f;

    [HideInInspector] public Transform SicklePosition;

    [SerializeField] public Transform BackpackTransform;
    [HideInInspector] public int MaxLoot;
    [HideInInspector] public float LootPosOffset;

    [HideInInspector] public ColliderTracker CollusionTracker;
    private IStaticDataService _staticData;

    [Inject]
    public void Construct (VariableJoystick joystick, IStaticDataService staticData)
    {
        Input = joystick;
        _staticData = staticData;
    }

    public void Awake()
    {
        SicklePosition = GetComponentInChildren<SickleMarker>().transform;
        //BackpackTransform = GetComponentInChildren<BackpackMarker>().transform;
        CollusionTracker = GetComponentInChildren<ColliderTracker>();
    }

    public void Start()
    {
        MovementSpeed = _staticData.ForHero().MovementSpeed;

        MaxLoot = _staticData.ForHero().MaxLoot;
        LootPosOffset = _staticData.ForHero().LootPosOffset;
    }
}