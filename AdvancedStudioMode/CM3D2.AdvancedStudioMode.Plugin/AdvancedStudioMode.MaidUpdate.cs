using ExIni;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CM3D2.AdvancedStudioMode.Plugin
{
    public partial class AdvancedStudioMode
	{
		private void MaidUpdate()
		{
			if (isHaiti)
			{
				for (int i = 0; i < maxMaidCnt; i++)
				{
					if (gHandL[i])
					{
						HandL1[i] = null;
						Object.Destroy(gHandL[i]);
						Object.Destroy(gArmL[i]);
						Object.Destroy(gFootL[i]);
						Object.Destroy(gHizaL[i]);
						Object.Destroy(gHandR[i]);
						Object.Destroy(gArmR[i]);
						Object.Destroy(gFootR[i]);
						Object.Destroy(gHizaR[i]);
						Object.Destroy(gClavicleL[i]);
						Object.Destroy(gClavicleR[i]);
						Object.Destroy(gNeck[i]);
						Object.Destroy(gSpine[i]);
						Object.Destroy(gSpine0a[i]);
						Object.Destroy(gSpine1a[i]);
						Object.Destroy(gSpine1[i]);
						Object.Destroy(gPelvis[i]);
					}
				}
				for (int j = 0; j < maidCnt; j++)
				{
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					if (selectList.Count <= 7)
					{
						if (selectList.Count % 2 == 1)
						{
							switch (j)
							{
								case 0:
									maidArray[j].SetPos(new Vector3(0f, 0f, 0f));
									break;
								case 1:
									maidArray[j].SetPos(new Vector3(-0.6f, 0f, 0.26f));
									break;
								case 2:
									maidArray[j].SetPos(new Vector3(0.6f, 0f, 0.26f));
									break;
								case 3:
									maidArray[j].SetPos(new Vector3(-1.1f, 0f, 0.69f));
									break;
								case 4:
									maidArray[j].SetPos(new Vector3(1.1f, 0f, 0.69f));
									break;
								case 5:
									maidArray[j].SetPos(new Vector3(-1.47f, 0f, 1.1f));
									break;
								case 6:
									maidArray[j].SetPos(new Vector3(1.47f, 0f, 1.1f));
									break;
							}
						}
						else
						{
							switch (j)
							{
								case 0:
									maidArray[j].SetPos(new Vector3(0.3f, 0f, 0f));
									break;
								case 1:
									maidArray[j].SetPos(new Vector3(-0.3f, 0f, 0f));
									break;
								case 2:
									maidArray[j].SetPos(new Vector3(0.7f, 0f, 0.4f));
									break;
								case 3:
									maidArray[j].SetPos(new Vector3(-0.7f, 0f, 0.4f));
									break;
								case 4:
									maidArray[j].SetPos(new Vector3(1f, 0f, 0.9f));
									break;
								case 5:
									maidArray[j].SetPos(new Vector3(-1f, 0f, 0.9f));
									break;
							}
						}
					}
					else
					{
						float num = 0f;
						if (selectList.Count >= 11)
						{
							num = -0.4f;
							if (selectList.Count % 2 == 1)
							{
								switch (j)
								{
									case 0:
										maidArray[j].SetPos(new Vector3(0f, 0f, 0f + num));
										break;
									case 1:
										maidArray[j].SetPos(new Vector3(-0.5f, 0f, 0.2f + num));
										break;
									case 2:
										maidArray[j].SetPos(new Vector3(0.5f, 0f, 0.2f + num));
										break;
									case 3:
										maidArray[j].SetPos(new Vector3(-0.9f, 0f, 0.55f + num));
										break;
									case 4:
										maidArray[j].SetPos(new Vector3(0.9f, 0f, 0.55f + num));
										break;
									case 5:
										maidArray[j].SetPos(new Vector3(-1.25f, 0f, 0.9f + num));
										break;
									case 6:
										maidArray[j].SetPos(new Vector3(1.25f, 0f, 0.9f + num));
										break;
									case 7:
										maidArray[j].SetPos(new Vector3(-1.57f, 0f, 1.3f + num));
										break;
									case 8:
										maidArray[j].SetPos(new Vector3(1.57f, 0f, 1.3f + num));
										break;
									case 9:
										maidArray[j].SetPos(new Vector3(-1.77f, 0f, 1.72f + num));
										break;
									case 10:
										maidArray[j].SetPos(new Vector3(1.77f, 0f, 1.72f + num));
										break;
									case 11:
										maidArray[j].SetPos(new Vector3(-1.85f, 0f, 2.17f + num));
										break;
									case 12:
										maidArray[j].SetPos(new Vector3(1.85f, 0f, 2.17f + num));
										break;
									default:
										maidArray[j].SetPos(new Vector3(0f, 0f, 0.7f + num));
										break;
								}
							}
							else
							{
								switch (j)
								{
									case 0:
										maidArray[j].SetPos(new Vector3(0.25f, 0f, 0f + num));
										break;
									case 1:
										maidArray[j].SetPos(new Vector3(-0.25f, 0f, 0f + num));
										break;
									case 2:
										maidArray[j].SetPos(new Vector3(0.7f, 0f, 0.25f + num));
										break;
									case 3:
										maidArray[j].SetPos(new Vector3(-0.7f, 0f, 0.25f + num));
										break;
									case 4:
										maidArray[j].SetPos(new Vector3(1.05f, 0f, 0.6f + num));
										break;
									case 5:
										maidArray[j].SetPos(new Vector3(-1.05f, 0f, 0.6f + num));
										break;
									case 6:
										maidArray[j].SetPos(new Vector3(1.35f, 0f, 0.9f + num));
										break;
									case 7:
										maidArray[j].SetPos(new Vector3(-1.35f, 0f, 0.9f + num));
										break;
									case 8:
										maidArray[j].SetPos(new Vector3(1.6f, 0f, 1.3f + num));
										break;
									case 9:
										maidArray[j].SetPos(new Vector3(-1.6f, 0f, 1.3f + num));
										break;
									case 10:
										maidArray[j].SetPos(new Vector3(1.8f, 0f, 1.72f + num));
										break;
									case 11:
										maidArray[j].SetPos(new Vector3(-1.8f, 0f, 1.72f + num));
										break;
									case 12:
										maidArray[j].SetPos(new Vector3(1.9f, 0f, 2.17f + num));
										break;
									case 13:
										maidArray[j].SetPos(new Vector3(-1.9f, 0f, 2.17f + num));
										break;
									default:
										maidArray[j].SetPos(new Vector3(0f, 0f, 0.7f + num));
										break;
								}
							}
						}
						else if (selectList.Count >= 8)
						{
							if (selectList.Count >= 9)
							{
								num = -0.2f;
							}
							if (selectList.Count % 2 == 1)
							{
								switch (j)
								{
									case 0:
										maidArray[j].SetPos(new Vector3(0f, 0f, 0f + num));
										break;
									case 1:
										maidArray[j].SetPos(new Vector3(-0.55f, 0f, 0.2f + num));
										break;
									case 2:
										maidArray[j].SetPos(new Vector3(0.55f, 0f, 0.2f + num));
										break;
									case 3:
										maidArray[j].SetPos(new Vector3(-1f, 0f, 0.6f + num));
										break;
									case 4:
										maidArray[j].SetPos(new Vector3(1f, 0f, 0.6f + num));
										break;
									case 5:
										maidArray[j].SetPos(new Vector3(-1.35f, 0f, 1f + num));
										break;
									case 6:
										maidArray[j].SetPos(new Vector3(1.35f, 0f, 1f + num));
										break;
									case 7:
										maidArray[j].SetPos(new Vector3(-1.6f, 0f, 1.4f + num));
										break;
									case 8:
										maidArray[j].SetPos(new Vector3(1.6f, 0f, 1.4f + num));
										break;
								}
							}
							else
							{
								switch (j)
								{
									case 0:
										maidArray[j].SetPos(new Vector3(0.28f, 0f, 0f + num));
										break;
									case 1:
										maidArray[j].SetPos(new Vector3(-0.28f, 0f, 0f + num));
										break;
									case 2:
										maidArray[j].SetPos(new Vector3(0.78f, 0f, 0.3f + num));
										break;
									case 3:
										maidArray[j].SetPos(new Vector3(-0.78f, 0f, 0.3f + num));
										break;
									case 4:
										maidArray[j].SetPos(new Vector3(1.22f, 0f, 0.7f + num));
										break;
									case 5:
										maidArray[j].SetPos(new Vector3(-1.22f, 0f, 0.7f + num));
										break;
									case 6:
										maidArray[j].SetPos(new Vector3(1.55f, 0f, 1.1f + num));
										break;
									case 7:
										maidArray[j].SetPos(new Vector3(-1.55f, 0f, 1.1f + num));
										break;
									case 8:
										maidArray[j].SetPos(new Vector3(1.77f, 0f, 1.58f + num));
										break;
									case 9:
										maidArray[j].SetPos(new Vector3(-1.77f, 0f, 1.58f + num));
										break;
								}
							}
						}
					}
					zero2.y = (float)(Math.Atan2((double)maidArray[j].transform.position.x, (double)(maidArray[j].transform.position.z - 1.5f)) * 180.0 / 3.1415926535897931) + 180f;
					maidArray[j].SetRot(zero2);
				}
				isHaiti = false;
			}
			if (isYobidashi)
			{
				bool flag = false;
				for (int j = 0; j < maxMaidCnt; j++)
				{
					if (selectList.Count > j && maidArray[j] != null && maidArray[j].IsBusy)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					for (int k = 0; k < selectList.Count; k++)
					{
						if ((k == maxMaidCnt - 1 || (k < maxMaidCnt - 1 && maidArray[k + 1] == null)) && maidArray[k] == null)
						{
							if ((k != 0 || !(maidArray[k + 1] == null) || !(maidArray[k] == null)) && (k <= 0 || !(maidArray[k - 1] != null) || maidArray[k - 1].IsBusy))
							{
								return;
							}
							if ((int)selectList[k] >= 12)
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.GetStockMaid((int)selectList[k]);
								if (!maidArray[k].body0.isLoadedBody)
								{
									maidArray[k].DutPropAll();
									maidArray[k].AllProcPropSeqStart();
								}
								maidArray[k].Visible = true;
							}
							else if (sceneLevel != 5)
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.Activate((int)selectList[k], (int)selectList[k], false, false);
								maidArray[k] = GameMain.Instance.CharacterMgr.CharaVisible((int)selectList[k], true, false);
							}
							else if (k == 0 && (int)selectList[k] == 0)
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.Activate((int)selectList[k], (int)selectList[k], false, false);
								maidArray[k] = GameMain.Instance.CharacterMgr.CharaVisible((int)selectList[k], true, false);
							}
							else if (k == 0)
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.Activate(0, (int)selectList[k], false, false);
								maidArray[k] = GameMain.Instance.CharacterMgr.CharaVisible(0, true, false);
							}
							else if ((int)selectList[k] + 1 == 12)
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.GetStockMaid((int)selectList[k]);
								if (!maidArray[k].body0.isLoadedBody)
								{
									maidArray[k].DutPropAll();
									maidArray[k].AllProcPropSeqStart();
								}
								maidArray[k].Visible = true;
							}
							else
							{
								maidArray[k] = GameMain.Instance.CharacterMgr.Activate((int)selectList[k] + 1, (int)selectList[k], false, false);
								maidArray[k] = GameMain.Instance.CharacterMgr.CharaVisible((int)selectList[k] + 1, true, false);
							}
							if (maidArray[k] && maidArray[k].Visible)
							{
								maidArray[k].body0.boHeadToCam = true;
								maidArray[k].body0.boEyeToCam = true;
							}
						}
					}
					isHaiti = true;
					isYobidashi = false;
				}
			}
			if (sceneLevel == 5 && !isFadeOut && Input.GetKeyDown(KeyCode.F7) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
			{
				if (!isF7S)
				{
					isF7S = true;
					isF7SInit = true;
					bGui = false;
					base.Preferences["config"]["shift_f7"].Value = "true";
					base.SaveConfig();
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					bgmCombo.selectedItemIndex = 2;
				}
				else
				{
					isF7S = false;
					base.Preferences["config"]["shift_f7"].Value = "false";
					base.SaveConfig();
					if (!isF7)
					{
						init2();
						okFlg = false;
						bGui = false;
					}
					GameMain.Instance.SoundMgr.PlaySe("se003.ogg", false);
				}
			}
			else if (sceneLevel == 5 && !isFadeOut && Input.GetKeyDown(KeyCode.F7))
			{
				if (isF7S && !isF7)
				{
					okFlg = true;
					faceFlg = false;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					bGui = true;
					isGuiInit = true;
					isF7 = true;
					bgmCombo.selectedItemIndex = 2;
				}
				else if (isF7S && isF7)
				{
					bGui = false;
					isF7 = false;
				}
				else if (!isF7)
				{
					init();
					okFlg = true;
					isGuiInit = true;
					comboBoxControl.selectedItemIndex = 0;
					comboBoxList = new GUIContent[11];
					comboBoxList[0] = new GUIContent("通常");
					comboBoxList[1] = new GUIContent("横一列");
					comboBoxList[2] = new GUIContent("縦一列");
					comboBoxList[3] = new GUIContent("斜め");
					comboBoxList[4] = new GUIContent("円（外向き）");
					comboBoxList[5] = new GUIContent("円（内向き）");
					comboBoxList[6] = new GUIContent("扇");
					comboBoxList[7] = new GUIContent("Ｖ");
					comboBoxList[8] = new GUIContent("^");
					comboBoxList[9] = new GUIContent("Ｍ");
					comboBoxList[10] = new GUIContent("Ｗ");
					listStyle.normal.textColor = Color.white;
					listStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
					listStyle.onHover.background = (listStyle.hover.background = new Texture2D(2, 2));
					listStyle.padding.left = (listStyle.padding.right = (listStyle.padding.top = (listStyle.padding.bottom = 4)));
					listStyle.fontSize = GetPix(13);
					isYobidashi = true;
					bGui = true;
					isFadeOut = true;
					if (!isF7S)
					{
						maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
					}
					List<Maid> stockMaidList = GameMain.Instance.CharacterMgr.GetStockMaidList();
					for (int i = 0; i < stockMaidList.Count; i++)
					{
						if (maidArray[0] == stockMaidList[i])
						{
							editMaid = i;
						}
					}
					selectList = new ArrayList();
					selectList.Add(editMaid);
					bgmCombo.selectedItemIndex = 2;
					try
					{
						shodaiFlg[(int)selectList[0]] = false;
						TMorph morph = maidArray[0].body0.Face.morph;
						float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
						float num2 = fieldValue[(int)morph.hash["tangopen"]];
					}
					catch
					{
						shodaiFlg[(int)selectList[0]] = true;
					}
					if (!isVR)
					{
						eyeL[(int)selectList[0]] = maidArray[0].body0.quaDefEyeL.eulerAngles;
						eyeR[(int)selectList[0]] = maidArray[0].body0.quaDefEyeR.eulerAngles;
					}
					isF7 = true;
				}
				else if (!isF7S)
				{
					if (!isVR)
					{
						if (!isDialog)
						{
							isDialog = true;
							GameMain.Instance.SysDlg.Show("複数メイド撮影を終了します。\nよろしいですか？", SystemDialog.TYPE.OK_CANCEL, delegate ()
							{
								GameMain.Instance.SysDlg.Close();
								isDialog = false;
								init();
								maidArray[0] = GameMain.Instance.CharacterMgr.Activate(0, editMaid, false, false);
								maidArray[0] = GameMain.Instance.CharacterMgr.CharaVisible(0, true, false);
								okFlg = false;
								bGui = false;
								isF7 = false;
							}, delegate ()
							{
								GameMain.Instance.SysDlg.Close();
								isDialog = false;
							});
						}
					}
					else
					{
						init();
						maidArray[0] = GameMain.Instance.CharacterMgr.Activate(0, editMaid, false, false);
						maidArray[0] = GameMain.Instance.CharacterMgr.CharaVisible(0, true, false);
						okFlg = false;
						bGui = false;
						isF7 = false;
					}
				}
				else
				{
					isF7SInit = true;
					bGui = false;
					isF7 = false;
				}
			}
			if (sceneLevel == 5 && isF7S && isF7SInit)
			{
				isF7SInit = false;
				init2();
				okFlg = true;
				ikMaid = 0;
				ikBui = 5;
				ikMode[0] = 0;
				bgmCombo.selectedItemIndex = 2;
				maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
				List<Maid> stockMaidList = GameMain.Instance.CharacterMgr.GetStockMaidList();
				for (int i = 0; i < stockMaidList.Count; i++)
				{
					if (maidArray[0] == stockMaidList[i])
					{
						editMaid = i;
					}
				}
				selectList = new ArrayList();
				selectList.Add(editMaid);
				listStyle.normal.textColor = Color.white;
				listStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle.onHover.background = (listStyle.hover.background = new Texture2D(2, 2));
				listStyle.padding.left = (listStyle.padding.right = (listStyle.padding.top = (listStyle.padding.bottom = 4)));
				listStyle.fontSize = GetPix(13);
				try
				{
					shodaiFlg[(int)selectList[0]] = false;
					TMorph morph = maidArray[0].body0.Face.morph;
					float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
					float num2 = fieldValue[(int)morph.hash["tangopen"]];
				}
				catch
				{
					shodaiFlg[(int)selectList[0]] = true;
				}
				if (!isVR)
				{
					eyeL[(int)selectList[0]] = maidArray[0].body0.quaDefEyeL.eulerAngles;
					eyeR[(int)selectList[0]] = maidArray[0].body0.quaDefEyeR.eulerAngles;
				}
				bGui = false;
			}
			if (sceneLevel == 3 && !isFadeOut && Input.GetKeyDown(KeyCode.F7))
			{
				if (isPanel)
				{
					GameObject gameObject = GameObject.Find("UI Root");
					GameObject gameObject2 = gameObject.transform.Find("DailyPanel").gameObject;
					if (!okFlg && !gameObject2.activeSelf)
					{
						return;
					}
					bool flag2 = GameMain.Instance.CharacterMgr.status.GetFlag("時間帯") == 3;
					CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
					for (int k = 0; k < characterMgr.GetStockMaidCount(); k++)
					{
						characterMgr.GetStockMaidList()[k].Visible = false;
					}
					init();
					okFlg = true;
					isF7 = true;
					isPanel = false;
					isGuiInit = true;
					comboBoxControl.selectedItemIndex = 0;
					comboBoxList = new GUIContent[11];
					comboBoxList[0] = new GUIContent("通常");
					comboBoxList[1] = new GUIContent("横一列");
					comboBoxList[2] = new GUIContent("縦一列");
					comboBoxList[3] = new GUIContent("斜め");
					comboBoxList[4] = new GUIContent("円（外向き）");
					comboBoxList[5] = new GUIContent("円（内向き）");
					comboBoxList[6] = new GUIContent("扇");
					comboBoxList[7] = new GUIContent("Ｖ");
					comboBoxList[8] = new GUIContent("^");
					comboBoxList[9] = new GUIContent("Ｍ");
					comboBoxList[10] = new GUIContent("Ｗ");
					listStyle.normal.textColor = Color.white;
					listStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
					listStyle.onHover.background = (listStyle.hover.background = new Texture2D(2, 2));
					listStyle.padding.left = (listStyle.padding.right = (listStyle.padding.top = (listStyle.padding.bottom = 4)));
					listStyle.fontSize = GetPix(13);
					GameMain.Instance.BgMgr.ChangeBg("Theater");
					if (!isVR)
					{
						GameMain.Instance.MainCamera.Reset(0, true);
						CameraMain cameraMain = GameMain.Instance.MainCamera;
						cameraMain.SetTargetPos(new Vector3(0f, 0.9f, 0f), true);
						cameraMain.SetDistance(3f, true);
					}
					isYobidashi = true;
					bGui = false;
					isFadeOut = true;
					GameMain.Instance.MainCamera.FadeOut(0f, false, null, true, default(Color));
					selectList = new ArrayList();
					selectList.Add(0);
					GameMain.Instance.SoundMgr.PlayBGM("BGM008.ogg", 0f, true);
					bgmCombo.selectedItemIndex = 0;
					gameObject2.SetActive(isPanel);
				}
				else if (!isVR)
				{
					if (!isDialog)
					{
						isDialog = true;
						GameMain.Instance.SysDlg.Show("複数メイド撮影を終了します。\nよろしいですか？", SystemDialog.TYPE.OK_CANCEL, delegate ()
						{
							GameMain.Instance.SysDlg.Close();
							isDialog = false;
							GameObject gameObject7 = GameObject.Find("UI Root");
							GameObject gameObject8 = gameObject7.transform.Find("DailyPanel").gameObject;
							if (okFlg || gameObject8.activeSelf)
							{
								bool flag14 = GameMain.Instance.CharacterMgr.status.GetFlag("時間帯") == 3;
								CharacterMgr characterMgr2 = GameMain.Instance.CharacterMgr;
								for (int num16 = 0; num16 < characterMgr2.GetStockMaidCount(); num16++)
								{
									characterMgr2.GetStockMaidList()[num16].Visible = false;
								}
								init();
								isPanel = true;
								isF7 = false;
								bGui = false;
								GameMain.Instance.SoundMgr.PlayBGM("BGM009.ogg", 1f, true);
								if (flag14)
								{
									GameMain.Instance.BgMgr.ChangeBg("ShinShitsumu_ChairRot_Night");
								}
								else
								{
									GameMain.Instance.BgMgr.ChangeBg("ShinShitsumu_ChairRot");
								}
								if (!isVR)
								{
									GameMain.Instance.MainCamera.Reset(0, true);
									GameMain.Instance.MainCamera.SetTargetPos(new Vector3(0.5609447f, 1.380762f, -1.382336f), true);
									GameMain.Instance.MainCamera.SetDistance(1.6f, true);
									GameMain.Instance.MainCamera.SetAroundAngle(new Vector2(245.5691f, 6.273283f), true);
								}
								gameObject8.SetActive(isPanel);
							}
						}, delegate ()
						{
							GameMain.Instance.SysDlg.Close();
							isDialog = false;
						});
					}
				}
				else
				{
					GameObject gameObject = GameObject.Find("UI Root");
					GameObject gameObject2 = gameObject.transform.Find("DailyPanel").gameObject;
					if (!okFlg && !gameObject2.activeSelf)
					{
						return;
					}
					bool flag2 = GameMain.Instance.CharacterMgr.status.GetFlag("時間帯") == 3;
					CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
					for (int k = 0; k < characterMgr.GetStockMaidCount(); k++)
					{
						characterMgr.GetStockMaidList()[k].Visible = false;
					}
					init();
					isPanel = true;
					isF7 = false;
					bGui = false;
					GameMain.Instance.SoundMgr.PlayBGM("BGM009.ogg", 1f, true);
					if (flag2)
					{
						GameMain.Instance.BgMgr.ChangeBg("ShinShitsumu_ChairRot_Night");
					}
					else
					{
						GameMain.Instance.BgMgr.ChangeBg("ShinShitsumu_ChairRot");
					}
					gameObject2.SetActive(isPanel);
				}
			}
			if (!okFlg)
			{
				if (maidArray[0] && maidArray[0].Visible)
				{
					int num3 = (int)maidArray[0].transform.position.y;
					if (num3 == 100)
					{
						okFlg = true;
						Vector3 vector = Vector3.zero;
						maidArray[0].SetPos(vector);
						isScript = true;
					}
				}
				else if (maidArray[0] && !maidArray[0].Visible)
				{
					maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
				}
				else if (!maidArray[0])
				{
					maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
				}
			}
			if (okFlg)
			{
				hFlg = false;
				mFlg = false;
				fFlg = false;
				sFlg = false;
				atFlg = false;
				yFlg = false;
				escFlg = false;
				maidCnt = 0;
				if (!cameraObj && !isVR)
				{
					cameraObj = new GameObject("subCamera");
					subcamera = cameraObj.AddComponent<Camera>();
					subcamera.CopyFrom(Camera.main);
					cameraObj.SetActive(true);
					subcamera.clearFlags = CameraClearFlags.Depth;
					subcamera.cullingMask = 256;
					subcamera.depth = 1f;
					subcamera.transform.parent = mainCamera.transform;
					float num2 = 2f;
					if (Application.unityVersion.StartsWith("4"))
					{
						num2 = 1f;
					}
					GameObject gameObject3 = new GameObject("Light");
					gameObject3.AddComponent<Light>();
					lightList.Add(gameObject3);
					lightColorR.Add(1f);
					lightColorG.Add(1f);
					lightColorB.Add(1f);
					lightIndex.Add(0);
					lightX.Add(40f);
					lightY.Add(180f);
					lightAkarusa.Add(num2);
					lightKage.Add(0.098f);
					lightRange.Add(50f);
					gameObject3.transform.position = GameMain.Instance.MainLight.transform.position;
					gameObject3.GetComponent<Light>().intensity = num2;
					gameObject3.GetComponent<Light>().spotAngle = 50f;
					gameObject3.GetComponent<Light>().range = 10f;
					gameObject3.GetComponent<Light>().type = LightType.Directional;
					gameObject3.GetComponent<Light>().color = new Color(0.5f, 1f, 0f);
					gameObject3.GetComponent<Light>().cullingMask = 256;
				}
				if (getModKeyPressing(AdvancedStudioMode.modKey.Shift) && !getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && !getModKeyPressing(AdvancedStudioMode.modKey.Alt) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Y) && !Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
				{
					float axis = Input.GetAxis("Mouse ScrollWheel");
					if (axis > 0f)
					{
						mainCamera.SetDistance(mainCamera.GetDistance() - 0.5f, true);
						if (mainCamera.GetDistance() < 0.1f)
						{
							mainCamera.SetDistance(0.1f, true);
						}
					}
					else if (axis < 0f)
					{
						mainCamera.SetDistance(mainCamera.GetDistance() + 0.5f, true);
						if (mainCamera.GetDistance() > 25f)
						{
							mainCamera.SetDistance(25f, true);
						}
					}
				}
				for (int j = 0; j < maxMaidCnt; j++)
				{
					if (maidArray[j] && maidArray[j].Visible)
					{
						maidCnt++;
					}
				}
				if (maidArray[0] != null && maidArray[0].Visible)
				{
					if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						speed = 5f * Time.deltaTime * 60f;
					}
					else
					{
						speed = 1f * Time.deltaTime * 60f;
					}
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					if (!isVR || isVR2)
					{
						if (!isVR)
						{
							if (isBloom)
							{
								Bloom fieldValue2 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
								fieldValue2.enabled = true;
								fieldValue2.bloomIntensity = bloom1;
								fieldValue2.bloomBlurIterations = (int)bloom2;
								fieldValue2.bloomThreshholdColor = new Color(1f - bloom3, 1f - bloom4, 1f - bloom5);
								if (isBloomA)
								{
									fieldValue2.hdr = Bloom.HDRBloomMode.On;
								}
								else
								{
									fieldValue2.hdr = Bloom.HDRBloomMode.Auto;
								}
								isBloom2 = true;
							}
							else if (isBloom2)
							{
								isBloom2 = false;
								Bloom fieldValue2 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
								fieldValue2.enabled = true;
								fieldValue2.bloomIntensity = 2.85f;
								fieldValue2.hdr = 0;
								fieldValue2.bloomThreshholdColor = new Color(1f, 1f, 1f);
								fieldValue2.bloomBlurIterations = 3;
							}
							if (isDepth)
							{
								depth_field_.enabled = true;
								depth_field_.focalLength = depth1;
								depth_field_.focalSize = depth2;
								depth_field_.aperture = depth3;
								depth_field_.maxBlurSize = depth4;
								if (isDepthA)
								{
									depth_field_.visualizeFocus = true;
								}
								else
								{
									depth_field_.visualizeFocus = false;
								}
							}
							else
							{
								if (depth_field_ == null)
								{
									depth_field_ = GameMain.Instance.MainCamera.gameObject.AddComponent<DepthOfFieldScatter>();
									if (depth_field_.dofHdrShader == null)
									{
										depth_field_.dofHdrShader = Shader.Find("Hidden/Dof/DepthOfFieldHdr");
									}
									if (depth_field_.dx11BokehShader == null)
									{
										depth_field_.dx11BokehShader = Shader.Find("Hidden/Dof/DX11Dof");
									}
									if (depth_field_.dx11BokehTexture == null)
									{
										depth_field_.dx11BokehTexture = (Resources.Load("Textures/hexShape") as Texture2D);
									}
								}
								depth_field_.enabled = false;
							}
							if (isFog)
							{
								if (fog_.fogShader == null)
								{
									fog_.fogShader = Shader.Find("Hidden/GlobalFog");
								}
								fog_.enabled = true;
								fog_.startDistance = fog1;
								fog_.globalDensity = fog2;
								fog_.heightScale = fog3;
								fog_.height = fog4;
								fog_.globalFogColor.r = fog5;
								fog_.globalFogColor.g = fog6;
								fog_.globalFogColor.b = fog7;
							}
							else
							{
								if (fog_ == null)
								{
									fog_ = GameMain.Instance.MainCamera.gameObject.AddComponent<GlobalFog>();
								}
								fog_.enabled = false;
							}
							if (isSepia != isSepian)
							{
								isSepia = isSepian;
								if (isSepia)
								{
									if (sepia_tone_.shader == null)
									{
										sepia_tone_.shader = Shader.Find("Hidden/Sepiatone Effect");
									}
									sepia_tone_.enabled = true;
								}
								else
								{
									if (sepia_tone_ == null)
									{
										sepia_tone_ = GameMain.Instance.MainCamera.gameObject.AddComponent<SepiaToneEffect>();
									}
									sepia_tone_.enabled = false;
								}
							}
							if (bokashi > 0f)
							{
								Blur component = GameMain.Instance.MainCamera.gameObject.GetComponent<Blur>();
								component.enabled = true;
								component.blurSize = bokashi / 10f;
								component.blurIterations = 0;
								component.downsample = 0;
								if (bokashi > 3f)
								{
									component.blurSize -= 0.3f;
									component.blurIterations = 1;
									component.downsample = 1;
								}
							}
							else
							{
								Blur component = GameMain.Instance.MainCamera.gameObject.GetComponent<Blur>();
								component.enabled = false;
							}
							if (kamiyure > 0f)
							{
								for (int j = 0; j < maidCnt; j++)
								{
									Maid maid = maidArray[j];
									for (int l = 0; l < maid.body0.goSlot.Count; l++)
									{
										if (maid.body0.goSlot[l].obj != null)
										{
											DynamicBone component2 = maid.body0.goSlot[l].obj.GetComponent<DynamicBone>();
											if (component2 != null && component2.enabled)
											{
												component2.m_Gravity = new Vector3(softG.x * 5f, (softG.y + 0.003f) * 5f, softG.z * 5f);
											}
										}
										List<THair1> fieldValue3 = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid.body0.goSlot[l].bonehair, "hair1list");
										for (int k = 0; k < fieldValue3.Count; k++)
										{
											fieldValue3[k].SoftG = new Vector3(softG.x, softG.y + kamiyure, softG.z);
										}
									}
								}
							}
							if (isBlur)
							{
								Vignetting component3 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
								component3.mode = 0;
								component3.intensity = blur1;
								component3.chromaticAberration = blur4;
								component3.blur = blur2;
								component3.blurSpread = blur3;
								component3.enabled = true;
								isBlur2 = true;
							}
							else if (isBlur2)
							{
								isBlur2 = false;
								Vignetting component3 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
								component3.mode = 0;
								component3.intensity = -3.98f;
								component3.chromaticAberration = -0.75f;
								component3.axialAberration = 1.18f;
								component3.blurSpread = 4.19f;
								component3.luminanceDependency = 0.494f;
								component3.blurDistance = 1.71f;
								component3.enabled = false;
							}
						}
						if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.I))
						{
							GameMain.Instance.MainLight.transform.eulerAngles -= Vector3.right / 2f;
							lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.K))
						{
							if (GameMain.Instance.MainLight.transform.eulerAngles.x < 85f)
							{
								GameMain.Instance.MainLight.transform.eulerAngles += Vector3.right / 2f;
								lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.J))
						{
							GameMain.Instance.MainLight.transform.eulerAngles -= Vector3.up / 1.5f;
							lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.L))
						{
							GameMain.Instance.MainLight.transform.eulerAngles += Vector3.up / 1.5f;
							lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.BackQuote))
						{
							GameMain.Instance.MainLight.Reset();
							GameMain.Instance.MainLight.SetIntensity(0.95f);
							GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
							lightIndex[0] = 0;
							lightColorR[0] = 1f;
							lightColorG[0] = 1f;
							lightColorB[0] = 1f;
							lightX[0] = 40f;
							lightY[0] = 180f;
							lightAkarusa[0] = 0.95f;
							lightKage[0] = 0.098f;
							lightRange[0] = 50f;
							bgObject.SetActive(true);
							mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
							isIdx1 = false;
							isIdx2 = false;
							isIdx3 = false;
							isIdx4 = false;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Minus) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							List<float> list;
							(list = lightColorR)[0] = list[0] + 0.01f;
							if (lightColorR[0] > 1f)
							{
								lightColorR[0] = 1f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Quote) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							List<float> list;
							(list = lightColorG)[0] = list[0] + 0.01f;
							if (lightColorG[0] > 1f)
							{
								lightColorG[0] = 1f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftBracket) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							List<float> list;
							(list = lightColorB)[0] = list[0] + 0.01f;
							if (lightColorB[0] > 1f)
							{
								lightColorB[0] = 1f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Minus))
						{
							List<float> list;
							(list = lightColorR)[0] = list[0] - 0.01f;
							if (lightColorR[0] < 0f)
							{
								lightColorR[0] = 0f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Quote))
						{
							List<float> list;
							(list = lightColorG)[0] = list[0] - 0.01f;
							if (lightColorG[0] < 0f)
							{
								lightColorG[0] = 0f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftBracket))
						{
							List<float> list;
							(list = lightColorB)[0] = list[0] - 0.01f;
							if (lightColorB[0] < 0f)
							{
								lightColorB[0] = 0f;
							}
							if (lightIndex[0] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Alpha0))
						{
							GameMain.Instance.MainLight.GetComponent<Light>().intensity += 0.12f * Time.deltaTime;
							lightAkarusa[0] = GameMain.Instance.MainLight.GetComponent<Light>().intensity;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.P))
						{
							GameMain.Instance.MainLight.GetComponent<Light>().intensity -= 0.12f * Time.deltaTime;
							lightAkarusa[0] = GameMain.Instance.MainLight.GetComponent<Light>().intensity;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Alpha9))
						{
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle += 0.2f;
							GameMain.Instance.MainLight.GetComponent<Light>().range += 0.2f;
							lightRange[0] = GameMain.Instance.MainLight.GetComponent<Light>().spotAngle;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.O))
						{
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle -= 0.2f;
							GameMain.Instance.MainLight.GetComponent<Light>().range -= 0.2f;
							lightRange[0] = GameMain.Instance.MainLight.GetComponent<Light>().spotAngle;
						}
						else if (Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.U))
						{
							List<int> list2;
							(list2 = lightIndex)[0] = list2[0] + 1;
							if (lightIndex[0] == 4)
							{
								lightIndex[0] = 0;
							}
							lightColorR[0] = 1f;
							lightColorG[0] = 1f;
							lightColorB[0] = 1f;
							lightX[0] = 40f;
							lightY[0] = 180f;
							lightAkarusa[0] = 0.95f;
							lightKage[0] = 0.098f;
							lightRange[0] = 50f;
							GameMain.Instance.MainLight.Reset();
							GameMain.Instance.MainLight.SetIntensity(0.95f);
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 50f;
							GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
							GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
							if (lightIndex[0] == 0)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
								bgObject.SetActive(true);
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
							}
							else if (lightIndex[0] == 1)
							{
								GameMain.Instance.MainLight.transform.eulerAngles += Vector3.right * 40f;
								lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
								GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Spot;
							}
							else if (lightIndex[0] == 2)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Point;
							}
							else if (lightIndex[0] == 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
								bgObject.SetActive(false);
							}
							isIdx1 = false;
							isIdx2 = false;
							isIdx3 = false;
							isIdx4 = false;
						}
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Insert))
					{
						GameMain.Instance.MainLight.transform.eulerAngles -= Vector3.right / 2f;
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Delete))
					{
						if (GameMain.Instance.MainLight.transform.eulerAngles.x < 85f)
						{
							GameMain.Instance.MainLight.transform.eulerAngles += Vector3.right / 2f;
						}
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Home))
					{
						GameMain.Instance.MainLight.transform.eulerAngles -= Vector3.up / 1.5f;
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.End))
					{
						GameMain.Instance.MainLight.transform.eulerAngles += Vector3.up / 1.5f;
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.BackQuote))
					{
						GameMain.Instance.MainLight.Reset();
						GameMain.Instance.MainLight.SetIntensity(0.95f);
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.PageUp))
					{
						GameMain.Instance.MainLight.GetComponent<Light>().intensity += 0.1f * Time.deltaTime;
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.PageDown))
					{
						GameMain.Instance.MainLight.GetComponent<Light>().intensity -= 0.1f * Time.deltaTime;
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.Quote))
					{
						if (lightIndex[0] == 1)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle += 0.2f;
						}
						else
						{
							GameMain.Instance.MainLight.GetComponent<Light>().range += 0.2f;
						}
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftBracket))
					{
						if (lightIndex[0] == 1)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle -= 0.2f;
						}
						else
						{
							GameMain.Instance.MainLight.GetComponent<Light>().range -= 0.2f;
						}
					}
					else if (Input.GetKey(KeyCode.R) && Input.GetKeyDown(KeyCode.Minus))
					{
						List<int> list2;
						(list2 = lightIndex)[0] = list2[0] + 1;
						if (lightIndex[0] == 3)
						{
							lightIndex[0] = 0;
						}
						GameMain.Instance.MainLight.Reset();
						GameMain.Instance.MainLight.SetIntensity(0.95f);
						GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 50f;
						GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
						GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
						if (lightIndex[0] == 0)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
						}
						else if (lightIndex[0] == 1)
						{
							GameMain.Instance.MainLight.transform.eulerAngles += Vector3.right * 40f;
							GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Spot;
						}
						else if (lightIndex[0] == 2)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Point;
						}
					}
					int i;
					for (i = 0; i < lightList.Count; i++)
					{
						if (i > 0)
						{
							lightList[i].GetComponent<Light>().color = new Color(lightColorR[i], lightColorG[i], lightColorB[i]);
							lightList[i].GetComponent<Light>().intensity = lightAkarusa[i];
							lightList[i].GetComponent<Light>().spotAngle = lightRange[i];
							lightList[i].GetComponent<Light>().range = lightRange[i] / 5f;
							if (!Input.GetKey(KeyCode.X) && (!Input.GetKey(KeyCode.Z) || !getModKeyPressing(AdvancedStudioMode.modKey.Shift)))
							{
								lightList[i].transform.eulerAngles = new Vector3(lightX[i], lightY[i], 18f);
							}
						}
						else
						{
							GameMain.Instance.MainLight.SetIntensity(lightAkarusa[0]);
							GameMain.Instance.MainLight.GetComponent<Light>().shadowStrength = lightKage[0];
							if (!Input.GetKey(KeyCode.X) && (!Input.GetKey(KeyCode.Z) || !getModKeyPressing(AdvancedStudioMode.modKey.Shift)))
							{
								GameMain.Instance.MainLight.SetRotation(new Vector3(lightX[0], lightY[0], 18f));
							}
							GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = lightRange[i];
							GameMain.Instance.MainLight.GetComponent<Light>().range = lightRange[i] / 5f;
							if (lightIndex[i] != 3)
							{
								GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
							else
							{
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							}
						}
					}
					bool flag3 = false;
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isVR && sceneLevel == 5 && j == isEditNo)
						{
							bool flag4 = shodaiFlg[(int)selectList[j]];
							shodaiFlg[(int)selectList[j]] = false;
							try
							{
								TMorph morph = maidArray[j].body0.Face.morph;
								float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
								float num2 = fieldValue[(int)morph.hash["tangopen"]];
							}
							catch
							{
								shodaiFlg[(int)selectList[j]] = true;
							}
							if (shodaiFlg[(int)selectList[j]] != flag4)
							{
								if (!isVR)
								{
									eyeL[(int)selectList[j]] = maidArray[j].body0.quaDefEyeL.eulerAngles;
									eyeR[(int)selectList[j]] = maidArray[j].body0.quaDefEyeR.eulerAngles;
								}
							}
						}
						if (maidArray[j] && maidArray[j].Visible && isStop[j])
						{
							maidArray[j].body0.m_Bones.GetComponent<Animation>().Stop();
						}
						if (sceneLevel == 5)
						{
							if (editSelectMaid == maidArray[j])
							{
								flag3 = true;
							}
						}
						Maid maid = maidArray[j];
					}
					if (sceneLevel == 5 && !flag3 && maidCnt > 0)
					{
						isEditNo = 0;
						SceneEdit component4 = GameObject.Find("__SceneEdit__").GetComponent<SceneEdit>();
						AdvancedStudioMode.SetFieldValue<SceneEdit, Maid>(component4, "m_maid", maidArray[0]);
					}
					i = 0;
					while (i < maidCnt)
					{
						Transform transform = maidArray[i].transform;
						Maid maid = maidArray[i];
						if (cafeFlg[i])
						{
							cafeCount[i]++;
							if (cafeCount[i] > 1)
							{
								maid.DelProp(MPN.handitem, true);
								maid.SetProp("handitem", "HandItemR_Etoile_Teacup_I_.menu", 0, true, false);
								maid.AllProcPropSeqStart();
								cafeFlg[i] = false;
								cafeCount[i] = 0;
							}
						}
						KeyCode key;
						if (i >= 14)
						{
							bool flag5 = false;
							bool flag6 = false;
							for (int k = 0; k < keyArray.Length; k++)
							{
								if (Input.GetKey(keyArray[k]))
								{
									flag5 = true;
									break;
								}
								if (Input.GetKeyUp(keyArray[k]))
								{
									flag6 = true;
									break;
								}
							}
							key = keyArray[6];
							if (!flag5 || getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
							{
								if (!flag6 || getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
								{
									goto IL_49FE;
								}
								idoFlg[i - 7] = false;
							}
						}
						else if (i >= 7)
						{
							bool flag5 = false;
							bool flag6 = false;
							for (int k = 0; k < keyArray.Length; k++)
							{
								if (Input.GetKey(keyArray[k]))
								{
									flag5 = true;
									break;
								}
								if (Input.GetKeyUp(keyArray[k]))
								{
									flag6 = true;
									break;
								}
							}
							key = keyArray[i - 7];
							if (!flag5 || getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
							{
								if (!flag6 || getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
								{
									goto IL_49FE;
								}
								idoFlg[i - 7] = false;
							}
						}
						else
						{
							bool flag5 = false;
							for (int k = 0; k < keyArray.Length; k++)
							{
								if (Input.GetKey(keyArray[k]))
								{
									flag5 = true;
									break;
								}
							}
							if (!flag5 || !getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
							{
								key = keyArray[i];
								goto IL_49FE;
							}
						}
					IL_165DC:
						i++;
						continue;
					IL_49FE:
						if (xFlg[i])
						{
							if (!maid.AudioMan.audiosource.isPlaying)
							{
								if (maid.status.personal.uniqueName == "Pride")
								{
									string text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < tunArray.Length; k++)
									{
										if (tunArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									string text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < coolArray.Length; k++)
									{
										if (coolArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									string text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < pureArray.Length; k++)
									{
										if (pureArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									string text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < yanArray.Length; k++)
									{
										if (yanArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									string text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h0Array.Length; k++)
									{
										if (h0Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									string text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h1Array.Length; k++)
									{
										if (h1Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									string text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h2Array.Length; k++)
									{
										if (h2Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
							}
						}
						if (zFlg[i])
						{
							if (!maid.AudioMan.audiosource.isPlaying)
							{
								string text = "";
								if (maid.status.personal.uniqueName == "Pride")
								{
									text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(tunArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										tunArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(coolArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										coolArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(pureArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										pureArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(yanArray.Length);
									text = text + string.Format("{0:00000}", yanArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(h0tArray.Length);
									text = text + string.Format("{0:00000}", h0tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(h1tArray.Length);
									text = text + string.Format("{0:00000}", h1tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(h2tArray.Length);
									text = text + string.Format("{0:00000}", h2tArray[num4]) + ".ogg";
								}
								maid.AudioMan.LoadPlay(text, 0f, false, false);
							}
						}
						if (!isVR || isVR2)
						{
							if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Q))
							{
								ikMaid = i;
								isIK[i] = true;
								ikBui = 1;
								ikMode[i] = 0;
								SetIK(maid, i);
								HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Hand", true);
								UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R UpperArm", true);
								ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Forearm", true);
								Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Clavicle", true);
								IK_hand = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								qFlg = true;
							}
							else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.W))
							{
								ikMaid = i;
								isIK[i] = true;
								ikBui = 2;
								ikMode[i] = 0;
								SetIK(maid, i);
								HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Hand", true);
								UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L UpperArm", true);
								ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Forearm", true);
								Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Clavicle", true);
								IK_hand = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.A))
							{
								ikMaid = i;
								isIK[i] = true;
								ikBui = 3;
								ikMode[i] = 0;
								SetIK(maid, i);
								HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Foot", true);
								UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Thigh", true);
								ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Calf", true);
								Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_L", true);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.S))
							{
								ikMaid = i;
								isIK[i] = true;
								ikBui = 4;
								ikMode[i] = 0;
								SetIK(maid, i);
								HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Foot", true);
								UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Thigh", true);
								ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Calf", true);
								Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_L", true);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								sFlg = true;
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.U))
							{
								maid.body0.transform.localRotation = Quaternion.Euler(maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis(-1.5f, Vector3.up);
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								idoFlg[i] = true;
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.O))
							{
								maid.body0.transform.localRotation = Quaternion.Euler(maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis(1.5f, Vector3.up);
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								idoFlg[i] = true;
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.F))
							{
								bGui = true;
								isPoseInit = true;
								isGuiInit = true;
								poseFlg = true;
								faceFlg = false;
								sceneFlg = false;
								kankyoFlg = false;
								kankyo2Flg = false;
								fFlg = true;
								for (int k = 0; k < maidCnt; k++)
								{
									if (maid == maidArray[k])
									{
										selectMaidIndex = k;
									}
								}
								copyIndex = 0;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								idoFlg[i] = true;
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.V))
							{
								bGui = true;
								isFaceInit = true;
								isGuiInit = true;
								faceFlg = true;
								poseFlg = false;
								sceneFlg = false;
								kankyoFlg = false;
								kankyo2Flg = false;
								maid.boMabataki = false;
								for (int k = 0; k < maidCnt; k++)
								{
									if (maid == maidArray[k])
									{
										selectMaidIndex = k;
									}
								}
								faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
								idoFlg[i] = true;
							}
							else if (Input.GetKey(key) && getModKeyPressing(AdvancedStudioMode.modKey.Shift) && (Input.GetKeyDown(KeyCode.X) || (isVR && Input.GetKeyDown(KeyCode.UpArrow))))
							{
								if (!xFlg[i])
								{
									xFlg[i] = true;
									zFlg[i] = false;
									if (maid.status.personal.uniqueName == "Pride")
									{
										string text = "s0_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < tunArray.Length; k++)
										{
											if (tunArray[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (!flag7)
										{
											text = text + "0" + num4.ToString() + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Cool")
									{
										string text = "s1_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < coolArray.Length; k++)
										{
											if (coolArray[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (!flag7)
										{
											text = text + "0" + num4.ToString() + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Pure")
									{
										string text = "s2_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < pureArray.Length; k++)
										{
											if (pureArray[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (!flag7)
										{
											text = text + "0" + num4.ToString() + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Yandere")
									{
										string text = "s3_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < yanArray.Length; k++)
										{
											if (yanArray[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (!flag7)
										{
											text = text + string.Format("{0:00000}", num4) + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Muku")
									{
										string text = "h0_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < h0Array.Length; k++)
										{
											if (h0Array[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (flag7)
										{
											text = text + string.Format("{0:00000}", num4) + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Majime")
									{
										string text = "h1_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < h1Array.Length; k++)
										{
											if (h1Array[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (flag7)
										{
											text = text + string.Format("{0:00000}", num4) + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
									if (maid.status.personal.uniqueName == "Rindere")
									{
										string text = "h2_";
										System.Random random = new System.Random();
										int num4 = random.Next(10000);
										bool flag7 = false;
										for (int k = 0; k < h2Array.Length; k++)
										{
											if (h2Array[k] == num4)
											{
												flag7 = true;
												break;
											}
										}
										if (flag7)
										{
											text = text + string.Format("{0:00000}", num4) + ".ogg";
											if (GameUty.FileSystem.IsExistentFile(text))
											{
												maid.AudioMan.LoadPlay(text, 0f, false, false);
											}
										}
									}
								}
								else
								{
									xFlg[i] = false;
									maid.AudioMan.Clear();
								}
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && getModKeyPressing(AdvancedStudioMode.modKey.Shift) && (Input.GetKeyDown(KeyCode.Z) || (isVR && Input.GetKeyDown(KeyCode.DownArrow))))
							{
								if (!zFlg[i])
								{
									zFlg[i] = true;
									xFlg[i] = false;
									string text = "";
									if (maid.status.personal.uniqueName == "Pride")
									{
										text = "s0_";
										System.Random random = new System.Random();
										int num4 = random.Next(tunArray.Length);
										object obj = text;
										text = string.Concat(new object[]
										{
											obj,
											"0",
											tunArray[num4],
											".ogg"
										});
									}
									if (maid.status.personal.uniqueName == "Cool")
									{
										text = "s1_";
										System.Random random = new System.Random();
										int num4 = random.Next(coolArray.Length);
										object obj = text;
										text = string.Concat(new object[]
										{
											obj,
											"0",
											coolArray[num4],
											".ogg"
										});
									}
									if (maid.status.personal.uniqueName == "Pure")
									{
										text = "s2_";
										System.Random random = new System.Random();
										int num4 = random.Next(pureArray.Length);
										object obj = text;
										text = string.Concat(new object[]
										{
											obj,
											"0",
											pureArray[num4],
											".ogg"
										});
									}
									if (maid.status.personal.uniqueName == "Yandere")
									{
										text = "s3_";
										System.Random random = new System.Random();
										int num4 = random.Next(yanArray.Length);
										text = text + string.Format("{0:00000}", yanArray[num4]) + ".ogg";
									}
									if (maid.status.personal.uniqueName == "Muku")
									{
										text = "h0_";
										System.Random random = new System.Random();
										int num4 = random.Next(h0tArray.Length);
										text = text + string.Format("{0:00000}", h0tArray[num4]) + ".ogg";
									}
									if (maid.status.personal.uniqueName == "Majime")
									{
										text = "h1_";
										System.Random random = new System.Random();
										int num4 = random.Next(h1tArray.Length);
										text = text + string.Format("{0:00000}", h1tArray[num4]) + ".ogg";
									}
									if (maid.status.personal.uniqueName == "Rindere")
									{
										text = "h2_";
										System.Random random = new System.Random();
										int num4 = random.Next(h2tArray.Length);
										text = text + string.Format("{0:00000}", h2tArray[num4]) + ".ogg";
									}
									maid.AudioMan.LoadPlay(text, 0f, false, false);
								}
								else
								{
									zFlg[i] = false;
									maid.AudioMan.Clear();
								}
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && (Input.GetKeyDown(KeyCode.Z) || (isVR && Input.GetKeyDown(KeyCode.DownArrow))))
							{
								string text = "";
								if (maid.status.personal.uniqueName == "Pride")
								{
									text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(tunArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										tunArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(coolArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										coolArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(pureArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										pureArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(yanArray.Length);
									text = text + string.Format("{0:00000}", yanArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(h0tArray.Length);
									text = text + string.Format("{0:00000}", h0tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(h1tArray.Length);
									text = text + string.Format("{0:00000}", h1tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(h2tArray.Length);
									text = text + string.Format("{0:00000}", h2tArray[num4]) + ".ogg";
								}
								maid.AudioMan.LoadPlay(text, 0f, false, false);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && (Input.GetKeyDown(KeyCode.X) || (isVR && Input.GetKeyDown(KeyCode.UpArrow))))
							{
								if (maid.status.personal.uniqueName == "Pride")
								{
									string text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < tunArray.Length; k++)
									{
										if (tunArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									string text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < coolArray.Length; k++)
									{
										if (coolArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									string text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < pureArray.Length; k++)
									{
										if (pureArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									string text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < yanArray.Length; k++)
									{
										if (yanArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									string text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h0Array.Length; k++)
									{
										if (h0Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									string text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h1Array.Length; k++)
									{
										if (h1Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									string text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h2Array.Length; k++)
									{
										if (h2Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && (Input.GetKeyDown(KeyCode.LeftBracket) || (Input.GetKeyDown(KeyCode.BackQuote) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))))
							{
								for (int k = 0; k < 10; k++)
								{
									maid.transform.RotateAround(maid.transform.position, Vector3.right, -maid.transform.rotation.eulerAngles.x);
									maid.transform.RotateAround(maid.transform.position, Vector3.forward, -maid.transform.rotation.eulerAngles.z);
								}
								idoFlg[i] = true;
								atFlg = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && ((Input.GetKey(KeyCode.Minus) && getModKeyPressing(AdvancedStudioMode.modKey.Shift)) || (Input.GetKey(KeyCode.I) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))))
							{
								maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), 0.4f);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && ((Input.GetKey(KeyCode.Quote) && getModKeyPressing(AdvancedStudioMode.modKey.Shift)) || (Input.GetKey(KeyCode.K) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))))
							{
								maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), -0.4f);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && (Input.GetKey(KeyCode.Minus) || (Input.GetKey(KeyCode.J) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))))
							{
								maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), 0.4f);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && (Input.GetKey(KeyCode.Quote) || (Input.GetKey(KeyCode.L) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))))
							{
								maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), -0.4f);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.I))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.K))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = maid.transform.position;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.Alpha0))
							{
								Vector3 vector = maid.transform.position;
								vector.y += 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKey(KeyCode.P))
							{
								Vector3 vector = maid.transform.position;
								vector.y -= 0.0075f * speed;
								maid.SetPos(vector);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (maid.boMabataki && Input.GetKey(key) && Input.GetKeyDown(KeyCode.Alpha8) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
							{
								faceIndex[i]--;
								if (faceIndex[i] <= -1)
								{
									faceIndex[i] = faceArray.Length - 1;
								}
								maid.FaceAnime(faceArray[faceIndex[i]], 1f, 0);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (maid.boMabataki && Input.GetKey(key) && Input.GetKeyDown(KeyCode.Alpha8))
							{
								faceIndex[i]++;
								if (faceIndex[i] == faceArray.Length)
								{
									faceIndex[i] = 0;
								}
								maid.FaceAnime(faceArray[faceIndex[i]], 1f, 0);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Alpha9))
							{
								faceBlendIndex[i]++;
								if (faceBlendIndex[i] == faceBlendArray.Length)
								{
									faceBlendIndex[i] = 0;
								}
								maid.FaceBlend(faceBlendArray[faceBlendIndex[i]]);
								idoFlg[i] = true;
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Space))
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
									maid.body0.jbMuneL.enabled = true;
									maid.body0.jbMuneR.enabled = true;
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
								isLock[i] = false;
								idoFlg[i] = true;
								mHandL[i].initFlg = false;
								mHandR[i].initFlg = false;
								mFootL[i].initFlg = false;
								mFootR[i].initFlg = false;
								pHandL[i] = 0;
								pHandR[i] = 0;
								muneIKL[i] = false;
								muneIKR[i] = false;
								if (!isVR)
								{
									maidArray[i].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[i]];
									maidArray[i].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[i]];
								}
								if (i >= 7)
								{
									idoFlg[i - 7] = true;
								}
							}
							else if (maid.body0.isLoadedBody)
							{
								Transform transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Calf", true);
								Transform transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Calf", true);
								float num7 = (transform3.position - transform4.position).magnitude / 1.8f;
								num7 -= 0.1f;
								if (num7 < 0f)
								{
								}
								float num8 = transform4.localEulerAngles.y;
								float num9 = transform3.localEulerAngles.y;
								if (num8 < 100f)
								{
									num8 = 360f;
								}
								if (num9 > 260f)
								{
									num9 = 0f;
								}
								float num10 = num8 - num9;
								num10 /= 300f;
								if (muneIKL[i] && vIKMuneL[i].x != 0f)
								{
									IKMuneLSub[i].localEulerAngles = vIKMuneLSub[i];
									IKMuneL[i].localEulerAngles = vIKMuneL[i];
								}
								if (muneIKR[i] && vIKMuneR[i].x != 0f)
								{
									IKMuneRSub[i].localEulerAngles = vIKMuneRSub[i];
									IKMuneR[i].localEulerAngles = vIKMuneR[i];
								}
								if (!HandL1[i])
								{
									SetIKInit(i);
									SetIK(maid, i);
									ikBui = 5;
								}
								else
								{
									bool flag5 = false;
									for (int k = 0; k < keyArray.Length; k++)
									{
										if (Input.GetKey(keyArray[k]))
										{
											flag5 = true;
											break;
										}
									}
									if ((!Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W)) || Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Y))
									{
										flag5 = true;
									}
									if (!flag5)
									{
										if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && getModKeyPressing(AdvancedStudioMode.modKey.Alt) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
										{
											ikMode[i] = 15;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Shift) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))
										{
											ikMode[i] = 5;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Shift) && Input.GetKey(KeyCode.Space))
										{
											ikMode[i] = 6;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && Input.GetKey(KeyCode.Space))
										{
											ikMode[i] = 7;
										}
										else if (Input.GetKey(KeyCode.Z) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
										{
											ikMode[i] = 11;
										}
										else if (Input.GetKey(KeyCode.Z) && getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
										{
											ikMode[i] = 12;
										}
										else if (Input.GetKey(KeyCode.X) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
										{
											ikMode[i] = 14;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
										{
											ikMode[i] = 1;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && getModKeyPressing(AdvancedStudioMode.modKey.Alt))
										{
											ikMode[i] = 8;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
										{
											ikMode[i] = 2;
										}
										else if (getModKeyPressing(AdvancedStudioMode.modKey.Alt))
										{
											ikMode[i] = 3;
										}
										else if (Input.GetKey(KeyCode.Space))
										{
											ikMode[i] = 4;
										}
										else if (Input.GetKey(KeyCode.X))
										{
											ikMode[i] = 9;
										}
										else if (Input.GetKey(KeyCode.Z))
										{
											ikMode[i] = 10;
										}
										else if (Input.GetKey(KeyCode.C))
										{
											ikMode[i] = 13;
										}
										else if (Input.GetKey(KeyCode.A))
										{
											ikMode[i] = 16;
										}
										else
										{
											ikMode[i] = 0;
										}
										if (!isIK[i])
										{
											if (ikMode[i] < 9)
											{
												ikMode[i] = 0;
											}
										}
										bool flag8 = false;
										bool flag9 = false;
										bool flag10 = false;
										bool flag11 = false;
										if (gFinger[i, 0])
										{
											for (int j = 0; j < 15; j++)
											{
												if (mFinger[i, j].isStop)
												{
													flag8 = true;
												}
											}
											for (int j = 15; j < 30; j++)
											{
												if (mFinger[i, j].isStop)
												{
													flag9 = true;
												}
											}
											for (int j = 0; j < 6; j++)
											{
												if (mFinger2[i, j].isStop)
												{
													flag10 = true;
												}
											}
											for (int j = 6; j < 12; j++)
											{
												if (mFinger2[i, j].isStop)
												{
													flag11 = true;
												}
											}
										}
										if (gMaid[i] != null)
										{
											if (ikMode[i] >= 9 && ikMode[i] <= 14)
											{
												gMaid[i].SetActive(true);
												if (isCube)
												{
													gMaidC[i].SetActive(true);
												}
											}
											else
											{
												gMaid[i].SetActive(false);
												gMaidC[i].SetActive(false);
											}
										}
										if (flag9 || mHandR[i].isSelect || mArmR[i].isSelect || mClavicleR[i].isSelect || (ikMode[i] == 4 && Input.GetKeyDown(KeyCode.Q)))
										{
											ikBui = 1;
											if (ikMode[i] != 4 || !Input.GetKeyDown(KeyCode.Q))
											{
												ikMaid = i;
											}
											if (ikMaid == i)
											{
												SetIK(maid, i);
												HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Hand", true);
												UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R UpperArm", true);
												ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Forearm", true);
												Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Clavicle", true);
												IK_hand = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
											}
											if (Input.GetKeyDown(KeyCode.Q))
											{
												qFlg = true;
											}
										}
										else if (flag8 || mHandL[i].isSelect || mArmL[i].isSelect || mClavicleL[i].isSelect || (ikMode[i] == 4 && Input.GetKeyDown(KeyCode.W)))
										{
											ikBui = 2;
											if (ikMode[i] != 4 || !Input.GetKeyDown(KeyCode.W))
											{
												ikMaid = i;
											}
											if (ikMaid == i)
											{
												SetIK(maid, i);
												HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Hand", true);
												UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L UpperArm", true);
												ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Forearm", true);
												Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Clavicle", true);
												IK_hand = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
											}
										}
										else if (flag11 || mFootR[i].isSelect || mHizaR[i].isSelect || (ikMode[i] == 4 && Input.GetKeyDown(KeyCode.A)))
										{
											ikBui = 3;
											if (ikMode[i] != 4 || !Input.GetKeyDown(KeyCode.A))
											{
												ikMaid = i;
											}
											if (ikMaid == i)
											{
												SetIK(maid, i);
												HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Foot", true);
												UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Thigh", true);
												ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Calf", true);
												Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_L", true);
											}
										}
										else if (flag10 || mFootL[i].isSelect || mHizaL[i].isSelect || (ikMode[i] == 4 && Input.GetKeyDown(KeyCode.S)))
										{
											ikBui = 4;
											if (ikMode[i] != 4 || !Input.GetKeyDown(KeyCode.S))
											{
												ikMaid = i;
											}
											if (ikMaid == i)
											{
												SetIK(maid, i);
												HandL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Foot", true);
												UpperArmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Thigh", true);
												ForearmL = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Calf", true);
												Clavicle = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_L", true);
											}
											if (Input.GetKeyDown(KeyCode.S))
											{
												sFlg = true;
											}
										}
										mHandR[i].isSelect = false;
										mArmR[i].isSelect = false;
										mHandL[i].isSelect = false;
										mArmL[i].isSelect = false;
										mFootR[i].isSelect = false;
										mHizaR[i].isSelect = false;
										mFootL[i].isSelect = false;
										mHizaL[i].isSelect = false;
										mClavicleL[i].isSelect = false;
										mClavicleR[i].isSelect = false;
										if (ikMode[i] == 16)
										{
											if (!gHead2[i])
											{
												SetIKInit6(i);
											}
											if (mHead2[i].isClick)
											{
												mHead2[i].isClick = false;
												bGui = true;
												isFaceInit = true;
												isGuiInit = true;
												faceFlg = true;
												poseFlg = false;
												sceneFlg = false;
												kankyoFlg = false;
												kankyo2Flg = false;
												maid.boMabataki = false;
												selectMaidIndex = mHead2[i].no;
												faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
											}
											if (mMaid2[i].isClick)
											{
												mMaid2[i].isClick = false;
												bGui = true;
												isPoseInit = true;
												isGuiInit = true;
												poseFlg = true;
												faceFlg = false;
												sceneFlg = false;
												kankyoFlg = false;
												kankyo2Flg = false;
												selectMaidIndex = mMaid2[i].no;
												copyIndex = 0;
											}
											gHead2[i].transform.position = new Vector3(Head2[i].position.x, (Head2[i].position.y * 1.2f + Head3[i].position.y * 0.8f) / 2f, Head2[i].position.z);
											gHead2[i].transform.eulerAngles = new Vector3(Head2[i].transform.eulerAngles.x, Head2[i].transform.eulerAngles.y, Head2[i].transform.eulerAngles.z + 90f);
											mHead2[i].no = i;
											mHead2[i].maid = maid;
											mHead2[i].ido = 1;
											gMaid2[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
											gMaid2[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
											mMaid2[i].no = i;
											mMaid2[i].maid = maid;
											mMaid2[i].ido = 2;
											gArmL[i].SetActive(false);
											gFootL[i].SetActive(false);
											gHizaL[i].SetActive(false);
											gHandR[i].SetActive(false);
											gArmR[i].SetActive(false);
											gFootR[i].SetActive(false);
											gHizaR[i].SetActive(false);
											gClavicleL[i].SetActive(false);
											gClavicleR[i].SetActive(false);
											gNeck[i].SetActive(false);
											gSpine[i].SetActive(false);
											gSpine0a[i].SetActive(false);
											gSpine1a[i].SetActive(false);
											gSpine1[i].SetActive(false);
											gPelvis[i].SetActive(false);
										}
										else if (ikMode[i] == 8)
										{
											if (ikModeOld[i] == 15 && gIKHandL[i])
											{
												mIKHandL[i].ido = 1;
												mIKHandL[i].reset = true;
												mIKHandR[i].ido = 1;
												mIKHandR[i].reset = true;
											}
											else if (ikModeOld[i] == 3 && gHead[i])
											{
												mHead[i].ido = 7;
												mHead[i].reset = true;
											}
											else
											{
												if (!gIKHandL[i])
												{
													SetIKInit5(i);
												}
												gIKHandL[i].transform.position = IKHandL[i].position;
												gIKHandL[i].transform.eulerAngles = IKHandL[i].eulerAngles;
												mIKHandL[i].maid = maid;
												mIKHandL[i].HandL = IKHandL[i];
												mIKHandL[i].ido = 1;
												gIKHandR[i].transform.position = IKHandR[i].position;
												gIKHandR[i].transform.eulerAngles = IKHandR[i].eulerAngles;
												mIKHandR[i].maid = maid;
												mIKHandR[i].HandL = IKHandR[i];
												mIKHandR[i].ido = 1;
												if (!gIKMuneL[i])
												{
													SetIKInit7(i);
												}
												if (!gHead[i])
												{
													SetIKInit4(i);
												}
												mIKMuneL[i].maid = maid;
												mIKMuneL[i].HandL = IKMuneLSub[i];
												mIKMuneL[i].UpperArmL = IKMuneL[i];
												mIKMuneL[i].ForearmL = IKMuneL[i];
												gIKMuneL[i].transform.position = (IKMuneL[i].position + IKMuneLSub[i].position) / 2f;
												mIKMuneR[i].maid = maid;
												mIKMuneR[i].HandL = IKMuneRSub[i];
												mIKMuneR[i].UpperArmL = IKMuneR[i];
												mIKMuneR[i].ForearmL = IKMuneR[i];
												gIKMuneR[i].transform.position = (IKMuneR[i].position + IKMuneRSub[i].position) / 2f;
												gHead[i].transform.position = new Vector3(Head2[i].position.x, (Head2[i].position.y * 1.2f + Head3[i].position.y * 0.8f) / 2f, Head2[i].position.z);
												gHead[i].transform.eulerAngles = new Vector3(Head2[i].transform.eulerAngles.x, Head2[i].transform.eulerAngles.y, Head2[i].transform.eulerAngles.z + 90f);
												mHead[i].head = Head1[i];
												mHead[i].maid = maid;
												mHead[i].ido = 7;
												mHead[i].shodaiFlg = shodaiFlg[(int)selectList[i]];
												gHead[i].SetActive(true);
												if (mHead[i].isClick)
												{
													mHead[i].isClick = false;
													mHead[i].isClick2 = false;
													maid.body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[i]];
													maid.body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[i]];
												}
												if (mIKMuneL[i].isMouseUp && mIKMuneL[i].isMouseDrag)
												{
													IKMuneLSub[i].localEulerAngles = mIKMuneL[i].HandLangles;
													IKMuneL[i].localEulerAngles = mIKMuneL[i].UpperArmLangles;
													vIKMuneLSub[i] = new Vector3(IKMuneLSub[i].localEulerAngles.x, IKMuneLSub[i].localEulerAngles.y, IKMuneLSub[i].localEulerAngles.z);
													vIKMuneL[i] = new Vector3(IKMuneL[i].localEulerAngles.x, IKMuneL[i].localEulerAngles.y, IKMuneL[i].localEulerAngles.z);
													muneIKL[i] = true;
													isStop[i] = true;
													isLock[i] = true;
													mIKMuneL[i].isMouseDrag = false;
												}
												if (mIKMuneL[i].isMouseDown)
												{
													maid.body0.MuneYureL(0f);
													maid.body0.MuneYureR(0f);
													maid.body0.jbMuneL.enabled = false;
													maid.body0.jbMuneR.enabled = false;
													muneIKL[i] = false;
													mIKMuneL[i].isMouseUp = false;
												}
												if (mIKMuneR[i].isMouseUp && mIKMuneR[i].isMouseDrag)
												{
													IKMuneRSub[i].localEulerAngles = mIKMuneR[i].HandLangles;
													IKMuneR[i].localEulerAngles = mIKMuneR[i].UpperArmLangles;
													vIKMuneRSub[i] = new Vector3(IKMuneRSub[i].localEulerAngles.x, IKMuneRSub[i].localEulerAngles.y, IKMuneRSub[i].localEulerAngles.z);
													vIKMuneR[i] = new Vector3(IKMuneR[i].localEulerAngles.x, IKMuneR[i].localEulerAngles.y, IKMuneR[i].localEulerAngles.z);
													muneIKR[i] = true;
													isStop[i] = true;
													isLock[i] = true;
													mIKMuneR[i].isMouseDrag = false;
												}
												if (mIKMuneR[i].isMouseDown)
												{
													maid.body0.MuneYureL(0f);
													maid.body0.MuneYureR(0f);
													maid.body0.jbMuneL.enabled = false;
													maid.body0.jbMuneR.enabled = false;
													muneIKR[i] = false;
													mIKMuneL[i].isMouseUp = false;
												}
												if (mHead[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mHead[i].isStop = false;
												}
												gJotai[i].SetActive(false);
												gKahuku[i].SetActive(false);
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
										}
										else if (ikMode[i] == 15)
										{
											if (ikModeOld[i] == 8 && gIKHandL[i])
											{
												mIKHandL[i].ido = 2;
												mIKHandL[i].reset = true;
												mIKHandR[i].ido = 2;
												mIKHandR[i].reset = true;
											}
											else
											{
												if (!gIKHandL[i])
												{
													SetIKInit5(i);
												}
												gIKHandL[i].transform.position = IKHandL[i].position;
												gIKHandL[i].transform.eulerAngles = IKHandL[i].eulerAngles;
												mIKHandL[i].maid = maid;
												mIKHandL[i].HandL = IKHandL[i];
												mIKHandL[i].ido = 2;
												gIKHandR[i].transform.position = IKHandR[i].position;
												gIKHandR[i].transform.eulerAngles = IKHandR[i].eulerAngles;
												mIKHandR[i].maid = maid;
												mIKHandR[i].HandL = IKHandR[i];
												mIKHandR[i].ido = 2;
												Object.Destroy(gHead[i]);
												Object.Destroy(gJotai[i]);
												Object.Destroy(gKahuku[i]);
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
										}
										else if (ikMode[i] == 1)
										{
											mHandL[i].ido = 10;
											mHandR[i].ido = 10;
											mFootL[i].ido = 10;
											mFootR[i].ido = 10;
											if (ikModeOld[i] == 0 && gNeck[i])
											{
												if (isBone[i])
												{
													mNeck[i].ido = 4;
													mNeck[i].reset = true;
													mSpine[i].ido = 4;
													mSpine[i].reset = true;
													mSpine0a[i].ido = 4;
													mSpine0a[i].reset = true;
													mSpine1a[i].ido = 4;
													mSpine1a[i].reset = true;
													mSpine1[i].ido = 4;
													mSpine1[i].reset = true;
													mPelvis[i].ido = 4;
													mPelvis[i].reset = true;
													mHizaL[i].ido = 5;
													mHizaL[i].reset = true;
													mHizaR[i].ido = 5;
													mHizaR[i].reset = true;
												}
											}
											else
											{
												gArmL[i].SetActive(false);
												gArmR[i].SetActive(false);
												if (!isBone[i])
												{
													gHizaL[i].SetActive(false);
													gHizaR[i].SetActive(false);
												}
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												if (isBone[i])
												{
													mNeck[i].maid = maid;
													mNeck[i].head = Neck[i];
													mNeck[i].no = i;
													mNeck[i].ido = 4;
													gNeck[i].transform.position = Neck[i].position;
													gNeck[i].transform.localEulerAngles = Neck[i].localEulerAngles;
													if (mNeck[i].isHead)
													{
														mNeck[i].isHead = false;
														isLookn[i] = isLook[i];
														isLook[i] = !isLook[i];
													}
													mSpine[i].maid = maid;
													mSpine[i].head = Spine2[i];
													mSpine[i].no = i;
													mSpine[i].ido = 4;
													gSpine[i].transform.position = Spine2[i].position;
													gSpine[i].transform.localEulerAngles = Spine2[i].localEulerAngles;
													mSpine0a[i].maid = maid;
													mSpine0a[i].head = Spine0a2[i];
													mSpine0a[i].no = i;
													mSpine0a[i].ido = 4;
													gSpine0a[i].transform.position = Spine0a2[i].position;
													gSpine0a[i].transform.localEulerAngles = Spine0a2[i].localEulerAngles;
													mSpine1a[i].maid = maid;
													mSpine1a[i].head = Spine1a2[i];
													mSpine1a[i].no = i;
													mSpine1a[i].ido = 4;
													gSpine1a[i].transform.position = Spine1a2[i].position;
													gSpine1a[i].transform.localEulerAngles = Spine1a2[i].localEulerAngles;
													mSpine1[i].maid = maid;
													mSpine1[i].head = Spine12[i];
													mSpine1[i].no = i;
													mSpine1[i].ido = 4;
													gSpine1[i].transform.position = Spine12[i].position;
													gSpine1[i].transform.localEulerAngles = Spine12[i].localEulerAngles;
													mPelvis[i].maid = maid;
													mPelvis[i].head = Pelvis2[i];
													mPelvis[i].no = i;
													mPelvis[i].ido = 8;
													gPelvis[i].transform.position = Pelvis2[i].position;
													gPelvis[i].transform.localEulerAngles = Pelvis2[i].localEulerAngles;
													mHizaL[i].ido = 5;
													mHizaR[i].ido = 5;
												}
											}
										}
										else if (ikMode[i] == 2)
										{
											if (ikModeOld[i] == 0 && gPelvis[i])
											{
												mPelvis[i].ido = 9;
												mPelvis[i].reset = true;
											}
											else
											{
												gArmL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gArmR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												mPelvis[i].maid = maid;
												mPelvis[i].head = Pelvis2[i];
												mPelvis[i].no = i;
												mPelvis[i].ido = 9;
												gPelvis[i].transform.position = Pelvis2[i].position;
												gPelvis[i].transform.localEulerAngles = Pelvis2[i].localEulerAngles;
											}
										}
										else if (ikMode[i] == 3)
										{
											if ((ikModeOld[i] == 5 || ikModeOld[i] == 8) && gHead[i])
											{
												mHead[i].ido = 1;
												mHead[i].reset = true;
												mJotai[i].ido = 2;
												mJotai[i].reset = true;
												mKahuku[i].ido = 3;
												mKahuku[i].reset = true;
												mHandL[i].ido = 1;
												mHandR[i].ido = 1;
												mFootL[i].ido = 3;
												mFootR[i].ido = 3;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												mHandL[i].reset = true;
												mHandR[i].reset = true;
												mFootL[i].reset = true;
												mFootR[i].reset = true;
												mHizaL[i].reset = true;
												mHizaR[i].reset = true;
											}
											else if (ikModeOld[i] == 0)
											{
												mHandL[i].ido = 1;
												mHandR[i].ido = 1;
												mFootL[i].ido = 3;
												mFootR[i].ido = 3;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												mHandL[i].reset = true;
												mHandR[i].reset = true;
												mFootL[i].reset = true;
												mFootR[i].reset = true;
												mHizaL[i].reset = true;
												mHizaR[i].reset = true;
											}
											else
											{
												if (!gHead[i])
												{
													SetIKInit4(i);
												}
												if (mHead[i].isHead)
												{
													mHead[i].isHead = false;
													isLookn[i] = isLook[i];
													isLook[i] = !isLook[i];
												}
												gHead[i].transform.position = new Vector3(Head2[i].position.x, (Head2[i].position.y * 1.2f + Head3[i].position.y * 0.8f) / 2f, Head2[i].position.z);
												gHead[i].transform.eulerAngles = new Vector3(Head2[i].transform.eulerAngles.x, Head2[i].transform.eulerAngles.y, Head2[i].transform.eulerAngles.z + 90f);
												mHead[i].head = Head1[i];
												mHead[i].maid = maid;
												mHead[i].no = i;
												mHead[i].ido = 1;
												Transform spine0a = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine0a", true);
												Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1", true);
												Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1a", true);
												Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine", true);
												Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Pelvis", true);
												gJotai[i].transform.position = new Vector3(transform5.position.x, (transform5.position.y * 0f + transform6.position.y * 2f) / 2f, transform5.position.z);
												gJotai[i].transform.eulerAngles = new Vector3(transform5.transform.eulerAngles.x, transform5.transform.eulerAngles.y, transform5.transform.eulerAngles.z + 90f);
												mJotai[i].Spine0a = spine0a;
												mJotai[i].Spine1 = transform5;
												mJotai[i].Spine1a = transform6;
												mJotai[i].Spine = transform7;
												mJotai[i].maid = maid;
												mJotai[i].ido = 2;
												gKahuku[i].transform.position = new Vector3(transform8.position.x, (transform8.position.y + transform7.position.y) / 2f, transform8.position.z);
												gKahuku[i].transform.eulerAngles = new Vector3(transform8.transform.eulerAngles.x + 90f, transform8.transform.eulerAngles.y + 90f, transform8.transform.eulerAngles.z);
												mKahuku[i].Pelvis = transform8;
												mKahuku[i].maid = maid;
												mKahuku[i].ido = 3;
												mHandL[i].ido = 1;
												mHandR[i].ido = 1;
												mFootL[i].ido = 3;
												mFootR[i].ido = 3;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												gJotai[i].SetActive(true);
												gKahuku[i].SetActive(true);
												gHandL[i].SetActive(true);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(true);
												gHizaL[i].SetActive(true);
												gHandR[i].SetActive(true);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(true);
												gHizaR[i].SetActive(true);
												Object.Destroy(gIKHandL[i]);
												Object.Destroy(gIKHandR[i]);
												Object.Destroy(gIKMuneL[i]);
												Object.Destroy(gIKMuneR[i]);
												if (isBone[i])
												{
													gHizaL[i].SetActive(false);
													gHizaR[i].SetActive(false);
													gHead[i].SetActive(false);
													gJotai[i].SetActive(false);
													gKahuku[i].SetActive(false);
												}
												else
												{
													gHizaL[i].SetActive(true);
													gHizaR[i].SetActive(true);
													gHead[i].SetActive(true);
													gJotai[i].SetActive(true);
													gKahuku[i].SetActive(true);
												}
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
												if (mHead[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mHead[i].isStop = false;
												}
												if (mJotai[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mJotai[i].isStop = false;
												}
												if (mKahuku[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mKahuku[i].isStop = false;
												}
												if (mKahuku[i].isSelect)
												{
													mKahuku[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
												if (mJotai[i].isSelect)
												{
													mJotai[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
												if (mKahuku[i].isSelect)
												{
													mKahuku[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
											}
										}
										else if (ikMode[i] == 5)
										{
											if (ikModeOld[i] == 3 && gHead[i])
											{
												mHead[i].ido = 4;
												mHead[i].reset = true;
												mJotai[i].ido = 5;
												mJotai[i].reset = true;
												mKahuku[i].ido = 6;
												mKahuku[i].reset = true;
												mHandL[i].ido = 2;
												mHandR[i].ido = 2;
												mFootL[i].ido = 4;
												mFootR[i].ido = 4;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												mHandL[i].reset = true;
												mHandR[i].reset = true;
												mFootL[i].reset = true;
												mFootR[i].reset = true;
												mHizaL[i].reset = true;
												mHizaR[i].reset = true;
											}
											else if (ikModeOld[i] == 0)
											{
												mHandL[i].ido = 2;
												mHandR[i].ido = 2;
												mFootL[i].ido = 4;
												mFootR[i].ido = 4;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												mHandL[i].reset = true;
												mHandR[i].reset = true;
												mFootL[i].reset = true;
												mFootR[i].reset = true;
												mHizaL[i].reset = true;
												mHizaR[i].reset = true;
											}
											else
											{
												if (!gHead[i])
												{
													SetIKInit4(i);
												}
												if (mHead[i].isHead)
												{
													mHead[i].isHead = false;
													isLookn[i] = isLook[i];
													isLook[i] = !isLook[i];
												}
												gHead[i].transform.position = new Vector3(Head2[i].position.x, (Head2[i].position.y * 1.2f + Head3[i].position.y * 0.8f) / 2f, Head2[i].position.z);
												gHead[i].transform.eulerAngles = new Vector3(Head2[i].transform.eulerAngles.x, Head2[i].transform.eulerAngles.y, Head2[i].transform.eulerAngles.z + 90f);
												mHead[i].head = Head1[i];
												mHead[i].maid = maid;
												mHead[i].ido = 4;
												Transform spine0a = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine0a", true);
												Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1", true);
												Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1a", true);
												Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine", true);
												Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Pelvis", true);
												gJotai[i].transform.position = new Vector3(transform5.position.x, (transform5.position.y * 0f + transform6.position.y * 2f) / 2f, transform5.position.z);
												gJotai[i].transform.eulerAngles = new Vector3(transform5.transform.eulerAngles.x, transform5.transform.eulerAngles.y, transform5.transform.eulerAngles.z + 90f);
												mJotai[i].Spine0a = spine0a;
												mJotai[i].Spine1 = transform5;
												mJotai[i].Spine1a = transform6;
												mJotai[i].Spine = transform7;
												mJotai[i].maid = maid;
												mJotai[i].ido = 5;
												gKahuku[i].transform.position = new Vector3(transform8.position.x, (transform8.position.y + transform7.position.y) / 2f, transform8.position.z);
												gKahuku[i].transform.eulerAngles = new Vector3(transform8.transform.eulerAngles.x + 90f, transform8.transform.eulerAngles.y + 90f, transform8.transform.eulerAngles.z);
												mKahuku[i].Pelvis = transform8;
												mKahuku[i].maid = maid;
												mKahuku[i].ido = 6;
												mHandL[i].ido = 2;
												mHandR[i].ido = 2;
												mFootL[i].ido = 4;
												mFootR[i].ido = 4;
												mHizaL[i].ido = 5;
												mHizaR[i].ido = 5;
												gArmL[i].SetActive(false);
												gArmR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
												if (isBone[i])
												{
													gHizaL[i].SetActive(false);
													gHizaR[i].SetActive(false);
													gHead[i].SetActive(false);
													gJotai[i].SetActive(false);
													gKahuku[i].SetActive(false);
												}
												else
												{
													gHizaL[i].SetActive(true);
													gHizaR[i].SetActive(true);
													gHead[i].SetActive(true);
													gJotai[i].SetActive(true);
													gKahuku[i].SetActive(true);
												}
												if (mHead[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mHead[i].isStop = false;
												}
												if (mJotai[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mJotai[i].isStop = false;
												}
												if (mKahuku[i].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mKahuku[i].isStop = false;
												}
												if (mHead[i].isSelect)
												{
													mHead[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
												if (mJotai[i].isSelect)
												{
													mJotai[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
												if (mKahuku[i].isSelect)
												{
													mKahuku[i].isSelect = false;
													if (ikMaid != i)
													{
														ikMaid = i;
														ikBui = 5;
														SetIK(maid, i);
													}
												}
											}
										}
										else if (ikMode[i] == 13)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 13 && gMaid[i])
											{
												mMaid[i].ido = 5;
												mMaid[i].reset = true;
												mMaidC[i].ido = 5;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 5;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 5;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 11)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 11 && gMaid[i])
											{
												mMaid[i].ido = 3;
												mMaid[i].reset = true;
												mMaidC[i].ido = 3;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 3;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 3;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 12)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 12 && gMaid[i])
											{
												mMaid[i].ido = 2;
												mMaid[i].reset = true;
												mMaidC[i].ido = 2;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 2;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 2;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 10)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 10 && gMaid[i])
											{
												mMaid[i].ido = 1;
												mMaid[i].reset = true;
												mMaidC[i].ido = 1;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 1;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 1;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 9)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 9 && gMaid[i])
											{
												mMaid[i].ido = 4;
												mMaid[i].reset = true;
												mMaidC[i].ido = 4;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 4;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 4;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 14)
										{
											if ((ikModeOld[i] == 0 || ikModeOld[i] >= 9) && ikModeOld[i] != 14 && gMaid[i])
											{
												mMaid[i].ido = 6;
												mMaid[i].reset = true;
												mMaidC[i].ido = 6;
												mMaidC[i].reset = true;
												gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f);
											}
											else
											{
												if (!gMaid[i])
												{
													SetIKInit3(i);
												}
												gMaid[i].transform.position = new Vector3((Pelvis2[i].position.x + Spine0a2[i].position.x) / 2f, (Spine12[i].position.y + Spine0a2[i].position.y) / 2f, (Spine0a2[i].position.z + Pelvis2[i].position.z) / 2f);
												gMaid[i].transform.eulerAngles = new Vector3(Spine0a2[i].transform.eulerAngles.x, Spine0a2[i].transform.eulerAngles.y, Spine0a2[i].transform.eulerAngles.z + 90f);
												mMaid[i].maid = maid;
												mMaid[i].ido = 6;
												gMaidC[i].transform.position = maid.transform.position;
												gMaidC[i].transform.eulerAngles = maid.transform.eulerAngles;
												mMaidC[i].maid = maid;
												mMaidC[i].ido = 6;
												if (isCube)
												{
													gMaidC[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f);
													gMaidC[i].SetActive(true);
												}
												else
												{
													gMaidC[i].SetActive(false);
												}
												gHandL[i].SetActive(false);
												gArmL[i].SetActive(false);
												gFootL[i].SetActive(false);
												gHizaL[i].SetActive(false);
												gHandR[i].SetActive(false);
												gArmR[i].SetActive(false);
												gFootR[i].SetActive(false);
												gHizaR[i].SetActive(false);
												gClavicleL[i].SetActive(false);
												gClavicleR[i].SetActive(false);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
											}
											if (mMaid[i].isSelect)
											{
												mMaid[i].isSelect = false;
												if (ikMaid != i)
												{
													ikMaid = i;
													ikBui = 5;
													SetIK(maid, i);
												}
											}
										}
										else if (ikMode[i] == 6)
										{
											if (!gFinger[i, 0])
											{
												SetIKInit2(i);
											}
											Object.Destroy(gMaid[i]);
											Object.Destroy(gMaidC[i]);
											Object.Destroy(gHead[i]);
											Object.Destroy(gJotai[i]);
											Object.Destroy(gKahuku[i]);
											gHandL[i].SetActive(false);
											gArmL[i].SetActive(false);
											gFootL[i].SetActive(false);
											gHizaL[i].SetActive(false);
											gHandR[i].SetActive(false);
											gArmR[i].SetActive(false);
											gFootR[i].SetActive(false);
											gHizaR[i].SetActive(false);
											gClavicleL[i].SetActive(false);
											gClavicleR[i].SetActive(false);
											gNeck[i].SetActive(false);
											gSpine[i].SetActive(false);
											gSpine0a[i].SetActive(false);
											gSpine1a[i].SetActive(false);
											gSpine1[i].SetActive(false);
											gPelvis[i].SetActive(false);
											for (int j = 0; j < 10; j++)
											{
												mFinger[i, j * 3 + 2].maid = maid;
												mFinger[i, j * 3 + 2].HandL = Finger[i, j * 4 + 3];
												mFinger[i, j * 3 + 2].UpperArmL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 2].ForearmL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 2].ido = 12;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3 + 2].ido = 15;
												}
												mFinger[i, j * 3 + 1].maid = maid;
												mFinger[i, j * 3 + 1].HandL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 1].UpperArmL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3 + 1].ForearmL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3 + 1].ido = 11;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3 + 1].ido = 14;
												}
												mFinger[i, j * 3].maid = maid;
												mFinger[i, j * 3].HandL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3].UpperArmL = Finger[i, j * 4];
												mFinger[i, j * 3].ForearmL = Finger[i, j * 4];
												mFinger[i, j * 3].ido = 13;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3].ido = 0;
												}
												gFinger[i, j * 3 + 2].SetActive(false);
												gFinger[i, j * 3 + 1].SetActive(false);
												gFinger[i, j * 3].SetActive(true);
												gFinger[i, j * 3].SetActive(true);
												mFinger[i, j * 3].ido = 16;
												if (ikModeOld[i] != 6)
												{
													mFinger[i, j * 3].reset = true;
												}
												gFinger[i, j * 3 + 2].transform.position = (Finger[i, j * 4 + 3].position + Finger[i, j * 4 + 3].position + Finger[i, j * 4 + 2].position) / 3f;
												gFinger[i, j * 3 + 1].transform.position = Finger[i, j * 4 + 2].position;
												gFinger[i, j * 3].transform.position = Finger[i, j * 4 + 1].position;
												if (mFinger[i, j * 3 + 2].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3 + 2].isStop = false;
												}
												if (mFinger[i, j * 3 + 1].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3 + 1].isStop = false;
												}
												if (mFinger[i, j * 3].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3].isStop = false;
												}
											}
											for (int j = 0; j < 6; j++)
											{
												mFinger2[i, j * 2 + 1].maid = maid;
												mFinger2[i, j * 2 + 1].HandL = Finger2[i, j * 3 + 2];
												mFinger2[i, j * 2 + 1].UpperArmL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2 + 1].ForearmL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2 + 1].ido = 0;
												mFinger2[i, j * 2].maid = maid;
												mFinger2[i, j * 2].HandL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2].UpperArmL = Finger2[i, j * 3];
												mFinger2[i, j * 2].ForearmL = Finger2[i, j * 3];
												mFinger2[i, j * 2].ido = 0;
												gFinger2[i, j * 2 + 1].SetActive(false);
												gFinger2[i, j * 2].SetActive(false);
												gFinger2[i, j * 2 + 1].transform.position = Finger2[i, j * 3 + 2].position;
												gFinger2[i, j * 2].transform.position = Finger2[i, j * 3 + 1].position;
												if (mFinger2[i, j * 2 + 1].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger2[i, j * 2 + 1].isStop = false;
												}
												if (mFinger2[i, j * 2].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger2[i, j * 2].isStop = false;
												}
											}
										}
										else if (ikMode[i] == 4)
										{
											if (!gFinger[i, 0])
											{
												SetIKInit2(i);
											}
											Object.Destroy(gMaid[i]);
											Object.Destroy(gMaidC[i]);
											Object.Destroy(gHead[i]);
											Object.Destroy(gJotai[i]);
											Object.Destroy(gKahuku[i]);
											gHandL[i].SetActive(false);
											gArmL[i].SetActive(false);
											gFootL[i].SetActive(false);
											gHizaL[i].SetActive(false);
											gHandR[i].SetActive(false);
											gArmR[i].SetActive(false);
											gFootR[i].SetActive(false);
											gHizaR[i].SetActive(false);
											gClavicleL[i].SetActive(false);
											gClavicleR[i].SetActive(false);
											gNeck[i].SetActive(false);
											gSpine[i].SetActive(false);
											gSpine0a[i].SetActive(false);
											gSpine1a[i].SetActive(false);
											gSpine1[i].SetActive(false);
											gPelvis[i].SetActive(false);
											for (int j = 0; j < 10; j++)
											{
												mFinger[i, j * 3 + 2].maid = maid;
												mFinger[i, j * 3 + 2].HandL = Finger[i, j * 4 + 3];
												mFinger[i, j * 3 + 2].UpperArmL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 2].ForearmL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 2].ido = 12;
												mFinger[i, j * 3 + 2].onFlg = true;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3 + 2].ido = 15;
												}
												mFinger[i, j * 3 + 1].maid = maid;
												mFinger[i, j * 3 + 1].HandL = Finger[i, j * 4 + 2];
												mFinger[i, j * 3 + 1].UpperArmL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3 + 1].ForearmL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3 + 1].ido = 11;
												mFinger[i, j * 3 + 1].onFlg = true;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3 + 1].ido = 14;
												}
												mFinger[i, j * 3].maid = maid;
												mFinger[i, j * 3].HandL = Finger[i, j * 4 + 1];
												mFinger[i, j * 3].UpperArmL = Finger[i, j * 4];
												mFinger[i, j * 3].ForearmL = Finger[i, j * 4];
												mFinger[i, j * 3].ido = 13;
												mFinger[i, j * 3].onFlg = true;
												if (j == 0 || j == 5)
												{
													mFinger[i, j * 3].ido = 0;
												}
												if (ikModeOld[i] != 4)
												{
													mFinger[i, j * 3].reset = true;
												}
												gFinger[i, j * 3 + 2].SetActive(true);
												gFinger[i, j * 3 + 1].SetActive(true);
												gFinger[i, j * 3].SetActive(true);
												gFinger[i, j * 3 + 2].transform.position = (Finger[i, j * 4 + 3].position + Finger[i, j * 4 + 3].position + Finger[i, j * 4 + 2].position) / 3f;
												gFinger[i, j * 3 + 1].transform.position = Finger[i, j * 4 + 2].position;
												gFinger[i, j * 3].transform.position = Finger[i, j * 4 + 1].position;
												if (mFinger[i, j * 3 + 2].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3 + 2].isStop = false;
												}
												if (mFinger[i, j * 3 + 1].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3 + 1].isStop = false;
												}
												if (mFinger[i, j * 3].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger[i, j * 3].isStop = false;
												}
											}
											for (int j = 0; j < 6; j++)
											{
												mFinger2[i, j * 2 + 1].maid = maid;
												mFinger2[i, j * 2 + 1].HandL = Finger2[i, j * 3 + 2];
												mFinger2[i, j * 2 + 1].UpperArmL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2 + 1].ForearmL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2 + 1].ido = 17;
												mFinger2[i, j * 2].maid = maid;
												mFinger2[i, j * 2].HandL = Finger2[i, j * 3 + 1];
												mFinger2[i, j * 2].UpperArmL = Finger2[i, j * 3];
												mFinger2[i, j * 2].ForearmL = Finger2[i, j * 3];
												mFinger2[i, j * 2].ido = 0;
												gFinger2[i, j * 2 + 1].SetActive(true);
												gFinger2[i, j * 2].SetActive(true);
												gFinger2[i, j * 2 + 1].transform.position = Finger2[i, j * 3 + 2].position;
												gFinger2[i, j * 2].transform.position = Finger2[i, j * 3 + 1].position;
												if (mFinger2[i, j * 2 + 1].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger2[i, j * 2 + 1].isStop = false;
												}
												if (mFinger2[i, j * 2].isStop)
												{
													isStop[i] = true;
													isLock[i] = true;
													mFinger2[i, j * 2].isStop = false;
												}
											}
										}
										else if (ikModeOld[i] == 1 || ikModeOld[i] == 3 || ikModeOld[i] == 5 || ikModeOld[i] == 8)
										{
											mHandL[i].ido = 0;
											mHandR[i].ido = 0;
											mFootL[i].ido = 0;
											mFootR[i].ido = 0;
											mHizaL[i].ido = 0;
											mHizaR[i].ido = 0;
											mHandL[i].reset = true;
											mHandR[i].reset = true;
											mFootL[i].reset = true;
											mFootR[i].reset = true;
											mHizaL[i].reset = true;
											mHizaR[i].reset = true;
										}
										else
										{
											if (gFinger[i, 0])
											{
												for (int j = 0; j < 30; j++)
												{
													gFinger[i, j].SetActive(false);
												}
												for (int j = 0; j < 12; j++)
												{
													gFinger2[i, j].SetActive(false);
												}
											}
											Object.Destroy(gHead[i]);
											Object.Destroy(gJotai[i]);
											Object.Destroy(gKahuku[i]);
											Object.Destroy(gIKHandL[i]);
											Object.Destroy(gIKHandR[i]);
											Object.Destroy(gIKMuneL[i]);
											Object.Destroy(gIKMuneR[i]);
											Object.Destroy(gHead2[i]);
											Object.Destroy(gMaid2[i]);
											gHandL[i].SetActive(true);
											gArmL[i].SetActive(true);
											gFootL[i].SetActive(true);
											gHizaL[i].SetActive(true);
											gHandR[i].SetActive(true);
											gArmR[i].SetActive(true);
											gFootR[i].SetActive(true);
											gHizaR[i].SetActive(true);
											mHandL[i].ido = 0;
											mHandR[i].ido = 0;
											mFootL[i].ido = 0;
											mFootR[i].ido = 0;
											gClavicleL[i].SetActive(true);
											gClavicleR[i].SetActive(true);
										}
										if (!isIK[i])
										{
											gHandL[i].SetActive(false);
											gArmL[i].SetActive(false);
											gFootL[i].SetActive(false);
											gHizaL[i].SetActive(false);
											gHandR[i].SetActive(false);
											gArmR[i].SetActive(false);
											gFootR[i].SetActive(false);
											gHizaR[i].SetActive(false);
											gClavicleL[i].SetActive(false);
											gClavicleR[i].SetActive(false);
											gNeck[i].SetActive(false);
											gSpine[i].SetActive(false);
											gSpine0a[i].SetActive(false);
											gSpine1a[i].SetActive(false);
											gSpine1[i].SetActive(false);
											gPelvis[i].SetActive(false);
										}
										if (isIK[i])
										{
											mHandL[i].maid = maid;
											mHandL[i].HandL = HandL1[i];
											if (ikMode[i] == 2)
											{
												mHandL[i].UpperArmL = ForearmL1[i];
											}
											else
											{
												mHandL[i].UpperArmL = UpperArmL1[i];
												if (ikModeOld[i] == 2 && isLock[i])
												{
													mHandL[i].OnMouseDown();
													mHandL[i].isSelect = false;
												}
											}
											mHandL[i].ForearmL = ForearmL1[i];
											mArmL[i].maid = maid;
											mArmL[i].HandL = ForearmL1[i];
											mArmL[i].UpperArmL = UpperArmL1[i];
											mArmL[i].ForearmL = UpperArmL1[i];
											mArmL[i].onFlg = true;
											if (mArmL[i].onFlg2)
											{
												mArmL[i].onFlg2 = false;
												mHandL[i].initFlg = false;
											}
											if (ikMode[i] == 2)
											{
												mHandL[i].initFlg = false;
												mHandR[i].initFlg = false;
												mFootL[i].initFlg = false;
												mFootR[i].initFlg = false;
												mHandL[i].onFlg = true;
												mHandR[i].onFlg = true;
												mFootL[i].onFlg = true;
												mFootR[i].onFlg = true;
											}
											else
											{
												mHandL[i].onFlg = false;
												mHandR[i].onFlg = false;
												mFootL[i].onFlg = false;
												mFootR[i].onFlg = false;
											}
											gHandL[i].transform.position = HandL1[i].position;
											gArmL[i].transform.position = ForearmL1[i].position;
											mClavicleL[i].maid = maid;
											mClavicleL[i].HandL = UpperArmL1[i];
											mClavicleL[i].UpperArmL = ClavicleL1[i];
											mClavicleL[i].ForearmL = ClavicleL1[i];
											gClavicleL[i].transform.position = UpperArmL1[i].position;
											if (mHandL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mHandL[i].isStop = false;
											}
											if (mArmL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mArmL[i].isStop = false;
											}
											if (mClavicleL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mClavicleL[i].isStop = false;
											}
											mHandR[i].maid = maid;
											mHandR[i].HandL = HandR1[i];
											if (ikMode[i] == 2)
											{
												mHandR[i].UpperArmL = ForearmR1[i];
											}
											else
											{
												mHandR[i].UpperArmL = UpperArmR1[i];
												if (ikModeOld[i] == 2 && isLock[i])
												{
													mHandR[i].OnMouseDown();
													mHandR[i].isSelect = false;
												}
											}
											mHandR[i].ForearmL = ForearmR1[i];
											mArmR[i].maid = maid;
											mArmR[i].HandL = ForearmR1[i];
											mArmR[i].UpperArmL = UpperArmR1[i];
											mArmR[i].ForearmL = UpperArmR1[i];
											mArmR[i].onFlg = true;
											if (mArmR[i].onFlg2)
											{
												mArmR[i].onFlg2 = false;
												mHandR[i].initFlg = false;
											}
											gHandR[i].transform.position = HandR1[i].position;
											gArmR[i].transform.position = ForearmR1[i].position;
											mClavicleR[i].maid = maid;
											mClavicleR[i].HandL = UpperArmR1[i];
											mClavicleR[i].UpperArmL = ClavicleR1[i];
											mClavicleR[i].ForearmL = ClavicleR1[i];
											gClavicleR[i].transform.position = UpperArmR1[i].position;
											if (mHandR[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mHandR[i].isStop = false;
											}
											if (mArmR[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mArmR[i].isStop = false;
											}
											if (mClavicleL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mClavicleL[i].isStop = false;
											}
											mFootL[i].maid = maid;
											mFootL[i].HandL = HandL2[i];
											if (ikMode[i] == 2)
											{
												mFootL[i].UpperArmL = ForearmL2[i];
											}
											else
											{
												mFootL[i].UpperArmL = UpperArmL2[i];
												if (ikModeOld[i] == 2 && isLock[i])
												{
													mFootL[i].OnMouseDown();
													mFootL[i].isSelect = false;
												}
											}
											mFootL[i].ForearmL = ForearmL2[i];
											mHizaL[i].maid = maid;
											mHizaL[i].HandL = ForearmL2[i];
											mHizaL[i].UpperArmL = UpperArmL2[i];
											mHizaL[i].ForearmL = UpperArmL2[i];
											mHizaL[i].onFlg = true;
											if (mHizaL[i].onFlg2)
											{
												mHizaL[i].onFlg2 = false;
												mFootL[i].initFlg = false;
											}
											gFootL[i].transform.position = HandL2[i].position;
											gHizaL[i].transform.position = ForearmL2[i].position;
											if (mFootL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mFootL[i].isStop = false;
											}
											if (mHizaL[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mHizaL[i].isStop = false;
											}
											mFootR[i].maid = maid;
											mFootR[i].HandL = HandR2[i];
											if (ikMode[i] == 2)
											{
												mFootR[i].UpperArmL = ForearmR2[i];
											}
											else
											{
												mFootR[i].UpperArmL = UpperArmR2[i];
												if (ikModeOld[i] == 2 && isLock[i])
												{
													mFootR[i].OnMouseDown();
													mFootR[i].isSelect = false;
												}
											}
											mFootR[i].ForearmL = ForearmR2[i];
											mHizaR[i].maid = maid;
											mHizaR[i].HandL = ForearmR2[i];
											mHizaR[i].UpperArmL = UpperArmR2[i];
											mHizaR[i].ForearmL = UpperArmR2[i];
											mHizaR[i].onFlg = true;
											if (mHizaR[i].onFlg2)
											{
												mHizaR[i].onFlg2 = false;
												mFootR[i].initFlg = false;
											}
											gFootR[i].transform.position = HandR2[i].position;
											gHizaR[i].transform.position = ForearmR2[i].position;
											if (mFootR[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mFootR[i].isStop = false;
											}
											if (mHizaR[i].isStop)
											{
												isStop[i] = true;
												isLock[i] = true;
												mHizaR[i].isStop = false;
											}
											bool flag12 = false;
											if (ikModeOld[i] == 9 || ikModeOld[i] == 10 || ikModeOld[i] == 11 || ikModeOld[i] == 12 || ikModeOld[i] == 14)
											{
												flag12 = true;
											}
											if (isBone[i])
											{
												if ((ikMode[i] != 3 && ikMode[i] != 5 && ikMode[i] != 8) || ikMode[i] != ikModeOld[i] || isChange[i])
												{
													gizmoFootL[i].Visible = false;
													gizmoHandR[i].Visible = false;
													gizmoHandL[i].Visible = false;
													gizmoFootR[i].Visible = false;
												}
												else
												{
													bool fieldValue4 = AdvancedStudioMode.GetFieldValue<GizmoRender, bool>(gizmoHandL[i], "is_drag_");
													bool fieldValue5 = AdvancedStudioMode.GetFieldValue<GizmoRender, bool>(gizmoHandR[i], "is_drag_");
													bool fieldValue6 = AdvancedStudioMode.GetFieldValue<GizmoRender, bool>(gizmoFootL[i], "is_drag_");
													bool fieldValue7 = AdvancedStudioMode.GetFieldValue<GizmoRender, bool>(gizmoFootR[i], "is_drag_");
													if (ikMode[i] == 3)
													{
														gizmoHandL[i].transform.position = HandL1[i].transform.position;
														gizmoHandR[i].transform.position = HandR1[i].transform.position;
														gizmoFootL[i].transform.position = HandL2[i].transform.position;
														gizmoFootR[i].transform.position = HandR2[i].transform.position;
														if (fieldValue4 || fieldValue5 || fieldValue6 || fieldValue7)
														{
															HandL1[i].transform.rotation = gizmoHandL[i].transform.rotation;
															HandR1[i].transform.rotation = gizmoHandR[i].transform.rotation;
															HandL2[i].transform.rotation = gizmoFootL[i].transform.rotation;
															HandR2[i].transform.rotation = gizmoFootR[i].transform.rotation;
														}
														else
														{
															gizmoHandL[i].transform.rotation = HandL1[i].transform.rotation;
															gizmoHandR[i].transform.rotation = HandR1[i].transform.rotation;
															gizmoFootL[i].transform.rotation = HandL2[i].transform.rotation;
															gizmoFootR[i].transform.rotation = HandR2[i].transform.rotation;
														}
													}
													else if (ikMode[i] == 5)
													{
														gizmoHandL[i].transform.position = UpperArmL1[i].transform.position;
														gizmoHandR[i].transform.position = UpperArmR1[i].transform.position;
														gizmoFootL[i].transform.position = UpperArmL2[i].transform.position;
														gizmoFootR[i].transform.position = UpperArmR2[i].transform.position;
														if (fieldValue4 || fieldValue5 || fieldValue6 || fieldValue7)
														{
															UpperArmL1[i].transform.rotation = gizmoHandL[i].transform.rotation;
															UpperArmR1[i].transform.rotation = gizmoHandR[i].transform.rotation;
															UpperArmL2[i].transform.rotation = gizmoFootL[i].transform.rotation;
															UpperArmR2[i].transform.rotation = gizmoFootR[i].transform.rotation;
															flag12 = true;
														}
														else
														{
															gizmoHandL[i].transform.rotation = UpperArmL1[i].transform.rotation;
															gizmoHandR[i].transform.rotation = UpperArmR1[i].transform.rotation;
															gizmoFootL[i].transform.rotation = UpperArmL2[i].transform.rotation;
															gizmoFootR[i].transform.rotation = UpperArmR2[i].transform.rotation;
														}
													}
													else if (ikMode[i] == 8)
													{
														gizmoHandL[i].transform.position = ForearmL1[i].transform.position;
														gizmoHandR[i].transform.position = ForearmR1[i].transform.position;
														gizmoFootL[i].transform.position = ForearmL2[i].transform.position;
														gizmoFootR[i].transform.position = ForearmR2[i].transform.position;
														if (fieldValue4 || fieldValue5 || fieldValue6 || fieldValue7)
														{
															ForearmL1[i].transform.rotation = gizmoHandL[i].transform.rotation;
															ForearmR1[i].transform.rotation = gizmoHandR[i].transform.rotation;
															ForearmL2[i].transform.rotation = gizmoFootL[i].transform.rotation;
															ForearmR2[i].transform.rotation = gizmoFootR[i].transform.rotation;
															flag12 = true;
														}
														else
														{
															gizmoHandL[i].transform.rotation = ForearmL1[i].transform.rotation;
															gizmoHandR[i].transform.rotation = ForearmR1[i].transform.rotation;
															gizmoFootL[i].transform.rotation = ForearmL2[i].transform.rotation;
															gizmoFootR[i].transform.rotation = ForearmR2[i].transform.rotation;
														}
													}
													gizmoHandL[i].Visible = true;
													gizmoHandR[i].Visible = true;
													gizmoFootL[i].Visible = true;
													gizmoFootR[i].Visible = true;
													gHandL[i].SetActive(false);
													gFootL[i].SetActive(false);
													gHandR[i].SetActive(false);
													gFootR[i].SetActive(false);
													if (!isLock[i])
													{
														if (fieldValue4 || fieldValue5 || fieldValue6 || fieldValue7)
														{
															isStop[i] = true;
															isLock[i] = true;
														}
													}
												}
												if (isChange[i])
												{
													isChange[i] = false;
													gHandL[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gHandR[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gArmL[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gArmR[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gFootL[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gFootR[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gHizaL[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gHizaR[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gClavicleL[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gClavicleR[i].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
													gHandL[i].GetComponent<Renderer>().enabled = true;
													gHandR[i].GetComponent<Renderer>().enabled = true;
													gArmL[i].GetComponent<Renderer>().enabled = true;
													gArmR[i].GetComponent<Renderer>().enabled = true;
													gFootL[i].GetComponent<Renderer>().enabled = true;
													gFootR[i].GetComponent<Renderer>().enabled = true;
													gHizaL[i].GetComponent<Renderer>().enabled = true;
													gHizaR[i].GetComponent<Renderer>().enabled = true;
													gClavicleL[i].GetComponent<Renderer>().enabled = true;
													gClavicleR[i].GetComponent<Renderer>().enabled = true;
													gHandL[i].GetComponent<Renderer>().material = m_material2;
													gHandR[i].GetComponent<Renderer>().material = m_material2;
													gArmL[i].GetComponent<Renderer>().material = m_material2;
													gArmR[i].GetComponent<Renderer>().material = m_material2;
													gFootL[i].GetComponent<Renderer>().material = m_material2;
													gFootR[i].GetComponent<Renderer>().material = m_material2;
													gHizaL[i].GetComponent<Renderer>().material = m_material2;
													gHizaR[i].GetComponent<Renderer>().material = m_material2;
													gClavicleL[i].GetComponent<Renderer>().material = m_material2;
													gClavicleR[i].GetComponent<Renderer>().material = m_material2;
												}
												if (ikMode[i] == 0 && (ikModeOld[i] == 1 || ikModeOld[i] == 2) && gNeck[i])
												{
													mNeck[i].ido = 1;
													mNeck[i].reset = true;
													mSpine[i].ido = 1;
													mSpine[i].reset = true;
													mSpine0a[i].ido = 1;
													mSpine0a[i].reset = true;
													mSpine1a[i].ido = 1;
													mSpine1a[i].reset = true;
													mSpine1[i].ido = 1;
													mSpine1[i].reset = true;
													mPelvis[i].ido = 1;
													mPelvis[i].reset = true;
												}
												else if (ikMode[i] == 0)
												{
													gNeck[i].SetActive(true);
													gSpine[i].SetActive(true);
													gSpine0a[i].SetActive(true);
													gSpine1a[i].SetActive(true);
													gSpine1[i].SetActive(true);
													gPelvis[i].SetActive(true);
													if (mNeck[i].isHead)
													{
														mNeck[i].isHead = false;
														isLookn[i] = isLook[i];
														isLook[i] = !isLook[i];
													}
													mNeck[i].maid = maid;
													mNeck[i].head = Neck[i];
													mNeck[i].no = i;
													mNeck[i].ido = 1;
													gNeck[i].transform.position = Neck[i].position;
													gNeck[i].transform.localEulerAngles = Neck[i].localEulerAngles;
													mSpine[i].maid = maid;
													mSpine[i].head = Spine2[i];
													mSpine[i].no = i;
													mSpine[i].ido = 1;
													gSpine[i].transform.position = Spine2[i].position;
													gSpine[i].transform.eulerAngles = new Vector3(Spine2[i].localEulerAngles.z, Spine2[i].localEulerAngles.y, Spine2[i].localEulerAngles.x);
													mSpine0a[i].maid = maid;
													mSpine0a[i].head = Spine0a2[i];
													mSpine0a[i].no = i;
													mSpine0a[i].ido = 1;
													gSpine0a[i].transform.position = Spine0a2[i].position;
													gSpine0a[i].transform.eulerAngles = new Vector3(-Spine0a2[i].localEulerAngles.z, Spine0a2[i].localEulerAngles.x, Spine0a2[i].localEulerAngles.y);
													mSpine1a[i].maid = maid;
													mSpine1a[i].head = Spine1a2[i];
													mSpine1a[i].no = i;
													mSpine1a[i].ido = 1;
													gSpine1a[i].transform.position = Spine1a2[i].position;
													gSpine1a[i].transform.localEulerAngles = new Vector3(-Spine1a2[i].localEulerAngles.z, Spine1a2[i].localEulerAngles.x, Spine1a2[i].localEulerAngles.y);
													mSpine1[i].maid = maid;
													mSpine1[i].head = Spine12[i];
													mSpine1[i].no = i;
													mSpine1[i].ido = 1;
													gSpine1[i].transform.position = Spine12[i].position;
													gSpine1[i].transform.localEulerAngles = new Vector3(-Spine12[i].localEulerAngles.z, Spine12[i].localEulerAngles.x, Spine12[i].localEulerAngles.y);
													mPelvis[i].maid = maid;
													mPelvis[i].head = Pelvis2[i];
													mPelvis[i].no = i;
													mPelvis[i].ido = 1;
													gPelvis[i].transform.position = Pelvis2[i].position;
													gPelvis[i].transform.localEulerAngles = Pelvis2[i].localEulerAngles;
													if (mNeck[i].isIdo)
													{
														mNeck[i].isIdo = false;
														isLock[i] = true;
													}
													if (mSpine[i].isIdo)
													{
														mSpine[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mSpine0a[i].isIdo)
													{
														mSpine0a[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mSpine1a[i].isIdo)
													{
														mSpine1a[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mSpine1[i].isIdo)
													{
														mSpine1[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mPelvis[i].isIdo)
													{
														mPelvis[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mClavicleL[i].isIdo)
													{
														mClavicleL[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
													if (mClavicleR[i].isIdo)
													{
														mClavicleR[i].isIdo = false;
														flag12 = true;
														isLock[i] = true;
													}
												}
											}
											else if (isChange[i])
											{
												isChange[i] = false;
												gHandL[i].GetComponent<Renderer>().enabled = false;
												gArmL[i].GetComponent<Renderer>().enabled = false;
												gFootL[i].GetComponent<Renderer>().enabled = false;
												gHizaL[i].GetComponent<Renderer>().enabled = false;
												gHandR[i].GetComponent<Renderer>().enabled = false;
												gArmR[i].GetComponent<Renderer>().enabled = false;
												gFootR[i].GetComponent<Renderer>().enabled = false;
												gHizaR[i].GetComponent<Renderer>().enabled = false;
												gClavicleL[i].GetComponent<Renderer>().enabled = false;
												gClavicleR[i].GetComponent<Renderer>().enabled = false;
												gHandL[i].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
												gHandR[i].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
												gArmL[i].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
												gArmR[i].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
												gFootL[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
												gFootR[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
												gHizaL[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
												gHizaR[i].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
												gClavicleL[i].transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
												mClavicleR[i].transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
												gNeck[i].SetActive(false);
												gSpine[i].SetActive(false);
												gSpine0a[i].SetActive(false);
												gSpine1a[i].SetActive(false);
												gSpine1[i].SetActive(false);
												gPelvis[i].SetActive(false);
												gizmoHandL[i].Visible = false;
												gizmoHandR[i].Visible = false;
												gizmoFootL[i].Visible = false;
												gizmoFootR[i].Visible = false;
											}
											if (flag12)
											{
												mHandL[i].initFlg = false;
												mHandR[i].initFlg = false;
												mFootL[i].initFlg = false;
												mFootR[i].initFlg = false;
											}
										}
										ikModeOld[i] = ikMode[i];
										bool flag13 = false;
										if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Q) && i == ikMaid)
										{
											if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.BackQuote))
											{
												for (int k = 0; k < 10; k++)
												{
													maid.transform.RotateAround(maid.transform.position, Vector3.right, -maid.transform.rotation.eulerAngles.x);
													maid.transform.RotateAround(maid.transform.position, Vector3.forward, -maid.transform.rotation.eulerAngles.z);
												}
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), 0.3f);
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), 0.3f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), 0.3f);
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), -0.3f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), -0.3f);
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), 0.3f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), -0.3f);
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), -0.3f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.I))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), 0.4f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.K))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), -0.4f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.J))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), 0.4f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.L))
											{
												maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), -0.4f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.U))
											{
												maid.body0.transform.localRotation = Quaternion.Euler(maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis(-1.5f, Vector3.up);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.O))
											{
												maid.body0.transform.localRotation = Quaternion.Euler(maid.body0.transform.localEulerAngles) * Quaternion.AngleAxis(1.5f, Vector3.up);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.U))
											{
												maid.body0.transform.RotateAround(maid.transform.position, Vector3.up, -1.5f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.O))
											{
												maid.body0.transform.RotateAround(maid.transform.position, Vector3.up, 1.5f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
												vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
												vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
												vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
												vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.I))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.K))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.J))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.L))
											{
												Vector3 vector = maid.transform.position;
												vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) && Input.GetKey(KeyCode.Alpha0))
											{
												Vector3 vector = maid.transform.position;
												vector.y += 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) && Input.GetKey(KeyCode.P))
											{
												Vector3 vector = maid.transform.position;
												vector.y -= 0.0075f * speed;
												maid.SetPos(vector);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.I))
											{
												maid.transform.localScale = new Vector3(maid.transform.localScale.x * 1.005f, maid.transform.localScale.y * 1.005f, maid.transform.localScale.z * 1.005f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.K))
											{
												maid.transform.localScale = new Vector3(maid.transform.localScale.x * 0.995f, maid.transform.localScale.y * 0.995f, maid.transform.localScale.z * 0.995f);
												flag13 = true;
											}
											else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.BackQuote))
											{
												maid.transform.localScale = new Vector3(1f, 1f, 1f);
												flag13 = true;
											}
										}
										if (!flag13 && (ikBui != 5 || (ikBui == 5 && (ikMode[i] == 4 || ikMode[i] == 6 || ikMode[i] == 7))) && !Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Q) && i == ikMaid)
										{
											if (Input.GetKey(KeyCode.I))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.004f;
													ikLeftArm.Init(UpperArmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.004f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 3)
												{
													if (ikBui == 1 || ikBui == 2)
													{
														HandL.RotateAround(HandL.position, new Vector3(vector3.x, 0f, vector3.z), 0.8f);
													}
													else
													{
														HandL.RotateAround(HandL.position, new Vector3(vector3.x, 0f, vector3.z), -0.8f);
													}
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, new Vector3(vector3.x, 0f, vector3.z), 0.4f);
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, new Vector3(vector3.x, 0f, vector3.z), 0.6f);
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, new Vector3(vector3.x, 0f, vector3.z), 0.03f);
													Spine1.RotateAround(Spine1.position, new Vector3(vector3.x, 0f, vector3.z), 0.1f);
													Spine0a.RotateAround(Spine0a.position, new Vector3(vector3.x, 0f, vector3.z), 0.09f);
													Spine.RotateAround(Spine.position, new Vector3(vector3.x, 0f, vector3.z), 0.07f);
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, new Vector3(vector3.x, 0f, vector3.z), -0.2f);
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
											else if (Input.GetKey(KeyCode.K))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.004f;
													ikLeftArm.Init(UpperArmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.004f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 3)
												{
													if (ikBui == 1 || ikBui == 2)
													{
														HandL.RotateAround(HandL.position, new Vector3(vector3.x, 0f, vector3.z), -0.8f);
													}
													else
													{
														HandL.RotateAround(HandL.position, new Vector3(vector3.x, 0f, vector3.z), 0.8f);
													}
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, new Vector3(vector3.x, 0f, vector3.z), -0.4f);
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, new Vector3(vector3.x, 0f, vector3.z), -0.6f);
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, new Vector3(vector3.x, 0f, vector3.z), -0.03f);
													Spine1.RotateAround(Spine1.position, new Vector3(vector3.x, 0f, vector3.z), -0.1f);
													Spine0a.RotateAround(Spine0a.position, new Vector3(vector3.x, 0f, vector3.z), -0.09f);
													Spine.RotateAround(Spine.position, new Vector3(vector3.x, 0f, vector3.z), -0.07f);
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, new Vector3(vector3.x, 0f, vector3.z), 0.2f);
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
											else if (Input.GetKey(KeyCode.J))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.004f;
													ikLeftArm.Init(UpperArmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.004f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 3)
												{
													if (ikBui == 1 || ikBui == 2)
													{
														HandL.RotateAround(HandL.position, new Vector3(vector2.x, 0f, vector2.z), 0.8f);
													}
													else
													{
														HandL.RotateAround(HandL.position, new Vector3(vector2.x, 0f, vector2.z), -0.8f);
													}
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, new Vector3(vector2.x, 0f, vector2.z), 0.4f);
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, new Vector3(vector2.x, 0f, vector2.z), 0.6f);
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, new Vector3(vector2.x, 0f, vector2.z), 0.03f);
													Spine1.RotateAround(Spine1.position, new Vector3(vector2.x, 0f, vector2.z), 0.1f);
													Spine0a.RotateAround(Spine0a.position, new Vector3(vector2.x, 0f, vector2.z), 0.09f);
													Spine.RotateAround(Spine.position, new Vector3(vector2.x, 0f, vector2.z), 0.07f);
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, new Vector3(vector2.x, 0f, vector2.z), -0.2f);
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
											else if (Input.GetKey(KeyCode.L))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.004f;
													ikLeftArm.Init(UpperArmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.004f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 3)
												{
													if (ikBui == 1 || ikBui == 2)
													{
														HandL.RotateAround(HandL.position, new Vector3(vector2.x, 0f, vector2.z), -0.8f);
													}
													else
													{
														HandL.RotateAround(HandL.position, new Vector3(vector2.x, 0f, vector2.z), 0.8f);
													}
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, new Vector3(vector2.x, 0f, vector2.z), -0.4f);
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, new Vector3(vector2.x, 0f, vector2.z), -0.6f);
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, new Vector3(vector2.x, 0f, vector2.z), -0.03f);
													Spine1.RotateAround(Spine1.position, new Vector3(vector2.x, 0f, vector2.z), -0.1f);
													Spine0a.RotateAround(Spine0a.position, new Vector3(vector2.x, 0f, vector2.z), -0.09f);
													Spine.RotateAround(Spine.position, new Vector3(vector2.x, 0f, vector2.z), -0.07f);
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, new Vector3(vector2.x, 0f, vector2.z), 0.2f);
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
											else if (Input.GetKey(KeyCode.U))
											{
												if (ikMode[i] == 3)
												{
													HandL.localRotation = Quaternion.Euler(HandL.localEulerAngles) * Quaternion.AngleAxis(1f, Vector3.right);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, Vector3.up, -0.7f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, Vector3.up, -0.7f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, Vector3.up, -0.084f);
													Spine0a.RotateAround(Spine0a.position, Vector3.up, -0.156f);
													Spine.RotateAround(Spine.position, Vector3.up, -0.156f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, Vector3.up, 0.4f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if ((ikMode[i] == 0 || ikMode[i] == 1 || ikMode[i] == 2) && (ikBui == 3 || ikBui == 4))
												{
													UpperArmL.localRotation = Quaternion.Euler(UpperArmL.localEulerAngles) * Quaternion.AngleAxis(0.5f, Vector3.right);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if ((ikMode[i] == 0 || ikMode[i] == 1 || ikMode[i] == 2) && (ikBui == 1 || ikBui == 2))
												{
													UpperArmL.RotateAround(UpperArmL.position, Vector3.right, 0.5f);
													isStop[i] = true;
													isLock[i] = true;
												}
											}
											else if (Input.GetKey(KeyCode.O))
											{
												if (ikMode[i] == 3)
												{
													HandL.localRotation = Quaternion.Euler(HandL.localEulerAngles) * Quaternion.AngleAxis(-1f, Vector3.right);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 4)
												{
													Head.RotateAround(Head.position, Vector3.up, 0.7f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 8)
												{
													IK_hand.RotateAround(IK_hand.position, Vector3.up, 0.7f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 6)
												{
													Spine1a.RotateAround(Spine1a.position, Vector3.up, 0.084f);
													Spine0a.RotateAround(Spine0a.position, Vector3.up, 0.156f);
													Spine.RotateAround(Spine.position, Vector3.up, 0.156f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if (ikMode[i] == 7)
												{
													Pelvis.RotateAround(Pelvis.position, Vector3.up, -0.4f);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if ((ikMode[i] == 0 || ikMode[i] == 1 || ikMode[i] == 2) && (ikBui == 3 || ikBui == 4))
												{
													UpperArmL.localRotation = Quaternion.Euler(UpperArmL.localEulerAngles) * Quaternion.AngleAxis(-0.5f, Vector3.right);
													isStop[i] = true;
													isLock[i] = true;
												}
												else if ((ikMode[i] == 0 || ikMode[i] == 1 || ikMode[i] == 2) && (ikBui == 1 || ikBui == 2))
												{
													UpperArmL.RotateAround(UpperArmL.position, Vector3.right, -0.5f);
													isStop[i] = true;
													isLock[i] = true;
												}
											}
											else if (Input.GetKey(KeyCode.Alpha0))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5.y += 0.005f;
													ikLeftArm.Init(UpperArmL, HandL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, HandL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5.y += 0.005f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5.y += 0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5.y += 0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
											else if (Input.GetKey(KeyCode.P))
											{
												if (ikMode[i] == 0)
												{
													Vector3 vector5 = HandL.position;
													vector5.y -= 0.005f;
													ikLeftArm.Init(UpperArmL, HandL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, HandL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 2)
												{
													Vector3 vector5 = HandL.position;
													vector5.y -= 0.005f;
													ikLeftArm.Init(ForearmL, ForearmL, HandL, maidArray[i].body0);
													ikLeftArm.Porc(ForearmL, ForearmL, HandL, vector5, default(Vector3));
												}
												else if (ikMode[i] == 5)
												{
													Vector3 vector5 = ForearmL.position;
													vector5.y -= 0.0035f;
													ikLeftArm.Init(Clavicle, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(Clavicle, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												else
												{
													Vector3 vector5 = ForearmL.position;
													vector5.y -= 0.0035f;
													ikLeftArm.Init(UpperArmL, UpperArmL, ForearmL, maidArray[i].body0);
													ikLeftArm.Porc(UpperArmL, UpperArmL, ForearmL, vector5, default(Vector3));
												}
												isStop[i] = true;
												isLock[i] = true;
											}
										}
									}
								}
							}
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.LeftBracket) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							maid.transform.RotateAround(maid.transform.position, Vector3.up, -0.75f);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.LeftBracket))
						{
							maid.transform.RotateAround(maid.transform.position, Vector3.up, 0.75f);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.DownArrow) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							if (!xFlg[i])
							{
								xFlg[i] = true;
								zFlg[i] = false;
								if (maid.status.personal.uniqueName == "Pride")
								{
									string text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < tunArray.Length; k++)
									{
										if (tunArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									string text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < coolArray.Length; k++)
									{
										if (coolArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									string text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < pureArray.Length; k++)
									{
										if (pureArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + "0" + num4.ToString() + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									string text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < yanArray.Length; k++)
									{
										if (yanArray[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (!flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									string text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h0Array.Length; k++)
									{
										if (h0Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									string text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h1Array.Length; k++)
									{
										if (h1Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									string text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(10000);
									bool flag7 = false;
									for (int k = 0; k < h2Array.Length; k++)
									{
										if (h2Array[k] == num4)
										{
											flag7 = true;
											break;
										}
									}
									if (flag7)
									{
										text = text + string.Format("{0:00000}", num4) + ".ogg";
										if (GameUty.FileSystem.IsExistentFile(text))
										{
											maid.AudioMan.LoadPlay(text, 0f, false, false);
										}
									}
								}
							}
							else
							{
								xFlg[i] = false;
								maid.AudioMan.Clear();
							}
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.UpArrow) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							if (!zFlg[i])
							{
								zFlg[i] = true;
								xFlg[i] = false;
								string text = "";
								if (maid.status.personal.uniqueName == "Pride")
								{
									text = "s0_";
									System.Random random = new System.Random();
									int num4 = random.Next(tunArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										tunArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Cool")
								{
									text = "s1_";
									System.Random random = new System.Random();
									int num4 = random.Next(coolArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										coolArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Pure")
								{
									text = "s2_";
									System.Random random = new System.Random();
									int num4 = random.Next(pureArray.Length);
									object obj = text;
									text = string.Concat(new object[]
									{
										obj,
										"0",
										pureArray[num4],
										".ogg"
									});
								}
								if (maid.status.personal.uniqueName == "Yandere")
								{
									text = "s3_";
									System.Random random = new System.Random();
									int num4 = random.Next(yanArray.Length);
									text = text + string.Format("{0:00000}", yanArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Muku")
								{
									text = "h0_";
									System.Random random = new System.Random();
									int num4 = random.Next(h0tArray.Length);
									text = text + string.Format("{0:00000}", h0tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Majime")
								{
									text = "h1_";
									System.Random random = new System.Random();
									int num4 = random.Next(h1tArray.Length);
									text = text + string.Format("{0:00000}", h1tArray[num4]) + ".ogg";
								}
								if (maid.status.personal.uniqueName == "Rindere")
								{
									text = "h2_";
									System.Random random = new System.Random();
									int num4 = random.Next(h2tArray.Length);
									text = text + string.Format("{0:00000}", h2tArray[num4]) + ".ogg";
								}
								maid.AudioMan.LoadPlay(text, 0f, false, false);
							}
							else
							{
								zFlg[i] = false;
								maid.AudioMan.Clear();
							}
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.UpArrow))
						{
							string text = "";
							if (maid.status.personal.uniqueName == "Pride")
							{
								text = "s0_";
								System.Random random = new System.Random();
								int num4 = random.Next(tunArray.Length);
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									"0",
									tunArray[num4],
									".ogg"
								});
							}
							if (maid.status.personal.uniqueName == "Cool")
							{
								text = "s1_";
								System.Random random = new System.Random();
								int num4 = random.Next(coolArray.Length);
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									"0",
									coolArray[num4],
									".ogg"
								});
							}
							if (maid.status.personal.uniqueName == "Pure")
							{
								text = "s2_";
								System.Random random = new System.Random();
								int num4 = random.Next(pureArray.Length);
								object obj = text;
								text = string.Concat(new object[]
								{
									obj,
									"0",
									pureArray[num4],
									".ogg"
								});
							}
							if (maid.status.personal.uniqueName == "Yandere")
							{
								text = "s3_";
								System.Random random = new System.Random();
								int num4 = random.Next(yanArray.Length);
								text = text + string.Format("{0:00000}", yanArray[num4]) + ".ogg";
							}
							if (maid.status.personal.uniqueName == "Muku")
							{
								text = "h0_";
								System.Random random = new System.Random();
								int num4 = random.Next(h0tArray.Length);
								text = text + string.Format("{0:00000}", h0tArray[num4]) + ".ogg";
							}
							if (maid.status.personal.uniqueName == "Majime")
							{
								text = "h1_";
								System.Random random = new System.Random();
								int num4 = random.Next(h1tArray.Length);
								text = text + string.Format("{0:00000}", h1tArray[num4]) + ".ogg";
							}
							if (maid.status.personal.uniqueName == "Rindere")
							{
								text = "h2_";
								System.Random random = new System.Random();
								int num4 = random.Next(h2tArray.Length);
								text = text + string.Format("{0:00000}", h2tArray[num4]) + ".ogg";
							}
							maid.AudioMan.LoadPlay(text, 0f, false, false);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.DownArrow))
						{
							if (maid.status.personal.uniqueName == "Pride")
							{
								string text = "s0_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < tunArray.Length; k++)
								{
									if (tunArray[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (!flag7)
								{
									text = text + "0" + num4.ToString() + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Cool")
							{
								string text = "s1_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < coolArray.Length; k++)
								{
									if (coolArray[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (!flag7)
								{
									text = text + "0" + num4.ToString() + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Pure")
							{
								string text = "s2_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < pureArray.Length; k++)
								{
									if (pureArray[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (!flag7)
								{
									text = text + "0" + num4.ToString() + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Yandere")
							{
								string text = "s3_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < yanArray.Length; k++)
								{
									if (yanArray[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (!flag7)
								{
									text = text + string.Format("{0:00000}", num4) + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Muku")
							{
								string text = "h0_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < h0Array.Length; k++)
								{
									if (h0Array[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (flag7)
								{
									text = text + string.Format("{0:00000}", num4) + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Majime")
							{
								string text = "h1_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < h1Array.Length; k++)
								{
									if (h1Array[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (flag7)
								{
									text = text + string.Format("{0:00000}", num4) + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							if (maid.status.personal.uniqueName == "Rindere")
							{
								string text = "h2_";
								System.Random random = new System.Random();
								int num4 = random.Next(10000);
								bool flag7 = false;
								for (int k = 0; k < h2Array.Length; k++)
								{
									if (h2Array[k] == num4)
									{
										flag7 = true;
										break;
									}
								}
								if (flag7)
								{
									text = text + string.Format("{0:00000}", num4) + ".ogg";
									if (GameUty.FileSystem.IsExistentFile(text))
									{
										maid.AudioMan.LoadPlay(text, 0f, false, false);
									}
								}
							}
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Insert) && Input.GetKey(KeyCode.Home))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Insert) && Input.GetKey(KeyCode.End))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Delete) && Input.GetKey(KeyCode.Home))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Delete) && Input.GetKey(KeyCode.End))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Insert))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Delete))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Home))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.End))
						{
							Vector3 vector = maid.transform.position;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.PageUp))
						{
							Vector3 vector = maid.transform.position;
							vector.y += 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.PageDown))
						{
							Vector3 vector = maid.transform.position;
							vector.y -= 0.0075f * speed;
							maid.SetPos(vector);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Minus) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							faceIndex[i]--;
							if (faceIndex[i] <= -1)
							{
								faceIndex[i] = faceArray.Length - 1;
							}
							maid.FaceAnime(faceArray[faceIndex[i]], 1f, 0);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Minus))
						{
							faceIndex[i]++;
							if (faceIndex[i] == faceArray.Length)
							{
								faceIndex[i] = 0;
							}
							maid.FaceAnime(faceArray[faceIndex[i]], 1f, 0);
							idoFlg[i] = true;
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.Quote))
						{
							faceBlendIndex[i]++;
							if (faceBlendIndex[i] == faceBlendArray.Length)
							{
								faceBlendIndex[i] = 0;
							}
							maid.FaceBlend(faceBlendArray[faceBlendIndex[i]]);
							idoFlg[i] = true;
						}
						if (Input.GetKey(key) && Input.GetKey(KeyCode.E))
						{
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
							maid.transform.localScale = new Vector3(maid.transform.localScale.x * 1.005f, maid.transform.localScale.y * 1.005f, maid.transform.localScale.z * 1.005f);
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.D))
						{
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
							maid.transform.localScale = new Vector3(maid.transform.localScale.x * 0.995f, maid.transform.localScale.y * 0.995f, maid.transform.localScale.z * 0.995f);
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.C))
						{
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
							maid.transform.localScale = new Vector3(1f, 1f, 1f);
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.H))
						{
							maid.body0.SetMask(TBody.SlotID.wear, true);
							maid.body0.SetMask(TBody.SlotID.skirt, true);
							maid.body0.SetMask(TBody.SlotID.bra, true);
							maid.body0.SetMask(TBody.SlotID.panz, true);
							maid.body0.SetMask(TBody.SlotID.mizugi, true);
							maid.body0.SetMask(TBody.SlotID.onepiece, true);
							isWear = true;
							isSkirt = true;
							isBra = true;
							isPanz = true;
							System.Random random = new System.Random();
							int num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.wear, false);
							}
							if (num4 == 1)
							{
								isWear = false;
							}
							num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.skirt, false);
							}
							if (num4 == 1)
							{
								isSkirt = false;
							}
							num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.bra, false);
							}
							if (num4 == 1)
							{
								isBra = false;
							}
							num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.panz, false);
							}
							if (num4 == 1)
							{
								isPanz = false;
							}
							num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.mizugi, false);
							}
							num4 = random.Next(2);
							if (num4 == 1)
							{
								maid.body0.SetMask(TBody.SlotID.onepiece, false);
							}
							hFlg = true;
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKeyDown(key) && Input.GetKey(KeyCode.LeftShift) && !isLock[i])
						{
							poseIndex[i]--;
							if (poseIndex[i] <= -1)
							{
								poseIndex[i] = poseArray.Length - 1;
							}
							if (maid && maid.Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maid.body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maid.body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maid.body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maid.body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKeyDown(key) && Input.GetKey(KeyCode.RightAlt) && !isLock[i])
						{
							int num11 = poseIndex[i];
							for (int k = 0; k < groupList.Count; k++)
							{
								if (poseIndex[i] < (int)groupList[k])
								{
									poseIndex[i] = (int)groupList[k];
									break;
								}
							}
							if (num11 == poseIndex[i] && poseIndex[i] >= (int)groupList[groupList.Count - 1])
							{
								poseIndex[i] = 0;
							}
							if (maid && maid.Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maid.CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maid.body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maid.body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maid.body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maid.body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKeyDown(key) && Input.GetKey(KeyCode.LeftAlt) && !isLock[i])
						{
							for (int k = 0; k < groupList.Count; k++)
							{
								if (k == 0 && poseIndex[i] <= (int)groupList[k])
								{
									if (poseIndex[i] == 0)
									{
										poseIndex[i] = (int)groupList[groupList.Count - 1];
									}
									else
									{
										poseIndex[i] = 0;
									}
									break;
								}
								if (k > 0 && poseIndex[i] > (int)groupList[k - 1] && poseIndex[i] <= (int)groupList[k])
								{
									poseIndex[i] = (int)groupList[k - 1];
									break;
								}
							}
							if (poseIndex[i] > (int)groupList[groupList.Count - 1])
							{
								poseIndex[i] = (int)groupList[groupList.Count - 1];
							}
							if (maid && maid.Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maid.CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maid.body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maid.body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maid.body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maid.body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKey(key) && Input.GetKeyDown(KeyCode.BackQuote) && !atFlg)
						{
							headEyeIndex[i]++;
							if (headEyeIndex[i] == headEyeArray.Length)
							{
								headEyeIndex[i] = 0;
							}
							maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
							maid.body0.boHeadToCam = true;
							maid.body0.boEyeToCam = true;
							if (headEyeIndex[i] == 0)
							{
								maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
								maid.body0.boHeadToCam = true;
								maid.body0.boEyeToCam = true;
							}
							else
							{
								maid.body0.trsLookTarget = null;
								CameraMain cameraMain = GameMain.Instance.MainCamera;
								if (headEyeIndex[i] == 1)
								{
									maid.body0.offsetLookTarget = new Vector3(-0.35f, 1f, -0.3f);
								}
								if (headEyeIndex[i] == 2)
								{
									maid.body0.offsetLookTarget = new Vector3(-0.4f, 1f, 0f);
								}
								if (headEyeIndex[i] == 3)
								{
									maid.body0.offsetLookTarget = new Vector3(-0.35f, 1f, 0.3f);
								}
								if (headEyeIndex[i] == 4)
								{
									maid.body0.offsetLookTarget = new Vector3(0f, 1f, -0.4f);
								}
								if (headEyeIndex[i] == 5)
								{
									maid.body0.offsetLookTarget = new Vector3(0f, 1f, 0f);
								}
								if (headEyeIndex[i] == 6)
								{
									maid.body0.offsetLookTarget = new Vector3(0f, 1f, 0.4f);
								}
								if (headEyeIndex[i] == 7)
								{
									maid.body0.offsetLookTarget = new Vector3(0.4f, 1f, -0.4f);
								}
								if (headEyeIndex[i] == 8)
								{
									maid.body0.offsetLookTarget = new Vector3(0.4f, 1f, 0f);
								}
								if (headEyeIndex[i] == 9)
								{
									maid.body0.offsetLookTarget = new Vector3(0.4f, 1f, 0.4f);
								}
							}
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKey(key) && Input.GetKey(KeyCode.Return))
						{
							idoFlg[i] = true;
							if (i >= 7)
							{
								idoFlg[i - 7] = true;
							}
						}
						else if (Input.GetKeyUp(key))
						{
							if (idoFlg[i])
							{
								idoFlg[i] = false;
							}
							else if (!getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) || i >= 7)
							{
								if (!isLock[i])
								{
									poseIndex[i]++;
									if (poseIndex[i] == poseArray.Length)
									{
										poseIndex[i] = 0;
									}
									if (maid && maid.Visible)
									{
										string[] array = poseArray[poseIndex[i]].Split(new char[]
										{
											','
										});
										isStop[i] = false;
										poseCount[i] = 20;
										if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
										{
											maid.body0.MuneYureL(0f);
											maid.body0.MuneYureR(0f);
										}
										else
										{
											maid.body0.MuneYureL(1f);
											maid.body0.MuneYureR(1f);
										}
										int num6;
										if (array[0].Contains("AdvancedStudioModePose"))
										{
											string path = array[0].Split(new char[]
											{
												'/'
											})[1];
											byte[] array2 = new byte[0];
											try
											{
												using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
												{
													array2 = new byte[fileStream.Length];
													fileStream.Read(array2, 0, array2.Length);
												}
											}
											catch
											{
											}
											if (0 < array2.Length)
											{
												string text = Path.GetFileName(path);
												long num5 = (long)text.GetHashCode();
												maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
												Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
												{
													default(Maid.AutoTwist),
													Maid.AutoTwist.ShoulderR,
													Maid.AutoTwist.WristL,
													Maid.AutoTwist.WristR,
													Maid.AutoTwist.ThighL,
													Maid.AutoTwist.ThighR
												};
												for (int m = 0; m < array3.Length; m++)
												{
													maid.SetAutoTwist(array3[m], true);
												}
											}
										}
										else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
										{
											loadPose[i] = array[0];
										}
										else if (!array[0].StartsWith("dance_"))
										{
											maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
										}
										else
										{
											if (!maid.body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
											{
												maid.body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
											}
											maid.body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
										}
										if (array.Length > 1)
										{
											maid.body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
											isStop[i] = true;
											if (array.Length > 2)
											{
												Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
												isPoseIti[i] = true;
												poseIti[i] = maidArray[i].transform.position;
												maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
											}
										}
									}
								}
							}
						}
						goto IL_165DC;
					}
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
				}
				if (!isVR && Input.GetKeyDown(KeyCode.F8) && sceneFlg && bGui)
				{
					bGui = false;
				}
				else if (!isVR && Input.GetKeyDown(KeyCode.F8))
				{
					sceneFlg = true;
					faceFlg = false;
					poseFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					bGui = true;
					int i = 0;
					while (i < 10)
					{
						date[i] = "未保存";
						ninzu[i] = "";
						string path2 = string.Concat(new object[]
						{
							Path.GetFullPath(".\\"),
							"Mod\\AdvancedStudioModeScene\\",
							page * 10 + i + 1,
							".png"
						});
						if (File.Exists(path2))
						{
							FileStream fileStream = new FileStream(path2, FileMode.Open, FileAccess.Read);
							BinaryReader binaryReader = new BinaryReader(fileStream);
							byte[] array4 = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
							byte[] value = new byte[]
							{
								array4[36],
								array4[35],
								array4[34],
								array4[33]
							};
							int count = BitConverter.ToInt32(value, 0) - 8;
							byte[] bytes = array4.Skip(49).Take(count).ToArray<byte>();
							string text2 = Encoding.UTF8.GetString(bytes);
							text2 = AdvancedStudioMode.StringFromBase64Comp(text2);
							if (text2 != "")
							{
								string[] array5 = text2.Split(new char[]
								{
									'_'
								});
								if (array5.Length >= 2)
								{
									string[] array6 = array5[0].Split(new char[]
									{
										','
									});
									date[i] = array6[0];
									ninzu[i] = array6[1] + "人";
								}
							}
						}
						else
						{
							IniKey iniKey = base.Preferences["scene"]["s" + (page * 10 + i + 1)];
							if (iniKey.Value != null && iniKey.Value.ToString() != "")
							{
								string[] array5 = iniKey.Value.Split(new char[]
								{
									'_'
								});
								if (array5.Length >= 2)
								{
									string[] array6 = array5[0].Split(new char[]
									{
										','
									});
									date[i] = array6[0];
									ninzu[i] = array6[1] + "人";
								}
							}
						}
						//IL_168F6:
						i++;
						continue;
						//goto IL_168F6;
					}
				}
				if (Input.GetKeyDown(KeyCode.F) && getModKeyPressing(AdvancedStudioMode.modKey.Shift) && !fFlg)
				{
					bgmIndex--;
					if (bgmIndex <= -1)
					{
						bgmIndex = bgmArray.Length - 1;
					}
					GameMain.Instance.SoundMgr.PlayBGM(bgmArray[bgmIndex] + ".ogg", 0f, true);
					bgmCombo.selectedItemIndex = bgmIndex;
				}
				else if (Input.GetKeyDown(KeyCode.F) && !fFlg)
				{
					bgmIndex++;
					if (bgmIndex == bgmArray.Length)
					{
						bgmIndex = 0;
					}
					GameMain.Instance.SoundMgr.PlayBGM(bgmArray[bgmIndex] + ".ogg", 0f, true);
					bgmCombo.selectedItemIndex = bgmIndex;
				}
				if (!isVR && Input.GetKeyDown(KeyCode.M) && !mFlg)
				{
					GameObject gameObject4 = GameObject.Find("__GameMain__/SystemUI Root");
					GameObject gameObject5 = gameObject4.transform.Find("MessageWindowPanel").gameObject;
					MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
					if (isMessage)
					{
						isMessage = false;
						messageWindowMgr.CloseMessageWindowPanel();
					}
					else if (!bGuiMessage)
					{
						bGuiMessage = true;
					}
					else
					{
						bGuiMessage = false;
						isMessage = false;
						messageWindowMgr.CloseMessageWindowPanel();
					}
				}
				if (isDanceChu && Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Escape))
				{
					escFlg = true;
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].StopKuchipakuPattern();
							if (isDanceStart7V)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
							}
							if (isDanceStart8V)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
							}
							if (isDanceStart11V)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
							}
							if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
								int i = j;
								Maid maid = maidArray[j];
								maid.DelProp(MPN.handitem, true);
								maid.DelProp(MPN.accvag, true);
								maid.DelProp(MPN.accanl, true);
								maid.DelProp(MPN.kousoku_upper, true);
								maid.DelProp(MPN.kousoku_lower, true);
								maid.AllProcPropSeqStart();
							}
							if (isDanceStart14V)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
							}
							if (isDanceStart15V)
							{
								maidArray[j].SetPos(dancePos[j]);
								maidArray[j].body0.transform.localRotation = danceRot[j];
							}
						}
					}
					isDanceStart1F = false;
					isDanceStart1K = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart3K = false;
					isDanceStart4F = false;
					isDanceStart4K = false;
					isDanceStart5F = false;
					isDanceStart5K = false;
					isDanceStart6F = false;
					isDanceStart6K = false;
					isDanceStart7F = false;
					isDanceStart7V = false;
					isDanceStart8F = false;
					isDanceStart8V = false;
					isDanceStart8P = false;
					isDanceStart9F = false;
					isDanceStart9K = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart11V = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart13K = false;
					isDanceStart14F = false;
					isDanceStart14V = false;
					isDanceStart15F = false;
					isDanceStart15V = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
						maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
						maidArray[j].SetAutoTwistAll(true);
					}
					danceCheckIndex = 0;
					for (int k = 0; k < danceCheck.Length; k++)
					{
						danceCheck[danceCheckIndex] = 1f;
					}
					isDance = false;
					isDanceChu = false;
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					GameMain.Instance.SoundMgr.PlayBGM(bgmArray[bgmIndex] + ".ogg", 0f, true);
				}
				if (Input.GetKeyDown(KeyCode.Tab) || (!escFlg && Input.GetKeyDown(KeyCode.Escape)))
				{
					if (isScript)
					{
						GameObject gameObject4 = GameObject.Find("__GameMain__/SystemUI Root");
						GameObject gameObject5 = gameObject4.transform.Find("MessageWindowPanel").gameObject;
						if (isPanel)
						{
							isPanel = false;
						}
						else
						{
							isPanel = true;
						}
						gameObject5.SetActive(isPanel);
					}
					else
					{
						bGui = !bGui;
					}
				}
				if (Input.GetKeyDown(KeyCode.Y) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
				{
					keyFlg = true;
					bgIndex--;
					if (bgIndex <= -1)
					{
						bgIndex = bgArray.Length - 1;
					}
					if (!moveBg)
					{
						bg.localScale = new Vector3(1f, 1f, 1f);
						if (bgArray[bgIndex].Length == 36)
						{
							GameMain.Instance.BgMgr.ChangeBgMyRoom(bgArray[bgIndex]);
						}
						else
						{
							GameMain.Instance.BgMgr.ChangeBg(bgArray[bgIndex]);
						}
						bgCombo.selectedItemIndex = bgIndex;
						if (bgArray[bgIndex] == "karaokeroom")
						{
							bg.transform.position = bgObject.transform.position;
							Vector3 vector = Vector3.zero;
							Vector3 zero3 = Vector3.zero;
							zero3.y = 90f;
							vector.z = 4f;
							vector.x = 1f;
							bg.transform.localPosition = vector;
							bg.transform.localRotation = Quaternion.Euler(zero3);
						}
					}
				}
				else if (Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.E))
				{
					keyFlg = true;
					bg.localScale = new Vector3(bg.localScale.x * 1.005f, bg.localScale.y * 1.005f, bg.localScale.z * 1.005f);
				}
				else if (Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.D))
				{
					keyFlg = true;
					bg.localScale = new Vector3(bg.localScale.x * 0.995f, bg.localScale.y * 0.995f, bg.localScale.z * 0.995f);
				}
				else if (Input.GetKey(KeyCode.Y) && Input.GetKeyDown(KeyCode.C))
				{
					keyFlg = true;
					bg.localScale = new Vector3(1f, 1f, 1f);
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
					vector += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
					vector += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
					vector += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
					vector += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.I))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.J))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.L))
				{
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					Vector3 vector = bg.position;
					vector += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.Alpha0))
				{
					Vector3 vector = bg.position;
					vector.y -= 0.015f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.P))
				{
					Vector3 vector = bg.position;
					vector.y += 0.015f * speed;
					bg.localPosition = vector;
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.U))
				{
					bg.RotateAround(maidArray[0].transform.position, Vector3.up, 0.7f);
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.O))
				{
					bg.RotateAround(maidArray[0].transform.position, Vector3.up, -0.7f);
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if (Input.GetKeyDown(KeyCode.Y) && Input.GetKey(KeyCode.Return))
				{
					if (Input.GetKey(KeyCode.Y))
					{
						keyFlg = true;
					}
				}
				else if (Input.GetKeyUp(KeyCode.Y) && !yFlg)
				{
					if (keyFlg)
					{
						keyFlg = false;
					}
					else
					{
						bgIndex++;
						if (bgIndex == bgArray.Length)
						{
							bgIndex = 0;
						}
						if (!moveBg)
						{
							bg.localScale = new Vector3(1f, 1f, 1f);
							if (bgArray[bgIndex].Length == 36)
							{
								GameMain.Instance.BgMgr.ChangeBgMyRoom(bgArray[bgIndex]);
							}
							else
							{
								GameMain.Instance.BgMgr.ChangeBg(bgArray[bgIndex]);
							}
							bgCombo.selectedItemIndex = bgIndex;
							if (bgArray[bgIndex] == "karaokeroom")
							{
								bg.transform.position = bgObject.transform.position;
								Vector3 vector = Vector3.zero;
								Vector3 zero3 = Vector3.zero;
								zero3.y = 90f;
								vector.z = 4f;
								vector.x = 1f;
								bg.transform.localPosition = vector;
								bg.transform.localRotation = Quaternion.Euler(zero3);
							}
						}
					}
				}
				if (maidArray[0] != null && maidArray[0].Visible)
				{
					if (Input.GetKeyDown(KeyCode.Comma) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						int[] array7 = new int[1];
						int[] array8 = array7;
						if (maidCnt == 2)
						{
							array8 = new int[]
							{
								0,
								1
							};
						}
						if (maidCnt == 3)
						{
							array8 = new int[]
							{
								0,
								1,
								2
							};
						}
						if (maidCnt == 4)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3
							};
						}
						if (maidCnt == 5)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4
							};
						}
						if (maidCnt == 6)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5
							};
						}
						if (maidCnt == 7)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6
							};
						}
						if (maidCnt == 8)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6,
								7
							};
						}
						if (maidCnt == 9)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6,
								7,
								8
							};
						}
						if (maidCnt == 10)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6,
								7,
								8,
								9
							};
						}
						if (maidCnt == 11)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6,
								7,
								8,
								9,
								10
							};
						}
						if (maidCnt == 12)
						{
							array8 = new int[]
							{
								0,
								1,
								2,
								3,
								4,
								5,
								6,
								7,
								8,
								9,
								10,
								11
							};
						}
						System.Random random = new System.Random();
						int n = array8.Length;
						while (n > 1)
						{
							n--;
							int j = random.Next(n + 1);
							int num12 = array8[j];
							array8[j] = array8[n];
							array8[n] = num12;
						}
						int[] array9 = new int[maidCnt];
						Vector3[] array10 = new Vector3[maidCnt];
						Vector3[] array11 = new Vector3[maidCnt];
						int[] array12 = new int[maidCnt];
						for (int i = 0; i < maidCnt; i++)
						{
							array9[array8[i]] = poseIndex[array8[i]];
							array10[array8[i]] = maidArray[array8[i]].transform.localRotation.eulerAngles;
							array11[array8[i]] = maidArray[array8[i]].transform.position;
							array12[array8[i]] = headEyeIndex[array8[i]];
						}
						for (int i = 0; i < maidCnt; i++)
						{
							if (maidArray[i] && maidArray[i].Visible && !isLock[i])
							{
								string[] array13 = poseArray[array9[array8[i]]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								Maid maid = maidArray[i];
								if (array13[0].Contains("_momi") || array13[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array13[0].Contains("AdvancedStudioModePose"))
								{
									string path = array13[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array13[0].StartsWith("p") && int.TryParse(array13[0].Substring(1), out num6))
								{
									loadPose[i] = array13[0];
								}
								else if (!array13[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array13[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array13[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array13[0] + ".anm", GameUty.FileSystem, array13[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array13[0] + ".anm");
								}
								if (array13.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array13[0] + ".anm"].time = float.Parse(array13[1]);
									isStop[i] = true;
									if (array13.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
							poseIndex[i] = array9[array8[i]];
							maidArray[i].SetRot(array10[array8[i]]);
							maidArray[i].SetPos(array11[array8[i]]);
							headEyeIndex[i] = array12[array8[i]];
							maidArray[i].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
							maidArray[i].body0.boHeadToCam = true;
							maidArray[i].body0.boEyeToCam = true;
							if (headEyeIndex[i] == 0)
							{
								maidArray[i].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
								maidArray[i].body0.boHeadToCam = true;
								maidArray[i].body0.boEyeToCam = true;
							}
							else
							{
								maidArray[i].body0.trsLookTarget = null;
								if (headEyeIndex[i] == 1)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.7f, 1f, -0.6f);
								}
								if (headEyeIndex[i] == 2)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.6f, 1f, 0f);
								}
								if (headEyeIndex[i] == 3)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.7f, 1f, 0.6f);
								}
								if (headEyeIndex[i] == 4)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.1f, 1f, -0.4f);
								}
								if (headEyeIndex[i] == 5)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0f, 1f, 0f);
								}
								if (headEyeIndex[i] == 6)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.1f, 1f, 0.4f);
								}
								if (headEyeIndex[i] == 7)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.4f, 1f, -0.5f);
								}
								if (headEyeIndex[i] == 8)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.3f, 1f, 0f);
								}
								if (headEyeIndex[i] == 9)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.4f, 1f, 0.5f);
								}
							}
						}
					}
					else if (Input.GetKeyDown(KeyCode.Comma))
					{
						int[] array9 = new int[maidCnt];
						Vector3[] array10 = new Vector3[maidCnt];
						Vector3[] array11 = new Vector3[maidCnt];
						int[] array12 = new int[maidCnt];
						for (int i = 0; i < maidCnt; i++)
						{
							array9[i] = poseIndex[i];
							array10[i] = maidArray[i].transform.localRotation.eulerAngles;
							array11[i] = maidArray[i].transform.position;
							array12[i] = headEyeIndex[i];
						}
						for (int i = 0; i < maidCnt; i++)
						{
							if (i == 0)
							{
								if (!isLock[i])
								{
									if (maidArray[i] && maidArray[i].Visible)
									{
										string[] array13 = poseArray[array9[maidCnt - i - 1]].Split(new char[]
										{
											','
										});
										isStop[i] = false;
										poseCount[i] = 20;
										Maid maid = maidArray[i];
										if (array13[0].Contains("_momi") || array13[0].Contains("paizuri_"))
										{
											maid.body0.MuneYureL(0f);
											maid.body0.MuneYureR(0f);
										}
										else
										{
											maid.body0.MuneYureL(1f);
											maid.body0.MuneYureR(1f);
										}
										int num6;
										if (array13[0].Contains("AdvancedStudioModePose"))
										{
											string path = array13[0].Split(new char[]
											{
												'/'
											})[1];
											byte[] array2 = new byte[0];
											try
											{
												using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
												{
													array2 = new byte[fileStream.Length];
													fileStream.Read(array2, 0, array2.Length);
												}
											}
											catch
											{
											}
											if (0 < array2.Length)
											{
												string text = Path.GetFileName(path);
												long num5 = (long)text.GetHashCode();
												maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
												Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
												{
													default(Maid.AutoTwist),
													Maid.AutoTwist.ShoulderR,
													Maid.AutoTwist.WristL,
													Maid.AutoTwist.WristR,
													Maid.AutoTwist.ThighL,
													Maid.AutoTwist.ThighR
												};
												for (int m = 0; m < array3.Length; m++)
												{
													maid.SetAutoTwist(array3[m], true);
												}
											}
										}
										else if (array13[0].StartsWith("p") && int.TryParse(array13[0].Substring(1), out num6))
										{
											loadPose[i] = array13[0];
										}
										else if (!array13[0].StartsWith("dance_"))
										{
											maidArray[i].CrossFade(array13[0] + ".anm", false, true, false, 0f, 1f);
										}
										else
										{
											if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array13[0] + ".anm"))
											{
												maidArray[i].body0.LoadAnime(array13[0] + ".anm", GameUty.FileSystem, array13[0] + ".anm", false, false);
											}
											maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array13[0] + ".anm");
										}
										if (array13.Length > 1)
										{
											maidArray[i].body0.m_Bones.GetComponent<Animation>()[array13[0] + ".anm"].time = float.Parse(array13[1]);
											isStop[i] = true;
											if (array13.Length > 2)
											{
												Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
												isPoseIti[i] = true;
												poseIti[i] = maidArray[i].transform.position;
												maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
											}
										}
									}
								}
								poseIndex[i] = array9[maidCnt - i - 1];
								headEyeIndex[i] = array12[maidCnt - i - 1];
							}
							else
							{
								if (!isLock[i])
								{
									if (maidArray[i] && maidArray[i].Visible)
									{
										string[] array13 = poseArray[array9[i - 1]].Split(new char[]
										{
											','
										});
										isStop[i] = false;
										poseCount[i] = 20;
										Maid maid = maidArray[i];
										if (array13[0].Contains("_momi") || array13[0].Contains("paizuri_"))
										{
											maid.body0.MuneYureL(0f);
											maid.body0.MuneYureR(0f);
										}
										else
										{
											maid.body0.MuneYureL(1f);
											maid.body0.MuneYureR(1f);
										}
										int num6;
										if (array13[0].Contains("AdvancedStudioModePose"))
										{
											string path = array13[0].Split(new char[]
											{
												'/'
											})[1];
											byte[] array2 = new byte[0];
											try
											{
												using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
												{
													array2 = new byte[fileStream.Length];
													fileStream.Read(array2, 0, array2.Length);
												}
											}
											catch
											{
											}
											if (0 < array2.Length)
											{
												string text = Path.GetFileName(path);
												long num5 = (long)text.GetHashCode();
												maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
												Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
												{
													default(Maid.AutoTwist),
													Maid.AutoTwist.ShoulderR,
													Maid.AutoTwist.WristL,
													Maid.AutoTwist.WristR,
													Maid.AutoTwist.ThighL,
													Maid.AutoTwist.ThighR
												};
												for (int m = 0; m < array3.Length; m++)
												{
													maid.SetAutoTwist(array3[m], true);
												}
											}
										}
										else if (array13[0].StartsWith("p") && int.TryParse(array13[0].Substring(1), out num6))
										{
											loadPose[i] = array13[0];
										}
										else if (!array13[0].StartsWith("dance_"))
										{
											maidArray[i].CrossFade(array13[0] + ".anm", false, true, false, 0f, 1f);
										}
										else
										{
											if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array13[0] + ".anm"))
											{
												maidArray[i].body0.LoadAnime(array13[0] + ".anm", GameUty.FileSystem, array13[0] + ".anm", false, false);
											}
											maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array13[0] + ".anm");
										}
										if (array13.Length > 1)
										{
											maidArray[i].body0.m_Bones.GetComponent<Animation>()[array13[0] + ".anm"].time = float.Parse(array13[1]);
											isStop[i] = true;
											if (array13.Length > 2)
											{
												Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
												isPoseIti[i] = true;
												poseIti[i] = maidArray[i].transform.position;
												maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
											}
										}
									}
								}
								poseIndex[i] = array9[i - 1];
								headEyeIndex[i] = array12[i - 1];
							}
							maidArray[i].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
							maidArray[i].body0.boHeadToCam = true;
							maidArray[i].body0.boEyeToCam = true;
							if (headEyeIndex[i] == 0)
							{
								maidArray[i].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
								maidArray[i].body0.boHeadToCam = true;
								maidArray[i].body0.boEyeToCam = true;
							}
							else
							{
								maidArray[i].body0.trsLookTarget = null;
								if (headEyeIndex[i] == 1)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.7f, 1f, -0.6f);
								}
								if (headEyeIndex[i] == 2)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.6f, 1f, 0f);
								}
								if (headEyeIndex[i] == 3)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.7f, 1f, 0.6f);
								}
								if (headEyeIndex[i] == 4)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.1f, 1f, -0.4f);
								}
								if (headEyeIndex[i] == 5)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0f, 1f, 0f);
								}
								if (headEyeIndex[i] == 6)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(-0.1f, 1f, 0.4f);
								}
								if (headEyeIndex[i] == 7)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.4f, 1f, -0.5f);
								}
								if (headEyeIndex[i] == 8)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.3f, 1f, 0f);
								}
								if (headEyeIndex[i] == 9)
								{
									maidArray[i].body0.offsetLookTarget = new Vector3(0.4f, 1f, 0.5f);
								}
							}
							if (i == 0)
							{
								maidArray[i].SetRot(array10[maidCnt - i - 1]);
								maidArray[i].SetPos(array11[maidCnt - i - 1]);
							}
							else
							{
								maidArray[i].SetRot(array10[i - 1]);
								maidArray[i].SetPos(array11[i - 1]);
							}
						}
					}
					for (int i = 0; i < maidCnt; i++)
					{
						if (Input.GetKey(KeyCode.B) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							if (maidArray[i] && maidArray[i].Visible)
							{
								maidArray[i].body0.transform.localRotation = Quaternion.Euler(maidArray[i].body0.transform.localEulerAngles) * Quaternion.AngleAxis(-1.5f, Vector3.up);
							}
						}
						else if (Input.GetKey(KeyCode.B))
						{
							if (maidArray[i] && maidArray[i].Visible)
							{
								maidArray[i].body0.transform.localRotation = Quaternion.Euler(maidArray[i].body0.transform.localEulerAngles) * Quaternion.AngleAxis(1.5f, Vector3.up);
							}
						}
						if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.LeftAlt) && !isLock[i])
						{
							if (maidArray[1] && maidArray[1].Visible)
							{
								if (maidArray[0].transform.position == maidArray[1].transform.position || (maidArray[2] && maidArray[0].transform.position == maidArray[2].transform.position))
								{
									Vector3 vector = Vector3.zero;
									vector.x = 0.6f;
									maidArray[1].SetPos(vector);
									if (maidArray[2] && maidArray[2].Visible)
									{
										vector.x = -0.6f;
										maidArray[2].SetPos(vector);
									}
									if (maidArray[3] && maidArray[3].Visible)
									{
										vector.x = 1.2f;
										maidArray[3].SetPos(vector);
									}
									if (maidArray[4] && maidArray[4].Visible)
									{
										vector.x = -1.2f;
										maidArray[4].SetPos(vector);
									}
									if (maidArray[5] && maidArray[5].Visible)
									{
										vector.x = 1.8f;
										maidArray[5].SetPos(vector);
									}
									if (maidArray[6] && maidArray[6].Visible)
									{
										vector.x = -1.8f;
										maidArray[6].SetPos(vector);
									}
									if (maidArray[7] && maidArray[7].Visible)
									{
										vector.x = 2.4f;
										maidArray[7].SetPos(vector);
									}
									if (maidArray[8] && maidArray[8].Visible)
									{
										vector.x = -2.4f;
										maidArray[8].SetPos(vector);
									}
									if (maidArray[9] && maidArray[9].Visible)
									{
										vector.x = 3f;
										maidArray[9].SetPos(vector);
									}
									if (maidArray[10] && maidArray[10].Visible)
									{
										vector.x = -3f;
										maidArray[10].SetPos(vector);
									}
									if (maidArray[11] && maidArray[11].Visible)
									{
										vector.x = 3.6f;
										maidArray[11].SetPos(vector);
									}
									if (maidArray[12] && maidArray[12].Visible)
									{
										vector.x = -3.6f;
										maidArray[12].SetPos(vector);
									}
									if (maidArray[13] && maidArray[13].Visible)
									{
										vector.x = 4.2f;
										maidArray[13].SetPos(vector);
									}
								}
							}
							for (int k = 0; k < groupList.Count; k++)
							{
								if (k == 0 && poseIndex[i] <= (int)groupList[k])
								{
									if (poseIndex[i] == 0)
									{
										poseIndex[i] = (int)groupList[groupList.Count - 1];
									}
									else
									{
										poseIndex[i] = 0;
									}
									break;
								}
								if (k > 0 && poseIndex[i] > (int)groupList[k - 1] && poseIndex[i] <= (int)groupList[k])
								{
									poseIndex[i] = (int)groupList[k - 1];
									break;
								}
							}
							if (poseIndex[i] > (int)groupList[groupList.Count - 1])
							{
								poseIndex[i] = (int)groupList[groupList.Count - 1];
							}
							if (maidArray[i] && maidArray[i].Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								Maid maid = maidArray[i];
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
						}
						else if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.RightAlt) && !isLock[i])
						{
							if (maidArray[1] && maidArray[1].Visible)
							{
								if (maidArray[0].transform.position == maidArray[1].transform.position || (maidArray[2] && maidArray[0].transform.position == maidArray[2].transform.position))
								{
									Vector3 vector = Vector3.zero;
									vector.x = 0.6f;
									maidArray[1].SetPos(vector);
									if (maidArray[2] && maidArray[2].Visible)
									{
										vector.x = -0.6f;
										maidArray[2].SetPos(vector);
									}
									if (maidArray[3] && maidArray[3].Visible)
									{
										vector.x = 1.2f;
										maidArray[3].SetPos(vector);
									}
									if (maidArray[4] && maidArray[4].Visible)
									{
										vector.x = -1.2f;
										maidArray[4].SetPos(vector);
									}
									if (maidArray[5] && maidArray[5].Visible)
									{
										vector.x = 1.8f;
										maidArray[5].SetPos(vector);
									}
									if (maidArray[6] && maidArray[6].Visible)
									{
										vector.x = -1.8f;
										maidArray[6].SetPos(vector);
									}
									if (maidArray[7] && maidArray[7].Visible)
									{
										vector.x = 2.4f;
										maidArray[7].SetPos(vector);
									}
									if (maidArray[8] && maidArray[8].Visible)
									{
										vector.x = -2.4f;
										maidArray[8].SetPos(vector);
									}
									if (maidArray[9] && maidArray[9].Visible)
									{
										vector.x = 3f;
										maidArray[9].SetPos(vector);
									}
									if (maidArray[10] && maidArray[10].Visible)
									{
										vector.x = -3f;
										maidArray[10].SetPos(vector);
									}
									if (maidArray[11] && maidArray[11].Visible)
									{
										vector.x = 3.6f;
										maidArray[11].SetPos(vector);
									}
									if (maidArray[12] && maidArray[12].Visible)
									{
										vector.x = -3.6f;
										maidArray[12].SetPos(vector);
									}
									if (maidArray[13] && maidArray[13].Visible)
									{
										vector.x = 4.2f;
										maidArray[13].SetPos(vector);
									}
								}
							}
							int num11 = poseIndex[i];
							for (int k = 0; k < groupList.Count; k++)
							{
								if (poseIndex[i] < (int)groupList[k])
								{
									poseIndex[i] = (int)groupList[k];
									break;
								}
							}
							if (num11 == poseIndex[i] && poseIndex[i] >= (int)groupList[groupList.Count - 1])
							{
								poseIndex[i] = 0;
							}
							if (maidArray[i] && maidArray[i].Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								Maid maid = maidArray[i];
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
						}
						else if (Input.GetKeyDown(KeyCode.N) && getModKeyPressing(AdvancedStudioMode.modKey.Shift) && !isLock[i])
						{
							if (maidArray[1] && maidArray[1].Visible)
							{
								if (maidArray[0].transform.position == maidArray[1].transform.position || (maidArray[2] && maidArray[0].transform.position == maidArray[2].transform.position))
								{
									Vector3 vector = Vector3.zero;
									vector.x = 0.6f;
									maidArray[1].SetPos(vector);
									if (maidArray[2] && maidArray[2].Visible)
									{
										vector.x = -0.6f;
										maidArray[2].SetPos(vector);
									}
									if (maidArray[3] && maidArray[3].Visible)
									{
										vector.x = 1.2f;
										maidArray[3].SetPos(vector);
									}
									if (maidArray[4] && maidArray[4].Visible)
									{
										vector.x = -1.2f;
										maidArray[4].SetPos(vector);
									}
									if (maidArray[5] && maidArray[5].Visible)
									{
										vector.x = 1.8f;
										maidArray[5].SetPos(vector);
									}
									if (maidArray[6] && maidArray[6].Visible)
									{
										vector.x = -1.8f;
										maidArray[6].SetPos(vector);
									}
									if (maidArray[7] && maidArray[7].Visible)
									{
										vector.x = 2.4f;
										maidArray[7].SetPos(vector);
									}
									if (maidArray[8] && maidArray[8].Visible)
									{
										vector.x = -2.4f;
										maidArray[8].SetPos(vector);
									}
									if (maidArray[9] && maidArray[9].Visible)
									{
										vector.x = 3f;
										maidArray[9].SetPos(vector);
									}
									if (maidArray[10] && maidArray[10].Visible)
									{
										vector.x = -3f;
										maidArray[10].SetPos(vector);
									}
									if (maidArray[11] && maidArray[11].Visible)
									{
										vector.x = 3.6f;
										maidArray[11].SetPos(vector);
									}
									if (maidArray[12] && maidArray[12].Visible)
									{
										vector.x = -3.6f;
										maidArray[12].SetPos(vector);
									}
									if (maidArray[13] && maidArray[13].Visible)
									{
										vector.x = 4.2f;
										maidArray[13].SetPos(vector);
									}
								}
							}
							poseIndex[i]--;
							if (poseIndex[i] <= -1)
							{
								poseIndex[i] = poseArray.Length - 1;
							}
							if (maidArray[i] && maidArray[i].Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								Maid maid = maidArray[i];
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
						}
						else if (Input.GetKeyDown(KeyCode.N) && !isLock[i])
						{
							if (maidArray[1] && maidArray[1].Visible)
							{
								if (maidArray[0].transform.position == maidArray[1].transform.position || (maidArray[2] && maidArray[0].transform.position == maidArray[2].transform.position))
								{
									Vector3 vector = Vector3.zero;
									vector.x = 0.6f;
									maidArray[1].SetPos(vector);
									if (maidArray[2] && maidArray[2].Visible)
									{
										vector.x = -0.6f;
										maidArray[2].SetPos(vector);
									}
									if (maidArray[3] && maidArray[3].Visible)
									{
										vector.x = 1.2f;
										maidArray[3].SetPos(vector);
									}
									if (maidArray[4] && maidArray[4].Visible)
									{
										vector.x = -1.2f;
										maidArray[4].SetPos(vector);
									}
									if (maidArray[5] && maidArray[5].Visible)
									{
										vector.x = 1.8f;
										maidArray[5].SetPos(vector);
									}
									if (maidArray[6] && maidArray[6].Visible)
									{
										vector.x = -1.8f;
										maidArray[6].SetPos(vector);
									}
									if (maidArray[7] && maidArray[7].Visible)
									{
										vector.x = 2.4f;
										maidArray[7].SetPos(vector);
									}
									if (maidArray[8] && maidArray[8].Visible)
									{
										vector.x = -2.4f;
										maidArray[8].SetPos(vector);
									}
									if (maidArray[9] && maidArray[9].Visible)
									{
										vector.x = 3f;
										maidArray[9].SetPos(vector);
									}
									if (maidArray[10] && maidArray[10].Visible)
									{
										vector.x = -3f;
										maidArray[10].SetPos(vector);
									}
									if (maidArray[11] && maidArray[11].Visible)
									{
										vector.x = 3.6f;
										maidArray[11].SetPos(vector);
									}
									if (maidArray[12] && maidArray[12].Visible)
									{
										vector.x = -3.6f;
										maidArray[12].SetPos(vector);
									}
									if (maidArray[13] && maidArray[13].Visible)
									{
										vector.x = 4.2f;
										maidArray[13].SetPos(vector);
									}
								}
							}
							poseIndex[i]++;
							if (Input.GetKey(KeyCode.Space))
							{
								poseIndex[i] += 9;
							}
							if (poseIndex[i] == poseArray.Length)
							{
								poseIndex[i] = 0;
							}
							if (maidArray[i] && maidArray[i].Visible)
							{
								string[] array = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = false;
								poseCount[i] = 20;
								Maid maid = maidArray[i];
								if (array[0].Contains("_momi") || array[0].Contains("paizuri_"))
								{
									maid.body0.MuneYureL(0f);
									maid.body0.MuneYureR(0f);
								}
								else
								{
									maid.body0.MuneYureL(1f);
									maid.body0.MuneYureR(1f);
								}
								int num6;
								if (array[0].Contains("AdvancedStudioModePose"))
								{
									string path = array[0].Split(new char[]
									{
										'/'
									})[1];
									byte[] array2 = new byte[0];
									try
									{
										using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
										{
											array2 = new byte[fileStream.Length];
											fileStream.Read(array2, 0, array2.Length);
										}
									}
									catch
									{
									}
									if (0 < array2.Length)
									{
										string text = Path.GetFileName(path);
										long num5 = (long)text.GetHashCode();
										maid.body0.CrossFade(num5.ToString(), array2, false, false, false, 0f, 1f);
										Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
										{
											default(Maid.AutoTwist),
											Maid.AutoTwist.ShoulderR,
											Maid.AutoTwist.WristL,
											Maid.AutoTwist.WristR,
											Maid.AutoTwist.ThighL,
											Maid.AutoTwist.ThighR
										};
										for (int m = 0; m < array3.Length; m++)
										{
											maid.SetAutoTwist(array3[m], true);
										}
									}
								}
								else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num6))
								{
									loadPose[i] = array[0];
								}
								else if (!array[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array[0] + ".anm", GameUty.FileSystem, array[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array[0] + ".anm");
								}
								if (array.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array[0] + ".anm"].time = float.Parse(array[1]);
									isStop[i] = true;
									if (array.Length > 2)
									{
										Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
							}
						}
					}
				}
				if (isDanceChu)
				{
					int j = 0;
					while (j < maidCnt)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							string text3 = danceName[j];
							float num = 0f;
							if (text3 == "dance_cmo_002_sd_f1.anm" || text3 == "dance_cmo_002_sd_f2.anm" || text3 == "dance_cm3d_004_kano_f1.anm")
							{
								num = 0.166666f;
							}
							if (text3 == "dance_cm3d21_kara_001_nmf_f1.anm")
							{
								num = -0.35f;
							}
							if (text3 != null)
							{
								if (danceCount > 0)
								{
									danceCount--;
									maidArray[j].body0.m_Bones.GetComponent<Animation>()[text3].time = audioSourceBgm.time - 0.03333f - num;
								}
								if (maidArray[j].body0.m_Bones.GetComponent<Animation>()[text3].time + num < audioSourceBgm.time - 0.1f)
								{
									danceCount = 20;
								}
							}
						}
						//IL_1C012:
						j++;
						continue;
						//goto IL_1C012;
					}
				}
				if (isDanceStart1)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								if (j == 0)
								{
									maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_001_f1.anm");
									maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_001_f1.anm"].time = 0f;
									danceName[j] = "dance_cm3d2_001_f1.anm";
								}
								else if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
								{
									maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_001_f2.anm");
									maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_001_f2.anm"].time = 0f;
									danceName[j] = "dance_cm3d2_001_f2.anm";
								}
								else
								{
									maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_001_f3.anm");
									maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_001_f3.anm"].time = 0f;
									danceName[j] = "dance_cm3d2_001_f3.anm";
								}
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara_003_ddfl_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara_003_ddfl_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara_003_ddfl_f1.anm";
							}
						}
					}
					isDanceStart1 = false;
					isDanceStart1F = true;
					isShift = false;
				}
				if (isDanceStart2)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d_001_f1.anm");
							maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d_001_f1.anm"].time = 0f;
							danceName[j] = "dance_cm3d_001_f1.anm";
						}
					}
					isDanceStart2 = false;
					isDanceStart2F = true;
				}
				if (isDanceStart3)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d_002_end_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d_002_end_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d_002_end_f1.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara_001_sl_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara_001_sl_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara_001_sl_f1.anm";
							}
						}
					}
					isDanceStart3 = false;
					isDanceStart3F = true;
					isShift = false;
				}
				if (isDanceStart4)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_002_smt_f.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_002_smt_f.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_002_smt_f.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara02_001_smt_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara02_001_smt_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara02_001_smt_f1.anm";
							}
						}
					}
					isDanceStart4 = false;
					isDanceStart4F = true;
					isShift = false;
				}
				if (isDanceStart5)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d_003_sp2_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d_003_sp2_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d_003_sp2_f1.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara02_002_rty_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara02_002_rty_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara02_002_rty_f1.anm";
							}
						}
					}
					isDanceStart5 = false;
					isDanceStart5F = true;
					isShift = false;
				}
				if (isDanceStart6)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f1.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara02_003_hs_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara02_003_hs_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara02_003_hs_f1.anm";
							}
						}
					}
					isDanceStart6 = false;
					isDanceStart6F = true;
					isShift = false;
				}
				if (isDanceStart7)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f2.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f2.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f2.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f1.anm";
							}
						}
					}
					isDanceStart7 = false;
					isDanceStart7V = true;
					isDanceStart7F = true;
				}
				if (isDanceStart8)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (j == 0 || j == 4 || j == 8 || j == 12)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f1.anm";
							}
							else if (j == 1 || j == 5 || j == 9 || j == 13)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f2.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f2.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f2.anm";
							}
							else if (j == 2 || j == 6 || j == 10)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f3.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f3.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f3.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_003_hs_f4.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_003_hs_f4.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_003_hs_f4.anm";
							}
						}
					}
					isDanceStart8 = false;
					isDanceStart8V = true;
					isDanceStart8F = true;
				}
				if (isDanceStart9)
				{
					isDanceStart9Count++;
					if (isShift)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_kara_002_cktc_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_kara_002_cktc_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_kara_002_cktc_f1.anm";
							}
						}
						isDanceStart9 = false;
						isDanceStart9F = true;
						isDanceStart9Count = 0;
						isShift = false;
					}
					if (isDanceStart9Count == 10)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d_004_kano_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d_004_kano_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d_004_kano_f1.anm";
							}
						}
						isDanceStart9 = false;
						isDanceStart9F = true;
						isDanceStart9Count = 0;
					}
				}
				if (isDanceStart10)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_004_sse_f1.anm");
							maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_004_sse_f1.anm"].time = 0f;
							danceName[j] = "dance_cm3d2_004_sse_f1.anm";
						}
					}
					isDanceStart10 = false;
					isDanceStart10F = true;
				}
				if (isDanceStart11)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (j == 1 || j == 4 || j == 7 || j == 10 || j == 13)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play(nameA + danceNo2 + ".anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()[nameA + danceNo2 + ".anm"].time = 0f;
								danceName[j] = nameA + danceNo2 + ".anm";
							}
							else if (j == 2 || j == 5 || j == 8 || j == 11)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play(nameA + danceNo3 + ".anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()[nameA + danceNo3 + ".anm"].time = 0f;
								danceName[j] = nameA + danceNo3 + ".anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play(nameA + danceNo1 + ".anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()[nameA + danceNo1 + ".anm"].time = 0f;
								danceName[j] = nameA + danceNo1 + ".anm";
							}
						}
					}
					isDanceStart11 = false;
					isDanceStart11V = true;
					isDanceStart11F = true;
				}
				if (isDanceStart12)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_005_khg_f.anm");
							maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_005_khg_f.anm"].time = 0f;
							danceName[j] = "dance_cm3d2_005_khg_f.anm";
						}
					}
					isDanceStart12 = false;
					isDanceStart12F = true;
				}
				if (isDanceStart13)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (!isShift)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_006_ssn_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_006_ssn_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_006_ssn_f1.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d21_kara_001_nmf_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d21_kara_001_nmf_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d21_kara_001_nmf_f1.anm";
							}
						}
					}
					isDanceStart13 = false;
					isDanceStart13F = true;
					isShift = false;
				}
				if (isDanceStart14)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (j == 1 || j == 4 || j == 7 || j == 10 || j == 13)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_006_ssn_f2.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_006_ssn_f2.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_006_ssn_f2.anm";
							}
							else if (j == 2 || j == 5 || j == 8 || j == 11)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_006_ssn_f3.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_006_ssn_f3.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_006_ssn_f3.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cm3d2_006_ssn_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cm3d2_006_ssn_f1.anm"].time = 0f;
								danceName[j] = "dance_cm3d2_006_ssn_f1.anm";
							}
						}
					}
					isDanceStart14 = false;
					isDanceStart14V = true;
					isDanceStart14F = true;
				}
				if (isDanceStart15)
				{
					isDanceStart15Count++;
				}
				if (isDanceStart15Count == 10)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							maidArray[j].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cmo_002_sd_f2.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cmo_002_sd_f2.anm"].time = 0f;
								danceName[j] = "dance_cmo_002_sd_f2.anm";
							}
							else
							{
								maidArray[j].body0.m_Bones.GetComponent<Animation>().Play("dance_cmo_002_sd_f1.anm");
								maidArray[j].body0.m_Bones.GetComponent<Animation>()["dance_cmo_002_sd_f1.anm"].time = 0f;
								danceName[j] = "dance_cmo_002_sd_f1.anm";
							}
						}
					}
					isDanceStart15 = false;
					isDanceStart15V = true;
					isDanceStart15F = true;
					isDanceStart15Count = 0;
				}
				if (isDance)
				{
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j].Visible)
						{
							if (isDanceStart1F)
							{
								if (isDanceStart1K)
								{
									foreach (string text4 in dance1KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 0)
								{
									foreach (string text4 in dance1Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
								{
									foreach (string text4 in dance1BArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance1CArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart2F)
							{
								foreach (string text4 in dance2Array)
								{
									string[] array = text4.Split(new char[]
									{
										','
									});
									float num14 = float.Parse(array[0]);
									if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
									{
										danceFace[j] = num14;
										FaceName[j] = FaceName2[j];
										FaceName2[j] = array[1];
										FaceName3[j] = array[2];
										FaceTime[j] = 0.0166666675f;
										break;
									}
								}
							}
							if (isDanceStart3F)
							{
								if (isDanceStart3K)
								{
									foreach (string text4 in dance3KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance3Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart4F)
							{
								if (isDanceStart4K)
								{
									foreach (string text4 in dance4KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance4Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart5F)
							{
								if (isDanceStart5K)
								{
									foreach (string text4 in dance5KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance5Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart6F)
							{
								if (isDanceStart6K)
								{
									foreach (string text4 in dance6KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance6Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart7F)
							{
								if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
								{
									foreach (string text4 in dance6BArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance6Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart7V && (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13))
							{
								if (audioSourceBgm.time > 42f && audioSourceBgm.time < 43f)
								{
									maidArray[j].SetPos(maidArray[j - 1].transform.position);
								}
								if (audioSourceBgm.time > 58.17f && audioSourceBgm.time < 60f)
								{
									maidArray[j].SetPos(new Vector3(maidArray[j - 1].transform.position.x, 100f, maidArray[j - 1].transform.position.z));
								}
							}
							if (isDanceStart8F)
							{
								if (j == 1 || j == 5 || j == 9 || j == 13)
								{
									foreach (string text4 in dance6BArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 2 || j == 6 || j == 10)
								{
									foreach (string text4 in dance6CArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 3 || j == 9 || j == 11)
								{
									foreach (string text4 in dance6DArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance6Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart8V && (j == 1 || j == 5 || j == 9 || j == 13))
							{
								if (audioSourceBgm.time > 42f && audioSourceBgm.time < 43f)
								{
									maidArray[j].SetPos(maidArray[j - 1].transform.position);
								}
								if (audioSourceBgm.time > 58.17f && audioSourceBgm.time < 60f)
								{
									maidArray[j].SetPos(new Vector3(maidArray[j - 1].transform.position.x, 100f, maidArray[j - 1].transform.position.z));
								}
							}
							if (isDanceStart8V)
							{
								if (!isDanceStart8P && audioSourceBgm.time > 40f && audioSourceBgm.time < 41f)
								{
									isDanceStart8P = true;
									for (int num15 = 0; num15 < maidCnt; num15++)
									{
										if (maidArray[num15].Visible)
										{
											if (num15 == 0 || num15 == 4 || num15 == 8 || num15 == 12)
											{
												Object original = Resources.Load("Prefab/Particle/pHeart01");
												GameObject gameObject6 = Object.Instantiate(original) as GameObject;
												gameObject6.transform.position = CMT.SearchObjName(maidArray[num15].body0.m_Bones.transform, "Bip01 Spine", true).position;
											}
										}
									}
								}
							}
							if (isDanceStart9F)
							{
								if (isDanceStart9K)
								{
									foreach (string text4 in dance9KArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance7Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart10F)
							{
								foreach (string text4 in danceO1Array)
								{
									string[] array = text4.Split(new char[]
									{
										','
									});
									float num14 = float.Parse(array[0]);
									if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
									{
										danceFace[j] = num14;
										FaceName[j] = FaceName2[j];
										FaceName2[j] = array[1];
										FaceName3[j] = array[2];
										FaceTime[j] = 0.0166666675f;
										break;
									}
								}
							}
							if (isDanceStart11F)
							{
								string[] array15 = null;
								string[] array13 = null;
								string[] array16 = null;
								if (isSS1)
								{
									array15 = danceO1Array;
									array13 = danceO1CArray;
									array16 = danceO1BArray;
								}
								if (isSS2)
								{
									array15 = danceO2Array;
									array13 = danceO2BArray;
									array16 = danceO2CArray;
								}
								if (isSS3)
								{
									array15 = danceO3CArray;
									array13 = danceO3Array;
									array16 = danceO3BArray;
								}
								if (isSS4)
								{
									array15 = danceO5BArray;
									array13 = danceO5Array;
									array16 = danceO5CArray;
								}
								if (isSS5)
								{
									array15 = danceO4Array;
									array13 = danceO4BArray;
									array16 = danceO4CArray;
								}
								if (j == 1 || j == 4 || j == 7 || j == 10 || j == 13)
								{
									foreach (string text4 in array13)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]) - 0.03f;
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 2 || j == 5 || j == 8 || j == 11)
								{
									foreach (string text4 in array16)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]) - 0.03f;
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in array15)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]) - 0.03f;
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart12F)
							{
								foreach (string text4 in dance12Array)
								{
									string[] array = text4.Split(new char[]
									{
										','
									});
									float num14 = float.Parse(array[0]);
									if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
									{
										danceFace[j] = num14;
										FaceName[j] = FaceName2[j];
										FaceName2[j] = array[1];
										FaceName3[j] = array[2];
										FaceTime[j] = 0.0166666675f;
										break;
									}
								}
							}
							if (isDanceStart13F)
							{
								if (!isDanceStart13K)
								{
									foreach (string text4 in dance13Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart14F)
							{
								if (j == 1 || j == 4 || j == 7 || j == 10 || j == 13)
								{
									foreach (string text4 in dance13BArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else if (j == 2 || j == 5 || j == 8 || j == 11)
								{
									foreach (string text4 in dance13CArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance13Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (isDanceStart15F)
							{
								if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11 || j == 13)
								{
									foreach (string text4 in dance15BArray)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
								else
								{
									foreach (string text4 in dance15Array)
									{
										string[] array = text4.Split(new char[]
										{
											','
										});
										float num14 = float.Parse(array[0]);
										if (danceFace[j] < num14 && num14 < audioSourceBgm.time)
										{
											danceFace[j] = num14;
											FaceName[j] = FaceName2[j];
											FaceName2[j] = array[1];
											FaceName3[j] = array[2];
											FaceTime[j] = 0.0166666675f;
											break;
										}
									}
								}
							}
							if (!isFadeOut)
							{
								Maid maid = maidArray[j];
								if (maid && maid.Visible)
								{
									if (maid.transform.position.y != 100f)
									{
										FaceTime[j] += Time.deltaTime;
										if (FaceTime[j] < 1f)
										{
											TMorph morph = maid.body0.Face.morph;
											maid.boMabataki = false;
											maid.body0.Face.morph.EyeMabataki = 0f;
											if (FaceName[j] != string.Empty)
											{
												maid.body0.Face.morph.MulBlendValues(FaceName[j], 1f);
											}
											if (FaceName2[j] != "")
											{
												maid.body0.Face.morph.MulBlendValues(FaceName2[j], UTY.COSS2(Mathf.Pow(FaceTime[j], 0.4f), 4f));
											}
											if (FaceName3[j] != string.Empty)
											{
												maid.body0.Face.morph.AddBlendValues(FaceName3[j], 1f);
											}
											maid.body0.Face.morph.FixBlendValues_Face();
										}
										else
										{
											FaceName[j] = FaceName2[j];
										}
										if (isHenkou)
										{
											TMorph morph = maid.body0.Face.morph;
											float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
											maid.boMabataki = false;
											if (isNamidaH)
											{
												fieldValue[(int)morph.hash["namida"]] = 1f;
											}
											else
											{
												fieldValue[(int)morph.hash["namida"]] = 0f;
											}
											if (isSekimenH)
											{
												fieldValue[(int)morph.hash["hohol"]] = 1f;
											}
											else
											{
												fieldValue[(int)morph.hash["hohol"]] = 0f;
											}
											if (isHohoH)
											{
												fieldValue[(int)morph.hash["hoho2"]] = 1f;
											}
											else
											{
												fieldValue[(int)morph.hash["hoho2"]] = 0f;
											}
											maid.body0.Face.morph.FixBlendValues_Face();
										}
									}
								}
							}
							FoceKuchipakuUpdate2(audioSourceBgm.time, maidArray[j], j);
						}
					}
					if (Input.GetKey(KeyCode.H) || isVR)
					{
						if (Input.GetKeyDown(KeyCode.KeypadPeriod))
						{
							h2Flg = true;
							isNamidaH = !isNamidaH;
							isHenkou = true;
						}
						if (Input.GetKeyDown(KeyCode.KeypadPlus))
						{
							h2Flg = true;
							isSekimenH = !isSekimenH;
							isHenkou = true;
						}
						if (Input.GetKeyDown(KeyCode.KeypadEnter))
						{
							h2Flg = true;
							isHohoH = !isHohoH;
							isHenkou = true;
						}
					}
					danceCheckIndex++;
					if (danceCheckIndex == 10)
					{
						danceCheckIndex = 0;
					}
					danceCheck[danceCheckIndex] = audioSourceBgm.time;
					isDanceChu = false;
					for (int k = 0; k < danceCheck.Length; k++)
					{
						if (danceCheck[k] > 0f)
						{
							isDanceChu = true;
							break;
						}
					}
					if (!isDanceChu)
					{
						danceWait--;
						if (danceWait > 0)
						{
							isDanceChu = true;
						}
					}
					if (!isDanceChu)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								maidArray[j].StopKuchipakuPattern();
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
								if (isDanceStart8V && (j == 1 || j == 5 || j == 9 || j == 13))
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
									isStop[i] = true;
									Transform transform2 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[i] = true;
									poseIti[i] = maidArray[i].transform.position;
									maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
								}
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart1F = false;
						isDanceStart1K = false;
						isDanceStart2F = false;
						isDanceStart3F = false;
						isDanceStart3K = false;
						isDanceStart4F = false;
						isDanceStart4K = false;
						isDanceStart5F = false;
						isDanceStart5K = false;
						isDanceStart6F = false;
						isDanceStart6K = false;
						isDanceStart7F = false;
						isDanceStart7V = false;
						isDanceStart8F = false;
						isDanceStart8V = false;
						isDanceStart8P = false;
						isDanceStart9F = false;
						isDanceStart9K = false;
						isDanceStart10F = false;
						isDanceStart11F = false;
						isDanceStart11V = false;
						isDanceStart12F = false;
						isDanceStart13F = false;
						isDanceStart13K = false;
						isDanceStart14F = false;
						isDanceStart14V = false;
						isDanceStart15F = false;
						isDanceStart15V = false;
						for (int j = 0; j < maidCnt; j++)
						{
							danceFace[j] = 0f;
						}
						danceCheckIndex = 0;
						for (int k = 0; k < danceCheck.Length; k++)
						{
							danceCheck[danceCheckIndex] = 1f;
						}
						isDance = false;
					}
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Keypad1)))
				{
					if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						isShift = true;
					}
					TextAsset textAsset = Resources.Load("SceneDance/dance_kp_m0") as TextAsset;
					string text5 = Regex.Replace(textAsset.text, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					if (!isShift)
					{
						GameMain.Instance.CharacterMgr.ResetCharaPosAll();
					}
					for (int j = 0; j < maidCnt; j++)
					{
						string text6 = "dance_cm3d2_001_f1.anm";
						switch (j)
						{
							case 1:
							case 3:
							case 5:
							case 7:
							case 9:
							case 11:
							case 13:
								text6 = "dance_cm3d2_001_f2.anm";
								break;
							case 2:
							case 4:
							case 6:
							case 8:
							case 10:
							case 12:
								text6 = "dance_cm3d2_001_f3.anm";
								break;
						}
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara_003_ddfl_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara_003_ddfl_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara_003_ddfl_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/DDF_kara_voice_2") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("dokidokifallinlove_short.ogg", 0f, false);
						sw = new Stopwatch();
						sw.Start();
					}
					else if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("dokidokifallinlove_short_sasaki_kara.ogg", 0f, false);
					}
					else
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("dokidokifallinlove_short_nao_kara.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						Vector3 vector = Vector3.zero;
						switch (j)
						{
							case 3:
								vector.x = 1f;
								break;
							case 4:
								vector.x = -1f;
								break;
							case 5:
								vector.x = 2f;
								break;
							case 6:
								vector.x = -2f;
								break;
							case 7:
								vector.x = 3f;
								break;
							case 8:
								vector.x = -3f;
								break;
							case 9:
								vector.x = 4f;
								break;
							case 10:
								vector.x = -4f;
								break;
							case 11:
								vector.x = 5f;
								break;
							case 12:
								vector.x = -5f;
								break;
							case 13:
								vector.x = 6f;
								break;
						}
						if (!isShift)
						{
							maidArray[j].SetPos(vector);
						}
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (isShift)
					{
						if (maidArray[1] && maidArray[1].Visible)
						{
							if (maidArray[0].transform.position == maidArray[1].transform.position)
							{
								Vector3 vector = Vector3.zero;
								vector.x = 0.6f;
								maidArray[1].SetPos(vector);
								if (maidArray[2] && maidArray[2].Visible)
								{
									vector.x = -0.6f;
									maidArray[2].SetPos(vector);
								}
								if (maidArray[3] && maidArray[3].Visible)
								{
									vector.x = 1.2f;
									maidArray[3].SetPos(vector);
								}
								if (maidArray[4] && maidArray[4].Visible)
								{
									vector.x = -1.2f;
									maidArray[4].SetPos(vector);
								}
								if (maidArray[5] && maidArray[5].Visible)
								{
									vector.x = 1.8f;
									maidArray[5].SetPos(vector);
								}
								if (maidArray[6] && maidArray[6].Visible)
								{
									vector.x = -1.8f;
									maidArray[6].SetPos(vector);
								}
								if (maidArray[7] && maidArray[7].Visible)
								{
									vector.x = 2.4f;
									maidArray[7].SetPos(vector);
								}
								if (maidArray[8] && maidArray[8].Visible)
								{
									vector.x = -2.4f;
									maidArray[8].SetPos(vector);
								}
								if (maidArray[9] && maidArray[9].Visible)
								{
									vector.x = 3f;
									maidArray[9].SetPos(vector);
								}
								if (maidArray[10] && maidArray[10].Visible)
								{
									vector.x = -3f;
									maidArray[10].SetPos(vector);
								}
								if (maidArray[11] && maidArray[11].Visible)
								{
									vector.x = 3.6f;
									maidArray[11].SetPos(vector);
								}
								if (maidArray[12] && maidArray[12].Visible)
								{
									vector.x = -3.6f;
									maidArray[12].SetPos(vector);
								}
								if (maidArray[13] && maidArray[13].Visible)
								{
									vector.x = 4.2f;
									maidArray[13].SetPos(vector);
								}
							}
						}
						isDanceStart1K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart1 = true;
				}
				if (isF2)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_EtY_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d_001_f1.anm"))
						{
							maidArray[j].body0.LoadAnime("dance_cm3d_001_f1.anm", GameUty.FileSystem, "dance_cm3d_001_f1.anm", false, false);
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					GameMain.Instance.SoundMgr.PlayDanceBGM("entrancetoyou_short.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					isDance = true;
					isDanceStart2 = true;
					isF2 = false;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Keypad2)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isF2 = true;
				}
				if (isF3)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_Scl_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d_002_end_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d_002_end_f1.anm", GameUty.FileSystem, "dance_cm3d_002_end_f1.anm", false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara_001_sl_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara_001_sl_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara_001_sl_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/scaret_kara_voice_2") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("scarlet leap_short.ogg", 0f, false);
					}
					else
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("scarlet leap_short_kara_1.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart3K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart3 = true;
					isF3 = false;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha3)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Keypad3)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isF3 = true;
					if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						isShift = true;
					}
				}
				if (isSF1 || isSF2 || isSF3)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_SmT_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_002_smt_f.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_002_smt_f.anm", GameUty.FileSystem, "dance_cm3d2_002_smt_f.anm", false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara02_001_smt_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara02_001_smt_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara02_001_smt_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/stellar my tears_kara_voice") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						if (isSF1)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short.ogg", 0f, false);
						}
						if (isSF2)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short2.ogg", 0f, false);
						}
						if (isSF3)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short3.ogg", 0f, false);
						}
					}
					else
					{
						if (isSF1)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short_nao_kara.ogg", 0f, false);
						}
						if (isSF2)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short_sasaki_kara.ogg", 0f, false);
						}
						if (isSF3)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("stellarmytears_short_misato_kara.ogg", 0f, false);
						}
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart4K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart4 = true;
					isSF1 = false;
					isSF2 = false;
					isSF3 = false;
				}
				if (isHS1 || isHS2 || isHS3)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_Hhs_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_003_hs_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_003_hs_f1.anm", GameUty.FileSystem, "dance_cm3d2_003_hs_f1.anm", false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara02_003_hs_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara02_003_hs_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara02_003_hs_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/happy_happy_scandal_kara_voice") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						if (isHS1)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal1.ogg", 0f, false);
						}
						if (isHS2)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal2.ogg", 0f, false);
						}
						if (isHS3)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal3.ogg", 0f, false);
						}
					}
					else
					{
						if (isHS1)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happyhappyscandal_short_nao_kara.ogg", 0f, false);
						}
						if (isHS2)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal_sasaki_kara.ogg", 0f, false);
						}
						if (isHS3)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal_misato_kara.ogg", 0f, false);
						}
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart6K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart6 = true;
					isHS1 = false;
					isHS2 = false;
					isHS3 = false;
				}
				if (isHS4 || isHS5 || isHS6)
				{
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						TextAsset textAsset = Resources.Load("SceneDance/dance_Hhs_m0") as TextAsset;
						string text6 = "dance_cm3d2_003_hs_f1.anm";
						switch (j)
						{
							case 1:
							case 3:
							case 5:
							case 7:
							case 9:
							case 11:
							case 13:
								text6 = "dance_cm3d2_003_hs_f2.anm";
								textAsset = (Resources.Load("SceneDance/dance_Hhs_m1") as TextAsset);
								break;
						}
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
							string text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							maidArray[j].StartKuchipakuPattern(0f, text5, true);
							m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
						}
					}
					if (isHS4)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal1.ogg", 0f, false);
					}
					if (isHS5)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal2.ogg", 0f, false);
					}
					if (isHS6)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal3.ogg", 0f, false);
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
						if (maidArray[j].transform.position.y != 100f)
						{
							dancePos[j] = maidArray[j].transform.position;
						}
						else
						{
							dancePos[j] = new Vector3(maidArray[j].transform.position.x, 0f, maidArray[j].transform.position.z);
						}
						danceRot[j] = maidArray[j].transform.localRotation;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						maidArray[1].SetPos(new Vector3(maidArray[0].transform.position.x, 100f, maidArray[0].transform.position.z));
						maidArray[1].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[3] && maidArray[3].Visible)
					{
						maidArray[3].SetPos(new Vector3(maidArray[2].transform.position.x, 100f, maidArray[2].transform.position.z));
						maidArray[3].body0.transform.localRotation = maidArray[2].body0.transform.localRotation;
					}
					if (maidArray[5] && maidArray[5].Visible)
					{
						maidArray[5].SetPos(new Vector3(maidArray[4].transform.position.x, 100f, maidArray[4].transform.position.z));
						maidArray[5].body0.transform.localRotation = maidArray[4].body0.transform.localRotation;
					}
					if (maidArray[7] && maidArray[7].Visible)
					{
						maidArray[7].SetPos(new Vector3(maidArray[6].transform.position.x, 100f, maidArray[6].transform.position.z));
						maidArray[7].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[9] && maidArray[9].Visible)
					{
						maidArray[9].SetPos(new Vector3(maidArray[8].transform.position.x, 100f, maidArray[8].transform.position.z));
						maidArray[9].body0.transform.localRotation = maidArray[8].body0.transform.localRotation;
					}
					if (maidArray[11] && maidArray[11].Visible)
					{
						maidArray[11].SetPos(new Vector3(maidArray[10].transform.position.x, 100f, maidArray[10].transform.position.z));
						maidArray[11].body0.transform.localRotation = maidArray[10].body0.transform.localRotation;
					}
					if (maidArray[13] && maidArray[13].Visible)
					{
						maidArray[13].SetPos(new Vector3(maidArray[12].transform.position.x, 100f, maidArray[12].transform.position.z));
						maidArray[13].body0.transform.localRotation = maidArray[12].body0.transform.localRotation;
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						if (maidArray[0].transform.position == maidArray[2].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					isDance = true;
					isDanceStart7 = true;
					isHS4 = false;
					isHS5 = false;
					isHS6 = false;
				}
				if (isHS7 || isHS8 || isHS9)
				{
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						string text6 = "dance_cm3d2_003_hs_f1.anm";
						TextAsset textAsset = Resources.Load("SceneDance/dance_Hhs_m0") as TextAsset;
						switch (j)
						{
							case 1:
							case 5:
							case 9:
							case 13:
								text6 = "dance_cm3d2_003_hs_f2.anm";
								textAsset = (Resources.Load("SceneDance/dance_Hhs_m1") as TextAsset);
								break;
							case 2:
							case 6:
							case 10:
								text6 = "dance_cm3d2_003_hs_f3.anm";
								textAsset = (Resources.Load("SceneDance/dance_Hhs_m2") as TextAsset);
								break;
							case 3:
							case 7:
							case 11:
								text6 = "dance_cm3d2_003_hs_f4.anm";
								textAsset = (Resources.Load("SceneDance/dance_Hhs_m2") as TextAsset);
								break;
						}
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
						}
						string text5 = textAsset.text;
						text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (isHS7)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal1.ogg", 0f, false);
					}
					if (isHS8)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal2.ogg", 0f, false);
					}
					if (isHS9)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("happy_happy_scandal3.ogg", 0f, false);
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
						if (maidArray[j].transform.position.y != 100f)
						{
							dancePos[j] = maidArray[j].transform.position;
						}
						else
						{
							dancePos[j] = new Vector3(maidArray[j].transform.position.x, 0f, maidArray[j].transform.position.z);
						}
						danceRot[j] = maidArray[j].transform.localRotation;
					}
					if ((maidArray[0].transform.position.x == 0.3f || maidArray[0].transform.position.x == -0.3f) && maidArray[0].transform.position.y == 0f && maidArray[0].transform.position.z == 0f)
					{
						maidArray[0].SetPos(Vector3.zero);
						maidArray[0].SetRot(Vector3.zero);
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						maidArray[1].SetPos(new Vector3(maidArray[0].transform.position.x, 100f, maidArray[0].transform.position.z));
						maidArray[1].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						maidArray[2].SetPos(maidArray[0].transform.position);
						maidArray[2].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[3] && maidArray[3].Visible)
					{
						maidArray[3].SetPos(maidArray[0].transform.position);
						maidArray[3].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[5] && maidArray[5].Visible)
					{
						maidArray[5].SetPos(new Vector3(maidArray[4].transform.position.x, 100f, maidArray[4].transform.position.z));
						maidArray[5].body0.transform.localRotation = maidArray[4].body0.transform.localRotation;
					}
					if (maidArray[6] && maidArray[6].Visible)
					{
						maidArray[6].SetPos(maidArray[4].transform.position);
						maidArray[6].body0.transform.localRotation = maidArray[4].body0.transform.localRotation;
					}
					if (maidArray[7] && maidArray[7].Visible)
					{
						maidArray[7].SetPos(maidArray[4].transform.position);
						maidArray[7].body0.transform.localRotation = maidArray[4].body0.transform.localRotation;
					}
					if (maidArray[9] && maidArray[9].Visible)
					{
						maidArray[9].SetPos(new Vector3(maidArray[8].transform.position.x, 100f, maidArray[8].transform.position.z));
						maidArray[9].body0.transform.localRotation = maidArray[8].body0.transform.localRotation;
					}
					if (maidArray[10] && maidArray[10].Visible)
					{
						maidArray[10].SetPos(maidArray[8].transform.position);
						maidArray[10].body0.transform.localRotation = maidArray[8].body0.transform.localRotation;
					}
					if (maidArray[11] && maidArray[11].Visible)
					{
						maidArray[11].SetPos(maidArray[8].transform.position);
						maidArray[11].body0.transform.localRotation = maidArray[8].body0.transform.localRotation;
					}
					if (maidArray[13] && maidArray[13].Visible)
					{
						maidArray[13].SetPos(new Vector3(maidArray[12].transform.position.x, 100f, maidArray[12].transform.position.z));
						maidArray[13].body0.transform.localRotation = maidArray[12].body0.transform.localRotation;
					}
					if (maidArray[4] && maidArray[4].Visible)
					{
						if (maidArray[0].transform.position == maidArray[4].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					isDance = true;
					isDanceStart8 = true;
					isHS7 = false;
					isHS8 = false;
					isHS9 = false;
				}
				if (isCF1)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_RtY_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d_003_sp2_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d_003_sp2_f1.anm", GameUty.FileSystem, "dance_cm3d_003_sp2_f1.anm", false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara02_002_rty_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara02_002_rty_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara02_002_rty_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/rhythmix to you_kara_voice") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("RhythmixToYou.ogg", 0f, false);
					}
					else
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("rhythmixtoyou_kara.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart5K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart5 = true;
					isCF1 = false;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha4)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Keypad4)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isCF1 = true;
					if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						isShift = true;
					}
				}
				if (isKT1)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_cktc_1_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d_004_kano_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d_004_kano_f1.anm", GameUty.FileSystem, "dance_cm3d_004_kano_f1.anm", false, false);
							}
							danceName[j] = "dance_cm3d_004_kano_f1.anm";
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_kara_002_cktc_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_kara_002_cktc_f1.anm", GameUty.FileSystem, "dance_cm3d2_kara_002_cktc_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/cktc_kara_voice_2") as TextAsset);
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("can_know_two_close.ogg", 0f, false);
					}
					else
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("canknowtwoclose_short_kara.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						if (maidArray[0].transform.position == maidArray[2].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart9K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart9 = true;
					isKT1 = false;
				}
				if (isSS && (isSS1 || isSS2 || isSS3 || isSS4 || isSS5 || isSS6))
				{
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					if (isSS1)
					{
						nameK = "dance_BlD_m";
						nameA = "dance_cm3d21_002_bid_f";
						nameS = "bloomingdreaming_short";
						danceNo1 = 1;
						danceNo2 = 3;
						danceNo3 = 2;
					}
					if (isSS2)
					{
						nameK = "dance_KAD_m";
						nameA = "dance_cm3d21_003_kad_f";
						nameS = "kiminiaijodelicious_short";
						danceNo1 = 1;
						danceNo2 = 2;
						danceNo3 = 3;
					}
					if (isSS3)
					{
						nameK = "dance_LUM_m";
						nameA = "dance_cm3d21_004_lm_f";
						nameS = "luminousmoment_short";
						danceNo1 = 3;
						danceNo2 = 1;
						danceNo3 = 2;
					}
					if (isSS4)
					{
						nameK = "dance_NmF_m";
						nameA = "dance_cm3d21_001_nmf_f";
						nameS = "nightmagicfire_short";
						danceNo1 = 2;
						danceNo2 = 1;
						danceNo3 = 3;
					}
					if (isSS5)
					{
						nameK = "dance_MoE_m";
						nameA = "dance_cm3d21_005_moe_f";
						nameS = "melodyofempire_short";
						danceNo1 = 1;
						danceNo2 = 3;
						danceNo3 = 2;
					}
					if (isSS6)
					{
						nameK = "dance_NmF_m";
						nameA = "dance_cm3d21_kara_001_nmf_f";
						nameS = "nightmagicfire_short";
						danceNo1 = 1;
						danceNo2 = 1;
						danceNo3 = 1;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						TextAsset textAsset = Resources.Load("SceneDance/" + nameK + (danceNo1 - 1)) as TextAsset;
						string text6 = nameA + danceNo1 + ".anm";
						switch (j)
						{
							case 1:
							case 4:
							case 7:
							case 10:
							case 13:
								text6 = nameA + danceNo2 + ".anm";
								textAsset = (Resources.Load("SceneDance/" + nameK + (danceNo2 - 1)) as TextAsset);
								break;
							case 2:
							case 5:
							case 8:
							case 11:
								text6 = nameA + danceNo3 + ".anm";
								textAsset = (Resources.Load("SceneDance/" + nameK + (danceNo3 - 1)) as TextAsset);
								break;
						}
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
							string text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							maidArray[j].StartKuchipakuPattern(0f, text5, true);
							m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
						}
					}
					GameMain.Instance.SoundMgr.PlayDanceBGM(nameS + ".ogg", 0f, false);
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
						if (maidArray[j].transform.position.y != 100f)
						{
							dancePos[j] = maidArray[j].transform.position;
						}
						else
						{
							dancePos[j] = new Vector3(maidArray[j].transform.position.x, 0f, maidArray[j].transform.position.z);
						}
						danceRot[j] = maidArray[j].transform.localRotation;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						maidArray[1].SetPos(new Vector3(maidArray[0].transform.position.x, maidArray[0].transform.position.y, maidArray[0].transform.position.z));
						maidArray[1].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						maidArray[2].SetPos(new Vector3(maidArray[0].transform.position.x, maidArray[0].transform.position.y, maidArray[0].transform.position.z));
						maidArray[2].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[4] && maidArray[4].Visible)
					{
						maidArray[4].SetPos(new Vector3(maidArray[3].transform.position.x, maidArray[3].transform.position.y, maidArray[3].transform.position.z));
						maidArray[4].body0.transform.localRotation = maidArray[3].body0.transform.localRotation;
					}
					if (maidArray[5] && maidArray[5].Visible)
					{
						maidArray[5].SetPos(new Vector3(maidArray[3].transform.position.x, maidArray[3].transform.position.y, maidArray[3].transform.position.z));
						maidArray[5].body0.transform.localRotation = maidArray[3].body0.transform.localRotation;
					}
					if (maidArray[7] && maidArray[7].Visible)
					{
						maidArray[7].SetPos(new Vector3(maidArray[6].transform.position.x, maidArray[6].transform.position.y, maidArray[6].transform.position.z));
						maidArray[7].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[8] && maidArray[8].Visible)
					{
						maidArray[8].SetPos(new Vector3(maidArray[6].transform.position.x, maidArray[6].transform.position.y, maidArray[6].transform.position.z));
						maidArray[8].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[10] && maidArray[10].Visible)
					{
						maidArray[10].SetPos(new Vector3(maidArray[9].transform.position.x, maidArray[9].transform.position.y, maidArray[9].transform.position.z));
						maidArray[10].body0.transform.localRotation = maidArray[9].body0.transform.localRotation;
					}
					if (maidArray[11] && maidArray[11].Visible)
					{
						maidArray[11].SetPos(new Vector3(maidArray[9].transform.position.x, maidArray[9].transform.position.y, maidArray[9].transform.position.z));
						maidArray[11].body0.transform.localRotation = maidArray[9].body0.transform.localRotation;
					}
					if (maidArray[13] && maidArray[13].Visible)
					{
						maidArray[13].SetPos(new Vector3(maidArray[12].transform.position.x, maidArray[12].transform.position.y, maidArray[12].transform.position.z));
						maidArray[13].body0.transform.localRotation = maidArray[12].body0.transform.localRotation;
					}
					isSS = false;
					isDance = true;
					isDanceStart11 = true;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha5)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha5)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSS1 = true;
					isSS2 = false;
					isSS3 = false;
					isSS4 = false;
					isSS5 = false;
					isSS6 = false;
					isSS = true;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha6)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha6)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSS1 = false;
					isSS2 = true;
					isSS3 = false;
					isSS4 = false;
					isSS5 = false;
					isSS6 = false;
					isSS = true;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha7)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha7)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSS1 = false;
					isSS2 = false;
					isSS3 = true;
					isSS4 = false;
					isSS5 = false;
					isSS6 = false;
					isSS = true;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha8)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha8)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSS1 = false;
					isSS2 = false;
					isSS3 = false;
					isSS4 = true;
					isSS5 = false;
					isSS6 = false;
					isSS = true;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha9)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha9)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSS1 = false;
					isSS2 = false;
					isSS3 = false;
					isSS4 = false;
					isSS5 = true;
					isSS6 = false;
					isSS = true;
				}
				if (isKHG1 || isKHG2)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_KhG_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_005_khg_f.anm"))
						{
							maidArray[j].body0.LoadAnime("dance_cm3d2_005_khg_f.anm", GameUty.FileSystem, "dance_cm3d2_005_khg_f.anm", false, false);
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (isKHG1)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("kaikaku_short1.ogg", 0f, false);
					}
					if (isKHG2)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("kaikaku_short2.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					isDance = true;
					isDanceStart12 = true;
					isKHG1 = false;
					isKHG2 = false;
				}
				if (isDanceStart13Count > 0)
				{
					isDanceStart13Count++;
				}
				if (isDanceStart13Count == 1)
				{
					GameMain.Instance.SoundMgr.PlayDanceBGM("nightmagicfire_short.ogg", 0f, false);
					isDanceStart13Count = 0;
					TextAsset textAsset = Resources.Load("SceneDance/dance_SsN_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
					}
				}
				if ((isSN1 || isSN2 || isSN3) && isShift)
				{
					TextAsset textAsset = Resources.Load("SceneDance/dance_SsN_m0") as TextAsset;
					string text5 = textAsset.text;
					text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						if (!isShift)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d2_006_ssn_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d2_006_ssn_f1.anm", GameUty.FileSystem, "dance_cm3d2_006_ssn_f1.anm", false, false);
							}
						}
						else
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip("dance_cm3d21_kara_001_nmf_f1.anm"))
							{
								maidArray[j].body0.LoadAnime("dance_cm3d21_kara_001_nmf_f1.anm", GameUty.FileSystem, "dance_cm3d21_kara_001_nmf_f1.anm", false, false);
							}
							textAsset = (Resources.Load("SceneDance/dance_NmF_m1") as TextAsset);
							string text6 = nameA + danceNo1 + ".anm";
							switch (j)
							{
								case 1:
								case 4:
								case 7:
								case 10:
								case 13:
									textAsset = (Resources.Load("SceneDance/dance_NmF_m0") as TextAsset);
									break;
								case 2:
								case 5:
								case 8:
								case 11:
									textAsset = (Resources.Load("SceneDance/dance_NmF_m2") as TextAsset);
									break;
							}
							text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							if (!isDanceStart1K && !isDanceStart3K && !isDanceStart4K && !isDanceStart5K && !isDanceStart6K && !isDanceStart9K && !isDanceStart13K)
							{
								dancePos[j] = maidArray[j].transform.position;
								danceRot[j] = maidArray[j].transform.localRotation;
							}
						}
						m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
					}
					if (!isShift)
					{
						if (isSN1)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("nightmagicfire_short.ogg", 0f, false);
						}
						if (isSN2)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("sunshinenatsu2.ogg", 0f, false);
						}
						if (isSN3)
						{
							GameMain.Instance.SoundMgr.PlayDanceBGM("sunshinenatsu3.ogg", 0f, false);
						}
					}
					else
					{
						danceWait = 400;
						isDanceStart13Count++;
						GameMain.Instance.SoundMgr.PlayDanceBGM("nightmagicfire_short.ogg", 0f, false);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						maidArray[j].StartKuchipakuPattern(0f, text5, true);
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						if (maidArray[0].transform.position == maidArray[1].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					if (isShift)
					{
						isDanceStart13K = true;
						for (int j = 0; j < maidCnt; j++)
						{
							maidArray[j].SetRot(new Vector3(maidArray[j].body0.transform.localRotation.x, maidArray[j].body0.transform.localRotation.y + 90f, maidArray[j].body0.transform.localRotation.z));
							maidArray[j].SetPos(new Vector3(maidArray[j].body0.transform.position.x + 1f, maidArray[j].body0.transform.position.y, maidArray[j].body0.transform.position.z + 4f));
							string text3 = "handitem,HanditemL_Karaoke_Mike_I_.menu";
							int i = j;
							Maid maid = maidArray[j];
							string[] array = new string[2];
							array = text3.Split(new char[]
							{
								','
							});
							maid.DelProp(MPN.handitem, true);
							maid.DelProp(MPN.accvag, true);
							maid.DelProp(MPN.accanl, true);
							maid.DelProp(MPN.kousoku_upper, true);
							maid.DelProp(MPN.kousoku_lower, true);
							if (array[0] != "")
							{
								maid.SetProp(array[0], array[1], 0, true, false);
							}
							maid.AllProcPropSeqStart();
						}
					}
					isDance = true;
					isDanceStart13 = true;
					isSN1 = false;
					isSN2 = false;
					isSN3 = false;
				}
				if ((Input.GetKey(KeyCode.Return) && Input.GetKeyDown(KeyCode.Alpha8)) || (Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Alpha8)))
				{
					GameMain.Instance.SoundMgr.StopBGM(0f);
					isSN1 = true;
					hFlg = true;
					if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						isShift = true;
					}
				}
				if (isSN4 || isSN5 || isSN6)
				{
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						TextAsset textAsset = Resources.Load("SceneDance/dance_SsN_m0") as TextAsset;
						string text6 = "dance_cm3d2_006_ssn_f1.anm";
						switch (j)
						{
							case 1:
							case 4:
							case 7:
							case 10:
							case 13:
								text6 = "dance_cm3d2_006_ssn_f2.anm";
								textAsset = (Resources.Load("SceneDance/dance_SsN_m1") as TextAsset);
								break;
							case 2:
							case 5:
							case 8:
							case 11:
								text6 = "dance_cm3d2_006_ssn_f3.anm";
								textAsset = (Resources.Load("SceneDance/dance_SsN_m1") as TextAsset);
								break;
						}
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
							string text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							maidArray[j].StartKuchipakuPattern(0f, text5, true);
							m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
						}
					}
					if (isSN4)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("sunshinenatsu1.ogg", 0f, false);
					}
					if (isSN5)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("sunshinenatsu2.ogg", 0f, false);
					}
					if (isSN6)
					{
						GameMain.Instance.SoundMgr.PlayDanceBGM("sunshinenatsu3.ogg", 0f, false);
					}
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
						if (maidArray[j].transform.position.y != 100f)
						{
							dancePos[j] = maidArray[j].transform.position;
						}
						else
						{
							dancePos[j] = new Vector3(maidArray[j].transform.position.x, 0f, maidArray[j].transform.position.z);
						}
						danceRot[j] = maidArray[j].transform.localRotation;
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						maidArray[1].SetPos(new Vector3(maidArray[0].transform.position.x, maidArray[0].transform.position.y, maidArray[0].transform.position.z));
						maidArray[1].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						maidArray[2].SetPos(new Vector3(maidArray[0].transform.position.x, maidArray[0].transform.position.y, maidArray[0].transform.position.z));
						maidArray[2].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[4] && maidArray[4].Visible)
					{
						maidArray[4].SetPos(new Vector3(maidArray[3].transform.position.x, maidArray[3].transform.position.y, maidArray[3].transform.position.z));
						maidArray[4].body0.transform.localRotation = maidArray[3].body0.transform.localRotation;
					}
					if (maidArray[5] && maidArray[5].Visible)
					{
						maidArray[5].SetPos(new Vector3(maidArray[3].transform.position.x, maidArray[3].transform.position.y, maidArray[3].transform.position.z));
						maidArray[5].body0.transform.localRotation = maidArray[3].body0.transform.localRotation;
					}
					if (maidArray[7] && maidArray[7].Visible)
					{
						maidArray[7].SetPos(new Vector3(maidArray[6].transform.position.x, maidArray[6].transform.position.y, maidArray[6].transform.position.z));
						maidArray[7].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[8] && maidArray[8].Visible)
					{
						maidArray[8].SetPos(new Vector3(maidArray[6].transform.position.x, maidArray[6].transform.position.y, maidArray[6].transform.position.z));
						maidArray[8].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[10] && maidArray[10].Visible)
					{
						maidArray[10].SetPos(new Vector3(maidArray[9].transform.position.x, maidArray[9].transform.position.y, maidArray[9].transform.position.z));
						maidArray[10].body0.transform.localRotation = maidArray[9].body0.transform.localRotation;
					}
					if (maidArray[11] && maidArray[11].Visible)
					{
						maidArray[11].SetPos(new Vector3(maidArray[9].transform.position.x, maidArray[9].transform.position.y, maidArray[9].transform.position.z));
						maidArray[11].body0.transform.localRotation = maidArray[9].body0.transform.localRotation;
					}
					if (maidArray[13] && maidArray[13].Visible)
					{
						maidArray[13].SetPos(new Vector3(maidArray[12].transform.position.x, maidArray[12].transform.position.y, maidArray[12].transform.position.z));
						maidArray[13].body0.transform.localRotation = maidArray[12].body0.transform.localRotation;
					}
					isDance = true;
					isDanceStart14 = true;
					isSN4 = false;
					isSN5 = false;
					isSN6 = false;
				}
				if (isSD1)
				{
					audioSourceBgm = GameMain.Instance.SoundMgr.GetAudioSourceBgm();
					GameMain.Instance.SoundMgr.PlayBGM("se002.ogg", 0f, false);
					for (int j = 0; j < maidCnt; j++)
					{
						TextAsset textAsset = Resources.Load("SceneDance/uta_mmlop_mix_Bok") as TextAsset;
						string text6 = "dance_cmo_002_sd_f1.anm";
						switch (j)
						{
							case 1:
							case 3:
							case 5:
							case 7:
							case 9:
							case 11:
							case 13:
								text6 = "dance_cmo_002_sd_f2.anm";
								textAsset = (Resources.Load("SceneDance/uta_mmlop_mix_Aok") as TextAsset);
								break;
						}
						danceName[j] = text6;
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (!maidArray[j].body0.m_Bones.GetComponent<Animation>().GetClip(text6))
							{
								maidArray[j].body0.LoadAnime(text6, GameUty.FileSystem, text6, false, false);
							}
							string text5 = textAsset.text;
							text5 = Regex.Replace(text5, "(\\r|\\n| )", string.Empty);
							maidArray[j].StartKuchipakuPattern(0f, text5, true);
							m_baKuchipakuPattern[j] = Convert.FromBase64String(text5);
						}
					}
					GameMain.Instance.SoundMgr.PlayDanceBGM("selfishdestiny_due_short_1.ogg", 0f, false);
					ikBui = 5;
					isDanceStart1F = false;
					isDanceStart2F = false;
					isDanceStart3F = false;
					isDanceStart4F = false;
					isDanceStart5F = false;
					isDanceStart6F = false;
					isDanceStart7F = false;
					isDanceStart8F = false;
					isDanceStart9F = false;
					isDanceStart10F = false;
					isDanceStart11F = false;
					isDanceStart12F = false;
					isDanceStart13F = false;
					isDanceStart14F = false;
					isDanceStart15F = false;
					for (int j = 0; j < maidCnt; j++)
					{
						danceFace[j] = 0f;
					}
					if (isDanceStart7V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart7V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart7V = false;
					}
					if (isDanceStart8V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart8V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart8V = false;
						isDanceStart8P = false;
					}
					if (isDanceStart11V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart11V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart11V = false;
					}
					if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart1K || isDanceStart3K || isDanceStart4K || isDanceStart5K || isDanceStart6K || isDanceStart9K || isDanceStart13K)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
									int i = j;
									Maid maid = maidArray[j];
									maid.DelProp(MPN.handitem, true);
									maid.DelProp(MPN.accvag, true);
									maid.DelProp(MPN.accanl, true);
									maid.DelProp(MPN.kousoku_upper, true);
									maid.DelProp(MPN.kousoku_lower, true);
									maid.AllProcPropSeqStart();
								}
							}
						}
						isDanceStart1K = false;
						isDanceStart3K = false;
						isDanceStart4K = false;
						isDanceStart5K = false;
						isDanceStart6K = false;
						isDanceStart9K = false;
						isDanceStart13K = false;
					}
					if (isDanceStart14V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart14V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart14V = false;
					}
					if (isDanceStart15V)
					{
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isDanceStart15V)
								{
									maidArray[j].SetPos(dancePos[j]);
									maidArray[j].body0.transform.localRotation = danceRot[j];
								}
							}
						}
						isDanceStart15V = false;
					}
					for (int j = 0; j < maidCnt; j++)
					{
						isStop[j] = false;
						isLock[j] = false;
						pHandL[j] = 0;
						pHandR[j] = 0;
						muneIKL[j] = false;
						muneIKR[j] = false;
						if (!isVR)
						{
							maidArray[j].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[j]];
							maidArray[j].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[j]];
						}
						if (maidArray[j].transform.position.y != 100f)
						{
							dancePos[j] = maidArray[j].transform.position;
						}
						else
						{
							dancePos[j] = new Vector3(maidArray[j].transform.position.x, 0f, maidArray[j].transform.position.z);
						}
						danceRot[j] = maidArray[j].transform.localRotation;
					}
					if (maidArray[0] && maidArray[0].Visible && maidArray[1] && maidArray[1].Visible)
					{
						maidArray[0].SetPos(new Vector3((maidArray[0].transform.position.x + maidArray[1].transform.position.x) / 2f, (maidArray[0].transform.position.y + maidArray[1].transform.position.y) / 2f, (maidArray[0].transform.position.z + maidArray[1].transform.position.z) / 2f));
						maidArray[0].body0.transform.localRotation = Quaternion.Lerp(maidArray[0].transform.localRotation, maidArray[1].transform.localRotation, 0.5f);
					}
					if (maidArray[2] && maidArray[2].Visible && maidArray[3] && maidArray[3].Visible)
					{
						maidArray[2].SetPos(new Vector3((maidArray[2].transform.position.x + maidArray[3].transform.position.x) / 2f, (maidArray[2].transform.position.y + maidArray[3].transform.position.y) / 2f, (maidArray[2].transform.position.z + maidArray[3].transform.position.z) / 2f));
						maidArray[2].body0.transform.localRotation = Quaternion.Lerp(maidArray[2].transform.localRotation, maidArray[3].transform.localRotation, 0.5f);
					}
					if (maidArray[4] && maidArray[4].Visible && maidArray[5] && maidArray[5].Visible)
					{
						maidArray[4].SetPos(new Vector3((maidArray[4].transform.position.x + maidArray[5].transform.position.x) / 2f, (maidArray[4].transform.position.y + maidArray[5].transform.position.y) / 2f, (maidArray[4].transform.position.z + maidArray[5].transform.position.z) / 2f));
						maidArray[4].body0.transform.localRotation = Quaternion.Lerp(maidArray[4].transform.localRotation, maidArray[5].transform.localRotation, 0.5f);
					}
					if (maidArray[6] && maidArray[6].Visible && maidArray[7] && maidArray[7].Visible)
					{
						maidArray[6].SetPos(new Vector3((maidArray[6].transform.position.x + maidArray[7].transform.position.x) / 2f, (maidArray[6].transform.position.y + maidArray[7].transform.position.y) / 2f, (maidArray[6].transform.position.z + maidArray[7].transform.position.z) / 2f));
						maidArray[6].body0.transform.localRotation = Quaternion.Lerp(maidArray[6].transform.localRotation, maidArray[7].transform.localRotation, 0.5f);
					}
					if (maidArray[8] && maidArray[8].Visible && maidArray[9] && maidArray[9].Visible)
					{
						maidArray[8].SetPos(new Vector3((maidArray[8].transform.position.x + maidArray[9].transform.position.x) / 2f, (maidArray[8].transform.position.y + maidArray[9].transform.position.y) / 2f, (maidArray[8].transform.position.z + maidArray[9].transform.position.z) / 2f));
						maidArray[8].body0.transform.localRotation = Quaternion.Lerp(maidArray[8].transform.localRotation, maidArray[9].transform.localRotation, 0.5f);
					}
					if (maidArray[10] && maidArray[10].Visible && maidArray[11] && maidArray[11].Visible)
					{
						maidArray[10].SetPos(new Vector3((maidArray[10].transform.position.x + maidArray[11].transform.position.x) / 2f, (maidArray[10].transform.position.y + maidArray[11].transform.position.y) / 2f, (maidArray[10].transform.position.z + maidArray[11].transform.position.z) / 2f));
						maidArray[10].body0.transform.localRotation = Quaternion.Lerp(maidArray[10].transform.localRotation, maidArray[11].transform.localRotation, 0.5f);
					}
					if (maidArray[12] && maidArray[12].Visible && maidArray[13] && maidArray[13].Visible)
					{
						maidArray[12].SetPos(new Vector3((maidArray[12].transform.position.x + maidArray[13].transform.position.x) / 2f, (maidArray[12].transform.position.y + maidArray[13].transform.position.y) / 2f, (maidArray[12].transform.position.z + maidArray[13].transform.position.z) / 2f));
						maidArray[12].body0.transform.localRotation = Quaternion.Lerp(maidArray[12].transform.localRotation, maidArray[13].transform.localRotation, 0.5f);
					}
					if (maidArray[1] && maidArray[1].Visible)
					{
						maidArray[1].SetPos(new Vector3(maidArray[0].transform.position.x, maidArray[0].transform.position.y, maidArray[0].transform.position.z));
						maidArray[1].body0.transform.localRotation = maidArray[0].body0.transform.localRotation;
					}
					if (maidArray[3] && maidArray[3].Visible)
					{
						maidArray[3].SetPos(new Vector3(maidArray[2].transform.position.x, maidArray[2].transform.position.y, maidArray[2].transform.position.z));
						maidArray[3].body0.transform.localRotation = maidArray[2].body0.transform.localRotation;
					}
					if (maidArray[5] && maidArray[5].Visible)
					{
						maidArray[5].SetPos(new Vector3(maidArray[4].transform.position.x, maidArray[4].transform.position.y, maidArray[4].transform.position.z));
						maidArray[5].body0.transform.localRotation = maidArray[4].body0.transform.localRotation;
					}
					if (maidArray[7] && maidArray[7].Visible)
					{
						maidArray[7].SetPos(new Vector3(maidArray[6].transform.position.x, maidArray[6].transform.position.y, maidArray[6].transform.position.z));
						maidArray[7].body0.transform.localRotation = maidArray[6].body0.transform.localRotation;
					}
					if (maidArray[9] && maidArray[9].Visible)
					{
						maidArray[9].SetPos(new Vector3(maidArray[8].transform.position.x, maidArray[8].transform.position.y, maidArray[8].transform.position.z));
						maidArray[9].body0.transform.localRotation = maidArray[8].body0.transform.localRotation;
					}
					if (maidArray[11] && maidArray[11].Visible)
					{
						maidArray[11].SetPos(new Vector3(maidArray[10].transform.position.x, maidArray[10].transform.position.y, maidArray[10].transform.position.z));
						maidArray[11].body0.transform.localRotation = maidArray[10].body0.transform.localRotation;
					}
					if (maidArray[13] && maidArray[13].Visible)
					{
						maidArray[13].SetPos(new Vector3(maidArray[12].transform.position.x, maidArray[12].transform.position.y, maidArray[12].transform.position.z));
						maidArray[13].body0.transform.localRotation = maidArray[12].body0.transform.localRotation;
					}
					if (maidArray[2] && maidArray[2].Visible)
					{
						if (maidArray[0].transform.position == maidArray[2].transform.position)
						{
							Vector3 vector = Vector3.zero;
							vector.x = 0.6f;
							maidArray[1].SetPos(vector);
							if (maidArray[2] && maidArray[2].Visible)
							{
								vector.x = -0.6f;
								maidArray[2].SetPos(vector);
							}
							if (maidArray[3] && maidArray[3].Visible)
							{
								vector.x = 1.2f;
								maidArray[3].SetPos(vector);
							}
							if (maidArray[4] && maidArray[4].Visible)
							{
								vector.x = -1.2f;
								maidArray[4].SetPos(vector);
							}
							if (maidArray[5] && maidArray[5].Visible)
							{
								vector.x = 1.8f;
								maidArray[5].SetPos(vector);
							}
							if (maidArray[6] && maidArray[6].Visible)
							{
								vector.x = -1.8f;
								maidArray[6].SetPos(vector);
							}
							if (maidArray[7] && maidArray[7].Visible)
							{
								vector.x = 2.4f;
								maidArray[7].SetPos(vector);
							}
							if (maidArray[8] && maidArray[8].Visible)
							{
								vector.x = -2.4f;
								maidArray[8].SetPos(vector);
							}
							if (maidArray[9] && maidArray[9].Visible)
							{
								vector.x = 3f;
								maidArray[9].SetPos(vector);
							}
							if (maidArray[10] && maidArray[10].Visible)
							{
								vector.x = -3f;
								maidArray[10].SetPos(vector);
							}
							if (maidArray[11] && maidArray[11].Visible)
							{
								vector.x = 3.6f;
								maidArray[11].SetPos(vector);
							}
							if (maidArray[12] && maidArray[12].Visible)
							{
								vector.x = -3.6f;
								maidArray[12].SetPos(vector);
							}
							if (maidArray[13] && maidArray[13].Visible)
							{
								vector.x = 4.2f;
								maidArray[13].SetPos(vector);
							}
						}
					}
					isDance = true;
					isDanceStart15 = true;
					isSD1 = false;
				}
				if (!Input.GetKeyUp(KeyCode.Return) && Input.GetKeyUp(KeyCode.H) && !hFlg)
				{
					if (h2Flg)
					{
						h2Flg = false;
					}
					else
					{
						string text5 = "";
						if (wearIndex == 0)
						{
							text5 = "Underwear";
							wearIndex = 1;
							isWear = false;
							isSkirt = false;
							isBra = true;
							isPanz = true;
							isHeadset = false;
							isGlove = false;
							isStkg = true;
							isShoes = false;
						}
						else if (wearIndex == 1)
						{
							text5 = "Nude";
							wearIndex = 2;
							isWear = false;
							isSkirt = false;
							isBra = false;
							isPanz = false;
							isHeadset = false;
							isGlove = false;
							isStkg = false;
							isShoes = false;
						}
						else if (wearIndex == 2)
						{
							text5 = "None";
							wearIndex = 0;
							isWear = true;
							isSkirt = true;
							isBra = true;
							isPanz = true;
							isHeadset = true;
							isGlove = true;
							isStkg = true;
							isShoes = true;
						}
						TBody.MaskMode maskMode = (TBody.MaskMode)Enum.Parse(typeof(TBody.MaskMode), text5);
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								maidArray[j].body0.SetMaskMode(maskMode);
							}
						}
					}
				}
				if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && Input.GetKeyDown(KeyCode.S) && !sFlg)
				{
					saveScene = 9999;
					saveScene2 = saveScene;
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					try
					{
						thum_byte_to_base64_ = string.Empty;
						thum_file_path_ = Path.Combine(Path.GetTempPath(), "cm3d2_" + Guid.NewGuid().ToString() + ".png");
						GameMain.Instance.MainCamera.ScreenShot(thum_file_path_, 1, false);
					}
					catch
					{
					}
				}
				if (getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && Input.GetKeyDown(KeyCode.A))
				{
					loadScene = 9999;
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
				}
				if (isScreen)
				{
					isScreen = false;
					bGui = isGui;
					isGui = false;
					if (!isMessage)
					{
						foreach (UICamera uicamera in ui_cam_hide_list_)
						{
							uicamera.GetComponent<Camera>().enabled = true;
						}
						ui_cam_hide_list_.Clear();
					}
					else
					{
						if (editUI != null)
						{
							editUI.SetActive(true);
						}
						if (GameMain.Instance.CMSystem.ViewFps)
						{
							GameObject childObject = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/FpsCounter", false);
							childObject.SetActive(true);
						}
						GameObject childObject2 = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/SystemDialog", false);
						GameObject childObject3 = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/SystemShortcut", false);
						childObject2.SetActive(true);
						childObject3.SetActive(true);
					}
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							isBone[j] = isBoneN[j];
							if (isBone[j])
							{
								gNeck[j].SetActive(true);
								gSpine[j].SetActive(true);
								gSpine0a[j].SetActive(true);
								gSpine1a[j].SetActive(true);
								gSpine1[j].SetActive(true);
								gPelvis[j].SetActive(true);
								gHandL[j].SetActive(true);
								gArmL[j].SetActive(true);
								gFootL[j].SetActive(true);
								gHizaL[j].SetActive(true);
								gHandR[j].SetActive(true);
								gArmR[j].SetActive(true);
								gFootR[j].SetActive(true);
								gHizaR[j].SetActive(true);
								gClavicleL[j].SetActive(true);
								gClavicleR[j].SetActive(true);
							}
						}
					}
				}
				if (isScreen2)
				{
					isScreen = true;
					isScreen2 = false;
				}
				if (!isVR && Input.GetKeyDown(KeyCode.S) && !sFlg && !Input.GetKey(KeyCode.KeypadEnter) && !Input.GetKey(KeyCode.Return) && !Input.GetKey(KeyCode.Q))
				{
					if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt) && !getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
					{
						isScreen = true;
						for (int j = 0; j < maidCnt; j++)
						{
							if (maidArray[j] && maidArray[j].Visible)
							{
								if (isBone[j])
								{
									gNeck[j].SetActive(false);
									gSpine[j].SetActive(false);
									gSpine0a[j].SetActive(false);
									gSpine1a[j].SetActive(false);
									gSpine1[j].SetActive(false);
									gPelvis[j].SetActive(false);
									gHandL[j].SetActive(false);
									gArmL[j].SetActive(false);
									gFootL[j].SetActive(false);
									gHizaL[j].SetActive(false);
									gHandR[j].SetActive(false);
									gArmR[j].SetActive(false);
									gFootR[j].SetActive(false);
									gHizaR[j].SetActive(false);
									gClavicleL[j].SetActive(false);
									gClavicleR[j].SetActive(false);
								}
								isBoneN[j] = isBone[j];
								isBone[j] = false;
							}
						}
						if (!isMessage)
						{
							ui_cam_hide_list_.Clear();
							UICamera[] array17 = NGUITools.FindActive<UICamera>();
							foreach (UICamera uicamera2 in array17)
							{
								if (uicamera2.GetComponent<Camera>().enabled)
								{
									uicamera2.GetComponent<Camera>().enabled = false;
									ui_cam_hide_list_.Add(uicamera2);
								}
							}
						}
						else
						{
							editUI = GameObject.Find("/UI Root/Camera");
							if (editUI != null)
							{
								editUI.SetActive(false);
							}
							if (GameMain.Instance.CMSystem.ViewFps)
							{
								GameObject childObject = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/FpsCounter", false);
								childObject.SetActive(false);
							}
							GameObject childObject2 = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/SystemDialog", false);
							GameObject childObject3 = UTY.GetChildObject(GameMain.Instance.gameObject, "SystemUI Root/SystemShortcut", false);
							childObject2.SetActive(false);
							childObject3.SetActive(false);
						}
						isGui = bGui;
						bGui = false;
						GameMain.Instance.MainCamera.ScreenShot(false);
						GameMain.Instance.SoundMgr.PlaySe("se022.ogg", false);
					}
				}
				if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.U) && !qFlg)
				{
					mainCameraTransform.Rotate(0f, 0f, -0.15f);
				}
				else if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.O) && !qFlg)
				{
					mainCameraTransform.Rotate(0f, 0f, 0.15f);
				}
				else if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.BackQuote) && !qFlg)
				{
					mainCameraTransform.eulerAngles = new Vector3(mainCameraTransform.rotation.eulerAngles.x, mainCameraTransform.rotation.eulerAngles.y, 0f);
				}
				else if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.R) && !qFlg)
				{
					GameMain.Instance.MainCamera.Reset(0, true);
					mainCamera.SetTargetPos(new Vector3(0f, 0.9f, 0f), true);
					mainCamera.SetDistance(3f, true);
				}
				else if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.S) && !qFlg)
				{
					cameraIti = mainCamera.GetTargetPos();
					cameraIti2 = mainCamera.GetPos();
					cameraItiAngle = mainCamera.GetAroundAngle();
					cameraItiDistance = mainCamera.GetDistance();
				}
				else if (!isVR && Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.A) && !qFlg)
				{
					mainCamera.SetTargetPos(cameraIti, true);
					mainCamera.SetPos(cameraIti2);
					mainCamera.SetAroundAngle(cameraItiAngle, true);
					mainCamera.SetDistance(cameraItiDistance, true);
				}
				if (!isVR && Input.GetKeyUp(KeyCode.Q) && qFlg)
				{
					qFlg = false;
				}
				if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.Space))
				{
					bool flag5 = false;
					for (int k = 0; k < keyArray.Length; k++)
					{
						if (Input.GetKey(keyArray[k]))
						{
							flag5 = true;
							break;
						}
					}
					if (!getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						if (!flag5)
						{
							Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
							Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
							Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
							if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 6E-05f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 6E-05f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -6E-05f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -6E-05f * speed;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector2.x, 0f, vector2.z) * -6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector3.x, 0f, vector3.z) * -6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.L))
							{
								Vector3 vector = softG;
								vector += new Vector3(vector3.x, 0f, vector3.z) * 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Alpha0))
							{
								Vector3 vector = softG;
								vector.y += 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.P))
							{
								Vector3 vector = softG;
								vector.y -= 6E-05f * speed;
								softG = vector;
							}
							else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.BackQuote) && !atFlg)
							{
								softG = new Vector3(0f, -0.003f, 0f);
							}
							for (int j = 0; j < maidCnt; j++)
							{
								Maid maid = maidArray[j];
								for (int l = 0; l < maid.body0.goSlot.Count; l++)
								{
									if (maid.body0.goSlot[l].obj != null)
									{
										DynamicBone component2 = maid.body0.goSlot[l].obj.GetComponent<DynamicBone>();
										if (component2 != null && component2.enabled)
										{
											component2.m_Gravity = new Vector3(softG.x * 5f, (softG.y + 0.003f) * 5f, softG.z * 5f);
										}
									}
									List<THair1> fieldValue3 = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid.body0.goSlot[l].bonehair, "hair1list");
									for (int k = 0; k < fieldValue3.Count; k++)
									{
										fieldValue3[k].SoftG = new Vector3(softG.x, softG.y + kamiyure, softG.z);
									}
								}
							}
						}
					}
					else if (!flag5)
					{
						Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
						Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
						Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
						if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.J))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 4E-05f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 4E-05f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -4E-05f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -4E-05f * speed;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.I))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * 4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.K))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector2.x, 0f, vector2.z) * -4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.J))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector3.x, 0f, vector3.z) * -4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector = softG2;
							vector += new Vector3(vector3.x, 0f, vector3.z) * 4E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Alpha0))
						{
							Vector3 vector = softG2;
							vector.y += 2E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.P))
						{
							Vector3 vector = softG2;
							vector.y -= 2E-05f * speed;
							softG2 = vector;
						}
						else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.BackQuote) && !atFlg)
						{
							softG2 = new Vector3(0f, -0.005f, 0f);
						}
						for (int j = 0; j < maidCnt; j++)
						{
							Maid maid = maidArray[j];
							for (int l = 0; l < maid.body0.goSlot.Count; l++)
							{
								if (maid.body0.goSlot[l].obj != null)
								{
									DynamicSkirtBone fieldValue8 = AdvancedStudioMode.GetFieldValue<BoneHair3, DynamicSkirtBone>(maidArray[j].body0.goSlot[l].bonehair3, "m_SkirtBone");
									if (fieldValue8 != null)
									{
										fieldValue8.m_vGravity = new Vector3(softG2.x, softG2.y, softG2.z);
										fieldValue8.UpdateParameters();
									}
								}
							}
						}
					}
				}
				if (!isVR)
				{
					int i;
					for (i = 0; i < 999; i++)
					{
						if (gDogu[i] != null)
						{
							gDogu[i].GetComponent<Renderer>().enabled = false;
							gDogu[i].SetActive(false);
							if (mDogu[i].del)
							{
								mDogu[i].del = false;
								Object.Destroy(doguBObject[i]);
								doguBObject.RemoveAt(i);
							}
							else if (mDogu[i].copy)
							{
								mDogu[i].copy = false;
								GameObject gameObject6 = Object.Instantiate<GameObject>(doguBObject[i]);
								gameObject6.transform.Translate(-0.3f, 0f, 0f);
								doguBObject.Add(gameObject6);
								gameObject6.name = doguBObject[i].name;
								doguCnt = doguBObject.Count - 1;
								gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
								gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
								gDogu[doguCnt].layer = 8;
								gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
								gDogu[doguCnt].SetActive(false);
								gDogu[doguCnt].transform.position = gameObject6.transform.position;
								mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
								mDogu[doguCnt].isScale = false;
								mDogu[doguCnt].obj = gDogu[doguCnt];
								mDogu[doguCnt].maid = gameObject6;
								mDogu[doguCnt].angles = gameObject6.transform.eulerAngles;
								gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
								mDogu[doguCnt].ido = 1;
							}
							else if (mDogu[i].count > 0)
							{
								mDogu[i].count--;
								if (doguBObject.Count > i && doguBObject[i] != null && doguBObject[i].name.StartsWith("Particle/p"))
								{
									if (mDogu[i].count == 1)
									{
										doguBObject[i].SetActive(false);
									}
									if (mDogu[i].count == 0)
									{
										doguBObject[i].SetActive(true);
										string name = doguBObject[i].name;
										if (name != null)
										{
											if (!(name == "Particle/pLineY"))
											{
												if (!(name == "Particle/pLineP02"))
												{
													if (!(name == "Particle/pLine_act2"))
													{
														if (name == "Particle/pHeart01")
														{
															mDogu[i].count = 77;
														}
													}
													else
													{
														mDogu[i].count = 90;
													}
												}
												else
												{
													mDogu[i].count = 115;
												}
											}
											else
											{
												mDogu[i].count = 180;
											}
										}
									}
								}
							}
						}
					}
					if (Input.GetKey(KeyCode.Z) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						ikMode2 = 11;
					}
					else if (Input.GetKey(KeyCode.Z) && getModKeyPressing(AdvancedStudioMode.modKey.Ctrl))
					{
						ikMode2 = 12;
					}
					else if (Input.GetKey(KeyCode.X) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						ikMode2 = 14;
					}
					else if (Input.GetKey(KeyCode.X))
					{
						ikMode2 = 9;
					}
					else if (Input.GetKey(KeyCode.Z))
					{
						ikMode2 = 10;
					}
					else if (Input.GetKey(KeyCode.C))
					{
						ikMode2 = 13;
					}
					else if (Input.GetKey(KeyCode.D))
					{
						ikMode2 = 15;
					}
					else if (Input.GetKey(KeyCode.V))
					{
						ikMode2 = 16;
					}
					else
					{
						ikMode2 = 0;
					}
					if (gBg != null)
					{
						if (!isCube3)
						{
							gBg.GetComponent<Renderer>().enabled = false;
							gBg.SetActive(false);
						}
						else
						{
							if (ikMode2 > 0 && ikMode2 != 15 && ikMode2 != 16)
							{
								gBg.GetComponent<Renderer>().enabled = true;
								gBg.SetActive(true);
							}
							else
							{
								gBg.GetComponent<Renderer>().enabled = false;
								gBg.SetActive(false);
							}
							if (ikMode2 == 10 || ikMode2 == 11 || ikMode2 == 12)
							{
								gBg.GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
							}
							if (ikMode2 == 9 || ikMode2 == 14)
							{
								gBg.GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f, 0.5f);
								mBg.Update();
							}
							if (ikMode2 == 13)
							{
								gBg.GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f, 0.5f);
								mBg.Update();
							}
							if (ikMode2 == 13)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 13 && gBg)
								{
									mBg.ido = 5;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 5;
								}
							}
							else if (ikMode2 == 11)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 11 && gBg)
								{
									mBg.ido = 3;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 3;
								}
							}
							else if (ikMode2 == 12)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 12 && gBg)
								{
									mBg.ido = 2;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 2;
								}
							}
							else if (ikMode2 == 10)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 10 && gBg)
								{
									mBg.ido = 1;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 1;
								}
							}
							else if (ikMode2 == 9)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 9 && gBg)
								{
									mBg.ido = 4;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 4;
								}
							}
							else if (ikMode2 == 14)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 14 && gBg)
								{
									mBg.ido = 6;
									mBg.reset = true;
								}
								else
								{
									gBg.transform.position = bg.position;
									gBg.transform.eulerAngles = bg.eulerAngles;
									mBg.maid = bgObject;
									mBg.ido = 6;
								}
							}
						}
					}
					i = 0;
					while (i < lightIndex.Count)
					{
						if (gLight[0] == null)
						{
							gLight[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
							Material material = new Material(Shader.Find("Transparent/Diffuse"));
							material.color = new Color(0.5f, 0.5f, 1f, 0.8f);
							gLight[0].GetComponent<Renderer>().material = material;
							gLight[0].layer = 8;
							gLight[0].GetComponent<Renderer>().enabled = false;
							gLight[0].SetActive(false);
							gLight[0].transform.position = GameMain.Instance.MainLight.transform.position;
							mLight[0] = gLight[0].AddComponent<MouseDrag6>();
							mLight[0].obj = gLight[0];
							mLight[0].maid = GameMain.Instance.MainLight.gameObject;
							mLight[0].angles = GameMain.Instance.MainLight.gameObject.transform.eulerAngles;
							gLight[0].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
							mLight[0].ido = 1;
							mLight[0].isScale = false;
						}
						if (gLight[i] != null)
						{
							if (!isCube4)
							{
								gLight[i].GetComponent<Renderer>().enabled = false;
								gLight[i].SetActive(false);
							}
							else if (lightList[i].GetComponent<Light>().type == LightType.Spot || lightList[i].GetComponent<Light>().type == LightType.Point)
							{
								if (ikMode2 > 0 && ikMode2 != 15)
								{
									gLight[i].GetComponent<Renderer>().enabled = true;
									gLight[i].SetActive(true);
								}
								else
								{
									gLight[i].GetComponent<Renderer>().enabled = false;
									gLight[i].SetActive(false);
									mLight[i].isAlt = false;
								}
								if (ikMode2 == 10 || ikMode2 == 11 || ikMode2 == 12)
								{
									gLight[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
									if (mLight[i].isAlt)
									{
										gLight[i].GetComponent<Renderer>().material.color = new Color(0.6f, 0.6f, 1f, 0.5f);
									}
								}
								if (ikMode2 == 9 || ikMode2 == 14)
								{
									gLight[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f, 0.5f);
									mLight[i].Update();
								}
								if (ikMode2 == 13)
								{
									gLight[i].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f, 0.5f);
									mLight[i].Update();
								}
								if (ikMode2 == 13)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 13 && gLight[i])
									{
										mLight[i].ido = 15;
										mLight[i].reset = true;
									}
									else
									{
										if (lightList[i].transform.localScale.x == 1f)
										{
											lightList[i].transform.localScale = new Vector3(lightRange[i], lightRange[i], lightRange[i]);
										}
										lightRange[i] = lightList[i].transform.localScale.x;
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										mLight[i].maid = lightList[i];
										mLight[i].ido = 15;
									}
								}
								else if (ikMode2 == 11)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 11 && gLight[i])
									{
										mLight[i].ido = 3;
										mLight[i].reset = true;
									}
									else
									{
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										lightX[i] = gLight[i].transform.eulerAngles.x;
										lightY[i] = gLight[i].transform.eulerAngles.y;
										mLight[i].maid = lightList[i];
										mLight[i].ido = 3;
									}
								}
								else if (ikMode2 == 12)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 12 && gLight[i])
									{
										mLight[i].ido = 2;
										mLight[i].reset = true;
									}
									else
									{
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										mLight[i].maid = lightList[i];
										mLight[i].ido = 2;
									}
								}
								else if (ikMode2 == 10)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 10 && gLight[i])
									{
										mLight[i].ido = 1;
										mLight[i].reset = true;
									}
									else
									{
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										mLight[i].maid = lightList[i];
										mLight[i].maidArray = lightList.ToArray();
										mLight[i].mArray = mLight.ToArray<MouseDrag6>();
										mLight[i].ido = 1;
									}
								}
								else if (ikMode2 == 9)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 9 && gLight[i])
									{
										mLight[i].ido = 4;
										mLight[i].reset = true;
									}
									else
									{
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										lightX[i] = gLight[i].transform.eulerAngles.x;
										lightY[i] = gLight[i].transform.eulerAngles.y;
										mLight[i].maid = lightList[i];
										mLight[i].ido = 4;
									}
								}
								else if (ikMode2 == 14)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 14 && gLight[i])
									{
										mLight[i].ido = 6;
										mLight[i].reset = true;
									}
									else
									{
										gLight[i].transform.position = lightList[i].transform.position;
										gLight[i].transform.eulerAngles = lightList[i].transform.eulerAngles;
										lightX[i] = gLight[i].transform.eulerAngles.x;
										lightY[i] = gLight[i].transform.eulerAngles.y;
										mLight[i].maid = lightList[i];
										mLight[i].ido = 6;
									}
								}
							}
						}
						//IL_31E59:
						i++;
						continue;
						//goto IL_31E59;
					}
					for (i = 0; i < doguBObject.Count; i++)
					{
						if (!isCube2)
						{
							gDogu[i].GetComponent<Renderer>().enabled = false;
							gDogu[i].SetActive(false);
						}
						else
						{
							if (ikMode2 > 0)
							{
								gDogu[i].GetComponent<Renderer>().enabled = true;
								gDogu[i].SetActive(true);
							}
							else
							{
								gDogu[i].GetComponent<Renderer>().enabled = false;
								gDogu[i].SetActive(false);
								mDogu[i].isAlt = false;
							}
							if (ikMode2 == 10 || ikMode2 == 11 || ikMode2 == 12)
							{
								gDogu[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
								if (mDogu[i].isAlt)
								{
									gDogu[i].GetComponent<Renderer>().material.color = new Color(0.6f, 0.6f, 1f, 0.5f);
								}
							}
							if (ikMode2 == 9 || ikMode2 == 14)
							{
								gDogu[i].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f, 0.5f);
								mDogu[i].Update();
							}
							if (ikMode2 == 15)
							{
								gDogu[i].GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 0.5f);
							}
							if (ikMode2 == 16)
							{
								gDogu[i].GetComponent<Renderer>().material.color = new Color(0.7f, 0.3f, 0.7f, 0.5f);
								mDogu[i].Update();
							}
							if (ikMode2 == 13)
							{
								gDogu[i].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f, 0.5f);
								mDogu[i].Update();
							}
							if (ikMode2 == 13)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 13 && gDogu[i])
								{
									mDogu[i].ido = 5;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 5;
								}
							}
							else if (ikMode2 == 11)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 11 && gDogu[i])
								{
									mDogu[i].ido = 3;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 3;
								}
							}
							else if (ikMode2 == 12)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 12 && gDogu[i])
								{
									mDogu[i].ido = 2;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 2;
								}
							}
							else if (ikMode2 == 10)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 10 && gDogu[i])
								{
									mDogu[i].ido = 1;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].maidArray = doguBObject.ToArray();
									mDogu[i].mArray = mDogu.ToArray<MouseDrag6>();
									mDogu[i].ido = 1;
								}
							}
							else if (ikMode2 == 9)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 9 && gDogu[i])
								{
									mDogu[i].ido = 4;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 4;
								}
							}
							else if (ikMode2 == 14)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 14 && gDogu[i])
								{
									mDogu[i].ido = 6;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 6;
								}
							}
							else if (ikMode2 == 15)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 15 && gDogu[i])
								{
									mDogu[i].ido = 7;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 7;
								}
							}
							else if (ikMode2 == 16)
							{
								if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 16 && gDogu[i])
								{
									mDogu[i].ido = 8;
									mDogu[i].reset = true;
								}
								else
								{
									gDogu[i].transform.position = doguBObject[i].transform.position;
									gDogu[i].transform.eulerAngles = doguBObject[i].transform.eulerAngles;
									mDogu[i].maid = doguBObject[i];
									mDogu[i].ido = 8;
								}
							}
						}
					}
					ikModeOld2 = ikMode2;
				}
				if (isVR)
				{
					if (Input.GetKeyDown(KeyCode.F8) && getModKeyPressing(AdvancedStudioMode.modKey.Shift))
					{
						if (!isVR2)
						{
							isVR2 = true;
							base.Preferences["config"]["shift_f8"].Value = "true";
							base.SaveConfig();
							GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
						}
						else
						{
							isVR2 = false;
							base.Preferences["config"]["shift_f8"].Value = "false";
							base.SaveConfig();
							GameMain.Instance.SoundMgr.PlaySe("se003.ogg", false);
						}
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha1))
					{
						loadScene = 1;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha2))
					{
						loadScene = 2;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha3))
					{
						loadScene = 3;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha4))
					{
						loadScene = 4;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha5))
					{
						loadScene = 5;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha6))
					{
						loadScene = 6;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha7))
					{
						loadScene = 7;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha8))
					{
						loadScene = 8;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha9))
					{
						loadScene = 9;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F8) && Input.GetKeyDown(KeyCode.Alpha0))
					{
						loadScene = 10;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					for (int j = 0; j < 7; j++)
					{
						if (!maidArray[j])
						{
							maidArray[j] = GameMain.Instance.CharacterMgr.GetMaid(j);
						}
					}
				}
			}
		}
	}
}
