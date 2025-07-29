using System.Linq;
using UnityEngine;

public class DrawOutline : MonoBehaviour
{
    public float moveSpeed = 5f;  // �̵� �ӵ�
    public float detectionRadius = 5f; // ���� ����
    public LayerMask layer;

    private GameObject closestObject;
    private GameObject previousClosestObject;

    void FixedUpdate()
    {
        //MovePlayer();
        DetectClosestObject();
    }

    //void MovePlayer()
    //{
    //    float moveX = Input.GetAxisRaw("Horizontal");  // A, D (����, ������)
    //    float moveZ = Input.GetAxisRaw("Vertical");    // W, S (��, �Ʒ�)

    //    Vector3 move = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed * Time.deltaTime;
    //    if (move != Vector3.zero)
    //    {
    //        transform.Translate(move, Space.World);
    //    }
    //}

    //void DetectClosestObject()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, layer);

    //    GameObject newClosestObject = colliders
    //        .Select(c => c.gameObject)
    //        .OrderBy(go => (go.transform.position - transform.position).sqrMagnitude)
    //        .FirstOrDefault();

    //    if (newClosestObject != closestObject)
    //    {
    //        UpdateOutlineEffect(newClosestObject);
    //        closestObject = newClosestObject;
    //    }
    //}

    public void DetectClosestObject()
    {
        // Physics.OverlapSphere�� �ֺ��� �ݶ��̴����� ã�� ��,
        // 1. �÷��̾� ���ʿ� �ִ��� (Dot product >= 0) üũ
        // 2. InteractableObject ������Ʈ�� �����ϴ���
        // 3. currentInteractableObject Ȥ�� currentObjectPrefab�� �ߺ����� �ʴ��� üũ�� ��
        // 4. ���� ����� ������ ������ ù ��° ��Ҹ� �����մϴ�.
        GameObject newClosestObject = Physics.OverlapSphere(transform.position, detectionRadius, layer)
            .Where(c => Vector3.Dot(transform.forward, (c.transform.position - transform.position).normalized) >= 0)
            .Select(c => c.gameObject)
            .OrderBy(io => (io.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (newClosestObject != closestObject)
        {
            UpdateOutlineEffect(newClosestObject);
            closestObject = newClosestObject;
        }
    }

    void UpdateOutlineEffect(GameObject newObject)
    {
        if (previousClosestObject != null)
        {
            RemoveOutline(previousClosestObject);
        }

        if (newObject != null)
        {
            AddOutline(newObject);
        }

        previousClosestObject = newObject;
    }

    void AddOutline(GameObject obj)
    {
        if (!obj.TryGetComponent(out Outline outline))
        {
            outline = obj.AddComponent<Outline>();
        }

        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 15f;
    }

    void RemoveOutline(GameObject obj)
    {
        if (obj.TryGetComponent(out Outline outline))
        {
            //Destroy(outline);
            outline.enabled = false;
        }
    }

    // ����׿����� ���� ���� �ð�ȭ
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
