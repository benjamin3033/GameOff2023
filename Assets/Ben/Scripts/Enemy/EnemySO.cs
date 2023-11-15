using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class EnemySO : ScriptableObject
{
    public GameObject Visual;
    public float VisualHeight;
    public Vector3 VisualRotation;

    public int NavMeshAgentType;

    [Header("Steering")]
    public float Speed = 3.5f;
    public float AngularSpeed = 120f;
    public float Acceleration = 8;
    public float StoppingDistance = 0;
    public bool AutoBraking = true;

    [Header("Obstacle Avoidance")]
    public float Radius = 0.5f;
    public float Height = 2f;
    public ObstacleAvoidanceType Quality = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    public int Priority = 50;
}
