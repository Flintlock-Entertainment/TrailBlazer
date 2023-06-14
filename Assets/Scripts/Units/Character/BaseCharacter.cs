using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is the base class for all characters in the game.
It inherits from the BaseUnit class.
*/
public class BaseCharacter : BaseUnit
{
    public Tile selectedTile => GridManager.Instance.selectedTile;
    [SerializeField] private AudioClip[] HitList;
    [SerializeField] private AudioSource HitSoundEffect;

    /*void Start()
    {
        HitSoundEffect = GetComponent<AudioSource>();
    }*/

    private void OnDestroy()
    {
        bool alive = unitData.GetCurrentHP() > 0;
        if (!alive)
        {
            UnitManager.Instance.CharacterDead() ;
            
        }
    }
    //Override the TakeDamage function from BaseUnit to include updating the menu.
    public override void TakeDamage(int damage)
    {
       /*if(damage != 0)
        {
            int rand = Random.Range(0, HitList.Length);
            HitSoundEffect.clip = HitList[rand];
            HitSoundEffect.PlayOneShot(HitSoundEffect.clip);
        }*/
       
        base.TakeDamage(damage);
        MenuManager.Instance.UpdateMenu(this);
    }

    public override void ResetTurn()
    {
        base.ResetTurn();
        MenuManager.Instance.UpdateMenu(this);
    }

    public override void UpdateTurns(int update)
    {
        base.UpdateTurns(update);
        MenuManager.Instance.UpdateMenu(this);
        ChangeTurn();
    }

    // Change the turn to the enemies' turn
    protected override void ChangeTurn()
    {
        if( Turns == 0)
            CombatManager.Instance.ChangeState(CombatState.EnemiesTurn);
    }
}