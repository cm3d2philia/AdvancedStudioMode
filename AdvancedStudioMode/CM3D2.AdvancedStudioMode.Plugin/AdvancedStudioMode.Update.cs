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
		public void Update()
		{
			if (isInit)
			{
				if (allowUpdate)
				{
					MaidUpdate();
					if (isFadeOut)
					{
						bool flag = false;
						for (int i2 = 0; i2 < maxMaidCnt; i2++)
						{
							if (maidArray[i2] && maidArray[i2].Visible && maidArray[i2].IsAllProcPropBusy)
							{
								flag = true;
							}
						}
						if (!flag)
						{
							if (!isBusyInit)
							{
								isBusyInit = true;
							}
							else
							{
								for (int i2 = 0; i2 < maxMaidCnt; i2++)
								{
									if (!isLock[i2])
									{
										if (maidArray[i2] != null)
										{
											maidArray[i2].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
											maidArray[i2].SetAutoTwistAll(true);
										}
									}
									poseCount[i2] = 30;
									if (maidArray[i2] && maidArray[i2].Visible)
									{
										maidArray[i2].body0.BoneHitHeightY = -10f;
										if (selectList.Count > i2)
										{
											if (goSlot[(int)selectList[i2]] == null)
											{
												maidArray[i2].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
												maidArray[i2].SetAutoTwistAll(true);
												goSlot[(int)selectList[i2]] = new List<TBodySkin>(maidArray[i2].body0.goSlot);
												if (!isVR)
												{
													try
													{
														shodaiFlg[(int)selectList[i2]] = false;
														TMorph morph = maidArray[i2].body0.Face.morph;
														float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
														float item = fieldValue[(int)morph.hash["tangopen"]];
													}
													catch
													{
														shodaiFlg[(int)selectList[i2]] = true;
													}
													if (!isVR)
													{
														eyeL[(int)selectList[i2]] = maidArray[i2].body0.quaDefEyeL.eulerAngles;
														eyeR[(int)selectList[i2]] = maidArray[i2].body0.quaDefEyeR.eulerAngles;
													}
												}
												if (isKamiyure)
												{
													for (int j = 0; j < maidArray[i2].body0.goSlot.Count; j++)
													{
														if (j >= 3 && j <= 6)
														{
															if (maidArray[i2].body0.goSlot[j].obj != null)
															{
																DynamicBone component = maidArray[i2].body0.goSlot[j].obj.GetComponent<DynamicBone>();
																if (component != null)
																{
																	component.m_Damping = kamiyure2;
																	component.m_Elasticity = kamiyure3;
																	if (j == 5)
																	{
																		component.m_Elasticity = kamiyure3 / 20f;
																	}
																	component.m_Radius = kamiyure4;
																	component.UpdateParameters();
																}
															}
														}
													}
												}
											}
										}
									}
								}
								isBusyInit = false;
								GameMain.Instance.MainCamera.FadeIn(1f, false, null, true, true, default(Color));
								isFadeOut = false;
								bGui = true;
							}
						}
					}
				}
				if (isVR && isVRScroll)
				{
					if (!getModKeyPressing(AdvancedStudioMode.modKey.Ctrl) && !getModKeyPressing(AdvancedStudioMode.modKey.Alt) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Y) && !Input.GetKey(KeyCode.V) && !Input.GetKey(KeyCode.C) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
					{
						Vector3 vector = AdvancedStudioMode.GetFieldValue<OvrCamera, Vector3>(GameMain.Instance.OvrMgr.OvrCamera, "v");
						string text = "TrackingSpace/CenterEyeAnchor";
						if (GameMain.Instance.VRFamily == GameMain.VRFamilyType.HTC)
						{
							text = "Main Camera (eye)";
						}
						GameObject childObject = UTY.GetChildObject(mainCamera.gameObject, text, false);
						Transform transform = childObject.transform;
						Vector3 a = transform.rotation * Vector3.forward;
						vector += a * (Input.GetAxis("Mouse ScrollWheel") * (5f * Time.deltaTime * 2f * 5f));
						AdvancedStudioMode.SetFieldValue3<OvrCamera, Vector3>(GameMain.Instance.OvrMgr.OvrCamera, "v", vector);
						Transform fieldValue2 = AdvancedStudioMode.GetFieldValue<OvrCamera, Transform>(GameMain.Instance.OvrMgr.OvrCamera, "m_trBaseHead");
						fieldValue2.position = vector;
						AdvancedStudioMode.SetFieldValue4<OvrCamera, Transform>(GameMain.Instance.OvrMgr.OvrCamera, "m_trBaseHead", fieldValue2);
					}
				}
				if (isMekure1a || isMekure2a || isZurasia)
				{
					for (int i2 = 0; i2 < maidCnt; i2++)
					{
						if (maidArray[i2] && maidArray[i2].Visible)
						{
							if (isMekure1a)
							{
								if (isMekure1)
								{
									maidArray[i2].ItemChangeTemp("skirt", "めくれスカート");
									maidArray[i2].ItemChangeTemp("onepiece", "めくれスカート");
								}
								else
								{
									ResetProp(maidArray[i2], MPN.skirt);
									ResetProp(maidArray[i2], MPN.onepiece);
								}
								maidArray[i2].AllProcPropSeqStart();
							}
							if (isMekure2a)
							{
								if (isMekure2)
								{
									maidArray[i2].ItemChangeTemp("skirt", "めくれスカート後ろ");
									maidArray[i2].ItemChangeTemp("onepiece", "めくれスカート後ろ");
								}
								else
								{
									ResetProp(maidArray[i2], MPN.skirt);
									ResetProp(maidArray[i2], MPN.onepiece);
								}
								maidArray[i2].AllProcPropSeqStart();
							}
							if (isZurasia)
							{
								if (isZurasi)
								{
									maidArray[i2].ItemChangeTemp("panz", "パンツずらし");
									maidArray[i2].ItemChangeTemp("mizugi", "パンツずらし");
								}
								else
								{
									ResetProp(maidArray[i2], MPN.panz);
									ResetProp(maidArray[i2], MPN.mizugi);
								}
								maidArray[i2].AllProcPropSeqStart();
							}
						}
					}
					isMekure1a = false;
					isMekure2a = false;
					isZurasia = false;
				}
				if (isKamiyure)
				{
					int num = maidCnt;
					if (num == 0)
					{
						num = 3;
					}
					for (int i2 = 0; i2 < num; i2++)
					{
						if (maidArray[i2] && maidArray[i2].Visible)
						{
							Maid maid = maidArray[i2];
							for (int j = 0; j < maid.body0.goSlot.Count; j++)
							{
								if (j >= 3 && j <= 6)
								{
									if (maid.body0.goSlot[j].obj != null)
									{
										DynamicBone component = maid.body0.goSlot[j].obj.GetComponent<DynamicBone>();
										if (component != null)
										{
											if (component.m_Damping != kamiyure2 || component.m_Elasticity != kamiyure3 || component.m_Radius != kamiyure4)
											{
												if (j == 5)
												{
													if (component.m_Damping == kamiyure2 && component.m_Elasticity == kamiyure3 / 20f && component.m_Radius == kamiyure4)
													{
														goto IL_A35;
													}
												}
												component.m_Damping = kamiyure2;
												component.m_Elasticity = kamiyure3;
												if (j == 5)
												{
													component.m_Elasticity = kamiyure3 / 20f;
												}
												component.m_Radius = kamiyure4;
												component.UpdateParameters();
											}
										}
									}
								}
							IL_A35:;
							}
						}
					}
				}
				if (isSkirtyure)
				{
					int num = maidCnt;
					if (num == 0)
					{
						num = 3;
					}
					for (int i2 = 0; i2 < num; i2++)
					{
						if (maidArray[i2] && maidArray[i2].Visible)
						{
							int j = 0;
							while (j < maidArray[i2].body0.goSlot.Count)
							{
								if (maidArray[i2].body0.goSlot[j].obj != null)
								{
									DynamicSkirtBone fieldValue3 = AdvancedStudioMode.GetFieldValue<BoneHair3, DynamicSkirtBone>(maidArray[i2].body0.goSlot[j].bonehair3, "m_SkirtBone");
									if (fieldValue3 != null)
									{
										fieldValue3.m_vGravity = new Vector3(0.5f, 0.5f, 0.5f);
										if (fieldValue3.m_fPanierForce != skirtyure3 || fieldValue3.m_fPanierForceDistanceThreshold != skirtyure2 || fieldValue3.m_fRegDefaultRadius != skirtyure4)
										{
											fieldValue3.m_fPanierForce = skirtyure3;
											fieldValue3.m_fPanierForceDistanceThreshold = skirtyure2;
											fieldValue3.m_fRegDefaultRadius = skirtyure4;
										}
									}
								}
								//IL_BC8:
								j++;
								continue;
								//goto IL_BC8;
							}
						}
					}
				}
				if ((!Input.GetKey(KeyCode.KeypadEnter) && Input.GetKeyDown(KeyCode.Keypad1)) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.KeypadDivide) || Input.GetKeyDown(KeyCode.KeypadMultiply) || Input.GetKeyDown(KeyCode.KeypadMinus))
				{
					if (Input.GetKey(KeyCode.H) || isVR)
					{
						if (Input.GetKeyDown(KeyCode.Keypad1))
						{
							isWear = !isWear;
						}
						if (Input.GetKeyDown(KeyCode.Keypad2))
						{
							isSkirt = !isSkirt;
						}
						if (Input.GetKeyDown(KeyCode.Keypad3))
						{
							isBra = !isBra;
						}
						if (Input.GetKeyDown(KeyCode.Keypad4))
						{
							isPanz = !isPanz;
						}
						if (Input.GetKeyDown(KeyCode.Keypad5))
						{
							isHeadset = !isHeadset;
						}
						if (Input.GetKeyDown(KeyCode.Keypad6))
						{
							isAccUde = !isAccUde;
						}
						if (Input.GetKeyDown(KeyCode.Keypad7))
						{
							isGlove = !isGlove;
						}
						if (Input.GetKeyDown(KeyCode.Keypad8))
						{
							isStkg = !isStkg;
						}
						if (Input.GetKeyDown(KeyCode.Keypad9))
						{
							isShoes = !isShoes;
						}
						if (Input.GetKeyDown(KeyCode.Keypad0))
						{
							isAccSenaka = !isAccSenaka;
						}
						if (Input.GetKeyDown(KeyCode.KeypadDivide))
						{
							isMekure1 = !isMekure1;
						}
						if (Input.GetKeyDown(KeyCode.KeypadMultiply))
						{
							isMekure2 = !isMekure2;
						}
						if (Input.GetKeyDown(KeyCode.KeypadMinus))
						{
							isZurasi = !isZurasi;
						}
						h2Flg = true;
						if (sceneLevel != 3 && sceneLevel != 5)
						{
							maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
							maidArray[1] = GameMain.Instance.CharacterMgr.GetMaid(1);
							maidArray[2] = GameMain.Instance.CharacterMgr.GetMaid(2);
							maidArray[3] = GameMain.Instance.CharacterMgr.GetMaid(3);
							maidCnt = 4;
						}
						for (int i2 = 0; i2 < maidCnt; i2++)
						{
							if (maidArray[i2] && maidArray[i2].Visible)
							{
								if (Input.GetKeyDown(KeyCode.KeypadDivide) || Input.GetKeyDown(KeyCode.KeypadMultiply) || Input.GetKeyDown(KeyCode.KeypadMinus))
								{
									if (sceneLevel == 3 || sceneLevel == 5)
									{
										if (Input.GetKeyDown(KeyCode.KeypadDivide))
										{
											isMekure1a = true;
										}
										if (Input.GetKeyDown(KeyCode.KeypadMultiply))
										{
											isMekure2a = true;
										}
										if (Input.GetKeyDown(KeyCode.KeypadMinus))
										{
											isZurasia = true;
										}
									}
									else
									{
										if (Input.GetKeyDown(KeyCode.KeypadDivide))
										{
											if (isMekure1)
											{
												maidArray[i2].ItemChangeTemp("skirt", "めくれスカート");
												maidArray[i2].ItemChangeTemp("onepiece", "めくれスカート");
											}
											else
											{
												ResetProp(maidArray[i2], MPN.skirt);
												ResetProp(maidArray[i2], MPN.onepiece);
											}
											maidArray[i2].AllProcPropSeqStart();
										}
										if (Input.GetKeyDown(KeyCode.KeypadMultiply))
										{
											if (isMekure2)
											{
												maidArray[i2].ItemChangeTemp("skirt", "めくれスカート後ろ");
												maidArray[i2].ItemChangeTemp("onepiece", "めくれスカート後ろ");
											}
											else
											{
												ResetProp(maidArray[i2], MPN.skirt);
												ResetProp(maidArray[i2], MPN.onepiece);
											}
											maidArray[i2].AllProcPropSeqStart();
										}
										if (Input.GetKeyDown(KeyCode.KeypadMinus))
										{
											if (isZurasi)
											{
												maidArray[i2].ItemChangeTemp("panz", "パンツずらし");
												maidArray[i2].ItemChangeTemp("mizugi", "パンツずらし");
											}
											else
											{
												ResetProp(maidArray[i2], MPN.panz);
												ResetProp(maidArray[i2], MPN.mizugi);
											}
											maidArray[i2].AllProcPropSeqStart();
										}
									}
								}
								else
								{
									Hashtable fieldValue4 = AdvancedStudioMode.GetFieldValue<TBody, Hashtable>(maidArray[i2].body0, "m_hFoceHide");
									if (Input.GetKeyDown(KeyCode.Keypad1))
									{
										fieldValue4[7] = isWear;
										fieldValue4[9] = isWear;
										fieldValue4[10] = isWear;
									}
									if (Input.GetKeyDown(KeyCode.Keypad2))
									{
										fieldValue4[8] = isSkirt;
									}
									if (Input.GetKeyDown(KeyCode.Keypad3))
									{
										fieldValue4[12] = isBra;
									}
									if (Input.GetKeyDown(KeyCode.Keypad4))
									{
										fieldValue4[11] = isPanz;
									}
									if (Input.GetKeyDown(KeyCode.Keypad5))
									{
										fieldValue4[15] = isHeadset;
										fieldValue4[40] = isHeadset;
									}
									if (Input.GetKeyDown(KeyCode.Keypad6))
									{
										fieldValue4[29] = isAccUde;
									}
									if (Input.GetKeyDown(KeyCode.Keypad7))
									{
										fieldValue4[16] = isGlove;
									}
									if (Input.GetKeyDown(KeyCode.Keypad8))
									{
										fieldValue4[13] = isStkg;
									}
									if (Input.GetKeyDown(KeyCode.Keypad9))
									{
										fieldValue4[14] = isShoes;
									}
									if (Input.GetKeyDown(KeyCode.Keypad0))
									{
										fieldValue4[31] = isAccSenaka;
									}
									AdvancedStudioMode.SetFieldValue6<TBody, Hashtable>(maidArray[i2].body0, "m_hFoceHide", fieldValue4);
									maidArray[i2].body0.FixMaskFlag();
									maidArray[i2].body0.FixVisibleFlag(false);
								}
							}
						}
					}
				}
				if (!yotogiFlg && (sceneLevel == 14 || sceneLevel == 24))
				{
					Maid maid = maidArray[0];
					Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
					Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
					Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
					if (Input.GetKeyDown(KeyCode.LeftBracket) || (Input.GetKeyDown(KeyCode.BackQuote) && getModKeyPressing(AdvancedStudioMode.modKey.Alt)))
					{
						for (int k = 0; k < 10; k++)
						{
							maid.transform.RotateAround(maid.transform.position, Vector3.right, -maid.transform.rotation.eulerAngles.x);
							maid.transform.RotateAround(maid.transform.position, Vector3.forward, -maid.transform.rotation.eulerAngles.z);
						}
					}
					else if ((Input.GetKey(KeyCode.Minus) && getModKeyPressing(AdvancedStudioMode.modKey.Shift)) || (Input.GetKey(KeyCode.I) && getModKeyPressing(AdvancedStudioMode.modKey.Alt)))
					{
						maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), 0.4f);
					}
					else if ((Input.GetKey(KeyCode.Quote) && getModKeyPressing(AdvancedStudioMode.modKey.Shift)) || (Input.GetKey(KeyCode.K) && getModKeyPressing(AdvancedStudioMode.modKey.Alt)))
					{
						maid.transform.RotateAround(maid.transform.position, new Vector3(vector3.x, 0f, vector3.z), -0.4f);
					}
					else if (Input.GetKey(KeyCode.Minus) || (Input.GetKey(KeyCode.J) && getModKeyPressing(AdvancedStudioMode.modKey.Alt)))
					{
						maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), 0.4f);
					}
					else if (Input.GetKey(KeyCode.Quote) || (Input.GetKey(KeyCode.L) && getModKeyPressing(AdvancedStudioMode.modKey.Alt)))
					{
						maid.transform.RotateAround(maid.transform.position, new Vector3(vector2.x, 0f, vector2.z), -0.4f);
					}
					if (Input.GetKeyUp(KeyCode.H) && !hFlg)
					{
						if (h2Flg)
						{
							h2Flg = false;
						}
						else
						{
							if (isVR)
							{
								isF6 = true;
							}
							if (!maidArray[0])
							{
								maidArray[0] = GameMain.Instance.CharacterMgr.GetMaid(0);
							}
							string value = "";
							if (wearIndex == 0)
							{
								value = "Underwear";
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
								value = "Nude";
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
								value = "None";
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
							TBody.MaskMode maskMode = (TBody.MaskMode)Enum.Parse(typeof(TBody.MaskMode), value);
							for (int i2 = 0; i2 < maidCnt; i2++)
							{
								if (maidArray[i2] && maidArray[i2].Visible)
								{
									maidArray[i2].body0.SetMaskMode(maskMode);
								}
							}
						}
					}
				}
				if (isVR)
				{
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha1))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10000;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha2))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10001;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha3))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10002;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha4))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10003;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha5))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10004;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha6))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10005;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha7))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10006;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha8))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10007;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha9))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10008;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
					if (Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.Alpha0))
					{
						string text2 = GameMain.Instance.BgMgr.GetBGName();
						int l = 0;
						foreach (string text3 in bgArray)
						{
							if (text3 == text2)
							{
								bgIndex = l;
								bgIndex6 = l;
								break;
							}
							l++;
						}
						lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
						lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
						lightX6 = lightX[0];
						lightY6 = lightY[0];
						loadScene = 10009;
						kankyoLoadFlg = true;
						GameMain.Instance.SoundMgr.PlaySe("se002.ogg", false);
					}
				}
				if (isSavePose4)
				{
					isSavePose4 = false;
					Maid maid = maidArray[selectMaidIndex];
					Vector3 localEulerAngles = maid.transform.localEulerAngles;
					maid.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					Vector3 position = maid.transform.position;
					maid.transform.position = new Vector3(0f, 0f, 0f);
					Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
					transform2.eulerAngles = bipRotation;
					transform2.position = bipPosition;
					maid.transform.localEulerAngles = localEulerAngles;
					maid.transform.position = position;
					CacheBoneDataArray cacheBoneDataArray = maid.gameObject.AddComponent<CacheBoneDataArray>();
					cacheBoneDataArray.CreateCache(maid.body0.GetBone("Bip01"));
					byte[] anmBinary = cacheBoneDataArray.GetAnmBinary(true, true);
					string path4 = Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose\\" + inName3 + ".anm";
					for (int l = 0; l < 100; l++)
					{
						if (!File.Exists(path4))
						{
							break;
						}
						inName3 += "_";
						path4 = Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose\\" + inName3 + ".anm";
					}
					File.WriteAllBytes(path4, anmBinary);
					strList2 = new List<string>();
					strListE = new List<string>();
					strListE2 = new List<string>();
					strListS = new List<string>();
					strListD = new List<string>();
					strS = "";
					List<string> list = new List<string>(350 + poseArray2.Length);
					list.AddRange(poseArray2);
					List<string> list2 = new List<string>();
					for (int l = 11; l < 200; l++)
					{
						if (l < 100)
						{
							using (AFileBase afileBase = GameUty.FileSystem.FileOpen("edit_pose_0" + l + "_f.anm"))
							{
								if (afileBase.IsValid())
								{
									list2.Add("edit_pose_0" + l + "_f");
								}
							}
						}
						else
						{
							using (AFileBase afileBase = GameUty.FileSystem.FileOpen("edit_pose_" + l + "_f.anm"))
							{
								if (afileBase.IsValid())
								{
									list2.Add("edit_pose_" + l + "_f");
								}
							}
						}
					}
					for (int l = 15; l < 25; l++)
					{
						for (int i2 = 0; i2 < 2; i2++)
						{
							string text = "s";
							if (i2 == 1)
							{
								text = "w";
							}
							for (int k = 1; k < 20; k++)
							{
								if (k < 10)
								{
									using (AFileBase afileBase = GameUty.FileSystem.FileOpen(string.Concat(new object[]
									{
										"edit_pose_dg",
										l,
										text,
										"_00",
										k,
										"_f.anm"
									})))
									{
										if (afileBase.IsValid())
										{
											list2.Add(string.Concat(new object[]
											{
												"edit_pose_dg",
												l,
												text,
												"_00",
												k,
												"_f"
											}));
										}
									}
								}
								else
								{
									using (AFileBase afileBase = GameUty.FileSystem.FileOpen(string.Concat(new object[]
									{
										"edit_pose_dg",
										l,
										text,
										"_0",
										k,
										"_f.anm"
									})))
									{
										if (afileBase.IsValid())
										{
											list2.Add(string.Concat(new object[]
											{
												"edit_pose_dg",
												l,
												text,
												"_0",
												k,
												"_f"
											}));
										}
									}
								}
							}
						}
					}
					if (list2.Count > 0)
					{
						list.AddRange(list2.ToArray());
					}
					list.AddRange(poseArrayVP2);
					list.AddRange(poseArrayFB);
					list.AddRange(poseArray4);
					list.AddRange(poseArray5);
					list.AddRange(poseArray6);
					poseArray = list.ToArray();
					Action<string, List<string>> action = delegate (string path, List<string> result_list)
					{
						string[] files = Directory.GetFiles(path);
						countS = 0;
						for (int num208 = 0; num208 < files.Length; num208++)
						{
							if (Path.GetExtension(files[num208]) == ".anm")
							{
								string text21 = files[num208].Split(new char[]
								{
									'\\'
								})[files[num208].Split(new char[]
								{
									'\\'
								}).Length - 1];
								text21 = text21.Split(new char[]
								{
									'.'
								})[0];
								strListS.Add(text21 + "\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000/" + files[num208]);
								countS++;
							}
						}
					};
					List<string> arg = new List<string>();
					action(Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose", arg);
					string[] list3 = GameUty.FileSystem.GetList("motion", AFileSystemBase.ListType.AllFile);
					int num2 = 0;
					List<string> list4 = new List<string>();
					string[] array = list3;
					int m = 0;
					while (m < array.Length)
					{
						string path2 = array[m];
						string text3 = Path.GetFileNameWithoutExtension(path2);
						string directoryName = Path.GetDirectoryName(path2);
						if (!text3.StartsWith("maid_motion_") && !text3.StartsWith("work_00") && !text3.EndsWith("_3_") && !text3.EndsWith("_5_") && !text3.StartsWith("ck_") && !text3.StartsWith("vr_") && !text3.StartsWith("dance_mc") && !text3.Contains("a01_") && !text3.StartsWith("j_") && !text3.StartsWith("k_") && !text3.StartsWith("t_") && !text3.StartsWith("cbl_") && !text3.Contains("b01_") && !text3.Contains("b02_") && !text3.Contains("_kubi_") && !text3.EndsWith("_m2") && !text3.EndsWith("_m3") && !text3.Contains("_m2_once") && !text3.Contains("_m3_once") && !text3.StartsWith("h_") && !text3.StartsWith("event_") && !text3.StartsWith("man_") && !text3.EndsWith("_m") && !text3.Contains("_m_") && !text3.Contains("_man"))
						{
							if (!(text3 == "dance_cm3d2_001_zoukin") && !(text3 == "dance_cm3d2_001_mop") && !(text3 == "maid_motion") && !(text3 == "aruki_1_idougo_f") && !(text3 == "sleep2") && !(text3 == "stand_akire2") && !(text3 == "ero_scene_001") && !(text3 == "ero_scenefm_001") && !(text3 == "training_001") && !(text3 == "workff_001") && !(text3 == "workfm_001") && !(text3 == "dance_cm3d21_005_moe_mset") && !(text3 == "hinpyoukai_001"))
							{
								if (!directoryName.Contains("\\sex\\"))
								{
									if (!text3.StartsWith("sex_"))
									{
										if (text3.StartsWith("dance_test"))
										{
											strListD.Add(text3);
										}
										else
										{
											bool flag2 = false;
											foreach (string text in strListS)
											{
												string text4 = text.Split(new char[]
												{
													'/'
												})[0].Replace("\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000", "");
												if (text3 == text4)
												{
													flag2 = true;
													break;
												}
											}
											if (!flag2)
											{
												list4.Add(text3);
											}
										}
									}
								}
								else if (!text3.StartsWith("pose_"))
								{
									strListE.Add(text3);
								}
							}
						}
						//IL_2E50:
						m++;
						continue;
						//goto IL_2E50;
					}
					foreach (string text in list4)
					{
						bool flag3 = false;
						for (int l = 0; l < poseArray.Length; l++)
						{
							if (text == poseArray[l])
							{
								flag3 = true;
							}
						}
						if (!flag3 && text.StartsWith("edit_"))
						{
							strList2.Add(text);
						}
					}
					foreach (string text in list4)
					{
						bool flag3 = false;
						for (int l = 0; l < poseArray.Length; l++)
						{
							if (text == poseArray[l])
							{
								flag3 = true;
							}
						}
						if (!flag3 && text.StartsWith("pose_"))
						{
							strList2.Add(text);
						}
					}
					foreach (string text in list4)
					{
						bool flag3 = false;
						for (int l = 0; l < poseArray.Length; l++)
						{
							if (text == poseArray[l])
							{
								flag3 = true;
							}
						}
						if (!flag3 && !text.StartsWith("edit_") && !text.StartsWith("pose_"))
						{
							strList2.Add(text);
						}
					}
                    foreach (string text in strListE)
                    {
                        bool flag3 = false;
                        for (int l = 0; l < poseArray.Length; l++)
                        {
                            if (text == poseArray[l])
                            {
                                flag3 = true;
                            }
                        }
                        if (!flag3 && !text.StartsWith("amayakasi_taimen_sex") && !text.StartsWith("amayakasi_tekoki") && !text.StartsWith("anal_name") && !text.StartsWith("kousoku_irruma2") && !text.StartsWith("om_ganmenkijyoui") && !text.StartsWith("om_haimenekiben") && !text.StartsWith("om_haimenzai2") && !text.StartsWith("om_kakae_seijyoui") && !text.StartsWith("om_manguri2") && !text.StartsWith("om_tati_kunni") && !text.StartsWith("ran3p_3ana_kakae_sex") && !text.StartsWith("ran4p_dai_seijyoui") && !text.StartsWith("sokui3") && !text.StartsWith("tati_aibu") && !text.StartsWith("tetunagi_kijyoui"))
                        {
                            strListE2.Add(text);
							num2++;
                        }
                    }
                    list.AddRange(strList2.ToArray());
                    list.AddRange(strListE2.ToArray());
                    existPose = false;
					poseIniStr = "";
					List<IniKey> keys = base.Preferences["pose"].Keys;
					foreach (IniKey iniKey in keys)
					{
						IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
						if (iniKey2.Value != null && iniKey2.Value.ToString() != "")
						{
							if (iniKey2.Value.ToString() != "del")
							{
								list.AddRange(new string[]
								{
									iniKey.Key
								});
								existPose = true;
								if (poseIniStr == "")
								{
									poseIniStr = iniKey.Key;
								}
							}
						}
					}
					list.AddRange(strListS.ToArray());
					poseArray = list.ToArray();
					List<string> list5 = new List<string>(50 + poseGroupArray2.Length);
					list5.AddRange(poseGroupArray2);
					list5.AddRange(poseGroupArrayVP);
					list5.AddRange(poseGroupArrayFB);
					list5.AddRange(poseGroupArray3);
					list5.Add(poseArray5[0]);
					list5.Add(poseArray6[0]);
                    list5.Add(strList2[0]);
                    list5.Add(strListE2[0]);
                    existPose = true;
					if (strListS.Count > 0 && poseIniStr == "")
					{
						list5.Add(strListS[0]);
					}
					if (poseIniStr != "")
					{
						list5.Add(poseIniStr);
					}
					poseGroupArray = list5.ToArray();
					groupList = new ArrayList();
					for (int k = 0; k < poseArray.Length; k++)
					{
						for (int i2 = 0; i2 < poseGroupArray.Length; i2++)
						{
							if (poseGroupArray[i2] == poseArray[k])
							{
								groupList.Add(k);
								if (poseGroupArray[i2] == strList2[0])
								{
									sPoseCount = k;
								}
							}
						}
					}
					poseGroupComboList = new GUIContent[poseGroupArray.Length + 1];
					poseGroupComboList[0] = new GUIContent("1:通常");
					for (int n = 0; n < poseGroupArray.Length; n++)
					{
						if (poseGroupArray[n] == "maid_dressroom01")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":立ち");
						}
						if (poseGroupArray[n] == "tennis_kamae_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":中腰");
						}
						if (poseGroupArray[n] == "senakanagasi_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":膝をつく");
						}
						if (poseGroupArray[n] == "work_hansei")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":座り");
						}
						if (poseGroupArray[n] == "inu_taiki_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":四つん這い");
						}
						if (poseGroupArray[n] == "syagami_pose_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":床座り");
						}
						if (poseGroupArray[n] == "densyasuwari_taiki_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":椅子座り");
						}
						if (poseGroupArray[n] == "work_kaiwa")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":ソファー座り");
						}
						if (poseGroupArray[n] == "dance_cm3d2_001_f1,14.14")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":ドキドキ☆Fallin' Love");
						}
						if (poseGroupArray[n] == "dance_cm3d_001_f1,39.25")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":entrance to you");
						}
						if (poseGroupArray[n] == "dance_cm3d_002_end_f1,50.71")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":scarlet leap");
						}
						if (poseGroupArray[n] == "dance_cm3d2_002_smt_f,7.76,")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":stellar my tears");
						}
						if (poseGroupArray[n] == "dance_cm3d_003_sp2_f1,90.15")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":rhythmix to you");
						}
						if (poseGroupArray[n] == "dance_cm3d2_003_hs_f1,0.01,")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":happy!happy!スキャンダル!!");
						}
						if (poseGroupArray[n] == "dance_cm3d_004_kano_f1,124.93")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":Can Know Two Close");
						}
						if (poseGroupArray[n] == "dance_cm3d2_004_sse_f1,0.01")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":sweet sweet everyday");
						}
						if (poseGroupArray[n] == "turusi_sex_in_taiki_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":拘束");
						}
						if (poseGroupArray[n] == "rosyutu_pose01_f")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":エロ");
						}
						if (poseGroupArray[n] == "rosyutu_aruki_f_once_,1.37")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":歩き");
						}
						if (poseGroupArray[n] == "stand_desk1")
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":その他");
						}
						if (poseGroupArray[n] == poseArray5[0])
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":ダンスMC");
						}
						if (poseGroupArray[n] == poseArray6[0])
						{
							poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":ダンス");
						}
                        if (n == poseGroupArray.Length - 3)
                        {
                            poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":通常2");
                        }
                        if (n == poseGroupArray.Length - 2)
                        {
                            poseGroupComboList[n + 1] = new GUIContent(n + 2 + ":エロ2");
                        }
                        if (n == poseGroupArray.Length - 1)
						{
							poseGroupComboList[n + 1] = new GUIContent("99:登録ポーズ");
						}
					}
					int num3 = -1;
					for (int k = 0; k < groupList.Count; k++)
					{
						if (poseIndex[selectMaidIndex] < (int)groupList[k])
						{
							num3 = k;
							break;
						}
					}
					int num4 = (int)groupList[0];
					int num5 = 0;
					if (num3 > 0)
					{
						num4 = (int)groupList[num3] - (int)groupList[num3 - 1];
						num5 = (int)groupList[num3 - 1];
					}
					if (num3 < 0)
					{
						num3 = groupList.Count;
						num4 = poseArray.Length - (int)groupList[num3 - 1];
						num5 = (int)groupList[num3 - 1];
					}
					poseComboList = new GUIContent[num4];
					int num6 = 0;
					for (int j = num5; j < num5 + num4; j++)
					{
						bool flag4 = false;
						List<IniKey> keys2 = base.Preferences["pose"].Keys;
						foreach (IniKey iniKey in keys2)
						{
							if (poseArray[j] == iniKey.Key)
							{
								IniKey iniKey2 = base.Preferences["pose"][iniKey.Key];
								if (iniKey2.Value != null && iniKey2.Value.ToString() != "" && iniKey2.Value.ToString() != "del")
								{
									poseComboList[num6] = new GUIContent(string.Concat(new object[]
									{
										num6 + 1,
										":",
										iniKey2.Value.Split(new char[]
										{
											'_'
										})[0],
										"\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000",
										iniKey.Key
									}));
									flag4 = true;
								}
							}
						}
						if (!flag4)
						{
							poseComboList[num6] = new GUIContent(num6 + 1 + ":" + poseArray[j]);
						}
						num6++;
					}
					poseGroupCombo.selectedItemIndex = num3;
					poseGroupIndex = num3;
					poseCombo.selectedItemIndex = 0;
					for (int l = (int)groupList[groupList.Count - 1]; l < poseArray.Length; l++)
					{
						string text4 = poseArray[l].Split(new char[]
						{
							'/'
						})[0].Replace("\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000", "");
						if (text4 == inName3)
						{
							poseIndex[selectMaidIndex] = l;
							string path3 = poseArray[l].Split(new char[]
							{
								'/'
							})[1];
							byte[] array210 = new byte[0];
							try
							{
								using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
								{
									array210 = new byte[fileStream.Length];
									fileStream.Read(array210, 0, array210.Length);
								}
							}
							catch
							{
							}
							if (0 < array210.Length)
							{
								string fileName = Path.GetFileName(path3);
								long num7 = (long)fileName.GetHashCode();
								maid.body0.CrossFade(num7.ToString(), array210, false, false, false, 0f, 1f);
								Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
								{
									default(Maid.AutoTwist),
									Maid.AutoTwist.ShoulderR,
									Maid.AutoTwist.WristL,
									Maid.AutoTwist.WristR,
									Maid.AutoTwist.ThighL,
									Maid.AutoTwist.ThighR
								};
								for (int k = 0; k < array3.Length; k++)
								{
									maid.SetAutoTwist(array3[k], true);
								}
							}
							break;
						}
					}
					isLock[selectMaidIndex] = false;
					inName3 = "";
					isSavePose = false;
				}
				if (isSavePose3)
				{
					Maid maid = maidArray[selectMaidIndex];
					isSavePose3 = false;
					isSavePose4 = true;
					Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
					transform2.eulerAngles = bipRotation;
					transform2.position = bipPosition;
				}
				if (isSavePose2)
				{
					Maid maid = maidArray[selectMaidIndex];
					isSavePose2 = false;
					isSavePose3 = true;
					Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
					transform2.eulerAngles = bipRotation;
					transform2.position = bipPosition;
				}
				if (isSavePose)
				{
					Maid maid = maidArray[selectMaidIndex];
					Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
					bipPosition = new Vector3(transform2.position.x - maid.transform.position.x, transform2.position.y, transform2.position.z - maid.transform.position.z);
					bipRotation = transform2.eulerAngles;
					maid.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					maid.transform.position = new Vector3(maid.transform.position.x, 0f, maid.transform.position.z);
					isSavePose = false;
					isSavePose2 = true;
					isStop[selectMaidIndex] = true;
				}
				if (saveScene > 0)
				{
					string text = "";
					string text5 = "";
					DateTime now = DateTime.Now;
					int year = now.Year;
					int month = now.Month;
					int day = now.Day;
					int hour = now.Hour;
					int minute = now.Minute;
					string text6 = string.Concat(new object[]
					{
						year,
						"/",
						month.ToString("00"),
						"/",
						day.ToString("00"),
						" ",
						hour.ToString("00"),
						":",
						minute.ToString("00")
					});
					if (saveScene < 9999)
					{
						date[saveScene - 1 - page * 10] = text6;
						ninzu[saveScene - 1 - page * 10] = maidCnt + "人";
					}
					text5 = text5 + text6 + ",";
					text5 = text5 + maidCnt + ",";
					text5 = text5 + bgArray[bgIndex].Replace("_", " ") + ",";
					string text7 = text5;
					string[] array4 = new string[7];
					array4[0] = text7;
					string[] array5 = array4;
					int num8 = 1;
					float num9 = bg.localEulerAngles.x;
					array5[num8] = num9.ToString("0.###");
					array4[2] = ",";
					string[] array6 = array4;
					int num10 = 3;
					num9 = bg.localEulerAngles.y;
					array6[num10] = num9.ToString("0.###");
					array4[4] = ",";
					string[] array7 = array4;
					int num11 = 5;
					num9 = bg.localEulerAngles.z;
					array7[num11] = num9.ToString("0.###");
					array4[6] = ",";
					text5 = string.Concat(array4);
					text7 = text5;
					array4 = new string[7];
					array4[0] = text7;
					string[] array8 = array4;
					int num12 = 1;
					num9 = bg.position.x;
					array8[num12] = num9.ToString("0.###");
					array4[2] = ",";
					string[] array9 = array4;
					int num13 = 3;
					num9 = bg.position.y;
					array9[num13] = num9.ToString("0.###");
					array4[4] = ",";
					string[] array10 = array4;
					int num14 = 5;
					num9 = bg.position.z;
					array10[num14] = num9.ToString("0.###");
					array4[6] = ",";
					text5 = string.Concat(array4);
					text7 = text5;
					array4 = new string[7];
					array4[0] = text7;
					string[] array11 = array4;
					int num15 = 1;
					num9 = bg.localScale.x;
					array11[num15] = num9.ToString("0.###");
					array4[2] = ",";
					string[] array12 = array4;
					int num16 = 3;
					num9 = bg.localScale.y;
					array12[num16] = num9.ToString("0.###");
					array4[4] = ",";
					string[] array13 = array4;
					int num17 = 5;
					num9 = bg.localScale.z;
					array13[num17] = num9.ToString("0.###");
					array4[6] = ",";
					text5 = string.Concat(array4);
					text7 = text5;
					text5 = string.Concat(new string[]
					{
						text7,
						softG.x.ToString("0.###"),
						",",
						softG.y.ToString("0.###"),
						",",
						softG.z.ToString("0.###"),
						","
					});
					text5 = text5 + bgmIndex + ",";
					text5 = text5 + effectIndex + ",";
					text5 = text5 + lightIndex[0] + ",";
					text5 = text5 + lightColorR[0] + ",";
					text5 = text5 + lightColorG[0] + ",";
					text5 = text5 + lightColorB[0] + ",";
					text7 = text5;
					array4 = new string[7];
					array4[0] = text7;
					string[] array14 = array4;
					int num18 = 1;
					num9 = GameMain.Instance.MainLight.transform.eulerAngles.x;
					array14[num18] = num9.ToString("0.###");
					array4[2] = ",";
					string[] array15 = array4;
					int num19 = 3;
					num9 = GameMain.Instance.MainLight.transform.eulerAngles.y;
					array15[num19] = num9.ToString("0.###");
					array4[4] = ",";
					string[] array16 = array4;
					int num20 = 5;
					num9 = GameMain.Instance.MainLight.transform.eulerAngles.z;
					array16[num20] = num9.ToString("0.###");
					array4[6] = ",";
					text5 = string.Concat(array4);
					text5 = text5 + GameMain.Instance.MainLight.GetComponent<Light>().intensity + ",";
					text5 = text5 + GameMain.Instance.MainLight.GetComponent<Light>().spotAngle + ",";
					text5 = text5 + GameMain.Instance.MainLight.GetComponent<Light>().range + ",";
					text5 = text5 + mainCamera.GetTargetPos().x + ",";
					text5 = text5 + mainCamera.GetTargetPos().y + ",";
					text5 = text5 + mainCamera.GetTargetPos().z + ",";
					text5 = text5 + mainCamera.GetDistance() + ",";
					text7 = text5;
					array4 = new string[7];
					array4[0] = text7;
					string[] array17 = array4;
					int num21 = 1;
					num9 = mainCamera.transform.eulerAngles.x;
					array17[num21] = num9.ToString("0.###");
					array4[2] = ",";
					string[] array18 = array4;
					int num22 = 3;
					num9 = mainCamera.transform.eulerAngles.y;
					array18[num22] = num9.ToString("0.###");
					array4[4] = ",";
					string[] array19 = array4;
					int num23 = 5;
					num9 = mainCamera.transform.eulerAngles.z;
					array19[num23] = num9.ToString("0.###");
					array4[6] = ",";
					text5 = string.Concat(array4);
					inName = inName.Replace("_", " ").Replace(",", " ");
					inText = inText.Replace("_", " ").Replace(",", " ");
					if (isMessage)
					{
						text7 = text5;
						text5 = string.Concat(new string[]
						{
							text7,
							"1,",
							inName,
							",",
							inText.Replace("\n", "&kaigyo")
						});
					}
					else
					{
						text5 += "0,,";
					}
					if (doguObject.Count > 0)
					{
						text5 = text5 + "," + doguArray[doguIndex[doguSelectIndex]].Replace("_", " ") + ",";
						text7 = text5;
						array4 = new string[7];
						array4[0] = text7;
						string[] array20 = array4;
						int num24 = 1;
						num9 = doguObject[doguSelectIndex].transform.localEulerAngles.x;
						array20[num24] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array21 = array4;
						int num25 = 3;
						num9 = doguObject[doguSelectIndex].transform.localEulerAngles.y;
						array21[num25] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array22 = array4;
						int num26 = 5;
						num9 = doguObject[doguSelectIndex].transform.localEulerAngles.z;
						array22[num26] = num9.ToString("0.###");
						array4[6] = ",";
						text5 = string.Concat(array4);
						text7 = text5;
						array4 = new string[7];
						array4[0] = text7;
						string[] array23 = array4;
						int num27 = 1;
						num9 = doguObject[doguSelectIndex].transform.position.x;
						array23[num27] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array24 = array4;
						int num28 = 3;
						num9 = doguObject[doguSelectIndex].transform.position.y;
						array24[num28] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array25 = array4;
						int num29 = 5;
						num9 = doguObject[doguSelectIndex].transform.position.z;
						array25[num29] = num9.ToString("0.###");
						array4[6] = ",";
						text5 = string.Concat(array4);
						text7 = text5;
						array4 = new string[6];
						array4[0] = text7;
						string[] array26 = array4;
						int num30 = 1;
						num9 = doguObject[doguSelectIndex].transform.localScale.x;
						array26[num30] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array27 = array4;
						int num31 = 3;
						num9 = doguObject[doguSelectIndex].transform.localScale.y;
						array27[num31] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array28 = array4;
						int num32 = 5;
						num9 = doguObject[doguSelectIndex].transform.localScale.z;
						array28[num32] = num9.ToString("0.###");
						text5 = string.Concat(array4);
					}
					text5 += "_";
					for (int l = 0; l < maidCnt; l++)
					{
						Maid maid = maidArray[l];
						string text8 = "";
						string text9 = "";
						SetIK(maid, l);
						for (int i2 = 0; i2 < 20; i2++)
						{
							text7 = text8;
							array4 = new string[7];
							array4[0] = text7;
							string[] array29 = array4;
							int num33 = 1;
							num9 = Finger[l, i2].localEulerAngles.x;
							array29[num33] = num9.ToString("0.###");
							array4[2] = ",";
							string[] array30 = array4;
							int num34 = 3;
							num9 = Finger[l, i2].localEulerAngles.y;
							array30[num34] = num9.ToString("0.###");
							array4[4] = ",";
							string[] array31 = array4;
							int num35 = 5;
							num9 = Finger[l, i2].localEulerAngles.z;
							array31[num35] = num9.ToString("0.###");
							array4[6] = ":";
							text8 = string.Concat(array4);
						}
						for (int i2 = 20; i2 < 40; i2++)
						{
							text7 = text9;
							array4 = new string[7];
							array4[0] = text7;
							string[] array32 = array4;
							int num36 = 1;
							num9 = Finger[l, i2].localEulerAngles.x;
							array32[num36] = num9.ToString("0.###");
							array4[2] = ",";
							string[] array33 = array4;
							int num37 = 3;
							num9 = Finger[l, i2].localEulerAngles.y;
							array33[num37] = num9.ToString("0.###");
							array4[4] = ",";
							string[] array34 = array4;
							int num38 = 5;
							num9 = Finger[l, i2].localEulerAngles.z;
							array34[num38] = num9.ToString("0.###");
							array4[6] = ":";
							text9 = string.Concat(array4);
						}
						string text10 = "";
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array35 = array4;
						int num39 = 1;
						num9 = Spine.eulerAngles.x;
						array35[num39] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array36 = array4;
						int num40 = 3;
						num9 = Spine.eulerAngles.y;
						array36[num40] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array37 = array4;
						int num41 = 5;
						num9 = Spine.eulerAngles.z;
						array37[num41] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array38 = array4;
						int num42 = 1;
						num9 = Spine0a.eulerAngles.x;
						array38[num42] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array39 = array4;
						int num43 = 3;
						num9 = Spine0a.eulerAngles.y;
						array39[num43] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array40 = array4;
						int num44 = 5;
						num9 = Spine0a.eulerAngles.z;
						array40[num44] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array41 = array4;
						int num45 = 1;
						num9 = Spine1.eulerAngles.x;
						array41[num45] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array42 = array4;
						int num46 = 3;
						num9 = Spine1.eulerAngles.y;
						array42[num46] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array43 = array4;
						int num47 = 5;
						num9 = Spine1.eulerAngles.z;
						array43[num47] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array44 = array4;
						int num48 = 1;
						num9 = Spine1a.eulerAngles.x;
						array44[num48] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array45 = array4;
						int num49 = 3;
						num9 = Spine1a.eulerAngles.y;
						array45[num49] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array46 = array4;
						int num50 = 5;
						num9 = Spine1a.eulerAngles.z;
						array46[num50] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array47 = array4;
						int num51 = 1;
						num9 = Pelvis.eulerAngles.x;
						array47[num51] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array48 = array4;
						int num52 = 3;
						num9 = Pelvis.eulerAngles.y;
						array48[num52] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array49 = array4;
						int num53 = 5;
						num9 = Pelvis.eulerAngles.z;
						array49[num53] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array50 = array4;
						int num54 = 1;
						num9 = HandL1[l].localEulerAngles.x;
						array50[num54] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array51 = array4;
						int num55 = 3;
						num9 = HandL1[l].localEulerAngles.y;
						array51[num55] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array52 = array4;
						int num56 = 5;
						num9 = HandL1[l].localEulerAngles.z;
						array52[num56] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array53 = array4;
						int num57 = 1;
						num9 = UpperArmL1[l].eulerAngles.x;
						array53[num57] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array54 = array4;
						int num58 = 3;
						num9 = UpperArmL1[l].eulerAngles.y;
						array54[num58] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array55 = array4;
						int num59 = 5;
						num9 = UpperArmL1[l].eulerAngles.z;
						array55[num59] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array56 = array4;
						int num60 = 1;
						num9 = ForearmL1[l].eulerAngles.x;
						array56[num60] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array57 = array4;
						int num61 = 3;
						num9 = ForearmL1[l].eulerAngles.y;
						array57[num61] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array58 = array4;
						int num62 = 5;
						num9 = ForearmL1[l].eulerAngles.z;
						array58[num62] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array59 = array4;
						int num63 = 1;
						num9 = HandR1[l].localEulerAngles.x;
						array59[num63] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array60 = array4;
						int num64 = 3;
						num9 = HandR1[l].localEulerAngles.y;
						array60[num64] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array61 = array4;
						int num65 = 5;
						num9 = HandR1[l].localEulerAngles.z;
						array61[num65] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array62 = array4;
						int num66 = 1;
						num9 = UpperArmR1[l].eulerAngles.x;
						array62[num66] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array63 = array4;
						int num67 = 3;
						num9 = UpperArmR1[l].eulerAngles.y;
						array63[num67] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array64 = array4;
						int num68 = 5;
						num9 = UpperArmR1[l].eulerAngles.z;
						array64[num68] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array65 = array4;
						int num69 = 1;
						num9 = ForearmR1[l].eulerAngles.x;
						array65[num69] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array66 = array4;
						int num70 = 3;
						num9 = ForearmR1[l].eulerAngles.y;
						array66[num70] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array67 = array4;
						int num71 = 5;
						num9 = ForearmR1[l].eulerAngles.z;
						array67[num71] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array68 = array4;
						int num72 = 1;
						num9 = HandL2[l].localEulerAngles.x;
						array68[num72] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array69 = array4;
						int num73 = 3;
						num9 = HandL2[l].localEulerAngles.y;
						array69[num73] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array70 = array4;
						int num74 = 5;
						num9 = HandL2[l].localEulerAngles.z;
						array70[num74] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array71 = array4;
						int num75 = 1;
						num9 = UpperArmL2[l].eulerAngles.x;
						array71[num75] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array72 = array4;
						int num76 = 3;
						num9 = UpperArmL2[l].eulerAngles.y;
						array72[num76] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array73 = array4;
						int num77 = 5;
						num9 = UpperArmL2[l].eulerAngles.z;
						array73[num77] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array74 = array4;
						int num78 = 1;
						num9 = ForearmL2[l].eulerAngles.x;
						array74[num78] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array75 = array4;
						int num79 = 3;
						num9 = ForearmL2[l].eulerAngles.y;
						array75[num79] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array76 = array4;
						int num80 = 5;
						num9 = ForearmL2[l].eulerAngles.z;
						array76[num80] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array77 = array4;
						int num81 = 1;
						num9 = HandR2[l].localEulerAngles.x;
						array77[num81] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array78 = array4;
						int num82 = 3;
						num9 = HandR2[l].localEulerAngles.y;
						array78[num82] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array79 = array4;
						int num83 = 5;
						num9 = HandR2[l].localEulerAngles.z;
						array79[num83] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array80 = array4;
						int num84 = 1;
						num9 = UpperArmR2[l].eulerAngles.x;
						array80[num84] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array81 = array4;
						int num85 = 3;
						num9 = UpperArmR2[l].eulerAngles.y;
						array81[num85] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array82 = array4;
						int num86 = 5;
						num9 = UpperArmR2[l].eulerAngles.z;
						array82[num86] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array83 = array4;
						int num87 = 1;
						num9 = ForearmR2[l].eulerAngles.x;
						array83[num87] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array84 = array4;
						int num88 = 3;
						num9 = ForearmR2[l].eulerAngles.y;
						array84[num88] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array85 = array4;
						int num89 = 5;
						num9 = ForearmR2[l].eulerAngles.z;
						array85[num89] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array86 = array4;
						int num90 = 1;
						num9 = Head.eulerAngles.x;
						array86[num90] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array87 = array4;
						int num91 = 3;
						num9 = Head.eulerAngles.y;
						array87[num91] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array88 = array4;
						int num92 = 5;
						num9 = Head.eulerAngles.z;
						array88[num92] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array89 = array4;
						int num93 = 1;
						num9 = maid.transform.localEulerAngles.x;
						array89[num93] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array90 = array4;
						int num94 = 3;
						num9 = maid.transform.localEulerAngles.y;
						array90[num94] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array91 = array4;
						int num95 = 5;
						num9 = maid.transform.localEulerAngles.z;
						array91[num95] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array92 = array4;
						int num96 = 1;
						num9 = maid.transform.position.x;
						array92[num96] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array93 = array4;
						int num97 = 3;
						num9 = maid.transform.position.y;
						array93[num97] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array94 = array4;
						int num98 = 5;
						num9 = maid.transform.position.z;
						array94[num98] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						text7 = text10;
						array4 = new string[7];
						array4[0] = text7;
						string[] array95 = array4;
						int num99 = 1;
						num9 = maid.transform.localScale.x;
						array95[num99] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array96 = array4;
						int num100 = 3;
						num9 = maid.transform.localScale.y;
						array96[num100] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array97 = array4;
						int num101 = 5;
						num9 = maid.transform.localScale.z;
						array97[num101] = num9.ToString("0.###");
						array4[6] = ":";
						text10 = string.Concat(array4);
						if (poseArray[poseIndex[l]].Contains("AdvancedStudioModePose"))
						{
							string text4 = poseArray[poseIndex[l]].Replace("\u3000", "").Split(new char[]
							{
								'/'
							})[0];
							text10 = text10 + "AdvancedStudioModePose" + text4.Replace("_", " ").Replace(",", "|") + ":";
						}
						else
						{
							text10 = text10 + poseArray[poseIndex[l]].Replace("_", " ").Replace(",", "|") + ":";
						}
						text10 = text10 + faceIndex[l] + ":";
						TMorph morph = maid.body0.Face.morph;
						float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
						float[] fieldValue5 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
						if (morph.bodyskin.PartsVersion < 120)
						{
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose"]] + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose2"]] + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose3"]] + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose6"]] + ",";
						}
						else
						{
							int num102 = 0;
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
							{
								num102 = 1;
							}
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
							{
								num102 = 2;
							}
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num102]]] + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num102]]] + ",";
							text10 = text10 + fieldValue[(int)morph.hash["eyeclose3"]] / 3f + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num102]]] + ",";
						}
						text10 = text10 + fieldValue[(int)morph.hash["hitomih"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["hitomis"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mayuha"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mayuup"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mayuv"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mayuvhalf"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["moutha"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouths"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthdw"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthup"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["tangout"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["tangup"]] + ",";
						if (morph.bodyskin.PartsVersion < 120)
						{
							text10 = text10 + fieldValue[(int)morph.hash["eyebig"]] + ",";
						}
						else
						{
							text10 = text10 + fieldValue[(int)morph.hash["eyebig"]] / 3f + ",";
						}
						if (morph.bodyskin.PartsVersion < 120)
						{
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose5"]] + ",";
						}
						else
						{
							int num102 = 0;
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
							{
								num102 = 1;
							}
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
							{
								num102 = 2;
							}
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num102]]] + ",";
						}
						text10 = text10 + fieldValue[(int)morph.hash["mayuw"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthhe"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthc"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthi"]] + ",";
						text10 = text10 + fieldValue[(int)morph.hash["mouthuphalf"]] + ",";
						try
						{
							text10 = text10 + fieldValue[(int)morph.hash["tangopen"]] + ",";
						}
						catch
						{
							text10 += "0,";
						}
						if (fieldValue[(int)morph.hash["namida"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["tear1"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["tear2"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["tear3"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["shock"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["yodare"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["hoho"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["hoho2"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["hohos"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["hohol"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["toothoff"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (fieldValue[(int)morph.hash["nosefook"]] > 0f)
						{
							text10 = text10 + 1 + ",";
						}
						else
						{
							text10 = text10 + 0 + ",";
						}
						if (morph.bodyskin.PartsVersion < 120)
						{
							if (morph.hash["eyeclose7"] != null)
							{
								text10 = text10 + fieldValue5[(int)morph.hash["eyeclose7"]] + ",";
								text10 = text10 + fieldValue5[(int)morph.hash["eyeclose8"]] + ":";
							}
							else
							{
								text10 += "0,0:";
							}
						}
						else
						{
							int num102 = 0;
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
							{
								num102 = 1;
							}
							if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
							{
								num102 = 2;
							}
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]] + ",";
							text10 = text10 + fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]] + ":";
						}
						string text11 = "";
						if (isLook[l])
						{
							text11 = text11 + 1 + ",";
							text11 = text11 + lookX[l].ToString("0.###") + ",";
							text11 = text11 + lookY[l].ToString("0.###") + ":";
						}
						else
						{
							text11 = text11 + 0 + ",0,0:";
						}
						text11 = text11 + itemIndex[l] + ":";
						Transform transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
						Transform transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array98 = array4;
						int num103 = 1;
						num9 = transform3.localEulerAngles.x;
						array98[num103] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array99 = array4;
						int num104 = 3;
						num9 = transform3.localEulerAngles.y;
						array99[num104] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array100 = array4;
						int num105 = 5;
						num9 = transform3.localEulerAngles.z;
						array100[num105] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array101 = array4;
						int num106 = 1;
						num9 = transform4.localEulerAngles.x;
						array101[num106] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array102 = array4;
						int num107 = 3;
						num9 = transform4.localEulerAngles.y;
						array102[num107] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array103 = array4;
						int num108 = 5;
						num9 = transform4.localEulerAngles.z;
						array103[num108] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array104 = array4;
						int num109 = 1;
						num9 = ClavicleL1[l].eulerAngles.x;
						array104[num109] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array105 = array4;
						int num110 = 3;
						num9 = ClavicleL1[l].eulerAngles.y;
						array105[num110] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array106 = array4;
						int num111 = 5;
						num9 = ClavicleL1[l].eulerAngles.z;
						array106[num111] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array107 = array4;
						int num112 = 1;
						num9 = ClavicleR1[l].eulerAngles.x;
						array107[num112] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array108 = array4;
						int num113 = 3;
						num9 = ClavicleR1[l].eulerAngles.y;
						array108[num113] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array109 = array4;
						int num114 = 5;
						num9 = ClavicleR1[l].eulerAngles.z;
						array109[num114] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						if (hanten[l])
						{
							text11 += "1:";
						}
						else
						{
							text11 += "0:";
						}
						Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
						Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
						Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
						Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
						Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
						Transform transform9 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
						Transform transform10 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
						Transform transform11 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
						Transform transform12 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
						Transform transform13 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
						Transform transform14 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
						Transform transform15 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
						Transform transform16 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
						Transform transform17 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
						Transform transform18 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
						Transform transform19 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
						Transform transform20 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
						Transform transform21 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
						Transform transform22 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array110 = array4;
						int num115 = 1;
						num9 = transform2.eulerAngles.x;
						array110[num115] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array111 = array4;
						int num116 = 3;
						num9 = transform2.eulerAngles.y;
						array111[num116] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array112 = array4;
						int num117 = 5;
						num9 = transform2.eulerAngles.z;
						array112[num117] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array113 = array4;
						int num118 = 1;
						num9 = transform5.localEulerAngles.x;
						array113[num118] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array114 = array4;
						int num119 = 3;
						num9 = transform5.localEulerAngles.y;
						array114[num119] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array115 = array4;
						int num120 = 5;
						num9 = transform5.localEulerAngles.z;
						array115[num120] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array116 = array4;
						int num121 = 1;
						num9 = transform6.localEulerAngles.x;
						array116[num121] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array117 = array4;
						int num122 = 3;
						num9 = transform6.localEulerAngles.y;
						array117[num122] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array118 = array4;
						int num123 = 5;
						num9 = transform6.localEulerAngles.z;
						array118[num123] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array119 = array4;
						int num124 = 1;
						num9 = transform7.localEulerAngles.x;
						array119[num124] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array120 = array4;
						int num125 = 3;
						num9 = transform7.localEulerAngles.y;
						array120[num125] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array121 = array4;
						int num126 = 5;
						num9 = transform7.localEulerAngles.z;
						array121[num126] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array122 = array4;
						int num127 = 1;
						num9 = transform8.localEulerAngles.x;
						array122[num127] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array123 = array4;
						int num128 = 3;
						num9 = transform8.localEulerAngles.y;
						array123[num128] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array124 = array4;
						int num129 = 5;
						num9 = transform8.localEulerAngles.z;
						array124[num129] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array125 = array4;
						int num130 = 1;
						num9 = transform9.localEulerAngles.x;
						array125[num130] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array126 = array4;
						int num131 = 3;
						num9 = transform9.localEulerAngles.y;
						array126[num131] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array127 = array4;
						int num132 = 5;
						num9 = transform9.localEulerAngles.z;
						array127[num132] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array128 = array4;
						int num133 = 1;
						num9 = transform10.localEulerAngles.x;
						array128[num133] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array129 = array4;
						int num134 = 3;
						num9 = transform10.localEulerAngles.y;
						array129[num134] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array130 = array4;
						int num135 = 5;
						num9 = transform10.localEulerAngles.z;
						array130[num135] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array131 = array4;
						int num136 = 1;
						num9 = transform11.localEulerAngles.x;
						array131[num136] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array132 = array4;
						int num137 = 3;
						num9 = transform11.localEulerAngles.y;
						array132[num137] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array133 = array4;
						int num138 = 5;
						num9 = transform11.localEulerAngles.z;
						array133[num138] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array134 = array4;
						int num139 = 1;
						num9 = transform12.localEulerAngles.x;
						array134[num139] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array135 = array4;
						int num140 = 3;
						num9 = transform12.localEulerAngles.y;
						array135[num140] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array136 = array4;
						int num141 = 5;
						num9 = transform12.localEulerAngles.z;
						array136[num141] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array137 = array4;
						int num142 = 1;
						num9 = transform13.localEulerAngles.x;
						array137[num142] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array138 = array4;
						int num143 = 3;
						num9 = transform13.localEulerAngles.y;
						array138[num143] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array139 = array4;
						int num144 = 5;
						num9 = transform13.localEulerAngles.z;
						array139[num144] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array140 = array4;
						int num145 = 1;
						num9 = transform14.localEulerAngles.x;
						array140[num145] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array141 = array4;
						int num146 = 3;
						num9 = transform14.localEulerAngles.y;
						array141[num146] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array142 = array4;
						int num147 = 5;
						num9 = transform14.localEulerAngles.z;
						array142[num147] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array143 = array4;
						int num148 = 1;
						num9 = transform15.localEulerAngles.x;
						array143[num148] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array144 = array4;
						int num149 = 3;
						num9 = transform15.localEulerAngles.y;
						array144[num149] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array145 = array4;
						int num150 = 5;
						num9 = transform15.localEulerAngles.z;
						array145[num150] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array146 = array4;
						int num151 = 1;
						num9 = transform16.localEulerAngles.x;
						array146[num151] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array147 = array4;
						int num152 = 3;
						num9 = transform16.localEulerAngles.y;
						array147[num152] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array148 = array4;
						int num153 = 5;
						num9 = transform16.localEulerAngles.z;
						array148[num153] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array149 = array4;
						int num154 = 1;
						num9 = transform17.localEulerAngles.x;
						array149[num154] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array150 = array4;
						int num155 = 3;
						num9 = transform17.localEulerAngles.y;
						array150[num155] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array151 = array4;
						int num156 = 5;
						num9 = transform17.localEulerAngles.z;
						array151[num156] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array152 = array4;
						int num157 = 1;
						num9 = transform18.localEulerAngles.x;
						array152[num157] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array153 = array4;
						int num158 = 3;
						num9 = transform18.localEulerAngles.y;
						array153[num158] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array154 = array4;
						int num159 = 5;
						num9 = transform18.localEulerAngles.z;
						array154[num159] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array155 = array4;
						int num160 = 1;
						num9 = transform19.localEulerAngles.x;
						array155[num160] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array156 = array4;
						int num161 = 3;
						num9 = transform19.localEulerAngles.y;
						array156[num161] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array157 = array4;
						int num162 = 5;
						num9 = transform19.localEulerAngles.z;
						array157[num162] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array158 = array4;
						int num163 = 1;
						num9 = transform20.localEulerAngles.x;
						array158[num163] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array159 = array4;
						int num164 = 3;
						num9 = transform20.localEulerAngles.y;
						array159[num164] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array160 = array4;
						int num165 = 5;
						num9 = transform20.localEulerAngles.z;
						array160[num165] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array161 = array4;
						int num166 = 1;
						num9 = transform21.localEulerAngles.x;
						array161[num166] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array162 = array4;
						int num167 = 3;
						num9 = transform21.localEulerAngles.y;
						array162[num167] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array163 = array4;
						int num168 = 5;
						num9 = transform21.localEulerAngles.z;
						array163[num168] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array164 = array4;
						int num169 = 1;
						num9 = transform22.localEulerAngles.x;
						array164[num169] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array165 = array4;
						int num170 = 3;
						num9 = transform22.localEulerAngles.y;
						array165[num170] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array166 = array4;
						int num171 = 5;
						num9 = transform22.localEulerAngles.z;
						array166[num171] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array167 = array4;
						int num172 = 1;
						num9 = maid.body0.quaDefEyeL.eulerAngles.x;
						array167[num172] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array168 = array4;
						int num173 = 3;
						num9 = maid.body0.quaDefEyeL.eulerAngles.y;
						array168[num173] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array169 = array4;
						int num174 = 5;
						num9 = maid.body0.quaDefEyeL.eulerAngles.z;
						array169[num174] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array170 = array4;
						int num175 = 1;
						num9 = maid.body0.quaDefEyeR.eulerAngles.x;
						array170[num175] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array171 = array4;
						int num176 = 3;
						num9 = maid.body0.quaDefEyeR.eulerAngles.y;
						array171[num176] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array172 = array4;
						int num177 = 5;
						num9 = maid.body0.quaDefEyeR.eulerAngles.z;
						array172[num177] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						Transform transform23 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_L", true);
						Transform transform24 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_L_sub", true);
						Transform transform25 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_R", true);
						Transform transform26 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_R_sub", true);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array173 = array4;
						int num178 = 1;
						num9 = transform23.localEulerAngles.x;
						array173[num178] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array174 = array4;
						int num179 = 3;
						num9 = transform23.localEulerAngles.y;
						array174[num179] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array175 = array4;
						int num180 = 5;
						num9 = transform23.localEulerAngles.z;
						array175[num180] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array176 = array4;
						int num181 = 1;
						num9 = transform24.localEulerAngles.x;
						array176[num181] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array177 = array4;
						int num182 = 3;
						num9 = transform24.localEulerAngles.y;
						array177[num182] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array178 = array4;
						int num183 = 5;
						num9 = transform24.localEulerAngles.z;
						array178[num183] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array179 = array4;
						int num184 = 1;
						num9 = transform25.localEulerAngles.x;
						array179[num184] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array180 = array4;
						int num185 = 3;
						num9 = transform25.localEulerAngles.y;
						array180[num185] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array181 = array4;
						int num186 = 5;
						num9 = transform25.localEulerAngles.z;
						array181[num186] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[7];
						array4[0] = text7;
						string[] array182 = array4;
						int num187 = 1;
						num9 = transform26.localEulerAngles.x;
						array182[num187] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array183 = array4;
						int num188 = 3;
						num9 = transform26.localEulerAngles.y;
						array183[num188] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array184 = array4;
						int num189 = 5;
						num9 = transform26.localEulerAngles.z;
						array184[num189] = num9.ToString("0.###");
						array4[6] = ":";
						text11 = string.Concat(array4);
						text7 = text11;
						array4 = new string[6];
						array4[0] = text7;
						string[] array185 = array4;
						int num190 = 1;
						num9 = transform2.position.x;
						array185[num190] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array186 = array4;
						int num191 = 3;
						num9 = transform2.position.y;
						array186[num191] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array187 = array4;
						int num192 = 5;
						num9 = transform2.position.z;
						array187[num192] = num9.ToString("0.###");
						text11 = string.Concat(array4);
						text7 = text;
						text = string.Concat(new string[]
						{
							text7,
							text8,
							text9,
							text10,
							text11
						});
						if (l + 1 != maidCnt)
						{
							text += ";";
						}
					}
					string text12 = "_";
					text12 = text12 + lightKage[0] + ",";
					if (isBloom)
					{
						text12 += "1,";
						text12 = text12 + bloom1 + ",";
						text12 = text12 + bloom2 + ",";
						text12 = text12 + bloom3 + ",";
						text12 = text12 + bloom4 + ",";
						text12 = text12 + bloom5 + ",";
						if (isBloomA)
						{
							text12 += "1,";
						}
						else
						{
							text12 += "0,";
						}
					}
					else
					{
						text12 += "0,0,0,0,0,0,0,";
					}
					if (isBlur)
					{
						text12 += "1,";
						text12 = text12 + blur1 + ",";
						text12 = text12 + blur2 + ",";
						text12 = text12 + blur3 + ",";
						text12 = text12 + blur4 + ",";
					}
					else
					{
						text12 += "0,0,0,0,0,";
					}
					text12 = text12 + bokashi + ",";
					text12 = text12 + kamiyure + ",";
					if (isDepth)
					{
						text12 += "1,";
						text12 = text12 + depth1 + ",";
						text12 = text12 + depth2 + ",";
						text12 = text12 + depth3 + ",";
						text12 = text12 + depth4 + ",";
						if (isDepthA)
						{
							text12 += "1,";
						}
						else
						{
							text12 += "0,";
						}
					}
					else
					{
						text12 += "0,0,0,0,0,0,";
					}
					if (isFog)
					{
						text12 += "1,";
						text12 = text12 + fog1 + ",";
						text12 = text12 + fog2 + ",";
						text12 = text12 + fog3 + ",";
						text12 = text12 + fog4 + ",";
						text12 = text12 + fog5 + ",";
						text12 = text12 + fog6 + ",";
						text12 = text12 + fog7 + ",";
					}
					else
					{
						text12 += "0,0,0,0,0,0,0,0,";
					}
					if (isSepia)
					{
						text12 += "1";
					}
					else
					{
						text12 += "0";
					}
					string text13 = "_";
					for (int l = 1; l < lightList.Count; l++)
					{
						text13 = text13 + lightIndex[l] + ",";
						text13 = text13 + lightColorR[l] + ",";
						text13 = text13 + lightColorG[l] + ",";
						text13 = text13 + lightColorB[l] + ",";
						text13 = text13 + lightX[l] + ",";
						text13 = text13 + lightY[l] + ",";
						text13 = text13 + lightAkarusa[l] + ",";
						text13 = text13 + lightRange[l] + ";";
					}
					string text14 = "_";
					for (int l = 0; l < doguBObject.Count; l++)
					{
						text14 = text14 + doguBObject[l].name.Replace("_", " ") + ",";
						text7 = text14;
						array4 = new string[7];
						array4[0] = text7;
						string[] array188 = array4;
						int num193 = 1;
						num9 = doguBObject[l].transform.localEulerAngles.x;
						array188[num193] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array189 = array4;
						int num194 = 3;
						num9 = doguBObject[l].transform.localEulerAngles.y;
						array189[num194] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array190 = array4;
						int num195 = 5;
						num9 = doguBObject[l].transform.localEulerAngles.z;
						array190[num195] = num9.ToString("0.###");
						array4[6] = ",";
						text14 = string.Concat(array4);
						text7 = text14;
						array4 = new string[7];
						array4[0] = text7;
						string[] array191 = array4;
						int num196 = 1;
						num9 = doguBObject[l].transform.position.x;
						array191[num196] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array192 = array4;
						int num197 = 3;
						num9 = doguBObject[l].transform.position.y;
						array192[num197] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array193 = array4;
						int num198 = 5;
						num9 = doguBObject[l].transform.position.z;
						array193[num198] = num9.ToString("0.###");
						array4[6] = ",";
						text14 = string.Concat(array4);
						text7 = text14;
						array4 = new string[6];
						array4[0] = text7;
						string[] array194 = array4;
						int num199 = 1;
						num9 = doguBObject[l].transform.localScale.x;
						array194[num199] = num9.ToString("0.###");
						array4[2] = ",";
						string[] array195 = array4;
						int num200 = 3;
						num9 = doguBObject[l].transform.localScale.y;
						array195[num200] = num9.ToString("0.###");
						array4[4] = ",";
						string[] array196 = array4;
						int num201 = 5;
						num9 = doguBObject[l].transform.localScale.z;
						array196[num201] = num9.ToString("0.###");
						text14 = string.Concat(array4);
						text14 += ";";
					}
					string text15 = "_";
					for (int l = 0; l < lightList.Count; l++)
					{
						text15 = text15 + lightList[l].transform.position.x + ",";
						text15 = text15 + lightList[l].transform.position.y + ",";
						text15 = text15 + lightList[l].transform.position.z + ";";
					}
					if (saveScene >= 10000)
					{
						base.Preferences["scene"]["s" + saveScene].Value = string.Concat(new string[]
						{
							text5,
							text,
							text12,
							text13,
							text14,
							text15
						});
						base.SaveConfig();
					}
					else
					{
						saveData = string.Concat(new string[]
						{
							text5,
							text,
							text12,
							text13,
							text14,
							text15
						});
					}
					saveScene = 0;
				}
				for (int i2 = 0; i2 < maidCnt; i2++)
				{
					if (haraCount[i2] > 0)
					{
						haraCount[i2]--;
						Transform transform2 = CMT.SearchObjName(maidArray[i2].body0.m_Bones.transform, "Bip01", true);
						transform2.position = haraPosition[i2];
					}
					if (isLoadFace[i2])
					{
						isLoadFace[i2] = false;
						TMorph morph = maidArray[i2].body0.Face.morph;
						float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
						fieldValue[(int)morph.hash["mouthuphalf"]] = fieldValue[(int)morph.hash["mouthuphalf"]] - 0.01f;
						maidArray[i2].body0.Face.morph.FixBlendValues_Face();
					}
				}
				if (isScene)
				{
					isScene = false;
					string path4 = string.Concat(new object[]
					{
						Path.GetFullPath(".\\"),
						"Mod\\AdvancedStudioModeScene\\",
						loadScene,
						".png"
					});
					string text16;
					if (File.Exists(path4))
					{
						FileStream fileStream = new FileStream(path4, FileMode.Open, FileAccess.Read);
						BinaryReader binaryReader = new BinaryReader(fileStream);
						byte[] array197 = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
						byte[] value2 = new byte[]
						{
							array197[36],
							array197[35],
							array197[34],
							array197[33]
						};
						int count = BitConverter.ToInt32(value2, 0) - 8;
						byte[] bytes = array197.Skip(49).Take(count).ToArray<byte>();
						text16 = Encoding.UTF8.GetString(bytes);
						text16 = AdvancedStudioMode.StringFromBase64Comp(text16);
					}
					else
					{
						text16 = base.Preferences["scene"]["s" + loadScene].Value;
					}
					string[] array198 = text16.Split(new char[]
					{
						'_'
					});
					string[] array199 = array198[1].Split(new char[]
					{
						';'
					});
					string[] array200 = array198[0].Split(new char[]
					{
						','
					});
					string[] array201 = null;
					string[] array202 = null;
					string[] array203 = null;
					string[] array204 = null;
					if (array198.Length >= 5)
					{
						array201 = array198[2].Split(new char[]
						{
							','
						});
						array202 = array198[3].Split(new char[]
						{
							';'
						});
						array203 = array198[4].Split(new char[]
						{
							';'
						});
					}
					if (array198.Length >= 6)
					{
						array204 = array198[5].Split(new char[]
						{
							';'
						});
					}
					if (!int.TryParse(array200[2], out bgIndex))
					{
						string text2 = array200[2].Replace(" ", "_");
						for (int i2 = 0; i2 < bgArray.Length; i2++)
						{
							if (text2 == bgArray[i2])
							{
								bgIndex = i2;
								break;
							}
						}
					}
					if (bgArray[bgIndex].Length == 36)
					{
						GameMain.Instance.BgMgr.ChangeBgMyRoom(bgArray[bgIndex]);
					}
					else
					{
						GameMain.Instance.BgMgr.ChangeBg(bgArray[bgIndex]);
					}
					bgCombo.selectedItemIndex = bgIndex;
					bg.localEulerAngles = new Vector3(float.Parse(array200[3]), float.Parse(array200[4]), float.Parse(array200[5]));
					bg.position = new Vector3(float.Parse(array200[6]), float.Parse(array200[7]), float.Parse(array200[8]));
					bg.localScale = new Vector3(float.Parse(array200[9]), float.Parse(array200[10]), float.Parse(array200[11]));
					softG = new Vector3(float.Parse(array200[12]), float.Parse(array200[13]), float.Parse(array200[14]));
					for (int i2 = 0; i2 < maidCnt; i2++)
					{
						Maid maid = maidArray[i2];
						for (int j = 0; j < maid.body0.goSlot.Count; j++)
						{
							if (maid.body0.goSlot[j].obj != null)
							{
								DynamicBone component = maid.body0.goSlot[j].obj.GetComponent<DynamicBone>();
								if (component != null && component.enabled)
								{
									component.m_Gravity = new Vector3(softG.x * 5f, (softG.y + 0.003f) * 5f, softG.z * 5f);
								}
							}
							List<THair1> fieldValue6 = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid.body0.goSlot[j].bonehair, "hair1list");
							for (int k = 0; k < fieldValue6.Count; k++)
							{
								fieldValue6[k].SoftG = new Vector3(softG.x, softG.y + kamiyure, softG.z);
							}
						}
					}
					if (!kankyoLoadFlg)
					{
						int num202 = bgmIndex;
						bgmIndex = int.Parse(array200[15]);
						if (num202 != bgmIndex)
						{
							GameMain.Instance.SoundMgr.PlayBGM(bgmArray[bgmIndex] + ".ogg", 0f, true);
							bgmCombo.selectedItemIndex = bgmIndex;
						}
					}
					if (doguObject.Count > 0 && doguObject[doguSelectIndex] != null)
					{
						UnityEngine.Object.Destroy(doguObject[doguSelectIndex]);
						doguObject.RemoveAt(doguSelectIndex);
						doguIndex.RemoveAt(doguSelectIndex);
					}
					doguSelectIndex = 0;
					if (array200.Length > 16)
					{
						effectIndex = int.Parse(array200[16]);
						if (!isVR)
						{
							Vignetting component2 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
							component2.mode = 0;
							component2.intensity = -3.98f;
							component2.chromaticAberration = -0.75f;
							component2.axialAberration = 1.18f;
							component2.blurSpread = 4.19f;
							component2.luminanceDependency = 0.494f;
							component2.blurDistance = 1.71f;
							component2.enabled = false;
							if (kami)
							{
								kami.SetActive(false);
							}
							if (effectIndex == 0)
							{
								component2.enabled = false;
							}
							if (effectIndex == 1)
							{
								component2.mode = 0;
								component2.intensity = -1f;
								component2.chromaticAberration = 0f;
								component2.axialAberration = 0f;
								component2.blurSpread = 5f;
								component2.luminanceDependency = 0f;
								component2.blurDistance = 0f;
								component2.enabled = true;
							}
							if (effectIndex == 2)
							{
								component2.enabled = true;
							}
							if (effectIndex == 3)
							{
								component2.mode = 0;
								component2.intensity = 9f;
								component2.chromaticAberration = 0f;
								component2.axialAberration = 0f;
								component2.blurSpread = 5f;
								component2.luminanceDependency = 0f;
								component2.blurDistance = 0f;
								component2.enabled = true;
							}
							if (effectIndex == 4)
							{
								UnityEngine.Object @object = Resources.Load("Prefab/p_kamihubuki_photo_ver");
								kami = (GameObject)UnityEngine.Object.Instantiate(@object);
								Vector3 localPosition = kami.transform.localPosition;
								localPosition.y = 3.5f;
								kami.transform.localPosition = localPosition;
							}
							if (effectIndex == 5)
							{
								UnityEngine.Object @object = Resources.Load("Prefab/p_kamihubuki_photo_ver");
								kami = (GameObject)UnityEngine.Object.Instantiate(@object);
								Vector3 localPosition = kami.transform.localPosition;
								localPosition.y = 3.5f;
								kami.transform.localPosition = localPosition;
								component2.enabled = true;
								component2.mode = 0;
								component2.intensity = -2.5f;
								component2.chromaticAberration = -0.3f;
								component2.axialAberration = 0.5f;
								component2.blurSpread = 4.5f;
								component2.luminanceDependency = 0.2f;
								component2.blurDistance = 0.8f;
								component2.enabled = true;
							}
							if (effectIndex == 6)
							{
								kami.SetActive(false);
								UnityEngine.Object @object = Resources.Load("Prefab/p_powder_snow_photo_ver");
								kami = (GameObject)UnityEngine.Object.Instantiate(@object);
								Vector3 localPosition = kami.transform.localPosition;
								localPosition.y = 3.5f;
								kami.transform.localPosition = localPosition;
							}
							if (effectIndex == 7)
							{
								kami.SetActive(false);
								UnityEngine.Object @object = Resources.Load("Prefab/p_powder_snow_photo_ver");
								kami = (GameObject)UnityEngine.Object.Instantiate(@object);
								Vector3 localPosition = kami.transform.localPosition;
								localPosition.y = 3.5f;
								kami.transform.localPosition = localPosition;
								component2.enabled = true;
								component2.mode = 0;
								component2.intensity = -2.5f;
								component2.chromaticAberration = -0.3f;
								component2.axialAberration = 0.5f;
								component2.blurSpread = 4.5f;
								component2.luminanceDependency = 0.2f;
								component2.blurDistance = 0.8f;
								component2.enabled = true;
							}
							if (effectIndex == 8)
							{
								kami.SetActive(false);
								UnityEngine.Object @object = Resources.Load("Prefab/p_powder_snow_photo_ver");
								kami = (GameObject)UnityEngine.Object.Instantiate(@object);
								Vector3 localPosition = kami.transform.localPosition;
								localPosition.y = 3.5f;
								kami.transform.localPosition = localPosition;
								component2.mode = 0;
								component2.intensity = 8f;
								component2.chromaticAberration = 0f;
								component2.axialAberration = 0f;
								component2.blurSpread = 5f;
								component2.luminanceDependency = 0f;
								component2.blurDistance = 0f;
								component2.enabled = true;
							}
						}
						for (int l = 1; l < lightList.Count; l++)
						{
							UnityEngine.Object.Destroy(lightList[l]);
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
						for (int l = 0; l < lightList.Count; l++)
						{
							if (l == 0)
							{
								lightComboList[l] = new GUIContent("メイン");
							}
							else
							{
								lightComboList[l] = new GUIContent("追加" + l);
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
						lightIndex[0] = int.Parse(array200[17]);
						lightColorR[0] = float.Parse(array200[18]);
						lightColorG[0] = float.Parse(array200[19]);
						lightColorB[0] = float.Parse(array200[20]);
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
						else if (lightIndex[0] == 3)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
							bgObject.SetActive(false);
						}
						if (lightIndex[0] != 3)
						{
							GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
							bgObject.SetActive(true);
							mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
						}
						else
						{
							mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
						}
						GameMain.Instance.MainLight.transform.eulerAngles = new Vector3(float.Parse(array200[21]), float.Parse(array200[22]), float.Parse(array200[23]));
						GameMain.Instance.MainLight.GetComponent<Light>().intensity = float.Parse(array200[24]);
						GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = float.Parse(array200[25]);
						GameMain.Instance.MainLight.GetComponent<Light>().range = float.Parse(array200[26]);
						lightX = new List<float>();
						lightX.Add(float.Parse(array200[21]));
						lightY = new List<float>();
						lightY.Add(float.Parse(array200[22]));
						lightAkarusa = new List<float>();
						lightAkarusa.Add(float.Parse(array200[24]));
						lightKage = new List<float>();
						lightKage.Add(0.098f);
						lightRange = new List<float>();
						lightRange.Add(float.Parse(array200[25]));
						selectLightIndex = 0;
						isIdx1 = false;
						isIdx2 = false;
						isIdx3 = false;
						isIdx4 = false;
						if (!kankyoLoadFlg)
						{
							if (!isVR)
							{
								mainCamera.SetTargetPos(new Vector3(float.Parse(array200[27]), float.Parse(array200[28]), float.Parse(array200[29])), true);
								mainCamera.SetDistance(float.Parse(array200[30]), true);
								mainCamera.transform.eulerAngles = new Vector3(float.Parse(array200[31]), float.Parse(array200[32]), float.Parse(array200[33]));
								if (int.Parse(array200[34]) == 1)
								{
									inName = array200[35];
									inText = array200[36];
									inText = inText.Replace("&kaigyo", "\n");
									isMessage = true;
									bGuiMessage = false;
									GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
									GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
									MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
									messageWindowMgr.OpenMessageWindowPanel();
									MessageClass messageClass = new MessageClass(gameObject2, messageWindowMgr);
									messageClass.SetText(inName, inText, "", 0);
									messageClass.FinishChAnime();
								}
								else if (isMessage)
								{
									isMessage = false;
									GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
									GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
									MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
									messageWindowMgr.CloseMessageWindowPanel();
								}
							}
						}
					}
					if (array200.Length > 37)
					{
						if (array200[37] != "")
						{
							if (doguSelectIndex == doguObject.Count)
							{
								doguIndex.Add(0);
							}
							else if (doguObject.Count > 0 && doguObject[doguSelectIndex] != null)
							{
								UnityEngine.Object.Destroy(doguObject[doguSelectIndex]);
								doguObject.RemoveAt(doguSelectIndex);
							}
							else
							{
								doguIndex.Add(0);
								doguSelectIndex = doguIndex.Count - 1;
							}
							string text17 = array200[37].Replace(" ", "_");
							for (int i2 = 0; i2 < doguArray.Length; i2++)
							{
								if (text17 == doguArray[i2])
								{
									doguIndex[doguSelectIndex] = i2;
									break;
								}
							}
							if (!doguArray[doguIndex[doguSelectIndex]].StartsWith("mirror"))
							{
								UnityEngine.Object @object = null;
								GameObject gameObject3 = null;
								for (int i2 = 0; i2 < 5; i2++)
								{
									@object = Resources.Load("Prefab/" + doguArray[doguIndex[doguSelectIndex]]);
									if (@object == null)
									{
										GameObject gameObject4 = GameMain.Instance.BgMgr.CreateAssetBundle(text17);
										if (gameObject4 != null)
										{
											gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject4);
										}
									}
									if (@object != null)
									{
										break;
									}
									if (gameObject3 != null)
									{
										break;
									}
									List<int> list6;
									int index;
									(list6 = doguIndex)[index = doguSelectIndex] = list6[index] + 1;
								}
								if (gameObject3 == null)
								{
									gameObject3 = (UnityEngine.Object.Instantiate(@object) as GameObject);
								}
								doguObject.Add(gameObject3);
							}
							else
							{
								Material material = new Material(Shader.Find("Mirror"));
								GameObject gameObject5 = GameObject.CreatePrimitive(PrimitiveType.Plane);
								gameObject5.transform.localScale = new Vector3(0.3f, 1f, 0.3f);
								gameObject5.GetComponent<Renderer>().material = material;
								gameObject5.AddComponent<MirrorReflection2>();
								MirrorReflection2 component3 = gameObject5.GetComponent<MirrorReflection2>();
								component3.m_TextureSize = 2048;
								component3.m_ClipPlaneOffset = 0f;
								gameObject5.GetComponent<Renderer>().enabled = true;
								doguObject.Add(gameObject5);
							}
							Vector3 vector5 = Vector3.zero;
							Vector3 zero = Vector3.zero;
							zero.x = -90f;
							switch (doguIndex[doguSelectIndex])
							{
								case 1:
									vector5.z = -0.5f;
									goto IL_A298;
								case 2:
									vector5.z = -0.5f;
									goto IL_A298;
								case 3:
									vector5.z = 0.5f;
									zero.z = 90f;
									goto IL_A298;
								case 4:
									vector5.z = 0.9f;
									goto IL_A298;
								case 5:
									vector5.z = 0.9f;
									goto IL_A298;
								case 6:
									vector5.z = 0.6f;
									zero.x = 0f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
									goto IL_A298;
								case 7:
									vector5.z = 0.6f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
									goto IL_A298;
								case 8:
									vector5.z = 0.5f;
									zero.x = 0f;
									goto IL_A298;
								case 9:
									vector5.z = 0.32f;
									zero.z = 180f;
									goto IL_A298;
								case 10:
									vector5.z = 0.6f;
									goto IL_A298;
								case 11:
									vector5.z = 0.6f;
									goto IL_A298;
								case 12:
									vector5.z = 0.5f;
									zero.z = 180f;
									goto IL_A298;
								case 13:
									vector5.z = 0.7f;
									zero.z = 180f;
									goto IL_A298;
								case 14:
									vector5.z = -0.4f;
									goto IL_A298;
								case 15:
									vector5.z = -0.4f;
									goto IL_A298;
								case 16:
									vector5.z = -0.4f;
									zero.z = 180f;
									goto IL_A298;
								case 17:
									vector5.z = -0.4f;
									goto IL_A298;
								case 18:
									vector5.z = -0.2f;
									goto IL_A298;
								case 19:
									vector5.z = 0f;
									goto IL_A298;
								case 20:
									vector5.z = 3.4f;
									vector5.x = -1f;
									goto IL_A298;
								case 21:
									vector5.z = 3.4f;
									vector5.x = -1f;
									goto IL_A298;
								case 22:
									vector5.z = 3.4f;
									vector5.y = 0.3f;
									vector5.x = -1f;
									goto IL_A298;
								case 23:
									vector5.z = 2.7f;
									vector5.x = 0.77f;
									zero.z = 90f;
									goto IL_A298;
								case 25:
									vector5.z = 0.5f;
									vector5.y = 0.9f;
									zero.z = 180f;
									goto IL_A298;
								case 26:
									vector5.z = 0.5f;
									vector5.y = 0.9f;
									zero.z = 180f;
									goto IL_A298;
								case 29:
									vector5.z = 0.5f;
									vector5.x = -1.05f;
									goto IL_A298;
								case 30:
									vector5.z = 0f;
									zero.z = 0f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
									goto IL_A298;
								case 31:
									vector5.z = -0.6f;
									vector5.y = 0.96f;
									zero.z = 180f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
									goto IL_A298;
								case 32:
									vector5.z = -0.6f;
									vector5.y = 0.96f;
									zero.z = 180f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(0.1f, 0.4f, 0.2f);
									goto IL_A298;
								case 33:
									vector5.z = -0.6f;
									vector5.y = 0.85f;
									zero.z = 180f;
									doguObject[doguSelectIndex].transform.localScale = new Vector3(0.03f, 0.18f, 0.124f);
									goto IL_A298;
							}
							vector5.z = 0.5f;
						IL_A298:
							doguObject[doguSelectIndex].transform.localPosition = vector5;
							doguObject[doguSelectIndex].transform.localRotation = Quaternion.Euler(zero);
							doguObject[doguSelectIndex].transform.localEulerAngles = new Vector3(float.Parse(array200[38]), float.Parse(array200[39]), float.Parse(array200[40]));
							doguObject[doguSelectIndex].transform.position = new Vector3(float.Parse(array200[41]), float.Parse(array200[42]), float.Parse(array200[43]));
							doguObject[doguSelectIndex].transform.localScale = new Vector3(float.Parse(array200[44]), float.Parse(array200[45]), float.Parse(array200[46]));
						}
					}
					if (!kankyoLoadFlg)
					{
						for (int l = 0; l < array199.Length; l++)
						{
							if (maidCnt <= l)
							{
								break;
							}
							Maid maid = maidArray[l];
							if (maid && maid.Visible)
							{
								SetIK(maid, l);
								Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
								Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
								Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
								Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
								Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
								Transform transform9 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
								Transform transform10 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
								Transform transform11 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
								Transform transform12 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
								Transform transform13 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
								Transform transform14 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
								Transform transform15 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
								Transform transform16 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
								Transform transform17 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
								Transform transform18 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
								Transform transform19 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
								Transform transform20 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
								Transform transform21 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
								Transform transform22 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
								string[] array205 = array199[l].Split(new char[]
								{
									':'
								});
								for (int i2 = 0; i2 < 40; i2++)
								{
									string[] array206 = array205[i2].Split(new char[]
									{
										','
									});
									Finger[l, i2].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
								}
								bool flag5 = false;
								if (array205.Length == 64)
								{
									flag5 = true;
								}
								if (!isVR)
								{
									maid.body0.quaDefEyeL.eulerAngles = eyeL[(int)selectList[l]];
									maid.body0.quaDefEyeR.eulerAngles = eyeR[(int)selectList[l]];
								}
								bool flag6 = false;
								for (int k = 0; k < 3; k++)
								{
									for (int i2 = 40; i2 < array205.Length; i2++)
									{
										string[] array206 = array205[i2].Split(new char[]
										{
											','
										});
										switch (i2)
										{
											case 40:
												if (flag5)
												{
													Spine.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 41:
												if (flag5)
												{
													Spine0a.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine0a.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 42:
												if (flag5)
												{
													Spine1.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine1.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 43:
												if (flag5)
												{
													Spine1a.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine1a.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 44:
												if (flag5)
												{
													Pelvis.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Pelvis.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 45:
												HandL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 46:
												if (flag5)
												{
													UpperArmL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 47:
												if (flag5)
												{
													ForearmL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 48:
												HandR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 49:
												if (flag5)
												{
													UpperArmR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 50:
												if (flag5)
												{
													ForearmR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 51:
												HandL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 52:
												if (flag5)
												{
													UpperArmL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmL2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 53:
												if (flag5)
												{
													ForearmL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmL2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 54:
												HandR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 55:
												if (flag5)
												{
													UpperArmR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmR2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 56:
												if (flag5)
												{
													ForearmR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmR2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 57:
												if (flag5)
												{
													Head.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Head.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 58:
												maid.transform.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 59:
												maid.transform.position = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 60:
												maid.transform.localScale = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 63:
												if (k <= 0)
												{
													hanten[l] = false;
													hantenn[l] = false;
													TMorph morph = maid.body0.Face.morph;
													float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
													float[] fieldValue5 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
													if (!isVR)
													{
														maid.boMabataki = false;
													}
													if (morph.bodyskin.PartsVersion < 120)
													{
														fieldValue5[(int)morph.hash["eyeclose"]] = float.Parse(array206[0]);
														fieldValue5[(int)morph.hash["eyeclose2"]] = float.Parse(array206[1]);
														fieldValue5[(int)morph.hash["eyeclose3"]] = float.Parse(array206[2]);
														fieldValue5[(int)morph.hash["eyeclose6"]] = float.Parse(array206[3]);
														fieldValue5[(int)morph.hash["eyeclose5"]] = float.Parse(array206[17]);
														if (morph.hash["eyeclose7"] != null)
														{
															if (array206.Length > 37)
															{
																fieldValue5[(int)morph.hash["eyeclose7"]] = float.Parse(array206[36]);
																fieldValue5[(int)morph.hash["eyeclose8"]] = float.Parse(array206[37]);
															}
															else
															{
																fieldValue5[(int)morph.hash["eyeclose7"]] = 0f;
																fieldValue5[(int)morph.hash["eyeclose8"]] = 0f;
															}
														}
													}
													else
													{
														int num102 = 0;
														if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
														{
															num102 = 1;
														}
														if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
														{
															num102 = 2;
														}
														fieldValue5[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[0]);
														fieldValue5[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[1]);
														fieldValue[(int)morph.hash["eyeclose3"]] = float.Parse(array206[2]) * 3f;
														fieldValue5[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[3]);
														fieldValue5[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[17]);
														if (array206.Length > 37)
														{
															fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[36]);
															fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]] = float.Parse(array206[37]);
														}
														else
														{
															fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]] = 0f;
															fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]] = 0f;
														}
													}
													fieldValue[(int)morph.hash["hitomih"]] = float.Parse(array206[4]);
													fieldValue[(int)morph.hash["hitomis"]] = float.Parse(array206[5]);
													fieldValue[(int)morph.hash["mayuha"]] = float.Parse(array206[6]);
													fieldValue[(int)morph.hash["mayuup"]] = float.Parse(array206[7]);
													fieldValue[(int)morph.hash["mayuv"]] = float.Parse(array206[8]);
													fieldValue[(int)morph.hash["mayuvhalf"]] = float.Parse(array206[9]);
													fieldValue[(int)morph.hash["moutha"]] = float.Parse(array206[10]);
													fieldValue[(int)morph.hash["mouths"]] = float.Parse(array206[11]);
													fieldValue[(int)morph.hash["mouthdw"]] = float.Parse(array206[12]);
													fieldValue[(int)morph.hash["mouthup"]] = float.Parse(array206[13]);
													fieldValue[(int)morph.hash["tangout"]] = float.Parse(array206[14]);
													fieldValue[(int)morph.hash["tangup"]] = float.Parse(array206[15]);
													if (morph.bodyskin.PartsVersion < 120)
													{
														fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array206[16]);
													}
													else
													{
														fieldValue[(int)morph.hash["eyebig"]] = float.Parse(array206[16]) * 3f;
													}
													fieldValue[(int)morph.hash["mayuw"]] = float.Parse(array206[18]);
													fieldValue[(int)morph.hash["mouthhe"]] = float.Parse(array206[19]);
													fieldValue[(int)morph.hash["mouthc"]] = float.Parse(array206[20]);
													fieldValue[(int)morph.hash["mouthi"]] = float.Parse(array206[21]);
													fieldValue[(int)morph.hash["mouthuphalf"]] = float.Parse(array206[22]) + 0.01f;
													isLoadFace[l] = true;
													try
													{
														fieldValue[(int)morph.hash["tangopen"]] = float.Parse(array206[23]);
													}
													catch
													{
													}
													if (float.Parse(array206[24]) == 1f)
													{
														fieldValue[(int)morph.hash["namida"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["namida"]] = 0f;
													}
													if (float.Parse(array206[25]) == 1f)
													{
														fieldValue[(int)morph.hash["tear1"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["tear1"]] = 0f;
													}
													if (float.Parse(array206[26]) == 1f)
													{
														fieldValue[(int)morph.hash["tear2"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["tear2"]] = 0f;
													}
													if (float.Parse(array206[27]) == 1f)
													{
														fieldValue[(int)morph.hash["tear3"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["tear3"]] = 0f;
													}
													if (float.Parse(array206[28]) == 1f)
													{
														fieldValue[(int)morph.hash["shock"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["shock"]] = 0f;
													}
													if (float.Parse(array206[29]) == 1f)
													{
														fieldValue[(int)morph.hash["yodare"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["yodare"]] = 0f;
													}
													if (float.Parse(array206[30]) == 1f)
													{
														fieldValue[(int)morph.hash["hoho"]] = 0.5f;
													}
													else
													{
														fieldValue[(int)morph.hash["hoho"]] = 0f;
													}
													if (float.Parse(array206[31]) == 1f)
													{
														fieldValue[(int)morph.hash["hoho2"]] = 0.5f;
													}
													else
													{
														fieldValue[(int)morph.hash["hoho2"]] = 0f;
													}
													if (float.Parse(array206[32]) == 1f)
													{
														fieldValue[(int)morph.hash["hohos"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["hohos"]] = 0f;
													}
													if (float.Parse(array206[33]) == 1f)
													{
														fieldValue[(int)morph.hash["hohol"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["hohol"]] = 0f;
													}
													if (float.Parse(array206[34]) == 1f)
													{
														fieldValue[(int)morph.hash["toothoff"]] = 1f;
													}
													else
													{
														fieldValue[(int)morph.hash["toothoff"]] = 0f;
													}
													if (array206.Length > 35)
													{
														if (float.Parse(array206[35]) == 1f)
														{
															morph.boNoseFook = true;
														}
														else
														{
															morph.boNoseFook = false;
														}
													}
													isFace[l] = true;
													isFaceInit = true;
												}
												break;
											case 64:
												if (k <= 0)
												{
													if (!isVR)
													{
														if (int.Parse(array206[0]) == 1)
														{
															isLook[l] = true;
															lookX[l] = float.Parse(array206[1]);
															lookY[l] = float.Parse(array206[2]);
															if (maid.body0.offsetLookTarget.x != lookY[l])
															{
																if (isLock[l] && lookY[l] < 0f)
																{
																	maid.body0.offsetLookTarget = new Vector3(lookY[l] * 0.6f, 1f, lookX[l]);
																}
																else
																{
																	maid.body0.offsetLookTarget = new Vector3(lookY[l], 1f, lookX[l]);
																}
															}
															if (lookX[l] != lookXn[l])
															{
																lookXn[l] = lookX[l];
																maid.body0.offsetLookTarget = new Vector3(lookY[l], 1f, lookX[l]);
															}
															if (lookY[l] != lookYn[l])
															{
																lookYn[l] = lookY[l];
																if (isLock[l] && lookY[l] < 0f)
																{
																	maid.body0.offsetLookTarget = new Vector3(lookY[l] * 0.6f, 1f, lookX[l]);
																}
																else
																{
																	maid.body0.offsetLookTarget = new Vector3(lookY[l], 1f, lookX[l]);
																}
															}
														}
														else
														{
															isLook[l] = false;
														}
														if (isLook[l])
														{
															maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
															maid.body0.boHeadToCam = true;
															maid.body0.boEyeToCam = true;
															maid.body0.trsLookTarget = null;
														}
														else
														{
															maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
															maid.body0.boHeadToCam = true;
															maid.body0.boEyeToCam = true;
														}
													}
												}
												break;
											case 65:
												if (k <= 0)
												{
													if (itemIndex[l] != int.Parse(array206[0]))
													{
														itemIndex[l] = int.Parse(array206[0]);
														if (itemIndex[l] == itemArray.Length - 1)
														{
															itemIndex[l] = 0;
														}
														string[] array207 = new string[2];
														array207 = itemArray[itemIndex[l]].Split(new char[]
														{
														','
														});
														if (itemIndex[l] > 13)
														{
															array207 = itemArray[itemIndex[l] + 1].Split(new char[]
															{
															','
															});
														}
														maid.DelProp(MPN.handitem, true);
														maid.DelProp(MPN.accvag, true);
														maid.DelProp(MPN.accanl, true);
														bool flag7 = false;
														if (itemIndex[l] == 12 || itemIndex[l] == 13)
														{
															flag7 = true;
														}
														if (!flag7)
														{
															maid.DelProp(MPN.kousoku_upper, true);
															maid.DelProp(MPN.kousoku_lower, true);
														}
														if (array207[0] != "")
														{
															maid.SetProp(array207[0], array207[1], 0, true, false);
														}
														if (itemIndex[l] == 12)
														{
															array207 = itemArray[itemIndex[l] - 1].Split(new char[]
															{
															','
															});
															maid.SetProp(array207[0], array207[1], 0, true, false);
														}
														if (itemIndex[l] == 13)
														{
															array207 = itemArray[itemIndex[l] + 1].Split(new char[]
															{
															','
															});
															maid.SetProp(array207[0], array207[1], 0, true, false);
														}
														if (itemIndex[l] == 23)
														{
															array207 = itemArray[itemIndex[l]].Split(new char[]
															{
															','
															});
															maid.SetProp(array207[0], array207[1], 0, true, false);
															cafeFlg[l] = true;
														}
														maid.AllProcPropSeqStart();
													}
													IK_hand = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
												}
												break;
											case 66:
												{
													Transform transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
													transform3.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
													break;
												}
											case 67:
												{
													Transform transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
													transform4.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
													break;
												}
											case 68:
												if (array206.Length >= 2)
												{
													ClavicleL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 69:
												ClavicleR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 70:
												if (int.Parse(array206[0]) == 1)
												{
													hanten[l] = true;
													hantenn[l] = true;
												}
												break;
											case 71:
												transform2.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 72:
												transform5.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 73:
												transform6.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 74:
												transform7.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 75:
												transform8.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 76:
												transform9.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 77:
												transform10.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 78:
												transform11.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 79:
												transform12.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 80:
												transform13.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 81:
												transform14.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 82:
												transform15.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 83:
												transform16.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 84:
												transform17.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 85:
												transform18.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 86:
												transform19.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 87:
												transform20.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 88:
												transform21.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 89:
												transform22.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 90:
												{
													float num203 = float.Parse(array206[0]);
													if (maid.body0.Face.morph.bodyskin.PartsVersion < 120)
													{
														if (num203 > 4.1f && num203 < 5.1f)
														{
															flag6 = true;
														}
													}
													else if (num203 > 0f && num203 < 0.6f)
													{
														flag6 = true;
													}
													if (!isVR && flag6)
													{
														maid.body0.quaDefEyeL.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
													}
													break;
												}
											case 91:
												if (!isVR && flag6)
												{
													maid.body0.quaDefEyeR.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 92:
												vIKMuneL[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												muneIKL[l] = true;
												break;
											case 93:
												vIKMuneLSub[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 94:
												vIKMuneR[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												muneIKR[l] = true;
												break;
											case 95:
												vIKMuneRSub[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 96:
												haraCount[l] = 2;
												haraPosition[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												transform2.position = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
										}
									}
								}
								isStop[l] = true;
								isLock[l] = true;
							}
						}
					}
					isBloom = false;
					bloom1 = 2.85f;
					bloom2 = 3f;
					bloom3 = 0f;
					bloom4 = 0f;
					bloom5 = 0f;
					isBloomA = false;
					isBlur = false;
					blur1 = -3.98f;
					blur4 = -0.75f;
					blur2 = 0.9f;
					blur3 = 4.19f;
					bokashi = 0f;
					kamiyure = 0f;
					isDepth = false;
					isDepthA = false;
					depth1 = 3f;
					depth2 = 0.1f;
					depth3 = 15f;
					depth4 = 3f;
					isFog = false;
					fog1 = 4f;
					fog2 = 1f;
					fog3 = 1f;
					fog4 = 0f;
					fog5 = 1f;
					fog6 = 1f;
					fog7 = 1f;
					isSepia = false;
					if (array201 != null)
					{
						lightKage[0] = float.Parse(array201[0]);
						if (int.Parse(array201[1]) == 1)
						{
							isBloom = true;
							bloom1 = float.Parse(array201[2]);
							bloom2 = float.Parse(array201[3]);
							bloom3 = float.Parse(array201[4]);
							bloom4 = float.Parse(array201[5]);
							bloom5 = float.Parse(array201[6]);
							if (int.Parse(array201[7]) == 1)
							{
								isBloomA = true;
							}
						}
						if (int.Parse(array201[8]) == 1)
						{
							isBlur = true;
							blur1 = float.Parse(array201[9]);
							blur2 = float.Parse(array201[10]);
							blur3 = float.Parse(array201[11]);
							blur4 = float.Parse(array201[12]);
						}
						bokashi = float.Parse(array201[13]);
						kamiyure = float.Parse(array201[14]);
						if (array201.Length > 15)
						{
							if (int.Parse(array201[15]) == 1)
							{
								isDepth = true;
								depth1 = float.Parse(array201[16]);
								depth2 = float.Parse(array201[17]);
								depth3 = float.Parse(array201[18]);
								depth4 = float.Parse(array201[19]);
								if (int.Parse(array201[20]) == 1)
								{
									isDepthA = true;
								}
							}
							if (int.Parse(array201[21]) == 1)
							{
								isFog = true;
								fog1 = float.Parse(array201[22]);
								fog2 = float.Parse(array201[23]);
								fog3 = float.Parse(array201[24]);
								fog4 = float.Parse(array201[25]);
								fog5 = float.Parse(array201[26]);
								fog6 = float.Parse(array201[27]);
								fog7 = float.Parse(array201[28]);
							}
							if (int.Parse(array201[29]) == 1)
							{
								isSepia = true;
							}
						}
					}
					if (array202 != null)
					{
						for (int l = 0; l < array202.Length - 1; l++)
						{
							string[] array206 = array202[l].Split(new char[]
							{
								','
							});
							GameObject gameObject6 = new GameObject("Light");
							gameObject6.AddComponent<Light>();
							lightList.Add(gameObject6);
							lightIndex.Add(int.Parse(array206[0]));
							lightColorR.Add(float.Parse(array206[1]));
							lightColorG.Add(float.Parse(array206[2]));
							lightColorB.Add(float.Parse(array206[3]));
							lightX.Add(float.Parse(array206[4]));
							lightY.Add(float.Parse(array206[5]));
							lightAkarusa.Add(float.Parse(array206[6]));
							lightRange.Add(float.Parse(array206[7]));
							lightKage.Add(0.098f);
							gameObject6.transform.position = GameMain.Instance.MainLight.transform.position;
							selectLightIndex = lightList.Count - 1;
							lightComboList = new GUIContent[lightList.Count];
							for (int k = 0; k < lightList.Count; k++)
							{
								if (k == 0)
								{
									lightComboList[k] = new GUIContent("メイン");
								}
								else
								{
									lightComboList[k] = new GUIContent("追加" + k);
								}
							}
							lightCombo.selectedItemIndex = selectLightIndex;
							gameObject6.GetComponent<Light>().intensity = lightAkarusa[l];
							gameObject6.GetComponent<Light>().spotAngle = lightRange[l];
							gameObject6.GetComponent<Light>().range = lightRange[l] / 5f;
							if (lightIndex[selectLightIndex] == 0)
							{
								lightList[selectLightIndex].GetComponent<Light>().type = LightType.Directional;
							}
							else if (lightIndex[selectLightIndex] == 1)
							{
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
					}
					if (array204 != null)
					{
						for (int l = 0; l < lightList.Count; l++)
						{
							string[] array206 = array204[l].Split(new char[]
							{
								','
							});
							lightList[l].transform.position = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
							if (gLight[l] == null)
							{
								gLight[l] = GameObject.CreatePrimitive(PrimitiveType.Cube);
								Material material2 = new Material(Shader.Find("Transparent/Diffuse"));
								material2.color = new Color(0.5f, 0.5f, 1f, 0.8f);
								gLight[l].GetComponent<Renderer>().material = material2;
								gLight[l].layer = 8;
								gLight[l].GetComponent<Renderer>().enabled = false;
								gLight[l].SetActive(false);
								gLight[l].transform.position = lightList[l].transform.position;
								mLight[l] = gLight[l].AddComponent<MouseDrag6>();
								mLight[l].obj = gLight[l];
								mLight[l].maid = lightList[l].gameObject;
								mLight[l].angles = lightList[l].transform.eulerAngles;
								gLight[l].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
								mLight[l].ido = 1;
								mLight[l].isScale = false;
							}
						}
					}
					if (array203 != null)
					{
						for (int l = 0; l < doguBObject.Count; l++)
						{
							UnityEngine.Object.Destroy(doguBObject[l]);
						}
						doguBObject = new List<GameObject>();
						List<string> list4 = new List<string>();
						for (int l = 0; l < array203.Length - 1; l++)
						{
							string[] array206 = array203[l].Split(new char[]
							{
								','
							});
							string text17 = array206[0].Replace(" ", "_");
							GameObject gameObject3 = null;
							bool flag8 = false;
							if (text17.Contains(".menu"))
							{
								string text18 = text17;
								byte[] array208 = null;
								try
								{
									using (AFileBase afileBase = GameUty.FileOpen(text18, null))
									{
										NDebug.Assert(afileBase.IsValid(), "メニューファイルが存在しません。 :" + text18);
										if (array208 == null)
										{
											array208 = new byte[afileBase.GetSize()];
										}
										else if (array208.Length < afileBase.GetSize())
										{
											array208 = new byte[afileBase.GetSize()];
										}
										afileBase.Read(ref array208, afileBase.GetSize());
									}
									string[] array209 = AdvancedStudioMode.ProcScriptBin(maidArray[0], array208, text18, false);
									gameObject3 = ImportCM2.LoadSkinMesh_R(array209[0], array209, "", maidArray[0].body0.goSlot[8], 1);
									doguBObject.Add(gameObject3);
									gameObject3.name = text18;
									Vector3 vector5 = Vector3.zero;
									Vector3 zero = Vector3.zero;
									vector5.z = 0.4f;
									if (text17.Contains("HandItem"))
									{
										string text19 = text17;
										if (text19 != null)
										{
											if (text19 == "HandItemR_WineGlass_I_.menu")
											{
												zero.z = 90f;
												vector5.y = 0.04f;
												goto IL_D31E;
											}
											if (text19 == "HandItemR_WineBottle_I_.menu")
											{
												zero.z = 90f;
												goto IL_D31E;
											}
											if (text19 == "HandItemR_Mop_I_.menu")
											{
												zero.x = 90f;
												goto IL_D31E;
											}
											if (text19 == "HandItemL_Dance_Hataki_I_.menu")
											{
												goto IL_D31E;
											}
											if (text19 == "HandItemL_Dance_Mop_I_.menu")
											{
												goto IL_D31E;
											}
										}
										zero.x = -90f;
									IL_D31E:;
									}
									gameObject3.transform.localPosition = vector5;
									gameObject3.transform.localRotation = Quaternion.Euler(zero);
								}
								catch
								{
									flag8 = true;
								}
							}
							else
							{
								try
								{
									bool flag9 = false;
									bool flag10 = false;
									if (text17.StartsWith("BG_"))
									{
										UnityEngine.Object @object = GameMain.Instance.BgMgr.CreateAssetBundle(text17.Replace("BG_", ""));
										if (@object == null)
										{
											@object = Resources.Load(text17.Replace("BG_", "BG/"));
										}
										gameObject3 = (UnityEngine.Object.Instantiate(@object) as GameObject);
										doguBObject.Add(gameObject3);
									}
									else if (text17.StartsWith("MYR_"))
									{
										GameObject gameObject = GameObject.Find("Deployment Object Parent");
										if (gameObject == null)
										{
											gameObject = new GameObject("Deployment Object Parent");
										}
										int num204 = int.Parse(text17.Replace("MYR_", ""));
										PlacementData.Data data = PlacementData.GetData(num204);
										GameObject prefab = data.GetPrefab();
										GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(prefab);
										GameObject gameObject4 = new GameObject(gameObject7.name);
										gameObject7.transform.SetParent(gameObject4.transform, true);
										gameObject4.transform.SetParent(gameObject.transform, false);
										gameObject3 = gameObject4;
										doguBObject.Add(gameObject3);
									}
									else if (text17.Contains("#"))
									{
										string[] array205 = text17.Split(new char[]
										{
											'#'
										});
										gameObject3 = GameMain.Instance.BgMgr.CreateAssetBundle(array205[1]);
										if (gameObject3 != null)
										{
											gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject3);
											MeshRenderer[] componentsInChildren = gameObject3.GetComponentsInChildren<MeshRenderer>();
											for (int j = 0; j < componentsInChildren.Length; j++)
											{
												if (componentsInChildren[j] != null)
												{
													componentsInChildren[j].shadowCastingMode = ShadowCastingMode.Off;
												}
											}
										}
										flag9 = true;
										if (!parArray[parIndex].Contains("Odogu_"))
										{
											flag10 = true;
										}
										doguBObject.Add(gameObject3);
									}
									else if (!text17.StartsWith("mirror") && text17.IndexOf(":") < 0)
									{
										UnityEngine.Object @object;
										if (text17.StartsWith("BG"))
										{
											string text3 = text17.Replace("BG", "");
											@object = GameMain.Instance.BgMgr.CreateAssetBundle(text3);
											if (@object == null)
											{
												@object = Resources.Load("BG/" + text3);
											}
										}
										else
										{
											@object = Resources.Load("Prefab/" + text17);
											if (@object == null)
											{
												GameObject gameObject4 = GameMain.Instance.BgMgr.CreateAssetBundle(text17);
												gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject4);
											}
										}
										if (gameObject3 == null)
										{
											gameObject3 = (UnityEngine.Object.Instantiate(@object) as GameObject);
										}
										doguBObject.Add(gameObject3);
									}
									else if (text17.StartsWith("mirror"))
									{
										Material material = new Material(Shader.Find("Mirror"));
										GameObject gameObject5 = GameObject.CreatePrimitive(PrimitiveType.Plane);
										gameObject5.transform.localScale = new Vector3(0.3f, 1f, 0.3f);
										gameObject5.GetComponent<Renderer>().material = material;
										gameObject5.AddComponent<MirrorReflection2>();
										MirrorReflection2 component3 = gameObject5.GetComponent<MirrorReflection2>();
										component3.m_TextureSize = 2048;
										component3.m_ClipPlaneOffset = 0f;
										gameObject5.GetComponent<Renderer>().enabled = true;
										gameObject3 = gameObject5;
										doguBObject.Add(gameObject3);
									}
									else
									{
										string[] array205 = text17.Split(new char[]
										{
											':'
										});
										UnityEngine.Object @object = GameMain.Instance.BgMgr.CreateAssetBundle(array205[0]);
										if (@object == null)
										{
											@object = Resources.Load("BG/" + array205[0]);
										}
										GameObject gameObject8 = UnityEngine.Object.Instantiate(@object) as GameObject;
										int num205 = 0;
										int num206 = 0;
										int.TryParse(array205[1], out num206);
										foreach (object obj in gameObject8.transform)
										{
											Transform transform27 = (Transform)obj;
											if (num206 == num205)
											{
												gameObject3 = transform27.gameObject;
												break;
											}
											num205++;
										}
										doguBObject.Add(gameObject3);
										gameObject3.transform.parent = null;
										UnityEngine.Object.Destroy(gameObject8);
										gameObject8.SetActive(false);
									}
									gameObject3.name = text17;
									Vector3 vector5 = Vector3.zero;
									Vector3 zero = Vector3.zero;
									if (text17.StartsWith("BG_"))
									{
										gameObject3.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
									}
									string text19 = text17;
									switch (text19)
									{
										case "Odogu_XmasTreeMini_photo_ver":
											vector5.z = 0.6f;
											gameObject3.transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);
											foreach (object obj2 in gameObject3.transform)
											{
												Transform transform28 = (Transform)obj2;
												if (transform28.GetComponent<Collider>() != null)
												{
													transform28.GetComponent<Collider>().enabled = false;
												}
											}
											goto IL_DEE9;
										case "Odogu_KadomatsuMini_photo_ver":
											vector5.z = 0.6f;
											gameObject3.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
											foreach (object obj3 in gameObject3.transform)
											{
												Transform transform28 = (Transform)obj3;
												if (transform28.GetComponent<Collider>() != null)
												{
													transform28.GetComponent<Collider>().enabled = false;
												}
											}
											goto IL_DEE9;
										case "Odogu_ClassRoomDesk":
											vector5.z = 0.5f;
											zero.x = -90f;
											goto IL_DEE9;
										case "mirror1":
											vector5.z = -0.6f;
											vector5.y = 0.96f;
											zero.z = 180f;
											zero.x = -90f;
											gameObject3.transform.localScale = new Vector3(0.2f, 0.4f, 0.2f);
											goto IL_DEE9;
										case "mirror2":
											vector5.z = -0.6f;
											vector5.y = 0.96f;
											zero.z = 180f;
											zero.x = -90f;
											gameObject3.transform.localScale = new Vector3(0.1f, 0.4f, 0.2f);
											goto IL_DEE9;
										case "mirror3":
											vector5.z = -0.6f;
											vector5.y = 0.85f;
											zero.z = 180f;
											zero.x = -90f;
											gameObject3.transform.localScale = new Vector3(0.03f, 0.18f, 0.124f);
											goto IL_DEE9;
										case "Particle/pLineY":
										case "Particle/pLineP02":
										case "Particle/pLine_act2":
											gameObject3.transform.localScale = new Vector3(3f, 3f, 3f);
											goto IL_DEE9;
										case "Mob_Man_Stand001":
										case "Mob_Man_Stand002":
										case "Mob_Man_Stand003":
										case "Mob_Man_Sit001":
										case "Mob_Man_Sit002":
										case "Mob_Man_Sit003":
										case "Mob_Girl_Stand001":
										case "Mob_Girl_Stand002":
										case "Mob_Girl_Stand003":
										case "Mob_Girl_Sit001":
										case "Mob_Girl_Sit002":
										case "Mob_Girl_Sit003":
										case "Salon:63":
										case "Salon:65":
										case "Salon:69":
										case "Salon_Entrance:0":
										case "Salon_Entrance:1":
										case "Salon_Entrance:2":
										case "Salon_Entrance:3":
										case "Salon_Entrance:4":
										case "Pool:26":
										case "Shitsumu:23":
										case "Shitsumu_Night:23":
											vector5.z = 0.5f;
											zero.x = -90f;
											goto IL_DEE9;
										case "Odogu_Dresser_photo_ver":
											GameObject.Find("Prim.00000000").GetComponent<Collider>().enabled = false;
											GameObject.Find("Prim.00000001").GetComponent<Collider>().enabled = false;
											GameObject.Find("Prim.00000002").GetComponent<Collider>().enabled = false;
											GameObject.Find("Prim.00000004").GetComponent<Collider>().enabled = false;
											goto IL_DEE9;
									}
									vector5.z = 0.5f;
									if (text17.StartsWith("Odogu_"))
									{
										foreach (object obj4 in gameObject3.transform)
										{
											Transform transform28 = (Transform)obj4;
											if (transform28.GetComponent<Collider>() != null)
											{
												transform28.GetComponent<Collider>().enabled = false;
											}
										}
									}
									else if (gameObject3.GetComponent<Collider>() != null)
									{
										gameObject3.GetComponent<Collider>().enabled = false;
									}
								IL_DEE9:
									if (flag9)
									{
										vector5.z = 0.4f;
										if (flag10)
										{
											zero.x = -90f;
										}
									}
									gameObject3.transform.localPosition = vector5;
									gameObject3.transform.localRotation = Quaternion.Euler(zero);
								}
								catch
								{
									flag8 = true;
								}
							}
							if (!flag8)
							{
								doguCnt = doguBObject.Count - 1;
								gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
								Vector3 localEulerAngles2 = gameObject3.transform.localEulerAngles;
								gameObject3.transform.localEulerAngles = new Vector3(float.Parse(array206[1]), float.Parse(array206[2]), float.Parse(array206[3]));
								gameObject3.transform.position = new Vector3(float.Parse(array206[4]), float.Parse(array206[5]), float.Parse(array206[6]));
								gDogu[doguCnt].transform.localEulerAngles = new Vector3(float.Parse(array206[1]), float.Parse(array206[2]), float.Parse(array206[3]));
								gDogu[doguCnt].transform.position = new Vector3(float.Parse(array206[4]), float.Parse(array206[5]), float.Parse(array206[6]));
								gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
								gDogu[doguCnt].layer = 8;
								gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
								gDogu[doguCnt].SetActive(false);
								gDogu[doguCnt].transform.position = gameObject3.transform.position;
								mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
								mDogu[doguCnt].obj = gDogu[doguCnt];
								mDogu[doguCnt].maid = gameObject3;
								mDogu[doguCnt].angles = localEulerAngles2;
								gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
								mDogu[doguCnt].ido = 1;
								mDogu[doguCnt].isScale = false;
								if (text17 == "Particle/pLineY")
								{
									mDogu[doguCnt].count = 180;
								}
								if (text17 == "Particle/pLineP02")
								{
									mDogu[doguCnt].count = 115;
								}
								if (gameObject3.name == "Particle/pLine_act2")
								{
									mDogu[doguCnt].count = 80;
									gameObject3.transform.localScale = new Vector3(3f, 3f, 3f);
								}
								if (gameObject3.name == "Particle/pHeart01")
								{
									mDogu[doguCnt].count = 80;
								}
								if (text17 == "mirror1" || text17 == "mirror2" || text17 == "mirror3")
								{
									mDogu[doguCnt].isScale = true;
									mDogu[doguCnt].isScale2 = true;
									mDogu[doguCnt].scale2 = gameObject3.transform.localScale;
									if (text17 == "mirror1")
									{
										mDogu[doguCnt].scale = new Vector3(gameObject3.transform.localScale.x * 5f, gameObject3.transform.localScale.y * 5f, gameObject3.transform.localScale.z * 5f);
									}
									if (text17 == "mirror2")
									{
										mDogu[doguCnt].scale = new Vector3(gameObject3.transform.localScale.x * 10f, gameObject3.transform.localScale.y * 10f, gameObject3.transform.localScale.z * 10f);
									}
									if (text17 == "mirror3")
									{
										mDogu[doguCnt].scale = new Vector3(gameObject3.transform.localScale.x * 33f, gameObject3.transform.localScale.y * 33f, gameObject3.transform.localScale.z * 33f);
									}
								}
								if (text17 == "Odogu_XmasTreeMini_photo_ver" || text17 == "Odogu_KadomatsuMini_photo_ver")
								{
									mDogu[doguCnt].isScale2 = true;
									mDogu[doguCnt].scale2 = gameObject3.transform.localScale;
								}
								gameObject3.transform.localScale = new Vector3(float.Parse(array206[7]), float.Parse(array206[8]), float.Parse(array206[9]));
							}
							else
							{
								list4.Add(text17);
							}
						}
						if (list4.Count > 0)
						{
							Console.WriteLine("\nシーンの読み込みに以下のファイルが不足しています。");
							foreach (string text in list4)
							{
								Console.WriteLine(text);
							}
						}
					}
					loadScene = 0;
					kankyoLoadFlg = false;
				}
				for (int l = 0; l < maidCnt; l++)
				{
					if (loadPose[l] != "" && isLoadPose[l])
					{
						IniKey iniKey3 = base.Preferences["pose"][loadPose[l]];
						if (iniKey3.Value == null || !(iniKey3.Value.ToString() != "") || !(iniKey3.Value.ToString() != "del"))
						{
							loadPose[l] = "";
							isLoadPose[l] = false;
						}
						else
						{
							if (loadCount[l] > 4)
							{
								isLoadPose[l] = false;
								loadPose[l] = "";
								loadCount[l] = 0;
							}
							for (int k = 0; k < 10; k++)
							{
							}
							loadCount[l]++;
							Maid maid = maidArray[l];
							isStop[l] = true;
							if (maid && maid.Visible)
							{
								SetIK(maid, l);
								Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
								Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
								Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
								Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
								Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
								Transform transform9 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
								Transform transform10 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
								Transform transform11 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
								Transform transform12 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
								Transform transform13 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
								Transform transform14 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
								Transform transform15 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
								Transform transform16 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
								Transform transform17 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
								Transform transform18 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
								Transform transform19 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
								Transform transform20 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
								Transform transform21 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
								Transform transform22 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
								string[] array205 = iniKey3.Value.Split(new char[]
								{
									'_'
								})[1].Split(new char[]
								{
									':'
								});
								for (int i2 = 0; i2 < 40; i2++)
								{
									string[] array206 = array205[i2].Split(new char[]
									{
										','
									});
									Finger[l, i2].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
								}
								bool flag5 = false;
								if (array205.Length == 64)
								{
									flag5 = true;
								}
								Vector3 localEulerAngles = maid.transform.localEulerAngles;
								maid.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
								Vector3 position2 = maid.transform.position;
								maid.transform.position = new Vector3(0f, 0f, 0f);
								for (int k = 0; k < 3; k++)
								{
									for (int i2 = 40; i2 < array205.Length; i2++)
									{
										string[] array206 = array205[i2].Split(new char[]
										{
											','
										});
										switch (i2)
										{
											case 40:
												if (flag5)
												{
													Spine.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 41:
												if (flag5)
												{
													Spine0a.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine0a.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 42:
												if (flag5)
												{
													Spine1.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine1.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 43:
												if (flag5)
												{
													Spine1a.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Spine1a.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 44:
												if (flag5)
												{
													Pelvis.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Pelvis.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 45:
												HandL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 46:
												if (flag5)
												{
													UpperArmL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 47:
												if (flag5)
												{
													ForearmL1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 48:
												HandR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 49:
												if (flag5)
												{
													UpperArmR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 50:
												if (flag5)
												{
													ForearmR1[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 51:
												HandL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 52:
												if (flag5)
												{
													UpperArmL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmL2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 53:
												if (flag5)
												{
													ForearmL2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmL2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 54:
												HandR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 55:
												if (flag5)
												{
													UpperArmR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													UpperArmR2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 56:
												if (flag5)
												{
													ForearmR2[l].localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													ForearmR2[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 57:
												if (flag5)
												{
													Head.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												if (!flag5)
												{
													Head.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 66:
												{
													Transform transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
													transform3.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
													break;
												}
											case 67:
												{
													Transform transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
													transform4.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
													break;
												}
											case 68:
												if (array206.Length >= 2)
												{
													ClavicleL1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												}
												break;
											case 69:
												ClavicleR1[l].eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 71:
												transform2.eulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 72:
												transform5.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 73:
												transform6.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 74:
												transform7.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 75:
												transform8.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 76:
												transform9.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 77:
												transform10.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 78:
												transform11.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 79:
												transform12.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 80:
												transform13.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 81:
												transform14.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 82:
												transform15.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 83:
												transform16.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 84:
												transform17.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 85:
												transform18.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 86:
												transform19.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 87:
												transform20.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 88:
												transform21.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 89:
												transform22.localEulerAngles = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 92:
												vIKMuneL[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												muneIKL[l] = true;
												break;
											case 93:
												vIKMuneLSub[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 94:
												vIKMuneR[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												muneIKR[l] = true;
												break;
											case 95:
												vIKMuneRSub[l] = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
											case 96:
												transform2.position = new Vector3(float.Parse(array206[0]), float.Parse(array206[1]), float.Parse(array206[2]));
												break;
										}
									}
								}
								maid.transform.localEulerAngles = localEulerAngles;
								maid.transform.position = position2;
							}
						}
					}
					if (loadPose[l] != "" && !isLoadPose[l])
					{
						IniKey iniKey3 = base.Preferences["pose"][loadPose[l]];
						if (iniKey3.Value != null && iniKey3.Value.ToString() != "" && iniKey3.Value.ToString() != "del")
						{
							isStop[l] = true;
						}
						isLoadPose[l] = true;
					}
				}
				if (loadScene > 0)
				{
					string path4 = string.Concat(new object[]
					{
						Path.GetFullPath(".\\"),
						"Mod\\AdvancedStudioModeScene\\",
						loadScene,
						".png"
					});
					string text20 = "";
					IniKey iniKey2 = null;
					if (File.Exists(path4))
					{
						FileStream fileStream = new FileStream(path4, FileMode.Open, FileAccess.Read);
						BinaryReader binaryReader = new BinaryReader(fileStream);
						byte[] array197 = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
						byte[] value2 = new byte[]
						{
							array197[36],
							array197[35],
							array197[34],
							array197[33]
						};
						int count = BitConverter.ToInt32(value2, 0) - 8;
						byte[] bytes = array197.Skip(49).Take(count).ToArray<byte>();
						text20 = Encoding.UTF8.GetString(bytes);
						text20 = AdvancedStudioMode.StringFromBase64Comp(text20);
					}
					else
					{
						iniKey2 = base.Preferences["scene"]["s" + loadScene];
					}
					if (text20 == "" && (iniKey2.Value == null || !(iniKey2.Value.ToString() != "")))
					{
						loadScene = 0;
					}
					else
					{
						isScene = true;
						if (!kankyoLoadFlg)
						{
							string text16 = text20;
							if (text20 == "")
							{
								text16 = iniKey2.Value;
							}
							string[] array198 = text16.Split(new char[]
							{
								'_'
							});
							string[] array199 = array198[1].Split(new char[]
							{
								';'
							});
							int i;
							for (i = 0; i < array199.Length; i++)
							{
								if (maidCnt <= i)
								{
									break;
								}
								Maid maid = maidArray[i];
								for (int k = 0; k < 10; k++)
								{
									maid.transform.RotateAround(maid.transform.position, Vector3.right, -maid.transform.rotation.eulerAngles.x);
									maid.transform.RotateAround(maid.transform.position, Vector3.forward, -maid.transform.rotation.eulerAngles.z);
								}
								string[] array2 = array199[i].Split(new char[]
								{
									':'
								});
								if (!int.TryParse(array2[61], out poseIndex[i]))
								{
									string a2 = array2[61].Replace(" ", "_").Replace("|", ",");
									for (int i2 = 0; i2 < poseArray.Length; i2++)
									{
										if (a2 == poseArray[i2])
										{
											poseIndex[i] = i2;
											break;
										}
									}
								}
								string[] array206 = poseArray[poseIndex[i]].Split(new char[]
								{
									','
								});
								isStop[i] = true;
								poseCount[i] = 20;
								int num207;
								if (array2[61].Contains("AdvancedStudioModePose"))
								{
									string url = "";
									Action<string, List<string>> action2 = delegate (string path, List<string> result_list)
									{
										string[] files = Directory.GetFiles(path);
										for (int num208 = 0; num208 < files.Length; num208++)
										{
											if (Path.GetExtension(files[num208]) == ".anm")
											{
												string text21 = files[num208].Split(new char[]
												{
													'\\'
												})[files[num208].Split(new char[]
												{
													'\\'
												}).Length - 1];
												text21 = text21.Split(new char[]
												{
													'.'
												})[0];
												if (text21 == array2[61].Replace("AdvancedStudioModePose", ""))
												{
													url = files[num208];
													for (int num209 = 0; num209 < poseArray.Length; num209++)
													{
														string b = poseArray[num209].Replace("\u3000", "").Split(new char[]
														{
															'/'
														})[0];
														if (text21 == b)
														{
															poseIndex[i] = num209;
														}
													}
													break;
												}
											}
										}
									};
									List<string> arg = new List<string>();
									action2(Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose", arg);
									if (url != "")
									{
										string path3 = url;
										byte[] array210 = new byte[0];
										try
										{
											using (FileStream fileStream = new FileStream(path3, FileMode.Open, FileAccess.Read))
											{
												array210 = new byte[fileStream.Length];
												fileStream.Read(array210, 0, array210.Length);
											}
										}
										catch
										{
										}
										if (0 < array210.Length)
										{
											string fileName = Path.GetFileName(path3);
											long num7 = (long)fileName.GetHashCode();
											maid.body0.CrossFade(num7.ToString(), array210, false, false, false, 0f, 1f);
											Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
											{
												default(Maid.AutoTwist),
												Maid.AutoTwist.ShoulderR,
												Maid.AutoTwist.WristL,
												Maid.AutoTwist.WristR,
												Maid.AutoTwist.ThighL,
												Maid.AutoTwist.ThighR
											};
											for (int n = 0; n < array3.Length; n++)
											{
												maid.SetAutoTwist(array3[n], true);
											}
										}
									}
								}
								else if (array206[0].StartsWith("p") && int.TryParse(array206[0].Substring(1), out num207))
								{
									loadPose[i] = array206[0];
								}
								else if (!array206[0].StartsWith("dance_"))
								{
									maidArray[i].CrossFade(array206[0] + ".anm", false, true, false, 0f, 1f);
								}
								else
								{
									if (!maidArray[i].body0.m_Bones.GetComponent<Animation>().GetClip(array206[0] + ".anm"))
									{
										maidArray[i].body0.LoadAnime(array206[0] + ".anm", GameUty.FileSystem, array206[0] + ".anm", false, false);
									}
									maidArray[i].body0.m_Bones.GetComponent<Animation>().Play(array206[0] + ".anm");
								}
								if (array206.Length > 1)
								{
									maidArray[i].body0.m_Bones.GetComponent<Animation>()[array206[0] + ".anm"].time = float.Parse(array206[1]);
									isStop[i] = true;
									if (array206.Length > 2)
									{
										Transform transform29 = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
										isPoseIti[i] = true;
										poseIti[i] = maidArray[i].transform.position;
										maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
									}
								}
								faceIndex[i] = int.Parse(array2[62]);
								if (faceIndex[i] < faceArray.Length)
								{
									maid.FaceAnime(faceArray[faceIndex[i]], 0.01f, 0);
								}
								else
								{
									faceIndex[i] = 0;
								}
								TMorph morph = maid.body0.Face.morph;
								if (!isVR)
								{
									maid.boMabataki = false;
									morph.EyeMabataki = 0f;
									maid.body0.Face.morph.FixBlendValues_Face();
								}
							}
						}
					}
				}
				if (allowUpdate || sceneLevel == 14 || sceneLevel == 24)
				{
					if (isF6 && !cameraObj && !isVR)
					{
						cameraObj = new GameObject("subCamera");
						subcamera = cameraObj.AddComponent<Camera>();
						subcamera.CopyFrom(Camera.main);
						cameraObj.SetActive(true);
						subcamera.clearFlags = CameraClearFlags.Depth;
						subcamera.cullingMask = 256;
						subcamera.depth = 1f;
						subcamera.transform.parent = mainCamera.transform;
						float item = 2f;
						if (Application.unityVersion.StartsWith("4"))
						{
							item = 1f;
						}
						GameObject gameObject6 = new GameObject("Light");
						gameObject6.AddComponent<Light>();
						lightList.Add(gameObject6);
						lightColorR.Add(1f);
						lightColorG.Add(1f);
						lightColorB.Add(1f);
						lightIndex.Add(0);
						lightX.Add(40f);
						lightY.Add(180f);
						lightAkarusa.Add(item);
						lightKage.Add(0.098f);
						lightRange.Add(50f);
						gameObject6.transform.position = GameMain.Instance.MainLight.transform.position;
						gameObject6.GetComponent<Light>().intensity = 2f;
						gameObject6.GetComponent<Light>().spotAngle = 50f;
						gameObject6.GetComponent<Light>().range = 10f;
						gameObject6.GetComponent<Light>().type = LightType.Directional;
						gameObject6.GetComponent<Light>().color = new Color(0.5f, 1f, 0f);
						gameObject6.GetComponent<Light>().cullingMask = 256;
					}
					if (!isF6S || !getModKeyPressing(AdvancedStudioMode.modKey.Shift) || !Input.GetKeyDown(KeyCode.F6))
					{
						if (isF6S || !getModKeyPressing(AdvancedStudioMode.modKey.Shift) || !Input.GetKeyDown(KeyCode.F6))
						{
							if (!isF6 && Input.GetKeyDown(KeyCode.F6) && sceneLevel != 5 && sceneLevel != 3 && !isVR && maidArray[0] && maidArray[0].Visible)
							{
								isF6 = true;
								bGui = true;
								isFaceInit = true;
								isGuiInit = true;
								maidArray[0].boMabataki = false;
								selectMaidIndex = 0;
								maidCnt = 1;
								isFace[0] = true;
								faceFlg = true;
								kankyoFlg = false;
								string text2 = GameMain.Instance.BgMgr.GetBGName();
								int l = 0;
								foreach (string text3 in bgArray)
								{
									if (text3 == text2)
									{
										bgIndex = l;
										bgIndex6 = l;
										break;
									}
									l++;
								}
								bgCombo.selectedItemIndex = bgIndex6;
								lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
								lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
							}
							else if (!isF6 && Input.GetKeyDown(KeyCode.F7) && sceneLevel != 5 && sceneLevel != 3 && !isVR && maidArray[0] && maidArray[0].Visible)
							{
								isF6 = true;
								bGui = true;
								isGuiInit = true;
								selectMaidIndex = 0;
								maidCnt = 1;
								isFace[0] = false;
								faceFlg = false;
								kankyoFlg = true;
								string text2 = GameMain.Instance.BgMgr.GetBGName();
								int l = 0;
								foreach (string text3 in bgArray)
								{
									if (text3 == text2)
									{
										bgIndex = l;
										bgIndex6 = l;
										break;
									}
									l++;
								}
								bgmCombo.selectedItemIndex = bgmIndex;
								bgCombo.selectedItemIndex = bgIndex6;
								lightX[0] = GameMain.Instance.MainLight.transform.eulerAngles.x;
								lightY[0] = GameMain.Instance.MainLight.transform.eulerAngles.y;
								lightX6 = lightX[0];
								lightY6 = lightY[0];
							}
							else if ((isVR && Input.GetKey(KeyCode.F7) && Input.GetKeyDown(KeyCode.BackQuote)) || (isF6 && (Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.F7))))
							{
								isF6 = false;
								bGui = false;
								maidArray[0].boMabataki = true;
								bgIndex = bgIndex6;
								bg.localScale = new Vector3(1f, 1f, 1f);
								if (!isVR)
								{
									Bloom fieldValue7 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
									fieldValue7.enabled = true;
									fieldValue7.bloomIntensity = 2.85f;
									fieldValue7.hdr = 0;
									fieldValue7.bloomThreshholdColor = new Color(1f, 1f, 1f);
									fieldValue7.bloomBlurIterations = 3;
								}
								else if (bgArray[bgIndex].Length == 36)
								{
									GameMain.Instance.BgMgr.ChangeBgMyRoom(bgArray[bgIndex]);
								}
								else
								{
									GameMain.Instance.BgMgr.ChangeBg(bgArray[bgIndex]);
								}
								mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
								maidCnt = 0;
								wearIndex = 0;
								faceFlg = false;
								faceFlg2 = false;
								sceneFlg = false;
								poseFlg = false;
								kankyoFlg = false;
								kankyo2Flg = false;
								unLockFlg = false;
								ikMaid = 0;
								ikBui = 0;
								isNamida = false;
								isTear1 = false;
								isTear2 = false;
								isTear3 = false;
								isShock = false;
								isYodare = false;
								isHoho = false;
								isHoho2 = false;
								isHohos = false;
								isHohol = false;
								isToothoff = false;
								isNosefook = false;
								isFaceInit = false;
								isPoseInit = false;
								isWear = true;
								isSkirt = true;
								isBra = true;
								isPanz = true;
								isHeadset = true;
								isAccUde = true;
								isStkg = true;
								isShoes = true;
								isGlove = true;
								isMegane = true;
								isAccSenaka = true;
								isPanel = true;
								isBloom = false;
								isBloomA = false;
								isBlur = false;
								isBlur2 = false;
								bloom1 = 2.85f;
								bloom2 = 3f;
								bloom3 = 0f;
								bloom4 = 0f;
								bloom5 = 0f;
								blur1 = -3.98f;
								blur4 = -0.75f;
								blur2 = 0.9f;
								blur3 = 4.19f;
								bokashi = 0f;
								kamiyure = 0f;
								isDepth = false;
								isDepthA = false;
								depth1 = 3f;
								depth2 = 0.1f;
								depth3 = 15f;
								depth4 = 2.5f;
								isFog = false;
								fog1 = 4f;
								fog2 = 1f;
								fog3 = 1f;
								fog4 = 0f;
								fog5 = 1f;
								fog6 = 1f;
								fog7 = 1f;
								isSepia = false;
								isBloomS = true;
								isDepthS = false;
								isBlurS = false;
								isFogS = false;
								if (depth_field_ != null)
								{
									depth_field_.enabled = false;
								}
								if (fog_ != null)
								{
									fog_.enabled = false;
								}
								if (sepia_tone_ != null)
								{
									sepia_tone_.enabled = false;
								}
								isCube = false;
								isCube2 = true;
								isCube3 = false;
								isCube4 = true;
								effectIndex = 0;
								selectMaidIndex = 0;
								copyIndex = 0;
								selectLightIndex = 0;
								parIndex = 0;
								isEditNo = 0;
								editSelectMaid = null;
								for (int i2 = 0; i2 < 10; i2++)
								{
									date[i2] = "";
									ninzu[i2] = "";
								}
								if (kami)
								{
									kami.SetActive(false);
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
								lightX.Add(lightX6);
								lightY = new List<float>();
								lightY.Add(lightY6);
								lightAkarusa = new List<float>();
								lightAkarusa.Add(0.95f);
								lightKage = new List<float>();
								lightKage.Add(0.098f);
								lightRange = new List<float>();
								lightRange.Add(50f);
								isIdx1 = false;
								isIdx2 = false;
								isIdx3 = false;
								isIdx4 = false;
								bgObject.SetActive(true);
								GameMain.Instance.MainLight.Reset();
								GameMain.Instance.MainLight.SetIntensity(0.95f);
								GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 30f;
								GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
								GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
								GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
								GameMain.Instance.MainLight.transform.eulerAngles = new Vector3(lightX6, lightY6, GameMain.Instance.MainLight.transform.eulerAngles.z);
								for (int l = 0; l < doguBObject.Count; l++)
								{
									UnityEngine.Object.Destroy(doguBObject[l]);
								}
								doguBObject.Clear();
								parIndex = 0;
								for (int l = 0; l < doguCombo.Length; l++)
								{
									doguCombo[l] = new ComboBox2();
									doguCombo[l].selectedItemIndex = 0;
								}
								parCombo.selectedItemIndex = 0;
								for (int l = 1; l < lightList.Count; l++)
								{
									UnityEngine.Object.Destroy(lightList[l]);
								}
								lightList = new List<GameObject>();
								lightList.Add(GameMain.Instance.MainLight.gameObject);
								lightComboList = new GUIContent[lightList.Count];
								lightComboList[0] = new GUIContent("メイン");
								lightCombo.selectedItemIndex = 0;
								selectLightIndex = 0;
								GameMain.Instance.BgMgr.ChangeBg(bgArray[bgIndex6]);
								bgCombo.selectedItemIndex = bgIndex6;
								bgCombo2.selectedItemIndex = 0;
								itemCombo2.selectedItemIndex = 0;
								myCombo.selectedItemIndex = 0;
								slotCombo.selectedItemIndex = 0;
								sortList.Clear();
								scrollPos = new Vector2(0f, 0f);
								if (!isVR)
								{
									Vignetting component2 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
									component2.mode = 0;
									component2.intensity = -3.98f;
									component2.chromaticAberration = -0.75f;
									component2.axialAberration = 1.18f;
									component2.blurSpread = 4.19f;
									component2.luminanceDependency = 0.494f;
									component2.blurDistance = 1.71f;
									component2.enabled = false;
								}
								doguIndex.Clear();
								doguSelectIndex = 0;
								for (int i2 = 0; i2 < doguObject.Count; i2++)
								{
									if (doguObject[i2] != null)
									{
										UnityEngine.Object.Destroy(doguObject[i2]);
										doguObject[i2] = null;
									}
								}
								doguObject.Clear();
							}
						}
					}
					for (int l = 0; l < maidCnt; l++)
					{
						if (maidArray[l] && maidArray[l].Visible)
						{
							Maid maid = maidArray[l];
							if (isLook[l] != isLookn[l])
							{
								isLookn[l] = isLook[l];
								if (isLook[l])
								{
									maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
									maid.body0.boHeadToCam = true;
									maid.body0.boEyeToCam = true;
									maid.body0.trsLookTarget = null;
								}
								else
								{
									maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
									maid.body0.boHeadToCam = true;
									maid.body0.boEyeToCam = true;
								}
							}
						}
					}
					if (maidArray[selectMaidIndex] && maidArray[selectMaidIndex].Visible)
					{
						if ((!faceFlg && !poseFlg && !sceneFlg && !kankyoFlg && !kankyo2Flg) || sceneFlg || kankyoFlg || kankyo2Flg)
						{
							for (int k = 0; k < maidCnt; k++)
							{
								if (maidArray[k] && !maidArray[k].boMabataki)
								{
									maidArray[k].body0.Face.morph.FixBlendValues_Face();
								}
							}
						}
					}
					if (maidArray[selectMaidIndex] && maidArray[selectMaidIndex].Visible && poseFlg)
					{
						if (isPoseInit)
						{
							if (!isDanceChu)
							{
								Maid maid = maidArray[selectMaidIndex];
								isPoseInit = false;
								if (maid.body0.GetMask(TBody.SlotID.wear) || maid.body0.GetMask(TBody.SlotID.mizugi) || maid.body0.GetMask(TBody.SlotID.onepiece))
								{
									isWear = true;
								}
								else
								{
									isWear = false;
								}
								isSkirt = maid.body0.GetMask(TBody.SlotID.skirt);
								isBra = maid.body0.GetMask(TBody.SlotID.bra);
								isPanz = maid.body0.GetMask(TBody.SlotID.panz);
								isMaid = maid.body0.GetMask(TBody.SlotID.body);
								if (maid.body0.GetMask(TBody.SlotID.headset) || maid.body0.GetMask(TBody.SlotID.accHat) || maid.body0.GetMask(TBody.SlotID.accHead) || maid.body0.GetMask(TBody.SlotID.accKamiSubL) || maid.body0.GetMask(TBody.SlotID.accKamiSubR) || maid.body0.GetMask(TBody.SlotID.accKami_1_) || maid.body0.GetMask(TBody.SlotID.accKami_2_) || maid.body0.GetMask(TBody.SlotID.accKami_3_))
								{
									isHeadset = true;
								}
								else
								{
									isHeadset = false;
								}
								isAccUde = maid.body0.GetMask(TBody.SlotID.accUde);
								isStkg = maid.body0.GetMask(TBody.SlotID.stkg);
								isShoes = maid.body0.GetMask(TBody.SlotID.shoes);
								isGlove = maid.body0.GetMask(TBody.SlotID.glove);
								isMegane = maid.body0.GetMask(TBody.SlotID.megane);
								isAccSenaka = maid.body0.GetMask(TBody.SlotID.accSenaka);
								TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
								float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
								float[] fieldValue5 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
								if (morph.bodyskin.PartsVersion < 120)
								{
									eyeclose = fieldValue5[(int)morph.hash["eyeclose"]];
									eyeclose2 = fieldValue5[(int)morph.hash["eyeclose2"]];
									eyeclose3 = fieldValue5[(int)morph.hash["eyeclose3"]];
									eyeclose6 = fieldValue5[(int)morph.hash["eyeclose6"]];
									eyeclose5 = fieldValue5[(int)morph.hash["eyeclose5"]];
									if (morph.hash["eyeclose7"] != null)
									{
										eyeclose7 = fieldValue5[(int)morph.hash["eyeclose7"]];
										eyeclose8 = fieldValue5[(int)morph.hash["eyeclose8"]];
									}
								}
								else
								{
									int num102 = 0;
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
									{
										num102 = 1;
									}
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
									{
										num102 = 2;
									}
									eyeclose = fieldValue5[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose2 = fieldValue5[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose3 = fieldValue[(int)morph.hash["eyeclose3"]];
									eyeclose6 = fieldValue5[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose5 = fieldValue5[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose8 = fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose7 = fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]];
								}
								hitomih = fieldValue[(int)morph.hash["hitomih"]];
								hitomis = fieldValue[(int)morph.hash["hitomis"]];
								mayuha = fieldValue[(int)morph.hash["mayuha"]];
								mayuup = fieldValue[(int)morph.hash["mayuup"]];
								mayuv = fieldValue[(int)morph.hash["mayuv"]];
								mayuvhalf = fieldValue[(int)morph.hash["mayuvhalf"]];
								moutha = fieldValue[(int)morph.hash["moutha"]];
								mouths = fieldValue[(int)morph.hash["mouths"]];
								mouthdw = fieldValue[(int)morph.hash["mouthdw"]];
								mouthup = fieldValue[(int)morph.hash["mouthup"]];
								tangout = fieldValue[(int)morph.hash["tangout"]];
								tangup = fieldValue[(int)morph.hash["tangup"]];
								eyebig = fieldValue[(int)morph.hash["eyebig"]];
								mayuw = fieldValue[(int)morph.hash["mayuw"]];
								mouthhe = fieldValue[(int)morph.hash["mouthhe"]];
								mouthc = fieldValue[(int)morph.hash["mouthc"]];
								mouthi = fieldValue[(int)morph.hash["mouthi"]];
								mouthuphalf = fieldValue[(int)morph.hash["mouthuphalf"]];
								try
								{
									tangopen = fieldValue[(int)morph.hash["tangopen"]];
								}
								catch
								{
								}
								if (fieldValue[(int)morph.hash["namida"]] > 0f)
								{
									isNamida = true;
								}
								else
								{
									isNamida = false;
								}
								if (fieldValue[(int)morph.hash["tear1"]] > 0f)
								{
									isTear1 = true;
								}
								else
								{
									isTear1 = false;
								}
								if (fieldValue[(int)morph.hash["tear2"]] > 0f)
								{
									isTear2 = true;
								}
								else
								{
									isTear2 = false;
								}
								if (fieldValue[(int)morph.hash["tear3"]] > 0f)
								{
									isTear3 = true;
								}
								else
								{
									isTear3 = false;
								}
								if (fieldValue[(int)morph.hash["shock"]] > 0f)
								{
									isShock = true;
								}
								else
								{
									isShock = false;
								}
								if (fieldValue[(int)morph.hash["yodare"]] > 0f)
								{
									isYodare = true;
								}
								else
								{
									isYodare = false;
								}
								if (fieldValue[(int)morph.hash["hoho"]] > 0f)
								{
									isHoho = true;
								}
								else
								{
									isHoho = false;
								}
								if (fieldValue[(int)morph.hash["hoho2"]] > 0f)
								{
									isHoho2 = true;
								}
								else
								{
									isHoho2 = false;
								}
								if (fieldValue[(int)morph.hash["hohos"]] > 0f)
								{
									isHohos = true;
								}
								else
								{
									isHohos = false;
								}
								if (fieldValue[(int)morph.hash["hohol"]] > 0f)
								{
									isHohol = true;
								}
								else
								{
									isHohol = false;
								}
								if (fieldValue[(int)morph.hash["toothoff"]] > 0f)
								{
									isToothoff = true;
								}
								else
								{
									isToothoff = false;
								}
								if (fieldValue[(int)morph.hash["nosefook"]] > 0f)
								{
									isNosefook = true;
								}
								else
								{
									isNosefook = false;
								}
							}
						}
						else
						{
							Maid maid = maidArray[selectMaidIndex];
							if (maid.body0.GetMask(TBody.SlotID.wear) != isWear)
							{
								maid.body0.SetMask(TBody.SlotID.wear, isWear);
							}
							if (maid.body0.GetMask(TBody.SlotID.mizugi) != isWear)
							{
								maid.body0.SetMask(TBody.SlotID.mizugi, isWear);
							}
							if (maid.body0.GetMask(TBody.SlotID.onepiece) != isWear)
							{
								maid.body0.SetMask(TBody.SlotID.onepiece, isWear);
							}
							if (maid.body0.GetMask(TBody.SlotID.skirt) != isSkirt)
							{
								maid.body0.SetMask(TBody.SlotID.skirt, isSkirt);
							}
							if (maid.body0.GetMask(TBody.SlotID.bra) != isBra)
							{
								maid.body0.SetMask(TBody.SlotID.bra, isBra);
							}
							if (maid.body0.GetMask(TBody.SlotID.panz) != isPanz)
							{
								maid.body0.SetMask(TBody.SlotID.panz, isPanz);
							}
							if (maid.body0.GetMask(TBody.SlotID.headset) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.headset, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accHat) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accHat, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accHead) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accHead, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accKamiSubL) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accKamiSubL, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accKamiSubR) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accKamiSubR, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accKami_1_) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accKami_1_, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accKami_2_) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accKami_2_, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accKami_3_) != isHeadset)
							{
								maid.body0.SetMask(TBody.SlotID.accKami_3_, isHeadset);
							}
							if (maid.body0.GetMask(TBody.SlotID.accUde) != isAccUde)
							{
								maid.body0.SetMask(TBody.SlotID.accUde, isAccUde);
							}
							if (maid.body0.GetMask(TBody.SlotID.stkg) != isStkg)
							{
								maid.body0.SetMask(TBody.SlotID.stkg, isStkg);
							}
							if (maid.body0.GetMask(TBody.SlotID.shoes) != isShoes)
							{
								maid.body0.SetMask(TBody.SlotID.shoes, isShoes);
							}
							if (maid.body0.GetMask(TBody.SlotID.glove) != isGlove)
							{
								maid.body0.SetMask(TBody.SlotID.glove, isGlove);
							}
							if (maid.body0.GetMask(TBody.SlotID.megane) != isMegane)
							{
								maid.body0.SetMask(TBody.SlotID.megane, isMegane);
							}
							if (maid.body0.GetMask(TBody.SlotID.accSenaka) != isAccSenaka)
							{
								maid.body0.SetMask(TBody.SlotID.accSenaka, isAccSenaka);
							}
							if (mekure1[selectMaidIndex] != mekure1n[selectMaidIndex])
							{
								mekure1n[selectMaidIndex] = mekure1[selectMaidIndex];
								if (mekure1[selectMaidIndex])
								{
									maid.ItemChangeTemp("skirt", "めくれスカート");
									maid.ItemChangeTemp("onepiece", "めくれスカート");
									mekure2[selectMaidIndex] = false;
									mekure2n[selectMaidIndex] = false;
								}
								else
								{
									ResetProp(maid, MPN.skirt);
									ResetProp(maid, MPN.onepiece);
								}
								maid.AllProcPropSeqStart();
							}
							if (mekure2[selectMaidIndex] != mekure2n[selectMaidIndex])
							{
								mekure2n[selectMaidIndex] = mekure2[selectMaidIndex];
								if (mekure2[selectMaidIndex])
								{
									maid.ItemChangeTemp("skirt", "めくれスカート後ろ");
									maid.ItemChangeTemp("onepiece", "めくれスカート後ろ");
									mekure1[selectMaidIndex] = false;
									mekure1n[selectMaidIndex] = false;
								}
								else
								{
									ResetProp(maid, MPN.skirt);
									ResetProp(maid, MPN.onepiece);
								}
								maid.AllProcPropSeqStart();
							}
							if (zurasi[selectMaidIndex] != zurasin[selectMaidIndex])
							{
								zurasin[selectMaidIndex] = zurasi[selectMaidIndex];
								if (zurasi[selectMaidIndex])
								{
									maid.ItemChangeTemp("panz", "パンツずらし");
									maid.ItemChangeTemp("mizugi", "パンツずらし");
								}
								else
								{
									ResetProp(maid, MPN.panz);
									ResetProp(maid, MPN.mizugi);
								}
								maid.AllProcPropSeqStart();
							}
							if (!isDanceChu)
							{
								if (maid.body0.GetMask(TBody.SlotID.body) != isMaid)
								{
									Hashtable fieldValue4 = AdvancedStudioMode.GetFieldValue<TBody, Hashtable>(maid.body0, "m_hFoceHide");
									fieldValue4[TBody.SlotID.body] = isMaid;
									fieldValue4[TBody.SlotID.head] = isMaid;
									fieldValue4[TBody.SlotID.eye] = isMaid;
									fieldValue4[TBody.SlotID.hairF] = isMaid;
									fieldValue4[TBody.SlotID.hairR] = isMaid;
									fieldValue4[TBody.SlotID.hairS] = isMaid;
									fieldValue4[TBody.SlotID.hairT] = isMaid;
									fieldValue4[TBody.SlotID.hairAho] = isMaid;
									fieldValue4[TBody.SlotID.chikubi] = isMaid;
									fieldValue4[TBody.SlotID.underhair] = isMaid;
									fieldValue4[TBody.SlotID.moza] = isMaid;
									if (maid.body0.goSlot[19].m_strModelFileName.Contains("melala_body"))
									{
										fieldValue4[TBody.SlotID.accHana] = isMaid;
									}
									maid.body0.FixMaskFlag();
									maid.body0.FixVisibleFlag(false);
								}
								if (isLook[selectMaidIndex] != isLookn[selectMaidIndex])
								{
									isLookn[selectMaidIndex] = isLook[selectMaidIndex];
									if (isLook[selectMaidIndex])
									{
										maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
										maid.body0.boHeadToCam = true;
										maid.body0.boEyeToCam = true;
										maid.body0.trsLookTarget = null;
									}
									else
									{
										maid.body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
										maid.body0.boHeadToCam = true;
										maid.body0.boEyeToCam = true;
									}
								}
								if (isLook[selectMaidIndex])
								{
									if (maid.body0.offsetLookTarget.x != lookY[selectMaidIndex])
									{
										if (isLock[selectMaidIndex] && lookY[selectMaidIndex] < 0f)
										{
											maid.body0.offsetLookTarget = new Vector3(lookY[selectMaidIndex] * 0.6f, 1f, lookX[selectMaidIndex]);
										}
										else
										{
											maid.body0.offsetLookTarget = new Vector3(lookY[selectMaidIndex], 1f, lookX[selectMaidIndex]);
										}
									}
									if (lookX[selectMaidIndex] != lookXn[selectMaidIndex])
									{
										lookXn[selectMaidIndex] = lookX[selectMaidIndex];
										maid.body0.offsetLookTarget = new Vector3(lookY[selectMaidIndex], 1f, lookX[selectMaidIndex]);
									}
									if (lookY[selectMaidIndex] != lookYn[selectMaidIndex])
									{
										lookYn[selectMaidIndex] = lookY[selectMaidIndex];
										if (isLock[selectMaidIndex] && lookY[selectMaidIndex] < 0f)
										{
											maid.body0.offsetLookTarget = new Vector3(lookY[selectMaidIndex] * 0.6f, 1f, lookX[selectMaidIndex]);
										}
										else
										{
											maid.body0.offsetLookTarget = new Vector3(lookY[selectMaidIndex], 1f, lookX[selectMaidIndex]);
										}
									}
								}
								if (isHanten)
								{
									isHanten = false;
									SetHanten(maid, selectMaidIndex);
								}
								if (hanten[selectMaidIndex] != hantenn[selectMaidIndex])
								{
									hantenn[selectMaidIndex] = hanten[selectMaidIndex];
									isStop[selectMaidIndex] = true;
									isLock[selectMaidIndex] = true;
									isHanten = true;
								}
								if (voice1[selectMaidIndex] != voice1n[selectMaidIndex])
								{
									voice1n[selectMaidIndex] = voice1[selectMaidIndex];
									if (voice1[selectMaidIndex])
									{
										zFlg[selectMaidIndex] = true;
										xFlg[selectMaidIndex] = false;
										voice2[selectMaidIndex] = false;
										voice2n[selectMaidIndex] = false;
									}
									else
									{
										zFlg[selectMaidIndex] = false;
									}
								}
								if (voice2[selectMaidIndex] != voice2n[selectMaidIndex])
								{
									voice2n[selectMaidIndex] = voice2[selectMaidIndex];
									if (voice2[selectMaidIndex])
									{
										xFlg[selectMaidIndex] = true;
										zFlg[selectMaidIndex] = false;
										voice1[selectMaidIndex] = false;
										voice1n[selectMaidIndex] = false;
									}
									else
									{
										xFlg[selectMaidIndex] = false;
									}
								}
								for (int k = 0; k < maidCnt; k++)
								{
									if (!maidArray[k].boMabataki)
									{
										maidArray[k].body0.Face.morph.FixBlendValues_Face();
									}
								}
							}
						}
					}
					if (maidArray[selectMaidIndex] && maidArray[selectMaidIndex].Visible && (isF6 || (okFlg && faceFlg)))
					{
						if (isFaceInit)
						{
							if (!isDanceChu)
							{
								TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
								float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
								float[] fieldValue5 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
								Maid maid = maidArray[selectMaidIndex];
								maidArray[selectMaidIndex].boMabataki = false;
								morph.EyeMabataki = 0f;
								isFaceInit = false;
								maidArray[selectMaidIndex].body0.Face.morph.FixBlendValues_Face();
								if (morph.bodyskin.PartsVersion < 120)
								{
									eyeclose = fieldValue5[(int)morph.hash["eyeclose"]];
									eyeclose2 = fieldValue5[(int)morph.hash["eyeclose2"]];
									eyeclose3 = fieldValue5[(int)morph.hash["eyeclose3"]];
									eyeclose6 = fieldValue5[(int)morph.hash["eyeclose6"]];
									eyeclose5 = fieldValue5[(int)morph.hash["eyeclose5"]];
									if (morph.hash["eyeclose7"] != null)
									{
										eyeclose7 = fieldValue5[(int)morph.hash["eyeclose7"]];
										eyeclose8 = fieldValue5[(int)morph.hash["eyeclose8"]];
									}
								}
								else
								{
									int num102 = 0;
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
									{
										num102 = 1;
									}
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
									{
										num102 = 2;
									}
									eyeclose = fieldValue5[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose2 = fieldValue5[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose3 = fieldValue[(int)morph.hash["eyeclose3"]];
									eyeclose6 = fieldValue5[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose5 = fieldValue5[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose8 = fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]];
									eyeclose7 = fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]];
								}
								hitomih = fieldValue[(int)morph.hash["hitomih"]];
								hitomis = fieldValue[(int)morph.hash["hitomis"]];
								mayuha = fieldValue[(int)morph.hash["mayuha"]];
								mayuup = fieldValue[(int)morph.hash["mayuup"]];
								mayuv = fieldValue[(int)morph.hash["mayuv"]];
								mayuvhalf = fieldValue[(int)morph.hash["mayuvhalf"]];
								moutha = fieldValue[(int)morph.hash["moutha"]];
								mouths = fieldValue[(int)morph.hash["mouths"]];
								mouthdw = fieldValue[(int)morph.hash["mouthdw"]];
								mouthup = fieldValue[(int)morph.hash["mouthup"]];
								tangout = fieldValue[(int)morph.hash["tangout"]];
								tangup = fieldValue[(int)morph.hash["tangup"]];
								eyebig = fieldValue[(int)morph.hash["eyebig"]];
								mayuw = fieldValue[(int)morph.hash["mayuw"]];
								mouthhe = fieldValue[(int)morph.hash["mouthhe"]];
								mouthc = fieldValue[(int)morph.hash["mouthc"]];
								mouthi = fieldValue[(int)morph.hash["mouthi"]];
								mouthuphalf = fieldValue[(int)morph.hash["mouthuphalf"]];
								try
								{
									tangopen = fieldValue[(int)morph.hash["tangopen"]];
								}
								catch
								{
								}
								if (maid.body0.GetMask(TBody.SlotID.wear) || maid.body0.GetMask(TBody.SlotID.mizugi) || maid.body0.GetMask(TBody.SlotID.onepiece))
								{
									isWear = true;
								}
								else
								{
									isWear = false;
								}
								isSkirt = maid.body0.GetMask(TBody.SlotID.skirt);
								isBra = maid.body0.GetMask(TBody.SlotID.bra);
								isPanz = maid.body0.GetMask(TBody.SlotID.panz);
								if (maid.body0.GetMask(TBody.SlotID.headset) || maid.body0.GetMask(TBody.SlotID.accHat) || maid.body0.GetMask(TBody.SlotID.accHead) || maid.body0.GetMask(TBody.SlotID.accKamiSubL) || maid.body0.GetMask(TBody.SlotID.accKamiSubR) || maid.body0.GetMask(TBody.SlotID.accKami_1_) || maid.body0.GetMask(TBody.SlotID.accKami_2_) || maid.body0.GetMask(TBody.SlotID.accKami_3_))
								{
									isHeadset = true;
								}
								else
								{
									isHeadset = false;
								}
								isAccUde = maid.body0.GetMask(TBody.SlotID.accUde);
								isStkg = maid.body0.GetMask(TBody.SlotID.stkg);
								isShoes = maid.body0.GetMask(TBody.SlotID.shoes);
								isGlove = maid.body0.GetMask(TBody.SlotID.glove);
								isMegane = maid.body0.GetMask(TBody.SlotID.megane);
								isAccSenaka = maid.body0.GetMask(TBody.SlotID.accSenaka);
								if (fieldValue[(int)morph.hash["namida"]] > 0f)
								{
									isNamida = true;
								}
								else
								{
									isNamida = false;
								}
								if (fieldValue[(int)morph.hash["tear1"]] > 0f)
								{
									isTear1 = true;
								}
								else
								{
									isTear1 = false;
								}
								if (fieldValue[(int)morph.hash["tear2"]] > 0f)
								{
									isTear2 = true;
								}
								else
								{
									isTear2 = false;
								}
								if (fieldValue[(int)morph.hash["tear3"]] > 0f)
								{
									isTear3 = true;
								}
								else
								{
									isTear3 = false;
								}
								if (fieldValue[(int)morph.hash["shock"]] > 0f)
								{
									isShock = true;
								}
								else
								{
									isShock = false;
								}
								if (fieldValue[(int)morph.hash["yodare"]] > 0f)
								{
									isYodare = true;
								}
								else
								{
									isYodare = false;
								}
								if (fieldValue[(int)morph.hash["hoho"]] > 0f)
								{
									isHoho = true;
								}
								else
								{
									isHoho = false;
								}
								if (fieldValue[(int)morph.hash["hoho2"]] > 0f)
								{
									isHoho2 = true;
								}
								else
								{
									isHoho2 = false;
								}
								if (fieldValue[(int)morph.hash["hohos"]] > 0f)
								{
									isHohos = true;
								}
								else
								{
									isHohos = false;
								}
								if (fieldValue[(int)morph.hash["hohol"]] > 0f)
								{
									isHohol = true;
								}
								else
								{
									isHohol = false;
								}
								if (fieldValue[(int)morph.hash["toothoff"]] > 0f)
								{
									isToothoff = true;
								}
								else
								{
									isToothoff = false;
								}
								if (fieldValue[(int)morph.hash["nosefook"]] > 0f)
								{
									isNosefook = true;
								}
								else
								{
									isNosefook = false;
								}
							}
						}
						else
						{
							Maid maid = maidArray[selectMaidIndex];
							if (!yotogiFlg && sceneLevel != 5 && sceneLevel != 3)
							{
								if (maid.body0.GetMask(TBody.SlotID.wear) != isWear)
								{
									maid.body0.SetMask(TBody.SlotID.wear, isWear);
								}
								if (maid.body0.GetMask(TBody.SlotID.mizugi) != isWear)
								{
									maid.body0.SetMask(TBody.SlotID.mizugi, isWear);
								}
								if (maid.body0.GetMask(TBody.SlotID.onepiece) != isWear)
								{
									maid.body0.SetMask(TBody.SlotID.onepiece, isWear);
								}
								if (maid.body0.GetMask(TBody.SlotID.skirt) != isSkirt)
								{
									maid.body0.SetMask(TBody.SlotID.skirt, isSkirt);
								}
								if (maid.body0.GetMask(TBody.SlotID.bra) != isBra)
								{
									maid.body0.SetMask(TBody.SlotID.bra, isBra);
								}
								if (maid.body0.GetMask(TBody.SlotID.panz) != isPanz)
								{
									maid.body0.SetMask(TBody.SlotID.panz, isPanz);
								}
								if (maid.body0.GetMask(TBody.SlotID.headset) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.headset, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accHat) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accHat, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accHead) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accHead, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accKamiSubL) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accKamiSubL, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accKamiSubR) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accKamiSubR, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accKami_1_) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accKami_1_, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accKami_2_) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accKami_2_, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accKami_3_) != isHeadset)
								{
									maid.body0.SetMask(TBody.SlotID.accKami_3_, isHeadset);
								}
								if (maid.body0.GetMask(TBody.SlotID.accUde) != isAccUde)
								{
									maid.body0.SetMask(TBody.SlotID.accUde, isAccUde);
								}
								if (maid.body0.GetMask(TBody.SlotID.stkg) != isStkg)
								{
									maid.body0.SetMask(TBody.SlotID.stkg, isStkg);
								}
								if (maid.body0.GetMask(TBody.SlotID.shoes) != isShoes)
								{
									maid.body0.SetMask(TBody.SlotID.shoes, isShoes);
								}
								if (maid.body0.GetMask(TBody.SlotID.glove) != isGlove)
								{
									maid.body0.SetMask(TBody.SlotID.glove, isGlove);
								}
								if (maid.body0.GetMask(TBody.SlotID.megane) != isMegane)
								{
									maid.body0.SetMask(TBody.SlotID.megane, isMegane);
								}
								if (maid.body0.GetMask(TBody.SlotID.accSenaka) != isAccSenaka)
								{
									maid.body0.SetMask(TBody.SlotID.accSenaka, isAccSenaka);
								}
								if (mekure1[selectMaidIndex] != mekure1n[selectMaidIndex])
								{
									mekure1n[selectMaidIndex] = mekure1[selectMaidIndex];
									if (mekure1[selectMaidIndex])
									{
										maid.ItemChangeTemp("skirt", "めくれスカート");
										maid.ItemChangeTemp("onepiece", "めくれスカート");
										mekure2[selectMaidIndex] = false;
										mekure2n[selectMaidIndex] = false;
									}
									else
									{
										ResetProp(maid, MPN.skirt);
										ResetProp(maid, MPN.onepiece);
									}
									maid.AllProcPropSeqStart();
								}
								if (mekure2[selectMaidIndex] != mekure2n[selectMaidIndex])
								{
									mekure2n[selectMaidIndex] = mekure2[selectMaidIndex];
									if (mekure2[selectMaidIndex])
									{
										maid.ItemChangeTemp("skirt", "めくれスカート後ろ");
										maid.ItemChangeTemp("onepiece", "めくれスカート後ろ");
										mekure1[selectMaidIndex] = false;
										mekure1n[selectMaidIndex] = false;
									}
									else
									{
										ResetProp(maid, MPN.skirt);
										ResetProp(maid, MPN.onepiece);
									}
									maid.AllProcPropSeqStart();
								}
								if (zurasi[selectMaidIndex] != zurasin[selectMaidIndex])
								{
									zurasin[selectMaidIndex] = zurasi[selectMaidIndex];
									if (zurasi[selectMaidIndex])
									{
										maid.ItemChangeTemp("panz", "パンツずらし");
										maid.ItemChangeTemp("mizugi", "パンツずらし");
									}
									else
									{
										ResetProp(maid, MPN.panz);
										ResetProp(maid, MPN.mizugi);
									}
									maid.AllProcPropSeqStart();
								}
							}
							if (!isDanceChu)
							{
								TMorph morph = maidArray[selectMaidIndex].body0.Face.morph;
								float[] fieldValue = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValues");
								float[] fieldValue5 = AdvancedStudioMode.GetFieldValue<TMorph, float[]>(morph, "BlendValuesBackup");
								if (morph.bodyskin.PartsVersion < 120)
								{
									fieldValue5[(int)morph.hash["eyeclose"]] = eyeclose;
									fieldValue5[(int)morph.hash["eyeclose2"]] = eyeclose2;
									if (eyeclose3 > 1f)
									{
										eyeclose3 = 1f;
									}
									fieldValue5[(int)morph.hash["eyeclose3"]] = eyeclose3;
									fieldValue5[(int)morph.hash["eyeclose6"]] = eyeclose6;
									fieldValue5[(int)morph.hash["eyeclose5"]] = eyeclose5;
									if (morph.hash["eyeclose7"] != null)
									{
										fieldValue5[(int)morph.hash["eyeclose7"]] = eyeclose7;
										fieldValue5[(int)morph.hash["eyeclose8"]] = eyeclose8;
									}
								}
								else
								{
									int num102 = 0;
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TARE)
									{
										num102 = 1;
									}
									if (morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.TSURI || morph.GetFaceTypeGP01FB() == TMorph.GP01FB_FACE_TYPE.MAX)
									{
										num102 = 2;
									}
									fieldValue5[(int)morph.hash["eyeclose1" + TMorph.crcFaceTypesStr[num102]]] = eyeclose;
									fieldValue5[(int)morph.hash["eyeclose2" + TMorph.crcFaceTypesStr[num102]]] = eyeclose2;
									fieldValue5[(int)morph.hash["eyeclose5" + TMorph.crcFaceTypesStr[num102]]] = eyeclose5;
									fieldValue5[(int)morph.hash["eyeclose6" + TMorph.crcFaceTypesStr[num102]]] = eyeclose6;
									fieldValue5[(int)morph.hash["eyeclose7" + TMorph.crcFaceTypesStr[num102]]] = eyeclose7;
									fieldValue5[(int)morph.hash["eyeclose8" + TMorph.crcFaceTypesStr[num102]]] = eyeclose8;
									fieldValue[(int)morph.hash["eyeclose3"]] = eyeclose3;
								}
								fieldValue[(int)morph.hash["hitomih"]] = hitomih;
								fieldValue[(int)morph.hash["hitomis"]] = hitomis;
								fieldValue[(int)morph.hash["mayuha"]] = mayuha;
								fieldValue[(int)morph.hash["mayuup"]] = mayuup;
								fieldValue[(int)morph.hash["mayuv"]] = mayuv;
								fieldValue[(int)morph.hash["mayuvhalf"]] = mayuvhalf;
								fieldValue[(int)morph.hash["tangout"]] = tangout;
								fieldValue[(int)morph.hash["tangup"]] = tangup;
								if (morph.bodyskin.PartsVersion < 120)
								{
									if (eyebig > 1f)
									{
										eyebig = 1f;
									}
								}
								fieldValue[(int)morph.hash["eyebig"]] = eyebig;
								fieldValue[(int)morph.hash["mayuw"]] = mayuw;
								try
								{
									fieldValue[(int)morph.hash["tangopen"]] = tangopen;
								}
								catch
								{
								}
								if (!isDanceChu)
								{
									fieldValue[(int)morph.hash["moutha"]] = moutha;
									fieldValue[(int)morph.hash["mouths"]] = mouths;
									fieldValue[(int)morph.hash["mouthdw"]] = mouthdw;
									fieldValue[(int)morph.hash["mouthup"]] = mouthup;
									fieldValue[(int)morph.hash["mouthhe"]] = mouthhe;
									fieldValue[(int)morph.hash["mouthc"]] = mouthc;
									fieldValue[(int)morph.hash["mouthi"]] = mouthi;
									fieldValue[(int)morph.hash["mouthuphalf"]] = mouthuphalf;
								}
								if (isNamida)
								{
									fieldValue[(int)morph.hash["namida"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["namida"]] = 0f;
								}
								if (isTear1)
								{
									fieldValue[(int)morph.hash["tear1"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["tear1"]] = 0f;
								}
								if (isTear2)
								{
									fieldValue[(int)morph.hash["tear2"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["tear2"]] = 0f;
								}
								if (isTear3)
								{
									fieldValue[(int)morph.hash["tear3"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["tear3"]] = 0f;
								}
								if (isShock)
								{
									fieldValue[(int)morph.hash["shock"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["shock"]] = 0f;
								}
								if (isYodare)
								{
									fieldValue[(int)morph.hash["yodare"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["yodare"]] = 0f;
								}
								if (isHoho)
								{
									fieldValue[(int)morph.hash["hoho"]] = 0.5f;
								}
								else
								{
									fieldValue[(int)morph.hash["hoho"]] = 0f;
								}
								if (isHoho2)
								{
									fieldValue[(int)morph.hash["hoho2"]] = 0.5f;
								}
								else
								{
									fieldValue[(int)morph.hash["hoho2"]] = 0f;
								}
								if (isHohos)
								{
									fieldValue[(int)morph.hash["hohos"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["hohos"]] = 0f;
								}
								if (isHohol)
								{
									fieldValue[(int)morph.hash["hohol"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["hohol"]] = 0f;
								}
								if (isToothoff)
								{
									fieldValue[(int)morph.hash["toothoff"]] = 1f;
								}
								else
								{
									fieldValue[(int)morph.hash["toothoff"]] = 0f;
								}
								if (isNosefook)
								{
									morph.boNoseFook = true;
								}
								else
								{
									morph.boNoseFook = false;
								}
								for (int k = 0; k < maidCnt; k++)
								{
									maidArray[k].body0.Face.morph.FixBlendValues_Face();
								}
							}
						}
					}
					if (isF6 && !okFlg && ((!escFlg && Input.GetKeyDown(KeyCode.Escape)) || Input.GetKeyDown(KeyCode.Tab)))
					{
						bGui = !bGui;
					}
					if (isF6 && maidArray[0] != null && maidArray[0].Visible)
					{
						int l;
						for (l = 0; l < 999; l++)
						{
							if (gDogu[l] != null)
							{
								gDogu[l].GetComponent<Renderer>().enabled = false;
								gDogu[l].SetActive(false);
								if (mDogu[l].del)
								{
									mDogu[l].del = false;
									UnityEngine.Object.Destroy(doguBObject[l]);
									doguBObject.RemoveAt(l);
								}
								else if (mDogu[l].copy)
								{
									mDogu[l].copy = false;
									GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(doguBObject[l]);
									gameObject3.transform.Translate(-0.3f, 0f, 0f);
									doguBObject.Add(gameObject3);
									gameObject3.name = doguBObject[l].name;
									doguCnt = doguBObject.Count - 1;
									gDogu[doguCnt] = GameObject.CreatePrimitive(PrimitiveType.Cube);
									gDogu[doguCnt].GetComponent<Renderer>().material = m_material;
									gDogu[doguCnt].layer = 8;
									gDogu[doguCnt].GetComponent<Renderer>().enabled = false;
									gDogu[doguCnt].SetActive(false);
									gDogu[doguCnt].transform.position = gameObject3.transform.position;
									mDogu[doguCnt] = gDogu[doguCnt].AddComponent<MouseDrag6>();
									mDogu[doguCnt].isScale = false;
									mDogu[doguCnt].obj = gDogu[doguCnt];
									mDogu[doguCnt].maid = gameObject3;
									mDogu[doguCnt].angles = gameObject3.transform.eulerAngles;
									gDogu[doguCnt].transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
									mDogu[doguCnt].ido = 1;
								}
								else if (mDogu[l].count > 0)
								{
									mDogu[l].count--;
									if (doguBObject.Count > l && doguBObject[l] != null && doguBObject[l].name.StartsWith("Particle/p"))
									{
										if (mDogu[l].count == 1)
										{
											doguBObject[l].SetActive(false);
										}
										if (mDogu[l].count == 0)
										{
											doguBObject[l].SetActive(true);
											string text19 = doguBObject[l].name;
											if (text19 != null)
											{
												if (!(text19 == "Particle/pLineY"))
												{
													if (!(text19 == "Particle/pLineP02"))
													{
														if (!(text19 == "Particle/pLine_act2"))
														{
															if (text19 == "Particle/pHeart01")
															{
																mDogu[l].count = 77;
															}
														}
														else
														{
															mDogu[l].count = 90;
														}
													}
													else
													{
														mDogu[l].count = 115;
													}
												}
												else
												{
													mDogu[l].count = 180;
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
						l = 0;
						while (l < lightIndex.Count)
						{
							if (gLight[0] == null)
							{
								gLight[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
								Material material2 = new Material(Shader.Find("Transparent/Diffuse"));
								material2.color = new Color(0.5f, 0.5f, 1f, 0.8f);
								gLight[0].GetComponent<Renderer>().material = material2;
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
							if (gLight[l] != null)
							{
								if (!isCube4)
								{
									gLight[l].GetComponent<Renderer>().enabled = false;
									gLight[l].SetActive(false);
								}
								else if (lightList[l].GetComponent<Light>().type == LightType.Spot || lightList[l].GetComponent<Light>().type == LightType.Point)
								{
									if (ikMode2 > 0 && ikMode2 != 15)
									{
										gLight[l].GetComponent<Renderer>().enabled = true;
										gLight[l].SetActive(true);
									}
									else
									{
										gLight[l].GetComponent<Renderer>().enabled = false;
										gLight[l].SetActive(false);
										mLight[l].isAlt = false;
									}
									if (ikMode2 == 10 || ikMode2 == 11 || ikMode2 == 12)
									{
										gLight[l].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
										if (mLight[l].isAlt)
										{
											gLight[l].GetComponent<Renderer>().material.color = new Color(0.6f, 0.6f, 1f, 0.5f);
										}
									}
									if (ikMode2 == 9 || ikMode2 == 14)
									{
										gLight[l].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f, 0.5f);
										mLight[l].Update();
									}
									if (ikMode2 == 13)
									{
										gLight[l].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f, 0.5f);
										mLight[l].Update();
									}
									if (ikMode2 == 13)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 13 && gLight[l])
										{
											mLight[l].ido = 15;
											mLight[l].reset = true;
										}
										else
										{
											if (lightList[l].transform.localScale.x == 1f)
											{
												lightList[l].transform.localScale = new Vector3(lightRange[l], lightRange[l], lightRange[l]);
											}
											lightRange[l] = lightList[l].transform.localScale.x;
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 15;
										}
									}
									else if (ikMode2 == 11)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 11 && gLight[l])
										{
											mLight[l].ido = 3;
											mLight[l].reset = true;
										}
										else
										{
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											lightX[l] = gLight[l].transform.eulerAngles.x;
											lightY[l] = gLight[l].transform.eulerAngles.y;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 3;
										}
									}
									else if (ikMode2 == 12)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 12 && gLight[l])
										{
											mLight[l].ido = 2;
											mLight[l].reset = true;
										}
										else
										{
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 2;
										}
									}
									else if (ikMode2 == 10)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 10 && gLight[l])
										{
											mLight[l].ido = 1;
											mLight[l].reset = true;
										}
										else
										{
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 1;
										}
									}
									else if (ikMode2 == 9)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 9 && gLight[l])
										{
											mLight[l].ido = 4;
											mLight[l].reset = true;
										}
										else
										{
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											lightX[l] = gLight[l].transform.eulerAngles.x;
											lightY[l] = gLight[l].transform.eulerAngles.y;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 4;
										}
									}
									else if (ikMode2 == 14)
									{
										if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 14 && gLight[l])
										{
											mLight[l].ido = 6;
											mLight[l].reset = true;
										}
										else
										{
											gLight[l].transform.position = lightList[l].transform.position;
											gLight[l].transform.eulerAngles = lightList[l].transform.eulerAngles;
											lightX[l] = gLight[l].transform.eulerAngles.x;
											lightY[l] = gLight[l].transform.eulerAngles.y;
											mLight[l].maid = lightList[l];
											mLight[l].ido = 6;
										}
									}
								}
							}
							//IL_159C0:
							l++;
							continue;
							//goto IL_159C0;
						}
						for (l = 0; l < doguBObject.Count; l++)
						{
							if (!isCube2)
							{
								gDogu[l].GetComponent<Renderer>().enabled = false;
								gDogu[l].SetActive(false);
							}
							else
							{
								if (ikMode2 > 0)
								{
									gDogu[l].GetComponent<Renderer>().enabled = true;
									gDogu[l].SetActive(true);
								}
								else
								{
									gDogu[l].GetComponent<Renderer>().enabled = false;
									gDogu[l].SetActive(false);
									mDogu[l].isAlt = false;
								}
								if (ikMode2 == 10 || ikMode2 == 11 || ikMode2 == 12)
								{
									gDogu[l].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f, 0.5f);
									if (mDogu[l].isAlt)
									{
										gDogu[l].GetComponent<Renderer>().material.color = new Color(0.6f, 0.6f, 1f, 0.5f);
									}
								}
								if (ikMode2 == 9 || ikMode2 == 14)
								{
									gDogu[l].GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.3f, 0.5f);
									mDogu[l].Update();
								}
								if (ikMode2 == 15)
								{
									gDogu[l].GetComponent<Renderer>().material.color = new Color(1f, 0.3f, 0.3f, 0.5f);
								}
								if (ikMode2 == 16)
								{
									gDogu[l].GetComponent<Renderer>().material.color = new Color(0.7f, 0.3f, 0.7f, 0.5f);
									mDogu[l].Update();
								}
								if (ikMode2 == 13)
								{
									gDogu[l].GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.3f, 0.5f);
									mDogu[l].Update();
								}
								if (ikMode2 == 13)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 13 && gDogu[l])
									{
										mDogu[l].ido = 5;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 5;
									}
								}
								else if (ikMode2 == 11)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 11 && gDogu[l])
									{
										mDogu[l].ido = 3;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 3;
									}
								}
								else if (ikMode2 == 12)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 12 && gDogu[l])
									{
										mDogu[l].ido = 2;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 2;
									}
								}
								else if (ikMode2 == 10)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 10 && gDogu[l])
									{
										mDogu[l].ido = 1;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].maidArray = doguBObject.ToArray();
										mDogu[l].mArray = mDogu.ToArray<MouseDrag6>();
										mDogu[l].ido = 1;
									}
								}
								else if (ikMode2 == 9)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 9 && gDogu[l])
									{
										mDogu[l].ido = 4;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 4;
									}
								}
								else if (ikMode2 == 14)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 14 && gDogu[l])
									{
										mDogu[l].ido = 6;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 6;
									}
								}
								else if (ikMode2 == 15)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 15 && gDogu[l])
									{
										mDogu[l].ido = 7;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 7;
									}
								}
								else if (ikMode2 == 16)
								{
									if ((ikModeOld2 == 0 || ikModeOld2 >= 9) && ikModeOld2 != 16 && gDogu[l])
									{
										mDogu[l].ido = 8;
										mDogu[l].reset = true;
									}
									else
									{
										gDogu[l].transform.position = doguBObject[l].transform.position;
										gDogu[l].transform.eulerAngles = doguBObject[l].transform.eulerAngles;
										mDogu[l].maid = doguBObject[l];
										mDogu[l].ido = 8;
									}
								}
							}
						}
						ikModeOld2 = ikMode2;
						Vector3 vector2 = mainCameraTransform.TransformDirection(Vector3.forward);
						Vector3 vector3 = mainCameraTransform.TransformDirection(Vector3.right);
						Vector3 vector4 = mainCameraTransform.TransformDirection(Vector3.up);
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
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.I))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * -0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.K))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector2.x, 0f, vector2.z) * 0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.J))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * 0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.L))
						{
							Vector3 vector5 = bg.position;
							vector5 += new Vector3(vector3.x, 0f, vector3.z) * -0.025f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.Alpha0))
						{
							Vector3 vector5 = bg.position;
							vector5.y -= 0.015f * speed;
							bg.localPosition = vector5;
							if (Input.GetKey(KeyCode.Y))
							{
								keyFlg = true;
							}
						}
						else if ((Input.GetKey(KeyCode.Y) || Input.GetKey(KeyCode.V)) && Input.GetKey(KeyCode.P))
						{
							Vector3 vector5 = bg.position;
							vector5.y += 0.015f * speed;
							bg.localPosition = vector5;
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
						else if (Input.GetKeyUp(KeyCode.Y))
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
								}
							}
						}
						if (getModKeyPressing(AdvancedStudioMode.modKey.Shift))
						{
							speed = 5f * Time.deltaTime * 60f;
						}
						else
						{
							speed = 1f * Time.deltaTime * 60f;
						}
						if (!isVR || isVR2)
						{
							if (!isVR)
							{
								if (isBloom)
								{
									Bloom fieldValue7 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
									fieldValue7.enabled = true;
									fieldValue7.bloomIntensity = bloom1;
									fieldValue7.bloomBlurIterations = (int)bloom2;
									fieldValue7.bloomThreshholdColor = new Color(1f - bloom3, 1f - bloom4, 1f - bloom5);
									if (isBloomA)
									{
										fieldValue7.hdr = Bloom.HDRBloomMode.On;
									}
									else
									{
										fieldValue7.hdr = Bloom.HDRBloomMode.Auto;
									}
									isBloom2 = true;
								}
								else if (isBloom2)
								{
									isBloom2 = false;
									Bloom fieldValue7 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
									fieldValue7.enabled = true;
									fieldValue7.bloomIntensity = 2.85f;
									fieldValue7.hdr = 0;
									fieldValue7.bloomThreshholdColor = new Color(1f, 1f, 1f);
									fieldValue7.bloomBlurIterations = 3;
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
								if (bokashi > 0f)
								{
									Blur component4 = GameMain.Instance.MainCamera.gameObject.GetComponent<Blur>();
									component4.enabled = true;
									component4.blurSize = bokashi / 10f;
									component4.blurIterations = 0;
									component4.downsample = 0;
									if (bokashi > 3f)
									{
										component4.blurSize -= 0.3f;
										component4.blurIterations = 1;
										component4.downsample = 1;
									}
								}
								else
								{
									Blur component4 = GameMain.Instance.MainCamera.gameObject.GetComponent<Blur>();
									component4.enabled = false;
								}
								if (kamiyure > 0f)
								{
									for (int i2 = 0; i2 < maidCnt; i2++)
									{
										Maid maid = maidArray[i2];
										for (int j = 0; j < maid.body0.goSlot.Count; j++)
										{
											if (maid.body0.goSlot[j].obj != null)
											{
												DynamicBone component = maid.body0.goSlot[j].obj.GetComponent<DynamicBone>();
												if (component != null && component.enabled)
												{
													component.m_Gravity = new Vector3(softG.x * 5f, (softG.y + 0.003f) * 5f, softG.z * 5f);
												}
											}
											List<THair1> fieldValue6 = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid.body0.goSlot[j].bonehair, "hair1list");
											for (int k = 0; k < fieldValue6.Count; k++)
											{
												fieldValue6[k].SoftG = new Vector3(softG.x, softG.y + kamiyure, softG.z);
											}
										}
									}
								}
								if (isBlur)
								{
									Vignetting component2 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
									component2.mode = 0;
									component2.intensity = blur1;
									component2.chromaticAberration = blur4;
									component2.blur = blur2;
									component2.blurSpread = blur3;
									component2.enabled = true;
									isBlur2 = true;
								}
								else if (isBlur2)
								{
									isBlur2 = false;
									Vignetting component2 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
									component2.mode = 0;
									component2.intensity = -3.98f;
									component2.chromaticAberration = -0.75f;
									component2.axialAberration = 1.18f;
									component2.blurSpread = 4.19f;
									component2.luminanceDependency = 0.494f;
									component2.blurDistance = 1.71f;
									component2.enabled = false;
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
								List<float> list7;
								(list7 = lightColorR)[0] = list7[0] + 0.01f;
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
								List<float> list7;
								(list7 = lightColorG)[0] = list7[0] + 0.01f;
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
								List<float> list7;
								(list7 = lightColorB)[0] = list7[0] + 0.01f;
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
								List<float> list7;
								(list7 = lightColorR)[0] = list7[0] - 0.01f;
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
								List<float> list7;
								(list7 = lightColorG)[0] = list7[0] - 0.01f;
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
								List<float> list7;
								(list7 = lightColorB)[0] = list7[0] - 0.01f;
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
								List<int> list6;
								(list6 = lightIndex)[0] = list6[0] + 1;
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
									if (gLight[0] == null)
									{
										gLight[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
										Material material2 = new Material(Shader.Find("Transparent/Diffuse"));
										material2.color = new Color(0.5f, 0.5f, 1f, 0.8f);
										gLight[0].GetComponent<Renderer>().material = material2;
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
							List<int> list6;
							(list6 = lightIndex)[0] = list6[0] + 1;
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
						for (l = 0; l < lightList.Count; l++)
						{
							if (l > 0)
							{
								lightList[l].GetComponent<Light>().color = new Color(lightColorR[l], lightColorG[l], lightColorB[l]);
								lightList[l].GetComponent<Light>().intensity = lightAkarusa[l];
								lightList[l].GetComponent<Light>().spotAngle = lightRange[l];
								lightList[l].GetComponent<Light>().range = lightRange[l] / 5f;
								if (!Input.GetKey(KeyCode.X) && (!Input.GetKey(KeyCode.Z) || !getModKeyPressing(AdvancedStudioMode.modKey.Shift)))
								{
									lightList[l].transform.eulerAngles = new Vector3(lightX[l], lightY[l], 18f);
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
								GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = lightRange[l];
								GameMain.Instance.MainLight.GetComponent<Light>().range = lightRange[l] / 5f;
								if (lightIndex[l] != 3)
								{
									GameMain.Instance.MainLight.GetComponent<Light>().color = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
								}
								else
								{
									mainCamera.GetComponent<Camera>().backgroundColor = new Color(lightColorR[0], lightColorG[0], lightColorB[0]);
								}
							}
						}
					}
				}
			}
		}
	}
}
