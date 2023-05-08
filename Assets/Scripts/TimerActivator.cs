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
	}

	public void StopWork()
	{
		if (timerCoroutine != null)
			StopCoroutine(timerCoroutine);

		timerCoroutine = null;
		isWorking = false;
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
