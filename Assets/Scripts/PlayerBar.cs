using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    health,
    mana
}

public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public BarType type;

    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();
        this.SetMaxValue();
    }

    private void SetMaxValue()
    {
        switch (this.type)
        {
            case BarType.health:
                this.slider.maxValue = PlayerController.sharedInstance.GetMaxHealth();
                this.slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.mana:
                this.slider.maxValue = PlayerController.sharedInstance.GetMaxMana();
                this.slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.type)
        {
            case BarType.health:
                this.slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.mana:
                this.slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}
