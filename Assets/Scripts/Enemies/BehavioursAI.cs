using System;
using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;


static public class BehavioursAI
{
    // Shared Behaviours
    static public IdleBehaviour idle = new IdleBehaviour();
    static public MoveToHomeBehaviour moveToHome = new MoveToHomeBehaviour();
    static public MoveToPlayerBehaviour moveToPlayer = new MoveToPlayerBehaviour();
    static public MoveAwayFromPlayerBehaviour moveAwayFromPlayer = new MoveAwayFromPlayerBehaviour();
    static public CirclePlayerBehaviour circlePlayer = new CirclePlayerBehaviour();

    // Melee Enemy Behaviour
    static public MeleeAttackBehaviour meleeAttack = new MeleeAttackBehaviour();

    // Ranged Enemy Behaviour
    static public RangedAttackBehaviour rangedAttack = new RangedAttackBehaviour();

    // Magic Enemy Behaviour
    static public FireballAttackBehaviour fireballAttack = new FireballAttackBehaviour();
    static public TeleportBehaviour teleport = new TeleportBehaviour();

    // Status Effected Behaviours
    static public RunRandomlyBehaviour runRandomly = new RunRandomlyBehaviour();

}
