using UnityEngine;

public class BulletTargetBehaviour : MonoBehaviour
{
    public float health = 50f;
    
    public void TakeDamage(float amount)
    {
        this.health -= amount;

        if (this.health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Target is dead.");
        Destroy(gameObject);
    }
}
