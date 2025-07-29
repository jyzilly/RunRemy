using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSpawner : MonoBehaviour
{
    public GameObject particleEffectPrefab; // �߰��� ��ƼŬ ������
    public float detectionRadius = 300f; // ����Ʈ�� ������ �Ÿ�
    public LayerMask drivableLayer; //carbody
    private Dictionary<GameObject, GameObject> activeEffects = new Dictionary<GameObject, GameObject>();

    void Update()
    {
        SpawnEffectsOnNearbyObjects();
    }

    void SpawnEffectsOnNearbyObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, drivableLayer);

        HashSet<GameObject> detectedObjects = new HashSet<GameObject>();

        foreach (var col in colliders)
        {
            GameObject target = col.gameObject;
            detectedObjects.Add(target);

            if (!activeEffects.ContainsKey(target)) // ���� ����Ʈ�� ���ٸ� �߰�
            {
                GameObject effectInstance = Instantiate(particleEffectPrefab, target.transform);
                effectInstance.transform.localPosition = Vector3.zero; // �θ� ���� ��ġ ����
                activeEffects[target] = effectInstance;
            }
        }

        // �־��� ������Ʈ�� ����Ʈ ����
        List<GameObject> toRemove = new List<GameObject>();
        foreach (var kvp in activeEffects)
        {
            if (!detectedObjects.Contains(kvp.Key)) // Ž�� ������ ����ٸ� ����
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }

        // ��ųʸ����� ����
        foreach (var obj in toRemove)
        {
            activeEffects.Remove(obj);
        }
    }
}
