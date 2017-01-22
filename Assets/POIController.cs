using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIController : MonoBehaviour {

    //public Animator animator;
    public Animation animooter;
    
    //public enum POIState
    //{
    //    Hidden,
    //    Pinging,
    //    Revealing,
    //    Maximized,
    //    Shrinking,
    //    Minimized
    //}

    public enum PlayingAnim
    {
        PulseFadeIn = 0,
        PulsePulse,
        PulseFadeOut,
        ImageFadeIn,
        ImagePlay,
        ImageFadeOut,
        ChevronFadeIn,
        ChevronPlay,
        ChevronFadeOut,

        NumAnims
    }

    public AnimationClip[] clips;

    PlayingAnim currentAnim = PlayingAnim.NumAnims;

    //POIState state = POIState.Hidden;
    public bool bOrbits = false;
    public float DistanceFromOrigin = 400;
    public Vector3 origin;
    public Vector3 orbitDirection;
    float raycastAccum = 0.0f;
    float minDurationToReveal = 0.5f;
    bool hidden = true;
    public bool hasBeenSeen = false;

	// Use this for initialization
	void Start () {
		
	}

    float timeInAnim = 0.0f;
    float animDuration = 0.0f;
    bool animLooops = false;

    void PlayAnim(PlayingAnim newAnim)
    {
        timeInAnim = 0.0f;
        animDuration = clips[(int)newAnim].length;
        animooter.Play(clips[(int)newAnim].name);
        animLooops = clips[(int)newAnim].name != "Nova";
        currentAnim = newAnim;

        Debug.Log("Playing Clip: " + clips[(int)newAnim].name);
    }
	
    void PrintTheBools()
    {
        //Debug.Log("GoToPulsing: " + animator.GetBool("GoToPulsing") +
        //        " GoToRevealing: " + animator.GetBool("GoToRevealing") +
        //        " GoToRevealed: " + animator.GetBool("GoToRevealed") +
        //        " GoToMinimizing: " + animator.GetBool("GoToMinimizing") +
        //        " GoToSeen: " + animator.GetBool("GoToSeen"));

                //animator.SetBool("GoToPulsing", false);
                //animator.SetBool("GoToRevealing", false);
                //animator.SetBool("GoToRevealed", false);
                //animator.SetBool("GoToMinimizing", false);
                //animator.SetBool("GoToSeen", false);
    }
    // Update is called once per frame
    void Update () {
        bool readyToMaximize = raycastAccum >= minDurationToReveal;
        raycastAccum = Mathf.Max(raycastAccum - Time.deltaTime, 0.0f);
        timeInAnim += Time.deltaTime;

        //Debug.Log("Raycast Accum: " + raycastAccum);

        //bool animationFinished = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0);
        bool animationFinished = timeInAnim >= animDuration;
        bool readyToMinimize = hasBeenSeen && raycastAccum <= 0.0f && (animLooops || animationFinished);
        //PrintTheBools();

        //switch (state)
        //{
           // case POIState.Hidden:
                //do nothing, wait for manager to un-hide
            //    break;

            //case POIState.Pinging:
                switch (currentAnim)
                {
                    case PlayingAnim.PulseFadeIn:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.PulsePulse);
                        }
                        break;

                    case PlayingAnim.PulsePulse:
                        {
                            if (readyToMaximize)
                            {
                                PlayAnim(PlayingAnim.PulseFadeOut);
                            }
                        }
                        break;

                    case PlayingAnim.PulseFadeOut:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.ImageFadeIn);
                        }
                        break;

                    case PlayingAnim.ImageFadeIn:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.ImagePlay);
                    hasBeenSeen = true;
                        }
                        break;

                    case PlayingAnim.ImagePlay:
                        if (readyToMinimize)
                        {
                            PlayAnim(PlayingAnim.ImageFadeOut);
                        }
                        break;

                    case PlayingAnim.ImageFadeOut:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.ChevronFadeIn);
                        }
                        break;

                    case PlayingAnim.ChevronFadeIn:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.ChevronPlay);
                        }
                        break;

                    case PlayingAnim.ChevronPlay:
                        if (readyToMaximize)
                        {
                            PlayAnim(PlayingAnim.ChevronFadeOut);
                        }
                        break;

                    case PlayingAnim.ChevronFadeOut:
                        if (animationFinished)
                        {
                            PlayAnim(PlayingAnim.ImageFadeIn);
                        }
                        break;
                }
                //if (animationFinished && readyToMaximize)
                //{
                //    animator.SetBool("GoToRevealing", true);
                //    state = POIState.Revealing;
                //    hasBeenShown = true;
                //    Debug.Log("Setting animation state:" + state);
                //}
                //break;

            //case POIState.Revealing:
                //if (animationFinished)
                //{
                //    animator.SetBool("GoToRevealed", true);
                //    state = POIState.Maximized;
                //    Debug.Log("Setting animation state:" + state);
                //}
            //    break;

            //case POIState.Maximized:
                //if (readyToMinimize)
                //{
                //    animator.SetBool("GoToMinimizing", true);
                //    hasBeenSeen = true;
                //    state = POIState.Shrinking;
                //    Debug.Log("Setting animation state:" + state);
                //}
           //     break;

            //case POIState.Shrinking:
                //if (animationFinished)
                //{
                //    animator.SetBool("GoToSeen", true);
                //    state = POIState.Minimized;
                //    Debug.Log("Setting animation state:" + state);
                //}
            //    break;

            //case POIState.Minimized:
                //if (readyToMaximize)
                //{
                //    animator.SetBool("GoToRevealing", true);
                //    state = POIState.Revealing;
                //    Debug.Log("Setting animation state:" + state);
                //}
              //  break;
        //}
    }

    public void BeginPulsing()
    {
        if (hidden)
        {
            hidden = false;
            PlayAnim(PlayingAnim.PulseFadeIn);
            //state = POIState.Pinging;
        }
    }

    public void UserRaycastHit(float dt)
    {
        if (hidden)
            return;

        raycastAccum = Mathf.Min(raycastAccum + dt * 2.0f, minDurationToReveal);
    }

    void OnTriggerEnter(Collider other)
    {
        UserRaycastHit(Time.deltaTime);
        print("Totes hit a collider");
    }
}
