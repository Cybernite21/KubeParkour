using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int Health { get; set; }
    public int AirInTank { get; set; }

    public void takeDamage(int damage);
}
