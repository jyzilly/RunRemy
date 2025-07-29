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

        //����� �� ����
        yellowRectTr.pivot = new Vector2(0f, 0.5f);  //���� �߾� ����
        yellowRectTr.anchorMin = new Vector2(0f, 0.5f);  //���� ����
        yellowRectTr.anchorMax = new Vector2(0f, 0.5f);  //���� ����

        //������ �� ����
        redImgTr.pivot = new Vector2(0f, 0.5f);  //���� �߾� ����
        redImgTr.anchorMin = new Vector2(0f, 0.5f);  //���� ����
        redImgTr.anchorMax = new Vector2(0f, 0.5f);

        //���� ��� ����
        transparentTr.pivot = new Vector2(0f, 0.5f);  //���� �߾� ����
        transparentTr.anchorMin = new Vector2(0f, 0.5f);  //���� ����
        transparentTr.anchorMax = new Vector2(0f, 0.5f);  //���� ����
    }

    //�ܺ� ȣ���� �� �ְ�
    public void UpdateHpBar(float _maxHp, float _curHp)
    {
        UpdateHpBar(_curHp / _maxHp);
    }

    public void UpdateHpBar(float _amount)
    {
        //���� ����� ���� �ʺ�
        float prevWidth = yellowRectTr.sizeDelta.x;
        //��ǥ �ʺ�
        float newWidth = maxWidth * _amount;

        StopAllCoroutines();

        if (newWidth < prevWidth)
        {
            //�ڷ�ƾ�� ����Ͽ� ����� �ٰ� �ε巴�� �پ��� �ִϸ��̼��� ����
            StartCoroutine(UpdateHpBarCoroutine(prevWidth, newWidth));
        }
        else
        {
            //��� ����� ���� �ʺ� ����
            yellowRectTr.sizeDelta = new Vector2(newWidth, maxHeight);
        }

        //������ �ٴ� �׻� ��ǥ �ʺ�� ��� ����
        redImgTr.sizeDelta = new Vector2(newWidth, maxHeight);
    }

    private IEnumerator UpdateHpBarCoroutine(float _prevWidth, float _newWidth)
    {
        //���� ������ �ʺ�� ����
        Vector2 size = new Vector2(_prevWidth, maxHeight);
        yellowRectTr.sizeDelta = size;

        //1�ʵ��� �ִϸ��̼� ����
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            //Mathf.Lerp�� ����Ͽ� ���� �ʺ�� ��ǥ �ʺ� ���̸� �ε巴�� ����
            size.x = Mathf.Lerp(_prevWidth, _newWidth, t);
            yellowRectTr.sizeDelta = size;
            yield return null;
        }
    }
}

