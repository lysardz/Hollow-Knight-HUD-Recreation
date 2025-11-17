using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Mask : MonoBehaviour
{
    public Image idleMask;
    public VisualEffect maskBreak;
    [SerializeField] VisualEffect maskIncrease;
    [SerializeField] VisualEffect maskShakePrt;
    Tween maskShake;
    bool isShaking = false;
    Tween maskTween;

    private void Start()
    {


        

    }
    public void ChangeMask(bool _enable, int _health)
    {
        
        if (_enable)
        {
            
            idleMask.enabled = true;
            idleMask.transform.localScale = new Vector3(0, 0.6f, 1);
            if(maskShake == null )
            {
                maskTween = idleMask.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce).SetAutoKill(false);
            }
            else
            {
                maskTween.Restart();
            }
            
            
            maskIncrease.Play();


            Debug.Log("Mask Changed");
        }
        else
        {

            maskBreak.Play();
            idleMask.enabled = false;
            
            Debug.Log("Mask Changed");
        }
    }

    void ResetPosition()
    {
        idleMask.rectTransform.localPosition = Vector3.zero;
    }

    public void ShakeMask()
    {
        isShaking = true;
        TweenCallback reset = ResetPosition;
        maskShakePrt.Play();
        maskShake.Kill();
        idleMask.rectTransform.localPosition = Vector3.zero;

        maskShake = idleMask.rectTransform.DOShakePosition(3, 5,10,150,true,false).SetLoops(-1, LoopType.Incremental);
      

    }
    public void StopShaking()
    {
        if (isShaking)
        {
            maskShakePrt.Stop();
            isShaking = false;
            maskShake.Kill();
            idleMask.rectTransform.localPosition = Vector3.zero;
        }
       
    }
}
