using System.Collections.Generic;
using System.Linq;

using UnityEngine;

/// <summary>
/// Classe derivada de <see cref="MonoBehaviour"/> para gerenciamento de batalha
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<BaseCharacterBehaviour> allies;
    /// <summary>
    /// Lista contendo <see cref="BaseCharacterBehaviour"/> aliados
    /// </summary>
    public List<BaseCharacterBehaviour> Allies
    {
        get { return allies; }
        set { allies = value; }
    }

    [SerializeField]
    private List<BaseCharacterBehaviour> enemies;
    /// <summary>
    /// Lista contendo <see cref="BaseCharacterBehaviour"/> inimigos
    /// </summary>
    public List<BaseCharacterBehaviour> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }

    /// <summary>
    /// Chamado no frame que o script for ativado antes de qualquer um dos métodos de update serem chamados pela primeira vez
    /// </summary>
    public void Start()
    {
        Allies = new List<BaseCharacterBehaviour>();
        Enemies = new List<BaseCharacterBehaviour>();

        var allCharacters = FindObjectsOfType<BaseCharacterBehaviour>();

        Allies.AddRange(allCharacters.Where(c => c.Group == GroupType.Allied));
        Enemies.AddRange(allCharacters.Where(c => c.Group == GroupType.Enemy));
    }

    /// <summary>
    /// Chamado a cada frame se o <see cref="MonoBehaviour"/> está ativo
    /// </summary>
    public void Update()
    {
        Allies.RemoveAll(c => c.State == StateCharacter.Die);
        Enemies.RemoveAll(c => c.State == StateCharacter.Die);

        foreach (var ally in Allies)
        {
            ally.Target = Enemies.OrderBy(c => c.Self.DistanceTo(ally.Self)).FirstOrDefault();
        }

        foreach(var enemy in Enemies)
        {
            enemy.Target = Allies.OrderBy(c => c.Self.DistanceTo(enemy.Self)).FirstOrDefault();
        }
    }
}