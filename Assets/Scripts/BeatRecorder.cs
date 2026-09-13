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

    struct Marker
    {
        public Transform transform;
        public float hitTime;
        public float laneOffset;
    }

    List<Marker> markers = new List<Marker>();

    void Update()
    {
        for (int i = 0; i < laneKeys.Length; i++)
        {
            if (Input.GetKeyDown(laneKeys[i]))
            {
                SpawnMarker(audioSource.time, i);
            }
        }

        float songTime = audioSource.time;
        foreach (Marker m in markers)
        {
            float xLocal = (m.hitTime - songTime) * speed;
            Vector3 localOffset = new Vector3(xLocal, 0f, m.laneOffset);
            m.transform.position = noteTrack.position + noteTrack.rotation * localOffset;
            m.transform.rotation = noteTrack.rotation;
        }
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
