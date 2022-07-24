using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sleepyness : MonoBehaviour
{
    public enum SleepyStates {Blinking, NarrowedVision, Yawn, Swaying, blurriness, hallucinations}
    public GameObject TextPrefab;
    public GameObject DebugDebuffs;
    Color nonBlink;

    public Prompt prompter;

    public SpriteRenderer SleepRenderer;
    private List<SleepyStates> effects;
    private List<SleepyStates> effectsToBeAdded;
    private List<SleepyStates> effectsToBeRemoved;
    private List<SleepyStates> avilable;
    public int sleepyModifer = 0;
    public float timeSinceLastSleepynessCheck = 0f;
    public int chanceOfgettingADebug = 90;
    private float sleepynessCheckFrequency = 1f;
    private float blinkTimer = 0f;
    private bool blinkingCurrently = false;
    private Coroutine blinkfunc;







    // Start is called before the first frame update
    void Awake()
    {
        effects = new List<SleepyStates>();
        avilable = new List<SleepyStates>();
        effectsToBeAdded = new List<SleepyStates>();
        effectsToBeRemoved = new List<SleepyStates>();
        avilable.Add(SleepyStates.Blinking);
        //avilable.Add(SleepyStates.NarrowedVision);
        //avilable.Add(SleepyStates.Yawn);
        //avilable.Add(SleepyStates.Swaying);
        //avilable.Add(SleepyStates.blurriness);
        //avilable.Add(SleepyStates.hallucinations);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSleepynessCheck += Time.deltaTime;
        
        
        for (int i = 0; i < effects.Count; i++)
        {
            ApplyEffect(effects[i]);
        }
        
        if (timeSinceLastSleepynessCheck > sleepynessCheckFrequency)
        {
            Debug.Log("Sleepyness : Checking if we add debuffs");
            float roll = UnityEngine.Random.Range(0,100) + sleepyModifer;
            if (roll > chanceOfgettingADebug)
            {
                // TODO replace this with something that picks a random sleep state when we have more states
                if (avilable.Count > 0)
                {
                    
                    int selectRoll = UnityEngine.Random.Range(0, avilable.Count);
                    Debug.Log("Sleepyness : Adding " + avilable[selectRoll]);
                    AddState(avilable[selectRoll]);
                }
                else
                {
                    Debug.Log("Sleepyness : No valid debuffs");
                }
            }
            timeSinceLastSleepynessCheck = 0f;
        }
    }

    private void ApplyPrompt(SleepyStates sleepyStates)
    {
        Debug.Log("Applying : " + sleepyStates);
        switch (sleepyStates)
        {
            case SleepyStates.Blinking: 
                break;
            case SleepyStates.blurriness:
                // not implamented 
                break;
            case SleepyStates.hallucinations:
                // not implamented 
                break;
            case SleepyStates.NarrowedVision:
                // not implamented 
                break;
            case SleepyStates.Swaying:
                // not implamented 
                break;
            case SleepyStates.Yawn:
                // not implamented 
                break;
            default:
                break;
        }
    }

    public void LateUpdate()
    {
        foreach (SleepyStates i in effectsToBeRemoved)
        {
            removeState(i);
        }
        effectsToBeRemoved.Clear();

        foreach (SleepyStates j in effectsToBeAdded)
        {
            AddState(j);
        }
        effectsToBeAdded.Clear();

        for (int i = 0; i < DebugDebuffs.transform.childCount; i++) 
        {
            if (effects.Count > i)
            {
                DebugDebuffs.transform.GetChild(i).gameObject.SetActive(true);
                DebugDebuffs.transform.GetChild(i).GetComponent<TextMeshProUGUI>().SetText(effects[i].ToString());
            }
            else
            {
                DebugDebuffs.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    
    }

    private void ApplyEffect(SleepyStates sleepyStates)
    {
        Debug.Log("Applying : " + sleepyStates);
        switch (sleepyStates)
        {
            case SleepyStates.Blinking:
                if (!blinkingCurrently)
                {
                    blinkTimer += Time.deltaTime;
                    if (blinkTimer > 3f)
                    {
                        blink();
                        blinkTimer = 0f;
                        blinkingCurrently = true;
                    }
                } 
                break;
            case SleepyStates.blurriness:
                // not implamented 
                break;
            case SleepyStates.hallucinations:
                // not implamented 
                break;
            case SleepyStates.NarrowedVision:
                // not implamented 
                break;
            case SleepyStates.Swaying:
                // not implamented 
                break;
            case SleepyStates.Yawn:
                // not implamented 
                break;
            default:
                break;
        }
    }


    // from https://gamedev.stackexchange.com/questions/128959/how-can-i-make-a-character-blink-effect-in-unity
    ///Renderer is your sprite's SpriteRenderer
    ///minAlpha is the lower bound that the alpha will "bounce" between
    ///maxAlpha is the upper bound that the alpha will "bounce" between
    ///interval is how long each "bounce" takes
    ///duration is how long the flashing lasts
    public static IEnumerator FlashSprite(SpriteRenderer renderer, float minAlpha, float maxAlpha, float interval, float duration, sleepyness me)
    {
        Color colorNow = renderer.color;
        Color minColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, minAlpha);
        Color maxColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);

        float currentInterval = 0;
        while(duration > 0)
        {
            float tColor = currentInterval / interval;
            renderer.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if(currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= Time.deltaTime;
            yield return null;
        }
        me.blinkingCurrently = false;
        renderer.color = colorNow;
    }


    public void blink()
    {
        nonBlink = SleepRenderer.color;
        Debug.Log("Starting corutine");
        blinkfunc = StartCoroutine(FlashSprite(SleepRenderer, 0f, 1f,1.5f,3f,this));
    }

    /// <summary>
    /// Removes the current state at the end of current frame in lateUpdate
    /// </summary>
    /// <param name="toBeRemoved"></param>
    public void RequestRemoveSleepState(SleepyStates toBeRemoved)
    {
        if (!effectsToBeRemoved.Contains(toBeRemoved))
        {
            effectsToBeRemoved.Add(toBeRemoved);
        }
    }

    public void RequestAddSleepState(SleepyStates toBeAdded)
    {
        if (!effectsToBeAdded.Contains(toBeAdded))
        {
            effectsToBeAdded.Add(toBeAdded);
        }
    }


    private void removeState(SleepyStates state)
    {
        prompter.RequestToDisablePrompt(state);
        effects.Remove(state);
        avilable.Add(state);
        switch (state)
        {
            case SleepyStates.Blinking: 
                Debug.Log("stopping corutine");
                StopCoroutine(blinkfunc);
                SleepRenderer.color = nonBlink;
                break;
            case SleepyStates.blurriness:
                // not implamented 
                break;
            case SleepyStates.hallucinations:
                // not implamented 
                break;
            case SleepyStates.NarrowedVision:
                // not implamented 
                break;
            case SleepyStates.Swaying:
                // not implamented 
                break;
            case SleepyStates.Yawn:
                // not implamented 
                break;
            default:
                break;
        }
    }


    private void AddState(SleepyStates state)
    {
        effects.Add(state);
        prompter.RequestToEnablePrompt(state);
        avilable.Remove(state);
        switch (state)
        {
            case SleepyStates.Blinking: 
                blink();
                blinkTimer = 0f;
                blinkingCurrently = true;
                break;
            case SleepyStates.blurriness:
                // not implamented 
                break;
            case SleepyStates.hallucinations:
                // not implamented 
                break;
            case SleepyStates.NarrowedVision:
                // not implamented 
                break;
            case SleepyStates.Swaying:
                // not implamented 
                break;
            case SleepyStates.Yawn:
                // not implamented 
                break;
            default:
                break;
        }
    }



    public List<SleepyStates> getCurrentSleepDebuffs()
    {
        return effects;
    }
}
