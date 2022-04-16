using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerNavigator : Navigator
{
    Transform _player;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DesiredX = (_player.position.x - transform.position.x);
        DesiredY = (_player.position.y - transform.position.y);
    }
}
