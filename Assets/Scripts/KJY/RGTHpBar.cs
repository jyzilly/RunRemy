using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RGTHpBar : MonoBehaviour
{
    [Header("Default settings")]
    [SerializeField] private RectTransform yellowRectTr = null;
    [SerializeField] private RectTransform redImgTr = null;
    [SerializeField] private RectTransform transparentTr = null;

    private float maxWidth = 100f;
    private float maxHeight = 100f;


    private void Awake()
    {
        maxWidth = yellowRectTr.sizeDelta.x;
        maxHeight = yellowRectTr.sizeDelta.y;

        //노란색 바 설정
        yellowRectTr.pivot = new Vector2(0f, 0.5f);  //왼쪽 중앙 기준
        yellowRectTr.anchorMin = new Vector2(0f, 0.5f);  //왼쪽 기준
        yellowRectTr.anchorMax = new Vector2(0f, 0.5f);  //왼쪽 기준

        //붉은색 바 설정
        redImgTr.pivot = new Vector2(0f, 0.5f);  //왼쪽 중앙 기준
        redImgTr.anchorMin = new Vector2(0f, 0.5f);  //왼쪽 기준
        redImgTr.anchorMax = new Vector2(0f, 0.5f);

        //투명 배경 설정
        transparentTr.pivot = new Vector2(0f, 0.5f);  //왼쪽 중앙 기준
        transparentTr.anchorMin = new Vector2(0f, 0.5f);  //왼쪽 기준
        transparentTr.anchorMax = new Vector2(0f, 0.5f);  //왼쪽 기준
    }

    //외부 호출할 수 있게
    public void UpdateHpBar(float _maxHp, float _curHp)
    {
        UpdateHpBar(_curHp / _maxHp);
    }

    public void UpdateHpBar(float _amount)
    {
        //현재 노란색 바의 너비
        float prevWidth = yellowRectTr.sizeDelta.x;
        //목표 너비
        float newWidth = maxWidth * _amount;

        StopAllCoroutines();

        if (newWidth < prevWidth)
        {
            //코루틴을 사용하여 노란색 바가 부드럽게 줄어드는 애니메이션을 실행
            StartCoroutine(UpdateHpBarCoroutine(prevWidth, newWidth));
        }
        else
        {
            //즉시 노란색 바의 너비를 변경
            yellowRectTr.sizeDelta = new Vector2(newWidth, maxHeight);
        }

        //붉은색 바는 항상 목표 너비로 즉시 변경
        redImgTr.sizeDelta = new Vector2(newWidth, maxHeight);
    }

    private IEnumerator UpdateHpBarCoroutine(float _prevWidth, float _newWidth)
    {
        //시작 시점의 너비로 설정
        Vector2 size = new Vector2(_prevWidth, maxHeight);
        yellowRectTr.sizeDelta = size;

        //1초동안 애니메이션 실행
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            //Mathf.Lerp를 사용하여 시작 너비와 목표 너비 사이를 부드럽게 보간
            size.x = Mathf.Lerp(_prevWidth, _newWidth, t);
            yellowRectTr.sizeDelta = size;
            yield return null;
        }
    }
}

