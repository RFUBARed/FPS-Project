using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public int health = 100; // Player starting health

    public AudioClip hitSFX;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            DecreaseHealth(10); // Lose 10 health per hit
        }
    }
    private void DecreaseHealth(int amount)
    {
        health -= amount;     // Reduce health
        PlayerLook.Instance.AddShake(0.1f, 0.25f);
        UI_Manager.Instance.InstantiateHitUI();
        AudioManager.Instance.PlaySFX(hitSFX);

        CheckDeath();         // Check if player is dead
    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Time.timeScale = 0f;  // Pause the game
    }
}
