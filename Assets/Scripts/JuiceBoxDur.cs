using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class JuiceBoxDur : MonoBehaviour
{

    [Header("Volume")]
    [SerializeField] Volume volume;

    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;

    [Header("Chromatic Aberration Pulse")]
    [SerializeField] float chromaticMin = 0f;
    [SerializeField] float chromaticMax = 0.3f;
    [SerializeField] float chromaticSpeed = 2f;

    [Header("Hue Shift Pulse")]
    [SerializeField] float hueMin = -10f;
    [SerializeField] float hueMax = 10f;
    [SerializeField] float hueSpeed = 1f;

    [Header("Fade")]
    [SerializeField] float fadeDuration = 0.5f;

    private bool pulsing;

    void Start()
    {
        if (volume == null)
        {
            return;
        }

        if (!volume.profile.TryGet(out chromaticAberration))
            Debug.LogWarning("no CA");

        if (!volume.profile.TryGet(out colorAdjustments))
            Debug.LogWarning("no ColA");
    }

    void Update()
    {
        if (!pulsing) return;

        if (chromaticAberration != null)
        {
            float t = (Mathf.Sin(Time.time * chromaticSpeed) + 1f) / 2f;
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticMin, chromaticMax, t);
        }

        if (colorAdjustments != null)
        {
            float t = (Mathf.Sin(Time.time * hueSpeed) + 1f) / 2f;
            colorAdjustments.hueShift.value = Mathf.Lerp(hueMin, hueMax, t);
        }
    }

    public void StartPulse() => pulsing = true;

    public void StopPulse()
    {
        pulsing = false;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float startCA = chromaticAberration != null ? chromaticAberration.intensity.value : 0f;
        float startHue = colorAdjustments != null ? colorAdjustments.hueShift.value : 0f;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / fadeDuration;
            if (chromaticAberration != null) chromaticAberration.intensity.value = Mathf.Lerp(startCA, chromaticMin, lerp);
            if (colorAdjustments != null) colorAdjustments.hueShift.value = Mathf.Lerp(startHue, 0f, lerp);
            yield return null;
        }
    }
}
