using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandler : MonoBehaviour
{
    enum State { FlyingUp, FlyingDown, Stabbing, Leaving };
    State state;

    public GameObject leftKnife;
    public GameObject rightKnife;



    void Start()
    {
        LeanTween.delayedCall(1, () =>
        {
            SwitchToState(State.FlyingUp);
        });
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

    void SwitchToState(State nextState)
    {
        state = nextState;
        switch (state)
        {
            case State.FlyingUp:
                LeanTween.move(leftKnife, new Vector2(leftKnife.transform.position.x, globalData.instance.screenTopEdge), 1).setEase(LeanTweenType.easeOutCubic);
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