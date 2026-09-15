using UnityEngine;
using UnityEngine.Events;
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

    [Header("Embarrassment Meter")]
    public float embarrassmentPerLate = 5f;
    public float embarrassmentPerMiss = 10f;
    public float maxEmbarrassment = 100f;
    public float embarrassment = 0f;
    public Image embarrassmentMeter;

    [Header("Characters")]
    public GameObject[] characters;

    private void Update()
    {
        Debug.Log(combo);
        Debug.Log(embarrassment);
    }

    public void RegisterPerfect()
    {
        combo = Mathf.Min(maxCombo, combo + comboPerfect);
        updateComboFill();
        CheckComboFull();
    }

    public void RegisterGood()
    {
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
            combo = 0;
        }
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
    }
}
