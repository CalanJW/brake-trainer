using System.Collections;
using System.Collections.Generic;
using Microsoft.Cci;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ListDevices : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<TMP_Dropdown> dropdowns = new List<TMP_Dropdown>();

    void Start()
    {
        Debug.Log("Start");
        foreach (InputDevice device in InputSystem.devices){
            Debug.Log(device.name);
            foreach (InputControl ic in device.allControls){
                foreach (InputControl icc in ic.children){
                    Debug.Log(ic.name+"|"+icc.name);
                }
            }
        }
        foreach (TMP_Dropdown dropdown in dropdowns){
            foreach (InputDevice device in InputSystem.devices){
                dropdown.options.Add(new TMP_Dropdown.OptionData(device.name));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
