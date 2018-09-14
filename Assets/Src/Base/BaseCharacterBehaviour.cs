using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="MonoBehaviour"/> com caracteristicas e comportamentos básicos de um personagem
/// </summary>
public class BaseCharacterBehaviour : BaseMonoBehaviour
{
    [SerializeField]
    private bool showGizmos;
    /// <summary>
    /// Mostrar informações de gizmos
    /// </summary>
    public bool ShowGizmos
    {
        get { return showGizmos; }
        set { showGizmos = value; }
    }

    [SerializeField]
    private GroupType group;
    /// <summary>
    /// Grupo a qual o personagem pertence
    /// </summary>
    public GroupType Group
    {
        get { return group; }
        set { group = value; }
    }

    [SerializeField]
    private StateCharacter state;
    /// <summary>
    /// Estado de comportamento do personagem
    /// </summary>
    public StateCharacter State
    {
        get { return state; }
        set
        {
            if (state != value)
            {
                state = value;
                OnStateChanged();
            }
        }
    }

    [SerializeField]
    private float maxHealth;
    /// <summary>
    /// Valor máximo de saúde do personagem
    /// </summary>
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    [SerializeField]
    private float health;
    /// <summary>
    /// Valor de saúde do personagem
    /// </summary>
    public float Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0f, MaxHealth);
            if (health <= 0f)
            {
                State = StateCharacter.Die;
                CancelInvoke();
            }
        }
    }

    [SerializeField]
    private float defense;
    /// <summary>
    /// Valor de defesa do personagem
    /// </summary>
    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    [SerializeField]
    private float range;
    /// <summary>
    /// Valor de alcance do personagem
    /// </summary>
    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    [SerializeField]
    private float attack;
    /// <summary>
    /// Valor de ataque do personagem
    /// </summary>
    public float Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    [SerializeField]
    private float attackSpeed;
    /// <summary>
    /// Valor de velocidade de ataque do personagem
    /// </summary>
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    [SerializeField]
    private float moveSpeed;
    /// <summary>
    /// Valor de velocidade de movimento do persongam
    /// </summary>
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    [SerializeField]
    private BaseCharacterBehaviour target;
    /// <summary>
    /// Alvo do personagem
    /// </summary>
    public BaseCharacterBehaviour Target
    {
        get { return target; }
        set { target = value; }
    }

    /// <summary>
    /// Valor da distância do personagem até o alvo
    /// </summary>
    public float DistanceToTarget
    {
        get
        {
            return Self.DistanceTo(Target.Self);
        }
    }

    [SerializeField]
    private Animator anim;
    /// <summary>
    /// Referência de cache para o <see cref="Animator"/> do personagem
    /// </summary>
    public Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }
            return anim;
        }
    }

    /// <summary>
    /// Chamado no frame que o script for ativado antes de qualquer um dos métodos de update serem chamados pela primeira vez
    /// </summary>
    public void Start()
    {
        Health = MaxHealth;
    }

    /// <summary>
    /// Chamado a cada frame se o <see cref="MonoBehaviour"/> está ativo
    /// </summary>
    public void Update()
    {
        if (State == StateCharacter.Die)
        {
            Target = null;

            CancelInvoke();
        }
        else if (Target != null)
        {
            if (Target.State == StateCharacter.Die)
            {
                Target = null;

                CancelInvoke();

                if (State != StateCharacter.Die)
                {
                    State = StateCharacter.Idle;
                }
            }
            else if (DistanceToTarget > Range)
            {
                TargetFollow();
            }
            else if (State != StateCharacter.Attack && State != StateCharacter.Die)
            {
                TargetAttack();
            }
        }
        else
        {
            State = StateCharacter.Idle;
        }
    }

    /// <summary>
    /// Desenhar gizmos selecionáveis e sempre desenhados
    /// </summary>
    public void OnDrawGizmos()
    {
        if (ShowGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Self.position, Range);
        }
    }

    /// <summary>
    /// Causar dano a esse personagem
    /// </summary>
    /// <param name="damage">Dano causado</param>
    public void Hurt(float damage)
    {
        if (Target != null)
        {
            Target.Health -= (damage - Target.Defense);
        }
    }

    /// <summary>
    /// Seguir em direção ao alvo
    /// </summary>
    public void TargetFollow()
    {
        State = StateCharacter.Walk;

        Self.LookAt(Target.Self);

        var position = Self.position;
        position += Self.forward * MoveSpeed * Time.deltaTime;
        Self.position = position;
    }

    /// <summary>
    /// Atacar o alvo
    /// </summary>
    public void TargetAttack()
    {
        State = StateCharacter.Attack;

        if (Target != null)
        {
            Self.LookAt(Target.Self);

            if (State != StateCharacter.Die && Target.State != StateCharacter.Die)
            {
                Hurt(Attack);

                Invoke("TargetAttack", AttackSpeed);
            }
        }
    }

    /// <summary>
    /// Chamado sempre que o estado do personagem é modificado
    /// </summary>
    public void OnStateChanged()
    {
        Anim.SetInteger("State", State.ToInt());
    }
}