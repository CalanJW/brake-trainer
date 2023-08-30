using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.IO.FileStream;

public class Done : MonoBehaviour
{

    [SerializeField] TMP_Dropdown brakeAxis;
    [SerializeField] TMP_Dropdown throttleAxis;
    [SerializeField] TMP_Dropdown steerAxis;
    [SerializeField] TMP_Dropdown gearUpAxis;
    [SerializeField] TMP_Dropdown gearDownAxis;

    [SerializeField] TMP_Text brakeMax;
    [SerializeField] TMP_Text brakeMin;
    [SerializeField] TMP_Text throttleMax;
    [SerializeField] TMP_Text throttleMin;
    [SerializeField] TMP_Text steerMax;
    [SerializeField] TMP_Text steerMin;
    [SerializeField] TMP_Text gearUpMax;
    [SerializeField] TMP_Text gearUpMin;
    [SerializeField] TMP_Text gearDownMax;
    [SerializeField] TMP_Text gearDownMin;
    [SerializeField] Toggle brakeInvert;
    [SerializeField] Toggle throttleInvert;
    [SerializeField] Toggle steerInvert;
    [SerializeField] Toggle gearUpInvert;
    [SerializeField] Toggle gearDownInvert;

    [SerializeField] string controlsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    // Start is called before the first frame update
    public void OnDoneButtonPressed(){
        string toWrite = "";

        toWrite += "Brake:"+brakeAxis.options[brakeAxis.value].text+","+brakeMax.text+","+brakeMin.text+","+brakeInvert.isOn+"\n";
        toWrite += "Throttle:"+throttleAxis.options[throttleAxis.value].text+","+throttleMax.text+","+throttleMin.text+","+throttleInvert.isOn+"\n";
        toWrite += "Steer:"+steerAxis.options[steerAxis.value].text+","+steerMax.text+","+steerMin.text+","+steerInvert.isOn+"\n";
        toWrite += "Gear Up:"+gearUpAxis.options[gearUpAxis.value].text+","+gearUpMax.text+","+gearUpMin.text+","+gearUpInvert.isOn+"\n";
        toWrite += "Gear Down:"+gearDownAxis.options[gearDownAxis.value].text+","+gearDownMax.text+","+gearDownMin.text+","+gearDownInvert.isOn+"\n";

        WriteToFile(controlsFilePath+"\\Controls.txt",toWrite);
    }

    void WriteToFile(string path, string datastring){
        using (FileStream fs = File.Create(path)) {
        
        // writing data in string
            byte[] info = new UTF8Encoding(true).GetBytes(datastring);
            fs.Write(info, 0, info.Length);

        }
    }
}
