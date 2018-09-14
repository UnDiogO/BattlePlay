using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="MonoBehaviour"/> para saída da aplicação
/// </summary>
public class Exit : MonoBehaviour
{
    /// <summary>
    /// Chamado a cada frame se o <see cref="MonoBehaviour"/> está ativo
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}