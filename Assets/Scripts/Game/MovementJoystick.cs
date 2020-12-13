using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

public class MovementJoystick : MonoBehaviour, IPointerDownHandler
{

    public GameObject joystick;
    public GameObject joystickBg;
    public Vector2 vector;
    public Player player;

    private Vector2 originalPos;
    private Vector2 touchPos;
    private float radious;

    void Start()
    {
        originalPos = joystickBg.transform.position;
        radious = joystickBg.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

   
    public void Drag(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        vector = (dragPos - touchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, touchPos);

        if (joystickDist < radious)
        {
            joystick.transform.position = touchPos + vector * joystickDist;
        }
        else
        {
            joystick.transform.position = touchPos + vector * radious;
        }
    }

    public void PointerUp()
    {
        if (player != null)
        {
            player.firing = false;
        }
        vector = Vector2.zero;
        joystick.transform.position = originalPos;
        joystickBg.transform.position = originalPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
        {
            player.firing = true;
        }
        joystick.transform.position = Input.mousePosition;
        joystickBg.transform.position = Input.mousePosition;
        touchPos = Input.mousePosition;
    }
}
