using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sleepyness : MonoBehaviour
{
    public enum SleepyStates {Blink, NarrowedVision, Yawn, Swaying, blurriness, hallucinations}

    public SpriteRenderer SleepRenderer;
    private List<SleepyStates> effects;
    private List<SleepyStates> effectsToBeRemoved;

    private List<SleepyStates> avilable;

    public int sleepyModifer = 0;

    public float timeSinceLastSleepynessCheck = 0f;

    public int chanceOfgettingADebug = 90;
    private float sleepynessCheckFrequency = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        effects = new List<SleepyStates>();
        avilable = new List<SleepyStates>();
        effectsToBeRemoved = new List<SleepyStates>();
        avilable.Add(SleepyStates.Blink);
        avilable.Add(SleepyStates.NarrowedVision);
        avilable.Add(SleepyStates.Yawn);
        avilable.Add(SleepyStates.Swaying);
        avilable.Add(SleepyStates.blurriness);
        avilable.Add(SleepyStates.hallucinations);
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
                    effects.Add(avilable[selectRoll]);
                    avilable.RemoveAt(selectRoll);
                }
                else
                {
                    Debug.Log("Sleepyness : No valid debuffs");
                }
            }
            timeSinceLastSleepynessCheck = 0;
        }
    }


    public void LateUpdate()
    {
        foreach (SleepyStates i in effectsToBeRemoved)
        {
            effects.Remove(i);
            avilable.Add(i);
        }
    }

    private void ApplyEffect(SleepyStates sleepyStates)
    {
        Debug.Log("Applying : " + sleepyStates);
        switch (sleepyStates)
        {
            case SleepyStates.Blink:
                blink();
                RequestRemoveSleepState(SleepyStates.Blink);
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
        me.avilable.Add(SleepyStates.Blink);
        renderer.color = colorNow;
    }


    public void blink()
    {
        StartCoroutine(FlashSprite(SleepRenderer, 0f, 1f,1.5f,3,this));
    }


    /// <summary>
    /// Removes the current state at the end of current frame in lateUpdate
    /// </summary>
    /// <param name="toBeRemoved"></param>
    public void RequestRemoveSleepState(SleepyStates toBeRemoved)
    {
        if (! effectsToBeRemoved.Contains(toBeRemoved))
        {
            effectsToBeRemoved.Add(toBeRemoved);
        }
    }

    public List<SleepyStates> getCurrentSleepDebuffs()
    {
        return effects;
    }




}
