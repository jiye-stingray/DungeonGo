using UnityEngine;

public class PlayStatus : Status
{
    [SerializeField] private double _hp;
    public double HP
    {
        get => _hp;
        set => _hp = value;
    }
}
