using System.Collections;
using UnityEngine;

public class FoodPowerUp : MonoBehaviour, IPowerUp
{
    public IEnumerator StartPowerUp(Snek snek)
    {
        snek.body.AddBodyPart();
        snek.body.AddBodyPart();
        yield return null;
    }
}
