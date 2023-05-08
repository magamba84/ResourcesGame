using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerActivator : MonoBehaviour
{
	[SerializeField]
	private MonoBehaviour target;

	[SerializeField]
	private float timeout = 1f;

	private IWorkable workableTarget;

	private Coroutine timerCoroutine;

	private void Start()
	{
		workableTarget = target as IWorkable;
		StartCoroutine(MainTimer());
	}

	public void StartWork()
	{
		StopWork();
		timerCoroutine = StartCoroutine(MainTimer());
	}

	public void StopWork()
	{
		if (timerCoroutine != null)
			StopCoroutine(timerCoroutine);

		timerCoroutine = null;
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
