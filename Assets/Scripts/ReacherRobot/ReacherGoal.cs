using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacherGoal : MonoBehaviour
{
    // ���� effector(hand)�� trigger�ϱ� ���ؼ� box collider ������Ʈ �߰�
    // hand�� goal�� ��Ҵ��� üũ
    public GameObject agent;
    public GameObject hand;
    public GameObject goalOn;

    // ���� goal�� sphere collider�� Is Trigger �� üũ�ؾ���.
    // �ٸ� collider(Trigger)�� �ش� collider�� �浹�ߴ����� �˻��ϴ� ��.
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� hand��� GoalOn ������Ʈ(�Ķ�)�� ũ�� �����
        if (other.gameObject == hand)
        {
            // 0.95f -> 1.05f
            goalOn.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        }
    }

    // Trigger�� �ش� ������Ʈ�� ���� ������
    void OnTriggerExit(Collider other)
    {
        // ���� trigger�� hand��� GoalOn�� ũ�� ���̱�.
        if(other.gameObject == hand)
        {
            // 1.05f -> 0.95f
            goalOn.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        }
    }

    // Trigger�� �ش� ������Ʈ�� ��� �ִٸ� ������ �ش�.
    void OnTriggerStay(Collider other)
    {
        // ���� trigger�� hand��� GoalOn�� ũ�� ���̱�.
        if (other.gameObject == hand)
        {
            // Trigger(hand)�� Goal�� �Ź� ���� ���� ������ �ش�.
            agent.GetComponent<ReacherRobot>().AddReward(0.01f);
        }
    }
}
