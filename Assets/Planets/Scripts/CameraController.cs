using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float m_RotateSpeed = 20.0f;
    public float m_ZoomSpeed = 1.0f;
    private Vector3 m_StartMousePosition = Vector3.zero;

    public Transform PlanetTransform = null;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                m_StartMousePosition = Input.mousePosition;
            else
            {
                Vector3 newAxis = (m_StartMousePosition - Input.mousePosition).normalized;
                transform.RotateAround(
                    PlanetTransform != null ? PlanetTransform.position : Vector3.zero,
                    new Vector3(newAxis.y, -newAxis.x),
                    m_RotateSpeed * Time.deltaTime);

                m_StartMousePosition = Input.mousePosition;
            }
        }

    }

}
