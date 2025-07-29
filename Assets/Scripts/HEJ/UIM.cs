using UnityEngine;
using TMPro;

public class UIM : MonoBehaviour
{
    public Transform player;      // �÷��̾� ��ġ
    public Vector3 offset;        // �÷��̾���� �Ÿ� ����
    public TextMeshProUGUI text;  // ��� ǥ��

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;  // �÷��̾� ���� ��ġ
            transform.LookAt(Camera.main.transform);       // ī�޶� ���ϵ��� ȸ��
        }
    }

   
}
