using UnityEngine;

public class FSM_State<T>
{
    virtual public void EnterState(T owner) { }
    virtual public void UpdateState(T owner) { }
    virtual public void ExitState(T owner) { }


}
