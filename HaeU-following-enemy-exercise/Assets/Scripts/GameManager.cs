using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{

    [SerializeField]
    private float _PlayerSpeed = 7.5f;

    [SerializeField]
    private float _EnemySpeed = 7.5f;

    [SerializeField]
    private float _SensingDistance = 5f;

    public float PlayerSpeed
    {
        get { return _PlayerSpeed; }
        private set { _PlayerSpeed = value; }
    }

    public float EnemySpeed
    {
        get { return _EnemySpeed; }
        set { _EnemySpeed = value; }
    }

    public float SensingDistance
    {
        get => _SensingDistance;
        set => _SensingDistance = value;
    }
}
