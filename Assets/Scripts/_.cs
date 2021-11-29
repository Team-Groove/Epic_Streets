using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Hitbox : MonoBehaviour, IHitDetector
{
    [SerializeField] private BoxCollider m_collider;
    [SerializeField] private LayerMask m_layermask;

    private float m_thickness = 0.025f;
    private IHitResponder m_hitResponder;
    
    public IHitResponder HitResponder { get => m_hitResponder; set => m_hitResponder = value; }

    public void CheckHit()
    {
        Vector3 _scaledSize = new Vector3(
        m_collider.size.x * transform.lossyScale.x,
        m_collider.size.x * transform.lossyScale.y,
        m_collider.size.x * transform.lossyScale.z);

        float _distance = _scaledSize.y - m_thickness;
        Vector3 _direction = transform.up;
        Vector3 _center = transform.TransformPoint(m_collider.center);
        Vector3 _start = _center - _direction * (_distance / 2);
        Vector3 _halfExtents = new Vector3(_scaledSize.x, m_thickness, _scaledSize.z) / 2;
        Quaternion _orientation = transform.rotation;
    }
}
