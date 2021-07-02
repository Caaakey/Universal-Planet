using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera m_Camera;
    private Vector3 m_ResetPosition;
    private Quaternion m_ResetRotation;

    private CameraController m_Controller = null;
    private Transform m_PlanetTransform = null;
    private float m_ZoomScale = 0;
    private bool m_IsUpdate = false;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
        m_Controller = GetComponent<CameraController>();
    }

    private void Update()
    {
        if (m_PlanetTransform == null) return;
        else
        {
            transform.position = new Vector3(
            m_PlanetTransform.position.x,
            m_PlanetTransform.position.y,
            m_PlanetTransform.position.z - m_ZoomScale
            );

            if (Input.GetKeyDown(KeyCode.Mouse1)) ResetCamera();
        }
    }

    public void ZoomPlanet(Transform target, float zoomScale)
    {
        if (m_PlanetTransform != null || m_IsUpdate) return;

        StartCoroutine(UpdateSmoothCamera(target, zoomScale));
    }

    public void ResetCamera()
    {
        if (m_PlanetTransform == null || m_IsUpdate) return;

        m_PlanetTransform = null;
        StartCoroutine(UpdateResetCamera());
    }

    private const float SmoothTimeSecond = 1f;
    private IEnumerator UpdateSmoothCamera(Transform planetTransform, float zoomScale)
    {
        m_IsUpdate = true;
        m_Controller.enabled = false;

        m_ResetPosition = transform.position;
        m_ResetRotation = transform.rotation;

        float currTime = Time.time;
        float fixedTime = Time.time + SmoothTimeSecond;
        Vector3 prevPosition = transform.position;
        Quaternion prevRotation = transform.rotation;

        while (Time.time <= fixedTime)
        {
            float time = (Time.time - currTime) / SmoothTimeSecond;

            Vector3 newPosition = new Vector3(
                Mathf.Lerp(prevPosition.x, planetTransform.position.x, time),
                Mathf.Lerp(prevPosition.y, planetTransform.position.y, time),
                Mathf.Lerp(prevPosition.z, planetTransform.position.z - zoomScale, time)
                );

            Vector3 direction = planetTransform.position - newPosition;
            Quaternion newRotation = Quaternion.LookRotation(direction);

            //  transform.position = newPosition;
            //  transform.rotation = Quaternion.Lerp(prevRotation, newRotation, time);
            transform.SetPositionAndRotation(newPosition, Quaternion.Lerp(prevRotation, newRotation, time));
            yield return null;
        }

        transform.SetPositionAndRotation(new Vector3(
            planetTransform.position.x,
            planetTransform.position.y,
            planetTransform.position.z - zoomScale
            ),
            Quaternion.LookRotation(planetTransform.position - transform.position));

        m_PlanetTransform = planetTransform.transform;
        m_ZoomScale = zoomScale;
        m_IsUpdate = false;

        m_Controller.PlanetTransform = planetTransform;
        m_Controller.enabled = true;

        yield break;
    }

    private const float ReleaseTimeSecond = 1f;
    private IEnumerator UpdateResetCamera()
    {
        m_IsUpdate = true;
        m_Controller.enabled = false;

        float currTime = Time.time;
        float fixedTime = Time.time + ReleaseTimeSecond;
        Vector3 prevPosition = transform.position;
        Quaternion prevRotation = transform.rotation;

        while (Time.time <= fixedTime)
        {
            float time = (Time.time - currTime) / ReleaseTimeSecond;

            Vector3 newPosition = Vector3.Lerp(prevPosition, m_ResetPosition, time);
            Quaternion newRotation = Quaternion.Lerp(prevRotation, m_ResetRotation, time);

            transform.SetPositionAndRotation(newPosition, newRotation);
            yield return null;
        }

        transform.SetPositionAndRotation(m_ResetPosition, m_ResetRotation);
        m_IsUpdate = false;

        m_Controller.PlanetTransform = null;
        m_Controller.enabled = true;

        yield break;
    }

}
