using UnityEngine;

public class HealthControl : MonoBehaviour {

    public int maxHealth;

    [HideInInspector]
    public int currentHealth;
	
	void Start ()
    {
        currentHealth = maxHealth;
	}
	
	void Update ()
    {
		if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log(this.gameObject + " killed");
        }
	}

    public void subtractHealth(int amount)
    {
        currentHealth -= amount;
        Debug.Log(this.gameObject + " minus " + amount + " health");
    }
}
