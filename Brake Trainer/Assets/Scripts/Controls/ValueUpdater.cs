using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ValueUpdater : MonoBehaviour
{

    [SerializeField] Slider valueSlider;
    [SerializeField] TMP_Text valueText;
    [SerializeField] TMP_Dropdown axisDropdown;
    [SerializeField] TMP_Dropdown deviceDropdown;

    [SerializeField] TMP_Text minText;
    [SerializeField] TMP_Text maxText;

    [SerializeField] Toggle invert;

    InputAction input = new InputAction();

    float maxValue = Mathf.NegativeInfinity;
    float minValue = Mathf.Infinity;

    bool run = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (run){
        //try{
            float value = input.ReadValue<float>();
            //Debug.Log(value);
            float adjValue = (value - minValue) / (maxValue - minValue);
            if (invert.isOn){
                adjValue = (value - maxValue) / (minValue - maxValue);
            }
            Debug.Log(maxValue +","+minValue);
            if (maxValue != minValue && maxValue != Mathf.NegativeInfinity && minValue != Mathf.Infinity)
                valueSlider.value = adjValue;
                //Debug.Log((value - minValue) / (maxValue - minValue));
            if ((""+value).Length > 5){
                valueText.text = (""+ value).Substring(0,5);
            } else {
                valueText.text = (""+ value);
            }
            if (value > maxValue){
                maxValue = value;
                if ((""+maxValue).Length > 5){
                    maxText.text = (""+ maxValue).Substring(0,5);
                } else {
                    maxText.text = (""+ maxValue);
                }
            }
            if (value < minValue){
                minValue = value;
                if ((""+minValue).Length > 5){
                    minText.text = (""+ minValue).Substring(0,5);
                } else {
                    minText.text = (""+ minValue);
                }
            }
            //Debug.Log(input);
           
        //} catch {
        //}
        }
    }

    public void NewAxisSelected(){
        run = false;
        maxValue = Mathf.NegativeInfinity;
        minValue = Mathf.Infinity;
        string axisName = axisDropdown.options[axisDropdown.value].text;
        string deviceName = deviceDropdown.options[deviceDropdown.value].text;
        InputDevice device = InputSystem.GetDevice(deviceName);
        InputControl ic = null;
        foreach (InputControl control in device.children){
            Debug.Log(control.ToString());
            if (control.ToString().Equals(axisName)){
                ic = control;
                break;
            }
            foreach (InputControl childControl in control.children){
                if (childControl.ToString().Equals(axisName)){
                    ic = childControl;
                }
            }
            
        }
        if (!axisName.Equals("")){
            Debug.Log("Axis: " + axisName);
            Debug.Log("Path: " + ic.path);
            input.Disable();
            input = new InputAction();
            input.AddBinding(ic.path);
            input.Enable();
        }
        run = true;
    }
}
