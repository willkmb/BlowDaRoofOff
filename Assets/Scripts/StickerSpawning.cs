using UnityEngine;

public class StickerSpawning : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject perfectPrefab;
    [SerializeField] GameObject goodPrefab;
    [SerializeField] GameObject latePrefab;
    [SerializeField] GameObject missPrefab;

    [Header("Spawn Area")]
    [SerializeField] RectTransform spawnArea;
    [SerializeField] Transform spawnParent;

    public void Spawn(string judgement)
    {
        GameObject prefabToSpawn = null;

        if (judgement == "Perfect")
        {
            prefabToSpawn = perfectPrefab;
        }
        else if (judgement == "Good")
        {
            prefabToSpawn = goodPrefab;
        }
        else if (judgement == "Late")
        {
            prefabToSpawn = latePrefab;
        }
        else if (judgement == "Miss")
        {
            prefabToSpawn = missPrefab;
        }

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No prefab found for: " + judgement);
            return;
        }

        GameObject spawnedImage = Instantiate(prefabToSpawn);
        RectTransform rectTransform = spawnedImage.GetComponent<RectTransform>();
        rectTransform.SetParent(spawnParent, false);
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);
        float randomX = Random.Range(corners[0].x, corners[2].x);
        float randomY = Random.Range(corners[0].y, corners[2].y);
        rectTransform.position = new Vector3(randomX, randomY, rectTransform.position.z);
    }
}
