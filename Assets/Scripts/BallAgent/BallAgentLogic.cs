using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BallAgentLogic : Agent
{
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform target;

    public override void OnEpisodeBegin()
    {
        // 1. ������Ʈ ��ġ ����Reset agent
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(-9, 0.5f, 0);

        // 2. Ÿ�� ��ġ ����Move target to new random spot (limited spot)
        target.localPosition = new Vector3(12 + Random.value * 8, Random.value * 3, Random.value * 10 - 5);
            // (12~20, 0~3, -5~5)
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent position & Agent velocity
        // ������ ������ �͵� �Է� -> �� 9�� ���� -> Behavior Parameters�� Space Size 9��
        sensor.AddObservation(target.localPosition); // Ÿ���� ������
        sensor.AddObservation(this.transform.localPosition); // agent�� ������
        sensor.AddObservation(rBody.velocity);
    }
    public float speed = 20;
    public override void OnActionReceived(float[] vectorAction)
    {
        /*
            branches size = 2
            branch0 size = 2 ���� : 0, 1    -> vectorAction[0] : 0, 1
                0 : �������� ����. 1 : 1��ŭ �����̵��� (x����)
            branch1 size = 3 ���� : 0, 1, 2 -> vectorAction[1] : 0, 1, 2
                0 : �������� ����. 1 : 1��ŭ �����̰�(z�ݴ����), 2 : 1��ŭ �����̰� (z����)
        */
        // control�� ���� force�� �޾� �����̰Ե�.
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];

        if (vectorAction[1] == 2) controlSignal.z = 1;    // z�������� 1(����)
        else controlSignal.z = -vectorAction[1]; // z�������� -1(������)
        

        // ������ ��������! ���⿡ �ӵ� ���ϱ�!
        if (this.transform.localPosition.x < 8.5f)
        {
            // ��������(8.5)�������� �����̴� �� �����ϵ���!
            rBody.AddForce(controlSignal * speed);
        }

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        // Reached target - Ÿ��� �����ϸ� ���Ǽҵ� ����
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell of platform - �ٴ����� �������� ���Ǽҵ� ����
        if(this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");
    }




}
