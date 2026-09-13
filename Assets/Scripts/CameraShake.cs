using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public float duration = 0.15f;
    public float magnitude = 0.1f;

    Vector3 originalPos;

    void Awake()
    {
        Instance = this;
    }

    public void Shake()
    {
        StopAllCoroutines();
        originalPos = transform.localPosition;
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsed = 0f;
        float seed = Random.Range(0f, 100f);

        while (elapsed < duration)
        {
            float x = (Mathf.PerlinNoise(seed, Time.time * 25f) - 0.5f) * magnitude;
            float y = (Mathf.PerlinNoise(seed + 1f, Time.time * 25f) - 0.5f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
