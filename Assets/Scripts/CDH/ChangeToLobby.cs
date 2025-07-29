using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeToLobby : MonoBehaviour
{
    public PlayableDirector EndingScene; // Ÿ�Ӷ���

    void Start()
    {
        if (EndingScene != null)
        {
            EndingScene.stopped += OnTimelineEnd; // Ÿ�Ӷ��� ���� �� �̺�Ʈ ����
        }
    }

    void OnTimelineEnd(PlayableDirector director)
    {
        if (director == EndingScene)
        {
            SceneManager.LoadScene("Lobby"); // ��ȯ�� �� �̸� �Է�
        }
    }
}
