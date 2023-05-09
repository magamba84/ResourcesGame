using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerActivator : MonoBehaviour
{
	[SerializeField]
	private MonoBehaviour target;

	[SerializeField]
	private float timeout = 1f;

	[SerializeField]
	private Animator animator;

	private IWorkable workableTarget;

	private Coroutine timerCoroutine;

	private bool isWorking;
	public bool IsWorking 
	{
		get { return isWorking; }
	}

	private void Start()
	{
		workableTarget = target as IWorkable;
	}

	public void StartWork()
	{
		StopWork();
		timerCoroutine = StartCoroutine(MainTimer());
		isWorking = true;

		if (animator != null)
			animator.SetFloat("speed", 1);
	}

	public void StopWork()
	{
		if (timerCoroutine != null)
			StopCoroutine(timerCoroutine);

		timerCoroutine = null;
		isWorking = false;

		if (animator != null)
			animator.SetFloat("speed", 0);
	}

	private IEnumerator MainTimer()
	{
		do
		{
			if (workableTarget != null)
				workableTarget.DoWork();

			yield return new WaitForSeconds(timeout);
		} while (true);
	}
}
