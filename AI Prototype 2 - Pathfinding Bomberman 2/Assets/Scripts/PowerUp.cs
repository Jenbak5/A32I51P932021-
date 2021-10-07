using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { ExtraBomb, SpeedUp, BombRange }

    public PowerUpType powerType;
    
    public void GetPowerup()
    {
        if (powerType == PowerUpType.ExtraBomb)
        {

        }
        if (powerType == PowerUpType.SpeedUp)
        {

        }
        if (powerType == PowerUpType.BombRange)
        {

        }
    }
}
