using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostMeterScript : MonoBehaviour
{
    public int maxMeterVal = 100;
    public int currentVal = 50;

    public Slider boostMeterSlider;

    public WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    private void Awake() {
        boostMeterSlider = GetComponent<Slider>();
        Debug.Log(boostMeterSlider.value);
        boostMeterSlider.value = currentVal; 
        Debug.Log(boostMeterSlider.value);
    }


    public void Update() {
        // boostMeterSlider.value += maxMeterVal/100 * Time.deltaTime;
        boostMeterSlider.value += 3 * Time.deltaTime;
        boostMeterSlider.value = Mathf.Clamp(boostMeterSlider.value, 0, maxMeterVal);
    }
    
    // public IEnumerator regenBoostMeter() {
    //     Debug.Log("in regenboost"+ boostMeterSlider.value);

    //     yield return new WaitForSeconds(2); 

    //     while(boostMeterSlider.value < maxMeterVal) {
    //         boostMeterSlider.value += maxMeterVal/100;
    //         // boostMeterSlider.value = currentVal;
    //         Debug.Log(boostMeterSlider.value);
    //         yield return regenTick; 
            
    //     }
    // }



}
