using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Countdown : MonoBehaviour
{

    private float startTime;
    public float countdown = 5f;
    private float time = 0;
    public AudioClip sound;


    private bool showTime = true;

    [SerializeField]
    private Text countdownTimerTxt;

    private void Start()
    {
        startTime = countdown;
    }

    private void Update()
    {
        if (countdown > 0)
            Deduct();
        else
        {
            LogicManager.Instance.IsLevelLost = true;

        }

        if (PlayerStats.NbAntidote==PlayerStats.RequireNbAntidote)
        {
            UIManager.Instance.ShowMissionCanvas();
            if (UIManager.Instance.missionUI != null)
            {
                UIManager.Instance.missionUI.text= "MISSION ACCOMPLIE...";
            }
            LogicManager.Instance.IsLevelWin = true;
        }

       /* if (countdown < 5&&PlayerStats.NbAntidote<PlayerStats.RequireNbAntidote)
        {
            UIManager.Instance.ShowMissionCanvas();
            if (UIManager.Instance.missionUI != null)
            {
                UIManager.Instance.missionUI.text= "Vous semblez mettre du temps il vous faut un dosage de plus ";
            }
            PlayerStats.RequireNbAntidote++;
            countdown += startTime;
            //TODO
            //activer le prochain antidote
        }*/
    }

    public void Deduct()
    {
        countdown -= Time.deltaTime;

        time += Time.deltaTime;

        if (time >= 1)
        {
            time = 0;
            showTime = true;

        }

        if (showTime)
        {
            //play beep sound
            GetComponent<AudioSource>().PlayOneShot(sound);
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            countdownTimerTxt.text = string.Format("{0:00}:{1:00}", (int) (Mathf.Round((countdown-1)>0?(countdown-1):0) / 60) , Mathf.Round((countdown-1)>0?(countdown-1):0)%60);
        }
        
        showTime = false;
    }
}
