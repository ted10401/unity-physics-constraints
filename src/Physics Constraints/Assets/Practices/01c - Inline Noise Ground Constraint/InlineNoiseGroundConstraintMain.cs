using UnityEngine;

public class InlineNoiseGroundConstraintMain : MonoBehaviour
{
    public float a = 1;
    public float b = 1;
    public float c = 1;
    public Vector2Int gizmoRange = new Vector2Int(-100, 100);

    public Vector3 gravity = new Vector3(0f, -9.8f, 0f);
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
        float fx = GetFx(pos.x);
        float fxDiff = GetFxDiff(pos.x);
        m_velocity += gravity * dt;

        if (pos.y <= fx || Mathf.Abs(fxDiff) != 0)
        {
            m_velocity.x += gravity.y * fxDiff * dt;
            m_velocity.y = -(beta / dt) * (pos.y - fx);
        }

        pos += m_velocity * dt;
        m_ballTransform.position = pos;
    }

    private float GetFx(float x)
    {
        return a * Mathf.Cos(b * x) + c * x * x;
    }

    private float GetFxDiff(float x)
    {
        return -a * Mathf.Sin(b * x) + 2 * c * x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(float i = gizmoRange.x; i < gizmoRange.y; i += 0.1f)
        {
            Gizmos.DrawLine(new Vector3(i, GetFx(i), 0), new Vector3(i + 0.1f, GetFx(i + 0.1f), 0));
        }
    }
}
