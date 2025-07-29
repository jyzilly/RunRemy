using UnityEngine;
using System.Collections;

public class Rock_Spawn : MonoBehaviour
{
    public GameObject rockPrefab;  // ������ �� ������
    //public Transform spawnPoint;  // ������ �� ���� ��ġ

    public float minSpawnDelay = 0.5f; // �ּ� ���� ����
    public float maxSpawnDelay = 3f; // �ִ� ���� ����

    public float minX = -50f; // ���� ������ X ��ǥ �ּҰ�
    public float maxX = 50f;  // ���� ������ X ��ǥ �ִ밪

    private void Start()
    {
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        while (true)
        {
            //���� �� ���� �ð�
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            // X ��ǥ�� �����ϰ� ����
            float randomX = Random.Range(minX, maxX);
            //Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);
            //Vector3 spawnPosition = new Vector3(randomX, 1600f, 1580f);

            // �� ���� ��ġ �� �ϳ� ����
            Vector3 spawnPosition = Random.value > 0.5f // 0.5���� ũ�� true
                ? new Vector3(randomX, 1600f, 1580f) // true�϶� ��ȯ
                : new Vector3(randomX, 760f, 730f); // false�� �� ��ȯ


            // �� ����
            //Instantiate(rockPrefab, spawnPosition, Quaternion.identity);


            GameObject newRock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

            // �ӵ� ���� ����
            Rigidbody rockRb = newRock.GetComponent<Rigidbody>();
            if (rockRb != null)
            {
                float randomSpeed = Random.Range(5f, 30f); // �ӵ� ����
                rockRb.linearVelocity = new Vector3(0, -randomSpeed, 0); // �Ʒ��� �̵��ϴ� �ӵ� �ٸ��� ����
            }

        }
    }
}
