using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject textSource;

    public GameObject GameUI;
    public GameObject PauseUI;

    private enum CursorStates
    {
        Aiming,
        UI
    }

    private static CursorStates cursorState = CursorStates.Aiming;

    private void Awake()
    {
        Pause.OnPauseToggle += ToggleGameUI;
        Pause.OnPauseToggle += UpdateCursorState;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void ToggleGameUI()
    {
        if (!GameUI)
            return;
        if (!PauseUI)
            return;

        GameUI.SetActive(!Pause.paused);
        PauseUI.SetActive(Pause.paused);
    }

    private void UpdateCursorState()
    {
        switch (Pause.paused)
        {
            case false:
                {
                    cursorState = CursorStates.Aiming;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                }

            case true:
                {
                    cursorState = CursorStates.UI;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                }
        }
    }
}
