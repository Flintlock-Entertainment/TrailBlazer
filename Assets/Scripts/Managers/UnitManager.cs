using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseCharacter SelectedCharacter;
    public List<BaseCharacter> Characters;
    public List<BaseEnemy> Enemies;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnCharacter() {
        var characterCount = 1;

        for (int i = 0; i < characterCount; i++) {
            var randomPrefab = GetRandomUnit<BaseCharacter>(Faction.Character);
            var spawnedCharacter = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetCharacterSpawnTile();

            randomSpawnTile.SetUnit(spawnedCharacter);
            Characters.Add(spawnedCharacter);
        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
            Enemies.Add(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.PlayersTurn);
    }
    public void EnemiesTurn()
    {
        foreach(BaseEnemy enemy in Enemies)
        {
            enemy.EnemyTurn();
        }
        GameManager.Instance.ChangeState(GameState.PlayersTurn);
    }
    private T GetRandomUnit<T>(Faction faction) where T : BaseUnit {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedCharacter(BaseCharacter character) {
        SelectedCharacter = character;
        //MenuManager.Instance.ShowSelectedCharacter(character);
    }
}
