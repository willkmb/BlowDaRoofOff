using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitDetector : MonoBehaviour
{
    public KeyCode key;
    private Transform hitZone;
    private float hitRange;
    private float colChangeRange;
    private MasterScript master;
    private Renderer rend;
    MaterialPropertyBlock mpb;
    bool hasBeenHit = false;
    private ParticleSystem hitParticles;

    [Header("Colours")]
    [SerializeField] Color blankCol = Color.white;
    [SerializeField] Color Ready = Color.red;
    static readonly int BaseColID = Shader.PropertyToID("_BaseCol");

    static List<NoteHitDetector> activeNotes = new List<NoteHitDetector>();

    private void Start()
    {
        hitZone = GameObject.FindWithTag("HitZone").transform;
        master = FindFirstObjectByType<MasterScript>();
        hitRange = master.HitRangeBefore;
        colChangeRange = master.ColChangeRangeBefore;
        hitParticles = GetComponentInChildren<ParticleSystem>();
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
        activeNotes.Add(this);
    }

    void OnDestroy()
    {
        activeNotes.Remove(this);
    }

    void Update()
    {
        if (hasBeenHit) return;

        float offset = transform.position.x - hitZone.position.x;
        float distance = Mathf.Abs(offset);
        bool before = offset > 0;
        float allowedRange = before? master.HitRangeBefore : master.HitRangeAfter;
        float colChangeRange = before ? master.ColChangeRangeBefore : master.ColChangeRangeAfter;

        float t = Mathf.InverseLerp(colChangeRange, allowedRange, distance);
        Color c = Color.Lerp(blankCol, Ready, t);
        rend.GetPropertyBlock(mpb);
        mpb.SetColor(BaseColID, c);
        rend.SetPropertyBlock(mpb);

        if (distance <= allowedRange && Input.GetKeyDown(key) && IsClosestForKey(distance))
        {
            hasBeenHit = true;
            StartCoroutine(HitRoutine());
        }
    }

    bool IsClosestForKey(float myDistance)
    {
        foreach (var note in activeNotes)
        {
            if (note != this && !note.hasBeenHit && note.key == key)
            {
                float otherDist = Mathf.Abs(note.transform.position.x - hitZone.position.x);
                if (otherDist < myDistance) return false;
            }
        }
        return true;
    }

    IEnumerator HitRoutine()
    {
        CameraShake.Instance.Shake();
        hitParticles.Play();
        this.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
