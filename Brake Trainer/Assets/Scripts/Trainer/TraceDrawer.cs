using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.ParticleSystem;
using UnityEngine.Rendering;
using System.Text;
using System.Threading;

public class TraceDrawer : MonoBehaviour
{
    InputAction input = new InputAction();
    float minValue = 0;
    float maxValue = 0;
    bool invert = false;
    [SerializeField] GameObject traceObject;
    List<Vector3> tracePoints = new List<Vector3>();
    [SerializeField] string traceName;
    [SerializeField] float topY = 50;
    [SerializeField] float height = 50;
    float xPerFrame = 3f;
    [SerializeField] float midX = 410f;
    LineRenderer lineRenderer;

    string controlsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Driver Training Tool" + Path.DirectorySeparatorChar + "Controls.txt";


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = traceObject.GetComponent<LineRenderer>();
        string controlsFileContent = ReadFile(controlsFilePath);
        string brakeAxis = controlsFileContent.Split(traceName+":")[1].Split(",")[0];
        minValue = float.Parse(controlsFileContent.Split(traceName + ":")[1].Split(",")[2]);
        maxValue = float.Parse(controlsFileContent.Split(traceName + ":")[1].Split(",")[1]);
        invert = controlsFileContent.Split(traceName + ":")[1].Split(",")[3] == "True";
        InputControl ic = null;
        foreach (InputDevice device in InputSystem.devices)
        {
            foreach (InputControl control in device.children)
            {
                Debug.Log(control.ToString());
                if (control.ToString().Equals(brakeAxis))
                {
                    ic = control;
                    break;
                }
                foreach (InputControl childControl in control.children)
                {
                    if (childControl.ToString().Equals(brakeAxis))
                    {
                        ic = childControl;
                    }
                }

            }
        }
        input.Disable();
        input = new InputAction();
        input.AddBinding(ic.path);
        input.Enable();

    }

    float frameCount = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Vector3.zero);
        float value = input.ReadValue<float>();
        float adjValue = (value - minValue) / (maxValue - minValue);
        if (invert)
        {
            adjValue = (value - maxValue) / (minValue - maxValue);
        }
        adjValue *= height;
        AddLinePosition(lineRenderer,new Vector3(midX, adjValue + topY, 0f),Color.red);
        transform.position.Set(transform.position.x, transform.position.y, transform.position.z);
        ShiftLine(lineRenderer, -xPerFrame);
        frameCount += 1f;
    }

    string ReadFile(string path)
    {
        return File.ReadAllText(path, Encoding.UTF8);
    }

    void ShiftLine(LineRenderer lr, float xValue)
    {
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 oldPos = lr.GetPosition(i);
            Vector3 newPos = new Vector3(oldPos.x + xValue, oldPos.y, oldPos.z);
            lr.SetPosition(i, newPos);
        }
    }

    void AddLinePosition(LineRenderer lr, Vector3 position, Color color)
    {
        lr.positionCount += 1;
        lr.SetPosition(lr.positionCount - 1, position);
        lr.startColor = color;
        lr.endColor = color;
    }
}
