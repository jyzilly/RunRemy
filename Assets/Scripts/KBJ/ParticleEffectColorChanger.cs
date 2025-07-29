using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectColorChanger : MonoBehaviour
{
    public float SelectRadius = 30f;
    public Color highlightColor = Color.green;
    public LayerMask particleLayer;

    private GameObject closestParticlePrefab = null;
    private Dictionary<ParticleSystem, Color> lastOriginalColors = new Dictionary<ParticleSystem, Color>();

    void Update()
    {
        FindAndHighlightClosestParticle();
    }

    void FindAndHighlightClosestParticle()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, SelectRadius, particleLayer);
        GameObject nearestPrefab = null;
        float minDistance = SelectRadius;

        foreach (var col in colliders)
        {
            GameObject Prefab = col.transform.gameObject;
            float distance = Vector3.Distance(transform.position, Prefab.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPrefab = Prefab;
            }
        }

        if (nearestPrefab != closestParticlePrefab)
        {
            ResetPreviousParticles(); // ���� ���� ����
            closestParticlePrefab = nearestPrefab;

            if (closestParticlePrefab != null)
            {
                StoreOriginalColors(closestParticlePrefab);
                ChangeParticlesColor(closestParticlePrefab, highlightColor);
            }
        }
    }

    void StoreOriginalColors(GameObject prefab)
    {
        lastOriginalColors.Clear(); // ���� ����� ���� ���� �ʱ�ȭ
        foreach (var ps in prefab.GetComponentsInChildren<ParticleSystem>())
        {
            lastOriginalColors[ps] = ps.main.startColor.color; // ���� ������ ����
        }
    }

    void ResetPreviousParticles()
    {
        if (lastOriginalColors.Count == 0) return; // ���� ������ ������� �ʾҴٸ� �н�

        foreach (var kvp in lastOriginalColors)
        {
            if (kvp.Key != null) // ParticleSystem�� ������ �����ϴ��� Ȯ��
            {
                SetParticleColor(kvp.Key, kvp.Value); // ���� ���� ����
            }
        }
        lastOriginalColors.Clear(); // ���� �� �ʱ�ȭ
    }

    void ChangeParticlesColor(GameObject prefab, Color color)
    {
        foreach (var ps in prefab.GetComponentsInChildren<ParticleSystem>())
        {
            SetParticleColor(ps, color);
        }
    }

    void SetParticleColor(ParticleSystem ps, Color color)
    {
        var main = ps.main;
        main.startColor = color;

        // ���� ��ƼŬ���� ������ ��� ����
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        int count = ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            particles[i].startColor = color;
        }

        ps.SetParticles(particles, count); // ���� ���� ����

        ps.Clear();
        ps.Play();
    }

}
