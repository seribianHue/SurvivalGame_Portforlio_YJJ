using UnityEngine;

interface Resource
{
    public void OnAttacked(int dam);
    public void OnDestroyed();
    public void DropItem(int num, GameObject obj);
}
