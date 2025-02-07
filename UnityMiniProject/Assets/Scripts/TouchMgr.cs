using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchMgr : MonoBehaviour
{
    public enum TouchState
    {
        None,
        Tap,
        DoubleTap,
        LongPress
    }

    public TouchState CurrentTouchState { get; private set; }

    public bool Tap => CurrentTouchState == TouchState.Tap;
    public bool DoubleTap => CurrentTouchState == TouchState.DoubleTap;
    public bool LongPress => CurrentTouchState == TouchState.LongPress;

    public Vector2 SwipeDirection { get; private set; }

    private float tapTimeThreshold = 0.2f;
    private float doubleTapTimeThreshold = 0.3f;
    private float longPressTimeThreshold = 0.5f;
    private float swipeTimeThreshold = 0.5f;
    private float swipeMinDistance = 0.5f;

    private float lastTapTime;
    private float touchStartTime;
    private Vector2 touchStartPos;
    private float moveThreshold = 50f;
    private bool isWaitingForDoubleTap;
    private bool isTouching;

    private int firstFingerId = -1;
    private int secondFingerId = -1;
    private Vector2 firstFingerStartPos;
    private Vector2 secondFingerStartPos;

    private void Update()
    {
        TouchState previousState = CurrentTouchState;
        CurrentTouchState = TouchState.None;
        SwipeDirection = Vector2.zero;

        if (isWaitingForDoubleTap && Time.time - lastTapTime > doubleTapTimeThreshold)
        {
            CurrentTouchState = TouchState.Tap;
            isWaitingForDoubleTap = false;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    touchStartPos = touch.position;
                    isTouching = true;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    if (touch.phase == TouchPhase.Ended)
                    {
                        float touchDuration = Time.time - touchStartTime;
                        Vector2 delta = touch.position - touchStartPos;
                        float touchDistance = delta.magnitude;
                        float touchDistanceInches = touchDistance / Screen.dpi;

                        if (touchDuration <= swipeTimeThreshold && touchDistanceInches >= swipeMinDistance)
                        {
                            SwipeDirection = delta.normalized;
                        }
                        else if (touchDistance < moveThreshold)
                        {
                            if (touchDuration < tapTimeThreshold)
                            {
                                if (isWaitingForDoubleTap)
                                {
                                    if (Time.time - lastTapTime < doubleTapTimeThreshold)
                                    {
                                        CurrentTouchState = TouchState.DoubleTap;
                                        isWaitingForDoubleTap = false;
                                    }
                                }
                                else
                                {
                                    lastTapTime = Time.time;
                                    isWaitingForDoubleTap = true;
                                }
                            }
                        }
                    }
                    break;

                case TouchPhase.Stationary:
                    if (isTouching && previousState != TouchState.LongPress)
                    {
                        float currentDuration = Time.time - touchStartTime;
                        if (currentDuration > longPressTimeThreshold)
                        {
                            CurrentTouchState = TouchState.LongPress;
                            isWaitingForDoubleTap = false;
                        }
                    }
                    break;
            }
        }
    }
}
