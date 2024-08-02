using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject roadPrefab; // Préfabriqué de la route
    public GameObject[] obstaclePrefabs; // Préfabriqués des obstacles
    public Transform playerTransform; // Référence au transform du joueur
    public float spawnDistance = 200f; // Distance à laquelle générer les nouveaux segments
    public int numSegments = 5; // Nombre de segments de route à générer initialement

    private Queue<GameObject> activeRoadSegments = new Queue<GameObject>(); // Segments de route actifs
    private float spawnZ = 0f; // Position Z pour le prochain segment de route

    public GameManager gameManager;

    private void Start()
    {
        playerTransform = gameManager.playerInstance.transform;
        // Générer les segments initiaux
        for (int i = 0; i < numSegments; i++)
        {
            SpawnRoadSegment();
        }
    }

    private void Update()
    {
        // Générer des segments de route supplémentaires si nécessaire
        if (playerTransform.position.z + spawnDistance > spawnZ)
        {
            SpawnRoadSegment();
            DeleteOldestSegment();
        }
    }

    private void SpawnRoadSegment()
    {
        // Instancier un nouveau segment de route
        GameObject segment = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), roadPrefab.transform.rotation);
        activeRoadSegments.Enqueue(segment);

        // Placer des obstacles sur le segment
        PlaceObstacles(segment);

        // Mettre à jour la position Z pour le prochain segment
        spawnZ += segment.GetComponent<Renderer>().bounds.size.z;
    }

    private void PlaceObstacles(GameObject segment)
    {
        // Définir le nombre et les positions des obstacles aléatoirement
        int numObstacles = Random.Range(1, 4); // Exemple : 1 à 3 obstacles par segment
        for (int i = 0; i < numObstacles; i++)
        {
            // Choisir un obstacle aléatoire
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Calculer une position aléatoire sur le segment
            float posX = Random.Range(-3f, 3f);
            float posZ = Random.Range(-segment.GetComponent<Renderer>().bounds.size.z / 2, segment.GetComponent<Renderer>().bounds.size.z / 2);

            // Instancier l'obstacle avec les coordonnées locales par rapport au segment
            Vector3 obstaclePosition = new Vector3(posX, 0.5f, posZ);
            GameObject obstacleInstance = Instantiate(obstaclePrefab, segment.transform);
            obstacleInstance.transform.localPosition = obstaclePosition;
            obstacleInstance.transform.localRotation = obstaclePrefab.transform.rotation;
        }
    }

    private void DeleteOldestSegment()
    {
        // Supprimer le segment de route le plus ancien pour éviter d'occuper trop de mémoire
        GameObject oldestSegment = activeRoadSegments.Dequeue();
        Destroy(oldestSegment);
    }
}
