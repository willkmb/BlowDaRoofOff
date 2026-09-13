using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private MasterScript master;
    private float speed;

    private void Start()
    {
        master = FindFirstObjectByType<MasterScript>();
        speed = master.speedOfNote;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
