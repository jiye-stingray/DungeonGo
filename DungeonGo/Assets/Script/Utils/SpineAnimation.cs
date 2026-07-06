using System;
using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class SpineAnimation : MonoSpineController
{
    public void SetAnimation(string animationName, bool loop, AnimationState.TrackEntryDelegate _event = null, float delay = 0, float mixDuration = 0)
    {
        if (AnimationState == null)
            return;
        
        TrackEntry entry = AnimationState.SetAnimation(0, animationName, loop);

        entry.MixDuration = mixDuration;
        entry.Delay = delay;

        if (_event != null)
            entry.Complete += _event;
    }

    public void AddAnimation(string animationName, bool loop, AnimationState.TrackEntryDelegate _event = null, float delay = 0)
    {
        TrackEntry entry = AnimationState.AddAnimation(0, animationName, loop, delay); ;

        if (_event != null)
            entry.Complete += _event;
    }

    public void SetTimeScale(float time)
    {
        AnimationState.TimeScale = time;
    }

    public void EnableSkeleton(bool state)
    {
        if(IsSkeletonGraphic)
        {
            SkeletonGraphic.enabled = state;
        }
        else
        {
            SkeletonAnimation.enabled = state;
        }
    }

    public void SetFlip(bool state)
    {
        Skeleton.ScaleX = state ? -1.0f : 1.0f;
    }

    public Spine.Animation FindAnimation(string animationName)
    {
        Spine.Animation anim = Skeleton.Data.FindAnimation(animationName);
        return anim;
    }

    public void SetSkin(string skinName)
    {
        Skeleton.SetSkin(skinName);
        Skeleton.SetSlotsToSetupPose();
    }

    public void SetAttachment(string slotName, string attachmentName)
    {
        Skeleton.SetAttachment(slotName, attachmentName);
        //Skeleton.SetSlotsToSetupPose();
    }

    public void ClearAttachment(string slotName)
    {
        Slot slot = GetSlot(slotName);
        slot.Attachment = null;
    }

    public void SetSlotAlpha(string slotName, float alpha)
    {
        Slot slot = Skeleton.FindSlot(slotName);
        if (slot != null)
        {
            slot.A = alpha;
        }
    }

    public Slot GetSlot(string slotName)
    {
        return Skeleton.FindSlot(slotName);
    }
    
    public Bone FindBone(string boneName)
    {
        return Skeleton.FindBone(boneName);
    }

    public void SetSortingOrder(int order)
    {
        if(IsSkeletonGraphic)
        {
            SkeletonGraphic.canvas.sortingOrder = order;
        }
        else
        {
            var renderer = SkeletonAnimation.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.sortingOrder = order;
        }
    }

    public void SpineClear()
    {
        if (SkeletonAnimation == null && SkeletonGraphic == null)
            return;
        
        SkeletonDataAsset.Clear();
    }

    public void SetAlpha(float alpha)
    {
        Skeleton.A = alpha;
    }

    public void DoTweenFade(float alpha, float duration, Action completeAction = null)
    {
        DOTween.To(() => Skeleton.A, (a) => { Skeleton.A = a; }, alpha, duration)
                    .OnComplete(() =>
                     {
                         if (completeAction != null)
                             completeAction();
                     });
    }

    public void AnimationStop()
    {
        AnimationState.TimeScale = 0.0f;
    }
    
    public void AnimationStart()
    {
        AnimationState.TimeScale = 1.0f;
    }

    private void OnDestroy()
    {
        SpineClear();
    }
}
