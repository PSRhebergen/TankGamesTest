using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float speed;
    public ParticleSystem particle;

    [HideInInspector]
    public GameObject shooter;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != shooter)
        {
            Destroy(this.gameObject);
            Instantiate(particle, transform.position, transform.rotation);
        }    
    }
}