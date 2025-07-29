using UnityEngine;

public class RandomPlacement : MonoBehaviour
{
    [Header("��ġ ����")]
    [Tooltip("��ġ�� ������ ������Ʈ(��: �÷���, ���)")]
    public GameObject placementSurface;

    [Tooltip("��ġ�� ������Ʈ ������ �迭")]
    public GameObject[] objectPrefab;

    [Tooltip("��ġ�� ������Ʈ ����")]
    public int numberOfObjects = 100;

    void Start()
    {
        if (placementSurface == null || objectPrefab == null || objectPrefab.Length == 0)
        {
            Debug.LogError("placementSurface �Ǵ� objectPrefab�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // placementSurface�� Collider�� �־�� Bounds�� ������ �� ����
        Collider surfaceCollider = placementSurface.GetComponent<Collider>();
        if (surfaceCollider == null)
        {
            Debug.LogError("placementSurface�� Collider ������Ʈ�� �����ϴ�.");
            return;
        }

        // Collider�� Bounds�� �̿��� ��ġ ���� ���� (���� ��ǥ)
        Bounds bounds = surfaceCollider.bounds;

        // ��ġ ǥ���� ��� ����(����)�� placementSurface�� up �������� ����
        Quaternion alignRotation = Quaternion.FromToRotation(Vector3.up, placementSurface.transform.up);

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Bounds ���� ������ x, z ��ǥ ����
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            // y ��ǥ�� Collider�� �ֻ��(bounds.max.y)�� ����ϰ�, �ణ�� �������� �߰�
            Vector3 finalPosition = new Vector3(randomX, bounds.max.y, randomZ) + new Vector3(0, 0.1f, 0);

            // ǥ���� ���⿡ ���� ȸ���ϰ�, �߰��� y���� �������� ���� ȸ�� ����
            Quaternion randomYRotation = Quaternion.Euler(0, Random.Range(-45f, 45f), 0);
            Quaternion finalRotation = alignRotation * randomYRotation;

            // �迭���� ���� ������ ���� �� ����
            GameObject prefab = RandomPrefab();
            Instantiate(prefab, finalPosition, finalRotation);
        }
    }

    // objectPrefab �迭���� ���� ������ ����
    private GameObject RandomPrefab()
    {
        int randomIndex = Random.Range(0, objectPrefab.Length);
        return objectPrefab[randomIndex];
    }
}