using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerControlScript : MonoBehaviour
{

    //public static PlayerControlScript Instance;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}


    //RectTransform platfTransf;

    //public bool pause;

    //float mult1;

    //public void Init()
    //{
    //    gameOver = false;

    //    pause = true;

    //    //Platform = GameManager.Instance.Platform;

    //    mult1 = canv.GetComponent<RectTransform>().lossyScale.x;
    //}

    //bool pressed;

    //bool gameOver;

    //public void SetPause(bool _pause) 
    //{
    //    pause = _pause;
    //}

    //public void SetGameOver(bool someBool)
    //{
    //    gameOver = someBool;
    //}

    //public void setPressed(bool _pressed) 
    //{
    //    pressed = _pressed;
    //}

    //private void FixedUpdate()
    //{
    //    if (pressed && !pause && !gameOver)
    //    {
    //        ChangePosition();
    //    }
    //    else 
    //    {
    //        isMoving = false;
    //    }
    //}

    //float x;

    //float treshold;

    //Vector3 speedVec;

    //float x_max;

    //float x_min;

    //public void Set_X_min(float _x_min)
    //{
    //    x_min = _x_min;
    //}

    //public void Set_X_max(float _x_max)
    //{
    //    x_max = _x_max;
    //}

    //public void SetPlatformSpeed(float ball_spd_x, float ball_spd_y) 
    //{
    //    speedVec = new Vector3(0.08f, 0.0f, 0.0f);

    //    treshold = 0.08f * 0.6f;
    //}

    //public Canvas canv;

    //bool last_dir_pstv;

    //private bool isMoving;

    //public List<bool> ShouldIMoveABit() 
    //{
    //    return new List<bool> { isMoving, last_dir_pstv };
    //}

    //private void ChangePosition()
    //{
    //    x = ((Input.mousePosition.x / Screen.width) - 0.5f) * x_max * 2;

    //    if (platfTransf.position.x - speedVec.x > x_min + (platfTransf.rect.size.x / 2) * mult1 && platfTransf.position.x + speedVec.x < x_max - (platfTransf.rect.size.x / 2) * mult1) 
    //    {
    //        isMoving = true;
    //        if (x - platfTransf.position.x > treshold)
    //        {
    //            platfTransf.position += speedVec;

    //            last_dir_pstv = true;
    //        }
    //        else if (platfTransf.position.x - x > treshold) 
    //        {
    //            platfTransf.position -= speedVec;

    //            last_dir_pstv = false;
    //        }
    //    }
    //    else 
    //    {
    //        isMoving = false;

    //        if (x > 0)
    //            platfTransf.position = new Vector3(Mathf.Min(x, x_max - (platfTransf.rect.size.x / 2) * mult1), platfTransf.position.y, 0.0f);
    //        else
    //            platfTransf.position = new Vector3(Mathf.Max(x, x_min + (platfTransf.rect.size.x / 2) * mult1), platfTransf.position.y, 0.0f);
    //    }
    //}
}
