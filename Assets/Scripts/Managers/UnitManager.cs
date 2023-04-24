using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;

    private List<ScriptableUnit> _units;
    public BaseCharacter SelectedCharacter;
    public BaseCharacter Character;
    public List<BaseEnemy> Enemies;

    void Awake() {
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();

    }

    public void SpawnCharacter() {
        var characterCount = 1;

        for (int i = 0; i < characterCount; i++) {
            var randomPrefab = GetRandomUnit<BaseCharacter>(Faction.Character);
            Character = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetCharacterSpawnTile();

            randomSpawnTile.SetUnit(Character);
            Character.HP = 30;
            Character.speed = 3;
            Character.attackRange = 1;
            Character.damage = 5;
            MenuManager.Instance.setupMenu(Character);

        }

        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var distMatrix = BFS.GetDistanceMatrix(GridManager.Instance._tiles[Character.GetCurrentPosition()]);
        var enemyCount = 1;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile(distMatrix);

            spawnedEnemy.HP = 10;
            spawnedEnemy.attackRange = 1;
            spawnedEnemy.speed = 2;
            spawnedEnemy.damage = 5;

            randomSpawnTile.SetUnit(spawnedEnemy);
            Enemies.Add(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.PlayersTurn);
    }
    public IEnumerator EnemiesTurn()
    {
        foreach(BaseEnemy enemy in Enemies)
        {
            Debug.Log("before turn");
            yield return enemy.EnemyTurn();
            Debug.Log("after turn");
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
