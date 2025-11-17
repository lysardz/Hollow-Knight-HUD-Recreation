using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Image fadeIn;

    public static UnityEvent<int,bool> updateHealth = new UnityEvent<int,bool>();
    public static UnityEvent<float,float,float> updateSoul = new UnityEvent<float,float,float>();
    public static UnityEvent<float> updateSoulAnimation = new UnityEvent<float>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fadeIn.DOFade(0, 1);
    }


}
