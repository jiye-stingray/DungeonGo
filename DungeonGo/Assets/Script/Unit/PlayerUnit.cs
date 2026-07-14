using UnityEngine;
using static Define;

public class PlayerUnit : BaseUnit
{
    private void Start()
    {
        _unitType = EUnitType.Character;
        // 임시 선언 추후 factory
        Init(1);
    }
}
