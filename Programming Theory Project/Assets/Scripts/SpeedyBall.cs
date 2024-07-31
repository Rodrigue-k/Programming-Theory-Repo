using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedyBall : Ball
{
    protected override void Start()
    {
        base.Start();
        Speed = 10f;
        JumpForce = 0;
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Jump()
    {
        base.Jump();
    }


}
