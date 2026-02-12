using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAnimationController : MonoBehaviour
{
    public Player player;

    private Animator _animator;

    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private static readonly int HasPlateHash = Animator.StringToHash("HasPlate");

    private void Awake() {
        _animator = GetComponent<Animator>();

        if (player == null)
            player = FindAnyObjectByType<Player>();
    }

    private void Update() {
        _animator.SetFloat(MoveSpeedHash, player.currentSpeedNormalized, .1f, Time.deltaTime);
        _animator.SetBool(HasPlateHash, player.HasKitchenObject());
    }


}