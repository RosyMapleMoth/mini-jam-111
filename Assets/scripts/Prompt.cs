using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Prompt : MonoBehaviour
{

    public sleepyness mysleepstate;
    public TextMeshProUGUI BlinkPromptText; 
    public float mouseMovesSum;
    public float mouseMoveABSSum;
    private Queue<float> mouseMoves;
    private float rotateHorizontal;
    private float rotateVertical;

    private List<sleepyness.SleepyStates> promptsEnabled;
    private List<sleepyness.SleepyStates> promptsToEnable;

    private List<sleepyness.SleepyStates> promptsToDisable;




    void Awake()
    {
        promptsEnabled = new List<sleepyness.SleepyStates>();
        promptsToEnable = new List<sleepyness.SleepyStates>();
        promptsToDisable = new List<sleepyness.SleepyStates>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (sleepyness.SleepyStates i in promptsEnabled)
        {
            applyUpdate(i);
        }
    }


    void LateUpdate()
    {
        foreach (sleepyness.SleepyStates i in promptsToDisable)
        {
            disablePrompt(i);
        }
        promptsToDisable.Clear();
        foreach (sleepyness.SleepyStates j in promptsToEnable)
        {
            enablePrompt(j);
        }
        promptsToEnable.Clear();

    }







    public void StartBlinkPrompt()
    {
        BlinkPromptText.SetText("Shake mouse to shake away sleepness");
    }

    public void StopBlinkPrompt()
    {
        BlinkPromptText.SetText("GoodShake");
    }



    public void MouseShakeStart()
    {
        mouseMoves = new Queue<float>();
        StartBlinkPrompt();
        mouseMovesSum = 0;
        mouseMoveABSSum = 0;

    }

    public void MouseShakeUpdate()
    {
        rotateHorizontal = Input.GetAxis ("Mouse X");
        rotateVertical = Input.GetAxis ("Mouse Y");
        
        if (mouseMoves.Count < 60)
        {
            mouseMoves.Enqueue(rotateHorizontal);
            mouseMovesSum += rotateHorizontal;
            //mouseMovesSum += 1f;
            mouseMoveABSSum += Mathf.Abs(rotateHorizontal);
        }
        else
        {
            float deque = mouseMoves.Dequeue();
            mouseMovesSum -= deque;
            mouseMoveABSSum -= deque;

            mouseMoves.Enqueue(rotateHorizontal);
            mouseMovesSum += rotateHorizontal;
            mouseMoveABSSum += Mathf.Abs(rotateHorizontal);
        }

        if (mouseMoveABSSum > 300 && mouseMovesSum < 30)
        {
            mysleepstate.RequestRemoveSleepState(sleepyness.SleepyStates.Blinking);
        }
    }

    private void MouseShakeStop()
    {
        StopBlinkPrompt();
    }

    public void RequestToDisablePrompt(sleepyness.SleepyStates state)
    {
        promptsToDisable.Add(state);
    }

    public void RequestToEnablePrompt(sleepyness.SleepyStates state)
    {   
        promptsToEnable.Add(state); 
    }


    private void applyUpdate(sleepyness.SleepyStates state)
    {
        switch (state)
        {
            case sleepyness.SleepyStates.Blinking: 
                MouseShakeUpdate();
                break;
            case sleepyness.SleepyStates.blurriness:
                // not implamented 
                break;
            case sleepyness.SleepyStates.hallucinations:
                // not implamented 
                break;
            case sleepyness.SleepyStates.NarrowedVision:
                // not implamented 
                break;
            case sleepyness.SleepyStates.Swaying:
                // not implamented 
                break;
            case sleepyness.SleepyStates.Yawn:
                // not implamented 
                break;
            default:
                break;
        }
    }

    private void enablePrompt(sleepyness.SleepyStates state)
    {
        if(!promptsEnabled.Contains(state))
        {
            promptsEnabled.Add(state);
            switch (state)
            {
                case sleepyness.SleepyStates.Blinking: 
                    MouseShakeStart();
                    break;
                case sleepyness.SleepyStates.blurriness:
                    //BlinkPromptText.SetText("blurriness");
                    break;
                case sleepyness.SleepyStates.hallucinations:
                    //BlinkPromptText.SetText("hallucinations");
                    break;
                case sleepyness.SleepyStates.NarrowedVision:
                    //BlinkPromptText.SetText("NarrowedVision");
                    break;
                case sleepyness.SleepyStates.Swaying:
                    //BlinkPromptText.SetText("Swaying"); 
                    break;
                case sleepyness.SleepyStates.Yawn:
                    //  BlinkPromptText.SetText("Yawn");
                    break;
                default:
                    break;
            }
        }
    }

    private void disablePrompt(sleepyness.SleepyStates state)
    {
        if(promptsEnabled.Contains(state))
        {
            promptsEnabled.Remove(state);
            switch (state)
            {
                case sleepyness.SleepyStates.Blinking: 
                    MouseShakeStop();
                    break;
                case sleepyness.SleepyStates.blurriness:
                    // not implamented 
                    break;
                case sleepyness.SleepyStates.hallucinations:
                    // not implamented 
                    break;
                case sleepyness.SleepyStates.NarrowedVision:
                    // not implamented 
                    break;
                case sleepyness.SleepyStates.Swaying:
                    // not implamented 
                    break;
                case sleepyness.SleepyStates.Yawn:
                    // not implamented 
                    break;
                default:
                    break;
            }
        }
    }
}
