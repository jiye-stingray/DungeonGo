using UnityEngine;

public class CalculateStatus : MonoBehaviour
{
    public static Status GetStatusCalculateStatusData(StatusData data)
    {
        Status status = new Status();
        // 추후 계산식을 통한 상세화
        status._maxHp = data.maxHp;
        status._atk = data.atk;
        status._def = data.def;

        return status;
    }
}
