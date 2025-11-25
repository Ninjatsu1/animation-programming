using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Scriptable Objects/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public string CharacterName = "Name";
    public float MaximumHealth = 10f;
    public float CurrentHealth = 1f;
    public float MaximumMana = 1.0f;
    public GameObject CharacterPrefab;
}
