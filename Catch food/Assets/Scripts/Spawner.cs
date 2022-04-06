using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _starPrefabs;//массив префабов для спавна
    [SerializeField] private Transform[] _spawnPoints;//массив точек спавна
    [SerializeField] private float _spawnDelay;//частота спавна

    private void Start()
    {
        StartCoroutine(Spawn());//Запускаем корутину с самого начала
    }

    IEnumerator Spawn()
    {
        while(true)//цикл с случайным выпадением предметов из массивов с периодом во времени
        {
            yield return new WaitForSeconds(_spawnDelay);
            int randomPrefab = Random.Range(0, _starPrefabs.Length);
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);

            Instantiate(_starPrefabs[randomPrefab], _spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }

}
