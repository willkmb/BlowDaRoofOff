using System.Collections.Generic;
using UnityEngine;

public class BeatRecorder : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject markerCubePrefab;
    public Transform noteTrack;
    public float speed = 5f;

    public KeyCode[] laneKeys = { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.L };
    public float laneSpacing = 1.5f;

    public KeyCode pauseKey = KeyCode.Space;
    public KeyCode undoKey = KeyCode.LeftBracket;
    private bool isPaused = false;
    public float resumeLeadIn = 3f;

    struct Marker
    {
        public Transform transform;
        public float hitTime;
        public float laneOffset;
    }

    List<Marker> markers = new List<Marker>();

    void Update()
    {

        if (Input.GetKeyDown(pauseKey))
        {
            togglePause();
        }

        if (isPaused)
        {
            if (Input.GetKeyDown(undoKey))
            {
                UndoLastMarker();
            }
            return;
        }

        for (int i = 0; i < laneKeys.Length; i++)
        {
            if (Input.GetKeyDown(laneKeys[i]))
            {
                SpawnMarker(audioSource.time, i);
            }
        }

        UpdateMarkerPositions(audioSource.time);
    }

    void UpdateMarkerPositions(float songTime)
    {
        foreach (Marker m in markers)
        {
            if (m.transform == null) continue;

            float xLocal = (m.hitTime - songTime) * speed;
            Vector3 localOffset = new Vector3(xLocal, 0f, m.laneOffset);
            m.transform.position = noteTrack.position + noteTrack.rotation * localOffset;
            m.transform.rotation = noteTrack.rotation;
        }
    }

    void togglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            float resumeTime = Mathf.Max(0f, audioSource.time - resumeLeadIn);
            audioSource.Stop();
            audioSource.time = resumeTime;
            audioSource.Play();

            UpdateMarkerPositions(resumeTime);
        }
    }

    void UndoLastMarker()
    {
        if (markers.Count == 0)
        {
            Debug.Log("No markers to undo.");
            return;
        }

        int lastIndex = markers.Count - 1;
        Marker last = markers[lastIndex];

        if (last.transform != null)
        {
            Destroy(last.transform.gameObject);
        }
        markers.RemoveAt(lastIndex);

        audioSource.Stop();
        audioSource.time = last.hitTime;
        audioSource.Play();
        audioSource.Pause();

        UpdateMarkerPositions(last.hitTime);

    }

    void SpawnMarker(float time, int lane)
    {
        GameObject marker = Instantiate(markerCubePrefab, noteTrack.position, noteTrack.rotation, noteTrack);
        marker.name = $"Preview_{laneKeys[lane]}_{time:F2}";

        markers.Add(new Marker
        {
            transform = marker.transform,
            hitTime = time,
            laneOffset = lane * laneSpacing
        });
    }
}