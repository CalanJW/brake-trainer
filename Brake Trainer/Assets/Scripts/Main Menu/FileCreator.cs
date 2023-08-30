using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileCreator : MonoBehaviour
{

    string mainDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Driver Training Tool";

    // Start is called before the first frame update
    void Start()
    {
        // If directory does not exist, create it
        if (!Directory.Exists(mainDirPath))
        {
            Directory.CreateDirectory(mainDirPath);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
