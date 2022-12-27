using System;
using UnityEditor.Animations;
using UnityEngine;

public static class AnimatorGenerator
{
    public static void GenerateAnimatorControllerFromExist(this AnimatorController animatorController, Enum animations,
        bool clearParameters = false)
    {
        var animationsArray = Enum.GetNames(animations.GetType());
        if (clearParameters) animatorController.parameters = null;
        foreach (var animationName in animationsArray)
        {
            animatorController.AddParameter(animationName, AnimatorControllerParameterType.Bool);
        }

        animatorController.GenerateStates(animationsArray);
        animatorController.GenerateTransitions(animationsArray);
    }

    private static void GenerateStates(this AnimatorController animatorController, string[] animations)
    {
        var rootStateMachine = animatorController.layers[0].stateMachine;
        rootStateMachine.states = null;
        foreach (var animation in animations)
        {
            rootStateMachine.AddState(animation);
        }
    }

    private static void GenerateTransitions(this AnimatorController animatorController, string[] animations)
    {
        var rootStateMachine = animatorController.layers[0].stateMachine;
        var states = rootStateMachine.states;

        for (int i = 0; i < states.Length; i++)
        {
            for (int k = 0; k < states.Length; k++)
            {
                if (k == i) continue;
                var transition = states[k].state.AddTransition(states[i].state);
                transition.AddCondition(AnimatorConditionMode.If, 0, states[i].state.name);
            }
        }
        // foreach (var animation in animations)
        // {
        //     foreach (var subAnimation in animations)
        //     {
        //     }
        // }
    }
}