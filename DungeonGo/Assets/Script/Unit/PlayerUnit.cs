using UnityEngine;
using static Define;

public class PlayerUnit : BaseUnit
{
    [SerializeField] PlayerStatusInfo _statusInfo;

    private void Start()
    {
        _unitType = EUnitType.Character;
        _statusInfo = new PlayerStatusInfo();
        // 임시 선언 추후 factory
        Init(1);
    }

    public override void Init(int id)
    {
        // status 
        // Dummy Setting
        StatusData statusData = new StatusData()
        {
            maxHp = 100,
            atk = 10,
            def = 0,
        };
        _statusInfo.SetStatus(statusData);
        _statusInfo.SetHP();

        base.Init(id);
    }
}
