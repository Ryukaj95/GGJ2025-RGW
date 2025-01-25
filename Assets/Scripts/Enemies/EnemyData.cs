using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public float speed = 1f;
    [SerializeField] public int health = 1;
    [SerializeField] public GameObject bullet;
}
