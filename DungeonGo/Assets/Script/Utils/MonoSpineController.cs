using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

[RequireComponent(typeof(ISkeletonAnimation))]
public class MonoSpineController : MonoBehaviour
{ 
    public SkeletonAnimation SkeletonAnimation { get; private set; }
    public SkeletonGraphic SkeletonGraphic { get; private set; }
    public ISkeletonAnimation ISkeletonAnimation { get; private set; }
    public IAnimationStateComponent IAnimationStateComponent { get; private set; }

    public SkeletonDataAsset SkeletonDataAsset
    {
        get
        {
            return IsSkeletonGraphic ? SkeletonGraphic.SkeletonDataAsset : SkeletonAnimation.SkeletonDataAsset;
        }
        set
        {
            if (IsSkeletonGraphic)
            {
                SkeletonGraphic.skeletonDataAsset = value;
                SkeletonGraphic.Initialize(true);
            }
            else
            {
                SkeletonAnimation.skeletonDataAsset = value;
                SkeletonAnimation.Initialize(true);
            }
        }
    }
    
    public Skeleton Skeleton => ISkeletonAnimation.Skeleton;
    public AnimationState AnimationState => IAnimationStateComponent.AnimationState;

    public bool IsSkeletonGraphic { get; protected set; }

    private bool _initialized = false;

    public void Initialize()
    {
        if (_initialized)
            return;
        
        _initialized = true;

        if (!TryGetComponent<ISkeletonAnimation>(out var iSkeletonAnimation))
        {
            Debug.LogError("ISkeletonAnimation Component is not attached");
            return;
        }

        if (iSkeletonAnimation is SkeletonAnimation skeletonAnimation)
        {
            SkeletonAnimation = skeletonAnimation;
            ISkeletonAnimation = skeletonAnimation;
            IAnimationStateComponent = skeletonAnimation;
            SkeletonAnimation.Initialize(false);
        }
        else if (iSkeletonAnimation is SkeletonGraphic skeletonGraphic)
        {
            IsSkeletonGraphic = true;
            SkeletonGraphic = skeletonGraphic;
            ISkeletonAnimation = skeletonGraphic;
            IAnimationStateComponent = skeletonGraphic;
            SkeletonGraphic.Initialize(false);
        }
    }

    public void SetSpine(SkeletonDataAsset asset)
    {
        Initialize();

        // if (SkeletonDataAsset != null)
        //     SkeletonDataAsset.Clear();
        SkeletonDataAsset = asset;
        
#if UNITY_EDITOR
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true))
        {
            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                Material mat = materials[i];

                if (mat == null)
                    continue;

                string shaderName = mat.shader != null ? mat.shader.name : string.Empty;

                if (string.IsNullOrEmpty(shaderName))
                {
                    Debug.LogError($"Shader name empty: {mat.name}", mat);
                    continue;
                }

                Shader shader = Shader.Find(shaderName);

                if (shader == null)
                {
                    Debug.LogError($"Shader not found: {shaderName} / Material: {mat.name}", mat);
                    continue;
                }

                mat.shader = shader;
            }
        }
#endif
    }
}