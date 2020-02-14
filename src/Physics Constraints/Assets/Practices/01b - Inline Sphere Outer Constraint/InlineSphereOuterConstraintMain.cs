using UnityEngine;

public class InlineSphereOuterConstraintMain : MonoBehaviour
{
    public float radius = 1f;
    public float beta = 0.2f;
    public GameObject ball;

    private Transform m_ballTransform;
    private Vector3 m_velocity;

    private void Awake()
    {
        m_ballTransform = ball?.transform;
    }

    private void FixedUpdate()
    {
        if (m_ballTransform == null)
        {
            return;
        }

        float dt = Time.deltaTime;
        Vector3 pos = m_ballTransform.position;
        m_velocity = Vector3.zero;

        if (pos.magnitude < radius)
        {
            m_velocity = -(beta / dt) * pos.normalized * (pos.magnitude - radius);
        }

        pos += m_velocity * dt;
        m_ballTransform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}
