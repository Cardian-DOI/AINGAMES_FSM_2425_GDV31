using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float speed = 50.0f;
    [SerializeField]
    private float lifeTime = 3.0f;
    [SerializeField]
    private int damage = 50;
    private void Start()
    {
        // Destroy bullet after lifetime expires
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move bullet forward constantly
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Deal damage to any object with a TankHealth component
        TankHealth health = collision.gameObject.GetComponent<TankHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Spawn explosion effect
        ContactPoint contact = collision.contacts[0];
        Instantiate(explosion, contact.point, Quaternion.identity);

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}
