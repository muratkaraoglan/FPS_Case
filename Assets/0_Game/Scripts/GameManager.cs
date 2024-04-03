using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public FPSController FPSController { get; private set; }
    [field: SerializeField,Tooltip("Playable Area Border")] public Vector2 AreaBound { get; private set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Time.timeScale = 1f;
        FPSController.OnPlayerDie += FPSController_OnPlayerDie;
    }

    private void FPSController_OnPlayerDie()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
