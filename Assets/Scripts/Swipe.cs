﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeForward, swipeBackward;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeForward = swipeBackward = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition; // Vị trí của chuột khi ấn chuột trái
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion


        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position; // Vị trí bắt đầu chạm vào
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Tính khoảng cách
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Khoảng cách phải lớn hơn 125
        if (swipeDelta.magnitude > 125)
        {
            // Tìm hướng
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;

            }
            else
            {
                // up or down
                if (y < 0)
                    swipeBackward = true;
                else
                    swipeForward = true;
            }
        }
    }
    // Reset giá trị
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
    public bool Tap { get { return tap; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeForward { get { return swipeForward; } }
    public bool SwipeBackward { get { return swipeBackward; } }
}
