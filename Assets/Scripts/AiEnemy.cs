using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    public string targetTag = "Player"; // Tag cilja
    private GameObject targetObject; // Objekt koji ima ciljni tag
    private NavMeshAgent agent; // NavMesh agent

    private Rigidbody rb;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindTarget();
    }

    void Update()
    {
        if (targetObject != null)
        {
            agent.SetDestination(targetObject.transform.position);
        }
        else
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetTag);
        if (possibleTargets.Length > 0)
        {
            targetObject = possibleTargets[Random.Range(0, possibleTargets.Length)];
        }
    }
}
