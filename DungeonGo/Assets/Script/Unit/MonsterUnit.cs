using UnityEngine;

public class MonsterUnit : BaseUnit
{
    MonsterStatusInfo _statusInfo;
    private void Start()
    {
        _unitType = Define.EUnitType.Monster;
        Init(1);
    }

    public override void Init(int id)
    {
        // status 
        // Dummy Setting
        StatusData statusData = new StatusData()
        {
            maxHp = 10,
            atk = 2,
            def = 0,
        };
        _statusInfo.SetStatus(statusData);
        _statusInfo.SetHP();

        base.Init(id);
    }
}
