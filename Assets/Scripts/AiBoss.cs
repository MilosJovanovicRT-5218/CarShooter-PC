using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiBoss : MonoBehaviour
{
    public string targetTag = "Player"; // tag ciljane mete
    public float approachDistance = 33f; // udaljenost koja se održava između ovog objekta i ciljne mete
    public float distanceToReturn = 83f; // udaljenost do koje će se ovaj objekat udaljiti od ciljne mete pre nego što se vrati nazad
    public float returnSpeed = 133f; // brzina kretanja dok se ovaj objekat vraća do ciljne mete
    public float rotationSpeed = 83f; // brzina rotacije ovog objekta prema ciljnoj meti
    public float delayTime = 2f; // vreme zadržavanja ovog objekta na ciljnoj poziciji pre nego što se vrati

    private GameObject target; // ciljna meta
    private NavMeshAgent agent; // referenca na NavMeshAgent komponentu
    private float timer; // brojač vremena kada treba da se vrati nazad

    void Start()
    {
        // Pronalaženje ciljne mete po tag-u
        target = GameObject.FindGameObjectWithTag(targetTag);

        // Pronalaženje NavMeshAgent komponente na ovom objektu
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Ako ciljna meta ne postoji, ne radi ništa
        if (target == null)
            return;

        // Izračunaj udaljenost između ovog objekta i ciljne mete
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        // Ako je ovaj objekat unutar udaljenosti za približavanje, stani
        if (distanceToTarget <= approachDistance)
        {
            agent.isStopped = true;

            // Rotiraj ovaj objekat prema ciljnoj meti
            Vector3 direction = target.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            // Pokreni brojač vremena za vraćanje nakon određenog vremena
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                // Resetuj brojač i pokreni kretanje unazad
                timer = 0;
                agent.isStopped = false;
                agent.SetDestination(transform.position + transform.forward * distanceToReturn);
            }
        }
        else // Ako je ovaj objekat izvan udaljenosti za približavanje, kreni prema ciljnoj meti
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
    }
}