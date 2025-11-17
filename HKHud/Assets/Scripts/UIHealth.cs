using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{

    int currentHealth;
    private void Start()
    {
        UIManager.updateHealth.AddListener(updateHealthUI);
    }

    private void OnDisable()
    {
        UIManager.updateHealth.RemoveListener(updateHealthUI);
    }

    private void updateHealthUI(int _currentHealth,bool _increase)
    {
        
        int health = 0;
       currentHealth = _currentHealth;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Mask>().idleMask.enabled == true)
            {
                health++;
            }

        }
        
        
        
        if (!_increase)
        {
           
            ChangeMask(_increase,_currentHealth);
            
        }
        else
        {
            ChangeMask(_increase, health);
            
        }


    }

    private void ChangeMask(bool _increaseMask, int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            
             if(i == 0)
            {
                if (i == 0 && currentHealth == 1)
                {
                    transform.GetChild(i).GetComponent<Mask>().ShakeMask();
                }
                else
                {
                    transform.GetChild(i).GetComponent<Mask>().StopShaking();
                }
                
            }
            
            if (i == _index)
            {
                
                if (_increaseMask)
                {
                    
                    transform.GetChild(_index).GetComponent<Mask>().ChangeMask(true,currentHealth);

                }
                else
                {
                        
                        transform.GetChild(_index ).GetComponent<Mask>().ChangeMask(false,currentHealth);
                    
                    
                }
                break;
            }

        }
    }
}
