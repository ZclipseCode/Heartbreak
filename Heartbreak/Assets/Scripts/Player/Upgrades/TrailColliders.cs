using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColliders : MonoBehaviour
{
    // make it so when combining with a collider, enemy freezes
    [SerializeField] GameObject colliderPrefab;
    float interval = 0.05f; // interval at which to place colliders
    TrailRenderer trailRenderer;
    List<Vector3> points = new List<Vector3>();
    List<GameObject> colliders = new List<GameObject>();

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        InvokeRepeating(nameof(GenerateColliders), 0f, interval);
    }

    void GenerateColliders()
    {
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], transform.position) > interval)
        {
            points.Add(transform.position);
            CreateCollider(points[points.Count - 1]);
        }
    }

    void CreateCollider(Vector3 position)
    {
        GameObject newCollider = Instantiate(colliderPrefab, position, Quaternion.identity);
        colliders.Add(newCollider);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            Destroy(colliders[i]);
        }
    }
}