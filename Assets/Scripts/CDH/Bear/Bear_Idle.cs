using UnityEngine;

public class AnimationLoopCounter : MonoBehaviour
{
    public Animator animator;
    public int loopPunchCount = 0;
    public int loopDanceCount = 0;
    public int maxLoops = 3;
    public int minLoop = 1;

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �Լ�
    void PunchCount()
    {
        loopDanceCount = 0;
        animator.SetBool("isDanceEnd", false);
        ++loopPunchCount;
        Debug.Log(loopPunchCount);

        if (loopPunchCount >= maxLoops)
        {
            Debug.Log("PunchEnd!");
            // �ٸ� �ִϸ��̼����� ��ȯ
            animator.SetBool("isPunchEnd", true);
        }
    }

    void DanceCount()
    {
        loopPunchCount = 0;
        animator.SetBool("isPunchEnd", false);
        ++loopDanceCount;
        if (loopDanceCount >= minLoop)
        {
            animator.SetBool("isDanceEnd", true);
        }
    }
}
