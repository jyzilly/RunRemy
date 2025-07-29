using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_InputField inputField; // �Է� �ʵ�
    public WordSpawner wordSpawner; // �ܾ� ���� ��ũ��Ʈ

    void Update()
    {
        // Enter Ű�� ������ �� �Էµ� �ܾ� Ȯ��
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string input = inputField.text; // �Էµ� �ؽ�Ʈ

            if (!string.IsNullOrEmpty(input))
            {
                wordSpawner.RemoveWord(input); // �ش� �ܾ� ����
                inputField.Select();  // InputField ����
                inputField.ActivateInputField();
                inputField.text = ""; // �Է� �ʵ� ����
            }
        }
    }

    public void hancomKey()
    {

    }
}
