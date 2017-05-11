using UnityEngine;

public class Target : MonoBehaviour {

	private float health = 100f;

    public void TakeDamage(float value)
    {
        health -= value;

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
