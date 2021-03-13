using ExIni;
using MyRoomCustom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

namespace CM3D2.AdvancedStudioMode.Plugin
{
    public partial class AdvancedStudioMode
    {
		public void OnGUI()
		{
			for (int i = 0; i < maidCnt; i++)
			{
				if (isPoseIti[i])
				{
					Maid maid = maidArray[i];
					isPoseIti[i] = false;
					maid.transform.position = poseIti[i];
					Vector3 eulerAngles = maid.transform.eulerAngles;
					for (int j = 0; j < 10; j++)
					{
						maid.transform.RotateAround(maid.transform.position, Vector3.right, -maid.transform.rotation.eulerAngles.x);
						maid.transform.RotateAround(maid.transform.position, Vector3.forward, -maid.transform.rotation.eulerAngles.z);
					}
					Transform transform = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
					transform.position = new Vector3(poseIti[i].x, transform.position.y, poseIti[i].z);
					maid.transform.eulerAngles = eulerAngles;
				}
			}
			GUIStyle guistyle = "box";
			guistyle.fontSize = GetPix(11);
			guistyle.alignment = TextAnchor.UpperRight;
			if (bGui)
			{
				if (isGuiInit || screenSize != new Vector2((float)Screen.width, (float)Screen.height))
				{
					isGuiInit = false;
					screenSize = new Vector2((float)Screen.width, (float)Screen.height);
				}
				if (sceneLevel != 5 && sceneLevel != 14)
				{
					if (kankyoFlg || kankyo2Flg)
					{
						rectWin.Set(0f, 0f, (float)GetPix(210), (float)Screen.height * 0.9f);
					}
					else
					{
						rectWin.Set(0f, 0f, (float)GetPix(170), (float)Screen.height * 0.9f);
					}
				}
				else if (kankyoFlg || kankyo2Flg)
				{
					rectWin.Set(0f, 0f, (float)GetPix(210), (float)Screen.height * 0.9f * 0.85f);
				}
				else
				{
					rectWin.Set(0f, 0f, (float)GetPix(170), (float)Screen.height * 0.9f * 0.85f);
				}
				rectWin.x = screenSize.x - rectWin.width;
				rectWin.y = (float)GetPix(65);
				if (sceneLevel == 14)
				{
					rectWin.x = screenSize.x - rectWin.width - (float)GetPix(23);
				}
				comboBoxControl.height = rectWin.height;
				faceCombo.height = rectWin.height;
				poseCombo.height = rectWin.height;
				poseGroupCombo.height = rectWin.height;
				itemCombo.height = rectWin.height;
				bgmCombo.height = rectWin.height;
				itemCombo2.height = rectWin.height;
				myCombo.height = rectWin.height;
				bgCombo2.height = rectWin.height;
				kankyoCombo.height = rectWin.height;
				bgCombo.height = rectWin.height;
				slotCombo.height = rectWin.height;
				doguCombo2.height = rectWin.height;
				for (int i = 0; i < doguCombo.Length; i++)
				{
					doguCombo[i].height = rectWin.height;
				}
				parCombo.height = rectWin.height;
				parCombo1.height = rectWin.height;
				lightCombo.height = rectWin.height;
				GameMain.Instance.MainCamera.SetControl(true);
				if (!sceneFlg && !faceFlg && !poseFlg && !kankyoFlg && !kankyo2Flg && !isF6 && okFlg)
				{
					if (Input.GetAxis("Mouse ScrollWheel") != 0f)
					{
						GameMain.Instance.MainCamera.SetControl(!rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)));
					}
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc), "", guistyle);
				}
				else if (sceneFlg)
				{
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc5), "", guistyle);
					Rect rect = new Rect(0f, 0f, 0f, 0f);
					dispNo = 0;
					for (int i = 0; i < 10; i++)
					{
						rect = new Rect(0f, 0f, (float)GetPix(170), (float)GetPix(36));
						rect.x = screenSize.x - rect.width;
						rect.y = rectWin.y + (float)GetPix(64 + 50 * i);
						if (rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
						{
							dispNo = i + 1;
							break;
						}
					}
					if (saveScene2 > 0)
					{
						dispNo = 0;
					}
					if (dispNo == 0)
					{
						texture2D = null;
						dispNoOld = 0;
					}
					else if (dispNo != dispNoOld)
					{
						dispNoOld = dispNo;
						texture2D = null;
						try
						{
							string path = string.Concat(new object[]
							{
								Path.GetFullPath(".\\"),
								"Mod\\AdvancedStudioModeScene\\",
								page * 10 + dispNo,
								".png"
							});
							if (File.Exists(path))
							{
								FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
								BinaryReader binaryReader = new BinaryReader(input);
								byte[] data = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
								binaryReader.Close();
								texture2D = new Texture2D(640, 360);
								texture2D.LoadImage(data);
							}
							else
							{
								IniKey iniKey = base.Preferences["scene"]["ss" + (page * 10 + dispNo)];
								if (iniKey.Value != null && iniKey.Value != "")
								{
									byte[] data2 = Convert.FromBase64String(iniKey.Value);
									texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
									texture2D.LoadImage(data2);
								}
							}
						}
						catch
						{
						}
					}
					if (texture2D != null)
					{
						if (waku == null)
						{
							waku = MakeTex(2, 2, new Color(1f, 1f, 1f, 1f));
							waku2 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.4f));
						}
						Rect position = new Rect(rect.x - (float)texture2D.width - (float)GetPix(18), rect.y - (float)(texture2D.height / 2) + (float)GetPix(12), (float)(texture2D.width + GetPix(12)), (float)(texture2D.height + GetPix(12)));
						Rect position2 = new Rect(rect.x - (float)texture2D.width - (float)GetPix(12), rect.y - (float)(texture2D.height / 2) + (float)GetPix(18), (float)texture2D.width, (float)texture2D.height);
						Rect position3 = new Rect(rect.x - (float)texture2D.width - (float)GetPix(16), rect.y - (float)(texture2D.height / 2) + (float)GetPix(14), (float)(texture2D.width + GetPix(12)), (float)(texture2D.height + GetPix(12)));
						if (position.y + position.height > (float)Screen.height)
						{
							float num = position3.y + position3.height - (float)Screen.height;
							position.y -= num;
							position2.y -= num;
							position3.y -= num;
						}
						if (position.y < 0f)
						{
							float num = position3.y;
							position.y -= num;
							position2.y -= num;
							position3.y -= num;
						}
						GUI.DrawTexture(position3, waku2);
						GUI.DrawTexture(position, waku);
						GUI.DrawTexture(position2, texture2D);
					}
				}
				else if (kankyoFlg)
				{
					if (bgmCombo.isClickedComboButton || bgCombo.isClickedComboButton || parCombo.isClickedComboButton)
					{
						if (Input.GetAxis("Mouse ScrollWheel") != 0f)
						{
							GameMain.Instance.MainCamera.SetControl(!rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)));
						}
					}
					else if (!yotogiFlg && sceneLevel != 3 && sceneLevel != 5 && rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
					{
						if (Input.GetMouseButtonDown(0))
						{
							Input.ResetInputAxes();
						}
					}
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc6), "", guistyle);
				}
				else if (kankyo2Flg)
				{
					if (Input.GetAxis("Mouse ScrollWheel") != 0f)
					{
						GameMain.Instance.MainCamera.SetControl(!rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)));
					}
					if (!yotogiFlg && sceneLevel != 3 && sceneLevel != 5 && rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
					{
						if (Input.GetMouseButtonDown(0))
						{
							Input.ResetInputAxes();
						}
					}
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc7), "", guistyle);
				}
				else if (poseFlg)
				{
					if (poseGroupCombo.isClickedComboButton || poseCombo.isClickedComboButton || itemCombo.isClickedComboButton)
					{
						if (Input.GetAxis("Mouse ScrollWheel") != 0f)
						{
							GameMain.Instance.MainCamera.SetControl(!rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)));
						}
					}
					else if (!yotogiFlg && sceneLevel != 3 && sceneLevel != 5 && rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
					{
						if (Input.GetMouseButtonDown(0))
						{
							Input.ResetInputAxes();
						}
					}
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc4), "", guistyle);
				}
				else
				{
					if (faceCombo.isClickedComboButton)
					{
						if (Input.GetAxis("Mouse ScrollWheel") != 0f)
						{
							GameMain.Instance.MainCamera.SetControl(!rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)));
						}
					}
					else if (!yotogiFlg && sceneLevel != 3 && sceneLevel != 5 && rectWin.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
					{
						if (Input.GetMouseButtonDown(0))
						{
							Input.ResetInputAxes();
						}
					}
					rectWin = GUI.Window(129, rectWin, new GUI.WindowFunction(GuiFunc2), "", guistyle);
				}
			}
			if (bGuiMessage)
			{
				screenSize = new Vector2((float)Screen.width, (float)Screen.height);
				rectWin2.Set(0f, 0f, (float)Screen.width * 0.4f, (float)Screen.height * 0.15f);
				rectWin2.x = screenSize.x / 2f - rectWin2.width / 2f;
				if (sceneLevel == 5)
				{
					rectWin2.y = screenSize.y * 0.94f - rectWin2.height;
				}
				else
				{
					rectWin2.y = screenSize.y - rectWin2.height;
				}
				rectWin2 = GUI.Window(129, rectWin2, new GUI.WindowFunction(GuiFunc3), "", guistyle);
			}
		}
		private void GuiFunc3(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "button";
			guistyle2.fontSize = GetPix(16);
			GUI.Label(new Rect((float)GetPix(8), (float)GetPix(4), (float)GetPix(80), (float)GetPix(25)), "名前", guistyle);
			inName = GUI.TextField(new Rect((float)GetPix(35), (float)GetPix(4), (float)GetPix(120), (float)GetPix(20)), inName);
			GUI.Label(new Rect((float)GetPix(180), (float)GetPix(4), (float)GetPix(100), (float)GetPix(25)), "サイズ", guistyle);
			fontSize = (int)GUI.HorizontalSlider(new Rect((float)GetPix(220), (float)GetPix(9), (float)GetPix(100), (float)GetPix(20)), (float)fontSize, 25f, 60f);
			if (fontSize != mFontSize)
			{
				mFontSize = fontSize;
				GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
				GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
				MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
				MessageClass messageClass = new MessageClass(gameObject2, messageWindowMgr);
				UILabel component = UTY.GetChildObject(gameObject2, "MessageViewer/MsgParent/Message", false).GetComponent<UILabel>();
				AdvancedStudioMode.SetFieldValue2<UILabel, int>(component, "mFontSize", fontSize);
			}
			GUI.Label(new Rect((float)GetPix(325), (float)GetPix(4), (float)GetPix(100), (float)GetPix(25)), fontSize + "pt", guistyle);
			Rect position = new Rect((float)GetPix(8), (float)GetPix(26), rectWin2.width - (float)GetPix(15), (float)GetPix(52));
			inText = GUI.TextArea(position, inText, 93);
			if (GUI.Button(new Rect((float)GetPix(8), (float)GetPix(82), (float)GetPix(60), (float)GetPix(20)), "決定", guistyle2))
			{
				isMessage = true;
				bGuiMessage = false;
				GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
				GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
				MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
				messageWindowMgr.OpenMessageWindowPanel();
				MessageClass messageClass = new MessageClass(gameObject2, messageWindowMgr);
				UILabel component = UTY.GetChildObject(gameObject2, "MessageViewer/MsgParent/Message", false).GetComponent<UILabel>();
				component.ProcessText();
				AdvancedStudioMode.SetFieldValue2<UILabel, int>(component, "mFontSize", fontSize);
				AdvancedStudioMode.SetFieldValue5<MessageClass, UILabel>(messageClass, "message_label_", component);
				messageClass.SetText(inName, inText, "", 0);
				messageClass.FinishChAnime();
			}
		}
		private void GuiFunc5(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "button";
			guistyle2.fontSize = GetPix(12);
			guistyle2.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect((float)GetPix(50), (float)GetPix(6), (float)GetPix(100), (float)GetPix(25)), "シーン管理", guistyle);
			if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(3), (float)GetPix(23), (float)GetPix(23)), "→", guistyle2))
			{
				faceFlg = false;
				poseFlg = false;
				sceneFlg = false;
				kankyoFlg = true;
				kankyo2Flg = false;
				bGui = true;
				isGuiInit = true;
				copyIndex = 0;
			}
			int num = 50;
			if (GUI.Button(new Rect((float)GetPix(25), (float)GetPix(31), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle2))
			{
				page--;
				if (page < 0)
				{
					page = maxPage - 1;
				}
				int i = 0;
				while (i < 10)
				{
					date[i] = "未保存";
					ninzu[i] = "";
					string path = string.Concat(new object[]
					{
						Path.GetFullPath(".\\"),
						"Mod\\AdvancedStudioModeScene\\",
						page * 10 + i + 1,
						".png"
					});
					if (File.Exists(path))
					{
						FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
						BinaryReader binaryReader = new BinaryReader(input);
						byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
						byte[] value = new byte[]
						{
							array[36],
							array[35],
							array[34],
							array[33]
						};
						int count = BitConverter.ToInt32(value, 0) - 8;
						byte[] bytes = array.Skip(49).Take(count).ToArray<byte>();
						string text = Encoding.UTF8.GetString(bytes);
						text = AdvancedStudioMode.StringFromBase64Comp(text);
						if (text != "")
						{
							string[] array2 = text.Split(new char[]
							{
								'_'
							});
							if (array2.Length >= 2)
							{
								string[] array3 = array2[0].Split(new char[]
								{
									','
								});
								date[i] = array3[0];
								ninzu[i] = array3[1] + "人";
							}
						}
					}
					else
					{
						IniKey iniKey = base.Preferences["scene"]["s" + (page * 10 + i + 1)];
						if (iniKey.Value != null && iniKey.Value.ToString() != "")
						{
							string[] array2 = iniKey.Value.Split(new char[]
							{
								'_'
							});
							if (array2.Length >= 2)
							{
								string[] array3 = array2[0].Split(new char[]
								{
									','
								});
								date[i] = array3[0];
								ninzu[i] = array3[1] + "人";
							}
						}
					}
					
				//IL_3CD:
					i++;
					continue;
					//goto IL_3CD;
				}
			}
			if (GUI.Button(new Rect((float)GetPix(115), (float)GetPix(31), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle2))
			{
				page++;
				if (page >= maxPage)
				{
					page = 0;
				}
				int i = 0;
				while (i < 10)
				{
					date[i] = "未保存";
					ninzu[i] = "";
					string path = string.Concat(new object[]
					{
						Path.GetFullPath(".\\"),
						"Mod\\AdvancedStudioModeScene\\",
						page * 10 + i + 1,
						".png"
					});
					if (File.Exists(path))
					{
						FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
						BinaryReader binaryReader = new BinaryReader(input);
						byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
						byte[] value = new byte[]
						{
							array[36],
							array[35],
							array[34],
							array[33]
						};
						int count = BitConverter.ToInt32(value, 0) - 8;
						byte[] bytes = array.Skip(49).Take(count).ToArray<byte>();
						string text = Encoding.UTF8.GetString(bytes);
						text = AdvancedStudioMode.StringFromBase64Comp(text);
						if (text != "")
						{
							string[] array2 = text.Split(new char[]
							{
								'_'
							});
							if (array2.Length >= 2)
							{
								string[] array3 = array2[0].Split(new char[]
								{
									','
								});
								date[i] = array3[0];
								ninzu[i] = array3[1] + "人";
							}
						}
					}
					else
					{
						IniKey iniKey = base.Preferences["scene"]["s" + (page * 10 + i + 1)];
						if (iniKey.Value != null && iniKey.Value.ToString() != "")
						{
							string[] array2 = iniKey.Value.Split(new char[]
							{
								'_'
							});
							if (array2.Length >= 2)
							{
								string[] array3 = array2[0].Split(new char[]
								{
									','
								});
								date[i] = array3[0];
								ninzu[i] = array3[1] + "人";
							}
						}
					}
					
				//IL_6B3:
					i++;
					continue;
					//goto IL_6B3;
				}
			}
			GUI.Label(new Rect((float)GetPix(60), (float)GetPix(32), (float)GetPix(100), (float)GetPix(25)), page * 10 + 1 + " ～ " + (page * 10 + 10), guistyle);
			if (saveScene2 > 0 && string.IsNullOrEmpty(thum_byte_to_base64_) && File.Exists(thum_file_path_))
			{
				try
				{
					Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
					texture2D.LoadImage(File.ReadAllBytes(thum_file_path_));
					float num2 = (float)texture2D.width / (float)texture2D.height;
					Vector2 vector = new Vector2(960f, 540f);
					int num3 = texture2D.width;
					int num4 = texture2D.height;
					if (vector.x < (float)texture2D.width && vector.y < (float)texture2D.height)
					{
						num3 = (int)vector.x;
						num4 = Mathf.RoundToInt((float)num3 / num2);
						if (vector.y < (float)num4)
						{
							num4 = (int)vector.y;
							num3 = Mathf.RoundToInt((float)num4 * num2);
						}
					}
					else if (vector.x < (float)texture2D.width)
					{
						num3 = (int)vector.x;
						num4 = Mathf.RoundToInt((float)num3 / num2);
					}
					else if (vector.y < (float)texture2D.height)
					{
						num4 = (int)vector.y;
						num3 = Mathf.RoundToInt((float)num4 * num2);
					}
					TextureScale.Bilinear(texture2D, num3, num4);
					byte[] array = texture2D.EncodeToPNG();
					string text2 = "tEXt";
					string text3 = "Comment";
					string text4 = saveData;
					text4 = AdvancedStudioMode.Base64FromStringComp(text4);
					List<byte> list = new List<byte>();
					int num5 = 4;
					int length = text2.Length;
					int length2 = text3.Length;
					int num6 = 1;
					int length3 = text4.Length;
					int num7 = 4;
					int num8 = num5 + length + length2 + num6 + length3 + num7;
					int num9 = length2 + num6 + length3;
					byte[] collection = new byte[]
					{
						(byte)(num9 >> 24),
						(byte)(num9 >> 16),
						(byte)(num9 >> 8),
						(byte)num9
					};
					byte[] bytes2 = Encoding.UTF8.GetBytes(text2);
					byte[] bytes3 = Encoding.UTF8.GetBytes(text3);
					list.Add(0);
					byte[] bytes4 = Encoding.UTF8.GetBytes(text4);
					List<byte> list2 = new List<byte>(1000000);
					list2.AddRange(bytes2);
					list2.AddRange(bytes3);
					list2.Add(0);
					list2.AddRange(bytes4);
					uint num10 = new CRC32().Calc(list2.ToArray());
					byte[] collection2 = new byte[]
					{
						(byte)(num10 >> 24),
						(byte)(num10 >> 16),
						(byte)(num10 >> 8),
						(byte)num10
					};
					List<byte> list3 = new List<byte>();
					for (int i = 0; i < 33; i++)
					{
						list3.Add(array[i]);
					}
					List<byte> list4 = new List<byte>();
					for (int i = 33; i < array.Length; i++)
					{
						list4.Add(array[i]);
					}
					List<byte> list5 = new List<byte>(1000000);
					list5.AddRange(list3);
					list5.AddRange(collection);
					list5.AddRange(bytes2);
					list5.AddRange(bytes3);
					list5.Add(0);
					list5.AddRange(bytes4);
					list5.AddRange(collection2);
					list5.AddRange(list4);
					array = list5.ToArray();
					string path = string.Concat(new object[]
					{
						Path.GetFullPath(".\\"),
						"Mod\\AdvancedStudioModeScene\\",
						saveScene2,
						".png"
					});
					File.WriteAllBytes(path, array);
					thum_file_path_ = "";
					saveScene2 = 0;
				}
				catch
				{
				}
			}
			for (int i = 0; i < 10; i++)
			{
				GUI.Label(new Rect((float)GetPix(5), (float)GetPix(60 + num * i), (float)GetPix(25), (float)GetPix(25)), string.Concat(page * 10 + i + 1), guistyle);
				if (GUI.Button(new Rect((float)GetPix(20), (float)GetPix(78 + num * i), (float)GetPix(50), (float)GetPix(20)), "保存", guistyle2))
				{
					saveScene = page * 10 + i + 1;
					saveScene2 = saveScene;
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					isScreen2 = true;
					if (!isMessage)
					{
						ui_cam_hide_list_.Clear();
						UICamera[] array4 = NGUITools.FindActive<UICamera>();
						foreach (UICamera uicamera in array4)
						{
							if (uicamera.GetComponent<Camera>().enabled)
							{
								uicamera.GetComponent<Camera>().enabled = false;
								ui_cam_hide_list_.Add(uicamera);
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
					try
					{
						thum_byte_to_base64_ = string.Empty;
						thum_file_path_ = Path.Combine(Path.GetTempPath(), "cm3d2_" + Guid.NewGuid().ToString() + ".png");
						GameMain.Instance.MainCamera.ScreenShot(thum_file_path_, 1, true);
					}
					catch
					{
					}
				}
				GUI.Label(new Rect((float)GetPix(25), (float)GetPix(60 + num * i), (float)GetPix(100), (float)GetPix(25)), date[i], guistyle);
				GUI.Label(new Rect((float)GetPix(130), (float)GetPix(60 + num * i), (float)GetPix(100), (float)GetPix(25)), ninzu[i], guistyle);
				if (date[i] != "未保存")
				{
					if (GUI.Button(new Rect((float)GetPix(100), (float)GetPix(78 + num * i), (float)GetPix(50), (float)GetPix(20)), "読込", guistyle2))
					{
						loadScene = page * 10 + i + 1;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
				}
			}
		}

		private void GuiFunc7(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "label";
			guistyle2.fontSize = GetPix(11);
			guistyle2.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle3 = "button";
			guistyle3.fontSize = GetPix(20);
			GUIStyle guistyle4 = "button";
			guistyle4.fontSize = GetPix(14);
			guistyle4.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle5 = "button";
			guistyle5.fontSize = GetPix(12);
			guistyle5.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle6 = new GUIStyle("toggle");
			guistyle6.fontSize = GetPix(13);
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			Maid maid = maidArray[selectMaidIndex];
			if (!kankyo2InitFlg)
			{
				listStyle2.normal.textColor = Color.white;
				listStyle2.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle2.onHover.background = (listStyle2.hover.background = new Texture2D(2, 2));
				RectOffset padding = listStyle2.padding;
				RectOffset padding2 = listStyle2.padding;
				RectOffset padding3 = listStyle2.padding;
				int num = listStyle2.padding.bottom = GetPix(0);
				num = (padding3.top = num);
				num = (padding2.right = num);
				padding.left = num;
				listStyle2.fontSize = GetPix(11);
				listStyle3.normal.textColor = Color.white;
				listStyle3.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle3.onHover.background = (listStyle3.hover.background = new Texture2D(2, 2));
				RectOffset padding4 = listStyle3.padding;
				RectOffset padding5 = listStyle3.padding;
				num = (listStyle3.padding.top = GetPix(0));
				num = (padding5.right = num);
				padding4.left = num;
				listStyle3.padding.bottom = GetPix(0);
				listStyle3.fontSize = GetPix(14);
				bgCombo2.selectedItemIndex = bgIndexB;
				bgCombo2List = new GUIContent[bgArray.Length];
				int i = 0;
				while (i < bgArray.Length)
				{
					string text = bgArray[i];
					if (text == null)
					{
						goto IL_1662;
					}
					if (bgUiArray == null)
					{
						bgUiArray = new Dictionary<string, int>(120)
						{
							{
								"Salon",
								0
							},
							{
								"Syosai",
								1
							},
							{
								"Syosai_Night",
								2
							},
							{
								"DressRoom_NoMirror",
								3
							},
							{
								"MyBedRoom",
								4
							},
							{
								"MyBedRoom_Night",
								5
							},
							{
								"MyBedRoom_NightOff",
								6
							},
							{
								"Bathroom",
								7
							},
							{
								"PlayRoom",
								8
							},
							{
								"Pool",
								9
							},
							{
								"SMRoom",
								10
							},
							{
								"PlayRoom2",
								11
							},
							{
								"Salon_Garden",
								12
							},
							{
								"LargeBathRoom",
								13
							},
							{
								"MaidRoom",
								14
							},
							{
								"OiranRoom",
								15
							},
							{
								"Penthouse",
								16
							},
							{
								"Town",
								17
							},
							{
								"Kitchen",
								18
							},
							{
								"Kitchen_Night",
								19
							},
							{
								"Shitsumu",
								20
							},
							{
								"Shitsumu_Night",
								21
							},
							{
								"Salon_Entrance",
								22
							},
							{
								"Bar",
								23
							},
							{
								"Toilet",
								24
							},
							{
								"Train",
								25
							},
							{
								"SMRoom2",
								26
							},
							{
								"LockerRoom",
								27
							},
							{
								"Oheya",
								28
							},
							{
								"Salon_Day",
								29
							},
							{
								"ClassRoom",
								30
							},
							{
								"ClassRoom_Play",
								31
							},
							{
								"HoneymoonRoom",
								32
							},
							{
								"OutletPark",
								33
							},
							{
								"BigSight",
								34
							},
							{
								"BigSight_Night",
								35
							},
							{
								"PrivateRoom",
								36
							},
							{
								"PrivateRoom_Night",
								37
							},
							{
								"Sea",
								38
							},
							{
								"Sea_Night",
								39
							},
							{
								"Yashiki",
								40
							},
							{
								"Yashiki_Day",
								41
							},
							{
								"Yashiki_Pillow",
								42
							},
							{
								"rotenburo",
								43
							},
							{
								"rotenburo_night",
								44
							},
							{
								"villa",
								45
							},
							{
								"villa_night",
								46
							},
							{
								"villa_bedroom",
								47
							},
							{
								"villa_bedroom_night",
								48
							},
							{
								"villa_farm",
								49
							},
							{
								"villa_farm_night",
								50
							},
							{
								"karaokeroom",
								51
							},
							{
								"Theater",
								52
							},
							{
								"Theater_LightOff",
								53
							},
							{
								"LiveStage",
								54
							},
							{
								"LiveStage_Side",
								55
							},
							{
								"LiveStage_use_dance",
								56
							},
							{
								"BackStage",
								57
							},
							{
								"DanceRoom",
								58
							},
							{
								"EmpireClub_Rotary",
								59
							},
							{
								"EmpireClub_Rotary_Night",
								60
							},
							{
								"EmpireClub_Entrance",
								61
							},
							{
								"ShinShitsumu",
								62
							},
							{
								"ShinShitsumu_ChairRot",
								63
							},
							{
								"ShinShitsumu_Night",
								64
							},
							{
								"MyRoom",
								65
							},
							{
								"MyRoom_Night",
								66
							},
							{
								"OpemCafe",
								67
							},
							{
								"OpemCafe_Night",
								68
							},
							{
								"Restaurant",
								69
							},
							{
								"Restaurant_Night",
								70
							},
							{
								"MainKitchen",
								71
							},
							{
								"MainKitchen_Night",
								72
							},
							{
								"MainKitchen_LightOff",
								73
							},
							{
								"BarLounge",
								74
							},
							{
								"Casino",
								75
							},
							{
								"CasinoMini",
								76
							},
							{
								"SMClub",
								77
							},
							{
								"Soap",
								78
							},
							{
								"Spa",
								79
							},
							{
								"Spa_Night",
								80
							},
							{
								"ShoppingMall",
								81
							},
							{
								"ShoppingMall_Night",
								82
							},
							{
								"GameShop",
								83
							},
							{
								"MusicShop",
								84
							},
							{
								"HeroineRoom_A1",
								85
							},
							{
								"HeroineRoom_A1_Night",
								86
							},
							{
								"HeroineRoom_B1",
								87
							},
							{
								"HeroineRoom_B1_Night",
								88
							},
							{
								"HeroineRoom_C1",
								89
							},
							{
								"HeroineRoom_C1_Night",
								90
							},
							{
								"HeroineRoom_A",
								91
							},
							{
								"HeroineRoom_A_Night",
								92
							},
							{
								"HeroineRoom_B",
								93
							},
							{
								"HeroineRoom_B_Night",
								94
							},
							{
								"HeroineRoom_C",
								95
							},
							{
								"HeroineRoom_C_Night",
								96
							},
							{
								"Shukuhakubeya_BedRoom",
								97
							},
							{
								"Shukuhakubeya_BedRoom_Night",
								98
							},
							{
								"Shukuhakubeya_Other_BedRoom",
								99
							},
							{
								"Shukuhakubeya_Living",
								100
							},
							{
								"Shukuhakubeya_Living_Night",
								101
							},
							{
								"Shukuhakubeya_Toilet",
								102
							},
							{
								"Shukuhakubeya_Toilet_Night",
								103
							},
							{
								"Shukuhakubeya_WashRoom",
								104
							},
							{
								"Shukuhakubeya_WashRoom_Night",
								105
							},
							{
								"opemcafe_rance10",
								106
							},
							{
								"opemcafe_rance10_night",
								107
							},
							{
								"opemcafe_riddlejoker",
								108
							},
							{
								"opemcafe_riddlejoker_night",
								109
							},
							{
								"opemcafe_wanko",
								110
							},
							{
								"opemcafe_wanko_night",
								111
							},
							{
								"opemcafe_raspberry",
								112
							},
							{
								"opemcafe_raspberry_night",
								113
							},
							{
								"seacafe",
								114
							},
							{
								"seacafe_night",
								115
							},
							{
								"com3d2pool",
								116
							},
							{
								"com3d2pool_night",
								117
							},
							{
								"shrine",
								118
							},
							{
								"shrine_night",
								119
							}
						};
					}
					if (!bgUiArray.TryGetValue(text, out num))
					{
						goto IL_1662;
					}
					switch (num)
					{
						case 0:
							bgCombo2List[i] = new GUIContent("サロン");
							break;
						case 1:
							bgCombo2List[i] = new GUIContent("書斎");
							break;
						case 2:
							bgCombo2List[i] = new GUIContent("書斎(夜)");
							break;
						case 3:
							bgCombo2List[i] = new GUIContent("ドレスルーム");
							break;
						case 4:
							bgCombo2List[i] = new GUIContent("自室");
							break;
						case 5:
							bgCombo2List[i] = new GUIContent("自室(夜)");
							break;
						case 6:
							bgCombo2List[i] = new GUIContent("自室(消灯)");
							break;
						case 7:
							bgCombo2List[i] = new GUIContent("風呂");
							break;
						case 8:
							bgCombo2List[i] = new GUIContent("プレイルーム");
							break;
						case 9:
							bgCombo2List[i] = new GUIContent("プール");
							break;
						case 10:
							bgCombo2List[i] = new GUIContent("SMルーム");
							break;
						case 11:
							bgCombo2List[i] = new GUIContent("プレイルーム2");
							break;
						case 12:
							bgCombo2List[i] = new GUIContent("サロン(中庭)");
							break;
						case 13:
							bgCombo2List[i] = new GUIContent("大浴場");
							break;
						case 14:
							bgCombo2List[i] = new GUIContent("メイド部屋");
							break;
						case 15:
							bgCombo2List[i] = new GUIContent("花魁ルーム");
							break;
						case 16:
							bgCombo2List[i] = new GUIContent("ペントハウス");
							break;
						case 17:
							bgCombo2List[i] = new GUIContent("街");
							break;
						case 18:
							bgCombo2List[i] = new GUIContent("キッチン");
							break;
						case 19:
							bgCombo2List[i] = new GUIContent("キッチン(夜)");
							break;
						case 20:
							bgCombo2List[i] = new GUIContent("執務室");
							break;
						case 21:
							bgCombo2List[i] = new GUIContent("執務室(夜)");
							break;
						case 22:
							bgCombo2List[i] = new GUIContent("エントランス");
							break;
						case 23:
							bgCombo2List[i] = new GUIContent("バー");
							break;
						case 24:
							bgCombo2List[i] = new GUIContent("トイレ");
							break;
						case 25:
							bgCombo2List[i] = new GUIContent("電車");
							break;
						case 26:
							bgCombo2List[i] = new GUIContent("地下室");
							break;
						case 27:
							bgCombo2List[i] = new GUIContent("ロッカールーム");
							break;
						case 28:
							bgCombo2List[i] = new GUIContent("四畳半部屋");
							break;
						case 29:
							bgCombo2List[i] = new GUIContent("サロン(昼)");
							break;
						case 30:
							bgCombo2List[i] = new GUIContent("教室");
							break;
						case 31:
							bgCombo2List[i] = new GUIContent("教室(夜伽)");
							break;
						case 32:
							bgCombo2List[i] = new GUIContent("ハネムーンルーム");
							break;
						case 33:
							bgCombo2List[i] = new GUIContent("アウトレットパーク");
							break;
						case 34:
							bgCombo2List[i] = new GUIContent("ビッグサイト");
							break;
						case 35:
							bgCombo2List[i] = new GUIContent("ビッグサイト(夜)");
							break;
						case 36:
							bgCombo2List[i] = new GUIContent("プライベートルーム");
							break;
						case 37:
							bgCombo2List[i] = new GUIContent("プライベートルーム(夜)");
							break;
						case 38:
							bgCombo2List[i] = new GUIContent("海");
							break;
						case 39:
							bgCombo2List[i] = new GUIContent("海(夜)");
							break;
						case 40:
							bgCombo2List[i] = new GUIContent("屋敷(夜)");
							break;
						case 41:
							bgCombo2List[i] = new GUIContent("屋敷");
							break;
						case 42:
							bgCombo2List[i] = new GUIContent("屋敷(夜・枕)");
							break;
						case 43:
							bgCombo2List[i] = new GUIContent("露天風呂");
							break;
						case 44:
							bgCombo2List[i] = new GUIContent("露天風呂(夜)");
							break;
						case 45:
							bgCombo2List[i] = new GUIContent("ヴィラ1F");
							break;
						case 46:
							bgCombo2List[i] = new GUIContent("ヴィラ1F(夜)");
							break;
						case 47:
							bgCombo2List[i] = new GUIContent("ヴィラ2F");
							break;
						case 48:
							bgCombo2List[i] = new GUIContent("ヴィラ2F(夜)");
							break;
						case 49:
							bgCombo2List[i] = new GUIContent("畑");
							break;
						case 50:
							bgCombo2List[i] = new GUIContent("畑(夜)");
							break;
						case 51:
							bgCombo2List[i] = new GUIContent("カラオケルーム");
							break;
						case 52:
							bgCombo2List[i] = new GUIContent("劇場");
							break;
						case 53:
							bgCombo2List[i] = new GUIContent("劇場(夜)");
							break;
						case 54:
							bgCombo2List[i] = new GUIContent("ステージ");
							break;
						case 55:
							bgCombo2List[i] = new GUIContent("ステージ(ライト)");
							break;
						case 56:
							bgCombo2List[i] = new GUIContent("ステージ(オフ)");
							break;
						case 57:
							bgCombo2List[i] = new GUIContent("ステージ裏");
							break;
						case 58:
							bgCombo2List[i] = new GUIContent("トレーニングルーム");
							break;
						case 59:
							bgCombo2List[i] = new GUIContent("ロータリー");
							break;
						case 60:
							bgCombo2List[i] = new GUIContent("ロータリー(夜)");
							break;
						case 61:
							bgCombo2List[i] = new GUIContent("エントランス");
							break;
						case 62:
							bgCombo2List[i] = new GUIContent("執務室");
							break;
						case 63:
							bgCombo2List[i] = new GUIContent("執務室(椅子)");
							break;
						case 64:
							bgCombo2List[i] = new GUIContent("執務室(夜)");
							break;
						case 65:
							bgCombo2List[i] = new GUIContent("主人公部屋");
							break;
						case 66:
							bgCombo2List[i] = new GUIContent("主人公部屋(夜)");
							break;
						case 67:
							bgCombo2List[i] = new GUIContent("カフェ");
							break;
						case 68:
							bgCombo2List[i] = new GUIContent("カフェ(夜)");
							break;
						case 69:
							bgCombo2List[i] = new GUIContent("レストラン");
							break;
						case 70:
							bgCombo2List[i] = new GUIContent("レストラン(夜)");
							break;
						case 71:
							bgCombo2List[i] = new GUIContent("キッチン");
							break;
						case 72:
							bgCombo2List[i] = new GUIContent("キッチン(夜)");
							break;
						case 73:
							bgCombo2List[i] = new GUIContent("キッチン(オフ)");
							break;
						case 74:
							bgCombo2List[i] = new GUIContent("バー");
							break;
						case 75:
							bgCombo2List[i] = new GUIContent("カジノ");
							break;
						case 76:
							bgCombo2List[i] = new GUIContent("カジノミニ");
							break;
						case 77:
							bgCombo2List[i] = new GUIContent("SMクラブ");
							break;
						case 78:
							bgCombo2List[i] = new GUIContent("ソープ");
							break;
						case 79:
							bgCombo2List[i] = new GUIContent("スパ");
							break;
						case 80:
							bgCombo2List[i] = new GUIContent("スパ(夜)");
							break;
						case 81:
							bgCombo2List[i] = new GUIContent("ショッピングモール");
							break;
						case 82:
							bgCombo2List[i] = new GUIContent("ショッピングモール(夜)");
							break;
						case 83:
							bgCombo2List[i] = new GUIContent("ゲームショップ");
							break;
						case 84:
							bgCombo2List[i] = new GUIContent("ミュージックショップ");
							break;
						case 85:
							bgCombo2List[i] = new GUIContent("無垢部屋");
							break;
						case 86:
							bgCombo2List[i] = new GUIContent("無垢部屋(夜)");
							break;
						case 87:
							bgCombo2List[i] = new GUIContent("真面目部屋");
							break;
						case 88:
							bgCombo2List[i] = new GUIContent("真面目部屋(夜)");
							break;
						case 89:
							bgCombo2List[i] = new GUIContent("凜デレ部屋");
							break;
						case 90:
							bgCombo2List[i] = new GUIContent("凜デレ部屋(夜)");
							break;
						case 91:
							bgCombo2List[i] = new GUIContent("ツンデレ部屋");
							break;
						case 92:
							bgCombo2List[i] = new GUIContent("ツンデレ部屋(夜)");
							break;
						case 93:
							bgCombo2List[i] = new GUIContent("クーデレ部屋");
							break;
						case 94:
							bgCombo2List[i] = new GUIContent("クーデレ部屋(夜)");
							break;
						case 95:
							bgCombo2List[i] = new GUIContent("純真部屋");
							break;
						case 96:
							bgCombo2List[i] = new GUIContent("純真部屋(夜)");
							break;
						case 97:
							bgCombo2List[i] = new GUIContent("宿泊-ベッドルーム");
							break;
						case 98:
							bgCombo2List[i] = new GUIContent("宿泊-ベッドルーム(夜)");
							break;
						case 99:
							bgCombo2List[i] = new GUIContent("宿泊-他ベッドルーム(夜)");
							break;
						case 100:
							bgCombo2List[i] = new GUIContent("宿泊-リビング");
							break;
						case 101:
							bgCombo2List[i] = new GUIContent("宿泊-リビング(夜)");
							break;
						case 102:
							bgCombo2List[i] = new GUIContent("宿泊-トイレ");
							break;
						case 103:
							bgCombo2List[i] = new GUIContent("宿泊-トイレ(夜)");
							break;
						case 104:
							bgCombo2List[i] = new GUIContent("宿泊-洗面所");
							break;
						case 105:
							bgCombo2List[i] = new GUIContent("宿泊-洗面所(夜)");
							break;
						case 106:
							bgCombo2List[i] = new GUIContent("ランス10");
							break;
						case 107:
							bgCombo2List[i] = new GUIContent("ランス10");
							break;
						case 108:
							bgCombo2List[i] = new GUIContent("リドル");
							break;
						case 109:
							bgCombo2List[i] = new GUIContent("リドル");
							break;
						case 110:
							bgCombo2List[i] = new GUIContent("わんこ");
							break;
						case 111:
							bgCombo2List[i] = new GUIContent("わんこ");
							break;
						case 112:
							bgCombo2List[i] = new GUIContent("ラズベリー");
							break;
						case 113:
							bgCombo2List[i] = new GUIContent("ラズベリー");
							break;
						case 114:
							bgCombo2List[i] = new GUIContent("シーカフェ");
							break;
						case 115:
							bgCombo2List[i] = new GUIContent("シーカフェ");
							break;
						case 116:
							bgCombo2List[i] = new GUIContent("プール");
							break;
						case 117:
							bgCombo2List[i] = new GUIContent("プール");
							break;
						case 118:
							bgCombo2List[i] = new GUIContent("神社");
							break;
						case 119:
							bgCombo2List[i] = new GUIContent("神社");
							break;
						default:
							goto IL_1662;
					}
				IL_16E1:
					Dictionary<string, string> saveDataDic = CreativeRoomManager.GetSaveDataDic();
					if (saveDataDic != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in saveDataDic)
						{
							if (bgArray[i] == keyValuePair.Key)
							{
								bgCombo2List[i] = new GUIContent(keyValuePair.Value);
							}
						}
					}
					i++;
					continue;
				IL_1662:
					string text2 = bgArray[i];
					for (int j = 0; j < bgNameList.Count; j++)
					{
						string[] array = bgNameList[j].Split(new char[]
						{
							','
						});
						if (text2 == array[0])
						{
							text2 = array[1];
						}
					}
					bgCombo2List[i] = new GUIContent(text2);
					goto IL_16E1;
				}
				slotCombo.selectedItemIndex = 0;
				slotComboList = new GUIContent[slotArray.Length];
				i = 0;
				while (i < slotArray.Length)
				{
					string text = slotArray[i];
					if (text == null)
					{
						goto IL_1C03;
					}
					if (PartsUIArray == null)
					{
						PartsUIArray = new Dictionary<string, int>(26)
						{
							{
								"acchat",
								0
							},
							{
								"headset",
								1
							},
							{
								"wear",
								2
							},
							{
								"skirt",
								3
							},
							{
								"onepiece",
								4
							},
							{
								"mizugi",
								5
							},
							{
								"bra",
								6
							},
							{
								"panz",
								7
							},
							{
								"stkg",
								8
							},
							{
								"shoes",
								9
							},
							{
								"acckami",
								10
							},
							{
								"megane",
								11
							},
							{
								"acchead",
								12
							},
							{
								"acchana",
								13
							},
							{
								"accmimi",
								14
							},
							{
								"glove",
								15
							},
							{
								"acckubi",
								16
							},
							{
								"acckubiwa",
								17
							},
							{
								"acckamisub",
								18
							},
							{
								"accnip",
								19
							},
							{
								"accude",
								20
							},
							{
								"accheso",
								21
							},
							{
								"accashi",
								22
							},
							{
								"accsenaka",
								23
							},
							{
								"accshippo",
								24
							},
							{
								"accxxx",
								25
							}
						};
					}
					if (!PartsUIArray.TryGetValue(text, out num))
					{
						goto IL_1C03;
					}
					switch (num)
					{
						case 0:
							slotComboList[i] = new GUIContent("帽子");
							break;
						case 1:
							slotComboList[i] = new GUIContent("ヘッドドレス");
							break;
						case 2:
							slotComboList[i] = new GUIContent("トップス");
							break;
						case 3:
							slotComboList[i] = new GUIContent("ボトムス");
							break;
						case 4:
							slotComboList[i] = new GUIContent("ワンピース");
							break;
						case 5:
							slotComboList[i] = new GUIContent("水着");
							break;
						case 6:
							slotComboList[i] = new GUIContent("ブラジャー");
							break;
						case 7:
							slotComboList[i] = new GUIContent("パンツ");
							break;
						case 8:
							slotComboList[i] = new GUIContent("靴下");
							break;
						case 9:
							slotComboList[i] = new GUIContent("靴");
							break;
						case 10:
							slotComboList[i] = new GUIContent("前髪");
							break;
						case 11:
							slotComboList[i] = new GUIContent("メガネ");
							break;
						case 12:
							slotComboList[i] = new GUIContent("アイマスク");
							break;
						case 13:
							slotComboList[i] = new GUIContent("鼻");
							break;
						case 14:
							slotComboList[i] = new GUIContent("耳");
							break;
						case 15:
							slotComboList[i] = new GUIContent("手袋");
							break;
						case 16:
							slotComboList[i] = new GUIContent("ネックレス");
							break;
						case 17:
							slotComboList[i] = new GUIContent("チョーカー");
							break;
						case 18:
							slotComboList[i] = new GUIContent("リボン");
							break;
						case 19:
							slotComboList[i] = new GUIContent("乳首");
							break;
						case 20:
							slotComboList[i] = new GUIContent("腕");
							break;
						case 21:
							slotComboList[i] = new GUIContent("へそ");
							break;
						case 22:
							slotComboList[i] = new GUIContent("足首");
							break;
						case 23:
							slotComboList[i] = new GUIContent("背中");
							break;
						case 24:
							slotComboList[i] = new GUIContent("しっぽ");
							break;
						case 25:
							slotComboList[i] = new GUIContent("前穴");
							break;
						default:
							goto IL_1C03;
					}
				IL_1C1C:
					i++;
					continue;
				IL_1C03:
					slotComboList[i] = new GUIContent(slotArray[i]);
					goto IL_1C1C;
				}
				myCombo.selectedItemIndex = 0;
				myComboList = new GUIContent[myArray.Length];
				List<int> categoryIDList = PlacementData.CategoryIDList;
				myComboList[0] = new GUIContent("");
				for (i = 1; i < myArray.Length; i++)
				{
					myComboList[i] = new GUIContent(PlacementData.GetCategoryName(categoryIDList[i - 1]));
				}
				itemCombo2.selectedItemIndex = 0;
				itemCombo2List = new GUIContent[itemBArray.Length];
				i = 0;
				while (i < itemBArray.Length)
				{
					string text = itemBArray[i];
					if (text == null)
					{
						goto IL_2E56;
					}
					if (ItemUIArray == null)
					{
						ItemUIArray = new Dictionary<string, int>(108)
						{
							{
								"handitem,HandItemR_WineGlass_I_.menu",
								0
							},
							{
								"handitem,HandItemR_WineBottle_I_.menu",
								1
							},
							{
								"handitem,handitemr_racket_I_.menu",
								2
							},
							{
								"handitem,HandItemR_Hataki_I_.menu",
								3
							},
							{
								"handitem,HandItemR_Mop_I_.menu",
								4
							},
							{
								"handitem,HandItemR_Houki_I_.menu",
								5
							},
							{
								"handitem,HandItemR_Zoukin2_I_.menu",
								6
							},
							{
								"handitem,HandItemR_Chu-B_Lip_I_.menu",
								7
							},
							{
								"handitem,HandItemR_Mimikaki_I_.menu",
								8
							},
							{
								"handitem,HandItemR_Pen_I_.menu",
								9
							},
							{
								"handitem,HandItemR_Otama_I_.menu",
								10
							},
							{
								"handitem,HandItemR_Houchou_I_.menu",
								11
							},
							{
								"handitem,HandItemR_Book_I_.menu",
								12
							},
							{
								"handitem,HandItemR_Puff_I_.menu",
								13
							},
							{
								"handitem,HandItemR_Rip_I_.menu",
								14
							},
							{
								"handitem,HandItemL_Shisyuu_I_.menu",
								15
							},
							{
								"handitem,HandItemR_Hari_I_.menu",
								16
							},
							{
								"handitem,HandItemL_Sara_I_.menu",
								17
							},
							{
								"handitem,HandItemR_Sponge_I_.menu",
								18
							},
							{
								"kousoku_upper,KousokuU_TekaseOne_I_.menu",
								19
							},
							{
								"kousoku_upper,KousokuU_TekaseTwo_I_.menu",
								20
							},
							{
								"kousoku_lower,KousokuL_BathTowel_I_.menu",
								21
							},
							{
								"kousoku_upper,KousokuU_Ushirode_I_.menu",
								22
							},
							{
								"kousoku_upper,KousokuU_SMRoom_Haritsuke_I_.menu",
								23
							},
							{
								"kousoku_upper,KousokuU_SMRoom2_Haritsuke_I_.menu",
								24
							},
							{
								"handitem,HandItemL_Dance_Hataki_I_.menu",
								25
							},
							{
								"handitem,HandItemL_Dance_Mop_I_.menu",
								26
							},
							{
								"handitem,HandItemL_Dance_Zoukin_I_.menu",
								27
							},
							{
								"handitem,HandItemL_Kozara_I_.menu",
								28
							},
							{
								"handitem,HandItemR_Teacup_I_.menu",
								29
							},
							{
								"handitem,HandItemL_Teasaucer_I_.menu",
								30
							},
							{
								"handitem,HandItemR_Wholecake_I_.menu",
								31
							},
							{
								"handitem,HandItemR_Menu_I_.menu",
								32
							},
							{
								"handitem,HandItemR_Vibe_I_.menu",
								33
							},
							{
								"handitem,HandItemR_VibePink_I_.menu",
								34
							},
							{
								"handitem,HandItemR_VibeBig_I_.menu",
								35
							},
							{
								"handitem,HandItemR_AnalVibe_I_.menu",
								36
							},
							{
								"handitem,HandItemH_SoutouVibe_I_.menu",
								37
							},
							{
								"accvag,accVag_Vibe_I_.menu",
								38
							},
							{
								"accvag,accVag_VibeBig_I_.menu",
								39
							},
							{
								"accvag,accVag_VibePink_I_.menu",
								40
							},
							{
								"accanl,accAnl_AnalVibe_I_.menu",
								41
							},
							{
								"accanl,accAnl_Photo_NomalVibe_I_.menu",
								42
							},
							{
								"accanl,accAnl_Photo_VibeBig_I_.menu",
								43
							},
							{
								"accanl,accAnl_Photo_VibePink_I_.menu",
								44
							},
							{
								"handitem,HandItemR_Curry_I_.menu",
								45
							},
							{
								"handitem,HandItemL_Karaoke_Mike_I_.menu",
								46
							},
							{
								"handitem,HandItemR_Pasta_I_.menu",
								47
							},
							{
								"handitem,HandItemR_Omurice1_I_.menu",
								48
							},
							{
								"handitem,HandItemR_Omurice2_I_.menu",
								49
							},
							{
								"handitem,HandItemR_Omurice3_I_.menu",
								50
							},
							{
								"handitem,HandItemR_BeerBottle(cap_on)_I_.menu",
								51
							},
							{
								"handitem,HandItemR_BeerBottle(cap_off)_I_.menu",
								52
							},
							{
								"handitem,HandItemR_BeerGlass_I_.menu",
								53
							},
							{
								"handitem,HandItemR_Crops_Suika_I_.menu",
								54
							},
							{
								"handitem,HandItemR_Diary_I_.menu",
								55
							},
							{
								"handitem,HandItemR_DVD1_I_.menu",
								56
							},
							{
								"handitem,HandItemR_DVD2_I_.menu",
								57
							},
							{
								"handitem,HandItemR_DVD3_I_.menu",
								58
							},
							{
								"handitem,HandItemR_DVD4_I_.menu",
								59
							},
							{
								"handitem,HandItemR_DVD5_I_.menu",
								60
							},
							{
								"handitem,HandItemR_Folk_I_.menu",
								61
							},
							{
								"handitem,HandItemR_Hanabi_I_.menu",
								62
							},
							{
								"handitem,HandItemR_Jyouro_I_.menu",
								63
							},
							{
								"handitem,HandItemR_Kobin_I_.menu",
								64
							},
							{
								"handitem,HandItemR_Kushiyaki_I_.menu",
								65
							},
							{
								"handitem,HandItemR_MilkBottle(cap_on)_I_.menu",
								66
							},
							{
								"handitem,HandItemR_MilkBottle(cap_off)_I_.menu",
								67
							},
							{
								"handitem,HandItemR_Mugcup_I_.menu",
								68
							},
							{
								"handitem,HandItemR_Natumikan_I_.menu",
								69
							},
							{
								"handitem,HandItemR_Ninjin_I_.menu",
								70
							},
							{
								"handitem,HandItemR_Ochoko_I_.menu",
								71
							},
							{
								"handitem,HandItemR_Satumaimo_I_.menu",
								72
							},
							{
								"handitem,HandItemR_Scoop_I_.menu",
								73
							},
							{
								"handitem,HandItemR_Senkouhanabi_I_.menu",
								74
							},
							{
								"handitem,HandItemR_Shell_I_.menu",
								75
							},
							{
								"handitem,HandItemR_Shihen_I_.menu",
								76
							},
							{
								"handitem,HandItemR_Spoon_Curry_I_.menu",
								77
							},
							{
								"handitem,HandItemR_Spoon_Omurice_I_.menu",
								78
							},
							{
								"handitem,HandItemR_Suika_I_.menu",
								79
							},
							{
								"handitem,HandItemR_Tomato_I_.menu",
								80
							},
							{
								"handitem,HandItemR_Tomorokoshi_I_.menu",
								81
							},
							{
								"handitem,HandItemR_Tomorokoshi_yaki_I_.menu",
								82
							},
							{
								"handitem,HandItemR_TropicalGlass_I_.menu",
								83
							},
							{
								"handitem,HandItemR_Uchiwa_I_.menu",
								84
							},
							{
								"handitem,HandItemR_Ukiwa_I_.menu",
								85
							},
							{
								"handitem,HandItemR_Furaidopoteto_I_.menu",
								86
							},
							{
								"handitem,HandItemR_Ketchup_I_.menu",
								87
							},
							{
								"handitem,HandItemR_MelonSoda_I_.menu",
								88
							},
							{
								"handitem,HandItemR_Spoon_Pafe_I_.menu",
								89
							},
							{
								"handitem,HandItemR_karaoke_maracas_I_.menu",
								90
							},
							{
								"handitem,HandItemR_karaoke_sensu_I_.menu",
								91
							},
							{
								"handitem,HandItemR_cocktail_red_I_.menu",
								92
							},
							{
								"handitem,HandItemR_cocktail_blue_I_.menu",
								93
							},
							{
								"handitem,HandItemR_cocktail_yellow_I_.menu",
								94
							},
							{
								"handitem,HandItemR_pretzel_I_.menu",
								95
							},
							{
								"handitem,HandItemR_smoothie_red_I_.menu",
								96
							},
							{
								"handitem,HandItemR_smoothie_green_I_.menu",
								97
							},
							{
								"handitem,HandItemL_Etoile_Saucer_I_.menu",
								98
							},
							{
								"handitem,HandItemR_Etoile_Teacup_I_.menu",
								99
							},
							{
								"handitem,HandItemL_Katuramuki_Daikon_I_.menu",
								100
							},
							{
								"handitem,HandItemR_Usuba_Houchou_I_.menu",
								101
							},
							{
								"handitem,HandItemL_Karte_I_.menu",
								102
							},
							{
								"handitem,HandItemR_Chusyaki_I_.menu",
								103
							},
							{
								"handitem,HandItemL_Cracker_I_.menu",
								104
							},
							{
								"handitem,HandItemR_Nei_Heartful_I_.menu",
								105
							},
							{
								"handitem,HandItemR_Shaker_I_.menu",
								106
							},
							{
								"handitem,HandItemR_SmartPhone_I_.menu",
								107
							}
						};
					}
					if (!ItemUIArray.TryGetValue(text, out num))
					{
						goto IL_2E56;
					}
					switch (num)
					{
						case 0:
							itemCombo2List[i] = new GUIContent("ワイングラス");
							break;
						case 1:
							itemCombo2List[i] = new GUIContent("ワインボトル");
							break;
						case 2:
							itemCombo2List[i] = new GUIContent("ラケット");
							break;
						case 3:
							itemCombo2List[i] = new GUIContent("ハタキ");
							break;
						case 4:
							itemCombo2List[i] = new GUIContent("モップ");
							break;
						case 5:
							itemCombo2List[i] = new GUIContent("ほうき");
							break;
						case 6:
							itemCombo2List[i] = new GUIContent("雑巾");
							break;
						case 7:
							itemCombo2List[i] = new GUIContent("Chu-B Lip");
							break;
						case 8:
							itemCombo2List[i] = new GUIContent("耳かき");
							break;
						case 9:
							itemCombo2List[i] = new GUIContent("ペン");
							break;
						case 10:
							itemCombo2List[i] = new GUIContent("おたま");
							break;
						case 11:
							itemCombo2List[i] = new GUIContent("包丁");
							break;
						case 12:
							itemCombo2List[i] = new GUIContent("本");
							break;
						case 13:
							itemCombo2List[i] = new GUIContent("パフ");
							break;
						case 14:
							itemCombo2List[i] = new GUIContent("リップ");
							break;
						case 15:
							itemCombo2List[i] = new GUIContent("刺繍");
							break;
						case 16:
							itemCombo2List[i] = new GUIContent("針");
							break;
						case 17:
							itemCombo2List[i] = new GUIContent("皿");
							break;
						case 18:
							itemCombo2List[i] = new GUIContent("スポンジ");
							break;
						case 19:
							itemCombo2List[i] = new GUIContent("手枷1");
							break;
						case 20:
							itemCombo2List[i] = new GUIContent("手枷2");
							break;
						case 21:
							itemCombo2List[i] = new GUIContent("バストレイ");
							break;
						case 22:
							itemCombo2List[i] = new GUIContent("後ろ手拘束具");
							break;
						case 23:
							itemCombo2List[i] = new GUIContent("磔台");
							break;
						case 24:
							itemCombo2List[i] = new GUIContent("磔台2");
							break;
						case 25:
							itemCombo2List[i] = new GUIContent("ダンスハタキ");
							break;
						case 26:
							itemCombo2List[i] = new GUIContent("ダンスモップ");
							break;
						case 27:
							itemCombo2List[i] = new GUIContent("ダンス雑巾");
							break;
						case 28:
							itemCombo2List[i] = new GUIContent("小皿");
							break;
						case 29:
							itemCombo2List[i] = new GUIContent("ティーカップ");
							break;
						case 30:
							itemCombo2List[i] = new GUIContent("ティーソーサー");
							break;
						case 31:
							itemCombo2List[i] = new GUIContent("ホールケーキ");
							break;
						case 32:
							itemCombo2List[i] = new GUIContent("メニュー表");
							break;
						case 33:
							itemCombo2List[i] = new GUIContent("バイブ");
							break;
						case 34:
							itemCombo2List[i] = new GUIContent("ピンクバイブ");
							break;
						case 35:
							itemCombo2List[i] = new GUIContent("太バイブ");
							break;
						case 36:
							itemCombo2List[i] = new GUIContent("アナルバイブ");
							break;
						case 37:
							itemCombo2List[i] = new GUIContent("双頭バイブ");
							break;
						case 38:
							itemCombo2List[i] = new GUIContent("前：バイブ");
							break;
						case 39:
							itemCombo2List[i] = new GUIContent("前：太バイブ");
							break;
						case 40:
							itemCombo2List[i] = new GUIContent("前：ピンクバイブ");
							break;
						case 41:
							itemCombo2List[i] = new GUIContent("後：アナルバイブ");
							break;
						case 42:
							itemCombo2List[i] = new GUIContent("後：バイブ");
							break;
						case 43:
							itemCombo2List[i] = new GUIContent("後：太バイブ");
							break;
						case 44:
							itemCombo2List[i] = new GUIContent("後：ピンクバイブ");
							break;
						case 45:
							itemCombo2List[i] = new GUIContent("カレー");
							break;
						case 46:
							itemCombo2List[i] = new GUIContent("カラオケマイク");
							break;
						case 47:
							itemCombo2List[i] = new GUIContent("パスタ");
							break;
						case 48:
							itemCombo2List[i] = new GUIContent("オムライス1");
							break;
						case 49:
							itemCombo2List[i] = new GUIContent("オムライス2");
							break;
						case 50:
							itemCombo2List[i] = new GUIContent("オムライス3");
							break;
						case 51:
							itemCombo2List[i] = new GUIContent("ビールボトル");
							break;
						case 52:
							itemCombo2List[i] = new GUIContent("ビールボトル(開)");
							break;
						case 53:
							itemCombo2List[i] = new GUIContent("ビールグラス");
							break;
						case 54:
							itemCombo2List[i] = new GUIContent("スイカ");
							break;
						case 55:
							itemCombo2List[i] = new GUIContent("日記");
							break;
						case 56:
							itemCombo2List[i] = new GUIContent("DVD1");
							break;
						case 57:
							itemCombo2List[i] = new GUIContent("DVD2");
							break;
						case 58:
							itemCombo2List[i] = new GUIContent("DVD3");
							break;
						case 59:
							itemCombo2List[i] = new GUIContent("DVD4");
							break;
						case 60:
							itemCombo2List[i] = new GUIContent("DVD5");
							break;
						case 61:
							itemCombo2List[i] = new GUIContent("フォーク");
							break;
						case 62:
							itemCombo2List[i] = new GUIContent("手持ち花火");
							break;
						case 63:
							itemCombo2List[i] = new GUIContent("じょうろ");
							break;
						case 64:
							itemCombo2List[i] = new GUIContent("小瓶");
							break;
						case 65:
							itemCombo2List[i] = new GUIContent("串焼き");
							break;
						case 66:
							itemCombo2List[i] = new GUIContent("牛乳");
							break;
						case 67:
							itemCombo2List[i] = new GUIContent("牛乳(開)");
							break;
						case 68:
							itemCombo2List[i] = new GUIContent("マグカップ");
							break;
						case 69:
							itemCombo2List[i] = new GUIContent("夏みかん");
							break;
						case 70:
							itemCombo2List[i] = new GUIContent("ニンジン");
							break;
						case 71:
							itemCombo2List[i] = new GUIContent("お猪口");
							break;
						case 72:
							itemCombo2List[i] = new GUIContent("さつまいも");
							break;
						case 73:
							itemCombo2List[i] = new GUIContent("スコップ");
							break;
						case 74:
							itemCombo2List[i] = new GUIContent("線香花火");
							break;
						case 75:
							itemCombo2List[i] = new GUIContent("貝殻");
							break;
						case 76:
							itemCombo2List[i] = new GUIContent("紙片");
							break;
						case 77:
							itemCombo2List[i] = new GUIContent("スプーン(カレー)");
							break;
						case 78:
							itemCombo2List[i] = new GUIContent("スプーン(オムライス)");
							break;
						case 79:
							itemCombo2List[i] = new GUIContent("スイカ2");
							break;
						case 80:
							itemCombo2List[i] = new GUIContent("トマト");
							break;
						case 81:
							itemCombo2List[i] = new GUIContent("トウモロコシ");
							break;
						case 82:
							itemCombo2List[i] = new GUIContent("焼きトウモロコシ");
							break;
						case 83:
							itemCombo2List[i] = new GUIContent("トロピカルグラス");
							break;
						case 84:
							itemCombo2List[i] = new GUIContent("うちわ");
							break;
						case 85:
							itemCombo2List[i] = new GUIContent("浮き輪");
							break;
						case 86:
							itemCombo2List[i] = new GUIContent("フライドポテト1本");
							break;
						case 87:
							itemCombo2List[i] = new GUIContent("ケチャップ");
							break;
						case 88:
							itemCombo2List[i] = new GUIContent("メロンソーダ");
							break;
						case 89:
							itemCombo2List[i] = new GUIContent("パフェスプーン");
							break;
						case 90:
							itemCombo2List[i] = new GUIContent("マラカス");
							break;
						case 91:
							itemCombo2List[i] = new GUIContent("扇子");
							break;
						case 92:
							itemCombo2List[i] = new GUIContent("カクテル・赤");
							break;
						case 93:
							itemCombo2List[i] = new GUIContent("カクテル・青");
							break;
						case 94:
							itemCombo2List[i] = new GUIContent("カクテル・黄");
							break;
						case 95:
							itemCombo2List[i] = new GUIContent("ポッキー");
							break;
						case 96:
							itemCombo2List[i] = new GUIContent("スムージー・赤");
							break;
						case 97:
							itemCombo2List[i] = new GUIContent("スムージー・緑");
							break;
						case 98:
							itemCombo2List[i] = new GUIContent("ティーソーサー");
							break;
						case 99:
							itemCombo2List[i] = new GUIContent("ティーカップ");
							break;
						case 100:
							itemCombo2List[i] = new GUIContent("桂むき大根");
							break;
						case 101:
							itemCombo2List[i] = new GUIContent("薄刃包丁");
							break;
						case 102:
							itemCombo2List[i] = new GUIContent("カルテ");
							break;
						case 103:
							itemCombo2List[i] = new GUIContent("注射器");
							break;
						case 104:
							itemCombo2List[i] = new GUIContent("クラッカー");
							break;
						case 105:
							itemCombo2List[i] = new GUIContent("ハートフルねい人形");
							break;
						case 106:
							itemCombo2List[i] = new GUIContent("シェイカー");
							break;
						case 107:
							itemCombo2List[i] = new GUIContent("スマートフォン");
							break;
						default:
							goto IL_2E56;
					}
				IL_2E6F:
					i++;
					continue;
				IL_2E56:
					itemCombo2List[i] = new GUIContent(itemBArray[i]);
					goto IL_2E6F;
				}
				for (int k = 0; k < doguCombo.Length; k++)
				{
					doguCombo[k].selectedItemIndex = 0;
					doguComboList.Add(new GUIContent[doguBArray[k].Length]);
					for (i = 0; i < doguBArray[k].Length; i++)
					{
						string text2 = doguBArray[k][i];
						for (int j = 0; j < doguNameList.Count; j++)
						{
							string[] array = doguNameList[j].Split(new char[]
							{
								','
							});
							if (text2 == array[0])
							{
								text2 = array[1];
							}
						}
						doguComboList[k][i] = new GUIContent(text2);
					}
				}
				kankyo2InitFlg = true;
			}
			listStyle3.padding.top = GetPix(1);
			listStyle3.padding.bottom = GetPix(0);
			listStyle3.fontSize = GetPix(13);
			if (poseCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (poseGroupCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (sceneLevel == 3 || sceneLevel == 5 || isF6)
			{
				if (!isF6)
				{
					bool value = true;
					if (faceFlg || poseFlg || sceneFlg || kankyoFlg || kankyo2Flg)
					{
						value = false;
					}
					if (GUI.Toggle(new Rect((float)GetPix(2), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), value, "配置", guistyle6))
					{
						faceFlg = false;
						poseFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
						bGui = true;
						isGuiInit = true;
					}
				}
				if (!yotogiFlg)
				{
					if (GUI.Toggle(new Rect((float)GetPix(42), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), poseFlg, "操作", guistyle6))
					{
						poseFlg = true;
						faceFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
					}
				}
				if (GUI.Toggle(new Rect((float)GetPix(82), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), faceFlg, "表情", guistyle6))
				{
					faceFlg = true;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					if (!faceFlg2)
					{
						isFaceInit = true;
						faceFlg2 = true;
						maidArray[selectMaidIndex].boMabataki = false;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					isFaceInit = true;
				}
				if (GUI.Toggle(new Rect((float)GetPix(122), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyoFlg, "環境", guistyle6))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = true;
					kankyo2Flg = false;
				}
				if (GUI.Toggle(new Rect((float)GetPix(162), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyo2Flg, "環2", guistyle6))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = true;
				}
				if (!line1)
				{
					line1 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.3f));
					line2 = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 0.6f));
				}
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(200), 2f), line1);
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(200), 1f), line2);
			}
			if (isDanceStop)
			{
				isStop[selectMaidIndex] = true;
				isDanceStop = false;
			}
			if (kankyoCombo.isClickedComboButton || slotCombo.isClickedComboButton || itemCombo2.isClickedComboButton || bgCombo2.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (doguSelectFlg3)
			{
				int stockMaidCount = characterMgr.GetStockMaidCount();
				float num2 = (float)GetPix(45);
				Rect position;
				Rect viewRect;
				if (sceneLevel != 5)
				{
					position = new Rect((float)GetPix(7), (float)GetPix(108), (float)(GetPix(44) * 4 + GetPix(20)), rectWin.height * 0.825f);
					viewRect = new Rect(0f, 0f, position.width * 0.85f, num2 * (float)Math.Ceiling((double)sortList.Count / 4.0) + (float)GetPix(50));
				}
				else
				{
					position = new Rect((float)GetPix(7), (float)GetPix(108), (float)(GetPix(44) * 4 + GetPix(20)), rectWin.height * 0.825f * 0.96f);
					viewRect = new Rect(0f, 0f, position.width * 0.85f, num2 * (float)Math.Ceiling((double)sortList.Count / 4.0) + (float)GetPix(50) * 0.92f);
				}
				scrollPos = GUI.BeginScrollView(position, scrollPos, viewRect);
				for (int i = 0; i < sortList.Count; i++)
				{
					Rect position2 = new Rect((float)GetPix(i % 4 * 45), (float)GetPix(i / 4 * 45), (float)GetPix(44), (float)GetPix(44));
					if (GUI.Button(position2, "Button"))
					{
						string text3 = sortList[i].menu;
						byte[] array2 = null;
						using (AFileBase afileBase = GameUty.FileOpen(text3, null))
						{
							NDebug.Assert(afileBase.IsValid(), "メニューファイルが存在しません。 :" + text3);
							if (array2 == null)
							{
								array2 = new byte[afileBase.GetSize()];
							}
							else if (array2.Length < afileBase.GetSize())
							{
								array2 = new byte[afileBase.GetSize()];
							}
							afileBase.Read(ref array2, afileBase.GetSize());
						}
						string[] array3 = AdvancedStudioMode.ProcScriptBin(maidArray[0], array2, text3, false);
						GameObject gameObject = ImportCM2.LoadSkinMesh_R(array3[0], array3, "", maidArray[0].body0.goSlot[8], 1);
						doguBObject.Add(gameObject);
						gameObject.name = text3;
						Vector3 zero = Vector3.zero;
						Vector3 zero2 = Vector3.zero;
						zero.z = 0.4f;
						gameObject.transform.localPosition = zero;
						gameObject.transform.localRotation = Quaternion.Euler(zero2);
						doguCnt = doguBObject.Count - 1;
						gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
						gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
						gDogu[doguCnt].layer = 8;
						gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
						gDogu[doguCnt].SetActive(false);
						gDogu[doguCnt].transform.position = gameObject.transform.position;
						mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
						mDogu[doguCnt].isScale = false;
						mDogu[doguCnt].obj = gDogu[doguCnt];
						mDogu[doguCnt].maid = gameObject;
						mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
						gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
						mDogu[doguCnt].ido = 1;
					}
					GUI.DrawTexture(position2, sortList[i].tex);
				}
				GUI.EndScrollView();
			}
			GUI.enabled = true;
			if (GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(25), (float)GetPix(39), (float)GetPix(20)), doguSelectFlg1, "道具", guistyle6))
			{
				doguSelectFlg1 = true;
				doguSelectFlg2 = false;
				doguSelectFlg3 = false;
			}
			if (GUI.Toggle(new Rect((float)GetPix(56), (float)GetPix(25), (float)GetPix(50), (float)GetPix(20)), doguSelectFlg2, "ﾏｲﾙｰﾑ", guistyle6))
			{
				doguSelectFlg1 = false;
				doguSelectFlg2 = true;
				doguSelectFlg3 = false;
			}
			if (GUI.Toggle(new Rect((float)GetPix(117), (float)GetPix(25), (float)GetPix(86), (float)GetPix(20)), doguSelectFlg3, "服装･ｱｸｾｻﾘ", guistyle6))
			{
				doguSelectFlg1 = false;
				doguSelectFlg2 = false;
				doguSelectFlg3 = true;
			}
			GUI.enabled = true;
			if (doguSelectFlg3)
			{
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(78), (float)GetPix(100), (float)GetPix(25)), "服装", guistyle2);
				guistyle2.fontSize = GetPix(9);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(89), (float)GetPix(100), (float)GetPix(25)), "アクセサリ", guistyle2);
				guistyle2.fontSize = GetPix(11);
			}
			if (doguSelectFlg1)
			{
				int num3 = 58;
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3), (float)GetPix(100), (float)GetPix(24)), "家具", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 30), (float)GetPix(100), (float)GetPix(24)), "道具", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 60), (float)GetPix(100), (float)GetPix(24)), "文房具", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 90), (float)GetPix(100), (float)GetPix(24)), "グルメ", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 120), (float)GetPix(100), (float)GetPix(24)), "ドリンク", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 150), (float)GetPix(100), (float)GetPix(24)), "カジノ", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 180), (float)GetPix(100), (float)GetPix(24)), "プレイ", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 210), (float)GetPix(100), (float)GetPix(24)), "ﾊﾟｰﾃｨｸﾙ", guistyle2);
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(num3 + 240), (float)GetPix(100), (float)GetPix(24)), "その他", guistyle2);
			}
			if (doguSelectFlg2)
			{
				if (myCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				float num2 = (float)GetPix(45);
				Rect position;
				Rect viewRect;
				if (sceneLevel != 5)
				{
					position = new Rect((float)GetPix(7), (float)GetPix(92), (float)(GetPix(44) * 4 + GetPix(20)), rectWin.height * 0.85f);
					viewRect = new Rect(0f, 0f, position.width * 0.85f, num2 * (float)Math.Ceiling((double)sortListMy.Count / 4.0) + (float)GetPix(50));
				}
				else
				{
					position = new Rect((float)GetPix(7), (float)GetPix(92), (float)(GetPix(44) * 4 + GetPix(20)), rectWin.height * 0.85f * 0.96f);
					viewRect = new Rect(0f, 0f, position.width * 0.85f, num2 * (float)Math.Ceiling((double)sortListMy.Count / 4.0) + (float)GetPix(50) * 0.92f);
				}
				scrollPos = GUI.BeginScrollView(position, scrollPos, viewRect);
				for (int i = 0; i < sortListMy.Count; i++)
				{
					Rect position2 = new Rect((float)GetPix(i % 4 * 45), (float)GetPix(i / 4 * 45), (float)GetPix(44), (float)GetPix(44));
					if (GUI.Button(position2, "Button"))
					{
						createMyRoomObject(sortListMy[i].order.ToString());
					}
					GUI.DrawTexture(position2, sortListMy[i].tex);
				}
				GUI.EndScrollView();
				GUI.enabled = true;
			}
			if (doguSelectFlg2)
			{
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(60), (float)GetPix(100), (float)GetPix(24)), "カテゴリ", guistyle2);
				int num4 = myCombo.List(new Rect((float)GetPix(51), (float)GetPix(58), (float)GetPix(100), (float)GetPix(23)), myComboList[myIndex].text, myComboList, guistyle4, "box", listStyle3);
				if (num4 != myIndex)
				{
					myIndex = num4;
					sortListMy.Clear();
					if (myIndex > 0)
					{
						List<int> categoryIDList = PlacementData.CategoryIDList;
						int placementObjCategoryID = categoryIDList[myIndex - 1];
						List<PlacementData.Data> datas = PlacementData.GetDatas((PlacementData.Data datam) => datam.categoryID == placementObjCategoryID);
						scrollPos = new Vector2(0f, 0f);
						if (sortListMy.Count == 0)
						{
							foreach (PlacementData.Data data in datas)
							{
								AdvancedStudioMode.SortItemMy sortItemMy = new AdvancedStudioMode.SortItemMy();
								sortItemMy.order = data.ID;
								sortItemMy.name = data.assetName;
								sortItemMy.tex = data.GetThumbnail();
								sortListMy.Add(sortItemMy);
							}
						}
					}
				}
			}
			GUI.enabled = true;
			if (doguSelectFlg1)
			{
				bool flag = false;
				for (int l = 0; l < doguBArray.Count; l++)
				{
					if (doguCombo[l].isClickedComboButton)
					{
						flag = true;
					}
				}
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(448), (float)GetPix(100), (float)GetPix(24)), "背景(小)", guistyle2);
				if (flag || itemCombo2.isClickedComboButton || parCombo1.isClickedComboButton || doguCombo2.isClickedComboButton || parCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				bgIndexB = bgCombo2.List(new Rect((float)GetPix(51), (float)GetPix(445), (float)GetPix(100), (float)GetPix(23)), bgCombo2List[bgIndexB].text, bgCombo2List, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(445), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
				{
					GameObject gameObject = null;
					UnityEngine.Object @object = GameMain.Instance.BgMgr.CreateAssetBundle(bgArray[bgIndexB]);
					if (@object == null)
					{
						@object = Resources.Load("BG/" + bgArray[bgIndexB]);
					}
					gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
					doguBObject.Add(gameObject);
					gameObject.name = "BG_" + bgArray[bgIndexB];
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					zero.z = 0.4f;
					gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					gameObject.transform.localPosition = zero;
					gameObject.transform.localRotation = Quaternion.Euler(zero2);
					doguCnt = doguBObject.Count - 1;
					gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
					gDogu[doguCnt].layer = 8;
					gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
					gDogu[doguCnt].SetActive(false);
					gDogu[doguCnt].transform.position = gameObject.transform.position;
					mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
					mDogu[doguCnt].isScale = false;
					mDogu[doguCnt].obj = gDogu[doguCnt];
					mDogu[doguCnt].maid = gameObject;
					mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
					gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
					mDogu[doguCnt].ido = 1;
				}
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(418), (float)GetPix(100), (float)GetPix(24)), "アイテム", guistyle2);
				if (flag || parCombo1.isClickedComboButton || doguCombo2.isClickedComboButton || parCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				itemIndexB = itemCombo2.List(new Rect((float)GetPix(51), (float)GetPix(415), (float)GetPix(100), (float)GetPix(23)), itemCombo2List[itemIndexB].text, itemCombo2List, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(415), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
				{
					string text3 = itemBArray[itemIndexB].Split(new char[]
					{
						','
					})[1];
					byte[] array2 = null;
					using (AFileBase afileBase = GameUty.FileOpen(text3, null))
					{
						NDebug.Assert(afileBase.IsValid(), "メニューファイルが存在しません。 :" + text3);
						if (array2 == null)
						{
							array2 = new byte[afileBase.GetSize()];
						}
						else if (array2.Length < afileBase.GetSize())
						{
							array2 = new byte[afileBase.GetSize()];
						}
						afileBase.Read(ref array2, afileBase.GetSize());
					}
					string[] array3 = AdvancedStudioMode.ProcScriptBin(maidArray[0], array2, text3, false);
					GameObject gameObject = ImportCM2.LoadSkinMesh_R(array3[0], array3, "", maidArray[0].body0.goSlot[8], 1);
					doguBObject.Add(gameObject);
					gameObject.name = text3;
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					zero.z = 0.4f;
					int num5 = itemIndexB;
					switch (num5)
					{
						case 0:
							zero2.z = 90f;
							zero.y = 0.04f;
							goto IL_47BA;
						case 1:
							zero2.z = 90f;
							goto IL_47BA;
						case 2:
						case 3:
							break;
						case 4:
							zero2.x = 90f;
							goto IL_47BA;
						default:
							switch (num5)
							{
								case 19:
									goto IL_47BA;
								case 20:
									goto IL_47BA;
								case 21:
									goto IL_47BA;
								case 22:
									goto IL_47BA;
								case 23:
									goto IL_47BA;
								case 25:
									zero2.z = 90f;
									goto IL_47BA;
								case 26:
									zero2.z = 90f;
									goto IL_47BA;
								case 27:
									zero2.z = 90f;
									goto IL_47BA;
							}
							break;
					}
					zero2.x = -90f;
				IL_47BA:
					string text = gameObject.name;
					switch (text)
					{
						case "HandItemR_Curry_I_.menu":
						case "HandItemR_Pasta_I_.menu":
						case "HandItemR_Omurice1_I_.menu":
						case "HandItemR_Omurice2_I_.menu":
						case "HandItemR_Omurice3_I_.menu":
						case "HandItemR_Kushiyaki_I_.menu":
						case "HandItemR_Tomorokoshi_I_.menu":
						case "HandItemR_Tomorokoshi_yaki_I_.menu":
						case "HandItemR_Spoon_Curry_I_.menu":
						case "HandItemR_Spoon_Omurice_I_.menu":
						case "HandItemR_Folk_I_.menu":
						case "HandItemR_Crops_Suika_I_.menu":
						case "HandItemR_Ninjin_I_.menu":
						case "HandItemR_Satumaimo_I_.menu":
						case "HandItemL_Karaoke_Mike_I_.menu":
						case "HandItemR_Hanabi_I_.menu":
						case "HandItemR_Senkouhanabi_I_.menu":
						case "HandItemR_DVD1_I_.menu":
						case "HandItemR_DVD2_I_.menu":
						case "HandItemR_DVD3_I_.menu":
						case "HandItemR_DVD4_I_.menu":
						case "HandItemR_DVD5_I_.menu":
						case "HandItemR_Scoop_I_.menu":
						case "HandItemR_Shell_I_.menu":
						case "HandItemR_Uchiwa_I_.menu":
							zero2.z = 90f;
							break;
						case "HandItemR_Tomato_I_.menu":
						case "HandItemR_Natumikan_I_.menu":
						case "HandItemR_Kobin_I_.menu":
						case "HandItemR_Jyouro_I_.menu":
						case "HandItemR_Mugcup_I_.menu":
						case "HandItemR_Suika_I_.menu":
						case "HandItemR_BeerBottle(cap_on)_I_.menu":
						case "HandItemR_BeerBottle(cap_off)_I_.menu":
						case "HandItemR_BeerGlass_I_.menu":
						case "HandItemR_TropicalGlass_I_.menu":
						case "HandItemR_MilkBottle(cap_on)_I_.menu":
						case "HandItemR_MilkBottle(cap_off)_I_.menu":
						case "HandItemR_Ochoko_I_.menu":
						case "HandItemR_Ketchup_I_.menu":
						case "HandItemR_MelonSoda_I_.menu":
						case "HandItemR_cocktail_red_I_.menu":
						case "HandItemR_cocktail_blue_I_.menu":
						case "HandItemR_cocktail_yellow_I_.menu":
						case "HandItemR_smoothie_red_I_.menu":
						case "HandItemR_smoothie_green_I_.menu":
						case "HandItemL_Etoile_Saucer_I_.menu":
						case "HandItemR_Etoile_Teacup_I_.menu":
						case "HandItemL_Katuramuki_Daikon_I_.menu":
						case "HandItemL_Karte_I_.menu":
						case "HandItemR_Nei_Heartful_I_.menu":
						case "HandItemR_Shaker_I_.menu":
						case "HandItemR_SmartPhone_I_.menu":
							zero2.x = 0f;
							zero2.z = 90f;
							break;
					}
					gameObject.transform.localPosition = zero;
					gameObject.transform.localRotation = Quaternion.Euler(zero2);
					doguCnt = doguBObject.Count - 1;
					gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
					gDogu[doguCnt].layer = 8;
					gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
					gDogu[doguCnt].SetActive(false);
					gDogu[doguCnt].transform.position = gameObject.transform.position;
					mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
					mDogu[doguCnt].isScale = false;
					mDogu[doguCnt].obj = gDogu[doguCnt];
					mDogu[doguCnt].maid = gameObject;
					mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
					gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
					mDogu[doguCnt].ido = 1;
				}
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(388), (float)GetPix(150), (float)GetPix(24)), "大道具2", guistyle2);
				if (flag || doguCombo2.isClickedComboButton || parCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				parIndex1 = parCombo1.List(new Rect((float)GetPix(51), (float)GetPix(385), (float)GetPix(100), (float)GetPix(23)), parCombo1List[parIndex1].text, parCombo1List, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(385), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
				{
					GameObject gameObject = null;
					bool flag2 = false;
					bool flag3 = false;
					if (parArray1[parIndex1].Contains("#"))
					{
						string[] array = parArray1[parIndex1].Split(new char[]
						{
							'#'
						});
						gameObject = GameMain.Instance.BgMgr.CreateAssetBundle(array[1]);
						if (gameObject != null)
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject);
							MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
							for (int l = 0; l < componentsInChildren.Length; l++)
							{
								if (componentsInChildren[l] != null)
								{
									componentsInChildren[l].shadowCastingMode = ShadowCastingMode.Off;
								}
							}
						}
						flag2 = true;
						if (!parArray1[parIndex1].Contains("Odogu_"))
						{
							flag3 = true;
						}
						doguBObject.Add(gameObject);
					}
					else if (!parArray1[parIndex1].StartsWith("mirror") && parArray1[parIndex1].IndexOf(":") < 0)
					{
						UnityEngine.Object @object = Resources.Load("Prefab/" + parArray1[parIndex1]);
						gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
						doguBObject.Add(gameObject);
					}
					else if (parArray1[parIndex1].StartsWith("mirror"))
					{
						Material material = new Material(Shader.Find("Mirror"));
						GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
						gameObject2.GetComponent<Renderer>().material = material;
						gameObject2.AddComponent<MirrorReflection2>();
						MirrorReflection2 component = gameObject2.GetComponent<MirrorReflection2>();
						component.m_TextureSize = 2048;
						component.m_ClipPlaneOffset = 0.07f;
						gameObject2.GetComponent<Renderer>().enabled = true;
						gameObject = gameObject2;
						doguBObject.Add(gameObject);
					}
					else
					{
						string[] array = parArray1[parIndex1].Split(new char[]
						{
							':'
						});
						UnityEngine.Object @object = GameMain.Instance.BgMgr.CreateAssetBundle(array[0]);
						if (@object == null)
						{
							@object = Resources.Load("BG/" + array[0]);
						}
						GameObject gameObject3 = UnityEngine.Object.Instantiate(@object) as GameObject;
						int num6 = 0;
						int num7 = 0;
						int.TryParse(array[1], out num7);
						foreach (object obj in gameObject3.transform)
						{
							Transform transform = (Transform)obj;
							if (num7 == num6)
							{
								gameObject = transform.gameObject;
								break;
							}
							num6++;
						}
						doguBObject.Add(gameObject);
						gameObject.transform.parent = null;
						UnityEngine.Object.Destroy(gameObject3);
						gameObject3.SetActive(false);
					}
					gameObject.name = parArray1[parIndex1];
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					switch (parIndex1)
					{
						case 0:
							zero.z = -0.6f;
							zero.y = 0.96f;
							zero2.z = 180f;
							zero2.x = -90f;
							gameObject.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
							break;
						case 1:
							zero.z = -0.6f;
							zero.y = 0.96f;
							zero2.z = 180f;
							zero2.x = -90f;
							gameObject.transform.localScale = new Vector3(0.1f, 0.4f, 0.2f);
							break;
						case 2:
							zero.z = -0.6f;
							zero.y = 0.85f;
							zero2.z = 180f;
							zero2.x = -90f;
							gameObject.transform.localScale = new Vector3(0.03f, 0.18f, 0.124f);
							break;
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
						case 16:
						case 17:
							zero.z = 0.5f;
							zero2.x = -90f;
							break;
					}
					if (flag2)
					{
						zero.z = 0.4f;
						if (flag3)
						{
							zero2.x = -90f;
						}
					}
					if (gameObject.name == "Salon_Entrance:3" || gameObject.name == "Salon_Entrance:4" || gameObject.name == "Salon_Entrance:1" || gameObject.name == "Salon_Entrance:2" || gameObject.name == "Salon_Entrance:0" || gameObject.name == "Shitsumu:23" || gameObject.name == "Shitsumu_Night:23")
					{
						zero.z = 0.5f;
						zero2.x = -90f;
					}
					if (gameObject.name == "Pool:26")
					{
						zero.z = 0.5f;
						zero2.x = -90f;
						zero2.z = 90f;
						zero.y = 0.15f;
					}
					if (gameObject.name == "Particle/pstarY_act2")
					{
						zero2.y = 90f;
					}
					gameObject.transform.localPosition = zero;
					gameObject.transform.localRotation = Quaternion.Euler(zero2);
					doguCnt = doguBObject.Count - 1;
					gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
					gDogu[doguCnt].layer = 8;
					gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
					gDogu[doguCnt].SetActive(false);
					gDogu[doguCnt].transform.position = gameObject.transform.position;
					mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
					mDogu[doguCnt].obj = gDogu[doguCnt];
					mDogu[doguCnt].maid = gameObject;
					mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
					gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
					mDogu[doguCnt].ido = 1;
					mDogu[doguCnt].isScale = false;
					if (gameObject.name == "Particle/pLineY")
					{
						mDogu[doguCnt].count = 180;
						gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
					}
					if (gameObject.name == "Particle/pLineP02")
					{
						mDogu[doguCnt].count = 115;
						gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
					}
					if (gameObject.name == "Particle/pLine_act2")
					{
						mDogu[doguCnt].count = 90;
						gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
					}
					if (gameObject.name == "Particle/pHeart01")
					{
						mDogu[doguCnt].count = 77;
					}
					if (parIndex1 < 3)
					{
						mDogu[doguCnt].isScale = true;
						mDogu[doguCnt].isScale2 = true;
						mDogu[doguCnt].scale2 = gameObject.transform.localScale;
						if (parIndex1 == 0)
						{
							mDogu[doguCnt].scale = new Vector3(gameObject.transform.localScale.x * 5f, gameObject.transform.localScale.y * 5f, gameObject.transform.localScale.z * 5f);
						}
						if (parIndex1 == 1)
						{
							mDogu[doguCnt].scale = new Vector3(gameObject.transform.localScale.x * 10f, gameObject.transform.localScale.y * 10f, gameObject.transform.localScale.z * 10f);
						}
						if (parIndex1 == 2)
						{
							mDogu[doguCnt].scale = new Vector3(gameObject.transform.localScale.x * 33f, gameObject.transform.localScale.y * 33f, gameObject.transform.localScale.z * 33f);
						}
					}
					if (gameObject.GetComponent<Collider>() != null)
					{
						gameObject.GetComponent<Collider>().enabled = false;
					}
				}
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(358), (float)GetPix(100), (float)GetPix(24)), "大道具1", guistyle2);
				if (flag || parCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				doguB2Index = doguCombo2.List(new Rect((float)GetPix(51), (float)GetPix(355), (float)GetPix(100), (float)GetPix(23)), doguCombo2List[doguB2Index].text, doguCombo2List, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(355), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
				{
					GameObject gameObject = null;
					if (!doguB1Array[doguB2Index].StartsWith("mirror"))
					{
						UnityEngine.Object @object;
						if (doguB1Array[doguB2Index].StartsWith("BG"))
						{
							string text2 = doguB1Array[doguB2Index].Replace("BG", "");
							@object = GameMain.Instance.BgMgr.CreateAssetBundle(text2);
							if (@object == null)
							{
								@object = Resources.Load("BG/" + text2);
							}
						}
						else
						{
							@object = Resources.Load("Prefab/" + doguB1Array[doguB2Index]);
						}
						gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
						doguBObject.Add(gameObject);
					}
					else
					{
						Material material = new Material(Shader.Find("Mirror"));
						GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
						gameObject2.transform.localScale = new Vector3(0.3f, 1f, 0.3f);
						gameObject2.GetComponent<Renderer>().material = material;
						gameObject2.AddComponent<MirrorReflection2>();
						MirrorReflection2 component = gameObject2.GetComponent<MirrorReflection2>();
						component.m_TextureSize = 2048;
						component.m_ClipPlaneOffset = 0f;
						gameObject2.GetComponent<Renderer>().enabled = true;
						gameObject = gameObject2;
						doguBObject.Add(gameObject);
					}
					gameObject.name = doguB1Array[doguB2Index];
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					string text = gameObject.name;
					switch (text)
					{
						case "Odogu_XmasTreeMini_photo_ver":
							zero.z = 0.6f;
							gameObject.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
							foreach (object obj2 in gameObject.transform)
							{
								Transform transform2 = (Transform)obj2;
								if (transform2.GetComponent<Collider>() != null)
								{
									transform2.GetComponent<Collider>().enabled = false;
								}
							}
							goto IL_6AE3;
						case "Odogu_KadomatsuMini_photo_ver":
							zero.z = 0.6f;
							gameObject.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
							foreach (object obj3 in gameObject.transform)
							{
								Transform transform2 = (Transform)obj3;
								if (transform2.GetComponent<Collider>() != null)
								{
									transform2.GetComponent<Collider>().enabled = false;
								}
							}
							goto IL_6AE3;
						case "Odogu_ClassRoomDesk":
							zero.z = 0.5f;
							zero2.x = -90f;
							goto IL_6AE3;
						case "Odogu_SimpleTable":
							zero.z = 0.5f;
							zero2.x = -90f;
							goto IL_6AE3;
						case "Odogu_DildoBox":
							zero.z = 0.5f;
							zero2.x = -90f;
							goto IL_6AE3;
						case "PlayAreaOut":
							zero.z = 0.5f;
							zero.y = 0.2f;
							goto IL_6AE3;
						case "Odogu_Dresser_photo_ver":
							GameObject.Find("Prim.00000000").GetComponent<Collider>().enabled = false;
							GameObject.Find("Prim.00000001").GetComponent<Collider>().enabled = false;
							GameObject.Find("Prim.00000002").GetComponent<Collider>().enabled = false;
							GameObject.Find("Prim.00000004").GetComponent<Collider>().enabled = false;
							goto IL_6AE3;
						case "BGodogu_bbqgrill":
						case "BGodogu_bucket":
						case "BGodogu_coolerbox":
						case "BGodogu_game_darts":
						case "BGodogu_game_dartsboard":
						case "BGodogu_nabe_huta":
						case "BGodogu_nabe_water":
						case "BGodogu_natumikan":
						case "BGodogu_rb_chair":
						case "BGodogu_rb_duck":
						case "BGodogu_rb_obon":
						case "BGodogu_rb_tokkuri":
						case "BGodogu_saracorn":
						case "BGodogu_saraimo":
						case "BGodogu_saratomato":
						case "BGodogu_sunanoshiro":
						case "BGodogu_sunanoyama":
						case "BGodogu_tsutsuhanabi":
						case "BGodogu_ukiwa":
						case "BGodogu_vf_crops_corn":
						case "BGodogu_vf_crops_gekkabijin":
						case "BGodogu_vf_crops_gekkabijinflower":
						case "BGodogu_vf_crops_himawari":
						case "BGodogu_vf_crops_natsumikan":
						case "BGodogu_vf_crops_suika":
						case "BGodogu_vf_crops_zakuro":
						case "BGodogu_villa_table":
						case "BGodogu_villa_tvrimocon":
						case "BGodogu_villabr_sideboard":
						case "BGOdogu_Game_Nei_USB":
						case "BGOdogu_Game_Wanage":
						case "BGOdogu_Game_Wa":
						case "BGodogu_pafe":
						case "BGodogu_furaidopoteto":
						case "BGodogu_karaoketable":
						case "BGodogu_omuriceh":
						case "BGodogu_omuricekao1":
						case "BGodogu_omuricekao2":
						case "BGodogu_omuriceoppai":
						case "BGodogu_kakigori":
						case "BGodogu_pretzel_sara":
						case "BGodogu_karaoke_box":
						case "BGOdogu_alicesoft_bluehoney":
						case "BGOdogu_alicesoft_brownhoney":
						case "BGOdogu_alicesoft_greenhoney":
						case "BGOdogu_alicesoft_redhoney":
						case "BGOdogu_sp001_beercan":
						case "BGOdogu_sp001_beerjug":
						case "BGOdogu_sp001_cake":
						case "BGOdogu_sp001_food":
						case "BGOdogu_sp001_yakisoba":
						case "BGOdogu_denkigai2018s_beachball_blue":
						case "BGOdogu_denkigai2018s_beachball_green":
						case "BGOdogu_denkigai2018s_beachball_red":
						case "BGOdogu_denkigai2018s_beachball_yellow":
						case "BGOdogu_denkigai2018s_coneice_chocomint":
						case "BGOdogu_denkigai2018s_coneice_strawberry":
						case "BGOdogu_denkigai2018s_coneice_vanilla":
						case "BGOdogu_denkigai2018s_melondish":
						case "BGOdogu_denkigai2018s_syatifloot":
						case "BGOdogu_denkigai2018s_toropicalicetea":
						case "BGOdogu_sp002_oharaibou":
						case "BGOdogu_sp002_omamori":
						case "BGOdogu_sp002_susuki":
						case "BGOdogu_sp002_takebouki":
						case "BGOdogu_sp002_tukimidango":
						case "BGOdogu_sp002_waraningyou":
						case "BGOdogu_sp002_waraningyou_gosunkugi":
						case "Odogu_StandMike":
						case "Odogu_StandMikeBase":
						case "Odogu_HeroineChair_muku":
						case "Odogu_HeroineChair_mazime":
						case "Odogu_HeroineChair_rindere":
						case "Odogu_HeroineChair_tsumdere":
						case "Odogu_HeroineChair_cooldere":
						case "Odogu_HeroineChair_junshin":
						case "Odogu_TabletPC":
						case "Odogu_Styluspen_black":
						case "Odogu_Styluspen_white":
						case "Odogu_Styluspen_red":
						case "Odogu_Styluspen_blue":
						case "Odogu_Styluspen_yellow":
						case "Odogu_Styluspen_green":
						case "Odogu_Omurice1":
						case "Odogu_Omurice3":
						case "Odogu_OmuriceH":
						case "Odogu_OmuriceKao1":
						case "Odogu_OmuriceKao2":
						case "Odogu_OmuriceOppai":
						case "Odogu_AcquaPazza":
						case "Odogu_Sandwich":
						case "Odogu_vichyssoise":
						case "Odogu_BirthdayCake":
						case "Odogu_Shortcake":
						case "Odogu_MontBlanc":
						case "Odogu_Pafe":
						case "Odogu_Smoothie_Red":
						case "Odogu_Smoothie_Green":
						case "Odogu_Cocktail_Red":
						case "Odogu_Cocktail_Blue":
						case "Odogu_Cocktail_Yellow":
						case "Odogu_Coffiecup":
						case "Odogu_WineBottle(cap_off)":
						case "Odogu_WineBottle(cap_on)":
						case "Odogu_Jyouro":
						case "Odogu_Planter_Red":
						case "Odogu_Planter_Lightblue":
						case "Odogu_MariGold":
						case "Odogu_CasinoChip_10":
						case "Odogu_CasinoChip_100":
						case "Odogu_CasinoChip_1000":
						case "Odogu_CardShooter":
						case "Odogu_CardsDeck":
						case "Odogu_Card_s1":
						case "Odogu_Card_s2":
						case "Odogu_Card_s3":
						case "Odogu_Card_s4":
						case "Odogu_Card_s5":
						case "Odogu_Card_s6":
						case "Odogu_Card_s7":
						case "Odogu_Card_s8":
						case "Odogu_Card_s9":
						case "Odogu_Card_s10":
						case "Odogu_Card_s11":
						case "Odogu_Card_s12":
						case "Odogu_Card_s13":
						case "Odogu_Card_h1":
						case "Odogu_Card_h2":
						case "Odogu_Card_h3":
						case "Odogu_Card_h4":
						case "Odogu_Card_h5":
						case "Odogu_Card_h6":
						case "Odogu_Card_h7":
						case "Odogu_Card_h8":
						case "Odogu_Card_h9":
						case "Odogu_Card_h10":
						case "Odogu_Card_h11":
						case "Odogu_Card_h12":
						case "Odogu_Card_h13":
						case "Odogu_Card_d1":
						case "Odogu_Card_d2":
						case "Odogu_Card_d3":
						case "Odogu_Card_d4":
						case "Odogu_Card_d5":
						case "Odogu_Card_d6":
						case "Odogu_Card_d7":
						case "Odogu_Card_d8":
						case "Odogu_Card_d9":
						case "Odogu_Card_d10":
						case "Odogu_Card_d11":
						case "Odogu_Card_d12":
						case "Odogu_Card_d13":
						case "Odogu_Card_c1":
						case "Odogu_Card_c2":
						case "Odogu_Card_c3":
						case "Odogu_Card_c4":
						case "Odogu_Card_c5":
						case "Odogu_Card_c6":
						case "Odogu_Card_c7":
						case "Odogu_Card_c8":
						case "Odogu_Card_c9":
						case "Odogu_Card_c10":
						case "Odogu_Card_c11":
						case "Odogu_Card_c12":
						case "Odogu_Card_c13":
						case "Odogu_Card_joker":
							zero.z = 0.5f;
							zero2.x = -90f;
							goto IL_6AE3;
					}
					zero.z = 0.5f;
					if (gameObject.name.StartsWith("Odogu_"))
					{
						foreach (object obj4 in gameObject.transform)
						{
							Transform transform2 = (Transform)obj4;
							if (transform2.GetComponent<Collider>() != null)
							{
								transform2.GetComponent<Collider>().enabled = false;
							}
						}
					}
					else if (gameObject.GetComponent<Collider>() != null)
					{
						gameObject.GetComponent<Collider>().enabled = false;
					}
				IL_6AE3:
					gameObject.transform.localPosition = zero;
					gameObject.transform.localRotation = Quaternion.Euler(zero2);
					doguCnt = doguBObject.Count - 1;
					gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
					gDogu[doguCnt].layer = 8;
					gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
					gDogu[doguCnt].SetActive(false);
					gDogu[doguCnt].transform.position = gameObject.transform.position;
					mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
					mDogu[doguCnt].isScale = false;
					mDogu[doguCnt].obj = gDogu[doguCnt];
					mDogu[doguCnt].maid = gameObject;
					mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
					gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
					mDogu[doguCnt].ido = 1;
					if (doguB2Index == 6 || doguB2Index == 7)
					{
						mDogu[doguCnt].isScale2 = true;
						mDogu[doguCnt].scale2 = gameObject.transform.localScale;
					}
				}
				GUI.enabled = true;
				if (bgmCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(3), (float)GetPix(328), (float)GetPix(150), (float)GetPix(24)), "デスク", guistyle2);
				if (flag)
				{
					GUI.enabled = false;
				}
				parIndex = parCombo.List(new Rect((float)GetPix(51), (float)GetPix(325), (float)GetPix(100), (float)GetPix(23)), parComboList[parIndex].text, parComboList, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(325), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
				{
					GameObject gameObject = null;
					if (parArray[parIndex].Contains("#"))
					{
						string[] array = parArray[parIndex].Split(new char[]
						{
							'#'
						});
						gameObject = GameMain.Instance.BgMgr.CreateAssetBundle(array[1]);
						if (gameObject != null)
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(gameObject);
							MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
							for (int l = 0; l < componentsInChildren.Length; l++)
							{
								if (componentsInChildren[l] != null)
								{
									componentsInChildren[l].shadowCastingMode = ShadowCastingMode.Off;
								}
							}
						}
						doguBObject.Add(gameObject);
					}
					else if (!parArray[parIndex].StartsWith("mirror") && parArray[parIndex].IndexOf(":") < 0)
					{
						UnityEngine.Object @object = Resources.Load("Prefab/" + parArray[parIndex]);
						gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
						doguBObject.Add(gameObject);
					}
					else if (parArray[parIndex].StartsWith("mirror"))
					{
						Material material = new Material(Shader.Find("Mirror"));
						GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
						gameObject2.GetComponent<Renderer>().material = material;
						gameObject2.AddComponent<MirrorReflection2>();
						MirrorReflection2 component = gameObject2.GetComponent<MirrorReflection2>();
						component.m_TextureSize = 2048;
						component.m_ClipPlaneOffset = 0.07f;
						gameObject2.GetComponent<Renderer>().enabled = true;
						gameObject = gameObject2;
						doguBObject.Add(gameObject);
					}
					else
					{
						string[] array = parArray[parIndex].Split(new char[]
						{
							':'
						});
						UnityEngine.Object @object = GameMain.Instance.BgMgr.CreateAssetBundle(array[0]);
						if (@object == null)
						{
							@object = Resources.Load("BG/" + array[0]);
						}
						GameObject gameObject3 = UnityEngine.Object.Instantiate(@object) as GameObject;
						int num6 = 0;
						int num7 = 0;
						int.TryParse(array[1], out num7);
						foreach (object obj5 in gameObject3.transform)
						{
							Transform transform = (Transform)obj5;
							if (num7 == num6)
							{
								gameObject = transform.gameObject;
								break;
							}
							num6++;
						}
						doguBObject.Add(gameObject);
						gameObject.transform.parent = null;
						UnityEngine.Object.Destroy(gameObject3);
						gameObject3.SetActive(false);
					}
					gameObject.name = parArray[parIndex];
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					zero.z = 0.5f;
					zero2.x = -90f;
					gameObject.transform.localPosition = zero;
					gameObject.transform.localRotation = Quaternion.Euler(zero2);
					doguCnt = doguBObject.Count - 1;
					gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
					gDogu[doguCnt].layer = 8;
					gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
					gDogu[doguCnt].SetActive(false);
					gDogu[doguCnt].transform.position = gameObject.transform.position;
					mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
					mDogu[doguCnt].obj = gDogu[doguCnt];
					mDogu[doguCnt].maid = gameObject;
					mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
					gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
					mDogu[doguCnt].ido = 1;
					mDogu[doguCnt].isScale = false;
					if (gameObject.GetComponent<Collider>() != null)
					{
						gameObject.GetComponent<Collider>().enabled = false;
					}
				}
				GUI.enabled = true;
				if (bgmCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				int k = doguBArray.Count - 1;
				while (0 <= k)
				{
					int num8 = doguBIndex[k];
					string text4 = doguComboList[k][doguBIndex[k]].text;
					if (flag)
					{
						GUI.enabled = false;
					}
					if (doguCombo[k].isClickedComboButton)
					{
						GUI.enabled = true;
						flag = false;
					}
					doguBIndex[k] = doguCombo[k].List(new Rect((float)GetPix(51), (float)GetPix(55 + k * 30), (float)GetPix(100), (float)GetPix(23)), doguComboList[k][doguBIndex[k]].text, doguComboList[k], guistyle4, "box", listStyle3);
					GUI.enabled = true;
					if (GUI.Button(new Rect((float)GetPix(156), (float)GetPix(55 + k * 30), (float)GetPix(38), (float)GetPix(23)), "追加", guistyle3))
					{
						GameObject gameObject = null;
						string text2;
						if (!doguBArray[k][doguBIndex[k]].StartsWith("mirror"))
						{
							UnityEngine.Object @object;
							if (doguBArray[k][doguBIndex[k]].StartsWith("BG"))
							{
								text2 = doguBArray[k][doguBIndex[k]].Replace("BG", "");
								@object = GameMain.Instance.BgMgr.CreateAssetBundle(text2);
								if (@object == null)
								{
									@object = Resources.Load("BG/" + text2);
								}
							}
							else
							{
								@object = Resources.Load("Prefab/" + doguBArray[k][doguBIndex[k]]);
								if (@object == null)
								{
									GameObject original = GameMain.Instance.BgMgr.CreateAssetBundle(doguBArray[k][doguBIndex[k]]);
									gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
								}
							}
							if (gameObject == null)
							{
								gameObject = (UnityEngine.Object.Instantiate(@object) as GameObject);
							}
							doguBObject.Add(gameObject);
						}
						else
						{
							Material material = new Material(Shader.Find("Mirror"));
							GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
							gameObject2.transform.localScale = new Vector3(0.3f, 1f, 0.3f);
							gameObject2.GetComponent<Renderer>().material = material;
							gameObject2.AddComponent<MirrorReflection2>();
							MirrorReflection2 component = gameObject2.GetComponent<MirrorReflection2>();
							component.m_TextureSize = 2048;
							component.m_ClipPlaneOffset = 0f;
							gameObject2.GetComponent<Renderer>().enabled = true;
							gameObject = gameObject2;
							doguBObject.Add(gameObject);
						}
						gameObject.name = doguBArray[k][doguBIndex[k]];
						Vector3 zero = Vector3.zero;
						Vector3 zero2 = Vector3.zero;
						string text = gameObject.name;
						if (text == null)
						{
							goto IL_8500;
						}
						if (OdoguUIArray == null)
						{
							OdoguUIArray = new Dictionary<string, int>(173)
							{
								{
									"Odogu_XmasTreeMini_photo_ver",
									0
								},
								{
									"Odogu_KadomatsuMini_photo_ver",
									1
								},
								{
									"Odogu_ClassRoomDesk",
									2
								},
								{
									"Odogu_SimpleTable",
									3
								},
								{
									"Odogu_DildoBox",
									4
								},
								{
									"PlayAreaOut",
									5
								},
								{
									"Odogu_Dresser_photo_ver",
									6
								},
								{
									"BGodogu_bbqgrill",
									7
								},
								{
									"BGodogu_bucket",
									8
								},
								{
									"BGodogu_coolerbox",
									9
								},
								{
									"BGodogu_game_darts",
									10
								},
								{
									"BGodogu_game_dartsboard",
									11
								},
								{
									"BGodogu_nabe_huta",
									12
								},
								{
									"BGodogu_nabe_water",
									13
								},
								{
									"BGodogu_natumikan",
									14
								},
								{
									"BGodogu_rb_chair",
									15
								},
								{
									"BGodogu_rb_duck",
									16
								},
								{
									"BGodogu_rb_obon",
									17
								},
								{
									"BGodogu_rb_tokkuri",
									18
								},
								{
									"BGodogu_saracorn",
									19
								},
								{
									"BGodogu_saraimo",
									20
								},
								{
									"BGodogu_saratomato",
									21
								},
								{
									"BGodogu_sunanoshiro",
									22
								},
								{
									"BGodogu_sunanoyama",
									23
								},
								{
									"BGodogu_tsutsuhanabi",
									24
								},
								{
									"BGodogu_ukiwa",
									25
								},
								{
									"BGodogu_vf_crops_corn",
									26
								},
								{
									"BGodogu_vf_crops_gekkabijin",
									27
								},
								{
									"BGodogu_vf_crops_gekkabijinflower",
									28
								},
								{
									"BGodogu_vf_crops_himawari",
									29
								},
								{
									"BGodogu_vf_crops_natsumikan",
									30
								},
								{
									"BGodogu_vf_crops_suika",
									31
								},
								{
									"BGodogu_vf_crops_zakuro",
									32
								},
								{
									"BGodogu_villa_table",
									33
								},
								{
									"BGodogu_villa_tvrimocon",
									34
								},
								{
									"BGodogu_villabr_sideboard",
									35
								},
								{
									"BGOdogu_Game_Nei_USB",
									36
								},
								{
									"BGOdogu_Game_Wanage",
									37
								},
								{
									"BGOdogu_Game_Wa",
									38
								},
								{
									"BGodogu_pafe",
									39
								},
								{
									"BGodogu_furaidopoteto",
									40
								},
								{
									"BGodogu_karaoketable",
									41
								},
								{
									"BGodogu_omuriceh",
									42
								},
								{
									"BGodogu_omuricekao1",
									43
								},
								{
									"BGodogu_omuricekao2",
									44
								},
								{
									"BGodogu_omuriceoppai",
									45
								},
								{
									"BGodogu_kakigori",
									46
								},
								{
									"BGodogu_pretzel_sara",
									47
								},
								{
									"BGodogu_karaoke_box",
									48
								},
								{
									"BGOdogu_alicesoft_bluehoney",
									49
								},
								{
									"BGOdogu_alicesoft_brownhoney",
									50
								},
								{
									"BGOdogu_alicesoft_greenhoney",
									51
								},
								{
									"BGOdogu_alicesoft_redhoney",
									52
								},
								{
									"BGOdogu_sp001_beercan",
									53
								},
								{
									"BGOdogu_sp001_beerjug",
									54
								},
								{
									"BGOdogu_sp001_cake",
									55
								},
								{
									"BGOdogu_sp001_food",
									56
								},
								{
									"BGOdogu_sp001_yakisoba",
									57
								},
								{
									"BGOdogu_denkigai2018s_beachball_blue",
									58
								},
								{
									"BGOdogu_denkigai2018s_beachball_green",
									59
								},
								{
									"BGOdogu_denkigai2018s_beachball_red",
									60
								},
								{
									"BGOdogu_denkigai2018s_beachball_yellow",
									61
								},
								{
									"BGOdogu_denkigai2018s_coneice_chocomint",
									62
								},
								{
									"BGOdogu_denkigai2018s_coneice_strawberry",
									63
								},
								{
									"BGOdogu_denkigai2018s_coneice_vanilla",
									64
								},
								{
									"BGOdogu_denkigai2018s_melondish",
									65
								},
								{
									"BGOdogu_denkigai2018s_syatifloot",
									66
								},
								{
									"BGOdogu_denkigai2018s_toropicalicetea",
									67
								},
								{
									"BGOdogu_sp002_oharaibou",
									68
								},
								{
									"BGOdogu_sp002_omamori",
									69
								},
								{
									"BGOdogu_sp002_susuki",
									70
								},
								{
									"BGOdogu_sp002_takebouki",
									71
								},
								{
									"BGOdogu_sp002_tukimidango",
									72
								},
								{
									"BGOdogu_sp002_waraningyou",
									73
								},
								{
									"BGOdogu_sp002_waraningyou_gosunkugi",
									74
								},
								{
									"Odogu_StandMike",
									75
								},
								{
									"Odogu_StandMikeBase",
									76
								},
								{
									"Odogu_HeroineChair_muku",
									77
								},
								{
									"Odogu_HeroineChair_mazime",
									78
								},
								{
									"Odogu_HeroineChair_rindere",
									79
								},
								{
									"Odogu_HeroineChair_tsumdere",
									80
								},
								{
									"Odogu_HeroineChair_cooldere",
									81
								},
								{
									"Odogu_HeroineChair_junshin",
									82
								},
								{
									"Odogu_TabletPC",
									83
								},
								{
									"Odogu_Styluspen_black",
									84
								},
								{
									"Odogu_Styluspen_white",
									85
								},
								{
									"Odogu_Styluspen_red",
									86
								},
								{
									"Odogu_Styluspen_blue",
									87
								},
								{
									"Odogu_Styluspen_yellow",
									88
								},
								{
									"Odogu_Styluspen_green",
									89
								},
								{
									"Odogu_Omurice1",
									90
								},
								{
									"Odogu_Omurice3",
									91
								},
								{
									"Odogu_OmuriceH",
									92
								},
								{
									"Odogu_OmuriceKao1",
									93
								},
								{
									"Odogu_OmuriceKao2",
									94
								},
								{
									"Odogu_OmuriceOppai",
									95
								},
								{
									"Odogu_AcquaPazza",
									96
								},
								{
									"Odogu_Sandwich",
									97
								},
								{
									"Odogu_vichyssoise",
									98
								},
								{
									"Odogu_BirthdayCake",
									99
								},
								{
									"Odogu_Shortcake",
									100
								},
								{
									"Odogu_MontBlanc",
									101
								},
								{
									"Odogu_Pafe",
									102
								},
								{
									"Odogu_Smoothie_Red",
									103
								},
								{
									"Odogu_Smoothie_Green",
									104
								},
								{
									"Odogu_Cocktail_Red",
									105
								},
								{
									"Odogu_Cocktail_Blue",
									106
								},
								{
									"Odogu_Cocktail_Yellow",
									107
								},
								{
									"Odogu_Coffiecup",
									108
								},
								{
									"Odogu_WineBottle(cap_off)",
									109
								},
								{
									"Odogu_WineBottle(cap_on)",
									110
								},
								{
									"Odogu_Jyouro",
									111
								},
								{
									"Odogu_Planter_Red",
									112
								},
								{
									"Odogu_Planter_Lightblue",
									113
								},
								{
									"Odogu_MariGold",
									114
								},
								{
									"Odogu_CasinoChip_10",
									115
								},
								{
									"Odogu_CasinoChip_100",
									116
								},
								{
									"Odogu_CasinoChip_1000",
									117
								},
								{
									"Odogu_CardShooter",
									118
								},
								{
									"Odogu_CardsDeck",
									119
								},
								{
									"Odogu_Card_s1",
									120
								},
								{
									"Odogu_Card_s2",
									121
								},
								{
									"Odogu_Card_s3",
									122
								},
								{
									"Odogu_Card_s4",
									123
								},
								{
									"Odogu_Card_s5",
									124
								},
								{
									"Odogu_Card_s6",
									125
								},
								{
									"Odogu_Card_s7",
									126
								},
								{
									"Odogu_Card_s8",
									127
								},
								{
									"Odogu_Card_s9",
									128
								},
								{
									"Odogu_Card_s10",
									129
								},
								{
									"Odogu_Card_s11",
									130
								},
								{
									"Odogu_Card_s12",
									131
								},
								{
									"Odogu_Card_s13",
									132
								},
								{
									"Odogu_Card_h1",
									133
								},
								{
									"Odogu_Card_h2",
									134
								},
								{
									"Odogu_Card_h3",
									135
								},
								{
									"Odogu_Card_h4",
									136
								},
								{
									"Odogu_Card_h5",
									137
								},
								{
									"Odogu_Card_h6",
									138
								},
								{
									"Odogu_Card_h7",
									139
								},
								{
									"Odogu_Card_h8",
									140
								},
								{
									"Odogu_Card_h9",
									141
								},
								{
									"Odogu_Card_h10",
									142
								},
								{
									"Odogu_Card_h11",
									143
								},
								{
									"Odogu_Card_h12",
									144
								},
								{
									"Odogu_Card_h13",
									145
								},
								{
									"Odogu_Card_d1",
									146
								},
								{
									"Odogu_Card_d2",
									147
								},
								{
									"Odogu_Card_d3",
									148
								},
								{
									"Odogu_Card_d4",
									149
								},
								{
									"Odogu_Card_d5",
									150
								},
								{
									"Odogu_Card_d6",
									151
								},
								{
									"Odogu_Card_d7",
									152
								},
								{
									"Odogu_Card_d8",
									153
								},
								{
									"Odogu_Card_d9",
									154
								},
								{
									"Odogu_Card_d10",
									155
								},
								{
									"Odogu_Card_d11",
									156
								},
								{
									"Odogu_Card_d12",
									157
								},
								{
									"Odogu_Card_d13",
									158
								},
								{
									"Odogu_Card_c1",
									159
								},
								{
									"Odogu_Card_c2",
									160
								},
								{
									"Odogu_Card_c3",
									161
								},
								{
									"Odogu_Card_c4",
									162
								},
								{
									"Odogu_Card_c5",
									163
								},
								{
									"Odogu_Card_c6",
									164
								},
								{
									"Odogu_Card_c7",
									165
								},
								{
									"Odogu_Card_c8",
									166
								},
								{
									"Odogu_Card_c9",
									167
								},
								{
									"Odogu_Card_c10",
									168
								},
								{
									"Odogu_Card_c11",
									169
								},
								{
									"Odogu_Card_c12",
									170
								},
								{
									"Odogu_Card_c13",
									171
								},
								{
									"Odogu_Card_joker",
									172
								}
							};
						}
						int num;
						if (!OdoguUIArray.TryGetValue(text, out num))
						{
							goto IL_8500;
						}
						switch (num)
						{
							case 0:
								zero.z = 0.6f;
								gameObject.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
								foreach (object obj6 in gameObject.transform)
								{
									Transform transform2 = (Transform)obj6;
									if (transform2.GetComponent<Collider>() != null)
									{
										transform2.GetComponent<Collider>().enabled = false;
									}
								}
								break;
							case 1:
								zero.z = 0.6f;
								gameObject.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
								foreach (object obj7 in gameObject.transform)
								{
									Transform transform2 = (Transform)obj7;
									if (transform2.GetComponent<Collider>() != null)
									{
										transform2.GetComponent<Collider>().enabled = false;
									}
								}
								break;
							case 2:
								zero.z = 0.5f;
								zero2.x = -90f;
								break;
							case 3:
								zero.z = 0.5f;
								zero2.x = -90f;
								break;
							case 4:
								zero.z = 0.5f;
								zero2.x = -90f;
								break;
							case 5:
								zero.z = 0.5f;
								zero.y = 0.2f;
								break;
							case 6:
								GameObject.Find("Prim.00000000").GetComponent<Collider>().enabled = false;
								GameObject.Find("Prim.00000001").GetComponent<Collider>().enabled = false;
								GameObject.Find("Prim.00000002").GetComponent<Collider>().enabled = false;
								GameObject.Find("Prim.00000004").GetComponent<Collider>().enabled = false;
								break;
							case 7:
							case 8:
							case 9:
							case 10:
							case 11:
							case 12:
							case 13:
							case 14:
							case 15:
							case 16:
							case 17:
							case 18:
							case 19:
							case 20:
							case 21:
							case 22:
							case 23:
							case 24:
							case 25:
							case 26:
							case 27:
							case 28:
							case 29:
							case 30:
							case 31:
							case 32:
							case 33:
							case 34:
							case 35:
							case 36:
							case 37:
							case 38:
							case 39:
							case 40:
							case 41:
							case 42:
							case 43:
							case 44:
							case 45:
							case 46:
							case 47:
							case 48:
							case 49:
							case 50:
							case 51:
							case 52:
							case 53:
							case 54:
							case 55:
							case 56:
							case 57:
							case 58:
							case 59:
							case 60:
							case 61:
							case 62:
							case 63:
							case 64:
							case 65:
							case 66:
							case 67:
							case 68:
							case 69:
							case 70:
							case 71:
							case 72:
							case 73:
							case 74:
							case 75:
							case 76:
							case 77:
							case 78:
							case 79:
							case 80:
							case 81:
							case 82:
							case 83:
							case 84:
							case 85:
							case 86:
							case 87:
							case 88:
							case 89:
							case 90:
							case 91:
							case 92:
							case 93:
							case 94:
							case 95:
							case 96:
							case 97:
							case 98:
							case 99:
							case 100:
							case 101:
							case 102:
							case 103:
							case 104:
							case 105:
							case 106:
							case 107:
							case 108:
							case 109:
							case 110:
							case 111:
							case 112:
							case 113:
							case 114:
							case 115:
							case 116:
							case 117:
							case 118:
							case 119:
							case 120:
							case 121:
							case 122:
							case 123:
							case 124:
							case 125:
							case 126:
							case 127:
							case 128:
							case 129:
							case 130:
							case 131:
							case 132:
							case 133:
							case 134:
							case 135:
							case 136:
							case 137:
							case 138:
							case 139:
							case 140:
							case 141:
							case 142:
							case 143:
							case 144:
							case 145:
							case 146:
							case 147:
							case 148:
							case 149:
							case 150:
							case 151:
							case 152:
							case 153:
							case 154:
							case 155:
							case 156:
							case 157:
							case 158:
							case 159:
							case 160:
							case 161:
							case 162:
							case 163:
							case 164:
							case 165:
							case 166:
							case 167:
							case 168:
							case 169:
							case 170:
							case 171:
							case 172:
								zero.z = 0.5f;
								zero2.x = -90f;
								break;
							default:
								goto IL_8500;
						}
					IL_865E:
						gameObject.transform.localPosition = zero;
						gameObject.transform.localRotation = Quaternion.Euler(zero2);
						doguCnt = doguBObject.Count - 1;
						gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
						gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
						gDogu[doguCnt].layer = 8;
						gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
						gDogu[doguCnt].SetActive(false);
						gDogu[doguCnt].transform.position = gameObject.transform.position;
						mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
						mDogu[doguCnt].isScale = false;
						mDogu[doguCnt].obj = gDogu[doguCnt];
						mDogu[doguCnt].maid = gameObject;
						mDogu[doguCnt].angles = gameObject.transform.eulerAngles;
						gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
						mDogu[doguCnt].ido = 1;
						if (doguBIndex[k] == 6 || doguBIndex[k] == 7)
						{
							mDogu[doguCnt].isScale2 = true;
							mDogu[doguCnt].scale2 = gameObject.transform.localScale;
						}
						goto IL_8847;
					IL_8500:
						text2 = gameObject.name;
						bool flag4 = false;
						for (int j = 0; j < doguNameList.Count; j++)
						{
							string[] array = doguNameList[j].Split(new char[]
							{
								','
							});
							if (text2 == array[0])
							{
								flag4 = true;
							}
						}
						if (flag4)
						{
							zero.z = 0.5f;
							zero2.x = -90f;
							goto IL_865E;
						}
						zero.z = 0.5f;
						if (gameObject.name.StartsWith("Odogu_"))
						{
							foreach (object obj8 in gameObject.transform)
							{
								Transform transform2 = (Transform)obj8;
								if (transform2.GetComponent<Collider>() != null)
								{
									transform2.GetComponent<Collider>().enabled = false;
								}
							}
						}
						else if (gameObject.GetComponent<Collider>() != null)
						{
							gameObject.GetComponent<Collider>().enabled = false;
						}
						goto IL_865E;
					}
				IL_8847:
					k--;
				}
			}
			GUI.enabled = true;
			if (doguSelectFlg3)
			{
				bool flag5 = GUI.Toggle(new Rect((float)GetPix(12), (float)GetPix(52), (float)GetPix(50), (float)GetPix(20)), modFlg, "MOD", guistyle6);
				if (flag5 != modFlg)
				{
					modFlg = true;
					nmodFlg = false;
					itemDataList = new List<AdvancedStudioMode.ItemData>(itemDataListMod);
				}
				bool flag6 = GUI.Toggle(new Rect((float)GetPix(82), (float)GetPix(52), (float)GetPix(39), (float)GetPix(20)), nmodFlg, "公式", guistyle6);
				if (flag6 != nmodFlg)
				{
					modFlg = false;
					nmodFlg = true;
					itemDataList = new List<AdvancedStudioMode.ItemData>(itemDataListNMod);
				}
				int num9 = slotCombo.List(new Rect((float)GetPix(51), (float)GetPix(81), (float)GetPix(100), (float)GetPix(23)), slotComboList[slotIndex].text, slotComboList, guistyle4, "box", listStyle3);
				if (num9 != slotIndex)
				{
					slotIndex = num9;
					sortList.Clear();
					scrollPos = new Vector2(0f, 0f);
					if (itemDataList.Count == 0)
					{
						string[] fileListAtExtension;
						if (modFlg)
						{
							fileListAtExtension = GameUty.FileSystemMod.GetFileListAtExtension(".menu");
						}
						else
						{
							fileListAtExtension = GameUty.FileSystem.GetFileListAtExtension(".menu");
						}
						foreach (string path in fileListAtExtension)
						{
							string text5 = Path.GetFileNameWithoutExtension(path) + ".menu";
							byte[] array2 = null;
							using (AFileBase afileBase = GameUty.FileOpen(text5, null))
							{
								NDebug.Assert(afileBase.IsValid(), "メニューファイルが存在しません。 :" + text5);
								array2 = new byte[afileBase.GetSize()];
								afileBase.Read(ref array2, afileBase.GetSize());
							}
							BinaryReader binaryReader = new BinaryReader(new MemoryStream(array2), Encoding.UTF8);
							if (binaryReader.ReadString() != "CM3D2_MENU")
							{
								binaryReader.Close();
							}
							else
							{
								binaryReader.ReadInt32();
								binaryReader.ReadString();
								string text6 = binaryReader.ReadString();
								string info = binaryReader.ReadString();
								string s = "";
								binaryReader.ReadString();
								binaryReader.ReadInt32();
								string text7 = "";
								try
								{
									for (; ; )
									{
										int num10 = (int)binaryReader.ReadByte();
										if (num10 != 0)
										{
											for (int n = 0; n < num10; n++)
											{
												string a = binaryReader.ReadString();
												if (a == "icons")
												{
													text7 = binaryReader.ReadString();
													break;
												}
												if (a == "priority")
												{
													s = binaryReader.ReadString();
													break;
												}
											}
											if (text7 != "")
											{
												break;
											}
										}
									}
									int order = 0;
									int.TryParse(s, out order);
									AdvancedStudioMode.ItemData itemData = new AdvancedStudioMode.ItemData();
									itemData.info = info;
									itemData.name = text7;
									itemData.menu = text5;
									itemData.order = order;
									itemData.cd = array2;
									itemDataList.Add(itemData);
								}
								catch
								{
								}
								binaryReader.Close();
							}
						}
						if (modFlg)
						{
							itemDataListMod = new List<AdvancedStudioMode.ItemData>(itemDataList);
						}
						else
						{
							itemDataListNMod = new List<AdvancedStudioMode.ItemData>(itemDataList);
						}
					}
					foreach (AdvancedStudioMode.ItemData itemData in itemDataList)
					{
						if (slotIndex != 0 && !(itemData.info.ToLower() != slotArray[slotIndex]))
						{
							if (itemData.order > 0)
							{
								AdvancedStudioMode.SortItem sortItem = new AdvancedStudioMode.SortItem();
								sortItem.order = itemData.order;
								sortItem.name = itemData.name;
								sortItem.menu = itemData.menu;
								sortItem.tex = itemData.tex;
								sortList.Add(sortItem);
							}
						}
					}
					IOrderedEnumerable<AdvancedStudioMode.SortItem> orderedEnumerable = from p in sortList
																				   orderby p.order, p.name
																				   select p;
					List<AdvancedStudioMode.SortItem> list = new List<AdvancedStudioMode.SortItem>();
					string b = "";
					foreach (AdvancedStudioMode.SortItem sortItem in orderedEnumerable)
					{
						try
						{
							if (!(sortItem.menu == b))
							{
								if (sortItem.tex == null)
								{
									byte[] data2 = ImportCM.LoadTexture(GameUty.FileSystem, sortItem.name, false).data;
									Texture2D texture2D = new Texture2D(50, 50, TextureFormat.RGB565, false);
									texture2D.LoadImage(data2);
									sortItem.tex = texture2D;
								}
								b = sortItem.menu;
								list.Add(sortItem);
							}
						}
						catch
						{
						}
					}
					sortList = list;
				}
			}
		}

		private void GuiFunc6(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "label";
			guistyle2.fontSize = GetPix(11);
			guistyle2.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle3 = "button";
			guistyle3.fontSize = GetPix(20);
			GUIStyle guistyle4 = "button";
			guistyle4.fontSize = GetPix(14);
			guistyle4.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle5 = "button";
			guistyle5.fontSize = GetPix(12);
			guistyle5.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle6 = new GUIStyle("toggle");
			guistyle6.fontSize = GetPix(13);
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			Maid maid = maidArray[selectMaidIndex];
			if (!kankyoInitFlg)
			{
				listStyle2.normal.textColor = Color.white;
				listStyle2.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle2.onHover.background = (listStyle2.hover.background = new Texture2D(2, 2));
				RectOffset padding = listStyle2.padding;
				RectOffset padding2 = listStyle2.padding;
				RectOffset padding3 = listStyle2.padding;
				int num = listStyle2.padding.bottom = GetPix(0);
				num = (padding3.top = num);
				num = (padding2.right = num);
				padding.left = num;
				listStyle2.fontSize = GetPix(11);
				listStyle3.normal.textColor = Color.white;
				listStyle3.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle3.onHover.background = (listStyle3.hover.background = new Texture2D(2, 2));
				RectOffset padding4 = listStyle3.padding;
				RectOffset padding5 = listStyle3.padding;
				num = (listStyle3.padding.top = GetPix(0));
				num = (padding5.right = num);
				padding4.left = num;
				listStyle3.padding.bottom = GetPix(0);
				listStyle3.fontSize = GetPix(12);
				bgmCombo.selectedItemIndex = bgmIndex;
				if (sceneLevel == 5)
				{
					bgmCombo.selectedItemIndex = 2;
				}
				bgmComboList = new GUIContent[bgmArray.Length];
				int i = 0;
				while (i < bgmArray.Length)
				{
					string text = bgmArray[i];
					if (text == null)
					{
						goto IL_501;
					}
					//if (<PrivateImplementationDetails>{54936A6C-01FC-422B-BFCA-C5A294F216D4}.$$method0x6000015-1 == null)
					if (DanceArray == null)
					{
						//<PrivateImplementationDetails>{54936A6C-01FC-422B-BFCA-C5A294F216D4}.$$method0x6000015-1 = new Dictionary<string, int>(11)
						DanceArray = new Dictionary<string, int>(11)
						{
							{
								"dokidokifallinlove_short",
								0
							},
							{
								"entrancetoyou_short",
								1
							},
							{
								"scarlet leap_short",
								2
							},
							{
								"stellarmytears_short",
								3
							},
							{
								"stellarmytears_short2",
								4
							},
							{
								"stellarmytears_short3",
								5
							},
							{
								"RhythmixToYou",
								6
							},
							{
								"happy_happy_scandal1",
								7
							},
							{
								"happy_happy_scandal2",
								8
							},
							{
								"happy_happy_scandal3",
								9
							},
							{
								"can_know_two_close",
								10
							}
						};
					}
					if (!DanceArray.TryGetValue(text, out num))
					{
						goto IL_501;
					}
					switch (num)
					{
						case 0:
							bgmComboList[i] = new GUIContent("ドキドキ☆Fallin' Love");
							break;
						case 1:
							bgmComboList[i] = new GUIContent("entrance to you");
							break;
						case 2:
							bgmComboList[i] = new GUIContent("scarlet leap");
							break;
						case 3:
							bgmComboList[i] = new GUIContent("stellar my tears1");
							break;
						case 4:
							bgmComboList[i] = new GUIContent("stellar my tears2");
							break;
						case 5:
							bgmComboList[i] = new GUIContent("stellar my tears3");
							break;
						case 6:
							bgmComboList[i] = new GUIContent("rhythmix to you");
							break;
						case 7:
							bgmComboList[i] = new GUIContent("happy!happy!スキャンダル!! 1");
							break;
						case 8:
							bgmComboList[i] = new GUIContent("happy!happy!スキャンダル!! 2");
							break;
						case 9:
							bgmComboList[i] = new GUIContent("happy!happy!スキャンダル!! 3");
							break;
						case 10:
							bgmComboList[i] = new GUIContent("Can Know Two Close");
							break;
						default:
							goto IL_501;
					}
				IL_51A:
					i++;
					continue;
				IL_501:
					bgmComboList[i] = new GUIContent(bgmArray[i]);
					goto IL_51A;
				}
				bgCombo.selectedItemIndex = bgIndex;
				bgComboList = new GUIContent[bgArray.Length];
				i = 0;
				while (i < bgArray.Length)
				{
					string text = bgArray[i];
					if (text == null)
					{
						goto IL_18DC;
					}
					if (bgUiArray == null)
					{
						bgUiArray = new Dictionary<string, int>(120)
						{
							{
								"Salon",
								0
							},
							{
								"Syosai",
								1
							},
							{
								"Syosai_Night",
								2
							},
							{
								"DressRoom_NoMirror",
								3
							},
							{
								"MyBedRoom",
								4
							},
							{
								"MyBedRoom_Night",
								5
							},
							{
								"MyBedRoom_NightOff",
								6
							},
							{
								"Bathroom",
								7
							},
							{
								"PlayRoom",
								8
							},
							{
								"Pool",
								9
							},
							{
								"SMRoom",
								10
							},
							{
								"PlayRoom2",
								11
							},
							{
								"Salon_Garden",
								12
							},
							{
								"LargeBathRoom",
								13
							},
							{
								"MaidRoom",
								14
							},
							{
								"OiranRoom",
								15
							},
							{
								"Penthouse",
								16
							},
							{
								"Town",
								17
							},
							{
								"Kitchen",
								18
							},
							{
								"Kitchen_Night",
								19
							},
							{
								"Shitsumu",
								20
							},
							{
								"Shitsumu_Night",
								21
							},
							{
								"Salon_Entrance",
								22
							},
							{
								"Bar",
								23
							},
							{
								"Toilet",
								24
							},
							{
								"Train",
								25
							},
							{
								"smroom2",
								26
							},
							{
								"LockerRoom",
								27
							},
							{
								"Oheya",
								28
							},
							{
								"Salon_Day",
								29
							},
							{
								"ClassRoom",
								30
							},
							{
								"ClassRoom_Play",
								31
							},
							{
								"HoneymoonRoom",
								32
							},
							{
								"OutletPark",
								33
							},
							{
								"BigSight",
								34
							},
							{
								"BigSight_Night",
								35
							},
							{
								"PrivateRoom",
								36
							},
							{
								"PrivateRoom_Night",
								37
							},
							{
								"Sea",
								38
							},
							{
								"Sea_Night",
								39
							},
							{
								"Yashiki",
								40
							},
							{
								"Yashiki_Day",
								41
							},
							{
								"Yashiki_Pillow",
								42
							},
							{
								"rotenburo",
								43
							},
							{
								"rotenburo_night",
								44
							},
							{
								"Villa",
								45
							},
							{
								"Villa_Night",
								46
							},
							{
								"Villa_BedRoom",
								47
							},
							{
								"Villa_BedRoom_Night",
								48
							},
							{
								"Villa_Farm",
								49
							},
							{
								"Villa_Farm_Night",
								50
							},
							{
								"karaokeroom",
								51
							},
							{
								"Theater",
								52
							},
							{
								"Theater_LightOff",
								53
							},
							{
								"LiveStage",
								54
							},
							{
								"LiveStage_Side",
								55
							},
							{
								"LiveStage_use_dance",
								56
							},
							{
								"BackStage",
								57
							},
							{
								"DanceRoom",
								58
							},
							{
								"EmpireClub_Rotary",
								59
							},
							{
								"EmpireClub_Rotary_Night",
								60
							},
							{
								"EmpireClub_Entrance",
								61
							},
							{
								"ShinShitsumu",
								62
							},
							{
								"ShinShitsumu_ChairRot",
								63
							},
							{
								"ShinShitsumu_Night",
								64
							},
							{
								"MyRoom",
								65
							},
							{
								"MyRoom_Night",
								66
							},
							{
								"OpemCafe",
								67
							},
							{
								"OpemCafe_Night",
								68
							},
							{
								"Restaurant",
								69
							},
							{
								"Restaurant_Night",
								70
							},
							{
								"MainKitchen",
								71
							},
							{
								"MainKitchen_Night",
								72
							},
							{
								"MainKitchen_LightOff",
								73
							},
							{
								"BarLounge",
								74
							},
							{
								"Casino",
								75
							},
							{
								"CasinoMini",
								76
							},
							{
								"SMClub",
								77
							},
							{
								"Soap",
								78
							},
							{
								"Spa",
								79
							},
							{
								"Spa_Night",
								80
							},
							{
								"ShoppingMall",
								81
							},
							{
								"ShoppingMall_Night",
								82
							},
							{
								"GameShop",
								83
							},
							{
								"MusicShop",
								84
							},
							{
								"HeroineRoom_A1",
								85
							},
							{
								"HeroineRoom_A1_Night",
								86
							},
							{
								"HeroineRoom_B1",
								87
							},
							{
								"HeroineRoom_B1_Night",
								88
							},
							{
								"HeroineRoom_C1",
								89
							},
							{
								"HeroineRoom_C1_Night",
								90
							},
							{
								"HeroineRoom_A",
								91
							},
							{
								"HeroineRoom_A_Night",
								92
							},
							{
								"HeroineRoom_B",
								93
							},
							{
								"HeroineRoom_B_Night",
								94
							},
							{
								"HeroineRoom_C",
								95
							},
							{
								"HeroineRoom_C_Night",
								96
							},
							{
								"Shukuhakubeya_BedRoom",
								97
							},
							{
								"Shukuhakubeya_BedRoom_Night",
								98
							},
							{
								"Shukuhakubeya_Other_BedRoom",
								99
							},
							{
								"Shukuhakubeya_Living",
								100
							},
							{
								"Shukuhakubeya_Living_Night",
								101
							},
							{
								"Shukuhakubeya_Toilet",
								102
							},
							{
								"Shukuhakubeya_Toilet_Night",
								103
							},
							{
								"Shukuhakubeya_WashRoom",
								104
							},
							{
								"Shukuhakubeya_WashRoom_Night",
								105
							},
							{
								"opemcafe_rance10",
								106
							},
							{
								"opemcafe_rance10_night",
								107
							},
							{
								"opemcafe_riddlejoker",
								108
							},
							{
								"opemcafe_riddlejoker_night",
								109
							},
							{
								"opemcafe_wanko",
								110
							},
							{
								"opemcafe_wanko_night",
								111
							},
							{
								"opemcafe_raspberry",
								112
							},
							{
								"opemcafe_raspberry_night",
								113
							},
							{
								"seacafe",
								114
							},
							{
								"seacafe_night",
								115
							},
							{
								"com3d2pool",
								116
							},
							{
								"com3d2pool_night",
								117
							},
							{
								"shrine",
								118
							},
							{
								"shrine_night",
								119
							}
						};
					}
					if (!bgUiArray.TryGetValue(text, out num))
					{
						goto IL_18DC;
					}
					switch (num)
					{
						case 0:
							bgComboList[i] = new GUIContent("サロン");
							break;
						case 1:
							bgComboList[i] = new GUIContent("書斎");
							break;
						case 2:
							bgComboList[i] = new GUIContent("書斎(夜)");
							break;
						case 3:
							bgComboList[i] = new GUIContent("ドレスルーム");
							break;
						case 4:
							bgComboList[i] = new GUIContent("自室");
							break;
						case 5:
							bgComboList[i] = new GUIContent("自室(夜)");
							break;
						case 6:
							bgComboList[i] = new GUIContent("自室(消灯)");
							break;
						case 7:
							bgComboList[i] = new GUIContent("風呂");
							break;
						case 8:
							bgComboList[i] = new GUIContent("プレイルーム");
							break;
						case 9:
							bgComboList[i] = new GUIContent("プール");
							break;
						case 10:
							bgComboList[i] = new GUIContent("SMルーム");
							break;
						case 11:
							bgComboList[i] = new GUIContent("プレイルーム2");
							break;
						case 12:
							bgComboList[i] = new GUIContent("サロン(中庭)");
							break;
						case 13:
							bgComboList[i] = new GUIContent("大浴場");
							break;
						case 14:
							bgComboList[i] = new GUIContent("メイド部屋");
							break;
						case 15:
							bgComboList[i] = new GUIContent("花魁ルーム");
							break;
						case 16:
							bgComboList[i] = new GUIContent("ペントハウス");
							break;
						case 17:
							bgComboList[i] = new GUIContent("街");
							break;
						case 18:
							bgComboList[i] = new GUIContent("キッチン");
							break;
						case 19:
							bgComboList[i] = new GUIContent("キッチン(夜)");
							break;
						case 20:
							bgComboList[i] = new GUIContent("執務室");
							break;
						case 21:
							bgComboList[i] = new GUIContent("執務室(夜)");
							break;
						case 22:
							bgComboList[i] = new GUIContent("エントランス");
							break;
						case 23:
							bgComboList[i] = new GUIContent("バー");
							break;
						case 24:
							bgComboList[i] = new GUIContent("トイレ");
							break;
						case 25:
							bgComboList[i] = new GUIContent("電車");
							break;
						case 26:
							bgComboList[i] = new GUIContent("地下室");
							break;
						case 27:
							bgComboList[i] = new GUIContent("ロッカールーム");
							break;
						case 28:
							bgComboList[i] = new GUIContent("四畳半部屋");
							break;
						case 29:
							bgComboList[i] = new GUIContent("サロン(昼)");
							break;
						case 30:
							bgComboList[i] = new GUIContent("教室");
							break;
						case 31:
							bgComboList[i] = new GUIContent("教室(夜伽)");
							break;
						case 32:
							bgComboList[i] = new GUIContent("ハネムーンルーム");
							break;
						case 33:
							bgComboList[i] = new GUIContent("アウトレットパーク");
							break;
						case 34:
							bgComboList[i] = new GUIContent("ビッグサイト");
							break;
						case 35:
							bgComboList[i] = new GUIContent("ビッグサイト(夜)");
							break;
						case 36:
							bgComboList[i] = new GUIContent("プライベートルーム");
							break;
						case 37:
							bgComboList[i] = new GUIContent("プライベートルーム(夜)");
							break;
						case 38:
							bgComboList[i] = new GUIContent("海");
							break;
						case 39:
							bgComboList[i] = new GUIContent("海(夜)");
							break;
						case 40:
							bgComboList[i] = new GUIContent("屋敷(夜)");
							break;
						case 41:
							bgComboList[i] = new GUIContent("屋敷");
							break;
						case 42:
							bgComboList[i] = new GUIContent("屋敷(夜・枕)");
							break;
						case 43:
							bgComboList[i] = new GUIContent("露天風呂");
							break;
						case 44:
							bgComboList[i] = new GUIContent("露天風呂(夜)");
							break;
						case 45:
							bgComboList[i] = new GUIContent("ヴィラ1F");
							break;
						case 46:
							bgComboList[i] = new GUIContent("ヴィラ1F(夜)");
							break;
						case 47:
							bgComboList[i] = new GUIContent("ヴィラ2F");
							break;
						case 48:
							bgComboList[i] = new GUIContent("ヴィラ2F(夜)");
							break;
						case 49:
							bgComboList[i] = new GUIContent("畑");
							break;
						case 50:
							bgComboList[i] = new GUIContent("畑(夜)");
							break;
						case 51:
							bgComboList[i] = new GUIContent("カラオケルーム");
							break;
						case 52:
							bgComboList[i] = new GUIContent("劇場");
							break;
						case 53:
							bgComboList[i] = new GUIContent("劇場(夜)");
							break;
						case 54:
							bgComboList[i] = new GUIContent("ステージ");
							break;
						case 55:
							bgComboList[i] = new GUIContent("ステージ(ライト)");
							break;
						case 56:
							bgComboList[i] = new GUIContent("ステージ(オフ)");
							break;
						case 57:
							bgComboList[i] = new GUIContent("ステージ裏");
							break;
						case 58:
							bgComboList[i] = new GUIContent("トレーニングルーム");
							break;
						case 59:
							bgComboList[i] = new GUIContent("ロータリー");
							break;
						case 60:
							bgComboList[i] = new GUIContent("ロータリー(夜)");
							break;
						case 61:
							bgComboList[i] = new GUIContent("エントランス");
							break;
						case 62:
							bgComboList[i] = new GUIContent("執務室");
							break;
						case 63:
							bgComboList[i] = new GUIContent("執務室(椅子)");
							break;
						case 64:
							bgComboList[i] = new GUIContent("執務室(夜)");
							break;
						case 65:
							bgComboList[i] = new GUIContent("主人公部屋");
							break;
						case 66:
							bgComboList[i] = new GUIContent("主人公部屋(夜)");
							break;
						case 67:
							bgComboList[i] = new GUIContent("カフェ");
							break;
						case 68:
							bgComboList[i] = new GUIContent("カフェ(夜)");
							break;
						case 69:
							bgComboList[i] = new GUIContent("レストラン");
							break;
						case 70:
							bgComboList[i] = new GUIContent("レストラン(夜)");
							break;
						case 71:
							bgComboList[i] = new GUIContent("キッチン");
							break;
						case 72:
							bgComboList[i] = new GUIContent("キッチン(夜)");
							break;
						case 73:
							bgComboList[i] = new GUIContent("キッチン(オフ)");
							break;
						case 74:
							bgComboList[i] = new GUIContent("バー");
							break;
						case 75:
							bgComboList[i] = new GUIContent("カジノ");
							break;
						case 76:
							bgComboList[i] = new GUIContent("カジノミニ");
							break;
						case 77:
							bgComboList[i] = new GUIContent("SMクラブ");
							break;
						case 78:
							bgComboList[i] = new GUIContent("ソープ");
							break;
						case 79:
							bgComboList[i] = new GUIContent("スパ");
							break;
						case 80:
							bgComboList[i] = new GUIContent("スパ(夜)");
							break;
						case 81:
							bgComboList[i] = new GUIContent("ショッピングモール");
							break;
						case 82:
							bgComboList[i] = new GUIContent("ショッピングモール(夜)");
							break;
						case 83:
							bgComboList[i] = new GUIContent("ゲームショップ");
							break;
						case 84:
							bgComboList[i] = new GUIContent("ミュージックショップ");
							break;
						case 85:
							bgComboList[i] = new GUIContent("無垢部屋");
							break;
						case 86:
							bgComboList[i] = new GUIContent("無垢部屋(夜)");
							break;
						case 87:
							bgComboList[i] = new GUIContent("真面目部屋");
							break;
						case 88:
							bgComboList[i] = new GUIContent("真面目部屋(夜)");
							break;
						case 89:
							bgComboList[i] = new GUIContent("凜デレ部屋");
							break;
						case 90:
							bgComboList[i] = new GUIContent("凜デレ部屋(夜)");
							break;
						case 91:
							bgComboList[i] = new GUIContent("ツンデレ部屋");
							break;
						case 92:
							bgComboList[i] = new GUIContent("ツンデレ部屋(夜)");
							break;
						case 93:
							bgComboList[i] = new GUIContent("クーデレ部屋");
							break;
						case 94:
							bgComboList[i] = new GUIContent("クーデレ部屋(夜)");
							break;
						case 95:
							bgComboList[i] = new GUIContent("純真部屋");
							break;
						case 96:
							bgComboList[i] = new GUIContent("純真部屋(夜)");
							break;
						case 97:
							bgComboList[i] = new GUIContent("宿泊-ベッドルーム");
							break;
						case 98:
							bgComboList[i] = new GUIContent("宿泊-ベッドルーム(夜)");
							break;
						case 99:
							bgComboList[i] = new GUIContent("宿泊-他ベッドルーム(夜)");
							break;
						case 100:
							bgComboList[i] = new GUIContent("宿泊-リビング");
							break;
						case 101:
							bgComboList[i] = new GUIContent("宿泊-リビング(夜)");
							break;
						case 102:
							bgComboList[i] = new GUIContent("宿泊-トイレ");
							break;
						case 103:
							bgComboList[i] = new GUIContent("宿泊-トイレ(夜)");
							break;
						case 104:
							bgComboList[i] = new GUIContent("宿泊-洗面所");
							break;
						case 105:
							bgComboList[i] = new GUIContent("宿泊-洗面所(夜)");
							break;
						case 106:
							bgComboList[i] = new GUIContent("ランス10");
							break;
						case 107:
							bgComboList[i] = new GUIContent("ランス10(夜)");
							break;
						case 108:
							bgComboList[i] = new GUIContent("リドルジョーカー");
							break;
						case 109:
							bgComboList[i] = new GUIContent("リドルジョーカー(夜)");
							break;
						case 110:
							bgComboList[i] = new GUIContent("わんこ");
							break;
						case 111:
							bgComboList[i] = new GUIContent("わんこ(夜)");
							break;
						case 112:
							bgComboList[i] = new GUIContent("ラズベリー");
							break;
						case 113:
							bgComboList[i] = new GUIContent("ラズベリー(夜)");
							break;
						case 114:
							bgComboList[i] = new GUIContent("シーカフェ");
							break;
						case 115:
							bgComboList[i] = new GUIContent("シーカフェ(夜)");
							break;
						case 116:
							bgComboList[i] = new GUIContent("プール");
							break;
						case 117:
							bgComboList[i] = new GUIContent("プール(夜)");
							break;
						case 118:
							bgComboList[i] = new GUIContent("神社");
							break;
						case 119:
							bgComboList[i] = new GUIContent("神社(夜)");
							break;
						default:
							goto IL_18DC;
					}
				IL_195B:
					Dictionary<string, string> saveDataDic = CreativeRoomManager.GetSaveDataDic();
					if (saveDataDic != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in saveDataDic)
						{
							if (bgArray[i] == keyValuePair.Key)
							{
								bgComboList[i] = new GUIContent(keyValuePair.Value);
							}
						}
					}
					i++;
					continue;
				IL_18DC:
					string text2 = bgArray[i];
					for (int j = 0; j < bgNameList.Count; j++)
					{
						string[] array = bgNameList[j].Split(new char[]
						{
							','
						});
						if (text2 == array[0])
						{
							text2 = array[1];
						}
					}
					bgComboList[i] = new GUIContent(text2);
					goto IL_195B;
				}
				doguCombo2.selectedItemIndex = 0;
				doguCombo2List = new GUIContent[doguB1Array.Length];
				i = 0;
				while (i < doguB1Array.Length)
				{
					string text = doguB1Array[i];
					if (text == null)
					{
						goto IL_4852;
					}
					if (OdoguUIArray == null)
					{
						OdoguUIArray = new Dictionary<string, int>(276)
						{
							{
								"Odogu_KousokuKijyouiChair_photo_ver",
								0
							},
							{
								"Odogu_VirginRoad_photo_ver",
								1
							},
							{
								"neirobo",
								2
							},
							{
								"Odogu_ClassRoomDesk_photo_ver",
								3
							},
							{
								"Odogu_ClassRoomChair_photo_ver",
								4
							},
							{
								"Odogu_TrumpTowerSmall_photo_ver",
								5
							},
							{
								"Odogu_TrumpTowerBig_photo_ver",
								6
							},
							{
								"Odogu_VVLight_photo_ver",
								7
							},
							{
								"Odogu_OXCamera_photo_ver",
								8
							},
							{
								"Odogu_HandCameraVV_photo_ver",
								9
							},
							{
								"Odogu_PC_photo_ver",
								10
							},
							{
								"Odogu_PC_Monitor_photo_ver",
								11
							},
							{
								"Odogu_PC_Keyboard_photo_ver",
								12
							},
							{
								"Odogu_PC_Mouse_photo_ver",
								13
							},
							{
								"Odogu_MaidRoomBook001_photo_ver",
								14
							},
							{
								"Odogu_MaidRoomBook002_photo_ver",
								15
							},
							{
								"Odogu_MaidRoomBook003_photo_ver",
								16
							},
							{
								"Odogu_MaidRoomBook004_photo_ver",
								17
							},
							{
								"Odogu_MaidRoomBook005_photo_ver",
								18
							},
							{
								"Odogu_Pen_photo_ver",
								19
							},
							{
								"Odogu_Pen_Black_photo_ver",
								20
							},
							{
								"Odogu_Pen_Brown_photo_ver",
								21
							},
							{
								"Odogu_Pen_Green_photo_ver",
								22
							},
							{
								"Odogu_Enpitsu_photo_ver",
								23
							},
							{
								"Odogu_Enpitsu_Black_photo_ver",
								24
							},
							{
								"Odogu_Enpitsu_Red_photo_ver",
								25
							},
							{
								"Odogu_Keshigomu_photo_ver",
								26
							},
							{
								"Odogu_Keshigomu_Purple_photo_ver",
								27
							},
							{
								"Odogu_Keshigomu_Yellow_photo_ver",
								28
							},
							{
								"Odogu_StickNori_photo_ver",
								29
							},
							{
								"Odogu_Condom_Close_photo_ver",
								30
							},
							{
								"Odogu_Condom_Open_photo_ver",
								31
							},
							{
								"Odogu_Condom_Pack_photo_ver",
								32
							},
							{
								"Odogu_SalonSofa_long_photo_ver",
								33
							},
							{
								"Odogu_SalonSofa_4P_photo_ver",
								34
							},
							{
								"Odogu_Girochin_A_photo_ver",
								35
							},
							{
								"Odogu_SankakuMokuba_photo_ver",
								36
							},
							{
								"Odogu_SMRoom2_SankakuMokuba_photo_ver",
								37
							},
							{
								"Odogu_Kousokudai_photo_ver",
								38
							},
							{
								"Odogu_XmasTreeMini_photo_ver",
								39
							},
							{
								"Odogu_KadomatsuMini_photo_ver",
								40
							},
							{
								"Odogu_Kitchen_photo_ver",
								41
							},
							{
								"Odogu_TableFlower_photo_ver",
								42
							},
							{
								"Odogu_Kadou_photo_ver",
								43
							},
							{
								"Odogu_Dresser_photo_ver",
								44
							},
							{
								"Odogu_ClassRoomDesk",
								45
							},
							{
								"Odogu_KadouChair_photo_ver",
								46
							},
							{
								"Odogu_DresserChair_photo_ver",
								47
							},
							{
								"Odogu_MaidRoomChair_photo_ver",
								48
							},
							{
								"Odogu_PublicToiletBenki_photo_ver",
								49
							},
							{
								"Odogu_Sukebeisu_photo_ver",
								50
							},
							{
								"Odogu_Mat_photo_ver",
								51
							},
							{
								"Odogu_Seikaku_Tsundere",
								52
							},
							{
								"Odogu_Seikaku_Jyunshin",
								53
							},
							{
								"Odogu_Seikaku_Cool",
								54
							},
							{
								"Odogu_Manaita_photo_ver",
								55
							},
							{
								"Odogu_Nabe_photo_ver",
								56
							},
							{
								"Odogu_NoteBook_photo_ver",
								57
							},
							{
								"Odogu_Sankousyo_photo_ver",
								58
							},
							{
								"Odogu_Sentaku_Kago_photo_ver",
								59
							},
							{
								"Odogu_Sentaku_Towel_photo_ver",
								60
							},
							{
								"Odogu_Sentakumono_photo_ver",
								61
							},
							{
								"Odogu_SalonScreen_photo_ver",
								62
							},
							{
								"Odogu_WineGlass_photo_ver",
								63
							},
							{
								"Odogu_SalonSofa_small_photo_ver",
								64
							},
							{
								"Odogu_Seikaku_Tsundere_photo_ver",
								65
							},
							{
								"Odogu_Seikaku_Jyunshin_photo_ver",
								66
							},
							{
								"Odogu_Seikaku_Cool_photo_ver",
								67
							},
							{
								"Megane001_z2_Scenario_Model",
								68
							},
							{
								"nei_photo_ver",
								69
							},
							{
								"neirobo_photo_ver",
								70
							},
							{
								"Odogu_SimpleTable",
								71
							},
							{
								"Odogu_DildoBox",
								72
							},
							{
								"PlayAreaOut",
								73
							},
							{
								"DesktopScreen",
								74
							},
							{
								"Odogu_ChuukaSet_chahan_photo_ver",
								75
							},
							{
								"Odogu_ChuukaSet_gyouza_photo_ver",
								76
							},
							{
								"Odogu_ChuukaSet_mabo_photo_ver",
								77
							},
							{
								"Odogu_ChuukaSet_tea_photo_ver",
								78
							},
							{
								"Odogu_WasyokuSet_gohan_photo_ver",
								79
							},
							{
								"Odogu_WasyokuSet_hashi_photo_ver",
								80
							},
							{
								"Odogu_WasyokuSet_misoshiru_photo_ver",
								81
							},
							{
								"Odogu_WasyokuSet_nimono_photo_ver",
								82
							},
							{
								"Odogu_WasyokuSet_ocha_photo_ver",
								83
							},
							{
								"Odogu_YousyokuSet_ChickenRice_photo_ver",
								84
							},
							{
								"Odogu_YousyokuSet_Coffee_photo_ver",
								85
							},
							{
								"Odogu_YousyokuSet_CornSoup_photo_ver",
								86
							},
							{
								"Odogu_YousyokuSet_Hamburg_photo_ver",
								87
							},
							{
								"Odogu_YousyokuSet_SakiwareSpoon_photo_ver",
								88
							},
							{
								"Odogu_PR_Table_photo_ver",
								89
							},
							{
								"Odogu_PR_Table_Chuuka_photo_ver",
								90
							},
							{
								"Odogu_PR_Table_Wasyoku_photo_ver",
								91
							},
							{
								"Odogu_PR_Table_Yousyoku_photo_ver",
								92
							},
							{
								"Odogu_LongDaiza_photo_ver",
								93
							},
							{
								"BGanimal_cat",
								94
							},
							{
								"BGanimal_dog",
								95
							},
							{
								"BGanimal_niwatori",
								96
							},
							{
								"BGanimal_suzume",
								97
							},
							{
								"BGodogu_bbqgrill",
								98
							},
							{
								"BGodogu_bucket",
								99
							},
							{
								"BGodogu_coolerbox",
								100
							},
							{
								"BGodogu_game_darts",
								101
							},
							{
								"BGodogu_game_dartsboard",
								102
							},
							{
								"BGodogu_nabe_huta",
								103
							},
							{
								"BGodogu_nabe_water",
								104
							},
							{
								"BGodogu_natumikan",
								105
							},
							{
								"BGodogu_rb_chair",
								106
							},
							{
								"BGodogu_rb_duck",
								107
							},
							{
								"BGodogu_rb_obon",
								108
							},
							{
								"BGodogu_rb_tokkuri",
								109
							},
							{
								"BGodogu_saracorn",
								110
							},
							{
								"BGodogu_saraimo",
								111
							},
							{
								"BGodogu_saratomato",
								112
							},
							{
								"BGodogu_sunanoshiro",
								113
							},
							{
								"BGodogu_sunanoyama",
								114
							},
							{
								"BGodogu_tsutsuhanabi",
								115
							},
							{
								"BGodogu_ukiwa",
								116
							},
							{
								"BGodogu_vf_crops_corn",
								117
							},
							{
								"BGodogu_vf_crops_gekkabijin",
								118
							},
							{
								"BGodogu_vf_crops_gekkabijinflower",
								119
							},
							{
								"BGodogu_vf_crops_himawari",
								120
							},
							{
								"BGodogu_vf_crops_natsumikan",
								121
							},
							{
								"BGodogu_vf_crops_suika",
								122
							},
							{
								"BGodogu_vf_crops_zakuro",
								123
							},
							{
								"BGodogu_vf_kanban_ok",
								124
							},
							{
								"BGodogu_vf_kanban_saibai",
								125
							},
							{
								"BGodogu_vf_kanban_taiki",
								126
							},
							{
								"BGodogu_vf_radio",
								127
							},
							{
								"BGodogu_villa_coffeemaker",
								128
							},
							{
								"BGodogu_villa_reizouko",
								129
							},
							{
								"BGodogu_villa_table",
								130
							},
							{
								"BGodogu_villa_tvrimocon",
								131
							},
							{
								"BGodogu_villa_winecellar",
								132
							},
							{
								"BGodogu_villabr_sideboard",
								133
							},
							{
								"BGOdogu_Game_Nei_USB",
								134
							},
							{
								"BGOdogu_Game_Wanage",
								135
							},
							{
								"BGOdogu_Game_Wa",
								136
							},
							{
								"BGodogu_pafe",
								137
							},
							{
								"BGodogu_furaidopoteto",
								138
							},
							{
								"BGodogu_karaoketable",
								139
							},
							{
								"BGodogu_omuriceh",
								140
							},
							{
								"BGodogu_omuricekao1",
								141
							},
							{
								"BGodogu_omuricekao2",
								142
							},
							{
								"BGodogu_omuriceoppai",
								143
							},
							{
								"BGodogu_kakigori",
								144
							},
							{
								"BGodogu_pretzel_sara",
								145
							},
							{
								"BGodogu_karaoke_box",
								146
							},
							{
								"Odogu_StandMike",
								147
							},
							{
								"Odogu_StandMikeBase",
								148
							},
							{
								"photo_ver/Odogu_Umeko_Mike_photo_ver",
								149
							},
							{
								"Odogu_HeroineChair_muku",
								150
							},
							{
								"Odogu_HeroineChair_mazime",
								151
							},
							{
								"Odogu_HeroineChair_rindere",
								152
							},
							{
								"Odogu_HeroineChair_tsumdere",
								153
							},
							{
								"Odogu_HeroineChair_cooldere",
								154
							},
							{
								"Odogu_HeroineChair_junshin",
								155
							},
							{
								"photo_ver/Odogu_Etoile_Chair_photo_ver",
								156
							},
							{
								"Odogu_LoveSofa",
								157
							},
							{
								"Odogu_TabletPC",
								158
							},
							{
								"Odogu_Styluspen_black",
								159
							},
							{
								"Odogu_Styluspen_white",
								160
							},
							{
								"Odogu_Styluspen_red",
								161
							},
							{
								"Odogu_Styluspen_blue",
								162
							},
							{
								"Odogu_Styluspen_yellow",
								163
							},
							{
								"Odogu_Styluspen_green",
								164
							},
							{
								"Odogu_Omurice1",
								165
							},
							{
								"Odogu_Omurice3",
								166
							},
							{
								"Odogu_OmuriceH",
								167
							},
							{
								"Odogu_OmuriceKao1",
								168
							},
							{
								"Odogu_OmuriceKao2",
								169
							},
							{
								"Odogu_OmuriceOppai",
								170
							},
							{
								"Odogu_AcquaPazza",
								171
							},
							{
								"Odogu_Sandwich",
								172
							},
							{
								"Odogu_vichyssoise",
								173
							},
							{
								"Odogu_BirthdayCake",
								174
							},
							{
								"Odogu_Shortcake",
								175
							},
							{
								"Odogu_MontBlanc",
								176
							},
							{
								"Odogu_Pafe",
								177
							},
							{
								"Odogu_Smoothie_Red",
								178
							},
							{
								"Odogu_Smoothie_Green",
								179
							},
							{
								"Odogu_Cocktail_Red",
								180
							},
							{
								"Odogu_Cocktail_Blue",
								181
							},
							{
								"Odogu_Cocktail_Yellow",
								182
							},
							{
								"Odogu_Coffiecup",
								183
							},
							{
								"Odogu_WineBottle(cap_off)",
								184
							},
							{
								"Odogu_WineBottle(cap_on)",
								185
							},
							{
								"Odogu_Jyouro",
								186
							},
							{
								"Odogu_Planter_Red",
								187
							},
							{
								"Odogu_Planter_Lightblue",
								188
							},
							{
								"Odogu_MariGold",
								189
							},
							{
								"Odogu_CasinoChip_10",
								190
							},
							{
								"Odogu_CasinoChip_100",
								191
							},
							{
								"Odogu_CasinoChip_1000",
								192
							},
							{
								"Odogu_CardShooter",
								193
							},
							{
								"Odogu_CardsDeck",
								194
							},
							{
								"Odogu_Card_s1",
								195
							},
							{
								"Odogu_Card_s2",
								196
							},
							{
								"Odogu_Card_s3",
								197
							},
							{
								"Odogu_Card_s4",
								198
							},
							{
								"Odogu_Card_s5",
								199
							},
							{
								"Odogu_Card_s6",
								200
							},
							{
								"Odogu_Card_s7",
								201
							},
							{
								"Odogu_Card_s8",
								202
							},
							{
								"Odogu_Card_s9",
								203
							},
							{
								"Odogu_Card_s10",
								204
							},
							{
								"Odogu_Card_s11",
								205
							},
							{
								"Odogu_Card_s12",
								206
							},
							{
								"Odogu_Card_s13",
								207
							},
							{
								"Odogu_Card_h1",
								208
							},
							{
								"Odogu_Card_h2",
								209
							},
							{
								"Odogu_Card_h3",
								210
							},
							{
								"Odogu_Card_h4",
								211
							},
							{
								"Odogu_Card_h5",
								212
							},
							{
								"Odogu_Card_h6",
								213
							},
							{
								"Odogu_Card_h7",
								214
							},
							{
								"Odogu_Card_h8",
								215
							},
							{
								"Odogu_Card_h9",
								216
							},
							{
								"Odogu_Card_h10",
								217
							},
							{
								"Odogu_Card_h11",
								218
							},
							{
								"Odogu_Card_h12",
								219
							},
							{
								"Odogu_Card_h13",
								220
							},
							{
								"Odogu_Card_d1",
								221
							},
							{
								"Odogu_Card_d2",
								222
							},
							{
								"Odogu_Card_d3",
								223
							},
							{
								"Odogu_Card_d4",
								224
							},
							{
								"Odogu_Card_d5",
								225
							},
							{
								"Odogu_Card_d6",
								226
							},
							{
								"Odogu_Card_d7",
								227
							},
							{
								"Odogu_Card_d8",
								228
							},
							{
								"Odogu_Card_d9",
								229
							},
							{
								"Odogu_Card_d10",
								230
							},
							{
								"Odogu_Card_d11",
								231
							},
							{
								"Odogu_Card_d12",
								232
							},
							{
								"Odogu_Card_d13",
								233
							},
							{
								"Odogu_Card_c1",
								234
							},
							{
								"Odogu_Card_c2",
								235
							},
							{
								"Odogu_Card_c3",
								236
							},
							{
								"Odogu_Card_c4",
								237
							},
							{
								"Odogu_Card_c5",
								238
							},
							{
								"Odogu_Card_c6",
								239
							},
							{
								"Odogu_Card_c7",
								240
							},
							{
								"Odogu_Card_c8",
								241
							},
							{
								"Odogu_Card_c9",
								242
							},
							{
								"Odogu_Card_c10",
								243
							},
							{
								"Odogu_Card_c11",
								244
							},
							{
								"Odogu_Card_c12",
								245
							},
							{
								"Odogu_Card_c13",
								246
							},
							{
								"Odogu_Card_joker",
								247
							},
							{
								"BGNeiMaid",
								248
							},
							{
								"BGOdogu_alicesoft_bluehoney",
								249
							},
							{
								"BGOdogu_alicesoft_brownhoney",
								250
							},
							{
								"BGOdogu_alicesoft_greenhoney",
								251
							},
							{
								"BGOdogu_alicesoft_redhoney",
								252
							},
							{
								"BGOdogu_sp001_beercan",
								253
							},
							{
								"BGOdogu_sp001_beerjug",
								254
							},
							{
								"BGOdogu_sp001_cake",
								255
							},
							{
								"BGOdogu_sp001_food",
								256
							},
							{
								"BGOdogu_sp001_yakisoba",
								257
							},
							{
								"BGOdogu_denkigai2018s_beachball_blue",
								258
							},
							{
								"BGOdogu_denkigai2018s_beachball_green",
								259
							},
							{
								"BGOdogu_denkigai2018s_beachball_red",
								260
							},
							{
								"BGOdogu_denkigai2018s_beachball_yellow",
								261
							},
							{
								"BGOdogu_denkigai2018s_coneice_chocomint",
								262
							},
							{
								"BGOdogu_denkigai2018s_coneice_strawberry",
								263
							},
							{
								"BGOdogu_denkigai2018s_coneice_vanilla",
								264
							},
							{
								"BGOdogu_denkigai2018s_melondish",
								265
							},
							{
								"BGOdogu_denkigai2018s_syatifloot",
								266
							},
							{
								"BGOdogu_denkigai2018s_toropicalicetea",
								267
							},
							{
								"BGOdogu_sp002_oharaibou",
								268
							},
							{
								"BGOdogu_sp002_omamori",
								269
							},
							{
								"BGOdogu_sp002_susuki",
								270
							},
							{
								"BGOdogu_sp002_takebouki",
								271
							},
							{
								"BGOdogu_sp002_tukimidango",
								272
							},
							{
								"BGOdogu_sp002_waraningyou",
								273
							},
							{
								"BGOdogu_sp002_waraningyou_gosunkugi",
								274
							},
							{
								"BGOdogu",
								275
							}
						};
					}
					if (!OdoguUIArray.TryGetValue(text, out num))
					{
						goto IL_4852;
					}
					switch (num)
					{
						case 0:
							doguCombo2List[i] = new GUIContent("拘束椅子");
							break;
						case 1:
							doguCombo2List[i] = new GUIContent("バージンロード");
							break;
						case 2:
							doguCombo2List[i] = new GUIContent("ロボねい人形");
							break;
						case 3:
							doguCombo2List[i] = new GUIContent("教室机");
							break;
						case 4:
							doguCombo2List[i] = new GUIContent("教室椅子");
							break;
						case 5:
							doguCombo2List[i] = new GUIContent("トランプタワー(小)");
							break;
						case 6:
							doguCombo2List[i] = new GUIContent("トランプタワー");
							break;
						case 7:
							doguCombo2List[i] = new GUIContent("Wライト");
							break;
						case 8:
							doguCombo2List[i] = new GUIContent("OXカメラ");
							break;
						case 9:
							doguCombo2List[i] = new GUIContent("レトロカメラ");
							break;
						case 10:
							doguCombo2List[i] = new GUIContent("PC");
							break;
						case 11:
							doguCombo2List[i] = new GUIContent("モニター");
							break;
						case 12:
							doguCombo2List[i] = new GUIContent("キーボード");
							break;
						case 13:
							doguCombo2List[i] = new GUIContent("マウス");
							break;
						case 14:
							doguCombo2List[i] = new GUIContent("参考書A");
							break;
						case 15:
							doguCombo2List[i] = new GUIContent("参考書B");
							break;
						case 16:
							doguCombo2List[i] = new GUIContent("参考書C");
							break;
						case 17:
							doguCombo2List[i] = new GUIContent("参考書D");
							break;
						case 18:
							doguCombo2List[i] = new GUIContent("参考書E");
							break;
						case 19:
							doguCombo2List[i] = new GUIContent("ペン(桃)");
							break;
						case 20:
							doguCombo2List[i] = new GUIContent("ペン(黒)");
							break;
						case 21:
							doguCombo2List[i] = new GUIContent("ペン(茶)");
							break;
						case 22:
							doguCombo2List[i] = new GUIContent("ペン(緑)");
							break;
						case 23:
							doguCombo2List[i] = new GUIContent("鉛筆(緑)");
							break;
						case 24:
							doguCombo2List[i] = new GUIContent("鉛筆(黒)");
							break;
						case 25:
							doguCombo2List[i] = new GUIContent("鉛筆(赤)");
							break;
						case 26:
							doguCombo2List[i] = new GUIContent("消しゴム(青)");
							break;
						case 27:
							doguCombo2List[i] = new GUIContent("消しゴム(紫)");
							break;
						case 28:
							doguCombo2List[i] = new GUIContent("消しゴム(黄)");
							break;
						case 29:
							doguCombo2List[i] = new GUIContent("スティック糊");
							break;
						case 30:
							doguCombo2List[i] = new GUIContent("コンドーム(閉)");
							break;
						case 31:
							doguCombo2List[i] = new GUIContent("コンドーム(開)");
							break;
						case 32:
							doguCombo2List[i] = new GUIContent("コンドーム(袋)");
							break;
						case 33:
							doguCombo2List[i] = new GUIContent("ソファー");
							break;
						case 34:
							doguCombo2List[i] = new GUIContent("ソファー(大)");
							break;
						case 35:
							doguCombo2List[i] = new GUIContent("ギロチン");
							break;
						case 36:
							doguCombo2List[i] = new GUIContent("三角木馬");
							break;
						case 37:
							doguCombo2List[i] = new GUIContent("三角木馬2");
							break;
						case 38:
							doguCombo2List[i] = new GUIContent("拘束台");
							break;
						case 39:
							doguCombo2List[i] = new GUIContent("クリスマスツリー");
							break;
						case 40:
							doguCombo2List[i] = new GUIContent("門松");
							break;
						case 41:
							doguCombo2List[i] = new GUIContent("キッチン");
							break;
						case 42:
							doguCombo2List[i] = new GUIContent("花とテーブル");
							break;
						case 43:
							doguCombo2List[i] = new GUIContent("華道");
							break;
						case 44:
							doguCombo2List[i] = new GUIContent("ドレッサー");
							break;
						case 45:
							doguCombo2List[i] = new GUIContent("教室机");
							break;
						case 46:
							doguCombo2List[i] = new GUIContent("華道椅子");
							break;
						case 47:
							doguCombo2List[i] = new GUIContent("ドレッサー椅子");
							break;
						case 48:
							doguCombo2List[i] = new GUIContent("メイド部屋椅子");
							break;
						case 49:
							doguCombo2List[i] = new GUIContent("ベンキ");
							break;
						case 50:
							doguCombo2List[i] = new GUIContent("スケベ椅子");
							break;
						case 51:
							doguCombo2List[i] = new GUIContent("マット");
							break;
						case 52:
							doguCombo2List[i] = new GUIContent("ツンデレ");
							break;
						case 53:
							doguCombo2List[i] = new GUIContent("純真");
							break;
						case 54:
							doguCombo2List[i] = new GUIContent("クール");
							break;
						case 55:
							doguCombo2List[i] = new GUIContent("まな板");
							break;
						case 56:
							doguCombo2List[i] = new GUIContent("鍋");
							break;
						case 57:
							doguCombo2List[i] = new GUIContent("ノート");
							break;
						case 58:
							doguCombo2List[i] = new GUIContent("参考書");
							break;
						case 59:
							doguCombo2List[i] = new GUIContent("洗濯かご");
							break;
						case 60:
							doguCombo2List[i] = new GUIContent("重ねたタオル");
							break;
						case 61:
							doguCombo2List[i] = new GUIContent("洗濯物");
							break;
						case 62:
							doguCombo2List[i] = new GUIContent("スクリーン");
							break;
						case 63:
							doguCombo2List[i] = new GUIContent("ワイングラス");
							break;
						case 64:
							doguCombo2List[i] = new GUIContent("ソファー(小)");
							break;
						case 65:
							doguCombo2List[i] = new GUIContent("ツンデレ");
							break;
						case 66:
							doguCombo2List[i] = new GUIContent("純真");
							break;
						case 67:
							doguCombo2List[i] = new GUIContent("クール");
							break;
						case 68:
							doguCombo2List[i] = new GUIContent("メガネ");
							break;
						case 69:
							doguCombo2List[i] = new GUIContent("ねい人形");
							break;
						case 70:
							doguCombo2List[i] = new GUIContent("ロボねい人形");
							break;
						case 71:
							doguCombo2List[i] = new GUIContent("テーブル");
							break;
						case 72:
							doguCombo2List[i] = new GUIContent("ディルドボックス");
							break;
						case 73:
							doguCombo2List[i] = new GUIContent("プレイエリア外");
							break;
						case 74:
							doguCombo2List[i] = new GUIContent("デスクトップスクリーン");
							break;
						case 75:
							doguCombo2List[i] = new GUIContent("チャーハン");
							break;
						case 76:
							doguCombo2List[i] = new GUIContent("餃子");
							break;
						case 77:
							doguCombo2List[i] = new GUIContent("麻婆豆腐");
							break;
						case 78:
							doguCombo2List[i] = new GUIContent("お茶");
							break;
						case 79:
							doguCombo2List[i] = new GUIContent("ご飯");
							break;
						case 80:
							doguCombo2List[i] = new GUIContent("箸");
							break;
						case 81:
							doguCombo2List[i] = new GUIContent("味噌汁");
							break;
						case 82:
							doguCombo2List[i] = new GUIContent("煮物");
							break;
						case 83:
							doguCombo2List[i] = new GUIContent("緑茶");
							break;
						case 84:
							doguCombo2List[i] = new GUIContent("チキンライス");
							break;
						case 85:
							doguCombo2List[i] = new GUIContent("コーヒー");
							break;
						case 86:
							doguCombo2List[i] = new GUIContent("コーンスープ");
							break;
						case 87:
							doguCombo2List[i] = new GUIContent("ハンバーグ");
							break;
						case 88:
							doguCombo2List[i] = new GUIContent("先割れスプーン");
							break;
						case 89:
							doguCombo2List[i] = new GUIContent("テーブル");
							break;
						case 90:
							doguCombo2List[i] = new GUIContent("中華テーブル");
							break;
						case 91:
							doguCombo2List[i] = new GUIContent("和食テーブル");
							break;
						case 92:
							doguCombo2List[i] = new GUIContent("洋食テーブル");
							break;
						case 93:
							doguCombo2List[i] = new GUIContent("エッチする時の台");
							break;
						case 94:
							doguCombo2List[i] = new GUIContent("猫");
							break;
						case 95:
							doguCombo2List[i] = new GUIContent("犬");
							break;
						case 96:
							doguCombo2List[i] = new GUIContent("ニワトリ");
							break;
						case 97:
							doguCombo2List[i] = new GUIContent("スズメ");
							break;
						case 98:
							doguCombo2List[i] = new GUIContent("バーベキューグリル");
							break;
						case 99:
							doguCombo2List[i] = new GUIContent("バケツ");
							break;
						case 100:
							doguCombo2List[i] = new GUIContent("クーラーボックス");
							break;
						case 101:
							doguCombo2List[i] = new GUIContent("ダーツ");
							break;
						case 102:
							doguCombo2List[i] = new GUIContent("ダーツボード");
							break;
						case 103:
							doguCombo2List[i] = new GUIContent("鍋");
							break;
						case 104:
							doguCombo2List[i] = new GUIContent("鍋");
							break;
						case 105:
							doguCombo2List[i] = new GUIContent("夏みかん");
							break;
						case 106:
							doguCombo2List[i] = new GUIContent("風呂椅子");
							break;
						case 107:
							doguCombo2List[i] = new GUIContent("アヒル");
							break;
						case 108:
							doguCombo2List[i] = new GUIContent("おぼん");
							break;
						case 109:
							doguCombo2List[i] = new GUIContent("とっくり");
							break;
						case 110:
							doguCombo2List[i] = new GUIContent("コーン皿");
							break;
						case 111:
							doguCombo2List[i] = new GUIContent("イモ皿");
							break;
						case 112:
							doguCombo2List[i] = new GUIContent("トマト皿");
							break;
						case 113:
							doguCombo2List[i] = new GUIContent("砂の城");
							break;
						case 114:
							doguCombo2List[i] = new GUIContent("砂山");
							break;
						case 115:
							doguCombo2List[i] = new GUIContent("筒花火");
							break;
						case 116:
							doguCombo2List[i] = new GUIContent("浮き輪");
							break;
						case 117:
							doguCombo2List[i] = new GUIContent("作物(コーン)");
							break;
						case 118:
							doguCombo2List[i] = new GUIContent("作物(月下美人)");
							break;
						case 119:
							doguCombo2List[i] = new GUIContent("作物(月下美人・咲)");
							break;
						case 120:
							doguCombo2List[i] = new GUIContent("作物(向日葵)");
							break;
						case 121:
							doguCombo2List[i] = new GUIContent("作物(夏みかん)");
							break;
						case 122:
							doguCombo2List[i] = new GUIContent("作物(スイカ)");
							break;
						case 123:
							doguCombo2List[i] = new GUIContent("作物(ザクロ)");
							break;
						case 124:
							doguCombo2List[i] = new GUIContent("");
							break;
						case 125:
							doguCombo2List[i] = new GUIContent("");
							break;
						case 126:
							doguCombo2List[i] = new GUIContent("");
							break;
						case 127:
							doguCombo2List[i] = new GUIContent("ラジオ");
							break;
						case 128:
							doguCombo2List[i] = new GUIContent("コーヒーメーカー");
							break;
						case 129:
							doguCombo2List[i] = new GUIContent("冷蔵庫");
							break;
						case 130:
							doguCombo2List[i] = new GUIContent("テーブル");
							break;
						case 131:
							doguCombo2List[i] = new GUIContent("テレビリモコン");
							break;
						case 132:
							doguCombo2List[i] = new GUIContent("ワインセラー");
							break;
						case 133:
							doguCombo2List[i] = new GUIContent("サイドボード");
							break;
						case 134:
							doguCombo2List[i] = new GUIContent("ねい人形USB");
							break;
						case 135:
							doguCombo2List[i] = new GUIContent("輪投げ");
							break;
						case 136:
							doguCombo2List[i] = new GUIContent("輪");
							break;
						case 137:
							doguCombo2List[i] = new GUIContent("パフェ");
							break;
						case 138:
							doguCombo2List[i] = new GUIContent("フライドポテト");
							break;
						case 139:
							doguCombo2List[i] = new GUIContent("カラオケテーブル");
							break;
						case 140:
							doguCombo2List[i] = new GUIContent("オムライスH");
							break;
						case 141:
							doguCombo2List[i] = new GUIContent("オムライス顔1");
							break;
						case 142:
							doguCombo2List[i] = new GUIContent("オムライス顔2");
							break;
						case 143:
							doguCombo2List[i] = new GUIContent("オムライスおっぱい");
							break;
						case 144:
							doguCombo2List[i] = new GUIContent("かき氷");
							break;
						case 145:
							doguCombo2List[i] = new GUIContent("スナックプレート");
							break;
						case 146:
							doguCombo2List[i] = new GUIContent("箱");
							break;
						case 147:
							doguCombo2List[i] = new GUIContent("スタンドマイク");
							break;
						case 148:
							doguCombo2List[i] = new GUIContent("スタンドマイクベース");
							break;
						case 149:
							doguCombo2List[i] = new GUIContent("コアラマイク");
							break;
						case 150:
							doguCombo2List[i] = new GUIContent("無垢椅子");
							break;
						case 151:
							doguCombo2List[i] = new GUIContent("真面目椅子");
							break;
						case 152:
							doguCombo2List[i] = new GUIContent("凛デレ椅子");
							break;
						case 153:
							doguCombo2List[i] = new GUIContent("ツンデレ椅子");
							break;
						case 154:
							doguCombo2List[i] = new GUIContent("クーデレ椅子");
							break;
						case 155:
							doguCombo2List[i] = new GUIContent("純真椅子");
							break;
						case 156:
							doguCombo2List[i] = new GUIContent("ふかふかチェア");
							break;
						case 157:
							doguCombo2List[i] = new GUIContent("ラブソファー");
							break;
						case 158:
							doguCombo2List[i] = new GUIContent("タブレットPC");
							break;
						case 159:
							doguCombo2List[i] = new GUIContent("スタイラスペン(黒)");
							break;
						case 160:
							doguCombo2List[i] = new GUIContent("スタイラスペン(白)");
							break;
						case 161:
							doguCombo2List[i] = new GUIContent("スタイラスペン(赤)");
							break;
						case 162:
							doguCombo2List[i] = new GUIContent("スタイラスペン(青)");
							break;
						case 163:
							doguCombo2List[i] = new GUIContent("スタイラスペン(黄)");
							break;
						case 164:
							doguCombo2List[i] = new GUIContent("スタイラスペン(緑)");
							break;
						case 165:
							doguCombo2List[i] = new GUIContent("オムライス1");
							break;
						case 166:
							doguCombo2List[i] = new GUIContent("オムライス3");
							break;
						case 167:
							doguCombo2List[i] = new GUIContent("オムライスH");
							break;
						case 168:
							doguCombo2List[i] = new GUIContent("オムライス顔1");
							break;
						case 169:
							doguCombo2List[i] = new GUIContent("オムライス顔2");
							break;
						case 170:
							doguCombo2List[i] = new GUIContent("オムライスおっぱい");
							break;
						case 171:
							doguCombo2List[i] = new GUIContent("アクアパッザ");
							break;
						case 172:
							doguCombo2List[i] = new GUIContent("サンドイッチ");
							break;
						case 173:
							doguCombo2List[i] = new GUIContent("スープ");
							break;
						case 174:
							doguCombo2List[i] = new GUIContent("バースデーケーキ");
							break;
						case 175:
							doguCombo2List[i] = new GUIContent("ショートケーキ");
							break;
						case 176:
							doguCombo2List[i] = new GUIContent("モンブラン");
							break;
						case 177:
							doguCombo2List[i] = new GUIContent("パフェ");
							break;
						case 178:
							doguCombo2List[i] = new GUIContent("スムージー・赤");
							break;
						case 179:
							doguCombo2List[i] = new GUIContent("スムージー・緑");
							break;
						case 180:
							doguCombo2List[i] = new GUIContent("カクテル・赤");
							break;
						case 181:
							doguCombo2List[i] = new GUIContent("カクテル・青");
							break;
						case 182:
							doguCombo2List[i] = new GUIContent("カクテル・黄");
							break;
						case 183:
							doguCombo2List[i] = new GUIContent("コーヒーカップ");
							break;
						case 184:
							doguCombo2List[i] = new GUIContent("ワインボトル");
							break;
						case 185:
							doguCombo2List[i] = new GUIContent("ワインボトル(蓋)");
							break;
						case 186:
							doguCombo2List[i] = new GUIContent("如雨露");
							break;
						case 187:
							doguCombo2List[i] = new GUIContent("プランター(赤)");
							break;
						case 188:
							doguCombo2List[i] = new GUIContent("プランター(青)");
							break;
						case 189:
							doguCombo2List[i] = new GUIContent("マリーゴールド");
							break;
						case 190:
							doguCombo2List[i] = new GUIContent("カジノチップ10");
							break;
						case 191:
							doguCombo2List[i] = new GUIContent("カジノチップ100");
							break;
						case 192:
							doguCombo2List[i] = new GUIContent("カジノチップ1000");
							break;
						case 193:
							doguCombo2List[i] = new GUIContent("カードシューター");
							break;
						case 194:
							doguCombo2List[i] = new GUIContent("カードデッキ");
							break;
						case 195:
							doguCombo2List[i] = new GUIContent("カード・スペードA");
							break;
						case 196:
							doguCombo2List[i] = new GUIContent("カード・スペード2");
							break;
						case 197:
							doguCombo2List[i] = new GUIContent("カード・スペード3");
							break;
						case 198:
							doguCombo2List[i] = new GUIContent("カード・スペード4");
							break;
						case 199:
							doguCombo2List[i] = new GUIContent("カード・スペード5");
							break;
						case 200:
							doguCombo2List[i] = new GUIContent("カード・スペード6");
							break;
						case 201:
							doguCombo2List[i] = new GUIContent("カード・スペード7");
							break;
						case 202:
							doguCombo2List[i] = new GUIContent("カード・スペード8");
							break;
						case 203:
							doguCombo2List[i] = new GUIContent("カード・スペード9");
							break;
						case 204:
							doguCombo2List[i] = new GUIContent("カード・スペード10");
							break;
						case 205:
							doguCombo2List[i] = new GUIContent("カード・スペードJ");
							break;
						case 206:
							doguCombo2List[i] = new GUIContent("カード・スペードQ");
							break;
						case 207:
							doguCombo2List[i] = new GUIContent("カード・スペードK");
							break;
						case 208:
							doguCombo2List[i] = new GUIContent("カード・ハートA");
							break;
						case 209:
							doguCombo2List[i] = new GUIContent("カード・ハート2");
							break;
						case 210:
							doguCombo2List[i] = new GUIContent("カード・ハート3");
							break;
						case 211:
							doguCombo2List[i] = new GUIContent("カード・ハート4");
							break;
						case 212:
							doguCombo2List[i] = new GUIContent("カード・ハート5");
							break;
						case 213:
							doguCombo2List[i] = new GUIContent("カード・ハート6");
							break;
						case 214:
							doguCombo2List[i] = new GUIContent("カード・ハート7");
							break;
						case 215:
							doguCombo2List[i] = new GUIContent("カード・ハート8");
							break;
						case 216:
							doguCombo2List[i] = new GUIContent("カード・ハート9");
							break;
						case 217:
							doguCombo2List[i] = new GUIContent("カード・ハート10");
							break;
						case 218:
							doguCombo2List[i] = new GUIContent("カード・ハートJ");
							break;
						case 219:
							doguCombo2List[i] = new GUIContent("カード・ハートQ");
							break;
						case 220:
							doguCombo2List[i] = new GUIContent("カード・ハートK");
							break;
						case 221:
							doguCombo2List[i] = new GUIContent("カード・ダイヤA");
							break;
						case 222:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ2");
							break;
						case 223:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ3");
							break;
						case 224:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ4");
							break;
						case 225:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ5");
							break;
						case 226:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ6");
							break;
						case 227:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ7");
							break;
						case 228:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ8");
							break;
						case 229:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ9");
							break;
						case 230:
							doguCombo2List[i] = new GUIContent("カード・ダイヤ10");
							break;
						case 231:
							doguCombo2List[i] = new GUIContent("カード・ダイヤJ");
							break;
						case 232:
							doguCombo2List[i] = new GUIContent("カード・ダイヤQ");
							break;
						case 233:
							doguCombo2List[i] = new GUIContent("カード・ダイヤK");
							break;
						case 234:
							doguCombo2List[i] = new GUIContent("カード・クラブA");
							break;
						case 235:
							doguCombo2List[i] = new GUIContent("カード・クラブ2");
							break;
						case 236:
							doguCombo2List[i] = new GUIContent("カード・クラブ3");
							break;
						case 237:
							doguCombo2List[i] = new GUIContent("カード・クラブ4");
							break;
						case 238:
							doguCombo2List[i] = new GUIContent("カード・クラブ5");
							break;
						case 239:
							doguCombo2List[i] = new GUIContent("カード・クラブ6");
							break;
						case 240:
							doguCombo2List[i] = new GUIContent("カード・クラブ7");
							break;
						case 241:
							doguCombo2List[i] = new GUIContent("カード・クラブ8");
							break;
						case 242:
							doguCombo2List[i] = new GUIContent("カード・クラブ9");
							break;
						case 243:
							doguCombo2List[i] = new GUIContent("カード・クラブ10");
							break;
						case 244:
							doguCombo2List[i] = new GUIContent("カード・クラブJ");
							break;
						case 245:
							doguCombo2List[i] = new GUIContent("カード・クラブQ");
							break;
						case 246:
							doguCombo2List[i] = new GUIContent("カード・クラブK");
							break;
						case 247:
							doguCombo2List[i] = new GUIContent("カード・ジョーカー");
							break;
						case 248:
							doguCombo2List[i] = new GUIContent("メイドねい人形");
							break;
						case 249:
							doguCombo2List[i] = new GUIContent("ハニー(青)");
							break;
						case 250:
							doguCombo2List[i] = new GUIContent("ハニー(茶)");
							break;
						case 251:
							doguCombo2List[i] = new GUIContent("ハニー(緑)");
							break;
						case 252:
							doguCombo2List[i] = new GUIContent("ハニー(赤)");
							break;
						case 253:
							doguCombo2List[i] = new GUIContent("缶ビール");
							break;
						case 254:
							doguCombo2List[i] = new GUIContent("ジョッキビール");
							break;
						case 255:
							doguCombo2List[i] = new GUIContent("ケーキ");
							break;
						case 256:
							doguCombo2List[i] = new GUIContent("フードプレート");
							break;
						case 257:
							doguCombo2List[i] = new GUIContent("焼きそば");
							break;
						case 258:
							doguCombo2List[i] = new GUIContent("ビーチボール(青)");
							break;
						case 259:
							doguCombo2List[i] = new GUIContent("ビーチボール(緑)");
							break;
						case 260:
							doguCombo2List[i] = new GUIContent("ビーチボール(赤)");
							break;
						case 261:
							doguCombo2List[i] = new GUIContent("ビーチボール(黄)");
							break;
						case 262:
							doguCombo2List[i] = new GUIContent("アイス(チョコミント)");
							break;
						case 263:
							doguCombo2List[i] = new GUIContent("アイス(ストロベリー)");
							break;
						case 264:
							doguCombo2List[i] = new GUIContent("アイス(バニラ)");
							break;
						case 265:
							doguCombo2List[i] = new GUIContent("メロン");
							break;
						case 266:
							doguCombo2List[i] = new GUIContent("シャチ");
							break;
						case 267:
							doguCombo2List[i] = new GUIContent("トロピカルアイスティー");
							break;
						case 268:
							doguCombo2List[i] = new GUIContent("お祓い棒");
							break;
						case 269:
							doguCombo2List[i] = new GUIContent("お守り");
							break;
						case 270:
							doguCombo2List[i] = new GUIContent("ススキ");
							break;
						case 271:
							doguCombo2List[i] = new GUIContent("竹帚");
							break;
						case 272:
							doguCombo2List[i] = new GUIContent("月見団子");
							break;
						case 273:
							doguCombo2List[i] = new GUIContent("藁人形");
							break;
						case 274:
							doguCombo2List[i] = new GUIContent("藁人形(釘)");
							break;
						case 275:
							doguCombo2List[i] = new GUIContent("ハニー");
							break;
						default:
							goto IL_4852;
					}
				IL_48D1:
					i++;
					continue;
				IL_4852:
					string text2 = doguB1Array[i];
					for (int j = 0; j < doguNameList.Count; j++)
					{
						string[] array = doguNameList[j].Split(new char[]
						{
							','
						});
						if (text2 == array[0])
						{
							text2 = array[1];
						}
					}
					doguCombo2List[i] = new GUIContent(text2);
					goto IL_48D1;
				}
				parCombo1.selectedItemIndex = 0;
				parCombo1List = new GUIContent[parArray1.Length];
				i = 0;
				while (i < parArray1.Length)
				{
					string text = parArray1[i];
					if (text == null)
					{
						goto IL_50A8;
					}
					if (bgUiArrayB == null)
					{
						bgUiArrayB = new Dictionary<string, int>(46)
						{
							{
								"Salon:63",
								0
							},
							{
								"Salon:65",
								1
							},
							{
								"Salon:69",
								2
							},
							{
								"Salon_Entrance:3",
								3
							},
							{
								"Salon_Entrance:4",
								4
							},
							{
								"Salon_Entrance:1",
								5
							},
							{
								"Salon_Entrance:2",
								6
							},
							{
								"Salon_Entrance:0",
								7
							},
							{
								"Pool:26",
								8
							},
							{
								"Shitsumu:23",
								9
							},
							{
								"Shitsumu_Night:23",
								10
							},
							{
								"OutletPark:54",
								11
							},
							{
								"HoneymoonRoom:102",
								12
							},
							{
								"mirror1",
								13
							},
							{
								"mirror2",
								14
							},
							{
								"mirror3",
								15
							},
							{
								"Mob_Man_Stand001",
								16
							},
							{
								"Mob_Man_Stand002",
								17
							},
							{
								"Mob_Man_Stand003",
								18
							},
							{
								"Mob_Man_Sit001",
								19
							},
							{
								"Mob_Man_Sit002",
								20
							},
							{
								"Mob_Man_Sit003",
								21
							},
							{
								"Mob_Girl_Stand001",
								22
							},
							{
								"Mob_Girl_Stand002",
								23
							},
							{
								"Mob_Girl_Stand003",
								24
							},
							{
								"Mob_Girl_Sit001",
								25
							},
							{
								"Mob_Girl_Sit002",
								26
							},
							{
								"Mob_Girl_Sit003",
								27
							},
							{
								"p_dance_star_photo_ver",
								28
							},
							{
								"p_kamihubuki_photo_ver",
								29
							},
							{
								"p_mizu001_photo_ver",
								30
							},
							{
								"p_powder_snow2_photo_ver",
								31
							},
							{
								"p_powder_snow_photo_ver",
								32
							},
							{
								"p_smoke_dance_photo_ver",
								33
							},
							{
								"p_soap_bubble01_photo_ver",
								34
							},
							{
								"p_soap_bukubuku_photo_ver",
								35
							},
							{
								"p_soap_photo_ver",
								36
							},
							{
								"p_steam001_photo_ver",
								37
							},
							{
								"p_steam002_photo_ver",
								38
							},
							{
								"p_steam_black_photo_ver",
								39
							},
							{
								"p_yuge_large_photo_ver",
								40
							},
							{
								"Particle/pLineP02",
								41
							},
							{
								"Particle/pLineY",
								42
							},
							{
								"Particle/pLine_act2",
								43
							},
							{
								"Particle/pstarY_act2",
								44
							},
							{
								"Particle/pHeart01",
								45
							}
						};
					}
					if (!bgUiArrayB.TryGetValue(text, out num))
					{
						goto IL_50A8;
					}
					switch (num)
					{
						case 0:
							parCombo1List[i] = new GUIContent("ステージライト(赤)");
							break;
						case 1:
							parCombo1List[i] = new GUIContent("ステージライト(黄)");
							break;
						case 2:
							parCombo1List[i] = new GUIContent("ステージライト(青)");
							break;
						case 3:
							parCombo1List[i] = new GUIContent("ドア(左)");
							break;
						case 4:
							parCombo1List[i] = new GUIContent("ドア(右)");
							break;
						case 5:
							parCombo1List[i] = new GUIContent("ホールドア(左)");
							break;
						case 6:
							parCombo1List[i] = new GUIContent("ホールドア(右)");
							break;
						case 7:
							parCombo1List[i] = new GUIContent("エントランス(扉無し)");
							break;
						case 8:
							parCombo1List[i] = new GUIContent("水面");
							break;
						case 9:
							parCombo1List[i] = new GUIContent("執務室(外・昼)");
							break;
						case 10:
							parCombo1List[i] = new GUIContent("執務室(外・夜)");
							break;
						case 11:
							parCombo1List[i] = new GUIContent("青空");
							break;
						case 12:
							parCombo1List[i] = new GUIContent("夜景");
							break;
						case 13:
							parCombo1List[i] = new GUIContent("鏡");
							break;
						case 14:
							parCombo1List[i] = new GUIContent("鏡(縦長)");
							break;
						case 15:
							parCombo1List[i] = new GUIContent("鏡(メイド部屋用)");
							break;
						case 16:
							parCombo1List[i] = new GUIContent("モブ男1");
							break;
						case 17:
							parCombo1List[i] = new GUIContent("モブ男2");
							break;
						case 18:
							parCombo1List[i] = new GUIContent("モブ男3");
							break;
						case 19:
							parCombo1List[i] = new GUIContent("モブ男1 座り");
							break;
						case 20:
							parCombo1List[i] = new GUIContent("モブ男2 座り");
							break;
						case 21:
							parCombo1List[i] = new GUIContent("モブ男3 座り");
							break;
						case 22:
							parCombo1List[i] = new GUIContent("モブ女1");
							break;
						case 23:
							parCombo1List[i] = new GUIContent("モブ女2");
							break;
						case 24:
							parCombo1List[i] = new GUIContent("モブ女3");
							break;
						case 25:
							parCombo1List[i] = new GUIContent("モブ女1 座り");
							break;
						case 26:
							parCombo1List[i] = new GUIContent("モブ女2 座り");
							break;
						case 27:
							parCombo1List[i] = new GUIContent("モブ女3 座り");
							break;
						case 28:
							parCombo1List[i] = new GUIContent("星");
							break;
						case 29:
							parCombo1List[i] = new GUIContent("紙吹雪");
							break;
						case 30:
							parCombo1List[i] = new GUIContent("水");
							break;
						case 31:
							parCombo1List[i] = new GUIContent("粉雪2");
							break;
						case 32:
							parCombo1List[i] = new GUIContent("粉雪");
							break;
						case 33:
							parCombo1List[i] = new GUIContent("煙");
							break;
						case 34:
							parCombo1List[i] = new GUIContent("泡(空間)");
							break;
						case 35:
							parCombo1List[i] = new GUIContent("泡");
							break;
						case 36:
							parCombo1List[i] = new GUIContent("手元の泡");
							break;
						case 37:
							parCombo1List[i] = new GUIContent("湯気1");
							break;
						case 38:
							parCombo1List[i] = new GUIContent("スチーム");
							break;
						case 39:
							parCombo1List[i] = new GUIContent("スチーム(黒)");
							break;
						case 40:
							parCombo1List[i] = new GUIContent("湯気2");
							break;
						case 41:
							parCombo1List[i] = new GUIContent("ライン：ハート");
							break;
						case 42:
							parCombo1List[i] = new GUIContent("ライン：星");
							break;
						case 43:
							parCombo1List[i] = new GUIContent("星2");
							break;
						case 44:
							parCombo1List[i] = new GUIContent("流れ星");
							break;
						case 45:
							parCombo1List[i] = new GUIContent("ハート");
							break;
						default:
							goto IL_50A8;
					}
				IL_50C1:
					i++;
					continue;
				IL_50A8:
					parCombo1List[i] = new GUIContent(parArray1[i]);
					goto IL_50C1;
				}
				parCombo.selectedItemIndex = 0;
				parComboList = new GUIContent[parArray.Length];
				for (i = 0; i < parArray.Length; i++)
				{
					string text3 = parArray[i];
					parComboList[i] = new GUIContent(parArray[i]);
				}
				lightCombo.selectedItemIndex = 0;
				lightList = new List<GameObject>();
				lightList.Add(GameMain.Instance.MainLight.gameObject);
				lightComboList = new GUIContent[lightList.Count];
				for (i = 0; i < lightList.Count; i++)
				{
					if (i == 0)
					{
						lightComboList[i] = new GUIContent("メイン");
					}
					else
					{
						lightComboList[i] = new GUIContent("追加" + i);
					}
				}
				kankyoCombo.selectedItemIndex = 0;
				kankyoComboList = new GUIContent[kankyoMax];
				for (i = 0; i < kankyoMax; i++)
				{
					IniKey iniKey = base.Preferences["kankyo"]["kankyo" + (i + 1)];
					kankyoComboList[i] = new GUIContent(iniKey.Value);
				}
				kankyoInitFlg = true;
			}
			listStyle3.padding.top = GetPix(1);
			listStyle3.padding.bottom = GetPix(0);
			listStyle3.fontSize = GetPix(12);
			if (poseCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (poseGroupCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (sceneLevel == 3 || sceneLevel == 5 || isF6)
			{
				if (!isF6)
				{
					bool value = true;
					if (faceFlg || poseFlg || sceneFlg || kankyoFlg || kankyo2Flg)
					{
						value = false;
					}
					if (GUI.Toggle(new Rect((float)GetPix(2), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), value, "配置", guistyle6))
					{
						faceFlg = false;
						poseFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
						bGui = true;
						isGuiInit = true;
					}
				}
				if (!yotogiFlg)
				{
					if (GUI.Toggle(new Rect((float)GetPix(42), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), poseFlg, "操作", guistyle6))
					{
						poseFlg = true;
						faceFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
					}
				}
				if (GUI.Toggle(new Rect((float)GetPix(82), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), faceFlg, "表情", guistyle6))
				{
					faceFlg = true;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					if (!faceFlg2)
					{
						isFaceInit = true;
						faceFlg2 = true;
						maidArray[selectMaidIndex].boMabataki = false;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					isFaceInit = true;
				}
				if (GUI.Toggle(new Rect((float)GetPix(122), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyoFlg, "環境", guistyle6))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = true;
					kankyo2Flg = false;
				}
				if (GUI.Toggle(new Rect((float)GetPix(162), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyo2Flg, "環2", guistyle6))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = true;
				}
				if (!line1)
				{
					line1 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.3f));
					line2 = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 0.6f));
				}
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(200), 2f), line1);
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(200), 1f), line2);
			}
			if (isDanceStop)
			{
				isStop[selectMaidIndex] = true;
				isDanceStop = false;
			}
			yotogiFlg = false;
			if (sceneLevel == 14)
			{
				if (GameObject.Find("/UI Root/YotogiPlayPanel/CommandViewer/SkillViewer/MaskGroup/SkillGroup/CommandParent/CommandUnit"))
				{
					yotogiFlg = true;
				}
			}
			if (!isF6)
			{
				if (GUI.Button(new Rect((float)GetPix(157), (float)GetPix(32), (float)GetPix(46), (float)GetPix(35)), "シーン\n 管 理", guistyle3))
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
						string path = string.Concat(new object[]
						{
							Path.GetFullPath(".\\"),
							"Mod\\AdvancedStudioModeScene\\",
							page * 10 + i + 1,
							".png"
						});
						if (File.Exists(path))
						{
							FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read);
							BinaryReader binaryReader = new BinaryReader(input);
							byte[] array2 = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
							byte[] value2 = new byte[]
							{
								array2[36],
								array2[35],
								array2[34],
								array2[33]
							};
							int count = BitConverter.ToInt32(value2, 0) - 8;
							byte[] bytes = array2.Skip(49).Take(count).ToArray<byte>();
							string text4 = Encoding.UTF8.GetString(bytes);
							text4 = AdvancedStudioMode.StringFromBase64Comp(text4);
							if (text4 != "")
							{
								string[] array3 = text4.Split(new char[]
								{
									'_'
								});
								if (array3.Length >= 2)
								{
									string[] array4 = array3[0].Split(new char[]
									{
										','
									});
									date[i] = array4[0];
									ninzu[i] = array4[1] + "人";
								}
							}
						}
						else
						{
							IniKey iniKey2 = base.Preferences["scene"]["s" + (page * 10 + i + 1)];
							if (iniKey2.Value != null && iniKey2.Value.ToString() != "")
							{
								string[] array3 = iniKey2.Value.Split(new char[]
								{
									'_'
								});
								if (array3.Length >= 2)
								{
									string[] array4 = array3[0].Split(new char[]
									{
										','
									});
									date[i] = array4[0];
									ninzu[i] = array4[1] + "人";
								}
							}
						}
						//IL_59EB:
						i++;
						continue;
						//goto IL_59EB;
					}
				}
			}
			if (parCombo.isClickedComboButton || bgCombo.isClickedComboButton || bgmCombo.isClickedComboButton || lightCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			int num2 = -1;
			if (lightIndex[selectLightIndex] == 0)
			{
				isIdx1 = true;
			}
			if (lightIndex[selectLightIndex] == 1)
			{
				isIdx2 = true;
			}
			if (lightIndex[selectLightIndex] == 2)
			{
				isIdx3 = true;
			}
			if (lightIndex[selectLightIndex] == 3)
			{
				isIdx4 = true;
			}
			if (GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(185), (float)GetPix(45), (float)GetPix(20)), isIdx1, "通常", guistyle6))
			{
				if (lightIndex[selectLightIndex] != 0)
				{
					isIdx1 = true;
					isIdx2 = false;
					isIdx3 = false;
					isIdx4 = false;
					num2 = 0;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(50), (float)GetPix(185), (float)GetPix(45), (float)GetPix(20)), isIdx2, "Spot", guistyle6))
			{
				if (lightIndex[selectLightIndex] != 1)
				{
					isIdx1 = false;
					isIdx2 = true;
					isIdx3 = false;
					isIdx4 = false;
					num2 = 1;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(99), (float)GetPix(185), (float)GetPix(45), (float)GetPix(20)), isIdx3, "Point", guistyle6))
			{
				if (lightIndex[selectLightIndex] != 2)
				{
					isIdx1 = false;
					isIdx2 = false;
					isIdx3 = true;
					isIdx4 = false;
					num2 = 2;
				}
			}
			if (selectLightIndex == 0)
			{
				if (GUI.Toggle(new Rect((float)GetPix(150), (float)GetPix(185), (float)GetPix(45), (float)GetPix(20)), isIdx4, "単色", guistyle6))
				{
					if (lightIndex[selectLightIndex] != 3)
					{
						isIdx1 = false;
						isIdx2 = false;
						isIdx3 = false;
						isIdx4 = true;
						num2 = 3;
					}
				}
			}
			else if (GUI.Toggle(new Rect((float)GetPix(150), (float)GetPix(185), (float)GetPix(45), (float)GetPix(20)), isIdx4, "無効", guistyle6))
			{
				if (lightIndex[selectLightIndex] != 3)
				{
					isIdx1 = false;
					isIdx2 = false;
					isIdx3 = false;
					isIdx4 = true;
					num2 = 3;
				}
			}
			if (num2 >= 0)
			{
				lightIndex[selectLightIndex] = num2;
				if (selectLightIndex == 0)
				{
					GameMain.Instance.MainLight.Reset();
					GameMain.Instance.MainLight.SetIntensity(0.95f);
					GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 50f;
					GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
					GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
					if (lightIndex[selectLightIndex] == 0)
					{
						GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
						bgObject.SetActive(true);
						mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
					}
					else if (lightIndex[selectLightIndex] == 1)
					{
						GameMain.Instance.MainLight.transform.eulerAngles += Vector3.right * 40f;
						GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Spot;
						bgObject.SetActive(true);
						mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
					}
					else if (lightIndex[selectLightIndex] == 2)
					{
						GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Point;
						bgObject.SetActive(true);
						mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
					}
					else if (lightIndex[selectLightIndex] == 3)
					{
						GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
						mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
						bgObject.SetActive(false);
					}
				}
				else
				{
					lightList[selectLightIndex].SetActive(true);
					if (lightIndex[selectLightIndex] == 0)
					{
						lightList[selectLightIndex].GetComponent<Light>().type = LightType.Directional;
					}
					else if (lightIndex[selectLightIndex] == 1)
					{
						lightList[selectLightIndex].transform.eulerAngles += Vector3.right * 40f;
						lightList[selectLightIndex].GetComponent<Light>().type = LightType.Spot;
					}
					else if (lightIndex[selectLightIndex] == 2)
					{
						lightList[selectLightIndex].GetComponent<Light>().type = LightType.Point;
					}
					else if (lightIndex[selectLightIndex] == 3)
					{
						lightList[selectLightIndex].GetComponent<Light>().type = LightType.Directional;
						lightList[selectLightIndex].SetActive(false);
					}
				}
				lightColorR[selectLightIndex] = 1f;
				lightColorG[selectLightIndex] = 1f;
				lightColorB[selectLightIndex] = 1f;
				lightX[selectLightIndex] = 40f;
				lightY[selectLightIndex] = 180f;
				lightAkarusa[selectLightIndex] = 0.95f;
				lightKage[selectLightIndex] = 0.098f;
				lightRange[selectLightIndex] = 50f;
				if (lightIndex[selectLightIndex] == 1)
				{
					lightX[selectLightIndex] = 90f;
				}
			}
			GUI.Label(new Rect((float)GetPix(10), (float)GetPix(108), (float)GetPix(100), (float)GetPix(25)), "キューブ表示", guistyle2);
			guistyle6.fontSize = GetPix(12);
			isCube2 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(123), (float)GetPix(55), (float)GetPix(20)), isCube2, "大道具(", guistyle6);
			if (!isF6)
			{
				isCube = GUI.Toggle(new Rect((float)GetPix(102), (float)GetPix(123), (float)GetPix(54), (float)GetPix(20)), isCube, "メイド", guistyle6);
			}
			bool flag = GUI.Toggle(new Rect((float)GetPix(160), (float)GetPix(123), (float)GetPix(44), (float)GetPix(20)), isCube3, "背景", guistyle6);
			guistyle6.fontSize = GetPix(13);
			bool flag2 = GUI.Toggle(new Rect((float)GetPix(61), (float)GetPix(123), (float)GetPix(38), (float)GetPix(20)), isCubeS, "小)", guistyle6);
			if (isCubeS != flag2)
			{
				isCubeS = flag2;
				if (isCubeS)
				{
					cubeSize = 0.05f;
				}
				else
				{
					cubeSize = 0.12f;
				}
				for (int i = 0; i < doguBObject.Count; i++)
				{
					gDogu[i].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
				}
			}
			if (isCube3 != flag)
			{
				isCube3 = flag;
				if (gBg == null)
				{
					gBg = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gBg.GetComponent<Renderer>().material = m_material;
					gBg.layer = 8;
					gBg.GetComponent<Renderer>().enabled = false;
					gBg.SetActive(false);
					gBg.transform.position = bgObject.transform.position;
					mBg = gBg.AddComponent<MouseDrag6>();
					mBg.obj = gBg;
					mBg.maid = bgObject;
					mBg.angles = bg.eulerAngles;
					gBg.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
					mBg.ido = 1;
					mBg.isScale = false;
				}
				if (isCube3)
				{
					gBg.SetActive(true);
				}
				else
				{
					gBg.SetActive(false);
				}
			}
			int num3 = 0;
			if (lightIndex[selectLightIndex] == 0 || lightIndex[selectLightIndex] == 1 || (selectLightIndex == 0 && lightIndex[selectLightIndex] == 3))
			{
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(207), (float)GetPix(100), (float)GetPix(25)), "向きX", guistyle2);
				lightX[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(221), (float)GetPix(192), (float)GetPix(20)), lightX[selectLightIndex], 220f, -140f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(231), (float)GetPix(100), (float)GetPix(25)), "向きY", guistyle2);
				lightY[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(245), (float)GetPix(192), (float)GetPix(20)), lightY[selectLightIndex], 0f, 360f);
			}
			else
			{
				num3 = 50;
			}
			if (lightIndex[selectLightIndex] != 3 || selectLightIndex <= 0)
			{
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(255 - num3), (float)GetPix(100), (float)GetPix(25)), "明るさ", guistyle2);
				lightAkarusa[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(269 - num3), (float)GetPix(192), (float)GetPix(20)), lightAkarusa[selectLightIndex], 0f, 1.9f);
				if (lightIndex[selectLightIndex] == 0 || lightIndex[selectLightIndex] == 3)
				{
					if (selectLightIndex == 0)
					{
						GUI.Label(new Rect((float)GetPix(10), (float)GetPix(279 - num3), (float)GetPix(100), (float)GetPix(25)), "影", guistyle2);
						lightKage[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(293 - num3), (float)GetPix(192), (float)GetPix(20)), lightKage[selectLightIndex], 0f, 1f);
					}
					else
					{
						num3 = 25;
					}
				}
				else if (lightIndex[selectLightIndex] == 1 || lightIndex[selectLightIndex] == 2)
				{
					GUI.Label(new Rect((float)GetPix(10), (float)GetPix(281 - num3), (float)GetPix(100), (float)GetPix(25)), "範囲", guistyle2);
					lightRange[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(295 - num3), (float)GetPix(192), (float)GetPix(20)), lightRange[selectLightIndex], 0f, 150f);
				}
				else
				{
					num3 = 75;
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(303 - num3), (float)GetPix(100), (float)GetPix(25)), "赤", guistyle2);
				lightColorR[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(317 - num3), (float)GetPix(92), (float)GetPix(20)), lightColorR[selectLightIndex], 0f, 1f);
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(303 - num3), (float)GetPix(100), (float)GetPix(25)), "緑", guistyle2);
				lightColorG[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(317 - num3), (float)GetPix(92), (float)GetPix(20)), lightColorG[selectLightIndex], 0f, 1f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(327 - num3), (float)GetPix(100), (float)GetPix(25)), "青", guistyle2);
				lightColorB[selectLightIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(341 - num3), (float)GetPix(92), (float)GetPix(20)), lightColorB[selectLightIndex], 0f, 1f);
			}
			if (GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(361), (float)GetPix(67), (float)GetPix(20)), isBloomS, "ブルーム", guistyle6))
			{
				isBloomS = true;
				isDepthS = false;
				isBlurS = false;
				isFogS = false;
			}
			if (isBloomS)
			{
				isBloom = GUI.Toggle(new Rect((float)GetPix(8), (float)GetPix(382), (float)GetPix(40), (float)GetPix(20)), isBloom, "有効", guistyle6);
				if (!isBloom)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "強さ", guistyle2);
				bloom1 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), bloom1, 0f, 5.7f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "広さ", guistyle2);
				bloom2 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), bloom2, 0f, 15f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "赤", guistyle2);
				bloom3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), bloom3, 0f, 0.5f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "緑", guistyle2);
				bloom4 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), bloom4, 0f, 0.5f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(450), (float)GetPix(100), (float)GetPix(25)), "青", guistyle2);
				bloom5 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(465), (float)GetPix(92), (float)GetPix(20)), bloom5, 0f, 0.5f);
				isBloomA = GUI.Toggle(new Rect((float)GetPix(110), (float)GetPix(461), (float)GetPix(50), (float)GetPix(20)), isBloomA, "HDR", guistyle6);
				if (!parCombo.isClickedComboButton && !bgCombo.isClickedComboButton && !bgmCombo.isClickedComboButton)
				{
					GUI.enabled = true;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(73), (float)GetPix(361), (float)GetPix(40), (float)GetPix(20)), isDepthS, "深度", guistyle6))
			{
				isBloomS = false;
				isDepthS = true;
				isBlurS = false;
				isFogS = false;
			}
			if (isDepthS)
			{
				isDepth = GUI.Toggle(new Rect((float)GetPix(8), (float)GetPix(382), (float)GetPix(40), (float)GetPix(20)), isDepth, "有効", guistyle6);
				if (!isDepth)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "焦点距離", guistyle2);
				depth1 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(415), (float)GetPix(192), (float)GetPix(20)), depth1, 0f, 10f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "焦点領域サイズ", guistyle2);
				depth2 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), depth2, 0f, 2f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "カメラ絞り", guistyle2);
				depth3 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), depth3, 0f, 60f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(450), (float)GetPix(100), (float)GetPix(25)), "ブレ", guistyle2);
				depth4 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(465), (float)GetPix(92), (float)GetPix(20)), depth4, 0f, 10f);
				isDepthA = GUI.Toggle(new Rect((float)GetPix(110), (float)GetPix(461), (float)GetPix(100), (float)GetPix(20)), isDepthA, "深度表示", guistyle6);
				if (!parCombo.isClickedComboButton && !bgCombo.isClickedComboButton && !bgmCombo.isClickedComboButton)
				{
					GUI.enabled = true;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(114), (float)GetPix(361), (float)GetPix(40), (float)GetPix(20)), isBlurS, "光学", guistyle6))
			{
				isBloomS = false;
				isDepthS = false;
				isBlurS = true;
				isFogS = false;
			}
			if (isBlurS)
			{
				isBlur = GUI.Toggle(new Rect((float)GetPix(8), (float)GetPix(382), (float)GetPix(40), (float)GetPix(20)), isBlur, "有効", guistyle6);
				if (!isBlur)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "強さ", guistyle2);
				blur1 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), blur1, -40f, 70f);
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "ブラー", guistyle2);
				blur2 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), blur2, 0f, 5f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "ブレ", guistyle2);
				blur3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), blur3, 0f, 40f);
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "色収差", guistyle2);
				blur4 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), blur4, -30f, 30f);
				if (!parCombo.isClickedComboButton && !bgCombo.isClickedComboButton && !bgmCombo.isClickedComboButton)
				{
					GUI.enabled = true;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(155), (float)GetPix(361), (float)GetPix(60), (float)GetPix(20)), isFogS, "フォグ", guistyle6))
			{
				isBloomS = false;
				isDepthS = false;
				isBlurS = false;
				isFogS = true;
			}
			if (isFogS)
			{
				isFog = GUI.Toggle(new Rect((float)GetPix(8), (float)GetPix(382), (float)GetPix(40), (float)GetPix(20)), isFog, "有効", guistyle6);
				if (!isFog)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(376), (float)GetPix(100), (float)GetPix(24)), "発生距離", guistyle2);
				fog1 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(390), (float)GetPix(92), (float)GetPix(20)), fog1, 0f, 30f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "密度", guistyle2);
				fog2 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), fog2, 0f, 10f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(400), (float)GetPix(100), (float)GetPix(25)), "強度", guistyle2);
				fog3 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(415), (float)GetPix(92), (float)GetPix(20)), fog3, -5f, 20f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "高さ", guistyle2);
				fog4 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), fog4, -10f, 10f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(425), (float)GetPix(100), (float)GetPix(25)), "赤", guistyle2);
				fog5 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(440), (float)GetPix(92), (float)GetPix(20)), fog5, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(450), (float)GetPix(100), (float)GetPix(25)), "緑", guistyle2);
				fog6 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(465), (float)GetPix(92), (float)GetPix(20)), fog6, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(110), (float)GetPix(450), (float)GetPix(100), (float)GetPix(25)), "青", guistyle2);
				fog7 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(465), (float)GetPix(92), (float)GetPix(20)), fog7, 0f, 1f);
				if (!parCombo.isClickedComboButton && !bgCombo.isClickedComboButton && !bgmCombo.isClickedComboButton)
				{
					GUI.enabled = true;
				}
			}
			isSepian = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(485), (float)GetPix(80), (float)GetPix(20)), isSepian, "セピア", guistyle6);
			GUI.Label(new Rect((float)GetPix(108), (float)GetPix(482), (float)GetPix(100), (float)GetPix(25)), "ぼかし", guistyle2);
			bokashi = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(497), (float)GetPix(92), (float)GetPix(20)), bokashi, 0f, 18f);
			if (GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(515), (float)GetPix(80), (float)GetPix(20)), isHairSetting, "髪の設定", guistyle6))
			{
				isHairSetting = true;
				isSkirtSetting = false;
			}
			if (isHairSetting)
			{
				bool flag3 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(536), (float)GetPix(40), (float)GetPix(20)), isKamiyure, "有効", guistyle6);
				if (isKamiyure != flag3)
				{
					isKamiyure = flag3;
					if (isKamiyure)
					{
						base.Preferences["config"]["hair_setting"].Value = "true";
						base.Preferences["config"]["hair_radius"].Value = kamiyure4.ToString();
						base.Preferences["config"]["hair_elasticity"].Value = kamiyure3.ToString();
						base.Preferences["config"]["hair_damping"].Value = kamiyure2.ToString();
						base.SaveConfig();
					}
					else
					{
						base.Preferences["config"]["hair_setting"].Value = "false";
						base.SaveConfig();
						for (int k = 0; k < maidCnt; k++)
						{
							for (int l = 0; l < maidArray[k].body0.goSlot.Count; l++)
							{
								if (l >= 3 && l <= 6)
								{
									if (maidArray[k].body0.goSlot[l].obj != null)
									{
										DynamicBone component = maidArray[k].body0.goSlot[l].obj.GetComponent<DynamicBone>();
										if (component != null)
										{
											component.m_Damping = 0.6f;
											component.m_Elasticity = 1f;
											if (l == 5)
											{
												component.m_Elasticity = 0.05f;
											}
											component.m_Radius = 0.02f;
											component.UpdateParameters();
										}
									}
								}
							}
						}
					}
				}
				if (!isKamiyure)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(535), (float)GetPix(100), (float)GetPix(25)), "当たり判定半径", guistyle2);
				float num4 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(549), (float)GetPix(92), (float)GetPix(20)), kamiyure4, 0f, 0.04f);
				if (kamiyure4 != num4)
				{
					kamiyure4 = num4;
					base.Preferences["config"]["hair_radius"].Value = kamiyure4.ToString();
					base.SaveConfig();
				}
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(560), (float)GetPix(100), (float)GetPix(25)), "減衰率", guistyle2);
				float num5 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(574), (float)GetPix(92), (float)GetPix(20)), kamiyure2, 0.2f, 1f);
				if (kamiyure2 != num5)
				{
					kamiyure2 = num5;
					base.Preferences["config"]["hair_damping"].Value = kamiyure2.ToString();
					base.SaveConfig();
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(560), (float)GetPix(100), (float)GetPix(25)), "復元率", guistyle2);
				float num6 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(574), (float)GetPix(92), (float)GetPix(20)), kamiyure3, 0f, 2f);
				if (kamiyure3 != num6)
				{
					kamiyure3 = num6;
					base.Preferences["config"]["hair_elasticity"].Value = kamiyure3.ToString();
					base.SaveConfig();
				}
				GUI.enabled = true;
			}
			if (bgCombo.isClickedComboButton || bgmCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (GUI.Toggle(new Rect((float)GetPix(105), (float)GetPix(515), (float)GetPix(100), (float)GetPix(20)), isSkirtSetting, "スカート設定", guistyle6))
			{
				isHairSetting = false;
				isSkirtSetting = true;
			}
			if (isSkirtSetting)
			{
				bool flag4 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(536), (float)GetPix(40), (float)GetPix(20)), isSkirtyure, "有効", guistyle6);
				if (isSkirtyure != flag4)
				{
					isSkirtyure = flag4;
					if (isSkirtyure)
					{
						base.Preferences["config"]["skirt_setting"].Value = "true";
						base.Preferences["config"]["skirt_radius"].Value = skirtyure4.ToString();
						base.Preferences["config"]["skirt_elasticity"].Value = skirtyure3.ToString();
						base.Preferences["config"]["skirt_damping"].Value = skirtyure2.ToString();
						base.SaveConfig();
					}
					else
					{
						base.Preferences["config"]["skirt_setting"].Value = "false";
						base.SaveConfig();
						for (int k = 0; k < maidCnt; k++)
						{
							for (int l = 0; l < maidArray[k].body0.goSlot.Count; l++)
							{
								if (maidArray[k].body0.goSlot[l].obj != null)
								{
									DynamicSkirtBone fieldValue = AdvancedStudioMode.GetFieldValue<BoneHair3, DynamicSkirtBone>(maidArray[k].body0.goSlot[l].bonehair3, "m_SkirtBone");
									if (fieldValue != null)
									{
										fieldValue.m_fPanierForce = 0.05f;
										fieldValue.m_fPanierForceDistanceThreshold = 0.1f;
										fieldValue.m_fRegDefaultRadius = 0.1f;
									}
								}
							}
						}
					}
				}
				if (!isSkirtyure)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(535), (float)GetPix(100), (float)GetPix(25)), "足側カプセル半径", guistyle2);
				float num4 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(549), (float)GetPix(92), (float)GetPix(20)), skirtyure4, 0f, 0.2f);
				if (skirtyure4 != num4)
				{
					skirtyure4 = num4;
					base.Preferences["config"]["skirt_radius"].Value = skirtyure4.ToString();
					base.SaveConfig();
				}
				GUI.Label(new Rect((float)GetPix(108), (float)GetPix(560), (float)GetPix(100), (float)GetPix(25)), "足との距離パニエ力", guistyle2);
				float num5 = GUI.HorizontalSlider(new Rect((float)GetPix(108), (float)GetPix(574), (float)GetPix(92), (float)GetPix(20)), skirtyure2, 0f, 0.2f);
				if (skirtyure2 != num5)
				{
					skirtyure2 = num5;
					base.Preferences["config"]["skirt_damping"].Value = skirtyure2.ToString();
					base.SaveConfig();
				}
				GUI.Label(new Rect((float)GetPix(10), (float)GetPix(560), (float)GetPix(100), (float)GetPix(25)), "パニエ力", guistyle2);
				float num6 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(574), (float)GetPix(92), (float)GetPix(20)), skirtyure3, 0f, 0.1f);
				if (skirtyure3 != num6)
				{
					skirtyure3 = num6;
					base.Preferences["config"]["skirt_elasticity"].Value = skirtyure3.ToString();
					base.SaveConfig();
				}
				GUI.enabled = true;
			}
			GUI.enabled = true;
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(359), (float)GetPix(195), 2f), line1);
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(359), (float)GetPix(195), 1f), line2);
			if (bgCombo.isClickedComboButton || bgmCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(146), (float)GetPix(195), 2f), line1);
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(146), (float)GetPix(195), 1f), line2);
			GUI.Label(new Rect((float)GetPix(8), (float)GetPix(157), (float)GetPix(100), (float)GetPix(25)), "照明", guistyle2);
			listStyle3.padding.top = GetPix(3);
			listStyle3.padding.bottom = GetPix(2);
			listStyle3.fontSize = GetPix(13);
			int num7 = lightCombo.List(new Rect((float)GetPix(34), (float)GetPix(155), (float)GetPix(78), (float)GetPix(23)), lightComboList[selectLightIndex].text, lightComboList, guistyle4, "box", listStyle3);
			if (num7 != selectLightIndex)
			{
				selectLightIndex = num7;
				isIdx1 = false;
				isIdx2 = false;
				isIdx3 = false;
				isIdx4 = false;
			}
			if (GUI.Button(new Rect((float)GetPix(115), (float)GetPix(155), (float)GetPix(35), (float)GetPix(23)), "追加", guistyle3))
			{
				GameObject gameObject = new GameObject("Light");
				gameObject.AddComponent<Light>();
				lightList.Add(gameObject);
				lightColorR.Add(1f);
				lightColorG.Add(1f);
				lightColorB.Add(1f);
				lightIndex.Add(0);
				lightX.Add(40f);
				lightY.Add(180f);
				lightAkarusa.Add(0.95f);
				lightKage.Add(0.098f);
				lightRange.Add(50f);
				gameObject.transform.position = GameMain.Instance.MainLight.transform.position;
				selectLightIndex = lightList.Count - 1;
				lightComboList = new GUIContent[lightList.Count];
				for (int i = 0; i < lightList.Count; i++)
				{
					if (i == 0)
					{
						lightComboList[i] = new GUIContent("メイン");
					}
					else
					{
						lightComboList[i] = new GUIContent("追加" + i);
					}
				}
				lightCombo.selectedItemIndex = selectLightIndex;
				gameObject.GetComponent<Light>().intensity = 0.95f;
				gameObject.GetComponent<Light>().spotAngle = 50f;
				gameObject.GetComponent<Light>().range = 10f;
				gameObject.GetComponent<Light>().type = LightType.Directional;
				gameObject.GetComponent<Light>().color = new Color(0.5f, 1f, 0f);
				if (gLight[selectLightIndex] == null)
				{
					gLight[selectLightIndex] = GameObject.CreatePrimitive(PrimitiveType.Cube);
					Material material = new Material(Shader.Find("Transparent/Diffuse"));
					material.color = new Color(0.5f, 0.5f, 1f, 0.8f);
					gLight[selectLightIndex].GetComponent<Renderer>().material = material;
					gLight[selectLightIndex].layer = 8;
					gLight[selectLightIndex].GetComponent<Renderer>().enabled = false;
					gLight[selectLightIndex].SetActive(false);
					gLight[selectLightIndex].transform.position = gameObject.transform.position;
					mLight[selectLightIndex] = gLight[selectLightIndex].AddComponent<MouseDrag6>();
					mLight[selectLightIndex].obj = gLight[selectLightIndex];
					mLight[selectLightIndex].maid = gameObject.gameObject;
					mLight[selectLightIndex].angles = gameObject.gameObject.transform.eulerAngles;
					gLight[selectLightIndex].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
					mLight[selectLightIndex].ido = 1;
					mLight[selectLightIndex].isScale = false;
				}
			}
			if (GUI.Button(new Rect((float)GetPix(153), (float)GetPix(155), (float)GetPix(23), (float)GetPix(23)), "R", guistyle3))
			{
				for (int i = 1; i < lightList.Count; i++)
				{
					UnityEngine.Object.Destroy(lightList[i]);
				}
				lightIndex = new List<int>();
				lightIndex.Add(0);
				lightColorR = new List<float>();
				lightColorR.Add(1f);
				lightColorG = new List<float>();
				lightColorG.Add(1f);
				lightColorB = new List<float>();
				lightColorB.Add(1f);
				lightX = new List<float>();
				lightX.Add(40f);
				lightY = new List<float>();
				lightY.Add(180f);
				lightAkarusa = new List<float>();
				lightAkarusa.Add(0.95f);
				lightKage = new List<float>();
				lightKage.Add(0.098f);
				lightRange = new List<float>();
				lightRange.Add(50f);
				lightCombo.selectedItemIndex = 0;
				lightList = new List<GameObject>();
				lightList.Add(GameMain.Instance.MainLight.gameObject);
				lightComboList = new GUIContent[lightList.Count];
				for (int i = 0; i < lightList.Count; i++)
				{
					if (i == 0)
					{
						lightComboList[i] = new GUIContent("メイン");
					}
					else
					{
						lightComboList[i] = new GUIContent("追加" + i);
					}
				}
				selectLightIndex = 0;
				GameMain.Instance.MainLight.Reset();
				GameMain.Instance.MainLight.SetIntensity(0.95f);
				GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 50f;
				GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
				GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
				GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
				bgObject.SetActive(true);
				mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
				isIdx1 = false;
				isIdx2 = false;
				isIdx3 = false;
				isIdx4 = false;
			}
			GUI.enabled = true;
			if (bgCombo.isClickedComboButton || bgmCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			listStyle3.padding.top = GetPix(0);
			listStyle3.padding.bottom = GetPix(0);
			listStyle3.fontSize = GetPix(12);
			if (nameFlg)
			{
				inName2 = GUI.TextField(new Rect((float)GetPix(5), (float)GetPix(86), (float)GetPix(100), (float)GetPix(20)), inName2);
				if (GUI.Button(new Rect((float)GetPix(110), (float)GetPix(86), (float)GetPix(35), (float)GetPix(20)), "更新", guistyle3))
				{
					nameFlg = false;
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					base.Preferences["kankyo"]["kankyo" + (kankyoCombo.selectedItemIndex + 1)].Value = inName2;
					base.SaveConfig();
					kankyoComboList = new GUIContent[kankyoMax];
					for (int i = 0; i < kankyoMax; i++)
					{
						IniKey iniKey = base.Preferences["kankyo"]["kankyo" + (i + 1)];
						kankyoComboList[i] = new GUIContent(iniKey.Value);
					}
				}
			}
			else
			{
				if (GUI.Button(new Rect((float)GetPix(180), (float)GetPix(86), (float)GetPix(24), (float)GetPix(20)), "名", guistyle3))
				{
					nameFlg = true;
					inName2 = kankyoComboList[kankyoIndex].text;
				}
				kankyoIndex = kankyoCombo.List(new Rect((float)GetPix(4), (float)GetPix(86), (float)GetPix(91), (float)GetPix(23)), kankyoComboList[kankyoIndex].text, kankyoComboList, guistyle4, "box", listStyle3);
				if (GUI.Button(new Rect((float)GetPix(100), (float)GetPix(86), (float)GetPix(35), (float)GetPix(20)), "保存", guistyle3))
				{
					saveScene = 10000 + kankyoIndex;
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
				if (GUI.Button(new Rect((float)GetPix(140), (float)GetPix(86), (float)GetPix(35), (float)GetPix(20)), "読込", guistyle3))
				{
					loadScene = 10000 + kankyoIndex;
					kankyoLoadFlg = true;
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
				}
				GUI.enabled = false;
				IniKey iniKey2 = base.Preferences["scene"]["s" + (10000 + kankyoIndex)];
				if (iniKey2.Value != null && iniKey2.Value.ToString() != "")
				{
					GUI.enabled = true;
				}
			}
			GUI.enabled = true;
			if (bgmCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			listStyle3.padding.top = GetPix(1);
			listStyle3.padding.bottom = GetPix(0);
			listStyle3.fontSize = GetPix(12);
			int num8 = bgCombo.List(new Rect((float)GetPix(31), (float)GetPix(53), (float)GetPix(95), (float)GetPix(23)), bgComboList[bgIndex].text, bgComboList, guistyle4, "box", listStyle3);
			if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(53), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
			{
				num8--;
				if (num8 <= -1)
				{
					num8 = bgArray.Length - 1;
				}
			}
			if (GUI.Button(new Rect((float)GetPix(129), (float)GetPix(53), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
			{
				num8++;
				if (num8 == bgArray.Length)
				{
					num8 = 0;
				}
			}
			if (bgIndex != num8)
			{
				bgIndex = num8;
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
					Vector3 zero = Vector3.zero;
					Vector3 zero2 = Vector3.zero;
					zero2.y = 90f;
					zero.z = 4f;
					zero.x = 1f;
					bg.transform.localPosition = zero;
					bg.transform.localRotation = Quaternion.Euler(zero2);
				}
			}
			GUI.enabled = true;
			int num9 = bgmCombo.List(new Rect((float)GetPix(31), (float)GetPix(25), (float)GetPix(95), (float)GetPix(23)), bgmComboList[bgmIndex].text, bgmComboList, guistyle4, "box", listStyle3);
			if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(25), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
			{
				num9--;
				if (num9 <= -1)
				{
					num9 = bgmArray.Length - 1;
				}
			}
			if (GUI.Button(new Rect((float)GetPix(129), (float)GetPix(25), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
			{
				num9++;
				if (num9 == bgmArray.Length)
				{
					num9 = 0;
				}
			}
			if (bgmIndex != num9)
			{
				bgmIndex = num9;
				GameMain.Instance.SoundMgr.PlayBGM(bgmArray[bgmIndex] + ".ogg", 0f, true);
				bgmCombo.selectedItemIndex = bgmIndex;
			}
			if (bgmCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
		}

		private void GuiFunc4(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "label";
			guistyle2.fontSize = GetPix(11);
			guistyle2.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle3 = "button";
			guistyle3.fontSize = GetPix(20);
			GUIStyle guistyle4 = "button";
			guistyle4.fontSize = GetPix(14);
			guistyle4.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle5 = "button";
			guistyle5.fontSize = GetPix(12);
			guistyle5.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle6 = new GUIStyle("toggle");
			guistyle6.fontSize = GetPix(13);
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			Maid maid = maidArray[selectMaidIndex];
			if (!poseInitFlg)
			{
				listStyle2.normal.textColor = Color.white;
				listStyle2.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle2.onHover.background = (listStyle2.hover.background = new Texture2D(2, 2));
				RectOffset padding = listStyle2.padding;
				RectOffset padding2 = listStyle2.padding;
				RectOffset padding3 = listStyle2.padding;
				int num = listStyle2.padding.bottom = GetPix(0);
				num = (padding3.top = num);
				num = (padding2.right = num);
				padding.left = num;
				listStyle2.fontSize = GetPix(12);
				listStyle3.normal.textColor = Color.white;
				listStyle3.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle3.onHover.background = (listStyle3.hover.background = new Texture2D(2, 2));
				RectOffset padding4 = listStyle3.padding;
				RectOffset padding5 = listStyle3.padding;
				num = (listStyle3.padding.top = GetPix(1));
				num = (padding5.right = num);
				padding4.left = num;
				listStyle3.padding.bottom = GetPix(0);
				listStyle3.fontSize = GetPix(12);
				listStyle4.normal.textColor = Color.white;
				listStyle4.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle4.onHover.background = (listStyle4.hover.background = new Texture2D(2, 2));
				RectOffset padding6 = listStyle4.padding;
				RectOffset padding7 = listStyle4.padding;
				num = (listStyle4.padding.top = 3);
				num = (padding7.right = num);
				padding6.left = num;
				listStyle4.padding.bottom = 3;
				listStyle4.fontSize = GetPix(13);
				poseCombo.selectedItemIndex = 0;
				int num2 = (int)groupList[0];
				poseComboList = new GUIContent[num2];
				for (int i = 0; i < num2; i++)
				{
					poseComboList[i] = new GUIContent(i + 1 + ":" + poseArray[i]);
				}
				poseGroupCombo.selectedItemIndex = 0;
				poseGroupComboList = new GUIContent[poseGroupArray.Length + 1];
				poseGroupComboList[0] = new GUIContent("1:通常");
				for (int i = 0; i < poseGroupArray.Length; i++)
				{
					if (poseGroupArray[i] == "maid_dressroom01")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":立ち");
					}
					if (poseGroupArray[i] == "tennis_kamae_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":中腰");
					}
					if (poseGroupArray[i] == "senakanagasi_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":膝をつく");
					}
					if (poseGroupArray[i] == "work_hansei")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":座り");
					}
					if (poseGroupArray[i] == "inu_taiki_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":四つん這い");
					}
					if (poseGroupArray[i] == "syagami_pose_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":床座り");
					}
					if (poseGroupArray[i] == "densyasuwari_taiki_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":椅子座り");
					}
					if (poseGroupArray[i] == "work_kaiwa")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":ソファー座り");
					}
					if (poseGroupArray[i] == "dance_cm3d2_001_f1,14.14")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":ドキドキ☆Fallin' Love");
					}
					if (poseGroupArray[i] == "dance_cm3d_001_f1,39.25")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":entrance to you");
					}
					if (poseGroupArray[i] == "dance_cm3d_002_end_f1,50.71")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":scarlet leap");
					}
					if (poseGroupArray[i] == "dance_cm3d2_002_smt_f,7.76,")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":stellar my tears");
					}
					if (poseGroupArray[i] == "dance_cm3d_003_sp2_f1,90.15")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":rhythmix to you");
					}
					if (poseGroupArray[i] == "dance_cm3d2_003_hs_f1,0.01,")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":happy!happy!スキャンダル!!");
					}
					if (poseGroupArray[i] == "dance_cm3d_004_kano_f1,124.93")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":Can Know Two Close");
					}
					if (poseGroupArray[i] == "dance_cm3d2_004_sse_f1,0.01")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":sweet sweet everyday");
					}
					if (poseGroupArray[i] == "turusi_sex_in_taiki_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":拘束");
					}
					if (poseGroupArray[i] == "rosyutu_pose01_f")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":エロ");
					}
					if (poseGroupArray[i] == "rosyutu_aruki_f_once_,1.37")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":歩き");
					}
					if (poseGroupArray[i] == "stand_desk1")
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":その他");
					}
					if (poseGroupArray[i] == poseArray5[0])
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":ダンスMC");
					}
					if (poseGroupArray[i] == poseArray6[0])
					{
						poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":ダンス");
					}
					if (existPose && strS != "")
					{
                        if (i == poseGroupArray.Length - 4)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":通常2");
                        }
                        if (i == poseGroupArray.Length - 3)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":エロ2");
                        }
                        if (i == poseGroupArray.Length - 2)
						{
							poseGroupComboList[i + 1] = new GUIContent("98:撮影モード");
						}
						if (i == poseGroupArray.Length - 1)
						{
							poseGroupComboList[i + 1] = new GUIContent("99:登録ポーズ");
						}
					}
					else if (existPose && strS == "")
					{
                        if (i == poseGroupArray.Length - 3)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":通常2");
                        }
                        if (i == poseGroupArray.Length - 2)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":エロ2");
                        }
                        if (i == poseGroupArray.Length - 1)
						{
							poseGroupComboList[i + 1] = new GUIContent("99:登録ポーズ");
						}
					}
					else if (!existPose && strS != "")
					{
                        if (i == poseGroupArray.Length - 3)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":通常2");
                        }
                        if (i == poseGroupArray.Length - 2)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":エロ2");
                        }
                        if (i == poseGroupArray.Length - 1)
						{
							poseGroupComboList[i + 1] = new GUIContent("98:撮影モード");
						}
					}
                    else
                    {
                        if (i == poseGroupArray.Length - 2)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":通常2");
                        }
                        if (i == poseGroupArray.Length - 1)
                        {
                            poseGroupComboList[i + 1] = new GUIContent(i + 2 + ":エロ2");
                        }
                    }
                }
				poseInitFlg = true;
				itemCombo.selectedItemIndex = 0;
				num2 = itemArray.Length;
				itemComboList = new GUIContent[num2 - 1];
				for (int i = 0; i < num2; i++)
				{
					if (i == 0)
					{
						itemComboList[i] = new GUIContent("アイテム無し");
					}
					else
					{
						string text = itemArray[i];
						switch (text)
						{
							case "handitem,HandItemR_WineGlass_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ワイングラス");
								break;
							case "handitem,HandItemR_WineBottle_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ワインボトル");
								break;
							case "handitem,handitemr_racket_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ラケット");
								break;
							case "handitem,HandItemR_Hataki_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ハタキ");
								break;
							case "handitem,HandItemR_Mop_I_.menu":
								itemComboList[i] = new GUIContent(i + ":モップ");
								break;
							case "handitem,HandItemR_Houki_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ほうき");
								break;
							case "handitem,HandItemR_Zoukin2_I_.menu":
								itemComboList[i] = new GUIContent(i + ":雑巾");
								break;
							case "handitem,HandItemR_Chu-B_Lip_I_.menu":
								itemComboList[i] = new GUIContent(i + ":Chu-B Lip");
								break;
							case "handitem,HandItemR_Mimikaki_I_.menu":
								itemComboList[i] = new GUIContent(i + ":耳かき");
								break;
							case "handitem,HandItemR_Pen_I_.menu":
								itemComboList[i] = new GUIContent(i + ":ペン");
								break;
							case "handitem,HandItemR_Otama_I_.menu":
								itemComboList[i] = new GUIContent(i + ":おたま");
								break;
							case "handitem,HandItemR_Houchou_I_.menu":
								itemComboList[i] = new GUIContent(i + ":包丁");
								break;
							case "handitem,HandItemR_Book_I_.menu":
								itemComboList[i] = new GUIContent(i + ":本");
								break;
							case "handitem,HandItemR_Puff_I_.menu":
								itemComboList[i] = new GUIContent(i + ":パフ");
								break;
							case "handitem,HandItemR_Rip_I_.menu":
								itemComboList[i] = new GUIContent(i + ":リップ");
								break;
							case "handitem,HandItemD_Shisyuu_Hari_I_.menu":
								itemComboList[i] = new GUIContent(i + ":刺繍");
								break;
							case "handitem,HandItemD_Sara_Sponge_I_.menu":
								itemComboList[i] = new GUIContent(i + ":皿・スポンジ");
								break;
							case "kousoku_upper,KousokuU_TekaseOne_I_.menu":
								itemComboList[i] = new GUIContent(i + ":手枷1");
								break;
							case "kousoku_upper,KousokuU_TekaseTwo_I_.menu":
								itemComboList[i] = new GUIContent(i + ":手枷2");
								break;
							case "kousoku_lower,KousokuL_AshikaseUp_I_.menu":
								itemComboList[i] = new GUIContent(i + ":足枷");
								break;
							case "handitem,HandItemR_Usuba_Houchou_I_.menu":
								itemComboList[i] = new GUIContent(i + "薄刃包丁");
								break;
							case "handitem,HandItemR_Chusyaki_I_.menu":
								itemComboList[i] = new GUIContent(i + "注射器");
								break;
							case "handitem,HandItemR_Nei_Heartful_I_.menu":
								itemComboList[i] = new GUIContent(i + "ハートフルねい人形");
								break;
							case "handitem,HandItemR_Shaker_I_.menu":
								itemComboList[i] = new GUIContent(i + "シェイカー");
								break;
							case "handitem,HandItemR_SmartPhone_I_.menu":
								itemComboList[i] = new GUIContent(i + "スマートフォン");
								break;
							case "kousoku_upper,KousokuU_Ushirode_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":後ろ手拘束具");
								break;
							case "kousoku_upper,KousokuU_SMRoom_Haritsuke_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":磔台・手枷足枷");
								break;
							case "kousoku_upper,KousokuU_SMRoom2_Haritsuke_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":磔台・手枷足枷2");
								break;
							case "handitem,HandItemL_Dance_Hataki_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ダンスハタキ");
								break;
							case "handitem,HandItemL_Dance_Mop_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ダンスモップ");
								break;
							case "handitem,HandItemL_Dance_Zoukin_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ダンス雑巾");
								break;
							case "handitem,HandItemL_Kozara_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":小皿");
								break;
							case "handitem,HandItemR_Teacup_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ティーカップ");
								break;
							case "handitem,HandItemL_Teasaucer_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ティーソーサー");
								break;
							case "handitem,HandItemR_Wholecake_I_.menu":
								itemComboList[i - 1] = new GUIContent("ホールケーキ");
								break;
							case "handitem,HandItemR_Menu_I_.menu":
								itemComboList[i - 1] = new GUIContent("メニュー表");
								break;
							case "handitem,HandItemR_Vibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":バイブ");
								break;
							case "handitem,HandItemR_VibePink_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":ピンクバイブ");
								break;
							case "handitem,HandItemR_VibeBig_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":太バイブ");
								break;
							case "handitem,HandItemR_AnalVibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":アナルバイブ");
								break;
							case "handitem,HandItemH_SoutouVibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":前：双頭バイブ");
								break;
							case "accvag,accVag_Vibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":前：バイブ");
								break;
							case "accvag,accVag_VibeBig_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":前：太バイブ");
								break;
							case "accvag,accVag_VibePink_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":前：ピンクバイブ");
								break;
							case "accanl,accAnl_AnalVibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":後：アナルバイブ");
								break;
							case "accanl,accAnl_Photo_NomalVibe_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":後：バイブ");
								break;
							case "accanl,accAnl_Photo_VibeBig_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":後：太バイブ");
								break;
							case "accanl,accAnl_Photo_VibePink_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + ":後：ピンクバイブ");
								break;
							case "handitem,HandItemL_Etoile_Saucer_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + "ティーソーサー");
								break;
							case "handitem,HandItemR_Etoile_Teacup_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + "ティーカップ");
								break;
							case "handitem,HandItemL_Katuramuki_Daikon_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + "桂むき大根");
								break;
							case "handitem,HandItemL_Karte_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + "カルテ");
								break;
							case "handitem,HandItemL_Cracker_I_.menu":
								itemComboList[i - 1] = new GUIContent(i - 1 + "クラッカー");
								break;
						}
						if (i == 12)
						{
							itemComboList[i] = new GUIContent(i + ":手枷・足枷");
						}
						if (i == 13)
						{
							itemComboList[i] = new GUIContent(i + ":手枷・足枷(下)");
						}
						if (i == 24)
						{
							itemComboList[i - 1] = new GUIContent(i - 1 + ":カップ＆ソーサー");
						}
					}
				}
			}
			if (poseCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (poseGroupCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (sceneLevel == 3 || sceneLevel == 5 || isF6)
			{
				if (!isF6)
				{
					bool value = true;
					if (faceFlg || poseFlg || sceneFlg || kankyoFlg || kankyo2Flg)
					{
						value = false;
					}
					if (GUI.Toggle(new Rect((float)GetPix(2), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), value, "配置", guistyle6))
					{
						faceFlg = false;
						poseFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
						bGui = true;
						isGuiInit = true;
					}
				}
				if (!yotogiFlg)
				{
					if (GUI.Toggle(new Rect((float)GetPix(41), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), poseFlg, "操作", guistyle6))
					{
						poseFlg = true;
						faceFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
					}
				}
				if (GUI.Toggle(new Rect((float)GetPix(80), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), faceFlg, "表情", guistyle6))
				{
					faceFlg = true;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					if (!faceFlg2)
					{
						isFaceInit = true;
						faceFlg2 = true;
						maidArray[selectMaidIndex].boMabataki = false;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					isFaceInit = true;
				}
				if (GUI.Toggle(new Rect((float)GetPix(119), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyoFlg, "環境", guistyle6))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = true;
					kankyo2Flg = false;
				}
				if (!line1)
				{
					line1 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.3f));
					line2 = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 0.6f));
				}
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 2f), line1);
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 1f), line2);
				guistyle.fontSize = GetPix(13);
				guistyle.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect((float)GetPix(125), (float)GetPix(25), (float)GetPix(40), (float)GetPix(25)), string.Concat(selectMaidIndex + 1), guistyle);
				guistyle.fontSize = GetPix(11);
				guistyle.alignment = TextAnchor.UpperLeft;
			}
			if (sceneLevel > 0)
			{
				int i = selectMaidIndex;
				if (sceneLevel == 3 || (sceneLevel == 5 && (isF7 || maidCnt > 1)))
				{
					if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(53), (float)GetPix(23), (float)GetPix(32)), "＜", guistyle3))
					{
						selectMaidIndex--;
						if (selectMaidIndex < 0)
						{
							selectMaidIndex = selectList.Count - 1;
						}
						isPoseInit = true;
						poseFlg = true;
						copyIndex = 0;
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex];
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(53), (float)GetPix(23), (float)GetPix(32)), "＞", guistyle3))
					{
						selectMaidIndex++;
						if (selectList.Count <= selectMaidIndex)
						{
							selectMaidIndex = 0;
						}
						isPoseInit = true;
						poseFlg = true;
						copyIndex = 0;
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex];
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
				}
				if (maidArray[selectMaidIndex].GetThumIcon())
				{
					GUI.DrawTexture(new Rect((float)GetPix(30), (float)GetPix(25), (float)GetPix(60), (float)GetPix(60)), maidArray[selectMaidIndex].GetThumIcon());
				}
				string text2 = maidArray[selectMaidIndex].status.lastName + "\n" + maidArray[selectMaidIndex].status.firstName;
				GUI.Label(new Rect((float)GetPix(90), (float)GetPix(50), (float)GetPix(140), (float)GetPix(210)), text2, guistyle);
			}
			if (!isF6)
			{
				if (isDanceStop)
				{
					isStop[selectMaidIndex] = true;
					isDanceStop = false;
				}
				if (sceneLevel == 5)
				{
					if (maidCnt > 1)
					{
						bool value2 = false;
						if (selectMaidIndex == isEditNo)
						{
							value2 = true;
						}
						isEdit[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(25), (float)GetPix(50), (float)GetPix(16)), value2, "Edit", guistyle6);
						if (isEdit[selectMaidIndex] && selectMaidIndex != isEditNo)
						{
							isEditNo = selectMaidIndex;
							for (int j = 0; j < maidCnt; j++)
							{
								if (j != isEditNo)
								{
									isEdit[j] = false;
								}
							}
							SceneEdit component = GameObject.Find("__SceneEdit__").GetComponent<SceneEdit>();
							AdvancedStudioMode.SetFieldValue<SceneEdit, Maid>(component, "m_maid", maidArray[selectMaidIndex]);
							component.PartsTypeCamera(MPN.stkg);
							editSelectMaid = maidArray[selectMaidIndex];
						}
					}
				}
				if (poseGroupCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				if (isLock[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(125), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
				{
					poseIndex[selectMaidIndex]--;
					if (poseGroupIndex > 0)
					{
						if ((int)groupList[poseGroupIndex - 1] > poseIndex[selectMaidIndex])
						{
							if (poseGroupIndex >= groupList.Count)
							{
								poseIndex[selectMaidIndex] = poseArray.Length - 1;
							}
							else
							{
								poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex] - 1;
							}
						}
					}
					else if (poseIndex[selectMaidIndex] < 0)
					{
						poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex] - 1;
					}
					isPoseInit = true;
					if (poseGroupIndex > 0)
					{
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex] - (int)groupList[poseGroupIndex - 1];
					}
					else
					{
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex];
					}
					if (!isLock[selectMaidIndex])
					{
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
				}
				if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(125), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
				{
					poseIndex[selectMaidIndex]++;
					if (poseIndex[selectMaidIndex] > (int)groupList[groupList.Count - 1])
					{
						if (poseIndex[selectMaidIndex] >= poseArray.Length)
						{
							poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex - 1];
						}
					}
					else if (poseIndex[selectMaidIndex] >= (int)groupList[poseGroupIndex])
					{
						if (poseGroupIndex > 0)
						{
							poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex - 1];
						}
						else
						{
							poseIndex[selectMaidIndex] = 0;
						}
					}
					isPoseInit = true;
					if (poseGroupIndex > 0)
					{
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex] - (int)groupList[poseGroupIndex - 1];
					}
					else
					{
						poseCombo.selectedItemIndex = poseIndex[selectMaidIndex];
					}
					if (!isLock[selectMaidIndex])
					{
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
				}
				GUI.enabled = true;
				int num5 = -1;
				for (int k = 0; k < groupList.Count; k++)
				{
					if (poseIndex[selectMaidIndex] < (int)groupList[k])
					{
						num5 = k;
						break;
					}
				}
				int num6 = (int)groupList[0];
				int num7 = 0;
				if (num5 > 0)
				{
					num6 = (int)groupList[num5] - (int)groupList[num5 - 1];
					num7 = (int)groupList[num5 - 1];
				}
				if (num5 < 0)
				{
					num5 = groupList.Count;
					num6 = poseArray.Length - (int)groupList[num5 - 1];
					num7 = (int)groupList[num5 - 1];
				}
				if (poseGroupCombo.selectedItemIndex != num5)
				{
					poseComboList = new GUIContent[num6];
					int j = 0;
					for (int i = num7; i < num7 + num6; i++)
					{
						bool flag = false;
						List<IniKey> keys = base.Preferences["pose"].Keys;
						foreach (IniKey iniKey in keys)
						{
							if (poseArray[i] == iniKey.Key)
							{
								IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
								if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
								{
									poseComboList[j] = new GUIContent(string.Concat(new object[]
									{
										j + 1,
										":",
										iniKey2.Value.Split(new char[]
										{
											'_'
										})[0],
										"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
										iniKey.Key
									}));
									flag = true;
								}
							}
						}
						if (!flag)
						{
							poseComboList[j] = new GUIContent(j + 1 + ":" + poseArray[i]);
						}
						j++;
					}
					poseGroupCombo.selectedItemIndex = num5;
					poseGroupIndex = num5;
					poseCombo.selectedItemIndex = 0;
				}
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				isLook[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(8), (float)GetPix(155), (float)GetPix(65), (float)GetPix(16)), isLook[selectMaidIndex], "顔の向き", guistyle6);
				isPoseEdit = GUI.Toggle(new Rect((float)GetPix(86), (float)GetPix(155), (float)GetPix(90), (float)GetPix(16)), isPoseEdit, "ポーズ登録", guistyle6);
				if (isPoseEdit)
				{
					inName3 = GUI.TextField(new Rect((float)GetPix(5), (float)GetPix(180), (float)GetPix(100), (float)GetPix(20)), inName3);
					if (GUI.Button(new Rect((float)GetPix(107), (float)GetPix(180), (float)GetPix(35), (float)GetPix(20)), "追加", guistyle3))
					{
						isSavePose = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
						existPose = true;
						GUI.FocusControl("");
					}
					if (poseGroupComboList[poseGroupCombo.selectedItemIndex].text != "99:登録ポーズ")
					{
						GUI.enabled = false;
					}
					if (GUI.Button(new Rect((float)GetPix(144), (float)GetPix(180), (float)GetPix(24), (float)GetPix(20)), "削", guistyle3))
					{
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
						List<string> list = new List<string>();
						list.AddRange(poseArray);
						if (poseComboList[poseCombo.selectedItemIndex].text.Contains("AdvancedStudioModePose"))
						{
							string text3 = poseArray[poseIndex[selectMaidIndex]];
							list.Remove(text3);
							string path2 = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								'/'
							})[1].Replace("\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000", "");
							if (File.Exists(path2))
							{
								File.Delete(path2);
							}
						}
						else
						{
							string[] array4 = poseComboList[poseCombo.selectedItemIndex].text.Split(new char[]
							{
								'p'
							});
							string text3 = array4[array4.Length - 1];
							IniKey iniKey3 = base.Preferences["pose"]["p" + text3];
							if (iniKey3.Value != "" || iniKey3.Value != "del")
							{
								base.Preferences["pose"]["p" + text3].Value = "del";
								base.SaveConfig();
							}
							list.Remove("p" + text3);
						}
						GUI.FocusControl("");
						poseArray = list.ToArray();
						num5 = -1;
						for (int k = 0; k < groupList.Count; k++)
						{
							if (poseIndex[selectMaidIndex] < (int)groupList[k])
							{
								num5 = k;
								break;
							}
						}
						num6 = (int)groupList[0];
						num7 = 0;
						if (num5 > 0)
						{
							num6 = (int)groupList[num5] - (int)groupList[num5 - 1];
							num7 = (int)groupList[num5 - 1];
						}
						if (num5 < 0)
						{
							num5 = groupList.Count;
							num6 = poseArray.Length - (int)groupList[num5 - 1];
							num7 = (int)groupList[num5 - 1];
						}
						poseComboList = new GUIContent[num6];
						int num8 = 0;
						bool existEdit = false;
						for (int l = num7; l < num7 + num6; l++)
						{
							bool flag = false;
							List<IniKey> keys = base.Preferences["pose"].Keys;
							foreach (IniKey iniKey in keys)
							{
								if (poseArray[l] == iniKey.Key)
								{
									IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
									if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
									{
										poseComboList[num8] = new GUIContent(string.Concat(new object[]
										{
											num8 + 1,
											":",
											iniKey2.Value.Split(new char[]
											{
												'_'
											})[0],
											"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
											iniKey.Key
										}));
										flag = true;
										existEdit = true;
									}
								}
							}
							if (!flag)
							{
								poseComboList[num8] = new GUIContent(num8 + 1 + ":" + poseArray[l]);
							}
							num8++;
						}
						Action<string, List<string>> action = delegate (string path, List<string> result_list)
						{
							string[] files = Directory.GetFiles(path);
							countS = 0;
							for (int n = 0; n < files.Length; n++)
							{
								if (Path.GetExtension(files[n]) == ".anm")
								{
									existEdit = true;
									break;
								}
							}
						};
						List<string> arg = new List<string>();
						action(Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose", arg);
						if (!existEdit)
						{
							poseIniStr = "";
							List<string> list2 = new List<string>(50 + poseGroupArray2.Length);
							list2.AddRange(poseGroupArray2);
							list2.AddRange(poseGroupArrayVP);
							list2.AddRange(poseGroupArrayFB);
							list2.AddRange(poseGroupArray3);
							list2.Add(poseArray5[0]);
							list2.Add(poseArray6[0]);
							list2.Add(strList2[0]);
                            list2.Add(strListE2[0]);
                            existPose = false;
							poseGroupArray = list2.ToArray();
							groupList = new ArrayList();
							for (int k = 0; k < poseArray.Length; k++)
							{
								for (int j = 0; j < poseGroupArray.Length; j++)
								{
									if (poseGroupArray[j] == poseArray[k])
									{
										groupList.Add(k);
										if (poseGroupArray[j] == strList2[0])
										{
											sPoseCount = k;
										}
									}
								}
							}
							poseIndex[selectMaidIndex] = 0;
							poseGroupComboList = new GUIContent[poseGroupArray.Length + 1];
							poseGroupComboList[0] = new GUIContent("1:通常");
							for (int m = 0; m < poseGroupArray.Length; m++)
							{
								if (poseGroupArray[m] == "maid_dressroom01")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":立ち");
								}
								if (poseGroupArray[m] == "tennis_kamae_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":中腰");
								}
								if (poseGroupArray[m] == "senakanagasi_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":膝をつく");
								}
								if (poseGroupArray[m] == "work_hansei")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":座り");
								}
								if (poseGroupArray[m] == "inu_taiki_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":四つん這い");
								}
								if (poseGroupArray[m] == "syagami_pose_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":床座り");
								}
								if (poseGroupArray[m] == "densyasuwari_taiki_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":椅子座り");
								}
								if (poseGroupArray[m] == "work_kaiwa")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":ソファー座り");
								}
								if (poseGroupArray[m] == "dance_cm3d2_001_f1,14.14")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":ドキドキ☆Fallin' Love");
								}
								if (poseGroupArray[m] == "dance_cm3d_001_f1,39.25")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":entrance to you");
								}
								if (poseGroupArray[m] == "dance_cm3d_002_end_f1,50.71")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":scarlet leap");
								}
								if (poseGroupArray[m] == "dance_cm3d2_002_smt_f,7.76,")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":stellar my tears");
								}
								if (poseGroupArray[m] == "dance_cm3d_003_sp2_f1,90.15")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":rhythmix to you");
								}
								if (poseGroupArray[m] == "dance_cm3d2_003_hs_f1,0.01,")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":happy!happy!スキャンダル!!");
								}
								if (poseGroupArray[m] == "dance_cm3d_004_kano_f1,124.93")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":Can Know Two Close");
								}
								if (poseGroupArray[m] == "dance_cm3d2_004_sse_f1,0.01")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":sweet sweet everyday");
								}
								if (poseGroupArray[m] == "turusi_sex_in_taiki_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":拘束");
								}
								if (poseGroupArray[m] == "rosyutu_pose01_f")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":エロ");
								}
								if (poseGroupArray[m] == "rosyutu_aruki_f_once_,1.37")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":歩き");
								}
								if (poseGroupArray[m] == "stand_desk1")
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":その他");
								}
								if (poseGroupArray[m] == poseArray5[0])
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":ダンスMC");
								}
								if (poseGroupArray[m] == poseArray6[0])
								{
									poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":ダンス");
								}
                                if (m == poseGroupArray.Length - 2)
                                {
                                    poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":通常2");
                                }
                                if (m == poseGroupArray.Length - 1)
                                {
                                    poseGroupComboList[m + 1] = new GUIContent(m + 2 + ":エロ2");
                                }
                            }
						}
						else
						{
							poseGroupCombo.selectedItemIndex = num5;
							poseGroupIndex = num5;
							poseCombo.selectedItemIndex = 0;
							poseIndex[selectMaidIndex] = (int)groupList[groupList.Count - 1];
							if (poseArray.Length <= poseIndex[selectMaidIndex])
							{
								poseIndex[selectMaidIndex]--;
							}
						}
					}
					if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton)
					{
						GUI.enabled = false;
					}
					else
					{
						GUI.enabled = true;
					}
				}
				else
				{
					if (!isLook[selectMaidIndex])
					{
						GUI.enabled = false;
					}
					GUI.Label(new Rect((float)GetPix(8), (float)GetPix(175), (float)GetPix(100), (float)GetPix(25)), "顔の向きX", guistyle2);
					lookX[selectMaidIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(191), (float)GetPix(70), (float)GetPix(20)), lookX[selectMaidIndex], -0.6f, 0.6f);
					GUI.Label(new Rect((float)GetPix(88), (float)GetPix(175), (float)GetPix(100), (float)GetPix(25)), "顔の向きY", guistyle2);
					lookY[selectMaidIndex] = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(191), (float)GetPix(70), (float)GetPix(20)), lookY[selectMaidIndex], 0.5f, -0.55f);
					if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton)
					{
						GUI.enabled = false;
					}
					else
					{
						GUI.enabled = true;
					}
				}
				int num9 = 0;
				if (poseGroupIndex > 0)
				{
					num9 = poseIndex[selectMaidIndex] - (int)groupList[poseGroupIndex - 1];
				}
				else
				{
					num9 = poseIndex[selectMaidIndex];
				}
				if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(215), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
				{
					itemIndex[selectMaidIndex]--;
					if (itemIndex[selectMaidIndex] <= -1)
					{
						itemIndex[selectMaidIndex] = itemArray.Length - 2;
					}
					string[] array = new string[2];
					array = itemArray[itemIndex[selectMaidIndex]].Split(new char[]
					{
						','
					});
					if (itemIndex[selectMaidIndex] > 13)
					{
						array = itemArray[itemIndex[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
					}
					maid.DelProp(MPN.handitem, true);
					maid.DelProp(MPN.accvag, true);
					maid.DelProp(MPN.accanl, true);
					bool flag2 = false;
					if (itemIndex[selectMaidIndex] == 12 || itemIndex[selectMaidIndex] == 13 || itemIndex[selectMaidIndex] == 23)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						maid.DelProp(MPN.kousoku_upper, true);
						maid.DelProp(MPN.kousoku_lower, true);
					}
					if (array[0] != "")
					{
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 12)
					{
						array = itemArray[itemIndex[selectMaidIndex] - 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 13)
					{
						array = itemArray[itemIndex[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 23)
					{
						array = itemArray[itemIndex[selectMaidIndex]].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
						cafeFlg[selectMaidIndex] = true;
					}
					maid.AllProcPropSeqStart();
					itemCombo.selectedItemIndex = itemIndex[selectMaidIndex];
				}
				if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(215), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
				{
					itemIndex[selectMaidIndex]++;
					if (itemIndex[selectMaidIndex] >= itemArray.Length - 1)
					{
						itemIndex[selectMaidIndex] = 0;
					}
					string[] array = new string[2];
					array = itemArray[itemIndex[selectMaidIndex]].Split(new char[]
					{
						','
					});
					if (itemIndex[selectMaidIndex] > 13)
					{
						array = itemArray[itemIndex[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
					}
					maid.DelProp(MPN.handitem, true);
					maid.DelProp(MPN.accvag, true);
					maid.DelProp(MPN.accanl, true);
					bool flag2 = false;
					if (itemIndex[selectMaidIndex] == 12 || itemIndex[selectMaidIndex] == 13)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						maid.DelProp(MPN.kousoku_upper, true);
						maid.DelProp(MPN.kousoku_lower, true);
					}
					if (array[0] != "")
					{
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 12)
					{
						array = itemArray[itemIndex[selectMaidIndex] - 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 13)
					{
						array = itemArray[itemIndex[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex[selectMaidIndex] == 23)
					{
						array = itemArray[itemIndex[selectMaidIndex]].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
						cafeFlg[selectMaidIndex] = true;
					}
					maid.AllProcPropSeqStart();
					itemCombo.selectedItemIndex = itemIndex[selectMaidIndex];
				}
				if (itemCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				isWear = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(248), (float)GetPix(70), (float)GetPix(20)), isWear, "トップス", guistyle6);
				isSkirt = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(248), (float)GetPix(70), (float)GetPix(20)), isSkirt, "ボトムス", guistyle6);
				isBra = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(273), (float)GetPix(80), (float)GetPix(20)), isBra, "ブラジャー", guistyle6);
				isPanz = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(273), (float)GetPix(60), (float)GetPix(20)), isPanz, "パンツ", guistyle6);
				isHeadset = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(298), (float)GetPix(70), (float)GetPix(20)), isHeadset, "ヘッド", guistyle6);
				isMegane = GUI.Toggle(new Rect((float)GetPix(95), (float)GetPix(298), (float)GetPix(70), (float)GetPix(20)), isMegane, "メガネ", guistyle6);
				isAccUde = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(323), (float)GetPix(40), (float)GetPix(20)), isAccUde, "腕", guistyle6);
				isGlove = GUI.Toggle(new Rect((float)GetPix(50), (float)GetPix(323), (float)GetPix(40), (float)GetPix(20)), isGlove, "手袋", guistyle6);
				isAccSenaka = GUI.Toggle(new Rect((float)GetPix(95), (float)GetPix(323), (float)GetPix(40), (float)GetPix(20)), isAccSenaka, "背中", guistyle6);
				isStkg = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(348), (float)GetPix(40), (float)GetPix(20)), isStkg, "靴下", guistyle6);
				isShoes = GUI.Toggle(new Rect((float)GetPix(50), (float)GetPix(348), (float)GetPix(40), (float)GetPix(20)), isShoes, "靴", guistyle6);
				isMaid = GUI.Toggle(new Rect((float)GetPix(95), (float)GetPix(348), (float)GetPix(70), (float)GetPix(20)), isMaid, "メイド", guistyle6);
				mekure1[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(373), (float)GetPix(62), (float)GetPix(20)), mekure1[selectMaidIndex], "めくれ前", guistyle6);
				mekure2[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(67), (float)GetPix(373), (float)GetPix(40), (float)GetPix(20)), mekure2[selectMaidIndex], "後ろ", guistyle6);
				zurasi[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(105), (float)GetPix(373), (float)GetPix(50), (float)GetPix(20)), zurasi[selectMaidIndex], "ずらし", guistyle6);
				voice1[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(530), (float)GetPix(70), (float)GetPix(20)), zFlg[selectMaidIndex], "ボイス", guistyle6);
				voice2[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(70), (float)GetPix(530), (float)GetPix(70), (float)GetPix(20)), xFlg[selectMaidIndex], "Hボイス", guistyle6);
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton || itemCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(402), (float)GetPix(160), 2f), line1);
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(402), (float)GetPix(160), 1f), line2);
				isIK[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(411), (float)GetPix(30), (float)GetPix(20)), isIK[selectMaidIndex], "IK", guistyle6);
				if (!isLock[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				isLock[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(45), (float)GetPix(411), (float)GetPix(40), (float)GetPix(20)), isLock[selectMaidIndex], "解除", guistyle6);
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton || itemCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				if (!isIK[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				isBone[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(100), (float)GetPix(411), (float)GetPix(60), (float)GetPix(20)), isBone[selectMaidIndex], "ボーン", guistyle6);
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				if (isBone[selectMaidIndex] != isBoneN[selectMaidIndex])
				{
					isBoneN[selectMaidIndex] = isBone[selectMaidIndex];
					isChange[selectMaidIndex] = true;
				}
				if (!isLock[selectMaidIndex] && unLockFlg != isLock[selectMaidIndex])
				{
					string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
					{
						','
					});
					isStop[selectMaidIndex] = false;
					poseCount[selectMaidIndex] = 20;
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
					int num4;
					if (array[0].Contains("AdvancedStudioModePose"))
					{
						string path3 = array[0].Split(new char[]
						{
							'/'
						})[1];
						byte[] array2 = new byte[0];
						try
						{
							using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
							string fileName = Path.GetFileName(path3);
							long num3 = (long)fileName.GetHashCode();
							maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
							Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
							{
								default(Maid.AutoTwist),
								Maid.AutoTwist.ShoulderR,
								Maid.AutoTwist.WristL,
								Maid.AutoTwist.WristR,
								Maid.AutoTwist.ThighL,
								Maid.AutoTwist.ThighR
							};
							for (int i = 0; i < array3.Length; i++)
							{
								maid.SetAutoTwist(array3[i], true);
							}
						}
					}
					else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
					{
						loadPose[selectMaidIndex] = array[0];
					}
					else if (!array[0].StartsWith("dance_"))
					{
						maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
						isDanceStop = true;
						if (array.Length > 2)
						{
							Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
							isPoseIti[selectMaidIndex] = true;
							poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
							maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
						}
					}
					mHandL[selectMaidIndex].initFlg = false;
					mHandR[selectMaidIndex].initFlg = false;
					mFootL[selectMaidIndex].initFlg = false;
					mFootR[selectMaidIndex].initFlg = false;
					pHandL[selectMaidIndex] = 0;
					pHandR[selectMaidIndex] = 0;
					hanten[selectMaidIndex] = false;
					hantenn[selectMaidIndex] = false;
					muneIKL[selectMaidIndex] = false;
					muneIKR[selectMaidIndex] = false;
					maid.body0.jbMuneL.enabled = true;
					maid.body0.jbMuneR.enabled = true;
					if (!GameMain.Instance.VRMode)
					{
						maidArray[selectMaidIndex].body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[selectMaidIndex]];
						maidArray[selectMaidIndex].body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[selectMaidIndex]];
					}
				}
				unLockFlg = isLock[selectMaidIndex];
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton || itemCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				if (!isIK[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				hanten[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(479), (float)GetPix(70), (float)GetPix(20)), hanten[selectMaidIndex], "左右反転", guistyle6);
				bool flag3 = GUI.Toggle(new Rect((float)GetPix(80), (float)GetPix(479), (float)GetPix(100), (float)GetPix(20)), kotei[selectMaidIndex], "スカート固定", guistyle6);
				if (kotei[selectMaidIndex] != flag3)
				{
					kotei[selectMaidIndex] = flag3;
					if (flag3)
					{
						SkirtListArray[selectMaidIndex] = new DynamicSkirtBone[100];
						for (int l = 0; l < maid.body0.goSlot.Count; l++)
						{
							DynamicSkirtBone fieldValue = AdvancedStudioMode.GetFieldValue<BoneHair3, DynamicSkirtBone>(maid.body0.goSlot[l].bonehair3, "m_SkirtBone");
							SkirtListArray[selectMaidIndex][l] = fieldValue;
							AdvancedStudioMode.SetFieldValue8<BoneHair3, DynamicSkirtBone>(maid.body0.goSlot[l].bonehair3, "m_SkirtBone", null);
						}
					}
					else
					{
						for (int l = 0; l < maid.body0.goSlot.Count; l++)
						{
							AdvancedStudioMode.SetFieldValue8<BoneHair3, DynamicSkirtBone>(maid.body0.goSlot[l].bonehair3, "m_SkirtBone", SkirtListArray[selectMaidIndex][l]);
						}
					}
				}
				GUI.Label(new Rect((float)GetPix(29), (float)GetPix(433), (float)GetPix(100), (float)GetPix(25)), "右手", guistyle);
				GUI.Label(new Rect((float)GetPix(109), (float)GetPix(433), (float)GetPix(100), (float)GetPix(25)), "左手", guistyle);
				string text4 = "未選択";
				if (copyIndex > 0)
				{
					text4 = copyIndex + ":" + maidArray[copyIndex - 1].status.firstName;
				}
				if (maidCnt <= 1)
				{
					GUI.enabled = false;
				}
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(505), (float)GetPix(100), (float)GetPix(25)), "コピー", guistyle);
				GUI.Label(new Rect((float)GetPix(70), (float)GetPix(505), (float)GetPix(100), (float)GetPix(25)), text4, guistyle);
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton || itemCombo.isClickedComboButton || !isIK[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				else
				{
					GUI.enabled = true;
				}
				guistyle.fontSize = GetPix(13);
				guistyle.alignment = TextAnchor.UpperCenter;
				GUI.Label(new Rect((float)GetPix(-10), (float)GetPix(449), (float)GetPix(100), (float)GetPix(25)), pHandR[selectMaidIndex].ToString(), guistyle);
				GUI.Label(new Rect((float)GetPix(70), (float)GetPix(449), (float)GetPix(100), (float)GetPix(25)), pHandL[selectMaidIndex].ToString(), guistyle);
				guistyle.fontSize = GetPix(11);
				guistyle.alignment = TextAnchor.UpperLeft;
				if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(448), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
				{
					pHandR[selectMaidIndex]--;
					if (pHandR[selectMaidIndex] < 1)
					{
						pHandR[selectMaidIndex] = fingerRArray.GetLength(0);
					}
					for (int j = 0; j < 20; j++)
					{
						string[] array = fingerRArray[pHandR[selectMaidIndex] - 1, j].Split(new char[]
						{
							','
						});
						Finger[selectMaidIndex, j + 20].localEulerAngles = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
					isStop[selectMaidIndex] = true;
					isLock[selectMaidIndex] = true;
					for (int j = 0; j < 10; j++)
					{
						if (j == 0 || j == 5)
						{
							if (mFinger[selectMaidIndex, j * 3])
							{
								mFinger[selectMaidIndex, j * 3].reset = true;
							}
						}
					}
				}
				if (GUI.Button(new Rect((float)GetPix(55), (float)GetPix(448), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
				{
					pHandR[selectMaidIndex]++;
					if (pHandR[selectMaidIndex] > fingerRArray.GetLength(0))
					{
						pHandR[selectMaidIndex] = 1;
					}
					for (int j = 0; j < 20; j++)
					{
						string[] array = fingerRArray[pHandR[selectMaidIndex] - 1, j].Split(new char[]
						{
							','
						});
						Finger[selectMaidIndex, j + 20].localEulerAngles = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
					isStop[selectMaidIndex] = true;
					isLock[selectMaidIndex] = true;
					for (int j = 0; j < 10; j++)
					{
						if (j == 0 || j == 5)
						{
							if (mFinger[selectMaidIndex, j * 3])
							{
								mFinger[selectMaidIndex, j * 3].reset = true;
							}
						}
					}
				}
				if (GUI.Button(new Rect((float)GetPix(85), (float)GetPix(448), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
				{
					pHandL[selectMaidIndex]--;
					if (pHandL[selectMaidIndex] < 1)
					{
						pHandL[selectMaidIndex] = fingerLArray.GetLength(0);
					}
					for (int j = 0; j < 20; j++)
					{
						string[] array = fingerLArray[pHandL[selectMaidIndex] - 1, j].Split(new char[]
						{
							','
						});
						Finger[selectMaidIndex, j].localEulerAngles = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
					isStop[selectMaidIndex] = true;
					isLock[selectMaidIndex] = true;
				}
				if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(448), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
				{
					pHandL[selectMaidIndex]++;
					if (pHandL[selectMaidIndex] > fingerRArray.GetLength(0))
					{
						pHandL[selectMaidIndex] = 1;
					}
					for (int j = 0; j < 20; j++)
					{
						string[] array = fingerLArray[pHandL[selectMaidIndex] - 1, j].Split(new char[]
						{
							','
						});
						Finger[selectMaidIndex, j].localEulerAngles = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
					}
					isStop[selectMaidIndex] = true;
					isLock[selectMaidIndex] = true;
				}
				if (maidCnt <= 1)
				{
					GUI.enabled = false;
				}
				if (GUI.Button(new Rect((float)GetPix(45), (float)GetPix(504), (float)GetPix(22), (float)GetPix(20)), "＞", guistyle3))
				{
					copyIndex++;
					if (copyIndex - 1 == selectMaidIndex)
					{
						copyIndex++;
					}
					if (copyIndex > maidCnt)
					{
						copyIndex = 0;
					}
				}
				if (isCopy)
				{
					isCopy = false;
					CopyIK2(maidArray[selectMaidIndex], selectMaidIndex, maidArray[copyIndex - 1], copyIndex - 1);
				}
				if (copyIndex == 0)
				{
					GUI.enabled = false;
				}
				if (GUI.Button(new Rect((float)GetPix(123), (float)GetPix(504), (float)GetPix(35), (float)GetPix(20)), "決定", guistyle3))
				{
					CopyIK(maidArray[selectMaidIndex], selectMaidIndex, maidArray[copyIndex - 1], copyIndex - 1);
					isCopy = true;
					pHandL[selectMaidIndex] = pHandL[copyIndex - 1];
					pHandR[selectMaidIndex] = pHandR[copyIndex - 1];
					hanten[selectMaidIndex] = hanten[copyIndex - 1];
					hantenn[selectMaidIndex] = hantenn[copyIndex - 1];
				}
				GUI.enabled = true;
				if (poseCombo.isClickedComboButton || poseGroupCombo.isClickedComboButton)
				{
					GUI.enabled = false;
				}
				itemIndex2[selectMaidIndex] = itemCombo.List(new Rect((float)GetPix(35), (float)GetPix(215), (float)GetPix(95), (float)GetPix(23)), itemComboList[itemIndex[selectMaidIndex]].text, itemComboList, guistyle4, "box", listStyle3);
				GUI.enabled = true;
				if (poseGroupCombo.isClickedComboButton || isLock[selectMaidIndex])
				{
					GUI.enabled = false;
				}
				poseCombo.List(new Rect((float)GetPix(35), (float)GetPix(125), (float)GetPix(95), (float)GetPix(23)), poseComboList[num9].text, poseComboList, guistyle4, "box", listStyle2);
				if (!isLock[selectMaidIndex])
				{
					GUI.enabled = true;
				}
				int num10 = -1;
				for (int k = 0; k < groupList.Count; k++)
				{
					if (poseIndex[selectMaidIndex] < (int)groupList[k])
					{
						num10 = k;
						break;
					}
				}
				if (num10 < 0)
				{
					num10 = groupList.Count;
				}
				if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(95), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
				{
					isPoseInit = true;
					if (!isLock[selectMaidIndex])
					{
						for (int k = 0; k < groupList.Count; k++)
						{
							if (k == 0 && poseIndex[selectMaidIndex] <= (int)groupList[k])
							{
								if (poseIndex[selectMaidIndex] == 0)
								{
									poseIndex[selectMaidIndex] = (int)groupList[groupList.Count - 1];
								}
								else
								{
									poseIndex[selectMaidIndex] = 0;
								}
								break;
							}
							if (k > 0 && poseIndex[selectMaidIndex] > (int)groupList[k - 1] && poseIndex[selectMaidIndex] <= (int)groupList[k])
							{
								poseIndex[selectMaidIndex] = (int)groupList[k - 1];
								break;
							}
						}
						if (poseIndex[selectMaidIndex] > (int)groupList[groupList.Count - 1])
						{
							poseIndex[selectMaidIndex] = (int)groupList[groupList.Count - 1];
						}
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
					int num11 = -1;
					for (int k = 0; k < groupList.Count; k++)
					{
						if (poseIndex[selectMaidIndex] < (int)groupList[k])
						{
							num11 = k;
							break;
						}
					}
					int num2 = (int)groupList[0];
					int num12 = 0;
					if (num11 > 0)
					{
						num2 = (int)groupList[num11] - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					if (num11 < 0)
					{
						num11 = groupList.Count;
						num2 = poseArray.Length - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					poseComboList = new GUIContent[num2];
					int j = 0;
					for (int i = num12; i < num12 + num2; i++)
					{
						bool flag = false;
						List<IniKey> keys = base.Preferences["pose"].Keys;
						foreach (IniKey iniKey in keys)
						{
							if (poseArray[i] == iniKey.Key)
							{
								IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
								if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
								{
									poseComboList[j] = new GUIContent(string.Concat(new object[]
									{
										j + 1,
										":",
										iniKey2.Value.Split(new char[]
										{
											'_'
										})[0],
										"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
										iniKey.Key
									}));
									flag = true;
								}
							}
						}
						if (!flag)
						{
							poseComboList[j] = new GUIContent(j + 1 + ":" + poseArray[i]);
						}
						j++;
					}
					poseCombo.scrollPos = new Vector2(0f, 0f);
					poseGroupCombo.selectedItemIndex = num11;
					poseCombo.selectedItemIndex = 0;
				}
				if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(95), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
				{
					isPoseInit = true;
					if (!isLock[selectMaidIndex])
					{
						int num13 = poseIndex[selectMaidIndex];
						for (int k = 0; k < groupList.Count; k++)
						{
							if (poseIndex[selectMaidIndex] < (int)groupList[k])
							{
								poseIndex[selectMaidIndex] = (int)groupList[k];
								break;
							}
						}
						if (num13 == poseIndex[selectMaidIndex] && poseIndex[selectMaidIndex] >= (int)groupList[groupList.Count - 1])
						{
							poseIndex[selectMaidIndex] = 0;
						}
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
					int num11 = -1;
					for (int k = 0; k < groupList.Count; k++)
					{
						if (poseIndex[selectMaidIndex] < (int)groupList[k])
						{
							num11 = k;
							break;
						}
					}
					int num2 = (int)groupList[0];
					int num12 = 0;
					if (num11 > 0)
					{
						num2 = (int)groupList[num11] - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					if (num11 < 0)
					{
						num11 = groupList.Count;
						num2 = poseArray.Length - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					poseComboList = new GUIContent[num2];
					int j = 0;
					for (int i = num12; i < num12 + num2; i++)
					{
						bool flag = false;
						List<IniKey> keys = base.Preferences["pose"].Keys;
						foreach (IniKey iniKey in keys)
						{
							if (poseArray[i] == iniKey.Key)
							{
								IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
								if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
								{
									poseComboList[j] = new GUIContent(string.Concat(new object[]
									{
										j + 1,
										":",
										iniKey2.Value.Split(new char[]
										{
											'_'
										})[0],
										"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
										iniKey.Key
									}));
									flag = true;
								}
							}
						}
						if (!flag)
						{
							poseComboList[j] = new GUIContent(j + 1 + ":" + poseArray[i]);
						}
						j++;
					}
					poseCombo.scrollPos = new Vector2(0f, 0f);
					poseGroupCombo.selectedItemIndex = num11;
					poseCombo.selectedItemIndex = 0;
				}
				poseGroupIndex = poseGroupCombo.List(new Rect((float)GetPix(35), (float)GetPix(95), (float)GetPix(95), (float)GetPix(23)), poseGroupComboList[num10].text, poseGroupComboList, guistyle5, "box", listStyle4);
				if (poseGroupCombo.isClickedComboButton)
				{
					isCombo2 = true;
				}
				else if (isCombo2)
				{
					isCombo2 = false;
					isPoseInit = true;
					if (poseGroupIndex > 0)
					{
						poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex - 1];
					}
					else
					{
						poseIndex[selectMaidIndex] = 0;
					}
					if (!isLock[selectMaidIndex])
					{
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
					int num11 = -1;
					for (int k = 0; k < groupList.Count; k++)
					{
						if (poseIndex[selectMaidIndex] < (int)groupList[k])
						{
							num11 = k;
							break;
						}
					}
					int num2 = (int)groupList[0];
					int num12 = 0;
					if (num11 > 0)
					{
						num2 = (int)groupList[num11] - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					if (num11 < 0)
					{
						num11 = groupList.Count;
						num2 = poseArray.Length - (int)groupList[num11 - 1];
						num12 = (int)groupList[num11 - 1];
					}
					poseComboList = new GUIContent[num2];
					int j = 0;
					for (int i = num12; i < num12 + num2; i++)
					{
						bool flag = false;
						List<IniKey> keys = base.Preferences["pose"].Keys;
						foreach (IniKey iniKey in keys)
						{
							if (poseArray[i] == iniKey.Key)
							{
								IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
								if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
								{
									poseComboList[j] = new GUIContent(string.Concat(new object[]
									{
										j + 1,
										":",
										iniKey2.Value.Split(new char[]
										{
											'_'
										})[0],
										"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
										iniKey.Key
									}));
									flag = true;
								}
							}
						}
						if (!flag)
						{
							poseComboList[j] = new GUIContent(j + 1 + ":" + poseArray[i]);
						}
						j++;
					}
					poseCombo.scrollPos = new Vector2(0f, 0f);
					poseGroupCombo.selectedItemIndex = num11;
					poseCombo.selectedItemIndex = 0;
				}
				if (poseCombo.isClickedComboButton)
				{
					isCombo = true;
				}
				else if (isCombo)
				{
					isCombo = false;
					isPoseInit = true;
					if (poseGroupIndex > 0)
					{
						poseIndex[selectMaidIndex] = (int)groupList[poseGroupIndex - 1] + poseCombo.selectedItemIndex;
					}
					else
					{
						poseIndex[selectMaidIndex] = poseCombo.selectedItemIndex;
					}
					if (poseIndex[selectMaidIndex] == poseArray.Length)
					{
						poseIndex[selectMaidIndex] = 0;
					}
					if (!isLock[selectMaidIndex])
					{
						if (maid && maid.Visible)
						{
							string[] array = poseArray[poseIndex[selectMaidIndex]].Split(new char[]
							{
								','
							});
							isStop[selectMaidIndex] = false;
							poseCount[selectMaidIndex] = 20;
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
							int num4;
							if (array[0].Contains("AdvancedStudioModePose"))
							{
								string path3 = array[0].Split(new char[]
								{
									'/'
								})[1];
								byte[] array2 = new byte[0];
								try
								{
									using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
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
									string fileName = Path.GetFileName(path3);
									long num3 = (long)fileName.GetHashCode();
									maid.body0.CrossFade(num3.ToString(), array2, false, false, false, 0f, 1f);
									Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
									{
										default(Maid.AutoTwist),
										Maid.AutoTwist.ShoulderR,
										Maid.AutoTwist.WristL,
										Maid.AutoTwist.WristR,
										Maid.AutoTwist.ThighL,
										Maid.AutoTwist.ThighR
									};
									for (int i = 0; i < array3.Length; i++)
									{
										maid.SetAutoTwist(array3[i], true);
									}
								}
							}
							else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num4))
							{
								loadPose[selectMaidIndex] = array[0];
							}
							else if (!array[0].StartsWith("dance_"))
							{
								maidArray[selectMaidIndex].CrossFade(array[0] + ".anm", false, true, false, 0f, 1f);
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
								isDanceStop = true;
								if (array.Length > 2)
								{
									Transform transform = CMT.SearchObjName(maidArray[selectMaidIndex].body0.m_Bones.transform, "Bip01", true);
									isPoseIti[selectMaidIndex] = true;
									poseIti[selectMaidIndex] = maidArray[selectMaidIndex].transform.position;
									maidArray[selectMaidIndex].transform.position = new Vector3(100f, 100f, 100f);
								}
							}
						}
					}
				}
				if (itemCombo.isClickedComboButton)
				{
					isCombo3 = true;
				}
				else if (isCombo3)
				{
					isCombo3 = false;
					string[] array = new string[2];
					array = itemArray[itemIndex2[selectMaidIndex]].Split(new char[]
					{
						','
					});
					if (itemIndex2[selectMaidIndex] > 13)
					{
						array = itemArray[itemIndex2[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
					}
					maid.DelProp(MPN.handitem, true);
					bool flag2 = false;
					if (itemIndex2[selectMaidIndex] == 0)
					{
						maid.DelProp(MPN.accvag, true);
						maid.DelProp(MPN.accanl, true);
					}
					if (itemIndex2[selectMaidIndex] == 12 || itemIndex2[selectMaidIndex] == 13)
					{
						flag2 = true;
					}
					if (!flag2)
					{
						maid.DelProp(MPN.kousoku_upper, true);
						maid.DelProp(MPN.kousoku_lower, true);
					}
					if (array[0] != "")
					{
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex2[selectMaidIndex] == 12)
					{
						array = itemArray[itemIndex2[selectMaidIndex] - 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex2[selectMaidIndex] == 13)
					{
						array = itemArray[itemIndex2[selectMaidIndex] + 1].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
					}
					if (itemIndex2[selectMaidIndex] == 23)
					{
						array = itemArray[itemIndex2[selectMaidIndex]].Split(new char[]
						{
							','
						});
						maid.SetProp(array[0], array[1], 0, true, false);
						cafeFlg[selectMaidIndex] = true;
					}
					maid.AllProcPropSeqStart();
					itemCombo.selectedItemIndex = itemIndex2[selectMaidIndex];
					itemIndex[selectMaidIndex] = itemIndex2[selectMaidIndex];
				}
			}
			else
			{
				isWear = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(98), (float)GetPix(70), (float)GetPix(20)), isWear, "トップス", guistyle6);
				isSkirt = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(98), (float)GetPix(70), (float)GetPix(20)), isSkirt, "ボトムス", guistyle6);
				isBra = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(123), (float)GetPix(80), (float)GetPix(20)), isBra, "ブラジャー", guistyle6);
				isPanz = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(123), (float)GetPix(60), (float)GetPix(20)), isPanz, "パンツ", guistyle6);
				isHeadset = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(148), (float)GetPix(70), (float)GetPix(20)), isHeadset, "ヘッド", guistyle6);
				isMegane = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(148), (float)GetPix(70), (float)GetPix(20)), isMegane, "メガネ", guistyle6);
				isAccUde = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(173), (float)GetPix(40), (float)GetPix(20)), isAccUde, "腕", guistyle6);
				isGlove = GUI.Toggle(new Rect((float)GetPix(45), (float)GetPix(173), (float)GetPix(40), (float)GetPix(20)), isGlove, "手袋", guistyle6);
				isAccSenaka = GUI.Toggle(new Rect((float)GetPix(97), (float)GetPix(173), (float)GetPix(40), (float)GetPix(20)), isAccSenaka, "背中", guistyle6);
				isStkg = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(198), (float)GetPix(70), (float)GetPix(20)), isStkg, "ソックス", guistyle6);
				isShoes = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(198), (float)GetPix(70), (float)GetPix(20)), isShoes, "シューズ", guistyle6);
				mekure1[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(223), (float)GetPix(62), (float)GetPix(20)), mekure1[selectMaidIndex], "めくれ前", guistyle6);
				mekure2[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(67), (float)GetPix(223), (float)GetPix(40), (float)GetPix(20)), mekure2[selectMaidIndex], "後ろ", guistyle6);
				zurasi[selectMaidIndex] = GUI.Toggle(new Rect((float)GetPix(105), (float)GetPix(223), (float)GetPix(50), (float)GetPix(20)), zurasi[selectMaidIndex], "ずらし", guistyle6);
			}
		}

		private void GuiFunc2(int winID)
		{
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(12);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "label";
			guistyle2.fontSize = GetPix(11);
			guistyle2.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle3 = "button";
			guistyle3.fontSize = GetPix(20);
			GUIStyle guistyle4 = "button";
			guistyle4.fontSize = GetPix(12);
			guistyle4.alignment = TextAnchor.MiddleLeft;
			GUIStyle guistyle5 = new GUIStyle("toggle");
			guistyle5.fontSize = GetPix(13);
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			if (!faceInitFlg)
			{
				listStyle2.normal.textColor = Color.white;
				listStyle2.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.5f));
				listStyle2.onHover.background = (listStyle2.hover.background = new Texture2D(2, 2));
				listStyle2.padding.left = (listStyle2.padding.right = (listStyle2.padding.top = (listStyle2.padding.bottom = GetPix(0))));
				listStyle2.fontSize = GetPix(12);
				faceCombo.selectedItemIndex = 0;
				List<string> list = new List<string>(300);
				list.AddRange(faceArray);
				for (int i = 1; i < 300; i++)
				{
					IniKey iniKey = base.Preferences["face"]["f" + i];
					if (iniKey.Value == null)
					{
						break;
					}
					string[] array = iniKey.Value.Split(new char[]
					{
						':'
					});
					if (array.Length > 1)
					{
						list.Add(string.Concat(new object[]
						{
							array[0],
							"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000:",
							i,
							":",
							array[1]
						}));
					}
				}
				faceCombo.selectedItemIndex = 0;
				faceComboList = new GUIContent[list.ToArray().Length];
				for (int i = 0; i < list.ToArray().Length; i++)
				{
					faceComboList[i] = new GUIContent(list.ToArray()[i]);
				}
				faceInitFlg = true;
			}
			if (faceCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (sceneLevel == 3 || sceneLevel == 5 || isF6)
			{
				if (!isF6)
				{
					bool value = true;
					if (faceFlg || poseFlg || sceneFlg || kankyoFlg || kankyo2Flg)
					{
						value = false;
					}
					if (GUI.Toggle(new Rect((float)GetPix(2), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), value, "配置", guistyle5))
					{
						faceFlg = false;
						poseFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
						bGui = true;
						isGuiInit = true;
					}
				}
				if (!yotogiFlg)
				{
					if (GUI.Toggle(new Rect((float)GetPix(41), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), poseFlg, "操作", guistyle5))
					{
						poseFlg = true;
						faceFlg = false;
						sceneFlg = false;
						kankyoFlg = false;
						kankyo2Flg = false;
					}
				}
				if (GUI.Toggle(new Rect((float)GetPix(80), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), faceFlg, "表情", guistyle5))
				{
					faceFlg = true;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					if (!faceFlg2)
					{
						isFaceInit = true;
						faceFlg2 = true;
						maidArray[selectMaidIndex].boMabataki = false;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
				}
				if (GUI.Toggle(new Rect((float)GetPix(119), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyoFlg, "環境", guistyle5))
				{
					poseFlg = false;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = true;
					kankyo2Flg = false;
				}
				if (!line1)
				{
					line1 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.3f));
					line2 = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 0.6f));
				}
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 2f), line1);
				GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 1f), line2);
				guistyle.fontSize = GetPix(13);
				guistyle.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect((float)GetPix(125), (float)GetPix(25), (float)GetPix(40), (float)GetPix(25)), string.Concat(selectMaidIndex + 1), guistyle);
				guistyle.fontSize = GetPix(11);
				guistyle.alignment = TextAnchor.UpperLeft;
			}
			if (sceneLevel > 0)
			{
				int i = selectMaidIndex;
				if (sceneLevel == 3 || (sceneLevel == 5 && (isF7 || maidCnt > 1)))
				{
					if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(53), (float)GetPix(23), (float)GetPix(32)), "＜", guistyle3))
					{
						selectMaidIndex--;
						if (selectMaidIndex < 0)
						{
							selectMaidIndex = selectList.Count - 1;
						}
						isFaceInit = true;
						faceFlg = true;
						copyIndex = 0;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
					if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(53), (float)GetPix(23), (float)GetPix(32)), "＞", guistyle3))
					{
						selectMaidIndex++;
						if (selectList.Count <= selectMaidIndex)
						{
							selectMaidIndex = 0;
						}
						isFaceInit = true;
						faceFlg = true;
						copyIndex = 0;
						faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
					}
				}
				if (maidArray[selectMaidIndex].GetThumIcon())
				{
					GUI.DrawTexture(new Rect((float)GetPix(30), (float)GetPix(25), (float)GetPix(60), (float)GetPix(60)), maidArray[selectMaidIndex].GetThumIcon());
				}
				string text = maidArray[selectMaidIndex].status.lastName + "\n" + maidArray[selectMaidIndex].status.firstName;
				GUI.Label(new Rect((float)GetPix(90), (float)GetPix(50), (float)GetPix(140), (float)GetPix(210)), text, guistyle);
				bool flag = GUI.Toggle(new Rect((float)GetPix(90), (float)GetPix(25), (float)GetPix(50), (float)GetPix(16)), isShosai, "詳細", guistyle5);
				if (flag != isShosai)
				{
					isShosai = flag;
					if (isShosai)
					{
						base.Preferences["config"]["hair_details"].Value = "true";
					}
					else
					{
						base.Preferences["config"]["hair_details"].Value = "false";
					}
					base.SaveConfig();
				}
				if (isFace[i])
				{
					if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(25), (float)GetPix(23), (float)GetPix(23)), "有", guistyle3))
					{
						TMorph morph = maidArray[i].body0.Face.morph;
						maidArray[i].boMabataki = false;
						isFace[i] = false;
					}
					maidArray[i].boMabataki = false;
				}
				else
				{
					if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(25), (float)GetPix(23), (float)GetPix(23)), "無", guistyle3))
					{
						TMorph morph = maidArray[i].body0.Face.morph;
						maidArray[i].boMabataki = false;
						morph.EyeMabataki = 0f;
						isFaceInit = true;
						isFace[i] = true;
						faceCombo.selectedItemIndex = faceIndex[i];
					}
					GUI.enabled = false;
					maidArray[i].boMabataki = true;
				}
			}
			if (GUI.Button(new Rect((float)GetPix(5), (float)GetPix(95), (float)GetPix(23), (float)GetPix(23)), "＜", guistyle3))
			{
				faceIndex[selectMaidIndex]--;
				if (faceIndex[selectMaidIndex] <= -1)
				{
					faceIndex[selectMaidIndex] = faceComboList.Length - 1;
				}
				TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
				maidArray[selectMaidIndex].boMabataki = false;
				morph.EyeMabataki = 0f;
				if (faceIndex[selectMaidIndex] < faceArray.Length)
				{
					morph.MulBlendValues(faceArray[faceIndex[selectMaidIndex]], 1f);
					if (morph.bodyskin.PartsVersion >= 120)
					{
						float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
						fieldValue[(int)morph.hash["eyeclose3"]] *= 3f;
					}
				}
				else
				{
					float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
					float[] fieldValue2 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
					if (!isVR)
					{
						maidArray[selectMaidIndex].boMabataki = false;
					}
					string[] array = faceComboList[faceIndex[selectMaidIndex]].text.Split(new char[]
					{
						':'
					})[2].Split(new char[]
					{
						','
					});
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue2[(int)morph.hash["eyeclose"]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2"]] = float.Parse(array[1]);
						fieldValue2[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]);
						fieldValue2[(int)morph.hash["eyeclose6"]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5"]] = float.Parse(array[17]);
						if (morph.hash["eyeclose7"] != null)
						{
							if (array.Length > 37)
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = float.Parse(array[36]);
								fieldValue2[(int)morph.hash["eyeclose8"]] = float.Parse(array[37]);
							}
							else
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = 0f;
								fieldValue2[(int)morph.hash["eyeclose8"]] = 0f;
							}
						}
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						fieldValue2[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[1]);
						fieldValue[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]) * 3f;
						fieldValue2[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[17]);
						if (array.Length > 37)
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[36]);
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[37]);
						}
						else
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = 0f;
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = 0f;
						}
					}
					fieldValue[(int)morph.hash["hitomih"]] = float.Parse(array[4]);
					fieldValue[(int)morph.hash["hitomis"]] = float.Parse(array[5]);
					fieldValue[(int)morph.hash["mayuha"]] = float.Parse(array[6]);
					fieldValue[(int)morph.hash["mayuup"]] = float.Parse(array[7]);
					fieldValue[(int)morph.hash["mayuv"]] = float.Parse(array[8]);
					fieldValue[(int)morph.hash["mayuvhalf"]] = float.Parse(array[9]);
					fieldValue[(int)morph.hash["moutha"]] = float.Parse(array[10]);
					fieldValue[(int)morph.hash["mouths"]] = float.Parse(array[11]);
					fieldValue[(int)morph.hash["mouthdw"]] = float.Parse(array[12]);
					fieldValue[(int)morph.hash["mouthup"]] = float.Parse(array[13]);
					fieldValue[(int)morph.hash["tangout"]] = float.Parse(array[14]);
					fieldValue[(int)morph.hash["tangup"]] = float.Parse(array[15]);
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]);
					}
					else
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]) * 3f;
					}
					fieldValue[(int)morph.hash["mayuw"]] = float.Parse(array[18]);
					fieldValue[(int)morph.hash["mouthhe"]] = float.Parse(array[19]);
					fieldValue[(int)morph.hash["mouthc"]] = float.Parse(array[20]);
					fieldValue[(int)morph.hash["mouthi"]] = float.Parse(array[21]);
					fieldValue[(int)morph.hash["mouthuphalf"]] = float.Parse(array[22]) + 0.01f;
					try
					{
						fieldValue[(int)morph.hash["tangopen"]] = float.Parse(array[23]);
					}
					catch
					{
					}
					if (float.Parse(array[24]) == 1f)
					{
						fieldValue[(int)morph.hash["namida"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["namida"]] = 0f;
					}
					if (float.Parse(array[25]) == 1f)
					{
						fieldValue[(int)morph.hash["tear1"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear1"]] = 0f;
					}
					if (float.Parse(array[26]) == 1f)
					{
						fieldValue[(int)morph.hash["tear2"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear2"]] = 0f;
					}
					if (float.Parse(array[27]) == 1f)
					{
						fieldValue[(int)morph.hash["tear3"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear3"]] = 0f;
					}
					if (float.Parse(array[28]) == 1f)
					{
						fieldValue[(int)morph.hash["shock"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["shock"]] = 0f;
					}
					if (float.Parse(array[29]) == 1f)
					{
						fieldValue[(int)morph.hash["yodare"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["yodare"]] = 0f;
					}
					if (float.Parse(array[30]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho"]] = 0f;
					}
					if (float.Parse(array[31]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0f;
					}
					if (float.Parse(array[32]) == 1f)
					{
						fieldValue[(int)morph.hash["hohos"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohos"]] = 0f;
					}
					if (float.Parse(array[33]) == 1f)
					{
						fieldValue[(int)morph.hash["hohol"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohol"]] = 0f;
					}
					if (float.Parse(array[34]) == 1f)
					{
						fieldValue[(int)morph.hash["toothoff"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["toothoff"]] = 0f;
					}
					if (array.Length > 35)
					{
						if (float.Parse(array[35]) == 1f)
						{
							morph.boNoseFook = true;
						}
						else
						{
							morph.boNoseFook = false;
						}
					}
				}
				maidArray[selectMaidIndex].body0.Face.morph.FixBlendValues_Face();
				isFaceInit = true;
				faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
			}
			if (GUI.Button(new Rect((float)GetPix(135), (float)GetPix(95), (float)GetPix(23), (float)GetPix(23)), "＞", guistyle3))
			{
				faceIndex[selectMaidIndex]++;
				if (faceIndex[selectMaidIndex] == faceComboList.Length)
				{
					faceIndex[selectMaidIndex] = 0;
				}
				TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
				maidArray[selectMaidIndex].boMabataki = false;
				morph.EyeMabataki = 0f;
				if (faceIndex[selectMaidIndex] < faceArray.Length)
				{
					morph.MulBlendValues(faceArray[faceIndex[selectMaidIndex]], 1f);
					if (morph.bodyskin.PartsVersion >= 120)
					{
						float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
						fieldValue[(int)morph.hash["eyeclose3"]] *= 3f;
					}
				}
				else
				{
					float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
					float[] fieldValue2 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
					if (!isVR)
					{
						maidArray[selectMaidIndex].boMabataki = false;
					}
					string[] array = faceComboList[faceIndex[selectMaidIndex]].text.Split(new char[]
					{
						':'
					})[2].Split(new char[]
					{
						','
					});
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue2[(int)morph.hash["eyeclose"]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2"]] = float.Parse(array[1]);
						fieldValue2[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]);
						fieldValue2[(int)morph.hash["eyeclose6"]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5"]] = float.Parse(array[17]);
						if (morph.hash["eyeclose7"] != null)
						{
							if (array.Length > 37)
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = float.Parse(array[36]);
								fieldValue2[(int)morph.hash["eyeclose8"]] = float.Parse(array[37]);
							}
							else
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = 0f;
								fieldValue2[(int)morph.hash["eyeclose8"]] = 0f;
							}
						}
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						fieldValue2[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[1]);
						fieldValue[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]) * 3f;
						fieldValue2[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[17]);
						if (array.Length > 37)
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[36]);
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[37]);
						}
						else
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = 0f;
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = 0f;
						}
					}
					fieldValue[(int)morph.hash["hitomih"]] = float.Parse(array[4]);
					fieldValue[(int)morph.hash["hitomis"]] = float.Parse(array[5]);
					fieldValue[(int)morph.hash["mayuha"]] = float.Parse(array[6]);
					fieldValue[(int)morph.hash["mayuup"]] = float.Parse(array[7]);
					fieldValue[(int)morph.hash["mayuv"]] = float.Parse(array[8]);
					fieldValue[(int)morph.hash["mayuvhalf"]] = float.Parse(array[9]);
					fieldValue[(int)morph.hash["moutha"]] = float.Parse(array[10]);
					fieldValue[(int)morph.hash["mouths"]] = float.Parse(array[11]);
					fieldValue[(int)morph.hash["mouthdw"]] = float.Parse(array[12]);
					fieldValue[(int)morph.hash["mouthup"]] = float.Parse(array[13]);
					fieldValue[(int)morph.hash["tangout"]] = float.Parse(array[14]);
					fieldValue[(int)morph.hash["tangup"]] = float.Parse(array[15]);
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]);
					}
					else
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]) * 3f;
					}
					fieldValue[(int)morph.hash["mayuw"]] = float.Parse(array[18]);
					fieldValue[(int)morph.hash["mouthhe"]] = float.Parse(array[19]);
					fieldValue[(int)morph.hash["mouthc"]] = float.Parse(array[20]);
					fieldValue[(int)morph.hash["mouthi"]] = float.Parse(array[21]);
					fieldValue[(int)morph.hash["mouthuphalf"]] = float.Parse(array[22]) + 0.01f;
					try
					{
						fieldValue[(int)morph.hash["tangopen"]] = float.Parse(array[23]);
					}
					catch
					{
					}
					if (float.Parse(array[24]) == 1f)
					{
						fieldValue[(int)morph.hash["namida"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["namida"]] = 0f;
					}
					if (float.Parse(array[25]) == 1f)
					{
						fieldValue[(int)morph.hash["tear1"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear1"]] = 0f;
					}
					if (float.Parse(array[26]) == 1f)
					{
						fieldValue[(int)morph.hash["tear2"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear2"]] = 0f;
					}
					if (float.Parse(array[27]) == 1f)
					{
						fieldValue[(int)morph.hash["tear3"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear3"]] = 0f;
					}
					if (float.Parse(array[28]) == 1f)
					{
						fieldValue[(int)morph.hash["shock"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["shock"]] = 0f;
					}
					if (float.Parse(array[29]) == 1f)
					{
						fieldValue[(int)morph.hash["yodare"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["yodare"]] = 0f;
					}
					if (float.Parse(array[30]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho"]] = 0f;
					}
					if (float.Parse(array[31]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0f;
					}
					if (float.Parse(array[32]) == 1f)
					{
						fieldValue[(int)morph.hash["hohos"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohos"]] = 0f;
					}
					if (float.Parse(array[33]) == 1f)
					{
						fieldValue[(int)morph.hash["hohol"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohol"]] = 0f;
					}
					if (float.Parse(array[34]) == 1f)
					{
						fieldValue[(int)morph.hash["toothoff"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["toothoff"]] = 0f;
					}
					if (array.Length > 35)
					{
						if (float.Parse(array[35]) == 1f)
						{
							morph.boNoseFook = true;
						}
						else
						{
							morph.boNoseFook = false;
						}
					}
				}
				maidArray[selectMaidIndex].body0.Face.morph.FixBlendValues_Face();
				isFaceInit = true;
				faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
			}
			TMorph morph2 = maidArray[selectMaidIndex].body0.Face.morph;
			if (!isShosai)
			{
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(130), (float)GetPix(100), (float)GetPix(25)), "目の開閉", guistyle);
				eyeclose = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(150), (float)GetPix(70), (float)GetPix(20)), eyeclose, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(130), (float)GetPix(100), (float)GetPix(25)), "にっこり", guistyle);
				eyeclose2 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(150), (float)GetPix(70), (float)GetPix(20)), eyeclose2, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(170), (float)GetPix(100), (float)GetPix(25)), "ジト目", guistyle);
				if (morph2.bodyskin.PartsVersion < 120)
				{
					eyeclose3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(190), (float)GetPix(70), (float)GetPix(20)), eyeclose3, 0f, 1f);
				}
				else
				{
					eyeclose3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(190), (float)GetPix(70), (float)GetPix(20)), eyeclose3, 0f, 3f);
				}
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(170), (float)GetPix(100), (float)GetPix(25)), "ウインク", guistyle);
				eyeclose6 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(190), (float)GetPix(70), (float)GetPix(20)), eyeclose6, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(210), (float)GetPix(100), (float)GetPix(25)), "ハイライト", guistyle);
				hitomih = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(230), (float)GetPix(70), (float)GetPix(20)), hitomih, 0f, 2f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(210), (float)GetPix(100), (float)GetPix(25)), "瞳サイズ", guistyle);
				hitomis = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(230), (float)GetPix(70), (float)GetPix(20)), hitomis, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(250), (float)GetPix(100), (float)GetPix(25)), "眉角度", guistyle);
				mayuha = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(270), (float)GetPix(70), (float)GetPix(20)), mayuha, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(290), (float)GetPix(100), (float)GetPix(25)), "眉上げ", guistyle);
				mayuup = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(310), (float)GetPix(70), (float)GetPix(20)), mayuup, 0f, 0.8f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(290), (float)GetPix(100), (float)GetPix(25)), "眉下げ", guistyle);
				mayuv = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(310), (float)GetPix(70), (float)GetPix(20)), mayuv, 0f, 0.8f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(330), (float)GetPix(100), (float)GetPix(25)), "口開け1", guistyle);
				moutha = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(350), (float)GetPix(70), (float)GetPix(20)), moutha, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(330), (float)GetPix(100), (float)GetPix(25)), "口開け2", guistyle);
				mouths = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(350), (float)GetPix(70), (float)GetPix(20)), mouths, 0f, 0.9f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(370), (float)GetPix(100), (float)GetPix(25)), "口角上げ", guistyle);
				mouthup = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(390), (float)GetPix(70), (float)GetPix(20)), mouthup, 0f, 1.4f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(370), (float)GetPix(100), (float)GetPix(25)), "口角下げ", guistyle);
				mouthdw = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(390), (float)GetPix(70), (float)GetPix(20)), mouthdw, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(410), (float)GetPix(100), (float)GetPix(25)), "舌出し", guistyle);
				tangout = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(430), (float)GetPix(70), (float)GetPix(20)), tangout, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(410), (float)GetPix(100), (float)GetPix(25)), "舌上げ", guistyle);
				tangup = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(430), (float)GetPix(70), (float)GetPix(20)), tangup, 0f, 0.7f);
				isHoho2 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(455), (float)GetPix(50), (float)GetPix(20)), isHoho2, "赤面", guistyle5);
				isShock = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(455), (float)GetPix(70), (float)GetPix(20)), isShock, "ショック", guistyle5);
				isNamida = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(480), (float)GetPix(50), (float)GetPix(20)), isNamida, "涙", guistyle5);
				isYodare = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(480), (float)GetPix(50), (float)GetPix(20)), isYodare, "涎", guistyle5);
				isTear1 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(505), (float)GetPix(50), (float)GetPix(20)), isTear1, "涙1", guistyle5);
				isTear2 = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(505), (float)GetPix(50), (float)GetPix(20)), isTear2, "涙2", guistyle5);
				isTear3 = GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(505), (float)GetPix(50), (float)GetPix(20)), isTear3, "涙3", guistyle5);
				isHohos = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(530), (float)GetPix(50), (float)GetPix(20)), isHohos, "頬1", guistyle5);
				isHoho = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(530), (float)GetPix(50), (float)GetPix(20)), isHoho, "頬2", guistyle5);
				isHohol = GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(530), (float)GetPix(50), (float)GetPix(20)), isHohol, "頬3", guistyle5);
			}
			else
			{
				int num2 = 120;
				int num3 = 135;
				int num4 = 28;
				if (morph2.bodyskin.PartsVersion < 120)
				{
					if (morph2.hash["eyeclose7"] != null)
					{
						num4 = 26;
					}
				}
				else
				{
					num4 = 26;
				}
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2), (float)GetPix(100), (float)GetPix(25)), "目の開閉", guistyle2);
				eyeclose = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3), (float)GetPix(70), (float)GetPix(20)), eyeclose, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2), (float)GetPix(100), (float)GetPix(25)), "にっこり", guistyle2);
				eyeclose2 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3), (float)GetPix(70), (float)GetPix(20)), eyeclose2, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4), (float)GetPix(100), (float)GetPix(25)), "ジト目", guistyle2);
				if (morph2.bodyskin.PartsVersion < 120)
				{
					eyeclose3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4), (float)GetPix(70), (float)GetPix(20)), eyeclose3, 0f, 1f);
				}
				else
				{
					eyeclose3 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4), (float)GetPix(70), (float)GetPix(20)), eyeclose3, 0f, 3f);
				}
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4), (float)GetPix(100), (float)GetPix(25)), "見開く", guistyle2);
				if (morph2.bodyskin.PartsVersion < 120)
				{
					eyebig = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4), (float)GetPix(70), (float)GetPix(20)), eyebig, 0f, 1f);
				}
				else
				{
					eyebig = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4), (float)GetPix(70), (float)GetPix(20)), eyebig, 0f, 3f);
				}
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "ウインク1", guistyle2);
				eyeclose6 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose6, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "ウインク2", guistyle2);
				eyeclose5 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose5, 0f, 1f);
				if (morph2.bodyskin.PartsVersion < 120)
				{
					if (morph2.hash["eyeclose7"] != null)
					{
						num2 += num4;
						num3 += num4;
						GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "右ウインク1", guistyle2);
						eyeclose8 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose8, 0f, 1f);
						GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "右ウインク2", guistyle2);
						eyeclose7 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose7, 0f, 1f);
					}
				}
				else
				{
					num2 += num4;
					num3 += num4;
					GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "右ウインク1", guistyle2);
					eyeclose8 = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose8, 0f, 1f);
					GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 2), (float)GetPix(100), (float)GetPix(25)), "右ウインク2", guistyle2);
					eyeclose7 = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 2), (float)GetPix(70), (float)GetPix(20)), eyeclose7, 0f, 1f);
				}
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 3), (float)GetPix(100), (float)GetPix(25)), "ハイライト", guistyle2);
				hitomih = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 3), (float)GetPix(70), (float)GetPix(20)), hitomih, 0f, 2f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 3), (float)GetPix(100), (float)GetPix(25)), "瞳サイズ", guistyle2);
				hitomis = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 3), (float)GetPix(70), (float)GetPix(20)), hitomis, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 4), (float)GetPix(100), (float)GetPix(25)), "眉角度1", guistyle2);
				mayuha = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 4), (float)GetPix(70), (float)GetPix(20)), mayuha, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 4), (float)GetPix(100), (float)GetPix(25)), "眉角度2", guistyle2);
				mayuw = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 4), (float)GetPix(70), (float)GetPix(20)), mayuw, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 5), (float)GetPix(100), (float)GetPix(25)), "眉上げ", guistyle2);
				mayuup = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 5), (float)GetPix(70), (float)GetPix(20)), mayuup, 0f, 0.8f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 5), (float)GetPix(100), (float)GetPix(25)), "眉下げ1", guistyle2);
				mayuv = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 5), (float)GetPix(70), (float)GetPix(20)), mayuv, 0f, 0.8f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 6), (float)GetPix(100), (float)GetPix(25)), "眉下げ2", guistyle2);
				mayuvhalf = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 6), (float)GetPix(70), (float)GetPix(20)), mayuvhalf, 0f, 0.9f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 7), (float)GetPix(100), (float)GetPix(25)), "口開け1", guistyle2);
				moutha = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 7), (float)GetPix(70), (float)GetPix(20)), moutha, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 7), (float)GetPix(100), (float)GetPix(25)), "口開け2", guistyle2);
				mouths = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 7), (float)GetPix(70), (float)GetPix(20)), mouths, 0f, 0.9f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 8), (float)GetPix(100), (float)GetPix(25)), "口幅狭く", guistyle2);
				mouthc = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 8), (float)GetPix(70), (float)GetPix(20)), mouthc, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 8), (float)GetPix(100), (float)GetPix(25)), "口幅広く", guistyle2);
				mouthi = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 8), (float)GetPix(70), (float)GetPix(20)), mouthi, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 9), (float)GetPix(100), (float)GetPix(25)), "口角上げ", guistyle2);
				mouthup = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 9), (float)GetPix(70), (float)GetPix(20)), mouthup, 0f, 1.4f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 9), (float)GetPix(100), (float)GetPix(25)), "口角下げ", guistyle2);
				mouthdw = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 9), (float)GetPix(70), (float)GetPix(20)), mouthdw, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 10), (float)GetPix(100), (float)GetPix(25)), "口中央上げ", guistyle2);
				mouthhe = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 10), (float)GetPix(70), (float)GetPix(20)), mouthhe, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 10), (float)GetPix(100), (float)GetPix(25)), "左口角上げ", guistyle2);
				mouthuphalf = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 10), (float)GetPix(70), (float)GetPix(20)), mouthuphalf, 0f, 2f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 11), (float)GetPix(100), (float)GetPix(25)), "舌出し", guistyle2);
				tangout = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 11), (float)GetPix(70), (float)GetPix(20)), tangout, 0f, 1f);
				GUI.Label(new Rect((float)GetPix(88), (float)GetPix(num2 + num4 * 11), (float)GetPix(100), (float)GetPix(25)), "舌上げ", guistyle2);
				tangup = GUI.HorizontalSlider(new Rect((float)GetPix(88), (float)GetPix(num3 + num4 * 11), (float)GetPix(70), (float)GetPix(20)), tangup, 0f, 0.7f);
				GUI.Label(new Rect((float)GetPix(8), (float)GetPix(num2 + num4 * 12), (float)GetPix(100), (float)GetPix(25)), "舌根上げ", guistyle2);
				tangopen = GUI.HorizontalSlider(new Rect((float)GetPix(8), (float)GetPix(num3 + num4 * 12), (float)GetPix(70), (float)GetPix(20)), tangopen, 0f, 1f);
				bool enabled = GUI.enabled;
				if (!faceCombo.isClickedComboButton)
				{
					GUI.enabled = true;
				}
				GUI.enabled = enabled;
				isHoho2 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(489), (float)GetPix(50), (float)GetPix(16)), isHoho2, "赤面", guistyle5);
				isShock = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(489), (float)GetPix(58), (float)GetPix(16)), isShock, "ショック", guistyle5);
				isNosefook = GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(489), (float)GetPix(62), (float)GetPix(16)), isNosefook, "鼻フック", guistyle5);
				isNamida = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(511), (float)GetPix(50), (float)GetPix(16)), isNamida, "涙", guistyle5);
				isYodare = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(511), (float)GetPix(50), (float)GetPix(16)), isYodare, "涎", guistyle5);
				isToothoff = !GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(511), (float)GetPix(50), (float)GetPix(16)), !isToothoff, "歯", guistyle5);
				isTear1 = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(533), (float)GetPix(50), (float)GetPix(16)), isTear1, "涙1", guistyle5);
				isTear2 = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(533), (float)GetPix(50), (float)GetPix(16)), isTear2, "涙2", guistyle5);
				isTear3 = GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(533), (float)GetPix(50), (float)GetPix(16)), isTear3, "涙3", guistyle5);
				isHohos = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(555), (float)GetPix(50), (float)GetPix(16)), isHohos, "頬1", guistyle5);
				isHoho = GUI.Toggle(new Rect((float)GetPix(60), (float)GetPix(555), (float)GetPix(50), (float)GetPix(16)), isHoho, "頬2", guistyle5);
				isHohol = GUI.Toggle(new Rect((float)GetPix(115), (float)GetPix(555), (float)GetPix(50), (float)GetPix(16)), isHohol, "頬3", guistyle5);
			}
			int num5 = 0;
			if (isShosai)
			{
				num5 = 22;
			}
			isFaceEdit = GUI.Toggle(new Rect((float)GetPix(5), (float)GetPix(555 + num5), (float)GetPix(50), (float)GetPix(16)), isFaceEdit, "登録", guistyle5);
			if (isFaceEdit)
			{
				inName4 = GUI.TextField(new Rect((float)GetPix(5), (float)GetPix(575 + num5), (float)GetPix(100), (float)GetPix(20)), inName4);
				if (GUI.Button(new Rect((float)GetPix(107), (float)GetPix(575 + num5), (float)GetPix(35), (float)GetPix(20)), "追加", guistyle3))
				{
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					GUI.FocusControl("");
					int num6 = 1;
					for (int i = 1; i < 1000; i++)
					{
						IniKey iniKey = base.Preferences["face"]["f" + i];
						if (iniKey.Value == null)
						{
							num6 = i;
							break;
						}
					}
					TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
					float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
					float[] fieldValue2 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
					string text2 = inName4 + ":";
					if (morph.bodyskin.PartsVersion < 120)
					{
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose"]] + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose2"]] + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose3"]] + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose6"]] + ",";
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num]]] + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num]]] + ",";
						text2 = text2 + fieldValue[(int)morph.hash["eyeclose3"]] / 3f + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num]]] + ",";
					}
					text2 = text2 + fieldValue[(int)morph.hash["hitomih"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["hitomis"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mayuha"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mayuup"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mayuv"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mayuvhalf"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["moutha"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouths"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthdw"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthup"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["tangout"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["tangup"]] + ",";
					if (morph.bodyskin.PartsVersion < 120)
					{
						text2 = text2 + fieldValue[(int)morph.hash["eyebig"]] + ",";
					}
					else
					{
						text2 = text2 + fieldValue[(int)morph.hash["eyebig"]] / 3f + ",";
					}
					if (morph.bodyskin.PartsVersion < 120)
					{
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose5"]] + ",";
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num]]] + ",";
					}
					text2 = text2 + fieldValue[(int)morph.hash["mayuw"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthhe"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthc"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthi"]] + ",";
					text2 = text2 + fieldValue[(int)morph.hash["mouthuphalf"]] + ",";
					try
					{
						text2 = text2 + fieldValue[(int)morph.hash["tangopen"]] + ",";
					}
					catch
					{
						text2 += "0,";
					}
					if (fieldValue[(int)morph.hash["namida"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["tear1"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["tear2"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["tear3"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["shock"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["yodare"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["hoho"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["hoho2"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["hohos"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["hohol"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (fieldValue[(int)morph.hash["toothoff"]] > 0f)
					{
						text2 = text2 + 1 + ",";
					}
					else
					{
						text2 = text2 + 0 + ",";
					}
					if (morph.bodyskin.PartsVersion < 120)
					{
						if (morph.hash["eyeclose7"] != null)
						{
							text2 = text2 + fieldValue2[(int)morph.hash["eyeclose7"]] + ",";
							text2 = text2 + fieldValue2[(int)morph.hash["eyeclose8"]] + ":";
						}
						else
						{
							text2 += "0,0:";
						}
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] + ",";
						text2 = text2 + fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] + ":";
					}
					base.Preferences["face"]["f" + num6].Value = text2;
					base.SaveConfig();
					List<string> list = new List<string>(300);
					list.AddRange(faceArray);
					for (int i = 1; i < 300; i++)
					{
						IniKey iniKey = base.Preferences["face"]["f" + i];
						if (iniKey.Value == null)
						{
							break;
						}
						string[] array = iniKey.Value.Split(new char[]
						{
							':'
						});
						if (array.Length > 1)
						{
							list.Add(string.Concat(new object[]
							{
								array[0],
								"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000:",
								i,
								":",
								array[1]
							}));
						}
					}
					faceCombo.selectedItemIndex = 0;
					faceComboList = new GUIContent[list.ToArray().Length];
					for (int i = 0; i < list.ToArray().Length; i++)
					{
						faceComboList[i] = new GUIContent(list.ToArray()[i]);
					}
					faceCombo.selectedItemIndex = list.ToArray().Length - 1;
					inName4 = "";
				}
				if (faceIndex[selectMaidIndex] < faceArray.Length)
				{
					GUI.enabled = false;
				}
				if (GUI.Button(new Rect((float)GetPix(144), (float)GetPix(575 + num5), (float)GetPix(24), (float)GetPix(20)), "削", guistyle3))
				{
					GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					string[] array = faceComboList[faceIndex[selectMaidIndex]].text.Split(new char[]
					{
						':'
					});
					base.Preferences["face"]["f" + array[1]].Value = "del";
					base.SaveConfig();
					List<string> list = new List<string>(300);
					list.AddRange(faceArray);
					for (int i = 1; i < 300; i++)
					{
						IniKey iniKey = base.Preferences["face"]["f" + i];
						if (iniKey.Value == null)
						{
							break;
						}
						string[] array2 = iniKey.Value.Split(new char[]
						{
							':'
						});
						if (array2.Length > 1)
						{
							list.Add(string.Concat(new object[]
							{
								array2[0],
								"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000:",
								i,
								":",
								array2[1]
							}));
						}
					}
					faceCombo.selectedItemIndex = 0;
					faceComboList = new GUIContent[list.ToArray().Length];
					for (int i = 0; i < list.ToArray().Length; i++)
					{
						faceComboList[i] = new GUIContent(list.ToArray()[i]);
					}
					faceCombo.selectedItemIndex = 0;
					for (int j = 0; j < maidCnt; j++)
					{
						if (maidArray[j] && maidArray[j].Visible)
						{
							if (list.ToArray().Length <= faceIndex[j])
							{
								faceIndex[j] = 0;
							}
						}
					}
				}
				GUI.enabled = true;
			}
			if (faceCombo.isClickedComboButton)
			{
				GUI.enabled = true;
			}
			if (isFace[selectMaidIndex])
			{
				faceIndex[selectMaidIndex] = faceCombo.List(new Rect((float)GetPix(35), (float)GetPix(95), (float)GetPix(95), (float)GetPix(23)), faceComboList[faceIndex[selectMaidIndex]].text, faceComboList, guistyle4, "box", listStyle2);
			}
			else
			{
				faceCombo.List(new Rect((float)GetPix(35), (float)GetPix(95), (float)GetPix(95), (float)GetPix(23)), faceComboList[faceIndex[selectMaidIndex]].text, faceComboList, guistyle4, "box", listStyle2);
			}
			if (faceCombo.isClickedComboButton)
			{
				isCombo = true;
			}
			else if (isCombo)
			{
				isCombo = false;
				TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
				float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
				morph.EyeMabataki = 0f;
				if (faceIndex[selectMaidIndex] < faceArray.Length)
				{
					morph.MulBlendValues(faceArray[faceIndex[selectMaidIndex]], 1f);
					if (morph.bodyskin.PartsVersion >= 120)
					{
						fieldValue[(int)morph.hash["eyeclose3"]] *= 3f;
					}
				}
				else
				{
					float[] fieldValue2 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
					if (!isVR)
					{
						maidArray[selectMaidIndex].boMabataki = false;
					}
					string[] array = faceComboList[faceIndex[selectMaidIndex]].text.Split(new char[]
					{
						':'
					})[2].Split(new char[]
					{
						','
					});
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue2[(int)morph.hash["eyeclose"]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2"]] = float.Parse(array[1]);
						fieldValue2[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]);
						fieldValue2[(int)morph.hash["eyeclose6"]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5"]] = float.Parse(array[17]);
						if (morph.hash["eyeclose7"] != null)
						{
							if (array.Length > 37)
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = float.Parse(array[36]);
								fieldValue2[(int)morph.hash["eyeclose8"]] = float.Parse(array[37]);
							}
							else
							{
								fieldValue2[(int)morph.hash["eyeclose7"]] = 0f;
								fieldValue2[(int)morph.hash["eyeclose8"]] = 0f;
							}
						}
					}
					else
					{
						int num = 0;
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
						{
							num = 1;
						}
						if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
						{
							num = 2;
						}
						fieldValue2[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[0]);
						fieldValue2[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[1]);
						fieldValue[(int)morph.hash["eyeclose3"]] = float.Parse(array[2]) * 3f;
						fieldValue2[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[3]);
						fieldValue2[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[17]);
						if (array.Length > 37)
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[36]);
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = float.Parse(array[37]);
						}
						else
						{
							fieldValue2[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num]]] = 0f;
							fieldValue2[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num]]] = 0f;
						}
					}
					fieldValue[(int)morph.hash["hitomih"]] = float.Parse(array[4]);
					fieldValue[(int)morph.hash["hitomis"]] = float.Parse(array[5]);
					fieldValue[(int)morph.hash["mayuha"]] = float.Parse(array[6]);
					fieldValue[(int)morph.hash["mayuup"]] = float.Parse(array[7]);
					fieldValue[(int)morph.hash["mayuv"]] = float.Parse(array[8]);
					fieldValue[(int)morph.hash["mayuvhalf"]] = float.Parse(array[9]);
					fieldValue[(int)morph.hash["moutha"]] = float.Parse(array[10]);
					fieldValue[(int)morph.hash["mouths"]] = float.Parse(array[11]);
					fieldValue[(int)morph.hash["mouthdw"]] = float.Parse(array[12]);
					fieldValue[(int)morph.hash["mouthup"]] = float.Parse(array[13]);
					fieldValue[(int)morph.hash["tangout"]] = float.Parse(array[14]);
					fieldValue[(int)morph.hash["tangup"]] = float.Parse(array[15]);
					if (morph.bodyskin.PartsVersion < 120)
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]);
					}
					else
					{
						fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array[16]) * 3f;
					}
					fieldValue[(int)morph.hash["mayuw"]] = float.Parse(array[18]);
					fieldValue[(int)morph.hash["mouthhe"]] = float.Parse(array[19]);
					fieldValue[(int)morph.hash["mouthc"]] = float.Parse(array[20]);
					fieldValue[(int)morph.hash["mouthi"]] = float.Parse(array[21]);
					fieldValue[(int)morph.hash["mouthuphalf"]] = float.Parse(array[22]) + 0.01f;
					try
					{
						fieldValue[(int)morph.hash["tangopen"]] = float.Parse(array[23]);
					}
					catch
					{
					}
					if (float.Parse(array[24]) == 1f)
					{
						fieldValue[(int)morph.hash["namida"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["namida"]] = 0f;
					}
					if (float.Parse(array[25]) == 1f)
					{
						fieldValue[(int)morph.hash["tear1"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear1"]] = 0f;
					}
					if (float.Parse(array[26]) == 1f)
					{
						fieldValue[(int)morph.hash["tear2"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear2"]] = 0f;
					}
					if (float.Parse(array[27]) == 1f)
					{
						fieldValue[(int)morph.hash["tear3"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["tear3"]] = 0f;
					}
					if (float.Parse(array[28]) == 1f)
					{
						fieldValue[(int)morph.hash["shock"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["shock"]] = 0f;
					}
					if (float.Parse(array[29]) == 1f)
					{
						fieldValue[(int)morph.hash["yodare"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["yodare"]] = 0f;
					}
					if (float.Parse(array[30]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho"]] = 0f;
					}
					if (float.Parse(array[31]) == 1f)
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0.5f;
					}
					else
					{
						fieldValue[(int)morph.hash["hoho2"]] = 0f;
					}
					if (float.Parse(array[32]) == 1f)
					{
						fieldValue[(int)morph.hash["hohos"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohos"]] = 0f;
					}
					if (float.Parse(array[33]) == 1f)
					{
						fieldValue[(int)morph.hash["hohol"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["hohol"]] = 0f;
					}
					if (float.Parse(array[34]) == 1f)
					{
						fieldValue[(int)morph.hash["toothoff"]] = 1f;
					}
					else
					{
						fieldValue[(int)morph.hash["toothoff"]] = 0f;
					}
					if (array.Length > 35)
					{
						if (float.Parse(array[35]) == 1f)
						{
							morph.boNoseFook = true;
						}
						else
						{
							morph.boNoseFook = false;
						}
					}
				}
				maidArray[selectMaidIndex].body0.Face.morph.FixBlendValues_Face();
				isFaceInit = true;
				faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
			}
			if (faceCombo.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			else
			{
				GUI.enabled = true;
			}
			yotogiFlg = false;
			if (sceneLevel == 14)
			{
				if (GameObject.Find("/UI Root/YotogiPlayPanel/CommandViewer/SkillViewer/MaskGroup/SkillGroup/CommandParent/CommandUnit"))
				{
					yotogiFlg = true;
				}
			}
		}

		private void GuiFunc(int winID)
		{
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			GUIStyle guistyle = "label";
			guistyle.fontSize = GetPix(14);
			guistyle.alignment = TextAnchor.UpperLeft;
			GUIStyle guistyle2 = "button";
			guistyle2.fontSize = GetPix(16);
			guistyle2.alignment = TextAnchor.MiddleCenter;
			GUIStyle guistyle3 = new GUIStyle("toggle");
			guistyle3.fontSize = GetPix(13);
			float num = (float)GetPix(70);
			if (comboBoxList == null)
			{
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
			}
			bool value = true;
			if (faceFlg || poseFlg || sceneFlg || kankyoFlg || kankyo2Flg)
			{
				value = false;
			}
			if (!isF6)
			{
				if (GUI.Toggle(new Rect((float)GetPix(2), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), value, "配置", guistyle3))
				{
					faceFlg = false;
					poseFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
					bGui = true;
					isGuiInit = true;
				}
			}
			if (!yotogiFlg)
			{
				if (GUI.Toggle(new Rect((float)GetPix(41), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), poseFlg, "操作", guistyle3))
				{
					poseFlg = true;
					faceFlg = false;
					sceneFlg = false;
					kankyoFlg = false;
					kankyo2Flg = false;
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(80), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), faceFlg, "表情", guistyle3))
			{
				faceFlg = true;
				poseFlg = false;
				sceneFlg = false;
				kankyoFlg = false;
				kankyo2Flg = false;
				if (!faceFlg2)
				{
					isFaceInit = true;
					faceFlg2 = true;
					maidArray[selectMaidIndex].boMabataki = false;
					faceCombo.selectedItemIndex = faceIndex[selectMaidIndex];
				}
			}
			if (GUI.Toggle(new Rect((float)GetPix(119), (float)GetPix(2), (float)GetPix(39), (float)GetPix(20)), kankyoFlg, "環境", guistyle3))
			{
				poseFlg = false;
				faceFlg = false;
				sceneFlg = false;
				kankyoFlg = true;
				kankyo2Flg = false;
			}
			if (!line1)
			{
				line1 = MakeTex(2, 2, new Color(0f, 0f, 0f, 0.3f));
				line2 = MakeTex(2, 2, new Color(0.7f, 0.7f, 0.7f, 0.6f));
			}
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 2f), line1);
			GUI.DrawTexture(new Rect((float)GetPix(5), (float)GetPix(20), (float)GetPix(160), 1f), line2);
			int stockMaidCount = characterMgr.GetStockMaidCount();
			Rect position;
			Rect viewRect;
			if (sceneLevel != 5)
			{
				position = new Rect((float)GetPix(7), (float)GetPix(110), rectWin.width - (float)GetPix(14), rectWin.height * 0.83f);
				viewRect = new Rect(0f, 0f, position.width * 0.85f, (num + (float)GetPix(5)) * (float)stockMaidCount + (float)GetPix(15));
			}
			else
			{
				position = new Rect((float)GetPix(7), (float)GetPix(110), rectWin.width - (float)GetPix(14), rectWin.height * 0.83f * 0.98f);
				viewRect = new Rect(0f, 0f, position.width * 0.85f, (num + (float)GetPix(5)) * (float)stockMaidCount + (float)GetPix(15) * 0.92f);
			}
			float num2 = 0f;
			if (comboBoxControl.isClickedComboButton)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(new Rect((float)GetPix(10), (float)GetPix(78), rectWin.width * 0.85f, (float)GetPix(28)), "呼び出す", guistyle2))
			{
				isYobidashi = true;
				selectMaidIndex = 0;
				copyIndex = 0;
				for (int i = 0; i < maxMaidCnt; i++)
				{
					if (!isLock[i])
					{
						if (maidArray[i] && maidArray[i].Visible)
						{
							maidArray[i].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							maidArray[i].SetAutoTwistAll(true);
						}
					}
					maidArray[i] = null;
				}
				for (int i = 0; i < maxMaidCnt; i++)
				{
					isStop[i] = false;
				}
				bGui = false;
				isFadeOut = true;
				GameMain.Instance.MainCamera.FadeOut(0f, false, null, true, default(Color));
				for (int j = 0; j < characterMgr.GetStockMaidCount(); j++)
				{
					characterMgr.GetStockMaidList()[j].Visible = false;
				}
			}
			GUIStyle guistyle4 = "button";
			guistyle4.fontSize = GetPix(13);
			GUIStyleState guistyleState = new GUIStyleState();
			if (GUI.Button(new Rect((float)GetPix(10), (float)GetPix(52), rectWin.width * 0.4f, (float)GetPix(23)), "7人選択", guistyle4))
			{
				if (sceneLevel != 5)
				{
					selectList = new ArrayList();
					selectList.Add(0);
					selectList.Add(1);
					selectList.Add(2);
					selectList.Add(3);
					selectList.Add(4);
					selectList.Add(5);
					selectList.Add(6);
				}
				else
				{
					int stockMaidCount2 = characterMgr.GetStockMaidCount();
					selectList = new ArrayList();
					selectList.Add(editMaid);
					if (stockMaidCount2 > 1)
					{
						if (editMaid >= 1)
						{
							selectList.Add(0);
						}
						else if (stockMaidCount2 > 2)
						{
							selectList.Add(1);
						}
					}
					if (stockMaidCount2 > 2)
					{
						if (editMaid >= 2)
						{
							selectList.Add(1);
						}
						else if (stockMaidCount2 > 3)
						{
							selectList.Add(2);
						}
					}
					if (stockMaidCount2 > 3)
					{
						if (editMaid >= 3)
						{
							selectList.Add(2);
						}
						else if (stockMaidCount2 > 4)
						{
							selectList.Add(3);
						}
					}
					if (stockMaidCount2 > 4)
					{
						if (editMaid >= 4)
						{
							selectList.Add(3);
						}
						else if (stockMaidCount2 > 5)
						{
							selectList.Add(4);
						}
					}
					if (stockMaidCount2 > 5)
					{
						if (editMaid >= 5)
						{
							selectList.Add(4);
						}
						else if (stockMaidCount2 > 6)
						{
							selectList.Add(5);
						}
					}
					if (stockMaidCount2 > 6)
					{
						if (editMaid >= 6)
						{
							selectList.Add(5);
						}
						else if (stockMaidCount2 > 7)
						{
							selectList.Add(6);
						}
					}
				}
			}
			if (GUI.Button(new Rect(rectWin.width * 0.5f, (float)GetPix(52), rectWin.width * 0.4f, (float)GetPix(23)), "選択解除", guistyle4))
			{
				selectList = new ArrayList();
				if (sceneLevel == 5)
				{
					selectList.Add(editMaid);
				}
			}
			GUI.enabled = true;
			scrollPos = GUI.BeginScrollView(position, scrollPos, viewRect);
			for (int k = 0; k < characterMgr.GetStockMaidCount(); k++)
			{
				GUI.enabled = true;
				GUI.DrawTexture(new Rect(2f, num2 + 2f, rectWin.width * 0.83f - 4f, num - 4f), Texture2D.whiteTexture);
				bool flag = false;
				for (int j = 0; j < selectList.Count; j++)
				{
					if ((int)selectList[j] == k)
					{
						flag = true;
						break;
					}
				}
				if (comboBoxControl.isClickedComboButton)
				{
					GUI.enabled = false;
					GUI.Button(new Rect(0f, num2, rectWin.width * 0.83f, num), "", guistyle2);
					GUI.Button(new Rect(0f, num2, rectWin.width * 0.83f, num), "", guistyle2);
				}
				if (GUI.Button(new Rect(0f, num2, rectWin.width * 0.83f, num), "", guistyle2))
				{
					if (flag)
					{
						for (int j = 0; j < selectList.Count; j++)
						{
							if ((int)selectList[j] == k)
							{
								if (sceneLevel != 5 || (int)selectList[j] != editMaid)
								{
									selectList.Remove(k);
									break;
								}
							}
						}
					}
					else
					{
						if (selectList.Count > maxMaidCnt - 1)
						{
							selectList.Remove(selectList[maxMaidCnt - 1]);
						}
						selectList.Add(k);
					}
				}
				GUI.enabled = true;
				if (flag)
				{
					GUI.DrawTexture(new Rect(5f, num2 + 5f, rectWin.width * 0.83f - 10f, num - 10f), Texture2D.whiteTexture);
				}
				if (characterMgr.GetStockMaid(k).GetThumIcon())
				{
					GUI.DrawTexture(new Rect(0f, num2 - 5f, num, num), characterMgr.GetStockMaid(k).GetThumIcon());
				}
				string text = characterMgr.GetStockMaid(k).status.lastName + "\n" + characterMgr.GetStockMaid(k).status.firstName;
				guistyleState.textColor = Color.black;
				guistyle.normal = guistyleState;
				GUI.Label(new Rect((float)GetPix(65), num2 + num / 4f, num * 2f, num * 3f), text, guistyle);
				if (flag)
				{
					for (int j = 0; j < selectList.Count; j++)
					{
						if ((int)selectList[j] == k)
						{
							GUI.Label(new Rect(rectWin.width * 0.7f, num2 + 6f, num, num), (j + 1).ToString(), guistyle);
							break;
						}
					}
				}
				num2 += num + (float)GetPix(5);
			}
			GUI.EndScrollView();
			guistyleState.textColor = Color.white;
			guistyle.normal = guistyleState;
			int num3 = comboBoxControl.GetSelectedItemIndex();
			num3 = comboBoxControl.List(new Rect((float)GetPix(10), (float)GetPix(25), rectWin.width * 0.56f, (float)GetPix(24)), comboBoxList[num3].text, comboBoxList, listStyle);
			if (GUI.Button(new Rect(rectWin.width * 0.66f, (float)GetPix(25), rectWin.width * 0.24f, (float)GetPix(24)), "決定", guistyle4))
			{
				for (int i = 0; i < maxMaidCnt; i++)
				{
					isStop[i] = false;
				}
				switch (comboBoxControl.GetSelectedItemIndex())
				{
					case 0:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							if (selectList.Count <= 7)
							{
								if (selectList.Count % 2 == 1)
								{
									switch (i)
									{
										case 0:
											maidArray[i].SetPos(new Vector3(0f, 0f, 0f));
											break;
										case 1:
											maidArray[i].SetPos(new Vector3(-0.6f, 0f, 0.26f));
											break;
										case 2:
											maidArray[i].SetPos(new Vector3(0.6f, 0f, 0.26f));
											break;
										case 3:
											maidArray[i].SetPos(new Vector3(-1.1f, 0f, 0.69f));
											break;
										case 4:
											maidArray[i].SetPos(new Vector3(1.1f, 0f, 0.69f));
											break;
										case 5:
											maidArray[i].SetPos(new Vector3(-1.47f, 0f, 1.1f));
											break;
										case 6:
											maidArray[i].SetPos(new Vector3(1.47f, 0f, 1.1f));
											break;
									}
								}
								else
								{
									switch (i)
									{
										case 0:
											maidArray[i].SetPos(new Vector3(0.3f, 0f, 0f));
											break;
										case 1:
											maidArray[i].SetPos(new Vector3(-0.3f, 0f, 0f));
											break;
										case 2:
											maidArray[i].SetPos(new Vector3(0.7f, 0f, 0.4f));
											break;
										case 3:
											maidArray[i].SetPos(new Vector3(-0.7f, 0f, 0.4f));
											break;
										case 4:
											maidArray[i].SetPos(new Vector3(1f, 0f, 0.9f));
											break;
										case 5:
											maidArray[i].SetPos(new Vector3(-1f, 0f, 0.9f));
											break;
									}
								}
							}
							else
							{
								float num4 = 0f;
								if (selectList.Count >= 11)
								{
									num4 = -0.4f;
									if (selectList.Count % 2 == 1)
									{
										switch (i)
										{
											case 0:
												maidArray[i].SetPos(new Vector3(0f, 0f, 0f + num4));
												break;
											case 1:
												maidArray[i].SetPos(new Vector3(-0.5f, 0f, 0.2f + num4));
												break;
											case 2:
												maidArray[i].SetPos(new Vector3(0.5f, 0f, 0.2f + num4));
												break;
											case 3:
												maidArray[i].SetPos(new Vector3(-0.9f, 0f, 0.55f + num4));
												break;
											case 4:
												maidArray[i].SetPos(new Vector3(0.9f, 0f, 0.55f + num4));
												break;
											case 5:
												maidArray[i].SetPos(new Vector3(-1.25f, 0f, 0.9f + num4));
												break;
											case 6:
												maidArray[i].SetPos(new Vector3(1.25f, 0f, 0.9f + num4));
												break;
											case 7:
												maidArray[i].SetPos(new Vector3(-1.57f, 0f, 1.3f + num4));
												break;
											case 8:
												maidArray[i].SetPos(new Vector3(1.57f, 0f, 1.3f + num4));
												break;
											case 9:
												maidArray[i].SetPos(new Vector3(-1.77f, 0f, 1.72f + num4));
												break;
											case 10:
												maidArray[i].SetPos(new Vector3(1.77f, 0f, 1.72f + num4));
												break;
											case 11:
												maidArray[i].SetPos(new Vector3(-1.85f, 0f, 2.17f + num4));
												break;
											case 12:
												maidArray[i].SetPos(new Vector3(1.85f, 0f, 2.17f + num4));
												break;
											default:
												maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f + num4));
												break;
										}
									}
									else
									{
										switch (i)
										{
											case 0:
												maidArray[i].SetPos(new Vector3(0.25f, 0f, 0f + num4));
												break;
											case 1:
												maidArray[i].SetPos(new Vector3(-0.25f, 0f, 0f + num4));
												break;
											case 2:
												maidArray[i].SetPos(new Vector3(0.7f, 0f, 0.25f + num4));
												break;
											case 3:
												maidArray[i].SetPos(new Vector3(-0.7f, 0f, 0.25f + num4));
												break;
											case 4:
												maidArray[i].SetPos(new Vector3(1.05f, 0f, 0.6f + num4));
												break;
											case 5:
												maidArray[i].SetPos(new Vector3(-1.05f, 0f, 0.6f + num4));
												break;
											case 6:
												maidArray[i].SetPos(new Vector3(1.35f, 0f, 0.9f + num4));
												break;
											case 7:
												maidArray[i].SetPos(new Vector3(-1.35f, 0f, 0.9f + num4));
												break;
											case 8:
												maidArray[i].SetPos(new Vector3(1.6f, 0f, 1.3f + num4));
												break;
											case 9:
												maidArray[i].SetPos(new Vector3(-1.6f, 0f, 1.3f + num4));
												break;
											case 10:
												maidArray[i].SetPos(new Vector3(1.8f, 0f, 1.72f + num4));
												break;
											case 11:
												maidArray[i].SetPos(new Vector3(-1.8f, 0f, 1.72f + num4));
												break;
											case 12:
												maidArray[i].SetPos(new Vector3(1.9f, 0f, 2.17f + num4));
												break;
											case 13:
												maidArray[i].SetPos(new Vector3(-1.9f, 0f, 2.17f + num4));
												break;
											default:
												maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f + num4));
												break;
										}
									}
								}
								else if (selectList.Count >= 8)
								{
									if (selectList.Count >= 9)
									{
										num4 = -0.2f;
									}
									if (selectList.Count % 2 == 1)
									{
										switch (i)
										{
											case 0:
												maidArray[i].SetPos(new Vector3(0f, 0f, 0f + num4));
												break;
											case 1:
												maidArray[i].SetPos(new Vector3(-0.55f, 0f, 0.2f + num4));
												break;
											case 2:
												maidArray[i].SetPos(new Vector3(0.55f, 0f, 0.2f + num4));
												break;
											case 3:
												maidArray[i].SetPos(new Vector3(-1f, 0f, 0.6f + num4));
												break;
											case 4:
												maidArray[i].SetPos(new Vector3(1f, 0f, 0.6f + num4));
												break;
											case 5:
												maidArray[i].SetPos(new Vector3(-1.35f, 0f, 1f + num4));
												break;
											case 6:
												maidArray[i].SetPos(new Vector3(1.35f, 0f, 1f + num4));
												break;
											case 7:
												maidArray[i].SetPos(new Vector3(-1.6f, 0f, 1.4f + num4));
												break;
											case 8:
												maidArray[i].SetPos(new Vector3(1.6f, 0f, 1.4f + num4));
												break;
										}
									}
									else
									{
										switch (i)
										{
											case 0:
												maidArray[i].SetPos(new Vector3(0.28f, 0f, 0f + num4));
												break;
											case 1:
												maidArray[i].SetPos(new Vector3(-0.28f, 0f, 0f + num4));
												break;
											case 2:
												maidArray[i].SetPos(new Vector3(0.78f, 0f, 0.3f + num4));
												break;
											case 3:
												maidArray[i].SetPos(new Vector3(-0.78f, 0f, 0.3f + num4));
												break;
											case 4:
												maidArray[i].SetPos(new Vector3(1.22f, 0f, 0.7f + num4));
												break;
											case 5:
												maidArray[i].SetPos(new Vector3(-1.22f, 0f, 0.7f + num4));
												break;
											case 6:
												maidArray[i].SetPos(new Vector3(1.55f, 0f, 1.1f + num4));
												break;
											case 7:
												maidArray[i].SetPos(new Vector3(-1.55f, 0f, 1.1f + num4));
												break;
											case 8:
												maidArray[i].SetPos(new Vector3(1.77f, 0f, 1.58f + num4));
												break;
											case 9:
												maidArray[i].SetPos(new Vector3(-1.77f, 0f, 1.58f + num4));
												break;
										}
									}
								}
							}
							zero2.y = (float)(Math.Atan2((double)maidArray[i].transform.position.x, (double)(maidArray[i].transform.position.z - 1.5f)) * 180.0 / 3.1415926535897931) + 180f;
							maidArray[i].SetRot(zero2);
						}
						break;
					case 1:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							if (maidCnt < 9)
							{
								switch (i)
								{
									case 1:
										zero.x = -0.6f;
										break;
									case 2:
										zero.x = 0.6f;
										break;
									case 3:
										zero.x = -1.2f;
										break;
									case 4:
										zero.x = 1.2f;
										break;
									case 5:
										zero.x = -1.8f;
										break;
									case 6:
										zero.x = 1.8f;
										break;
									case 7:
										zero.x = -2.4f;
										break;
									case 8:
										zero.x = 2.4f;
										break;
									case 9:
										zero.x = -3f;
										break;
									case 10:
										zero.x = 3f;
										break;
								}
							}
							else
							{
								switch (i)
								{
									case 1:
										zero.x = -0.5f;
										break;
									case 2:
										zero.x = 0.5f;
										break;
									case 3:
										zero.x = -1f;
										break;
									case 4:
										zero.x = 1f;
										break;
									case 5:
										zero.x = -1.5f;
										break;
									case 6:
										zero.x = 1.5f;
										break;
									case 7:
										zero.x = -2f;
										break;
									case 8:
										zero.x = 2f;
										break;
									case 9:
										zero.x = -2.5f;
										break;
									case 10:
										zero.x = 2.5f;
										break;
									case 11:
										zero.x = -3f;
										break;
									case 12:
										zero.x = 3f;
										break;
									case 13:
										zero.x = -3.5f;
										break;
								}
							}
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 2:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							if (maidCnt < 9)
							{
								switch (i)
								{
									case 1:
										zero.z = 0.6f;
										break;
									case 2:
										zero.z = -0.6f;
										break;
									case 3:
										zero.z = 1.2f;
										break;
									case 4:
										zero.z = -1.2f;
										break;
									case 5:
										zero.z = 1.8f;
										break;
									case 6:
										zero.z = -1.8f;
										break;
									case 7:
										zero.z = 2.4f;
										break;
									case 8:
										zero.z = -2.4f;
										break;
									case 9:
										zero.z = 3f;
										break;
									case 10:
										zero.z = -3f;
										break;
								}
							}
							else
							{
								switch (i)
								{
									case 1:
										zero.z = 0.5f;
										break;
									case 2:
										zero.z = -0.5f;
										break;
									case 3:
										zero.z = 1f;
										break;
									case 4:
										zero.z = -1f;
										break;
									case 5:
										zero.z = 1.5f;
										break;
									case 6:
										zero.z = -1.5f;
										break;
									case 7:
										zero.z = 2f;
										break;
									case 8:
										zero.z = -2f;
										break;
									case 9:
										zero.z = 2.5f;
										break;
									case 10:
										zero.z = -2.5f;
										break;
									case 11:
										zero.z = 3f;
										break;
									case 12:
										zero.z = -3f;
										break;
									case 13:
										zero.z = 3.5f;
										break;
								}
							}
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 3:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num5 = 0.5f;
							if (maidCnt > 11)
							{
								num5 = 0.25f;
							}
							else if (maidCnt > 9)
							{
								num5 = 0.32f;
							}
							else if (maidCnt > 7)
							{
								num5 = 0.4f;
							}
							switch (i)
							{
								case 1:
									zero.x = -num5;
									zero.z = num5;
									break;
								case 2:
									zero.x = num5;
									zero.z = -num5;
									break;
								case 3:
									zero.x = -num5 * 2f;
									zero.z = num5 * 2f;
									break;
								case 4:
									zero.x = num5 * 2f;
									zero.z = -num5 * 2f;
									break;
								case 5:
									zero.x = -num5 * 3f;
									zero.z = num5 * 3f;
									break;
								case 6:
									zero.x = num5 * 3f;
									zero.z = -num5 * 3f;
									break;
								case 7:
									zero.x = -num5 * 4f;
									zero.z = num5 * 4f;
									break;
								case 8:
									zero.x = num5 * 4f;
									zero.z = -num5 * 4f;
									break;
								case 9:
									zero.x = -num5 * 5f;
									zero.z = num5 * 5f;
									break;
								case 10:
									zero.x = num5 * 5f;
									zero.z = -num5 * 5f;
									break;
								case 11:
									zero.x = -num5 * 6f;
									zero.z = num5 * 6f;
									break;
								case 12:
									zero.x = num5 * 6f;
									zero.z = -num5 * 6f;
									break;
								case 13:
									zero.x = -num5 * 7f;
									zero.z = num5 * 7f;
									break;
							}
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 4:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num6 = 0.4f + 0.08f * (float)maidCnt;
							zero.x = (float)((double)num6 * Math.Cos(0.017453292519943295 * (double)(90 + 360 * i / maidCnt)));
							zero.z = (float)((double)num6 * Math.Sin(0.017453292519943295 * (double)(90 + 360 * i / maidCnt)));
							maidArray[i].SetPos(zero);
							zero2.y = (float)(Math.Atan2((double)zero.x, (double)zero.z) * 180.0 / 3.1415926535897931);
							maidArray[i].SetRot(zero2);
						}
						break;
					case 5:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num6 = 0.4f + 0.08f * (float)maidCnt;
							zero.x = (float)((double)num6 * Math.Cos(0.017453292519943295 * (double)(90 + 360 * i / maidCnt)));
							zero.z = (float)((double)num6 * Math.Sin(0.017453292519943295 * (double)(90 + 360 * i / maidCnt)));
							maidArray[i].SetPos(zero);
							zero2.y = (float)(Math.Atan2((double)zero.x, (double)zero.z) * 180.0 / 3.1415926535897931) + 180f;
							maidArray[i].SetRot(zero2);
						}
						break;
					case 6:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num7 = 0f;
							if (maidCnt > 9)
							{
								num7 = -0.4f;
							}
							else if (maidCnt > 7)
							{
								num7 = -0.2f;
							}
							switch (i)
							{
								case 0:
									zero.z = 0.3f;
									break;
								case 1:
									zero.x = -0.9f;
									zero.z = -0.4f;
									zero2.y = 40f;
									break;
								case 2:
									zero.x = 0.9f;
									zero.z = -0.4f;
									zero2.y = -40f;
									break;
								case 3:
									zero.x = -0.4f;
									zero.z = -0.8f;
									zero2.y = 20f;
									break;
								case 4:
									zero.x = 0.4f;
									zero.z = -0.8f;
									zero2.y = -20f;
									break;
								case 5:
									zero.x = -1.2f;
									zero.z = 0.1f;
									zero2.y = 60f;
									break;
								case 6:
									zero.x = 1.2f;
									zero.z = 0.1f;
									zero2.y = -60f;
									break;
								case 7:
									zero.x = -1.5f;
									zero.z = 0.6f;
									zero2.y = 80f;
									break;
								case 8:
									zero.x = 1.5f;
									zero.z = 0.6f;
									zero2.y = -80f;
									break;
								case 9:
									zero.x = -1.6f;
									zero.z = 1.15f;
									zero2.y = 100f;
									break;
								case 10:
									zero.x = 1.6f;
									zero.z = 1.15f;
									zero2.y = -100f;
									break;
								case 11:
									zero.x = -1.6f;
									zero.z = 1.65f;
									zero2.y = 110f;
									break;
								case 12:
									zero.x = 1.65f;
									zero.z = 1.65f;
									zero2.y = -110f;
									break;
								case 13:
									zero.x = -1.65f;
									zero.z = 2.15f;
									zero2.y = 120f;
									break;
							}
							if (i > 0)
							{
								zero.z += num7;
							}
							else
							{
								zero.z -= num7;
							}
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 7:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num7 = 0f;
							if (maidCnt > 11)
							{
								num7 = 0.6f;
							}
							else if (maidCnt > 9)
							{
								num7 = 0.4f;
							}
							else if (maidCnt > 7)
							{
								num7 = 0.2f;
							}
							switch (i)
							{
								case 0:
									zero.z = 0.8f;
									break;
								case 1:
									zero.x = -0.45f;
									zero.z = 0.3f;
									zero2.y = -10f;
									break;
								case 2:
									zero.x = 0.45f;
									zero.z = 0.3f;
									zero2.y = 10f;
									break;
								case 3:
									zero.x = -0.8f;
									zero.z = -0.2f;
									zero2.y = -20f;
									break;
								case 4:
									zero.x = 0.8f;
									zero.z = -0.2f;
									zero2.y = 20f;
									break;
								case 5:
									zero.x = -1.2f;
									zero.z = -0.75f;
									zero2.y = -30f;
									break;
								case 6:
									zero.x = 1.2f;
									zero.z = -0.75f;
									zero2.y = 30f;
									break;
								case 7:
									zero.x = -1.6f;
									zero.z = -1.25f;
									zero2.y = -40f;
									break;
								case 8:
									zero.x = 1.6f;
									zero.z = -1.25f;
									zero2.y = 40f;
									break;
								case 9:
									zero.x = -2f;
									zero.z = -1.75f;
									zero2.y = -50f;
									break;
								case 10:
									zero.x = 2f;
									zero.z = -1.75f;
									zero2.y = 50f;
									break;
								case 11:
									zero.x = -2.4f;
									zero.z = -2.25f;
									zero2.y = -60f;
									break;
								case 12:
									zero.x = 2.4f;
									zero.z = -2.25f;
									zero2.y = 60f;
									break;
								case 13:
									zero.x = -2.8f;
									zero.z = -2.75f;
									zero2.y = -70f;
									break;
							}
							zero.z += num7;
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 8:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num7 = 0f;
							if (maidCnt > 11)
							{
								num7 = -0.6f;
							}
							else if (maidCnt > 9)
							{
								num7 = -0.4f;
							}
							else if (maidCnt > 7)
							{
								num7 = -0.2f;
							}
							switch (i)
							{
								case 0:
									zero.z = -0.75f;
									break;
								case 1:
									zero.x = -0.45f;
									zero.z = -0.2f;
									zero2.y = 20f;
									break;
								case 2:
									zero.x = 0.45f;
									zero.z = -0.2f;
									zero2.y = -20f;
									break;
								case 3:
									zero.x = -0.8f;
									zero.z = 0.3f;
									zero2.y = 35f;
									break;
								case 4:
									zero.x = 0.8f;
									zero.z = 0.3f;
									zero2.y = -35f;
									break;
								case 5:
									zero.x = -1.2f;
									zero.z = 0.8f;
									zero2.y = 50f;
									break;
								case 6:
									zero.x = 1.2f;
									zero.z = 0.8f;
									zero2.y = -50f;
									break;
								case 7:
									zero.x = -1.6f;
									zero.z = 1.3f;
									zero2.y = 65f;
									break;
								case 8:
									zero.x = 1.6f;
									zero.z = 1.3f;
									zero2.y = -65f;
									break;
								case 9:
									zero.x = -2f;
									zero.z = 1.8f;
									zero2.y = 80f;
									break;
								case 10:
									zero.x = 2f;
									zero.z = 1.8f;
									zero2.y = -80f;
									break;
								case 11:
									zero.x = -2.4f;
									zero.z = 2.3f;
									zero2.y = 90f;
									break;
								case 12:
									zero.x = 2.4f;
									zero.z = 2.3f;
									zero2.y = -90f;
									break;
								case 13:
									zero.x = -2.8f;
									zero.z = 2.8f;
									zero2.y = 100f;
									break;
							}
							zero.z += num7;
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 9:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num5 = 1f;
							if (maidCnt > 9)
							{
								num5 = 0.84f;
							}
							else if (maidCnt > 7)
							{
								num5 = 0.92f;
							}
							switch (i)
							{
								case 0:
									zero.z = 0f;
									break;
								case 1:
									zero.x = -0.5f;
									zero.z = -0.5f;
									break;
								case 2:
									zero.x = 0.5f;
									zero.z = -0.5f;
									break;
								case 3:
									zero.x = -1f;
									zero.z = 0.2f;
									break;
								case 4:
									zero.x = 1f;
									zero.z = 0.2f;
									break;
								case 5:
									zero.x = -1.5f;
									zero.z = -0.5f;
									break;
								case 6:
									zero.x = 1.5f;
									zero.z = -0.5f;
									break;
								case 7:
									zero.x = -2f;
									zero.z = 0.2f;
									break;
								case 8:
									zero.x = 2f;
									zero.z = 0.2f;
									break;
								case 9:
									zero.x = -2.5f;
									zero.z = -0.5f;
									break;
								case 10:
									zero.x = 2.5f;
									zero.z = -0.5f;
									break;
								case 11:
									zero.x = -3f;
									zero.z = 0.2f;
									break;
								case 12:
									zero.x = 3f;
									zero.z = 0.2f;
									break;
								case 13:
									zero.x = -3.5f;
									zero.z = -0.5f;
									break;
							}
							zero.x *= num5;
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
					case 10:
						for (int i = 0; i < maidCnt; i++)
						{
							Vector3 zero = Vector3.zero;
							Vector3 zero2 = Vector3.zero;
							float num5 = 1f;
							if (maidCnt > 9)
							{
								num5 = 0.84f;
							}
							else if (maidCnt > 7)
							{
								num5 = 0.92f;
							}
							switch (i)
							{
								case 0:
									zero.z = -0.4f;
									break;
								case 1:
									zero.x = -0.5f;
									zero.z = 0.2f;
									break;
								case 2:
									zero.x = 0.5f;
									zero.z = 0.2f;
									break;
								case 3:
									zero.x = -1f;
									zero.z = -0.5f;
									break;
								case 4:
									zero.x = 1f;
									zero.z = -0.5f;
									break;
								case 5:
									zero.x = -1.5f;
									zero.z = 0.2f;
									break;
								case 6:
									zero.x = 1.5f;
									zero.z = 0.2f;
									break;
								case 7:
									zero.x = -2f;
									zero.z = -0.5f;
									break;
								case 8:
									zero.x = 2f;
									zero.z = -0.5f;
									break;
								case 9:
									zero.x = -2.5f;
									zero.z = 0.2f;
									break;
								case 10:
									zero.x = 2.5f;
									zero.z = 0.2f;
									break;
								case 11:
									zero.x = -3f;
									zero.z = -0.5f;
									break;
								case 12:
									zero.x = 3f;
									zero.z = -0.5f;
									break;
								case 13:
									zero.x = -3.5f;
									zero.z = 0.2f;
									break;
							}
							zero.x *= num5;
							maidArray[i].SetPos(zero);
							maidArray[i].SetRot(zero2);
							if (i >= 14)
							{
								maidArray[i].SetPos(new Vector3(0f, 0f, 0.7f));
							}
						}
						break;
				}
				for (int i = 0; i < maxMaidCnt; i++)
				{
					if (!isLock[i])
					{
						if (maidArray[i] != null && maidArray[i].Visible)
						{
							maidArray[i].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
							maidArray[i].SetAutoTwistAll(true);
						}
					}
				}
			}
		}
	}
}
