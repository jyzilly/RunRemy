using UnityEngine;

public interface IBoxCastFinder
{
    /// <summary>
    /// ������ ī�޶�� �Ķ���͸� ����� BoxCast�� �����ϰ�,
    /// ����Ʈ �߾ӿ� ���� ����� ��Ʈ ������Ʈ�� ��ȯ�մϴ�.
    /// </summary>
    GameObject GetCenterBoxCastHit(Camera cam, Vector3 originOffset, Vector3 halfExtents, float maxDistance, LayerMask layerMask);
}
