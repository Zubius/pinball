using System;
using System.Collections;
using UnityEngine;

internal class Flipper : MonoBehaviour
{
    [SerializeField] private float velocity = 500;
    [SerializeField] private HingeJoint cachedHingeJoint;
    [SerializeField] private Side side;

    private WaitUntil _limitReachedWaiter;

    internal void Up()
    {
        MoveFlipper(VelocityUp);
    }

    internal void Down()
    {
        MoveFlipper(VelocityDown);
    }

    private void MoveFlipper(float velocity)
    {
        var jointMotor = cachedHingeJoint.motor;
        jointMotor.targetVelocity = velocity;
        cachedHingeJoint.motor = jointMotor;
    }

    private float VelocityUp => velocity * (side == Side.Left ? -1 : 1);
    private float VelocityDown => velocity * (side == Side.Left ? 1 : -1);
}
