using UnityEngine;

public class TomatoScript : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject imagePrefab;

    [Header("Spawn Area")]
    [SerializeField] RectTransform spawnArea;
    [SerializeField] Transform spawnParent;

    [Header("Spawning")]
    [SerializeField] float spawnInterval = 5f;
    public bool shouldSpawn = false;

    private float timer = 0f;

    void Update()
    {
        if (!shouldSpawn)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        if (imagePrefab == null)
        {
            Debug.LogWarning("No prefab assigned to spawn.");
            return;
        }

        GameObject spawnedImage = Instantiate(imagePrefab);
        RectTransform rectTransform = spawnedImage.GetComponent<RectTransform>();
        rectTransform.SetParent(spawnParent, false);

        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);
        float randomX = Random.Range(corners[0].x, corners[2].x);
        float randomY = Random.Range(corners[0].y, corners[2].y);
        rectTransform.position = new Vector3(randomX, randomY, rectTransform.position.z);
    }
}
