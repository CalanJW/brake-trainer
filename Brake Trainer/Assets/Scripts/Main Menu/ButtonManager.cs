using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ControlsButtonPressed()
    {
        SceneManager.LoadScene("Controls");
    }

    public void TrainerButtonPressed()
    {
        SceneManager.LoadScene("Trainer");
    }
}
