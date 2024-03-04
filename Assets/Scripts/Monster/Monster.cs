using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Monster
{
    public void OnAttacked(int dam, GameObject attacker);

    public void OnFollowPlayer();

    public void OnAttack();
}
