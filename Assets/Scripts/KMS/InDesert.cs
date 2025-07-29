using ArcadeVP;
using UnityEngine;

public class InDesert : MonoBehaviour
{
    public delegate void InDesertDelegate();
     public event InDesertDelegate InDesertEvent;

    // ���� ���, "Target" �±׸� ���� ������Ʈ�� Ʈ���ŵǾ��� �� ����˴ϴ�.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�縷 ���� Ʈ���� �߻�");
        // Ư�� ������Ʈ�� �±׷� ���͸� (�ʿ信 ���� ������ �����ϼ���)
        Debug.Log("Ʈ���ŵ� Ż�� ���̾� : " + other.gameObject.layer);
        Debug.Log("carbody ���̾� : " + LayerMask.NameToLayer("carbody"));
        if (other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            Debug.Log("Ʈ���ŵ� ���� ���̾� ã��");
            Debug.Log("other : " + other);
            // "Body"��� �̸��� �ڽ� ������Ʈ�� ã���ϴ�.
            var vehicle = other.GetComponentInParent<ArcadeVehicleController>();
            var bodyTransform = FindChildRecursive(other.transform, "Mesh");
            Debug.Log("bodyTransform : ", bodyTransform);
            Debug.Log("vehicle : ", vehicle);
            Debug.Log("���ε尡 �����");
            if (vehicle != null && bodyTransform != null)
            {
                vehicle.InDesert = true;
                //vehicle.BodyTr = bodyTransform;
            }
            else
            {
                Debug.LogWarning("�̹� InDesert Ʈ���Ű� �ߵ���");
            }
        }
    }

    Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform found = FindChildRecursive(child, childName);
            if (found != null)
                return found;
        }
        return null;
    }
}
