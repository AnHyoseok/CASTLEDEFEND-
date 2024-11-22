using System.Collections.Generic;
using UnityEngine;

/*
�⺻Ÿ�� => Ÿ�ٰ���
���÷���Ÿ�� => ��������
��Ƽ����Ÿ�� => Ÿ�ٰ���
������Ÿ�� => Ÿ�ٰ���
���ο�Ÿ�� => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
��ȭŸ��   => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
���Ÿ��   => ���� ������ ���� �ʰ� ���� ������ �ش� ȿ�� ����
*/
/// <summary>
/// Tower�� �������� ������ ������ ���� Ŭ����
/// </summary>
public abstract class TowerBase : MonoBehaviour
{
    #region Variables
    [SerializeField] List<Transform> targets;           // �ٶ� Ÿ�� ������Ʈ
    [SerializeField] private float rotationSpeed = 5f;  // ȸ�� �ӵ�
    [SerializeField] protected float attackRange = 5f;  // ���� ��Ÿ�

    public Transform currentTarget;                     // ���� ���� ����� Ÿ��
    public LayerMask targetLayer;                       // Ÿ�� ������Ʈ�� ���̾�
    #endregion

    #region Variables For Test
    public Color gizmoColor = Color.green;              // ����� ����
    public float sphereRadius;                          // ���� ������
    public float lineLength = 10f;                      // ������ ����

    LineRenderer lineRenderer;                          // ���� ������
    #endregion

    protected virtual void Start()
    {
        // Layer ����
        targetLayer = LayerMask.GetMask(Constants.enemyLayer);

        // ���� �ֱ�� Ÿ�� Ž��
        InvokeRepeating(nameof(SetClosestTarget), 0f, 0.5f);

        #region Test�� ���� �ð�ȭ => LineRenderer �ʱ�ȭ, Gizmo
        {
            // ����� ���� �ʱ�ȭ
            sphereRadius = attackRange;

            // LineRenderer ������Ʈ�� �������ų� �߰�
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
                lineRenderer = gameObject.AddComponent<LineRenderer>();

            // LineRenderer �ʱ� ����
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2; // �������� ����
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ���̴�
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        #endregion
    }

    protected virtual void Update()
    {
        SetRotationToTarget(); // �� �����Ӹ��� Ÿ���� �ٶ󺸵��� ȸ��

        // TEST
        DrawLine();            // Ÿ�� �������� ���� �׸���
    }

    // Ÿ���� �������� ȸ��
    protected virtual void SetRotationToTarget()
    {
        if (currentTarget != null)
        {
            // Ÿ�� ����
            Vector3 targetPosition = currentTarget.position;

            // ���� ������Ʈ���� Ÿ���� ���ϴ� ���� ���
            Vector3 direction = targetPosition - transform.position;

            // Ÿ���� �ٶ󺸴� ȸ�� ���
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ������ ȸ�� (Slerp ���)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // ���� ���� Ÿ���� ����
    List<Transform> UpdateTargets()
    {
        // ���� Ÿ�� �ʱ�ȭ
        targets.Clear();

        // ��ȯ�� Ÿ�ٵ�
        List<Transform> tempTarget = new List<Transform>();

        // ���� ���� �� Enemy Layer Ž��
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

        foreach (var collider in colliders)
        {
            // Ÿ���� Transform �߰�
            tempTarget.Add(collider.transform);
        }

        return tempTarget;
    }

    // ���� ����� Ÿ�� ����
    void SetClosestTarget()
    {
        // Enemy �޾ƿ���
        targets = UpdateTargets();

        // ���� ����� Ÿ�� ã��
        {
            float closestDistance = Mathf.Infinity;
            Transform closestTarget = null;

            foreach (var target in targets)
            {
                if (target == null) continue;

                float distance = Vector3.Distance(transform.position, target.position);

                // Ÿ���� ���� ���� ���� �ְ�, ���� ����� Ÿ������ Ȯ��
                if (distance <= attackRange && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
            currentTarget = closestTarget;
        }
    }

    // ���ݹ��� ���� ������
    private void DrawLine()
    {
        // ������ (������Ʈ�� ���� ��ġ)
        lineRenderer.SetPosition(0, transform.position);

        // ���� (������Ʈ�� ���� �������� lineLength��ŭ ������ ��ġ)
        Vector3 endPosition = transform.position + transform.forward * lineLength;
        lineRenderer.SetPosition(1, endPosition);
    }

    // ���ݹ��� �����
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
