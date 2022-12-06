using System;
using System.Collections;
using UnityEngine;

public static class AnimatorExtensions
{
    public static Animator PlayStateAnimation<T>(this Animator animator, T animationType) where T : Enum
    {
        animator.SetInteger(AnimatorConstants.State, Convert.ToInt32(animationType));
        return animator;
    }

    public static void AddOnComplete(this Animator animator, Action onComplete)
    {
        Toolbox.Get<CoroutinesProcessor>().PlayCoroutine(0, animator.CheckAnimationCompleted(onComplete));
    }

    private static IEnumerator CheckAnimationCompleted(this Animator animator, Action onComplete)
    {
        yield return new WaitForSeconds(0.1f);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
            yield return null;
        onComplete?.Invoke();
    }
}