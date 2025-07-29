using UnityEngine;

public class ClearEndToLobby : MonoBehaviour
{
    public GameObject clearEnd;  // ���� Ŭ���� �̹���(3D ������Ʈ)

    void Start()
    {
        // ���� Ŭ���� ���� Ȯ��
        if (PlayerPrefs.GetInt("GameCleared", 0) == 1)
        {
            // ���� Ŭ���� �̹��� Ȱ��ȭ
            clearEnd.SetActive(true);
        }
        else
        {
            // ���� Ŭ���� �̹��� ��Ȱ��ȭ
            clearEnd.SetActive(false);
        }
    }
}
