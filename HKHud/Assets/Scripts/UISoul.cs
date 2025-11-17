using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class UISoul : MonoBehaviour
{
    [SerializeField] SoulParticles soulParticles;
    [SerializeField] Image eyes;
    [SerializeField] Material soulMaterial;
    bool isChanging = false;
    [SerializeField] float newFill;
    [SerializeField] float currentFill;
    [SerializeField] VisualEffect bubbles;
    [SerializeField] VisualEffect soulIncrease;
    [SerializeField] float waveLength = 1;
    [SerializeField] float glowStrenght = 0;
    [SerializeField] float eyeEdgeStrenght = 1;
    [SerializeField] float glowSpeed = 0.2f;
    [SerializeField] float glowPulsingSpeed = 0.3f;
    [SerializeField] float waveLenghtSpeed = 0.1f;
    [Range(0, 1)][SerializeField] float eyeAppearThreshold = 0.3f;
    [Range(0, 1)]
    [SerializeField] float transparencyThreshold;
    Sequence waveAnimation;
    Sequence glowAnimation;
    Sequence pulseAnimation;
    Sequence eyeAnimation;
    float maxSoul;
    float currentSoul;
    bool eyesOpen = true;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip waveUp;
    [SerializeField] AudioClip waveDown;
    [SerializeField] AudioClip waveFull;


    bool isPulsing = false;
    private void Start()
    {
        UIManager.updateSoul.AddListener(UpdateSoulFill);
        UIManager.updateSoulAnimation.AddListener(LenghtenWaves);
        UIManager.updateSoulAnimation.AddListener(GlowPulse);
        currentFill = soulMaterial.GetFloat("_fillAmount");
        soulMaterial.SetFloat("_fillAmount", 1);
        waveAnimation = DOTween.Sequence();
        glowAnimation = DOTween.Sequence();
        pulseAnimation = DOTween.Sequence();
        eyeAnimation = DOTween.Sequence();
        GlowPulsing();
       
    }



    private void OnDisable()
    {
        UIManager.updateSoul.RemoveListener(UpdateSoulFill);
    }

    private void FixedUpdate()
    {

        UpdateMaterial("_waveMultiplier", waveLength, soulMaterial);
        UpdateMaterial("_glowMultiplier", glowStrenght, soulMaterial);
        UpdateMaterial("_edgeMultiplier", eyeEdgeStrenght, eyes.material);


        if (isChanging)
        {

            DOTween.To(() => currentFill, x => currentFill = x, newFill, 0.2f);
            soulMaterial.SetFloat("_fillAmount", currentFill);
            if (soulMaterial.GetFloat("_fillAmount") == newFill)
            {
                isChanging = false;
                soulMaterial.SetFloat("_waveMultiplier", 1);
            }
        }


    }

    void GlowPulsing()
    {
        pulseAnimation.Kill();
        glowAnimation.Kill();
        pulseAnimation = DOTween.Sequence();
        pulseAnimation.SetLoops(-1);
        float start = 1;
        float end = 0;
        glowStrenght = 1;
        eyeEdgeStrenght = 0;
        pulseAnimation.Append(DOTween.To(() => glowStrenght, x => glowStrenght = x, end, glowPulsingSpeed));
        pulseAnimation.Join(DOTween.To(() => eyeEdgeStrenght, x => eyeEdgeStrenght = x, start, glowPulsingSpeed));
        pulseAnimation.Append(DOTween.To(() => glowStrenght, x => glowStrenght = x, start, glowPulsingSpeed));
        pulseAnimation.Join(DOTween.To(() => eyeEdgeStrenght, x => eyeEdgeStrenght = x, end, glowPulsingSpeed));

    }
    void UpdateMaterial(string _name, float _value, Material _material)
    {
        if (_material.GetFloat(_name) != _value)
        {
            _material.SetFloat(_name, _value);
        }
    }

    private void UpdateSoulFill(float _currentSoul, float _maxSoul, float _soulModifier)
    {

        if (_currentSoul == maxSoul)
        {
            audioSource.PlayOneShot(waveFull);
        }
        currentSoul = _currentSoul;
        maxSoul = _maxSoul;
        Debug.Log($"Current soul {_currentSoul}/{_maxSoul}");
        if (_currentSoul >= eyeAppearThreshold)
        {
            if (eyes.transform.localScale == new Vector3(1, 0, 1))
            {
                eyeAnimation.Kill();

                eyes.transform.localScale = new Vector3(1, 0, 1);
                eyeAnimation.Append(eyes.transform.DOScale(new Vector3(1, 1, 1), 0.13f).SetEase(Ease.OutSine));
            }
            else { eyeAnimation.Kill(); }


        }
        else
        {
            if (eyes.transform.localScale == new Vector3(1, 1, 1))
            {
                eyeAnimation.Kill();
                eyes.transform.localScale = new Vector3(1, 1, 1);
                eyeAnimation.Append(eyes.transform.DOScale(new Vector3(1, 0, 1), 0.15f).SetEase(Ease.OutElastic));
            }
            else
            {
                eyeAnimation.Kill();
            }


        }

        if (_currentSoul < _soulModifier && soulMaterial.GetFloat("_fillAlpha") != 0.2f)
        {
            soulMaterial.SetFloat("_fillAlpha", 0.2f);
        }
        if (_currentSoul >= _soulModifier && soulMaterial.GetFloat("_fillAlpha") == 0.2f)
        {

            soulParticles.Spawn();
            soulIncrease.Play();
            soulMaterial.SetFloat("_fillAlpha", 1);
        }

        newFill = _currentSoul / _maxSoul;
        currentFill = soulMaterial.GetFloat("_fillAmount");
        isChanging = true;
        if (_currentSoul == _maxSoul)
        {
            Debug.Log("StartGlowing");
            GlowPulsing();
        }
        else
        {
            Debug.Log("StopGlowing");
            glowAnimation.Kill();
            pulseAnimation.Kill();
            eyeEdgeStrenght = 1;
            glowStrenght = 0;
            UpdateMaterial("_edgeMultiplier", eyeEdgeStrenght, eyes.material);
            UpdateMaterial("_glowMultiplier", glowStrenght, soulMaterial);
        }

    }

    private void GlowPulse(float _waveAffector)
    {
        if (_waveAffector > 1)
        {
            if (currentSoul != maxSoul)
            {
                glowAnimation.Kill();
                glowAnimation = DOTween.Sequence();
                float start = 1;
                float end = 0;
                glowStrenght = 0;
                eyeEdgeStrenght = 1;
                glowAnimation.Append(DOTween.To(() => glowStrenght, x => glowStrenght = x, start, glowSpeed / 2));
                glowAnimation.Join(DOTween.To(() => eyeEdgeStrenght, x => eyeEdgeStrenght = x, end, glowSpeed / 2));
                glowAnimation.Append(DOTween.To(() => glowStrenght, x => glowStrenght = x, end, glowSpeed));
                glowAnimation.Join(DOTween.To(() => eyeEdgeStrenght, x => eyeEdgeStrenght = x, start, glowSpeed));
            }


        }
    }

    private void LenghtenWaves(float _waveAffector)
    {
        Debug.Log("wavelenght Change");
        waveAnimation.Kill();
        waveAnimation = DOTween.Sequence();
        float start = 1;
        waveLength = 1;


        if (_waveAffector < 1 && currentSoul != 0)
        {
            bubbles.Play();

        }
        if (_waveAffector > 1)
        {

            audioSource.PlayOneShot(waveDown);
        }
        else
        {
            audioSource.PlayOneShot(waveUp);
        }

        waveAnimation.Append(DOTween.To(() => waveLength, x => waveLength = x, _waveAffector, waveLenghtSpeed / 2));
        waveAnimation.Append(DOTween.To(() => waveLength, x => waveLength = x, start, waveLenghtSpeed));


    }


}
