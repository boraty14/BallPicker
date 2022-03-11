using System;
using System.Collections.Generic;
using Core;
using Level;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class UIManager : MonoBehaviourSingleton<UIManager>
    {
	    [SerializeField] private List<Panel> panels = new List<Panel>();

        [Space, Header("Level Texts")]
		[SerializeField] private TextMeshProUGUI winLevelText = null;
		[SerializeField] private TextMeshProUGUI loseLevelText = null;
		[SerializeField] private TextMeshProUGUI levelText = null;
		
		

		private void OnEnable()
		{
			EventBus.OnLevelWin += OnLevelWin;
			EventBus.OnLevelLose += OnLevelLose;
			EventBus.OnLevelReset += OnLevelReset;
			EventBus.OnTapToPlay += OnTapToPlay;
		}

		private void OnDisable()
		{
			EventBus.OnLevelWin -= OnLevelWin;
			EventBus.OnLevelLose -= OnLevelLose;
			EventBus.OnLevelReset -= OnLevelReset;
			EventBus.OnTapToPlay -= OnTapToPlay;
		}

		private void OnTapToPlay()
		{
			ShowPanel(PanelType.Gameplay);
		}

		private void OnLevelReset()
		{
			HidePanel(PanelType.All);
			UpdateLevelTexts();
			ShowPanel(PanelType.TapToPlay);
		}

		private void OnLevelWin()
		{
			UpdateLevelTexts();
			ShowPanel(PanelType.Win);
		}

		private void OnLevelLose()
		{
			UpdateLevelTexts();
			ShowPanel(PanelType.Lose);
		}

		private void ShowPanel(PanelType panelType, bool hideOthers = true)
		{
			panels.ForEach(p =>
			{
				if (p.panelType == panelType || panelType == PanelType.All)
					p.Show();
				else if (hideOthers)
					p.Hide();
			});
		}
		

		private void HidePanel(PanelType panelType)
		{
			panels.ForEach(p =>
			{
				if (p.panelType == panelType || panelType == PanelType.All)
					p.Hide();
			});
		}
		
		private void UpdateLevelTexts()
		{
			int level = PlayerPrefs.GetInt(LevelManager.PrefsLevelKey) + 1;
		
			if (winLevelText)
			{
				winLevelText.text = $"Level {level} Completed";
			}

			if (loseLevelText)
			{
				loseLevelText.text = $"Level {level} Failed";
			}
			if (levelText)
			{
				levelText.text = $"Level {level}";
			}
		}
    }

	public enum FadeType
	{
		In,
		Out
	}

	[Serializable]
	public class Panel
	{
		public GameObject panelObject;
		public PanelType panelType = PanelType.None;

		public void Show() => panelObject.SetActive(true);
		public void Hide() => panelObject.SetActive(false);
	}
	
	public enum PanelType
	{
		None,
		All,
		TapToPlay,
		Gameplay,
		Win,
		Lose,
		Tutorial,
	}
}