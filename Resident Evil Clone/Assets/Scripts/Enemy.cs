using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.GetComponent<PlayerController>().TakeDamage(1 * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.GetComponent<PlayerController>().TakeDamage(1 * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Zombie took Damage: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}
