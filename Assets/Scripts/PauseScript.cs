using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas gameCanvas;

    private void Start()
    {
        pauseCanvas.enabled = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
        gameCanvas.enabled = false;
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        gameCanvas.enabled = true;
    }
}
