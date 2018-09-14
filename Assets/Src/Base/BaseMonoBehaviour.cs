using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="MonoBehaviour"/> com cache do <see cref="Transform"/>
/// </summary>
public class BaseMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    /// <summary>
    /// Referência de cache para o <see cref="Transform"/> do objeto
    /// </summary>
    public Transform Self
    {
        get
        {
            if (self == null)
            {
                self = GetComponent<Transform>();
            }
            return self;
        }
    }
}