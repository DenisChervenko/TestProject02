using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/PunkEnemy")]
public class EnemyInfo : ScriptableObject
{
    public float health;
    public float defend;
    public float damage;
    public float speed;
}
