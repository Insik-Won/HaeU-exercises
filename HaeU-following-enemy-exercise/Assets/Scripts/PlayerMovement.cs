using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    IGameManager gameManager;

    Rigidbody rb; 
    

    // Start is called before the first frame update
    void Start()
    {
        var gameManagerObject = GameObject.FindGameObjectWithTag("GameManager")?? throw new NullReferenceException("cannot find the GameObject tagged 'GameManager'");

        gameManager = gameManagerObject.GetComponent<IGameManager>() ?? throw new NullReferenceException("cannot find the Component whose type is 'IGameManager'");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxisRaw("Horizontal") * gameManager.PlayerSpeed * Time.deltaTime;
        var dz = Input.GetAxisRaw("Vertical") * gameManager.PlayerSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + new Vector3(dx, 0, dz));
    }
}
