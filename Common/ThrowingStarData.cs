using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowingStarData : MonoBehaviour
{
    public int Power { get; private set; }

    void Start()
    {
        int grade = int.Parse(GetComponent<Image>().sprite.name);
        Power = grade * DataManager.Instance.GetStarsUpgradeLevelData(grade - 1) * DataManager.Instance.Increased_Attack_Damage;

        if (DataManager.Instance.GetCriticalChance())
        {
            Power *= (1 + DataManager.Instance.Critical_Damage);
        }
    }
}
