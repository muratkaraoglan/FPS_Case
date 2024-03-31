using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(-9)]
public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public FPSController FPSController;
    [field: SerializeField] public Vector2 AreaBound;
}
