using UnityEngine;

public class RecorderSprites : MonoBehaviour
{
    [SerializeField] GameObject empty;
    [SerializeField] GameObject spriteD;
    [SerializeField] GameObject spriteF;
    [SerializeField] GameObject spriteJ;
    [SerializeField] GameObject spriteK;
    [SerializeField] GameObject spriteL;

    KeyCode[] keys;
    GameObject[] sprites;
    int current = -1;

    void Awake()
    {
        keys = new[] { KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.L };
        sprites = new[] { spriteD, spriteF, spriteJ, spriteK, spriteL };
        Show(-1);
    }

    void Update()
    {
        for (int i = 0; i < keys.Length; i++) if (Input.GetKeyDown(keys[i])) Show(i);
        if (current >= 0 && !Input.GetKey(keys[current])) Show(-1);
    }

    void Show(int index)
    {
        for (int i = 0; i < sprites.Length; i++) sprites[i].SetActive(i == index);
        empty.SetActive(index < 0);
        current = index;
    }
}
