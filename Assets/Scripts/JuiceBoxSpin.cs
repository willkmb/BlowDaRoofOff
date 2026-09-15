using UnityEngine;
using System.Collections;

public class JuiceBoxSpin : MonoBehaviour
{
    public GameObject[] images;

    [Range(0.1f, 2f)]
    public float speed = 1f;

    int current = 0;

    void Start()
    {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (true)
        {
            images[current].SetActive(false);
            current = (current + 1) % images.Length;
            images[current].SetActive(true);
            yield return new WaitForSeconds(speed);
        }
    }
}
