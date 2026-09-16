using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public float duration = 0.15f;
    public float magnitude = 0.03f;
    public float missMagnitude = 1f;

    Vector3 originalPos;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        transform.localPosition = originalPos;
        StartCoroutine(DoShake(magnitude));
    }

    public void ShakeMiss()
    {
        StopAllCoroutines();
        transform.localPosition = originalPos;
        StartCoroutine(DoShake(missMagnitude));
    }

    IEnumerator DoShake(float shakeMag)
    {
        float elapsed = 0f;
        float seed = Random.Range(0f, 100f);

        while (elapsed < duration)
        {
            float x = (Mathf.PerlinNoise(seed, Time.time * 25f) - 0.5f) * shakeMag;
            float y = (Mathf.PerlinNoise(seed + 1f, Time.time * 25f) - 0.5f) * shakeMag;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
