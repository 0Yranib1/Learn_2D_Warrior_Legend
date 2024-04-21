using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healImage;
    public Image healDelayIimage;
    public Image powerImage;

    private void Update()
    {
        if (healDelayIimage.fillAmount > healImage.fillAmount)
        {
            healDelayIimage.fillAmount -= Time.deltaTime;
        }
    }

    public void OnHealthChange(float persentage)
    {
        healImage.fillAmount = persentage;
    }
}
