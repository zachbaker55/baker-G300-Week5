using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction {
    Player,
    Enemy
}

public interface IAttackable {

    public void DoDamage(int damage);
    public Faction GetFaction();
}
