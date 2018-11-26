using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandler : MonoBehaviour
{
    enum State { FlyingUp, FlyingDown, Stabbing, Leaving };
    State state;

    public Vector2 leftKnifeStartPos;
    public Vector2 rightKnifeStartPos;
    public float startStabPosY;
    public float endStabPosY;

    float knifeWidth;
    float knifeHeight;
    [HideInInspector]
    public bool finishedPanning;

    public GameObject leftKnife;
    public GameObject rightKnife;

    CameraHandler cameraHandler;
    [HideInInspector]
    public Pawser pawser;



    void Start()
    {
        leftKnife.transform.position = leftKnifeStartPos;
        rightKnife.transform.position = rightKnifeStartPos;

        knifeWidth = leftKnife.GetComponent<SpriteRenderer>().sprite.bounds.size.x * leftKnife.transform.localScale.x;
        knifeHeight = leftKnife.GetComponent<SpriteRenderer>().sprite.bounds.size.y * rightKnife.transform.localScale.x;

        cameraHandler = Camera.main.GetComponent<CameraHandler>();
        SwitchToState(State.FlyingUp);
    }

    void SwitchToState(State nextState)
    {
        state = nextState;
        switch (state)
        {
            case State.FlyingUp:
                LeanTween.moveY(leftKnife, cameraHandler.screenTopEdge + knifeHeight, 0.75f).setEase(LeanTweenType.easeOutCubic);
                LeanTween.moveY(rightKnife, cameraHandler.screenTopEdge + knifeHeight, 0.75f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                {
                    LeanTween.delayedCall(0.25f, () => { SwitchToState(State.FlyingDown); });
                });
                break;

            case State.FlyingDown:
                leftKnife.transform.position = new Vector2(1, leftKnife.transform.position.y);
                rightKnife.transform.position = new Vector2(leftKnife.transform.position.x + knifeWidth, leftKnife.transform.position.y);
                leftKnife.transform.eulerAngles = new Vector3(0, 0, 0);
                rightKnife.transform.eulerAngles = new Vector3(0, 0, 0);

                LeanTween.moveY(leftKnife, startStabPosY, 1).setEase(LeanTweenType.easeOutCubic);
                LeanTween.moveY(rightKnife, startStabPosY, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
                {
                    SwitchToState(State.Stabbing);
                });
                break;

            case State.Stabbing:
                leftKnife.GetComponent<Knife>().StabDown();
                LeanTween.delayedCall(0.5f, () => { rightKnife.GetComponent<Knife>().StabDown(); });
                LeanTween.moveX(gameObject, 17.5f, 6).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => { SwitchToState(State.Leaving); });
                break;

            case State.Leaving:
                finishedPanning = true;
                LeanTween.moveY(gameObject, cameraHandler.screenTopEdge + knifeHeight, 1).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
                {
                    pawser.state = Pawser.State.Idle;
                    Destroy(gameObject);
                });
                break;
        }
    }
	
	public void Pause()
	{
		enabled = false;
		LeanTween.pauseAll();
	}
	
	public void UnPause()
	{
		enabled = true;
		LeanTween.resumeAll();
	}
}