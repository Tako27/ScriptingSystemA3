using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void UpdateExpBar(int currentExp, int limit)
    {
        slider.maxValue = limit;
        slider.value = currentExp;
    }
}
