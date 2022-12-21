using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityCode.CodeLibrary.Utilities;

public class ShooterEventSystem : UnitySingleton<ShooterEventSystem>
{
    public Action onEnemyKilled;
    public Action onGameWon;
    public Action onGameLost;
    public Action onNextLevel;
    public Action onGameStarted;
    public Action onAttackSpeedBuff;
}
