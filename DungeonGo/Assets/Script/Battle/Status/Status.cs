using System;
using UnityEngine;

public enum EStatus
{
    MAXHP,
    ATK,
    DEF
}

[Serializable]
public class Status 
{
    public double _maxHp;
    public double _atk;
    public double _def;

    public virtual void Set(Status status)
    {
        _maxHp = status._maxHp;
        _atk = status._atk;
        _def = status._def;
    }

    public void Reset()
    {
        _maxHp = 0;
        _atk = 0;
        _def = 0;
    }

    public void Plus(EStatus kind, double value)
    {
        switch (kind)
        {
            case EStatus.MAXHP:
                _maxHp += value;
                break;
            case EStatus.ATK:
                _atk += value;
                break;
            case EStatus.DEF:
                _def += value;
                break;
            default:
                break;
        }
    }

    public double GetStatus(EStatus kind)
    {
        double status = 0f;
        switch (kind)
        {
            case EStatus.MAXHP:
                status = _maxHp;
                break;
            case EStatus.ATK:
                status = _atk;
                break;
            case EStatus.DEF:
                status = _def;
                break;
        }
        return status;
    }

    public static Status operator +(Status a , Status b)
    {
        if(b == null) return a;
        Status status = new Status();
        status._maxHp = a._maxHp + b._maxHp;
        status._atk = a._atk + b._atk;
        status._def = a._def + b._def;
        return status;  
    }

    public static Status operator -(Status a, Status b)
    {
        if(b == null) return a;
        Status status = new Status();
        status._maxHp = a._maxHp -b._maxHp;
        status._atk = a._atk - b._atk;
        status._def =a._def - b._def;
        return status;
    }
}
