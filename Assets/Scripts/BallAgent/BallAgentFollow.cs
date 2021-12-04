using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAgentFollow : MonoBehaviour
{
    public Transform BallAgentTransform;

    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� ��ġ ����
        _cameraOffset = transform.position - BallAgentTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // �� ������ ���� ī�޶� ������Ʈ�� ���󰡵���.
        transform.position = BallAgentTransform.position + _cameraOffset;

    }
}
