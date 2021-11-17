using System.Collections;
using UnityEngine;

public class DiscoPowerUp : MonoBehaviour, IPowerUp
{
    [SerializeField] private float powerUpTime;
    
    public IEnumerator StartPowerUp(Snek snek)
    {
        snek.screenOverlay.SetActive(true);
        yield return new WaitForSeconds(powerUpTime);
        snek.screenOverlay.SetActive(false);
    }
}
