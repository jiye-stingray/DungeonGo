using Cysharp.Threading.Tasks;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Define;
using static Utils;

public class BaseUnit : MonoBehaviour
{
    StateMachine<BaseUnit> _state = new StateMachine<BaseUnit>();

    public SpineAnimation _spineAnim;

    private void Awake()
    {
        _spineAnim = GetOrAddComponent<SpineAnimation>(gameObject);
    }

    private void Start()
    {
        // 임시 선언 추후 factory
        Init();
    }

    public virtual void Init()
    {
        SetSpine().Forget();

        if (_state == null)
            _state = new StateMachine<BaseUnit>();
        _state.Init(this, State_Idle.Instance);
    }

    protected virtual async UniTask SetSpine()
    {
        // 추후 동적으로 불러오기
        SkeletonDataAsset skeletonData = Resources.Load<SkeletonDataAsset>("Spine/Cr_001/Cr_001_SkeletonData");       
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
                animName = CharacterAnimationName.IDLE;
                break;
            case EState.Move:
                animName = CharacterAnimationName.MOVE;
                break;
            case EState.Attack:
                animName = CharacterAnimationName.ATTACK;
                break;
            case EState.Die:
                animName = CharacterAnimationName.DIE;
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
