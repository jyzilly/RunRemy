using UnityEngine;
using TMPro;

public class UIM1 : MonoBehaviour
{
    public Transform player;      // �÷��̾� ��ġ
    public Vector3 offset;        // �÷��̾���� �Ÿ� ����

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;  // �÷��̾� ���� ��ġ
            transform.LookAt(Camera.main.transform);       // ī�޶� ���ϵ��� ȸ��
        }
    }

   
}
