using UnityEngine;

public class MonsterUnit : BaseUnit
{
    private void Start()
    {
        _unitType = Define.EUnitType.Monster;
        Init(1);
    }
}
