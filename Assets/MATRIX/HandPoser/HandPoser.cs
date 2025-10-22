using UnityEngine;

public class HandPoser : MonoBehaviour
{
    [SerializeField] bool LeftHand = false;
    [SerializeField] [Range(0f, 1f)] float Thumb = 0f;
    [SerializeField] [Range(0f, 1f)] float Index = 0f;
    [SerializeField] [Range(0f, 1f)] float Middle = 0f;
    [SerializeField] [Range(0f, 1f)] float Ring = 0f;
    [SerializeField] [Range(0f, 1f)] float Pinky = 0f;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(LeftHand ? -1 : 1, 1, 1);
        animator.SetLayerWeight(1, Thumb);
        animator.SetLayerWeight(2, Index);
        animator.SetLayerWeight(3, Middle);
        animator.SetLayerWeight(4, Ring);
        animator.SetLayerWeight(5, Pinky);
    }
}
