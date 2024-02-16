using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StatusBar : MonoBehaviour, IBar
{
	private Slider _slider;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
	}

	public void SetValue(float value, float maxValue)
	{
		_slider.value = value / maxValue;
	}
}

public interface IBar
{
	public void SetValue(float value, float maxValue);
}
