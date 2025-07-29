using UnityEngine;

public class OutDesert : MonoBehaviour
{
    public delegate void InDesertDelegate();
    public event InDesertDelegate InDesertEvent;

    // ���� ���, "Target" �±׸� ���� ������Ʈ�� Ʈ���ŵǾ��� �� ����˴ϴ�.
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�縷 ���� Ʈ���� �߻�");
        // Ư�� ������Ʈ�� �±׷� ���͸� (�ʿ信 ���� ������ �����ϼ���)
        Debug.Log("Ʈ���ŵ� Ż�� ���̾� : " + other.gameObject.layer);
        Debug.Log("carbody ���̾� : " + LayerMask.NameToLayer("carbody"));
        if (other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            Debug.Log("Ʈ���ŵ� ���� ���̾� ã��");
            // "Body"��� �̸��� �ڽ� ������Ʈ�� ã���ϴ�.
            Transform playerTransform = FindChildRecursive(other.transform, "Remy Ragdoll");
            Transform bodyTransform = FindChildRecursive(other.transform, "Mesh");
            Debug.Log("playerTransform : " + playerTransform);
            Debug.Log("���ε尡 �����");
            if (playerTransform != null)
            {
            }
            else
            {
                Debug.LogWarning("�縷�� ����");
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
