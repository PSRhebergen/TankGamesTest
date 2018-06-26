using UnityEngine;

public class BulletControl : MonoBehaviour {

    public ParticleSystem particle;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Instantiate(particle, transform.position, transform.rotation);
        }    
    }
}