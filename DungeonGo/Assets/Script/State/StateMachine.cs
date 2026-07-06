using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

class CallEventData
{
    public string name;
    public float time;
}

public class StateMachine<T> where T : BaseUnit
{
    private T baseUnit;     // 상태 Unit

    private FSM_State<T> CurrentState;  // 현재 상태
    private FSM_State<T> PreState;      // 이전 상태

    [Header("Animation")]
    float _time;
    float _duration;
    Spine.Animation _anim;
    // 각 Status의 Event 
    List<CallEventData> _callEventDataList = new();
    int _callIndex = 0;
    int CallIndex
    {
        set
        {
            _callIndex = value;
            SetSkillDetailIndex();
        }
        get => _callIndex;
    }

    public void Init(T owner, FSM_State<T> initState)
    {
        baseUnit = owner;
        ChangeState(initState);
    }

    public void ChangeState(FSM_State<T> newState)
    {
        PreState = CurrentState;
        if (CurrentState != null)
            CurrentState.ExitState(baseUnit);
        CurrentState = newState;

        if(CurrentState != null)
            CurrentState.EnterState(baseUnit);
    }

    public void Update()
    {
        if (CurrentState == null)
            return;
        CurrentState.UpdateState(baseUnit);
    }

    private void SetSkillDetailIndex()
    {
        // 이미 모든 event 진행 
        if (CallIndex >= _callEventDataList.Count)
            return;

        // 추후 unit & skill 쪽에 index 셋팅 


    }
}
