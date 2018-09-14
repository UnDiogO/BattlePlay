using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="BaseMonoBehaviour"/> para rotacionar um objeto em torno do eixo Y
/// </summary>
public class TransformPivotRotate : BaseMonoBehaviour
{
    [SerializeField]
    private float degreesPerSecond;
    /// <summary>
    /// Graus por segundo
    /// </summary>
    public float DegreesPerSecond
    {
        get { return degreesPerSecond; }
        set { degreesPerSecond = value; }
    }

    /// <summary>
    /// Chamado a cada frame se o <see cref="MonoBehaviour"/> está ativo após todos os updates
    /// </summary>
    public void LateUpdate()
    {
        var localAngles = Self.localEulerAngles;
        localAngles.y += Time.deltaTime * DegreesPerSecond;
        Self.localEulerAngles = localAngles;
    }
}