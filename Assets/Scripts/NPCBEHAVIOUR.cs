using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBEHAVIOUR : MonoBehaviour
{
    [SerializeField] List<AudioClip> moans;
    [SerializeField] AudioSource sourceMoan;

    [SerializeField] GameObject bloodPref;
    [SerializeField] float distance;

    bool isDead = false;
    [SerializeField] Transform player;
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    [SerializeField] Transform currentPatrolDestination;
    [SerializeField] List<Transform> patrolPositions;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        currentPatrolDestination = patrolPositions[Random.Range(0, patrolPositions.Count)];
        _navMeshAgent.SetDestination(currentPatrolDestination.position);
    }
    private void Update()
    {
        _animator.SetFloat("Move", _navMeshAgent.velocity.magnitude / 20);
        if (_navMeshAgent.remainingDistance < 1)
        {
            currentPatrolDestination = patrolPositions[Random.Range(0, patrolPositions.Count)];
            _navMeshAgent.SetDestination(currentPatrolDestination.position);
        }
        if (Vector3.Distance(transform.position, player.position) < distance && !isDead)
        {
            isDead = true;
            Debug.Log("Skibidi pap yeyeyes");
            StartCoroutine(Die());
        }
    }
    private void DieMoan()
    {
        sourceMoan.clip = moans[Random.Range(0, moans.Count)];
        sourceMoan.PlayOneShot(sourceMoan.clip);
    }
    IEnumerator Die()
    {
        Spawn.Instance.Died();
        DieMoan();
        Instantiate(bloodPref, transform.position, Quaternion.identity);
        _animator.enabled = false;
        _navMeshAgent.enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + player.transform.position.normalized * distance);
        }
    }
}
