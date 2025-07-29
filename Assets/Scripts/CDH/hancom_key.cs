using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldEnter : MonoBehaviour
{
    //public InputField inputField; // InputField UI�� ������ ����
    public TMP_InputField inputField;
    //public hancom_fallingText HCFT;

    void Start()
    {
        // InputField���� �ؽ�Ʈ �Է� �Ϸ� �� ���Ͱ� ������ �� ó���� �޼��� ����
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    // Enter Ű�� ������ �� ȣ��� �޼���
    void OnEndEdit(string input)
    {
        // �Էµ� ���� ��� ���� ������
        if (!string.IsNullOrEmpty(input))
        {
            // Enter Ű�� ������ �� ó���� �ڵ�
            Debug.Log("�Էµ� �ؽ�Ʈ: " + input);
            inputField.text = "";
        }
    }


}
