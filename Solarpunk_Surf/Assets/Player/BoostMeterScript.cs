using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostMeterScript : MonoBehaviour
{
    public int maxMeterVal = 100;
    public int currentVal = 100;

    public Slider boostMeterSlider;
    //public Image boostSun;

    [SerializeField]
    private Image boostMeterFill;
    private Color baseColor;

    [SerializeField]
    private Image overheatedImg;

    [SerializeField]
    private Color overheatedColor;

    public WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    private void Awake() {
        boostMeterSlider = GetComponent<Slider>();
        //Debug.Log(boostMeterSlider.value);
        boostMeterSlider.value = 100;
        //boostSun.fillAmount = 1;
        //Debug.Log(boostMeterSlider.value);
        baseColor = boostMeterFill.color;
        overheatedImg.enabled = false;
    }

    public void regenBoostMeter() {
        boostMeterSlider.value += 20 * Time.deltaTime;
        //boostSun.fillAmount += .1f * Time.deltaTime;
        boostMeterSlider.value = Mathf.Clamp(boostMeterSlider.value, 0, maxMeterVal);
        //boostSun.fillAmount = Mathf.Clamp(boostMeterSlider.value/100, 0, maxMeterVal/100);
    }

    public void showOverheated(bool overheated)
    {
        if (overheated)
        {
            boostMeterFill.color = Color.red;
            overheatedImg.enabled = true;
        } else
        {
            boostMeterFill.color = baseColor;
            overheatedImg.enabled = false;
        }
    }


}
