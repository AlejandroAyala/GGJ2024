using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    public int energy;
    public int maxEnergy = 3;
    public Animator animator;

    internal void AddEnergy(int applyAmmount)
    {
        energy += applyAmmount;
    }


    private void Update()
    {
        UIManager.Instance.SetEnergy(energy);
    }

    public void ReloadEnergy()
    {
        energy = maxEnergy;
    }
}
