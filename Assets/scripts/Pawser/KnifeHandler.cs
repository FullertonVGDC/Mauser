using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandler : MonoBehaviour
{
    enum State { FlyingUp, FlyingDown, Stabbing, Leaving };
    State state;

    void Start()
    {
        state = State.FlyingUp;
    }

    void Update()
    {
        switch (state)
        {
            case State.FlyingUp:
                break;

            case State.FlyingDown:
                break;

            case State.Stabbing:
                break;

            case State.Leaving:
                break;
        }
    }
}