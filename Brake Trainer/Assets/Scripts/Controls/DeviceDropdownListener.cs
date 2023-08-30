using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceDropdownListener : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Dropdown axisDropdown;
    TMP_Dropdown dropdown;

    public void OnDropdownValueChanged(){
        dropdown = this.GetComponent<TMP_Dropdown>();
        axisDropdown.options.Clear();
        string deviceName = dropdown.options[dropdown.value].text;
        InputDevice device = InputSystem.GetDevice(deviceName);
        foreach (InputControl ic in device.allControls){
            Debug.Log(ic.name);
            Debug.Log(ic.shortDisplayName);
            axisDropdown.options.Add(new TMP_Dropdown.OptionData(ic.ToString()));
        }
    }
}
