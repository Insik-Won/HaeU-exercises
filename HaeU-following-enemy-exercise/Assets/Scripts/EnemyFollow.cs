using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class EnemyFollow : MonoBehaviour
{

    public Transform position1;
    public Transform position2;
    public Transform player;

    public float nextWaypointDistance = 3f;

    IGameManager gameManager;
    Rigidbody rb;

    Path path;
    int currentWaypoint = 0;

    bool catchedPlayer = false;
    bool reachedTarget = false;

    Seeker seeker;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        var gameManagerObject = GameObject.FindGameObjectWithTag("GameManager") ?? throw new NullReferenceException("cannot find the GameObject tagged 'GameManager'");

        gameManager = gameManagerObject.GetComponent<IGameManager>() ?? throw new NullReferenceException("cannot find the Component whose type is 'IGameManager'");
        rb = GetComponent<Rigidbody>();
        seeker = GetComponent<Seeker>();

        UpdateTarget();

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return;

        if (UpdateTarget())
        {
            Debug.Log("Change Target");
            UpdatePath();
            return;
        }
        
        if (currentWaypoint >= path.vectorPath.Count)
        {
            Debug.Log("Reach End of Path");
            return;
        }

        Vector3 direction = (path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 deltaPostion = direction * gameManager.EnemySpeed * Time.deltaTime;

        deltaPostion.y = 0;

        rb.MovePosition(rb.position + deltaPostion);

        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    bool UpdateTarget()
    {

        if (Vector3.Distance(rb.position, player.position) < gameManager.SensingDistance)
        {
            if (target == player) return false;

            target = player;
            return true;
        }
        else
        {
            if (target == player) target = null;
        }

        if (target == null)
        {
            if (Vector3.Distance(rb.position, position1.position) > Vector3.Distance(rb.position, position2.position))
                target = position1;            
            else
                target = position2;            
        }

        if (Vector3.Distance(rb.position, target.position) < nextWaypointDistance)
        {
            if (reachedTarget) return false;

            if (target == player) 
                catchedPlayer = true;
            else if (target == position1)
                target = position2;
            else
                target = position1;

            reachedTarget = true;

            return true;
        }
        else
        {
            reachedTarget = false;
        }

        return false;
    }
}
