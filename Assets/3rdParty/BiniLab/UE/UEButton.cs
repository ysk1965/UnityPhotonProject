﻿/*********************************************
 * NHN StarFish - UI Extends
 * CHOI YOONBIN
 * 
 *********************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum UEReactionType
{
	None  = 0,
	Jelly = 1,
	Punch = 11,
}

public class UEButton : Button {

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// overrides MonoBehaviour
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		this.localScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(!this.tweenScaleX.Completed)
		{
			float delteTime = Time.unscaledDeltaTime;
			this.tweenScaleX.Update(delteTime);
			this.tweenScaleY.Update(delteTime);

			float scaleX = Mathf.Round(this.tweenScaleXValue.Value / .01f ) * .01f;
			float scaleY = Mathf.Round(this.tweenScaleYValue.Value / .01f ) * .01f;
			Vector3 scale = new Vector3(scaleX, scaleY, 1f);
			this.transform.localScale = scale;
		}
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		if(!Application.isPlaying) return;

		if(this.localScale == Vector3.zero)
			this.localScale = this.transform.localScale;
		else
			this.transform.localScale = this.localScale;
	}

	protected override	void OnDisable()
	{
		base.OnDisable();
		if(!Application.isPlaying) return;

		this.tweenScaleX.Kill ();
		this.tweenScaleY.Kill ();
		this.transform.localScale = this.localScale;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////
	// public overrides Button

	public override void OnPointerDown (UnityEngine.EventSystems.PointerEventData eventData)
	{
		if(this.reactionType == UEReactionType.None) return;
		base.OnPointerDown (eventData);

		this.clickInvoked = false;
		this.cancelClick = false;
		this.buttonPressed = true;

		switch(this.reactionType)
		{
		case UEReactionType.Jelly:
			this.JellyTweenReady();
			break;
		case UEReactionType.Punch:
			this.punchTweenReady();
			break;
		}
	}

	public override void OnPointerUp (UnityEngine.EventSystems.PointerEventData eventData)
	{
		if(this.reactionType == UEReactionType.None) return;
		if(this.cancelClick) return;

		base.OnPointerUp (eventData);
		this.buttonPressed = false;
		switch(this.reactionType)
		{
		case UEReactionType.Jelly:
			this.StartCoroutine(this.JellyTweenStart());
			break;
		case UEReactionType.Punch:
			this.BackToNormal();
			break;
		}

		this.ForceClick();
	}

	public override void OnPointerExit (UnityEngine.EventSystems.PointerEventData eventData)
	{
		if(this.reactionType == UEReactionType.None) return;
		base.OnPointerExit (eventData);

		if(this.buttonPressed)
		{
			this.cancelClick = true;
			this.buttonPressed = false;
			this.BackToNormal();
		}
	}

	public override void OnPointerClick (UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (this.reactionType != UEReactionType.None) return;

		this.clickInvoked = false;
		this.ForceClick ();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// public

	public UEReactionType ReactionType
	{
		get { return this.reactionType; }
		set { this.reactionType = value; }
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	// private

	[SerializeField] private UEReactionType reactionType = UEReactionType.None;

	private Vector3 localScale;

	private SimpleTweener tweenScaleX = new SimpleTweener();
	private TweenLerp<float> tweenScaleXValue;

	private SimpleTweener tweenScaleY = new SimpleTweener();
	private TweenLerp<float> tweenScaleYValue;

	private bool cancelClick = false;
	private bool clickInvoked = false;
	private bool buttonPressed = false;

	private void ForceClick()
	{
		if (!this.cancelClick && !this.clickInvoked) {
			this.clickInvoked = true;
			this.onClick.Invoke ();
		}
	}

	private void JellyTweenReady()
	{
		this.tweenScaleX.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (1f, 1.248f);
		
		this.tweenScaleY.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (1f, 0.904f);
	}

	private void punchTweenReady()
	{
		this.tweenScaleX.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (1f, 1.1f);
		
		this.tweenScaleY.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (1f, 1.1f);
	}

	private void BackToNormal()
	{
		this.tweenScaleX.Reset (0.2f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (this.tweenScaleXValue.Value, 1f);
		
		this.tweenScaleY.Reset (0.2f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (this.tweenScaleYValue.Value, 1f);
	}
	
	private IEnumerator JellyTweenStart()
	{
		this.tweenScaleX.Reset (0.1f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (1.248f, 0.92f);

		this.tweenScaleY.Reset (0.1f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (0.904f, 1.12f);

		yield return StartCoroutine (this.WaitForRealSeconds (0.15f));

		this.tweenScaleX.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (0.92f, 1.112f);
		
		this.tweenScaleY.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (1.12f, 0.888f);
		
		yield return StartCoroutine (this.WaitForRealSeconds (0.15f));

		this.tweenScaleX.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (1.112f, 0.944f);
		
		this.tweenScaleY.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (0.904f, 1f);
		
		yield return StartCoroutine (this.WaitForRealSeconds (0.15f));

		this.tweenScaleX.Reset (0.2f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (0.944f, 1.04f);

		this.tweenScaleY.Reset (0.2f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (1f, 0.976f);

		yield return StartCoroutine (this.WaitForRealSeconds (0.2f));

		this.tweenScaleX.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleXValue = this.tweenScaleX.CreateTween (1.04f, 1f);
		
		this.tweenScaleY.Reset (0.15f, EasingObject.LinearEasing);
		this.tweenScaleYValue = this.tweenScaleY.CreateTween (0.976f, 1f);

		yield break;
	}

	private IEnumerator WaitForRealSeconds (float seconds)
	{
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < seconds)
		{
			yield return null;
		}
	}
}
