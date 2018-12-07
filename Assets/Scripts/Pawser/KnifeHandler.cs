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
	
	//The id of the first active LeanTween animation. Needed for pause functionality.
	private int leanTweenActiveAnimID1 = -1;
	
	//The id of the second active LeanTween animation. Needed for pause functionality.
	private int leanTweenActiveAnimID2 = -1;



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
		//The current LeanTween animation being created.
		LTDescr curLeanTweenAnim;
		
        state = nextState;
		
		//Enable different animations based on the current state of the knife handler.
        switch (state)
        {
            case State.FlyingUp:
				//The animation for the left knife flying up off the screen.
				curLeanTweenAnim = LeanTween.moveY(leftKnife, cameraHandler.screenTopEdge + knifeHeight, 0.75f);
				curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
                leanTweenActiveAnimID1 = curLeanTweenAnim.id;
				
				//The animation for the right knife flying up off the screen.
				curLeanTweenAnim = LeanTween.moveY(rightKnife, cameraHandler.screenTopEdge + knifeHeight, 0.75f);
				curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
                curLeanTweenAnim.setOnComplete(() =>
                {
                    LeanTween.delayedCall(0.25f, () => 
					{ 
						SwitchToState(State.FlyingDown); 
					});
                });
				leanTweenActiveAnimID2 = curLeanTweenAnim.id;
				
                break;

            case State.FlyingDown:
                leftKnife.transform.position = new Vector2(1.75f, leftKnife.transform.position.y);
                rightKnife.transform.position = new Vector2(leftKnife.transform.position.x + knifeWidth, leftKnife.transform.position.y);
                leftKnife.transform.eulerAngles = new Vector3(0, 0, 0);
                rightKnife.transform.eulerAngles = new Vector3(0, 0, 0);

				//The animation for the left knife getting into stabbing position.
				curLeanTweenAnim = LeanTween.moveY(leftKnife, startStabPosY, 1);
				curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
                leanTweenActiveAnimID1 = curLeanTweenAnim.id;
				
				//The animation for the right knife getting into stabbing position.
                curLeanTweenAnim = LeanTween.moveY(rightKnife, startStabPosY, 1);
				curLeanTweenAnim.setEase(LeanTweenType.easeOutCubic);
				curLeanTweenAnim.setOnComplete(() =>
                {
                    SwitchToState(State.Stabbing);
                });
				leanTweenActiveAnimID2 = curLeanTweenAnim.id;
				
                break;

            case State.Stabbing:
				//Tell the left knife to start stabbing down.
                leftKnife.GetComponent<Knife>().StabDown();
				
				//Tell the right knife to start stabbing down after a set amount of time.
                curLeanTweenAnim = LeanTween.delayedCall(0.5f, () => 
				{
					rightKnife.GetComponent<Knife>().StabDown(); 
				});
				leanTweenActiveAnimID1 = curLeanTweenAnim.id;
				
				//Move both knives to the right until they are offscreen. When they are offscreen, switch to the leaving state.
                curLeanTweenAnim = LeanTween.moveX(gameObject, 14.5f, 6);
				curLeanTweenAnim.setEase(LeanTweenType.easeInOutQuad);
				curLeanTweenAnim.setOnComplete(() => 
				{ 
					SwitchToState(State.Leaving); 
				});
				leanTweenActiveAnimID2 = curLeanTweenAnim.id;
				
                break;

            case State.Leaving:
                finishedPanning = true;
				
				//Create the animation for making the knives disappear offscreen. Once they disappear, then delete the knife objects.
                curLeanTweenAnim = LeanTween.moveY(gameObject, cameraHandler.screenTopEdge + knifeHeight, 1);
				curLeanTweenAnim.setEase(LeanTweenType.easeInCubic);
				curLeanTweenAnim.setOnComplete(() =>
                {
                    Destroy(gameObject);
					leanTweenActiveAnimID1 = -1;
					leanTweenActiveAnimID2 = -1;
                });
				
				leanTweenActiveAnimID1 = curLeanTweenAnim.id;
				leanTweenActiveAnimID2 = -1;
				
                break;
			default:
				break;
        }
    }
	
	public void Pause()
	{
		enabled = false;
		
		if(leanTweenActiveAnimID1 != -1)
		{
			LeanTween.pause(leanTweenActiveAnimID1);
		}
		
		if(leanTweenActiveAnimID2 != -1)
		{
			LeanTween.pause(leanTweenActiveAnimID2);
		}
	}
	
	public void UnPause()
	{
		enabled = true;
		
		if(leanTweenActiveAnimID1 != -1)
		{
			LeanTween.resume(leanTweenActiveAnimID1);
		}
		
		if(leanTweenActiveAnimID2 != -1)
		{
			LeanTween.resume(leanTweenActiveAnimID2);
		}
	}
}