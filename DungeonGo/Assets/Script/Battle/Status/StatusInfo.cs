using System;
using UnityEngine;

[Serializable]
public class StatusInfo 
{
    public Status _dbStatus = new Status();

    public Status _growthStatus = new Status();

    public PlayStatus _playStatus = new PlayStatus();      // 인게임 진행중 status

    public StatusData _statusData;  

    public virtual void SetStatus(StatusData statusData)
    {
        _statusData = statusData;
        _dbStatus = CalculateStatus.GetStatusCalculateStatusData(statusData);

        _growthStatus = _dbStatus;
        _playStatus.Set(_growthStatus);

    }

    public virtual void SetHP()
    {
        _playStatus.HP = _dbStatus._maxHp;
    }
}
