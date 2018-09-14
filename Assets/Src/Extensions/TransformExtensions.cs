using UnityEngine;

/// <summary>
/// Classe de extensões para <see cref="Transform"/>
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    /// Obter a distância até outro <see cref="Transform"/>
    /// </summary>
    /// <param name="transform">Transform referência</param>
    /// <param name="other">Outro Transform</param>
    /// <returns>Retornar a distância até outro transform</returns>
    public static float DistanceTo(this Transform transform, Transform other)
    {
        return (other.position - transform.position).magnitude;
    }
}