using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="BaseMonoBehaviour"/> para controle de exibição do indicado de saúde do personagem
/// </summary>
public class HealthIndicatorController : BaseMonoBehaviour
{
    [SerializeField]
    private Transform cam;
    /// <summary>
    /// Refderência para a <see cref="Camera"/> principal
    /// </summary>
    public Transform Cam
    {
        get
        {
            if (cam == null)
            {
                if (Camera.main != null)
                {
                    cam = Camera.main.transform;
                }
            }
            return cam;
        }
    }

    [SerializeField]
    private BaseCharacterBehaviour character;
    /// <summary>
    /// Referência para <see cref="BaseCharacterBehaviour"/> do personagem
    /// </summary>
    public BaseCharacterBehaviour Character
    {
        get { return character; }
        set { character = value; }
    }

    [SerializeField]
    private Transform indicator;
    /// <summary>
    /// Referência para a barra indicadora da saúde do personagem
    /// </summary>
    public Transform Indicator
    {
        get { return indicator; }
        set { indicator = value; }
    }

    /// <summary>
    /// Chamado a cada frame se o <see cref="MonoBehaviour"/> está ativo após todos os updates
    /// </summary>
    public void LateUpdate()
    {
        if (Cam != null)
        {
            Self.LookAt(Cam);
        }

        if (Indicator != null && Character != null)
        {
            var scale = Indicator.localScale;
            scale.x = Character.Health / Character.MaxHealth;
            Indicator.localScale = scale;
        }
    }
}