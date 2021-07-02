using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    [Header("SkyBox")]
    public Material Material;
    public float RotateSpeed;
    private float m_RotateValue = 0;

    // Update is called once per frame
    void Update()
    {
        m_RotateValue = m_RotateValue + (RotateSpeed * Time.deltaTime);
        if (m_RotateValue >= 360f) m_RotateValue -= 360f;

        Material.SetFloat("_Rotation", m_RotateValue);
    }
}
