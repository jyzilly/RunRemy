using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab; // �ܾ� ������ (TextMeshProUGUI�� ���Ե� ������Ʈ)
    public float spawnInterval = 2f; // �ܾ �����Ǵ� ����
    //public List<string> wordList; // ���� �ܾ� ���

    public string[] fallText = { "ŷ��¯", "���� �ְ�", "������" };

    private List<GameObject> activeWords = new List<GameObject>(); // ȭ�鿡 ǥ�õ� �ܾ��

    //public Camera mainCamera;
    public float spawnYOffset = -2f;
    public float minXOffset = -5f;
    public float maxOffset = 5f;
    public float fixedZ = 0f;

    public Canvas canvas;

    void Start()
    {
        InvokeRepeating("SpawnRandomWord", 0f, spawnInterval); // ���� �ð� �������� �ܾ ����
    }

    // ���� �ܾ �����Ͽ� ȭ�鿡 ǥ��
    void SpawnRandomWord()
    {
        //Vector3 screenTop = new Vector3(Screen.width / 2, Screen.height, 10f);
        //Vector3 worldTop = Camera.main.ScreenToWorldPoint(screenTop);

        //float randomX = Random.Range(minXOffset, maxOffset);
        //float spawnY = worldTop.y + spawnYOffset;

       // Vector3 spawnPosition = new Vector3(randomX, spawnY, fixedZ);

        string randomWord = fallText[Random.Range(0, fallText.Length)]; // ���� �ܾ� ����
        //GameObject newWord = Instantiate(wordPrefab, new Vector3(Random.Range(300f,900f), 920f, 0f), Quaternion.identity); // ȭ�� ��ܿ��� ����


        GameObject newWord = Instantiate(wordPrefab, canvas.transform);
        newWord.transform.localPosition = new Vector3(Random.Range(-830f, 450f), 380f, 0f);

        newWord.GetComponent<TextMeshProUGUI>().text = randomWord; // �ؽ�Ʈ ����
        activeWords.Add(newWord); // ������ �ܾ ����Ʈ�� �߰�
    }

    // ȭ�鿡�� ��ġ�ϴ� �ܾ �����ϴ� �޼���
    public void RemoveWord(string word)
    {
        for (int i = 0; i < activeWords.Count; i++)
        {
            GameObject activeWord = activeWords[i];
            if (activeWord != null && activeWord.GetComponent<TextMeshProUGUI>().text == word)
            {
                activeWords.RemoveAt(i); // �ܾ ����Ʈ���� ����
                Destroy(activeWord); // �ش� �ܾ� ����
                if(word == "��ȸ��")
                {

                }
                break;
            }
        }
    }
}
