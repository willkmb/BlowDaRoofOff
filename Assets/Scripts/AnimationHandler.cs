using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Header("Speed Range")]
    [SerializeField] float minSpeed = 0.8f;
    [SerializeField] float maxSpeed = 1.2f;

    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();

        float randomSpeed = Random.Range(minSpeed, maxSpeed);

        foreach (AnimationState state in anim)
        {
            state.speed = randomSpeed;
        }
    }
}
