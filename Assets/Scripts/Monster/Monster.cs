using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Monster
{
    public void OnAttacked(int dam);

    public void OnFollowPlayer();

    public void OnAttack();
}
