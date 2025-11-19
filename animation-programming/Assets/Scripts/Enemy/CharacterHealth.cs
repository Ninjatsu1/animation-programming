using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private CharacterStats _characterStats
        ;
    [SerializeField] float _currentHealth = 1f;

    private void Awake()
    {
        _currentHealth = _characterStats.MaximumHealth;
    }


   public void DamageHealth(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Despawn();
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false); //Not sure if object should be destroyed. Might changed later if enemies spawns
    }
}
