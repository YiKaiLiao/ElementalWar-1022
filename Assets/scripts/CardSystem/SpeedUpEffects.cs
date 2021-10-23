using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public class SpeedUpEffects : MonoBehaviour
{
    public float addSpeed = 20f;
    public EnergyBar energyBar;
    public void onClick() {
        if(energyBar.getCurrentEnergy() >= 3){
            // Debug.Log(Move.moveSpeed);
            Move.moveSpeed += addSpeed;
            energyBar.UseEnergy(3);
            Debug.Log("analyticsResult=" + Analytics.CustomEvent("Click Skill Card: MoveSpeedUp"));
        }
    }
}
