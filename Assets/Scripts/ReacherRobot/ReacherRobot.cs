using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class ReacherRobot : Agent
{

    // ����Ƽ �� ���� ������Ʈ�� �� �κе��� �Ҵ��ϱ� ����. ���� ����
    public GameObject pendulumA;
    public GameObject pendulumB;
    public GameObject pendulumC;
    public GameObject pendulumD;
    public GameObject pendulumE;
    public GameObject pendulumF;

    Rigidbody m_RbA;
    Rigidbody m_RbB;
    Rigidbody m_RbC;
    Rigidbody m_RbD;
    Rigidbody m_RbE;
    Rigidbody m_RbF;

    public GameObject hand;
    public GameObject goal;

    // �� �����̱� ����
    public float m_GoalHeight = 1.2f;
    float m_GoalRadius;     // ���� ������ �� �ִ� ������ �ݰ�
    float m_GoalDegree;     // �ѹ� ������Ʈ �Ҷ� �󸶳� ���� �����ΰ�
    float m_GoalSpeed;      // ���� ���ư��� �ӵ�
    float m_GoalDeviation;  // ���� �ö󰡰� �������� ����
    float m_GoalDeviationFreq;  // �󸶳� ���� �ö󰡰� ������ ���ΰ�.

    public override void Initialize()
    {
        // �� ������Ʈ�� �̸� �߰���Ų Rigidbody ������Ʈ�� �ҷ���.
        m_RbA = pendulumA.GetComponent<Rigidbody>();
        m_RbB = pendulumB.GetComponent<Rigidbody>();
        m_RbC = pendulumC.GetComponent<Rigidbody>();
        m_RbD = pendulumD.GetComponent<Rigidbody>();
        m_RbE = pendulumE.GetComponent<Rigidbody>();
        m_RbF = pendulumF.GetComponent<Rigidbody>();

        SetResetParameters();// Start()�� Initialize()�� ���� ����̴ϱ�. 
    }

    public override void OnEpisodeBegin()
    {
        // �� ���Ǽҵ尡 ó�� �����Ҷ� �� ������Ʈ���� ��ġ �� ȸ��, �ӵ� ���� �� �ʱ�ȭ(�ڽ��� �� ��ġ(����Ƽ��) + �θ��� ��ġ)
        // transform.position : �θ��� ��ġ�� �ݿ��ؾ� ���߿� ���� ���纻�� ���鶧 ��������.
        pendulumA.transform.position = new Vector3(0f, 0.55f, 0f) + transform.position;
        pendulumA.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbA.velocity = Vector3.zero;
        m_RbA.angularVelocity = Vector3.zero;

        pendulumB.transform.position = new Vector3(-0.15f, 0.55f, 0f) + transform.position;
        pendulumB.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbB.velocity = Vector3.zero;
        m_RbB.angularVelocity = Vector3.zero;

        pendulumC.transform.position = new Vector3(-0.15f, 1.375f, 0f) + transform.position;
        pendulumC.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbC.velocity = Vector3.zero;
        m_RbC.angularVelocity = Vector3.zero;

        pendulumD.transform.position = new Vector3(-0.15f, 1.375f, 0f) + transform.position;
        pendulumD.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbD.velocity = Vector3.zero;
        m_RbD.angularVelocity = Vector3.zero;

        pendulumE.transform.position = new Vector3(-0.15f, 2f, 0f) + transform.position;
        pendulumE.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbE.velocity = Vector3.zero;
        m_RbE.angularVelocity = Vector3.zero;

        pendulumF.transform.position = new Vector3(-0.15f, 2.11f, 0f) + transform.position;
        pendulumF.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        m_RbF.velocity = Vector3.zero;
        m_RbF.angularVelocity = Vector3.zero;

        SetResetParameters(); // ���Ǽҵ� �����Ҷ����� ���� ��ġ,�ӵ� ���� ����

        // Start�Ҷ� ���� ��ġ���� ������Ʈ�ϱ�
        m_GoalDegree += m_GoalSpeed; // ���ӵ� ������Ŵ.
        UpdateGoalPosition();
    }

    public void SetResetParameters()
    {
        m_GoalRadius = Random.Range(1f, 1.3f);
        m_GoalDegree = Random.Range(0f, 360f);
        m_GoalSpeed = Random.Range(-2f, 2f);
        m_GoalDeviation = Random.Range(-1f, 1f);
        m_GoalDeviationFreq = Random.Range(0f, 3.14f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // �� �����ؾ��ϴ� �� : 85 space size(13*6 + 7)
        // �� joint ������ ����(��ġ,ȸ��,�ӵ�,���ӵ�)�� ��� sensing �ϴ� �κ�
        sensor.AddObservation(pendulumA.transform.localPosition); //3  // position ��ü�� ��ġ vs localPosition �ش� �θ𿡼��� ��ġ?
        sensor.AddObservation(pendulumA.transform.rotation); //4
        sensor.AddObservation(m_RbA.velocity); //3
        sensor.AddObservation(m_RbA.angularVelocity); //3
        

        sensor.AddObservation(pendulumB.transform.localPosition);
        sensor.AddObservation(pendulumB.transform.rotation);
        sensor.AddObservation(m_RbB.velocity);
        sensor.AddObservation(m_RbB.angularVelocity);

        sensor.AddObservation(pendulumC.transform.localPosition);
        sensor.AddObservation(pendulumC.transform.rotation);
        sensor.AddObservation(m_RbC.velocity);
        sensor.AddObservation(m_RbC.angularVelocity);

        sensor.AddObservation(pendulumD.transform.localPosition);
        sensor.AddObservation(pendulumD.transform.rotation);
        sensor.AddObservation(m_RbD.velocity);
        sensor.AddObservation(m_RbD.angularVelocity);

        sensor.AddObservation(pendulumE.transform.localPosition);
        sensor.AddObservation(pendulumE.transform.rotation);
        sensor.AddObservation(m_RbE.velocity);
        sensor.AddObservation(m_RbE.angularVelocity);

        sensor.AddObservation(pendulumF.transform.localPosition);
        sensor.AddObservation(pendulumF.transform.rotation);
        sensor.AddObservation(m_RbF.velocity);
        sensor.AddObservation(m_RbF.angularVelocity);

        // Ÿ�� ���� ��ġ, ��� ��(effector)�� ��ġ, 
        sensor.AddObservation(goal.transform.localPosition); //3
        sensor.AddObservation(hand.transform.localPosition); //3

        // ���� �ӵ��� sensing�Ѵ�.
        sensor.AddObservation(m_GoalSpeed); // 1

    }
    // action�� ���� ������ ������Ʈ�Ѵ�. 
    public override void OnActionReceived(float[] vectorAction)
    {
        // vectorAction 6�� - �� ����Ʈ 6���� ���Ͽ�.
            // Continuous : Decimal , Discrete : 0 , 1 
        // �� joint�� ȸ����(torque) : �ش� �������� �о��ִ� �� (���� 150f ���� Ű����)
        var torque = Mathf.Clamp(vectorAction[0], -1f, 1f) * 150f;
        m_RbA.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(vectorAction[1], -1f, 1f) * 150f;
        m_RbB.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(vectorAction[2], -1f, 1f) * 150f;
        m_RbC.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(vectorAction[3], -1f, 1f) * 150f;
        m_RbD.AddTorque(new Vector3(0f, torque, 0f));

        torque = Mathf.Clamp(vectorAction[4], -1f, 1f) * 150f;
        m_RbE.AddTorque(new Vector3(0f, 0f, torque));

        torque = Mathf.Clamp(vectorAction[5], -1f, 1f) * 150f;
        m_RbF.AddTorque(new Vector3(0f, torque, 0f));

        // action�� ���������� ������Ʈ �ϴϱ� Update()�Լ��� ���
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
        // ���� �������� �θ��� ��ġ ������ �ݿ��ؾ��ϹǷ� �����ش�.
    }
}
