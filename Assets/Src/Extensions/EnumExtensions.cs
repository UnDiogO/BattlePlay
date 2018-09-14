using System;

/// <summary>
/// Classe de extensões para <see cref="Enum"/>
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Converter um elemento de enum para sua representação numérica
    /// </summary>
    /// <typeparam name="T">Tipo do enum</typeparam>
    /// <param name="value">Valor do enum</param>
    /// <returns>Retornar o valor numérico de um elemento de um enum</returns>
    public static int ToInt<T>(this T value) where T : struct, IFormattable, IConvertible, IComparable
    {
        return value.ToInt32(System.Globalization.CultureInfo.InvariantCulture);
    }
}