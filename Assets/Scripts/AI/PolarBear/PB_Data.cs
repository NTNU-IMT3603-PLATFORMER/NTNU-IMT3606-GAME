using UnityEngine;

/// <summary>
/// Shared data for Polar Bear states
/// </summary>
public class PB_Data : MonoBehaviour {

    [SerializeField, Tooltip("The speed of the boss")]
    float _speed;
    [SerializeField, Tooltip("The effect that should be spawned when performing shockwave attack")]
    GameObject _prefabShockwave;
    [SerializeField, Tooltip("Offset for prefabShockwave position when spawned")]
    Vector3 _prefabShockwaveOffset = new Vector3(0f, -0.5f, 0f);

    public GameObject player => PlayerEntity.INSTANCE.gameObject;

    public CharacterController2D characterController2D { get; private set; }
    public EnemyEntity enemyEntity { get; private set; }
    public AttackStrategy currentAttackStrategy { get; set; }

    public float speed => _speed;
    public GameObject prefabShockwave => _prefabShockwave;
    public Vector3 prefabShockwaveOffset => _prefabShockwaveOffset;

    public enum AttackStrategy {
        NormalAttack,
        Charge,
        Shockwave
    }

    void Start() {
        characterController2D = GetComponentInParent<CharacterController2D>();
        enemyEntity = GetComponentInParent<EnemyEntity>();
    }

}
