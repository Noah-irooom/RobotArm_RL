using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacherGoal_MoveTest : MonoBehaviour
{
    public float m_GoalHeight = 1.2f;

    public GameObject goal;
    float m_GoalRadius;     // ���� ������ �� �ִ� ������ �ݰ�
    float m_GoalDegree;     // �ѹ� ������Ʈ �Ҷ� �󸶳� ���� �����ΰ�
    float m_GoalSpeed;      // ���� ���ư��� �ӵ�
    float m_GoalDeviation;  // ���� �ö󰡰� �������� ����
    float m_GoalDeviationFreq;  // �󸶳� ���� �ö󰡰� ������ ���ΰ�.
    void Start()
    {
        SetResetParameters();
    }

    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(1f, 1.3f);
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-2f, 2f);
        m_GoalDeviation = Random.Range(-1f, 1f);
        m_GoalDeviationFreq = Random.Range(0f, 3.14f);
    }

    void Update()
    {
        m_GoalDegree += m_GoalSpeed; // ���ӵ� ������Ŵ.
        UpdateGoalPosition();
    }

    void UpdateGoalPosition()
    {
        var m_GoalDegree_rad = m_GoalDegree * Mathf.PI / 180f;
        var goalX = m_GoalRadius * Mathf.Cos(m_GoalDegree_rad);
        var goalZ = m_GoalRadius * Mathf.Sin(m_GoalDegree_rad);
        var goalY = m_GoalHeight + m_GoalDeviation * Mathf.Cos(m_GoalDeviationFreq * m_GoalDegree_rad);

        goal.transform.position = new Vector3(goalX, goalY, goalZ) + transform.position;
    }

}
