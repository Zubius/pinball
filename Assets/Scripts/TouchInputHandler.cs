using System;
using UnityEngine;

internal class TouchInputHandler : MonoBehaviour, IInputSource
{
    public event Action<Side, float> OnGameInput;
}
