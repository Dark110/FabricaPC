using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabConProbabilidad
    {
        public GameObject prefab;
        [Range(0f, 100f)] public float probabilidad;
    }

    [Header("Prefabs y Probabilidades")]
    public List<PrefabConProbabilidad> prefabsConProbabilidades = new List<PrefabConProbabilidad>();

    [Header("Configuración de Spawneo")]
    public int cantidadASpawnear = 10;
    public Transform puntoDeSpawneo;
    public float intervaloSpawn = 3f;

    private int cantidadSpawneada = 0;

    void Start()
    {
        if (ValidarProbabilidades())
        {
            StartCoroutine(SpawnearPrefabsConDelay());
        }
        else
        {
            Debug.LogError("Las probabilidades no suman 100%. Corrige eso en el inspector.");
        }
    }

    bool ValidarProbabilidades()
    {
        float total = 0f;
        foreach (var item in prefabsConProbabilidades)
        {
            total += item.probabilidad;
        }
        return Mathf.Approximately(total, 100f);
    }

    IEnumerator SpawnearPrefabsConDelay()
    {
        while (cantidadSpawneada < cantidadASpawnear)
        {
            SpawnearUnPrefab();
            cantidadSpawneada++;
            yield return new WaitForSeconds(intervaloSpawn);
        }
    }

    void SpawnearUnPrefab()
    {
        GameObject prefabElegido = ElegirPrefabPorProbabilidad();
        Instantiate(prefabElegido, puntoDeSpawneo.position, Quaternion.identity);
    }

    GameObject ElegirPrefabPorProbabilidad()
    {
        float random = Random.Range(0f, 100f);
        float acumulado = 0f;

        foreach (var item in prefabsConProbabilidades)
        {
            acumulado += item.probabilidad;
            if (random <= acumulado)
            {
                return item.prefab;
            }
        }

        return prefabsConProbabilidades[0].prefab;
    }
}
