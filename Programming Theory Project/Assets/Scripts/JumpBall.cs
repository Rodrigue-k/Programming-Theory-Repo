using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBall : Ball
{
    protected override void Start()
    {
        base.Start();
        Speed = 5f;
        JumpForce = 10f;
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
