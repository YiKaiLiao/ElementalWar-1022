using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CureEffects : MonoBehaviour
{
    //public float addSpeed = 20f;

    public int addHP = 20;
    public EnergyBar energyBar;
    public void onClick() {
        if(energyBar.getCurrentEnergy() >= 5){
            Player.currentHP += addHP;
            energyBar.UseEnergy(5);
            Debug.Log("analyticsResult=" + Analytics.CustomEvent("Click Skill Card: Cure"));
        }

    }
}
