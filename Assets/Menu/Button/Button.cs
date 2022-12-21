using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Button : MonoBehaviour
{
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		AnimatorControllerParameter parameter;
		for (int i = 0; i < _animator.parameterCount; i++)
		{
			parameter = _animator.GetParameter(0);
			if (parameter.type == AnimatorControllerParameterType.Trigger)
			{
				_animator.ResetTrigger(parameter.name);
			}
		}
	}
}
