using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Catapult _catapult;

    private void OnEnable() =>
        _catapult.OnReloadComplete += SpawnProjectile;

    private void OnDisable() =>
        _catapult.OnReloadComplete -= SpawnProjectile;

    private void SpawnProjectile() =>
        Instantiate(_projectile, _spawnPoint.position, Quaternion.identity);
}