using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    [Header("Combo")]
    public float comboPerfect = 3f;
    public float comboGood = 1f;
    public float maxCombo = 100f;
    public float combo = 0f;

    [Header("Fill UI")]
    public Image[] comboFill;

    [Header("combo drain")]
    [SerializeField] float drainDur = 2f;
    private bool isDrain = false;
    public JuiceBoxDur JB;

    [Header("Embarrassment Meter")]
    public float embarrassmentPerLate = 5f;
    public float embarrassmentPerMiss = 10f;
    public float maxEmbarrassment = 100f;
    public float embarrassment = 0f;
    public Image embarrassmentMeter;
    [SerializeField] GameObject tom;

    [Header("Characters")]
    public GameObject[] characters;

    private void Update()
    {
        Debug.Log(combo);
        Debug.Log(embarrassment);
    }

    public void RegisterPerfect()
    {
        if (isDrain) return;
        combo = Mathf.Min(maxCombo, combo + comboPerfect);
        updateComboFill();
        CheckComboFull();
    }

    public void RegisterGood()
    {
        if(isDrain) return;
        combo = Mathf.Min(maxCombo, combo + comboGood);
        updateComboFill();
        CheckComboFull();
    }

    public void RegisterLate()
    {
        embarrassment = Mathf.Min(maxEmbarrassment, embarrassment + embarrassmentPerLate);
        updateEmbarrassFill();
        CheckEmbarrassmentFull();
    }

    public void RegisterMiss()
    {
        embarrassment = Mathf.Min(maxEmbarrassment, embarrassment + embarrassmentPerMiss);
        updateEmbarrassFill();
        CheckEmbarrassmentFull();
    }

    void updateComboFill()
    {
        float fill = combo / maxCombo;
        foreach (Image img in comboFill)
        {
            img.fillAmount = fill;
        }
    }

    void updateEmbarrassFill()
    {
        embarrassmentMeter.fillAmount = embarrassment / maxEmbarrassment;
    }

    void CheckComboFull()
    {
        if (combo >= maxCombo)
        {
            Debug.Log("Combo is full!");
            StartCoroutine(drainCombo());
        }
    }

    IEnumerator drainCombo()
    {
        isDrain = true;
        float startCombo = combo;
        float elapsed = 0f;
        while (elapsed < drainDur)
        {
            elapsed += Time.deltaTime;
            combo = Mathf.Lerp(startCombo, 0f, elapsed / drainDur);
            JB.StartPulse();
            updateComboFill();
            yield return null;
        }

        combo = 0f;
        JB.StopPulse();
        updateComboFill();
        isDrain = false;
    }

    void CheckEmbarrassmentFull()
    {
        if (embarrassment >= maxEmbarrassment) Debug.Log("Embarrassment is full!");
        if(embarrassment > 30f)
        {
            foreach(GameObject cha in characters)
            {
                cha.GetComponent<Animation>().Stop();
            }
        }

        if(embarrassment > 50f)
        {
            tom.SetActive(true);
        }
    }
}
