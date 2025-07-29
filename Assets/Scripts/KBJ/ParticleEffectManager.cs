using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    // ���� �ݰ� �� ���� �ݰ� ����
    public float selectRadius = 50f;
    public float detectionRadius = 300f;
    public Color highlightColor = Color.green;
    public GameObject particleEffectPrefab;
    public LayerMask targetLayer;

    private GameObject closestTarget = null;
    private Dictionary<GameObject, Dictionary<ParticleSystem, Color>> originalColors = new Dictionary<GameObject, Dictionary<ParticleSystem, Color>>();
    private Dictionary<GameObject, GameObject> activeEffects = new Dictionary<GameObject, GameObject>();

    void Update()
    {
        // ���� �ݰ� ���� ��� ������Ʈ�� �˻�
        Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Max(selectRadius, detectionRadius), targetLayer);
        ProcessDetectedObjects(colliders);
    }

    void ProcessDetectedObjects(Collider[] colliders)
    {
        GameObject nearest = null;
        float minDistanceSqr = selectRadius * selectRadius;
        List<GameObject> detectedObjects = new List<GameObject>();

        // ������ ��� ������Ʈ�� ��ȸ
        foreach (var col in colliders)
        {
            GameObject target = col.gameObject;
            detectedObjects.Add(target);

            // ���� ������Ʈ���� �Ÿ� ���
            float distanceSqr = (transform.position - target.transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                nearest = target;
            }

            // ��ƼŬ ȿ���� ������ ���� ����
            if (!activeEffects.ContainsKey(target))
            {
                GameObject effectInstance = Instantiate(particleEffectPrefab, target.transform);
                effectInstance.transform.localPosition = Vector3.zero;
                activeEffects[target] = effectInstance;
            }
        }

        // ���� ����� ����� ����� ��� ó��
        if (nearest != closestTarget)
        {
            // ���� ����� ���� ���� ����
            if (closestTarget != null)
            {
                ResetOriginalColors(closestTarget);
            }

            closestTarget = nearest;

            // �� ����� ���� ����
            if (closestTarget != null)
            {
                StoreOriginalColors(closestTarget);
                ChangeParticleColor(closestTarget, highlightColor);
            }
        }

        // �������� ���� ������Ʈ�� ��ƼŬ ȿ�� ����
        List<GameObject> toRemove = new List<GameObject>();
        foreach (var kvp in activeEffects)
        {
            if (!detectedObjects.Contains(kvp.Key))
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }

        // ���ŵ� ������Ʈ ��Ͽ��� ����
        foreach (var obj in toRemove)
        {
            activeEffects.Remove(obj);
        }
    }

    // ���� ���� ����
    void StoreOriginalColors(GameObject target)
    {
        if (!originalColors.ContainsKey(target))
        {
            originalColors[target] = new Dictionary<ParticleSystem, Color>();
            foreach (var ps in target.GetComponentsInChildren<ParticleSystem>())
            {
                originalColors[target][ps] = ps.main.startColor.color;
            }
        }
    }

    // ���� ���� ����
    void ResetOriginalColors(GameObject target)
    {
        if (originalColors.TryGetValue(target, out var colors))
        {
            foreach (var kvp in colors)
            {
                if (kvp.Key != null)
                {
                    SetParticleColor(kvp.Key, kvp.Value);
                }
            }
            originalColors.Remove(target);
        }
    }

    // ��ƼŬ ���� ����
    void ChangeParticleColor(GameObject target, Color color)
    {
        foreach (var ps in target.GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.main.startColor.color != color)
            {
                SetParticleColor(ps, color);
            }
        }
    }

    // Ư�� ��ƼŬ �ý����� ������ ����
    void SetParticleColor(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color);

        // ���� ����� ��� ��ƼŬ ��������
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        int count = ps.GetParticles(particles);

        // ��� ��ƼŬ�� ������ ����
        for (int i = 0; i < count; i++)
        {
            particles[i].startColor = color;
        }

        // ����� ������ ����
        ps.SetParticles(particles, count);

        // ���� ��ƼŬ�� ����� ���� �����Ͽ� ��� �ݿ�
        ps.Clear();
        ps.Play();
    }
}
