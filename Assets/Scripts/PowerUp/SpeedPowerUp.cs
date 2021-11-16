using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] private float speed;
    [SerializeField] private float powerUpTime;
    
    public IEnumerator StartPowerUp(Snek snek)
    {
        snek.speedModifier = speed;
        yield return new WaitForSeconds(powerUpTime);
        snek.speedModifier = 0;
    }
}
