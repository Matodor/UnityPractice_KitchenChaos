using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static readonly int PropIsWalking = Animator.StringToHash("IsWalking");

    [SerializeField]
    private Player _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(PropIsWalking, _player.IsWalking);
    }

    private void Update()
    {
        _animator.SetBool(PropIsWalking, _player.IsWalking);
    }
}
