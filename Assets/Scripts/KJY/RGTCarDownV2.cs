﻿using System;
using UnityEngine;
public class RGTCarDownV2 : ICarDown
{
    //빠지는 만큼
    [SerializeField] private float sinkDepth = 2f;
    //빠지는 속도
    [SerializeField] private float sinkSpeed = 1f;
    //연타 간격
    [SerializeField] private float requiredTapSpeed = 0.5f;

    //위치 저장
    private Vector3 startPosition;
    private bool isSinking = true;
    private bool isRising = false;
    private float lastTapTime = 0f;
    private float riseTimer = 0f;
    private float KeyTimer = 0f;
    private bool hasStart = true;
    float targetY;

    //차량이 파괴되어야 할 때 발생하는 이벤트
    public event Action Die;

    public void Sink(Transform _body)
    {
        //Debug.Log("AAA Sink" + isRising.ToString());
        if (hasStart)
        {
            startPosition = _body.localPosition;
            targetY = startPosition.y - sinkDepth;
            hasStart = false;
            //Debug.Log("Start Position set: " + startPosition);
        }

        //만약에 현재의 맵은 사막맵이고 속도가 60아래 떨어지면 isSinking = true; 나중에 조건을 추가해야됨

        if (isSinking)
        {
            SinkSand(_body);
        }


    }

    public void Rising(Transform _body)
    {
        if (hasStart)
        {
            startPosition = _body.localPosition;
            targetY = startPosition.y - sinkDepth;
            hasStart = false;
            //Debug.Log("Start Position set: " + startPosition);
            //Debug.Log("AAA Risging : " + isRising);
        }
        // 연타 검사
        KeepTheKey();

        if (isRising)
        {
            SandUp(_body);
        }
    }



    private void SinkSand(Transform _body)
    {
        //현재 위치에서 목표 위치로 서서히 내려가도록 Lerp 사용
        float newY = Mathf.Lerp(_body.localPosition.y, targetY, Time.deltaTime * sinkSpeed);
        _body.localPosition = new Vector3(_body.localPosition.x, newY, _body.localPosition.z);

        //디버깅용 로그
        //Debug.Log("Sinking - Current Height: " + _body.localPosition.y + " Target: " + targetY);
    }

    private void SandUp(Transform _body)
    {
        //현재 y값에서 목표 y값(startPosition.y)으로 Lerp 보간
        float newY = Mathf.Lerp(_body.localPosition.y, startPosition.y, Time.deltaTime * sinkSpeed);
        //x, z값은 그대로 유지
        _body.localPosition = new Vector3(_body.localPosition.x, newY, _body.localPosition.z);

        //디버깅용 로그
        //Debug.Log("Rising - Current Height: " + _body.localPosition.y + " Target: " + startPosition.y);

        //목표 위치에 도달하면 멈춤
        if (Mathf.Abs(_body.position.y - startPosition.y) < 0.01f)
        {
            isRising = false;
            isSinking = true; // 다시 가라앉기 시작
            //Debug.Log("Reached original height, resuming sink");
        }
    }

    private void KeepTheKey()
    {
        //키입력 없는 시간을 기록
        KeyTimer += Time.deltaTime;

        if(KeyTimer >= 2f)
        {
            //터진다.
            Die();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //속도 체크
            if (Time.time - lastTapTime < requiredTapSpeed)
            {
                //연타감지 타이머 초기화
                KeyTimer = 0f;
                isRising = true;
                isSinking = false;
                riseTimer = 1f;
                //Debug.Log("M key rapid tap detected! Starting rise");
            }
            lastTapTime = Time.time;
        }

        //떠오르는 상태일 때 남은 시간을 감소시킨다
        if (isRising)
        {
            riseTimer -= Time.deltaTime;
            //소진시 다시 가라앉는 상태로 변경
            if (riseTimer <= 0)
            {
                isRising = false;
                isSinking = true;
                //Debug.Log("Rise timer expired, resuming sink");
            }
        }
    }
}