using System;
using UnityEngine;


public class RGTTornadoRandomTrs : MonoBehaviour
{
    //����� ��ġ �迭
    [SerializeField] private Transform[] positions;
    //�̵��� ������Ʈ
    [SerializeField] private GameObject obj;
    //�̵� �ӵ�
    [SerializeField] private float speed = 10f; 
    [SerializeField] Transform originPos;


    private Vector3 Opos;
    //���� ��ǥ ��ġ �ε���
    private int currentIndex = 0;
    //��ǥ ��ġ
    private Vector3 targetPosition; 

    private void Start()
    {
        //�ʱ� ��ǥ ��ġ ����
        SetNextTargetPosition();
    }

    private void Update()
    {
        MoveToNextPosition();
    }

    //��ǥ ��ġ�� �̵��ϴ� �Լ�
    private void MoveToNextPosition()
    {
        //�ε巴�� ��ǥ ��ġ�� �̵�
        obj.transform.position = Vector3.MoveTowards(
            obj.transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        //��ǥ ��ġ�� �����ϸ� ���� ��ġ ����
        if (Vector3.Distance(obj.transform.position, targetPosition) < 0.1f)
        {
            SetNextTargetPosition();
        }
    }

    //���� ��ǥ ��ġ ���� �Լ�
    private void SetNextTargetPosition()
    {
        currentIndex = (currentIndex + 1) % positions.Length;
        targetPosition = positions[currentIndex].position;
    }

}