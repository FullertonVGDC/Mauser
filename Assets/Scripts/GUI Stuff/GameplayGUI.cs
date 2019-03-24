using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        mPlayerComp = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mGlobalDataComp = GameObject.Find("GlobalData").GetComponent<GlobalData>();

        //Create the default hollow cookies.
        for (uint i = 0; i < 3; i++)
        {
            GameObject curCookieUI;

            curCookieUI = Instantiate(mCookieDarkUIPrefab, new Vector3(
                0.0f, 0.0f, -10.0f),
                Quaternion.identity);

            curCookieUI.transform.SetParent(transform.GetChild(1).transform);
            curCookieUI.transform.localPosition = new Vector3(
                200.0f + (i * 70.0f), -35.0f, -10.0f);

            Image curCookieSprite = curCookieUI.GetComponent<Image>();
            curCookieSprite.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthAndCurrencyDisplay();
        CheckIfPausing();
    }

    //Updates the health and currency display.
    private void UpdateHealthAndCurrencyDisplay()
    {
        //The player's health.
        uint playerHealth = mPlayerComp.GetHealth();

        //The player's armor cookies.
        uint playerArmor = mPlayerComp.GetArmor();

        //The number of bottle caps collected.
        uint playerBottleCaps = mGlobalDataComp.GetCurrency();

        //Create the cookies.
        if (playerHealth != mCurPlayerHealth)
        {
            mCurPlayerHealth = playerHealth;

            //Destroy the old cookies.
            foreach (Transform childTransform in transform.GetChild(1))
            {
                if (childTransform.gameObject.tag == "cookieUI")
                {
                    Destroy(childTransform.gameObject);
                }
            }

            //Create the new cookies.
            for (uint i = 0; i < mCurPlayerHealth; i++)
            {
                GameObject curCookieUI;

                curCookieUI = Instantiate(mCookieUIPrefab, new Vector3(
                    0.0f, 0.0f, -10.0f),
                    Quaternion.identity);

                curCookieUI.transform.SetParent(transform.GetChild(1).transform);
                curCookieUI.transform.localPosition = new Vector3(
                    200.0f + (i * 70.0f), -35.0f, -10.0f);
            }
        }

        //Create the armor cookies.
        if (playerArmor != mCurPlayerArmor)
        {
            mCurPlayerArmor = playerArmor;

            //Destroy the old armor cookies.
            foreach (Transform childTransform in transform.GetChild(1))
            {
                if (childTransform.gameObject.tag == "armorCookieUI")
                {
                    Destroy(childTransform.gameObject);
                }
            }

            //Create the new armor cookies.
            for (uint i = 0; i < mCurPlayerArmor; i++)
            {
                GameObject curArmorUI;

                curArmorUI = Instantiate(mArmorCookieUIPrefab, new Vector3(
                    0.0f, 0.0f, -10.0f),
                    Quaternion.identity);

                curArmorUI.transform.SetParent(transform.GetChild(1).transform);
                curArmorUI.transform.localPosition = new Vector3(
                    200.0f + ((i + 3) * 70.0f), -35.0f, -10.0f);
            }
        }

        //Update the bottle cap values.
        if (playerBottleCaps != mCurPlayerBottleCaps)
        {
            mCurPlayerBottleCaps = playerBottleCaps;

            //The text component of the bottle caps UI.
            Text textComp = transform.GetChild(0).GetChild(2).GetComponent<Text>();

            textComp.text = mCurPlayerBottleCaps.ToString();
        }
    }

    //Used to check if the game is being paused.
    private void CheckIfPausing()
    {
        //Check if the Pause key is pressed. If so, pause the game.
        if ((Input.GetButtonDown("Pause")) &&
                !GameObject.Find("Player").GetComponent<Player>().GetIsDead())
        {
            if (mIsPaused)
            {
                SetPaused(false);
                //mMusicPlayer.volume = 0.8f;
            }
            else
            {
                SetPaused(true);
                //mMusicPlayer.volume = 0.2f;
            }
        }
    }

    //Setters:
    public void SetPaused(bool paused)
    {
        if (!paused)
        {
            mIsPaused = false;

            Destroy(transform.GetChild(3).gameObject);

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                Player playerScript = gameObject.GetComponent<Player>();

                playerScript.UnPause();
            }

            GameObject.Find("GlobalData").GetComponent<GlobalData>().UnPause();

            if (GameObject.Find("Pawser") != null)
            {
                GameObject.Find("Pawser").GetComponent<Pawser>().UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("enemy1"))
            {
                DustBunny dustBunnyScript = gameObject.GetComponent<DustBunny>();

                dustBunnyScript.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("spider"))
            {
                Spider spiderScriptComp = gameObject.GetComponent<Spider>();

                spiderScriptComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("bullet"))
            {
                Bullet bulletComp = gameObject.GetComponent<Bullet>();

                bulletComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("knifeHandler"))
            {
                KnifeHandler knifeHandlerComp = gameObject.GetComponent<KnifeHandler>();

                knifeHandlerComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("knife"))
            {
                Knife knifeComp = gameObject.GetComponent<Knife>();

                knifeComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("tilechunk"))
            {
                TileChunk tileChunkComp = gameObject.GetComponent<TileChunk>();

                tileChunkComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("shockwaveSpawner"))
            {
                ShockwaveSpawner curComp = gameObject.GetComponent<ShockwaveSpawner>();

                curComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("hairball"))
            {
                Hairball curComp = gameObject.GetComponent<Hairball>();

                curComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("debris"))
            {
                TileDebris tileDebrisComp = gameObject.GetComponent<TileDebris>();

                tileDebrisComp.UnPause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("bee"))
            {
                Bee beeComp = gameObject.GetComponent<Bee>();

                beeComp.UnPause();
            }

            if (Camera.main.GetComponent<WallCamera>())
            {
                Camera.main.GetComponent<WallCamera>().UnPause();
            }
        }
        else
        {
            mIsPaused = true;

            GameObject pauseMenuObj = GameObject.Instantiate(mPauseMenuPrefab, new Vector3(
                    0.0f, 0.0f, -10.0f),
                    Quaternion.identity);

            pauseMenuObj.transform.SetParent(transform);

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                Player playerScript = gameObject.GetComponent<Player>();

                playerScript.Pause();
            }

            GameObject.Find("GlobalData").GetComponent<GlobalData>().Pause();

            if (GameObject.Find("Pawser") != null)
            {
                GameObject.Find("Pawser").GetComponent<Pawser>().Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("enemy1"))
            {
                DustBunny dustBunnyScript = gameObject.GetComponent<DustBunny>();

                dustBunnyScript.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("spider"))
            {
                Spider spiderScriptComp = gameObject.GetComponent<Spider>();

                spiderScriptComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("bullet"))
            {
                Bullet bulletComp = gameObject.GetComponent<Bullet>();

                bulletComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("knifeHandler"))
            {
                KnifeHandler knifeHandlerComp = gameObject.GetComponent<KnifeHandler>();

                knifeHandlerComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("knife"))
            {
                Knife knifeComp = gameObject.GetComponent<Knife>();

                knifeComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("tilechunk"))
            {
                TileChunk tileChunkComp = gameObject.GetComponent<TileChunk>();

                tileChunkComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("shockwaveSpawner"))
            {
                ShockwaveSpawner curComp = gameObject.GetComponent<ShockwaveSpawner>();

                curComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("hairball"))
            {
                Hairball curComp = gameObject.GetComponent<Hairball>();

                curComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("debris"))
            {
                TileDebris tileDebrisComp = gameObject.GetComponent<TileDebris>();

                tileDebrisComp.Pause();
            }

            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("bee"))
            {
                Bee beeComp = gameObject.GetComponent<Bee>();

                beeComp.Pause();
            }

            if (Camera.main.GetComponent<WallCamera>())
            {
                Camera.main.GetComponent<WallCamera>().Pause();
            }
        }
    }

    //Checks if the game is paused.
    private bool mIsPaused = false;

    //The current player health cached by the player.
    private uint mCurPlayerHealth;

    //The current player armor cookies cached by the player.
    private uint mCurPlayerArmor;

    //The current player bottle caps cached by the player.
    private uint mCurPlayerBottleCaps;

    //The player component.
    private Player mPlayerComp;

    //The global data component.
    private GlobalData mGlobalDataComp;

    //Prefabs.

    //The prefab for the cookie UI.
    public GameObject mCookieUIPrefab;

    //The prefab for the armor cookie UI.
    public GameObject mArmorCookieUIPrefab;

    //The prefab for the dark cookie UI.
    public GameObject mCookieDarkUIPrefab;

    //The prefab for the pause menu.
    public GameObject mPauseMenuPrefab;
}
