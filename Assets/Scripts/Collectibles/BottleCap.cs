using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BottleCapType {BOTTLECAP_TYPE_RED = 0, BOTTLECAP_TYPE_GOLD = 1, BOTTLECAP_TYPE_DEBUG = 2};

public class BottleCap : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        switch (bottleCapType)
        {
            case BottleCapType.BOTTLECAP_TYPE_RED: 
                mCurrency = 1;
                break;
            case BottleCapType.BOTTLECAP_TYPE_GOLD: 
                mCurrency = 5;
                break;
            case BottleCapType.BOTTLECAP_TYPE_DEBUG: 
                mCurrency = 999;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    //Getters:
    public uint GetCurrency()
    {
        return mCurrency;
    }

    //Variables:

    //The currency of the bottle cap.
    private uint mCurrency = 1;

    //Public variables.

    //The type of bottle cap. Has different currency and sprites.
    public BottleCapType bottleCapType;
}
