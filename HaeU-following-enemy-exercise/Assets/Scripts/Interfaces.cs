using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    float PlayerSpeed { get; }
    float EnemySpeed { get; }
    float SensingDistance { get; }
}
