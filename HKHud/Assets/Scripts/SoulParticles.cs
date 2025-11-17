using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SoulParticles : MonoBehaviour
{
    public RectTransform uiElement;
    public VisualEffect vfxGraphObject;
    [SerializeField] AudioSource particleSound;
    [SerializeField] AudioClip sound;
    public void Spawn()
    {
        Vector3 worldCenter = GetRectTransformWorldCenter(uiElement);
        float worldWidth = uiElement.rect.width * uiElement.lossyScale.x;
        float worldHeight = uiElement.rect.height * uiElement.lossyScale.y;
        Vector3 worldScale = new Vector3 (worldWidth,worldHeight,0);
        

        vfxGraphObject.SetVector3("spawnPosition", worldCenter);
        vfxGraphObject.SetVector3("worldScale", worldScale);
        particleSound.PlayOneShot(sound);

    }
    Vector3 GetRectTransformWorldCenter(RectTransform rect)
    {
        Vector3 worldPosition = rect.position;

       

       
        worldPosition = rect.TransformPoint(rect.rect.center);

        
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }

    
}
