using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script handles the assignment of map boundaries 
public class CameraBounds : MonoBehaviour
{
    public CinemachineConfiner2D cinemachine;
    public void AssignCameraBounds(GameObject mapInstance)
    {
        Collider2D mapBounds = mapInstance.GetComponentInChildren<PolygonCollider2D>();
        cinemachine.m_BoundingShape2D = mapBounds; //assign map boundary to cinemachine to stop camera from moving out of map
    }
}
