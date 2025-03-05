using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;

    public static Action OnPauseToggle;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }
    public static void TogglePause()
    {
        if (!paused)
        {
            Time.timeScale = 0f;
            paused = true;
            OnPauseToggle.Invoke();
        }
        else if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
            OnPauseToggle.Invoke();
        }
    }
}