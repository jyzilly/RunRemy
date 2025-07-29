using UnityEngine;

public class FindAudioSource : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in allAudioSources)
        {
            Debug.Log("ã�� ����� �ҽ�: " + audio.name);
        }
    }

}
