using UnityEngine;
using System.Collections;

public class TomatoScript : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] GameObject imagePrefab;

    [Header("Spawn Area")]
    [SerializeField] RectTransform spawnArea;
    [SerializeField] Transform spawnParent;

    [Header("Spawning")]
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] float swapDelay = 0.4f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject spawnedImage = Instantiate(imagePrefab);
        RectTransform rectTransform = spawnedImage.GetComponent<RectTransform>();
        rectTransform.SetParent(spawnParent, false);

        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);
        float randomX = Random.Range(corners[0].x, corners[2].x);
        float randomY = Random.Range(corners[0].y, corners[2].y);
        rectTransform.position = new Vector3(randomX, randomY, rectTransform.position.z);

        Transform activeSprite = spawnedImage.transform.GetChild(0);
        Transform inactiveSprite = spawnedImage.transform.GetChild(1);

        activeSprite.gameObject.SetActive(true);
        inactiveSprite.gameObject.SetActive(false);

        StartCoroutine(SwapAfterDelay(activeSprite.gameObject, inactiveSprite.gameObject, swapDelay));
    }

    IEnumerator SwapAfterDelay(GameObject active, GameObject inactive, float delay)
    {
        yield return new WaitForSeconds(delay);
        active.SetActive(false);
        inactive.SetActive(true);
    }

}
