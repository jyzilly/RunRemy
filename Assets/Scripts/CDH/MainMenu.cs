using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽� �߰�

public class MainMenu : MonoBehaviour
{
    // ù ��° ��ư Ŭ�� �� ���� ������ �̵�
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); // MainScene�� ���� �� �̸�
    }

    // �� ��° ��ư Ŭ�� �� ���� ����
    public void QuitGame()
    {
#if UNITY_EDITOR // ����Ƽ������ ���� ���߿� �۵��Ǵ� �Լ�
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // ����� ���ӿ��� �۵��Ǵ� �Լ�
#endif
    }
}
