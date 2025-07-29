using UnityEngine;

public class RandomTerrain : MonoBehaviour
{
    public Terrain[] terrain;           // �ͷ��� ����
    public GameObject[] objectPrefab;   // ��ġ�� ������Ʈ ������
    public int numberOfObjects = 100;     // ��ġ�� ������Ʈ ����

    void Start()
    {
        if (terrain == null || objectPrefab == null)
        {
            Debug.LogError("Terrain �Ǵ� objectPrefab�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        foreach (var t in terrain)
        {
            TerrainData terrainData = t.terrainData;
            Vector3 terrainSize = terrainData.size;
            Vector3 terrainPosition = t.transform.position;

            for (int i = 0; i < numberOfObjects; i++)
            {
                // �ͷ��� ���� ���� ���� x, z ��ǥ (�ͷ��� ���� ��ǥ ����)
                float randomX = Random.Range(0f, terrainSize.x);
                float randomZ = Random.Range(0f, terrainSize.z);

                // ���� ��ǥ�� ��ȯ
                Vector3 samplePosition = new Vector3(randomX + terrainPosition.x, 0f, randomZ + terrainPosition.z);

                // �ش� ��ġ�� ����(y) �� ���
                float terrainHeight = t.SampleHeight(samplePosition) + terrainPosition.y;
                // �⺻ ��ġ ��ġ (�ͷ��� ǥ��)
                Vector3 finalPosition = new Vector3(samplePosition.x, terrainHeight, samplePosition.z);

                // �ͷ����� ����ȭ�� ��ǥ (0 ~ 1)
                float normalizedX = randomX / terrainSize.x;
                float normalizedZ = randomZ / terrainSize.z;
                // �ش� ������ ����(normal) ���
                Vector3 terrainNormal = terrainData.GetInterpolatedNormal(normalizedX, normalizedZ);

                // �ͷ����� ������ �°� ������Ʈ�� ȸ�� ����
                Quaternion alignRotation = Quaternion.FromToRotation(Vector3.up, terrainNormal);
                // ���� ȸ���� �����ϰ� ���� (�ɼ�)
                Quaternion randomYRotation = Quaternion.Euler(0, Random.Range(-45f, 45f), 0);
                Quaternion finalRotation = alignRotation * randomYRotation;

                // ���� ������ ����
                GameObject prefab = RandomPrefab();

                // ������Ʈ ����
                GameObject instance = Instantiate(prefab, finalPosition + new Vector3(0,0.1f,0), alignRotation);

                // // Collider�� �̿��� ������Ʈ�� �ͷ��� ���� ��Ȯ�� ��ġ�ϵ��� Y ������ ����
                // Collider col = instance.GetComponent<Collider>();
                // if (col != null)
                // {
                //     // collider.bounds.min.y�� ������Ʈ�� ���� ��ǥ�� ������
                //     float colliderBottom = col.bounds.min.y;
                //     // �ͷ��� ǥ�� ���̰� finalPosition.y�� ���Ǿ����Ƿ�,
                //     // �� ���� ���̸� ���� ������Ʈ�� ���� �̵���ŵ�ϴ�.
                //     float yAdjustment = finalPosition.y - colliderBottom;
                //     instance.transform.position += new Vector3(0, yAdjustment/5, 0);
                // }
            }
        }
    }

    // �迭���� ���� ������ ����
    private GameObject RandomPrefab()
    {
        int randomIndex = Random.Range(0, objectPrefab.Length);
        return objectPrefab[randomIndex];
    }
}
