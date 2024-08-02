using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject roadPrefab; // Pr�fabriqu� de la route
    public GameObject[] obstaclePrefabs; // Pr�fabriqu�s des obstacles
    public Transform playerTransform; // R�f�rence au transform du joueur
    public float spawnDistance = 200f; // Distance � laquelle g�n�rer les nouveaux segments
    public int numSegments = 5; // Nombre de segments de route � g�n�rer initialement

    private Queue<GameObject> activeRoadSegments = new Queue<GameObject>(); // Segments de route actifs
    private float spawnZ = 0f; // Position Z pour le prochain segment de route

    public GameManager gameManager;

    private void Start()
    {
        playerTransform = gameManager.playerInstance.transform;
        // G�n�rer les segments initiaux
        for (int i = 0; i < numSegments; i++)
        {
            SpawnRoadSegment();
        }
    }

    private void Update()
    {
        // G�n�rer des segments de route suppl�mentaires si n�cessaire
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

        // Mettre � jour la position Z pour le prochain segment
        spawnZ += segment.GetComponent<Renderer>().bounds.size.z;
    }

    private void PlaceObstacles(GameObject segment)
    {
        // D�finir le nombre et les positions des obstacles al�atoirement
        int numObstacles = Random.Range(1, 4); // Exemple : 1 � 3 obstacles par segment
        for (int i = 0; i < numObstacles; i++)
        {
            // Choisir un obstacle al�atoire
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            // Calculer une position al�atoire sur le segment
            float posX = Random.Range(-3f, 3f);
            float posZ = Random.Range(-segment.GetComponent<Renderer>().bounds.size.z / 2, segment.GetComponent<Renderer>().bounds.size.z / 2);

            // Instancier l'obstacle avec les coordonn�es locales par rapport au segment
            Vector3 obstaclePosition = new Vector3(posX, 0.5f, posZ);
            GameObject obstacleInstance = Instantiate(obstaclePrefab, segment.transform);
            obstacleInstance.transform.localPosition = obstaclePosition;
            obstacleInstance.transform.localRotation = obstaclePrefab.transform.rotation;
        }
    }

    private void DeleteOldestSegment()
    {
        // Supprimer le segment de route le plus ancien pour �viter d'occuper trop de m�moire
        GameObject oldestSegment = activeRoadSegments.Dequeue();
        Destroy(oldestSegment);
    }
}
