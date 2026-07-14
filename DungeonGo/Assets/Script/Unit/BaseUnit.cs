using Cysharp.Threading.Tasks;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Define;
using static Utils;

public class BaseUnit : MonoBehaviour
{
    public int _id;
    public EUnitType _unitType;

    StateMachine<BaseUnit> _state = new StateMachine<BaseUnit>();

    public SpineAnimation _spineAnim;

    private void Awake()
    {
        _spineAnim = GetOrAddComponent<SpineAnimation>(gameObject);
    }

    public virtual void Init(int id)
    {
        _id = id;
        SetSpine().Forget();

        if (_state == null)
            _state = new StateMachine<BaseUnit>();
        _state.Init(this, State_Idle.Instance);
    }

    protected virtual async UniTask SetSpine()
    {
        // 추후 동적으로 불러오기
        string idStr = _id.ToString("D3");
        string path = _unitType == EUnitType.Character ? $"Spine/Character/Cr_{idStr}/Cr_{idStr}_SkeletonData" :
                                                    $"Spine/Monster/Mn_{idStr}/Mn_{idStr}_SkeletonData";
        
        SkeletonDataAsset skeletonData = Resources.Load<SkeletonDataAsset>(path);     
        
        if (skeletonData == null)
        {
            Debug.Log("Not Find Spine : ");
            return;
        }
        _spineAnim.SetSpine(skeletonData);

    }

    public virtual void SetAnimation(EState state)
    {
        bool loop = GetLoopByState(state);
        string animName = GetAnimationName(state);
        _spineAnim.SetAnimation(animName, loop);
    }


    private string GetAnimationName(EState state)
    {
        string animName = string.Empty;
        switch (state)
        {
            case EState.Idle:
                animName = _unitType == EUnitType.Character ?  CharacterAnimationName.IDLE : ObjectAnimationName.IDLE;
                break;
            case EState.Move:
                animName = _unitType == EUnitType.Character ?  CharacterAnimationName.MOVE : ObjectAnimationName.MOVE;
                break;
            case EState.Attack:
                animName = _unitType == EUnitType.Character ? CharacterAnimationName.ATTACK : ObjectAnimationName.ATTACK;
                break;
            case EState.Die:
                animName =  _unitType == EUnitType.Character ? CharacterAnimationName.DIE : ObjectAnimationName.DIE;
                break;
            default:
                break;
        }

        return animName;
    }

    protected virtual bool GetLoopByState(EState state)
    {
        return state switch
        {
            EState.Idle => true,
            EState.Move => true,
            EState.Die => true,
            _ => false
        };
    }
}
