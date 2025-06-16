using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] Deck deck;
    public Deck Deck => deck;
    [SerializeField] int maxHealth;
    int currentHealth;
    [SerializeField] Sprite sprite;
    public Sprite Sprite => sprite;

    public float HealthPrecentage => (float)currentHealth / (float)maxHealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
    }
}
