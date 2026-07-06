using UnityEngine;
using static Define;

public class State_Idle : FSM_State<BaseUnit>
{
    static readonly State_Idle instance = new State_Idle();
    public static State_Idle Instance
    {
        get { return instance; }
    }

    public override void EnterState(BaseUnit owner)
    {
        owner.SetAnimation(EState.Idle);
        base.EnterState(owner);
    }
}
