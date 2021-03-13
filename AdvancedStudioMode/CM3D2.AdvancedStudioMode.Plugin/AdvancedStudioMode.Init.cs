using ExIni;
using MyRoomCustom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.PostProcessing;
using wf;

namespace CM3D2.AdvancedStudioMode.Plugin
{
    public partial class AdvancedStudioMode
    {
		public void init2()
		{
			for (int i = 0; i < GameMain.Instance.CharacterMgr.GetStockMaidCount(); i++)
			{
				Maid maid = GameMain.Instance.CharacterMgr.GetStockMaidList()[i];
				if (goSlot[i] != null)
				{
					maid.body0.goSlot = new List<TBodySkin>(goSlot[i]);
					if (!GameMain.Instance.VRMode)
					{
						maid.body0.quaDefEyeL.eulerAngles = eyeL[i];
						maid.body0.quaDefEyeR.eulerAngles = eyeR[i];
					}
					shodaiFlg[i] = false;
				}
				if (SkirtListArray[i] != null)
				{
					for (int j = 0; j < maid.body0.goSlot.Count; j++)
					{
						AdvancedStudioMode.SetFieldValue8<BoneHair3, DynamicSkirtBone>(maid.body0.goSlot[j].bonehair3, "m_SkirtBone", SkirtListArray[i][j]);
					}
				}
			}
			for (int k = 0; k < maxMaidCnt; k++)
			{
				if (maidArray[k])
				{
					maidArray[k].StopKuchipakuPattern();
				}
				if (maidArray[k])
				{
					maidArray[k].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
				}
				if (maidArray[k] && maidArray[k].Visible && maidArray[k].body0.isLoadedBody)
				{
					maidArray[k].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
					maidArray[k].SetAutoTwistAll(true);
					maidArray[k].body0.MuneYureL(1f);
					maidArray[k].body0.MuneYureR(1f);
					maidArray[k].body0.jbMuneL.enabled = true;
					maidArray[k].body0.jbMuneR.enabled = true;
				}
				if (maidArray[k])
				{
					maidArray[k].boMabataki = true;
					if (maidArray[k].body0.isLoadedBody)
					{
						Maid maid2 = maidArray[k];
						for (int j = 0; j < maid2.body0.goSlot.Count; j++)
						{
							List<THair1> fieldValue = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid2.body0.goSlot[j].bonehair, "hair1list");
							for (int i = 0; i < fieldValue.Count; i++)
							{
								fieldValue[i].SoftG = new Vector3(0f, -0.003f, 0f);
							}
						}
					}
				}
			}
			goSlot = new List<TBodySkin>[500];
			bodyHit = new List<TBodyHit>[500];
			allowUpdate = true;
			okFlg = false;
			isDance = false;
			isDanceChu = false;
			isSavePose = false;
			bgIndex = 0;
			bgIndexB = 0;
			bg.localScale = new Vector3(1f, 1f, 1f);
			softG = new Vector3(0f, -0.003f, 0f);
			softG2 = new Vector3(0f, -0.005f, 0f);
			if (!GameMain.Instance.VRMode)
			{
				Bloom fieldValue2 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
				fieldValue2.enabled = true;
				fieldValue2.bloomIntensity = 2.85f;
				fieldValue2.hdr = 0;
				fieldValue2.bloomThreshholdColor = new Color(1f, 1f, 1f);
				fieldValue2.bloomBlurIterations = 3;
			}
			mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
			maidCnt = 1;
			editMaid = 0;
			wearIndex = 0;
			bGuiMessage = false;
			inName = "";
			inName2 = "";
			inName3 = "";
			inText = "";
			fontSize = 25;
			isMessage = false;
			GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
			GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
			MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
			MessageClass messageClass = new MessageClass(gameObject2, messageWindowMgr);
			messageClass.Clear();
			messageWindowMgr.CloseMessageWindowPanel();
			UILabel component = UTY.GetChildObject(gameObject2, "MessageViewer/MsgParent/Message", false).GetComponent<UILabel>();
			AdvancedStudioMode.SetFieldValue2<UILabel, int>(component, "mFontSize", fontSize);
			ikMaid = 0;
			ikBui = 0;
			faceFlg = false;
			faceFlg2 = false;
			sceneFlg = false;
			poseFlg = false;
			kankyoFlg = false;
			kankyo2Flg = false;
			doguSelectFlg1 = true;
			doguSelectFlg2 = false;
			doguSelectFlg3 = false;
			unLockFlg = false;
			isNamida = false;
			isNamidaH = false;
			isSekimenH = false;
			isHohoH = false;
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
			isMekure1 = false;
			isMekure2 = false;
			isZurasi = false;
			isBra = true;
			isPanz = true;
			isHeadset = true;
			isAccUde = true;
			isStkg = true;
			isShoes = true;
			isGlove = true;
			isMegane = true;
			isAccSenaka = true;
			isMaid = true;
			isF6 = false;
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
			isSepian = false;
			isBloomS = true;
			isDepthS = false;
			isBlurS = false;
			isFogS = false;
			isHairSetting = true;
			isSkirtSetting = false;
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
			isCubeS = false;
			cubeSize = 0.12f;
			isPoseEdit = false;
			isFaceEdit = false;
			bgmIndex = 0;
			if (sceneLevel == 5)
			{
				bgmIndex = 2;
			}
			effectIndex = 0;
			selectMaidIndex = 0;
			copyIndex = 0;
			selectLightIndex = 0;
			doguB2Index = 0;
			parIndex = 0;
			parIndex1 = 0;
			isEditNo = 0;
			editSelectMaid = null;
			for (int k = 0; k < 10; k++)
			{
				date[k] = "";
				ninzu[k] = "";
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
			for (int k = 0; k < maxMaidCnt; k++)
			{
				danceFace[k] = 0f;
				FaceName[k] = "";
				FaceName2[k] = "";
				FaceName3[k] = "";
				isStop[k] = false;
				isBone[k] = false;
				isBoneN[k] = false;
				poseIndex[k] = 0;
				itemIndex[k] = 0;
				itemIndex2[k] = 0;
				faceIndex[k] = 0;
				faceBlendIndex[k] = 0;
				headEyeIndex[k] = 0;
				isLock[k] = false;
				isFace[k] = true;
				mekure1[k] = false;
				mekure2[k] = false;
				zurasi[k] = false;
				mekure1n[k] = false;
				mekure2n[k] = false;
				zurasin[k] = false;
				isLook[k] = false;
				isLookn[k] = false;
				lookX[k] = 0f;
				lookY[k] = -0f;
				lookXn[k] = 0f;
				lookYn[k] = -0f;
				voice1[k] = false;
				voice2[k] = false;
				voice1n[k] = false;
				voice2n[k] = false;
				hanten[k] = false;
				hantenn[k] = false;
				kotei[k] = false;
				xFlg[k] = false;
				zFlg[k] = false;
				ikMode[k] = 0;
				UnityEngine.Object.Destroy(gMaid[k]);
				UnityEngine.Object.Destroy(gMaidC[k]);
				UnityEngine.Object.Destroy(gHandL[k]);
				UnityEngine.Object.Destroy(gArmL[k]);
				UnityEngine.Object.Destroy(gHandR[k]);
				UnityEngine.Object.Destroy(gArmR[k]);
				UnityEngine.Object.Destroy(gFootL[k]);
				UnityEngine.Object.Destroy(gHizaL[k]);
				UnityEngine.Object.Destroy(gFootR[k]);
				UnityEngine.Object.Destroy(gHizaR[k]);
				UnityEngine.Object.Destroy(gClavicleL[k]);
				UnityEngine.Object.Destroy(gClavicleR[k]);
				UnityEngine.Object.Destroy(gNeck[k]);
				UnityEngine.Object.Destroy(gSpine[k]);
				UnityEngine.Object.Destroy(gSpine0a[k]);
				UnityEngine.Object.Destroy(gSpine1a[k]);
				UnityEngine.Object.Destroy(gSpine1[k]);
				UnityEngine.Object.Destroy(gPelvis[k]);
				UnityEngine.Object.Destroy(gizmoHandL[k]);
				UnityEngine.Object.Destroy(gizmoHandR[k]);
				UnityEngine.Object.Destroy(gizmoFootL[k]);
				UnityEngine.Object.Destroy(gizmoFootR[k]);
				HandL1[k] = null;
				for (int i = 0; i < 30; i++)
				{
					UnityEngine.Object.Destroy(gFinger[k, i]);
				}
				for (int i = 0; i < 12; i++)
				{
					UnityEngine.Object.Destroy(gFinger2[k, i]);
				}
				if (isIKAll)
				{
					isIK[k] = true;
				}
				else
				{
					isIK[k] = false;
				}
				pHandL[k] = 0;
				pHandR[k] = 0;
				muneIKL[k] = false;
				muneIKR[k] = false;
			}
			if (kami)
			{
				kami.SetActive(false);
			}
			danceCheckIndex = 0;
			for (int i = 0; i < danceCheck.Length; i++)
			{
				danceCheck[danceCheckIndex] = 1f;
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
			isIdx1 = false;
			isIdx2 = false;
			isIdx3 = false;
			isIdx4 = false;
			bgObject.SetActive(true);
			GameMain.Instance.MainLight.Reset();
			GameMain.Instance.MainLight.SetIntensity(0.95f);
			GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 30f;
			GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
			GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
			GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
			for (int l = 0; l < doguBObject.Count; l++)
			{
				UnityEngine.Object.Destroy(doguBObject[l]);
			}
			doguBObject.Clear();
			doguB2Index = 0;
			parIndex = 0;
			parIndex1 = 0;
			doguCombo2.selectedItemIndex = 0;
			for (int l = 0; l < doguCombo.Length; l++)
			{
				doguCombo[l] = new ComboBox2();
				doguCombo[l].selectedItemIndex = 0;
			}
			parCombo.selectedItemIndex = 0;
			parCombo1.selectedItemIndex = 0;
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
			bgCombo.selectedItemIndex = 0;
			kankyoCombo.selectedItemIndex = 0;
			bgCombo2.selectedItemIndex = 0;
			itemCombo2.selectedItemIndex = 0;
			myCombo.selectedItemIndex = 0;
			slotCombo.selectedItemIndex = 0;
			sortList.Clear();
			itemDataList.Clear();
			itemDataListMod.Clear();
			itemDataListNMod.Clear();
			scrollPos = new Vector2(0f, 0f);
			Vignetting component2 = GameMain.Instance.MainCamera.gameObject.GetComponent<Vignetting>();
			component2.mode = 0;
			component2.intensity = -3.98f;
			component2.chromaticAberration = -0.75f;
			component2.axialAberration = 1.18f;
			component2.blurSpread = 4.19f;
			component2.luminanceDependency = 0.494f;
			component2.blurDistance = 1.71f;
			component2.enabled = false;
			doguIndex.Clear();
			doguSelectIndex = 0;
			for (int k = 0; k < doguObject.Count; k++)
			{
				if (doguObject[k] != null)
				{
					UnityEngine.Object.Destroy(doguObject[k]);
					doguObject[k] = null;
				}
			}
			doguObject.Clear();
		}

		public void init()
		{
			isInit = true;
			isVR = GameMain.Instance.VRMode;
			for (int i = 0; i < GameMain.Instance.CharacterMgr.GetStockMaidCount(); i++)
			{
				Maid maid = GameMain.Instance.CharacterMgr.GetStockMaidList()[i];
				if (goSlot[i] != null)
				{
					maid.body0.goSlot = new List<TBodySkin>(goSlot[i]);
					if (!GameMain.Instance.VRMode)
					{
						maid.body0.quaDefEyeL.eulerAngles = eyeL[i];
						maid.body0.quaDefEyeR.eulerAngles = eyeR[i];
					}
					shodaiFlg[i] = false;
				}
				if (SkirtListArray[i] != null)
				{
					for (int j = 0; j < maid.body0.goSlot.Count; j++)
					{
						AdvancedStudioMode.SetFieldValue8<BoneHair3, DynamicSkirtBone>(maid.body0.goSlot[j].bonehair3, "m_SkirtBone", SkirtListArray[i][j]);
					}
				}
			}
			for (int k = 0; k < maxMaidCnt; k++)
			{
				if (maidArray[k])
				{
					maidArray[k].StopKuchipakuPattern();
				}
				if (maidArray[k])
				{
					maidArray[k].body0.trsLookTarget = GameMain.Instance.MainCamera.transform;
				}
				if (maidArray[k] && maidArray[k].Visible && maidArray[k].body0.isLoadedBody)
				{
					maidArray[k].CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
					maidArray[k].SetAutoTwistAll(true);
					maidArray[k].body0.MuneYureL(1f);
					maidArray[k].body0.MuneYureR(1f);
					maidArray[k].body0.jbMuneL.enabled = true;
					maidArray[k].body0.jbMuneR.enabled = true;
				}
				if (maidArray[k])
				{
					maidArray[k].body0.SetMask(TBody.SlotID.wear, true);
					maidArray[k].body0.SetMask(TBody.SlotID.skirt, true);
					maidArray[k].body0.SetMask(TBody.SlotID.bra, true);
					maidArray[k].body0.SetMask(TBody.SlotID.panz, true);
					maidArray[k].body0.SetMask(TBody.SlotID.mizugi, true);
					maidArray[k].body0.SetMask(TBody.SlotID.onepiece, true);
					if (maidArray[k].body0.isLoadedBody)
					{
						Maid maid2 = maidArray[k];
						for (int j = 0; j < maid2.body0.goSlot.Count; j++)
						{
							List<THair1> fieldValue = AdvancedStudioMode.GetFieldValue<TBoneHair_, List<THair1>>(maid2.body0.goSlot[j].bonehair, "hair1list");
							for (int i = 0; i < fieldValue.Count; i++)
							{
								fieldValue[i].SoftG = new Vector3(0f, -0.003f, 0f);
							}
						}
					}
				}
				maidArray[k] = null;
			}
			goSlot = new List<TBodySkin>[500];
			bodyHit = new List<TBodyHit>[500];
			allowUpdate = true;
			if (okFlg)
			{
				GameMain.Instance.CharacterMgr.ResetCharaPosAll();
				for (int i = 0; i < GameMain.Instance.CharacterMgr.GetStockMaidCount(); i++)
				{
					if (sceneLevel != 5 || i != editMaid)
					{
						Maid maid = GameMain.Instance.CharacterMgr.GetStockMaidList()[i];
						maid.transform.localScale = new Vector3(1f, 1f, 1f);
						TBody.MaskMode maskMode = (TBody.MaskMode)Enum.Parse(typeof(TBody.MaskMode), "None");
						maid.body0.SetMaskMode(maskMode);
						maid.boMabataki = true;
					}
				}
				GameMain.Instance.CharacterMgr.DeactivateCharaAll();
				for (int i = 11; i < GameMain.Instance.CharacterMgr.GetStockMaidCount(); i++)
				{
					Maid maid = GameMain.Instance.CharacterMgr.GetStockMaidList()[i];
					Vector3 zero = Vector3.zero;
					maid.SetPos(zero);
					maid.SetRot(zero);
					maid.SetPosOffset(zero);
					if (maid.body0 != null)
					{
						maid.body0.SetBoneHitHeightY(0f);
					}
					maid.Visible = false;
					maid.ActiveSlotNo = -1;
					maid.DelPrefabAll();
					if (maid.body0.isLoadedBody)
					{
						maid.CrossFade(poseArray[0] + ".anm", false, true, false, 0f, 1f);
						maid.SetAutoTwistAll(true);
					}
					maid.boMabataki = true;
				}
			}
			okFlg = false;
			isDance = false;
			isDanceChu = false;
			isSavePose = false;
			bgIndex = 0;
			bgIndexB = 0;
			bg.localScale = new Vector3(1f, 1f, 1f);
			softG = new Vector3(0f, -0.003f, 0f);
			softG2 = new Vector3(0f, -0.005f, 0f);
			if (!GameMain.Instance.VRMode)
			{
				Bloom fieldValue2 = AdvancedStudioMode.GetFieldValue<CameraMain, Bloom>(mainCamera, "m_gcBloom");
				fieldValue2.enabled = true;
				fieldValue2.bloomIntensity = 2.85f;
				fieldValue2.hdr = 0;
				fieldValue2.bloomThreshholdColor = new Color(1f, 1f, 1f);
				fieldValue2.bloomBlurIterations = 3;
			}
			mainCamera.GetComponent<Camera>().backgroundColor = new Color(0f, 0f, 0f);
			maidCnt = 0;
			wearIndex = 0;
			isF6 = false;
			faceFlg = false;
			faceFlg2 = false;
			sceneFlg = false;
			poseFlg = false;
			kankyoFlg = false;
			kankyo2Flg = false;
			unLockFlg = false;
			inName = "";
			inName2 = "";
			inName3 = "";
			inText = "";
			fontSize = 25;
			bGuiMessage = false;
			isMessage = false;
			GameObject gameObject = GameObject.Find("__GameMain__/SystemUI Root");
			GameObject gameObject2 = gameObject.transform.Find("MessageWindowPanel").gameObject;
			MessageWindowMgr messageWindowMgr = GameMain.Instance.ScriptMgr.adv_kag.MessageWindowMgr;
			MessageClass messageClass = new MessageClass(gameObject2, messageWindowMgr);
			messageClass.Clear();
			messageWindowMgr.CloseMessageWindowPanel();
			UILabel component = UTY.GetChildObject(gameObject2, "MessageViewer/MsgParent/Message", false).GetComponent<UILabel>();
			AdvancedStudioMode.SetFieldValue2<UILabel, int>(component, "mFontSize", fontSize);
			ikMaid = 0;
			ikBui = 0;
			isNamida = false;
			isNamidaH = false;
			isSekimenH = false;
			isHohoH = false;
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
			isMekure1 = false;
			isMekure2 = false;
			isZurasi = false;
			isBra = true;
			isPanz = true;
			isHeadset = true;
			isAccUde = true;
			isStkg = true;
			isShoes = true;
			isGlove = true;
			isMegane = true;
			isAccSenaka = true;
			isMaid = true;
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
			isSepian = false;
			isBloomS = true;
			isDepthS = false;
			isBlurS = false;
			isFogS = false;
			isHairSetting = true;
			isSkirtSetting = false;
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
			isCubeS = false;
			cubeSize = 0.12f;
			isPoseEdit = false;
			isFaceEdit = false;
			bgmIndex = 0;
			if (sceneLevel == 5)
			{
				bgmIndex = 2;
			}
			effectIndex = 0;
			selectMaidIndex = 0;
			copyIndex = 0;
			selectLightIndex = 0;
			doguB2Index = 0;
			parIndex = 0;
			parIndex1 = 0;
			isEditNo = 0;
			editSelectMaid = null;
			for (int k = 0; k < 10; k++)
			{
				date[k] = "";
				ninzu[k] = "";
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
			for (int k = 0; k < maxMaidCnt; k++)
			{
				danceFace[k] = 0f;
				FaceName[k] = "";
				FaceName2[k] = "";
				FaceName3[k] = "";
				isStop[k] = false;
				isBone[k] = false;
				isBoneN[k] = false;
				poseIndex[k] = 0;
				itemIndex[k] = 0;
				itemIndex2[k] = 0;
				faceIndex[k] = 0;
				faceBlendIndex[k] = 0;
				headEyeIndex[k] = 0;
				isLock[k] = false;
				isFace[k] = true;
				mekure1[k] = false;
				mekure2[k] = false;
				zurasi[k] = false;
				mekure1n[k] = false;
				mekure2n[k] = false;
				zurasin[k] = false;
				isLook[k] = false;
				isLookn[k] = false;
				lookX[k] = 0f;
				lookY[k] = -0f;
				lookXn[k] = 0f;
				lookYn[k] = -0f;
				voice1[k] = false;
				voice2[k] = false;
				voice1n[k] = false;
				voice2n[k] = false;
				hanten[k] = false;
				hantenn[k] = false;
				kotei[k] = false;
				xFlg[k] = false;
				zFlg[k] = false;
				ikMode[k] = 0;
				UnityEngine.Object.Destroy(gMaid[k]);
				UnityEngine.Object.Destroy(gMaidC[k]);
				UnityEngine.Object.Destroy(gHandL[k]);
				UnityEngine.Object.Destroy(gArmL[k]);
				UnityEngine.Object.Destroy(gHandR[k]);
				UnityEngine.Object.Destroy(gArmR[k]);
				UnityEngine.Object.Destroy(gFootL[k]);
				UnityEngine.Object.Destroy(gHizaL[k]);
				UnityEngine.Object.Destroy(gFootR[k]);
				UnityEngine.Object.Destroy(gHizaR[k]);
				UnityEngine.Object.Destroy(gClavicleL[k]);
				UnityEngine.Object.Destroy(gClavicleR[k]);
				UnityEngine.Object.Destroy(gNeck[k]);
				UnityEngine.Object.Destroy(gSpine[k]);
				UnityEngine.Object.Destroy(gSpine0a[k]);
				UnityEngine.Object.Destroy(gSpine1a[k]);
				UnityEngine.Object.Destroy(gSpine1[k]);
				UnityEngine.Object.Destroy(gPelvis[k]);
				UnityEngine.Object.Destroy(gizmoHandL[k]);
				UnityEngine.Object.Destroy(gizmoHandR[k]);
				UnityEngine.Object.Destroy(gizmoFootL[k]);
				UnityEngine.Object.Destroy(gizmoFootR[k]);
				HandL1[k] = null;
				for (int i = 0; i < 30; i++)
				{
					UnityEngine.Object.Destroy(gFinger[k, i]);
				}
				for (int i = 0; i < 12; i++)
				{
					UnityEngine.Object.Destroy(gFinger2[k, i]);
				}
				if (isIKAll)
				{
					isIK[k] = true;
				}
				else
				{
					isIK[k] = false;
				}
				pHandL[k] = 0;
				pHandR[k] = 0;
				muneIKL[k] = false;
				muneIKR[k] = false;
			}
			if (kami)
			{
				kami.SetActive(false);
			}
			danceCheckIndex = 0;
			for (int i = 0; i < danceCheck.Length; i++)
			{
				danceCheck[danceCheckIndex] = 1f;
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
			isIdx1 = false;
			isIdx2 = false;
			isIdx3 = false;
			isIdx4 = false;
			UnityEngine.Object.Destroy(cameraObj);
			UnityEngine.Object.Destroy(subcamera);
			bgObject.SetActive(true);
			GameMain.Instance.MainLight.Reset();
			GameMain.Instance.MainLight.SetIntensity(0.95f);
			GameMain.Instance.MainLight.GetComponent<Light>().spotAngle = 30f;
			GameMain.Instance.MainLight.GetComponent<Light>().range = 10f;
			GameMain.Instance.MainLight.GetComponent<Light>().type = LightType.Directional;
			GameMain.Instance.MainLight.gameObject.transform.position = new Vector3(0f, 2f, 0f);
			if (!GameMain.Instance.VRMode)
			{
				m_material = new Material(Shader.Find("Transparent/Diffuse"));
				m_material.color = new Color(0.4f, 0.4f, 1f, 0.8f);
				m_UOCamera = AdvancedStudioMode.GetFieldValue<CameraMain, UltimateOrbitCamera>(mainCamera, "m_UOCamera");
				if (sceneLevel == 3)
				{
					m_UOCamera.enabled = true;
				}
			}
			for (int l = 0; l < doguBObject.Count; l++)
			{
				UnityEngine.Object.Destroy(doguBObject[l]);
			}
			doguBObject.Clear();
			doguB2Index = 0;
			parIndex = 0;
			parIndex1 = 0;
			doguCombo2.selectedItemIndex = 0;
			for (int l = 0; l < doguCombo.Length; l++)
			{
				doguCombo[l] = new ComboBox2();
				doguCombo[l].selectedItemIndex = 0;
			}
			parCombo.selectedItemIndex = 0;
			parCombo1.selectedItemIndex = 0;
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
			bgCombo.selectedItemIndex = 0;
			kankyoCombo.selectedItemIndex = 0;
			bgCombo2.selectedItemIndex = 0;
			itemCombo2.selectedItemIndex = 0;
			myCombo.selectedItemIndex = 0;
			slotCombo.selectedItemIndex = 0;
			sortList.Clear();
			itemDataList.Clear();
			itemDataListMod.Clear();
			itemDataListNMod.Clear();
			scrollPos = new Vector2(0f, 0f);
			if (!GameMain.Instance.VRMode)
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
			for (int k = 0; k < doguObject.Count; k++)
			{
				if (doguObject[k] != null)
				{
					UnityEngine.Object.Destroy(doguObject[k]);
					doguObject[k] = null;
				}
			}
			doguObject.Clear();
			string text = Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose";
			if (!File.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path3 = Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModeScene";
			if (!File.Exists(path3))
			{
				Directory.CreateDirectory(path3);
			}
			int num = countS;
			Action<string, List<string>> action = delegate (string path, List<string> result_list)
			{
				string[] files = Directory.GetFiles(path);
				countS = 0;
				for (int n = 0; n < files.Length; n++)
				{
					if (Path.GetExtension(files[n]) == ".anm")
					{
						string text4 = files[n].Split(new char[]
						{
							'\\'
						})[files[n].Split(new char[]
						{
							'\\'
						}).Length - 1];
						text4 = text4.Split(new char[]
						{
							'.'
						})[0];
						strListS.Add(text4 + "\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000/" + files[n]);
						countS++;
					}
				}
			};
			List<string> arg = new List<string>();
			action(text, arg);
			if (countS != num)
			{
				poseArray = null;
			}
			if (poseArray == null)
			{
				strList2 = new List<string>();
				strListE = new List<string>();
				strListE2 = new List<string>();
				strListS = new List<string>();
				strListD = new List<string>();
				strS = "";
				List<string> list = new List<string>();
				list.Add(string.Empty);
				list.AddRange(GameUty.PathList);
				List<string> bgList2 = new List<string>();
				HashSet<int> hashSet = new HashSet<int>();
				CsvCommonIdManager.ReadEnabledIdList(0, true, "phot_bg_enabled_list", ref hashSet);
				Action<string> action2 = delegate (string strFileName)
				{
					if (GameUty.FileSystem.IsExistentFile(strFileName))
					{
						using (AFileBase afileBase2 = GameUty.FileSystem.FileOpen(strFileName))
						{
							using (CsvParser csvParser2 = new CsvParser())
							{
								csvParser2.Open(afileBase2);
								for (int n = 1; n < csvParser2.max_cell_y; n++)
								{
									if (csvParser2.IsCellToExistData(0, n) && hashSet.Contains(csvParser2.GetCellAsInteger(0, n)))
									{
										bgList2.Add(csvParser2.GetCellAsString(3, n));
										string text4 = csvParser2.GetCellAsString(2, n);
										if (csvParser2.GetCellAsString(1, n) == "夜")
										{
											text4 += "(夜)";
										}
										bgNameList.Add(csvParser2.GetCellAsString(3, n) + "," + text4);
									}
								}
							}
						}
					}
				};
				action2("phot_bg_list.nei");
				for (int j = 0; j < list.Count; j++)
				{
					action2("edit_bg_" + list[j] + ".nei");
				}
				List<string> list2 = new List<string>();
				for (int i = 0; i < bgList2.Count; i++)
				{
					bool flag = false;
					for (int k = 0; k < bgArray21.Length; k++)
					{
						if (bgList2[i] == bgArray21[k])
						{
							flag = true;
						}
					}
					if (!flag)
					{
						if (bgList2[i] != "HoneymoonRoom" && bgList2[i] != "ClassRoom_Play" && bgList2[i] != "BigSight" && bgList2[i] != "PrivateRoom" && bgList2[i] != "Sea_Night" && bgList2[i] != "Yashiki")
						{
							list2.Add(bgList2[i]);
						}
					}
				}
				UnityEngine.Object x = GameMain.Instance.BgMgr.CreateAssetBundle("SMRoom2");
				if (x != null)
				{
					isVP = true;
				}
				x = GameMain.Instance.BgMgr.CreateAssetBundle("Train");
				if (x != null)
				{
					isPP = true;
				}
				x = GameMain.Instance.BgMgr.CreateAssetBundle("HoneymoonRoom");
				if (x != null)
				{
					isPP2 = true;
				}
				x = GameMain.Instance.BgMgr.CreateAssetBundle("BigSight");
				if (x != null)
				{
					isPP3 = true;
				}
				x = GameMain.Instance.BgMgr.CreateAssetBundle("rotenburo");
				if (x != null)
				{
					isVA = true;
				}
				x = GameMain.Instance.BgMgr.CreateAssetBundle("karaokeroom");
				if (x != null)
				{
					isKA = true;
				}
				if (GameUty.IsEnabledCompatibilityMode)
				{
					isCM3D2 = true;
				}
				List<string> list3 = new List<string>(350 + poseArray2.Length);
				list3.AddRange(poseArray2);
				List<string> list4 = new List<string>();
				for (int l = 11; l < 200; l++)
				{
					if (l < 100)
					{
						using (AFileBase afileBase = GameUty.FileSystem.FileOpen("edit_pose_0" + l + "_f.anm"))
						{
							if (afileBase.IsValid())
							{
								list4.Add("edit_pose_0" + l + "_f");
							}
						}
					}
					else
					{
						using (AFileBase afileBase = GameUty.FileSystem.FileOpen("edit_pose_" + l + "_f.anm"))
						{
							if (afileBase.IsValid())
							{
								list4.Add("edit_pose_" + l + "_f");
							}
						}
					}
				}
				for (int l = 15; l < 25; l++)
				{
					for (int k = 0; k < 2; k++)
					{
						string text2 = "s";
						if (k == 1)
						{
							text2 = "w";
						}
						for (int i = 1; i < 20; i++)
						{
							if (i < 10)
							{
								using (AFileBase afileBase = GameUty.FileSystem.FileOpen(string.Concat(new object[]
								{
									"edit_pose_dg",
									l,
									text2,
									"_00",
									i,
									"_f.anm"
								})))
								{
									if (afileBase.IsValid())
									{
										list4.Add(string.Concat(new object[]
										{
											"edit_pose_dg",
											l,
											text2,
											"_00",
											i,
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
									text2,
									"_0",
									i,
									"_f.anm"
								})))
								{
									if (afileBase.IsValid())
									{
										list4.Add(string.Concat(new object[]
										{
											"edit_pose_dg",
											l,
											text2,
											"_0",
											i,
											"_f"
										}));
									}
								}
							}
						}
					}
				}
				if (list4.Count > 0)
				{
					list3.AddRange(list4.ToArray());
				}
				using (GameUty.FileSystem.FileOpen("dance_cm3d_003_sp2_f1.anm"))
				{
				}
				using (GameUty.FileSystem.FileOpen("dance_cm3d2_kara_003_ddfl_f1.anm"))
				{
				}
				using (GameUty.FileSystem.FileOpen("dance_cm3d2_kara02_001_smt_f1.anm"))
				{
				}
				list3.AddRange(poseArrayVP2);
				list3.AddRange(poseArrayFB);
				list3.AddRange(poseArray4);
				list3.AddRange(poseArray5);
				list3.AddRange(poseArray6);
				poseArray = list3.ToArray();
				Action<string, List<string>> action3 = delegate (string path, List<string> result_list)
				{
					string[] files = Directory.GetFiles(path);
					countS = 0;
					for (int n = 0; n < files.Length; n++)
					{
						if (Path.GetExtension(files[n]) == ".anm")
						{
							string text4 = files[n].Split(new char[]
							{
								'\\'
							})[files[n].Split(new char[]
							{
								'\\'
							}).Length - 1];
							text4 = text4.Split(new char[]
							{
								'.'
							})[0];
							strListS.Add(text4 + "\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000/" + files[n]);
							countS++;
						}
					}
				};
				List<string> arg2 = new List<string>();
				action3(Path.GetFullPath(".\\") + "Mod\\AdvancedStudioModePose", arg2);
				string[] list5 = GameUty.FileSystem.GetList("motion", AFileSystemBase.ListType.AllFile);
				int num2 = 0;
				List<string> list6 = new List<string>();
				string[] array = list5;
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
										bool flag = false;
										foreach (string text2 in strListS)
										{
											string b = text2.Split(new char[]
											{
												'/'
											})[0].Replace("\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000", "");
											if (text3 == b)
											{
												flag = true;
												break;
											}
										}
										if (!flag)
										{
											list6.Add(text3);
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
					//IL_217A:
					m++;
					continue;
					//goto IL_217A;
				}
				foreach (string text2 in list6)
				{
					bool flag2 = false;
					for (int l = 0; l < poseArray.Length; l++)
					{
						if (text2 == poseArray[l])
						{
							flag2 = true;
						}
					}
					if (!flag2 && text2.StartsWith("edit_"))
					{
						strList2.Add(text2);
					}
				}
				foreach (string text2 in list6)
				{
					bool flag2 = false;
					for (int l = 0; l < poseArray.Length; l++)
					{
						if (text2 == poseArray[l])
						{
							flag2 = true;
						}
					}
					if (!flag2 && text2.StartsWith("pose_"))
					{
						strList2.Add(text2);
					}
				}
				foreach (string text2 in list6)
				{
					bool flag2 = false;
					for (int l = 0; l < poseArray.Length; l++)
					{
						if (text2 == poseArray[l])
						{
							flag2 = true;
						}
					}
					if (!flag2 && !text2.StartsWith("edit_") && !text2.StartsWith("pose_"))
					{
						strList2.Add(text2);
					}
				}
                foreach (string text2 in strListE)
                {
                    bool flag2 = false;
                    for (int l = 0; l < poseArray.Length; l++)
                    {
                        if (text2 == poseArray[l])
                        {
                            flag2 = true;
                        }
                    }
					if (!flag2 && !text2.StartsWith("amayakasi_taimen_sex") && !text2.StartsWith("amayakasi_tekoki") && !text2.StartsWith("anal_name") && !text2.StartsWith("kousoku_irruma2") && !text2.StartsWith("om_ganmenkijyoui") && !text2.StartsWith("om_haimenekiben") && !text2.StartsWith("om_haimenzai2") && !text2.StartsWith("om_kakae_seijyoui") && !text2.StartsWith("om_manguri2") && !text2.StartsWith("om_tati_kunni") && !text2.StartsWith("ran3p_3ana_kakae_sex") && !text2.StartsWith("ran4p_dai_seijyoui") && !text2.StartsWith("sokui3") && !text2.StartsWith("tati_aibu") && !text2.StartsWith("tetunagi_kijyoui"))
					{
						strListE2.Add(text2);
						num2++;
					}
				}
                list3.AddRange(strList2.ToArray());
                list3.AddRange(strListE2.ToArray());
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
							list3.AddRange(new string[]
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
				list3.AddRange(strListS.ToArray());
				poseArray = list3.ToArray();
				List<string> list7 = new List<string>(50 + poseGroupArray2.Length);
				list7.AddRange(poseGroupArray2);
				list7.AddRange(poseGroupArrayVP);
				list7.AddRange(poseGroupArrayFB);
				list7.AddRange(poseGroupArray3);
				list7.Add(poseArray5[0]);
				list7.Add(poseArray6[0]);
                list7.Add(strList2[0]);
                list7.Add(strListE2[0]);
                if (strListS.Count > 0 && poseIniStr == "")
				{
					list7.Add(strListS[0]);
					existPose = true;
				}
				if (poseIniStr != "")
				{
					list7.Add(poseIniStr);
				}
				poseGroupArray = list7.ToArray();
				groupList = new ArrayList();
				for (int i = 0; i < poseArray.Length; i++)
				{
					for (int k = 0; k < poseGroupArray.Length; k++)
					{
						if (poseGroupArray[k] == poseArray[i])
						{
							groupList.Add(i);
							if (poseGroupArray[k] == strList2[0])
							{
								sPoseCount = i;
							}
						}
					}
				}
				string[] collection = new string[]
				{
					"Salon_Day"
				};
				string[] collection2 = new string[]
				{
					"SMRoom2",
					"LockerRoom"
				};
				string[] collection3 = new string[]
				{
					"Train",
					"Toilet",
					"Oheya",
					"MyBedRoom_NightOff"
				};
				string[] collection4 = new string[]
				{
					"ClassRoom",
					"ClassRoom_Play",
					"HoneymoonRoom",
					"OutletPark"
				};
				string[] collection5 = new string[]
				{
					"BigSight",
					"BigSight_Night",
					"PrivateRoom",
					"PrivateRoom_Night",
					"Sea",
					"Sea_Night",
					"Yashiki_Day",
					"Yashiki",
					"Yashiki_Pillow"
				};
				string[] collection6 = new string[]
				{
					"rotenburo",
					"rotenburo_night",
					"villa",
					"villa_night",
					"villa_bedroom",
					"villa_bedroom_night",
					"villa_farm",
					"villa_farm_night"
				};
				string[] collection7 = new string[]
				{
					"karaokeroom"
				};
				List<string> list8 = new List<string>(50 + poseArray2.Length);
				list8.AddRange(bgArray2);
				List<string> list9 = new List<string>();
				Dictionary<string, string> saveDataDic = CreativeRoomManager.GetSaveDataDic();
				if (saveDataDic != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in saveDataDic)
					{
						list9.Add(keyValuePair.Key);
					}
				}
				list8.AddRange(list9.ToArray());
				if (isCM3D2)
				{
					list8.AddRange(bgArray21);
					if (isVP)
					{
						list8.AddRange(collection2);
					}
					if (isPP)
					{
						list8.AddRange(collection3);
					}
					if (isPP2)
					{
						list8.AddRange(collection4);
					}
					if (isPP3)
					{
						list8.AddRange(collection5);
					}
					if (isVA)
					{
						list8.AddRange(collection6);
					}
					if (isKA || isKA2)
					{
						list8.AddRange(collection7);
					}
					list8.AddRange(collection);
				}
				List<string> list10 = new List<string>();
				for (int i = 0; i < bgList2.Count; i++)
				{
					bool flag3 = false;
					for (int k = 0; k < list8.Count; k++)
					{
						if (list8[k] == bgList2[i])
						{
							flag3 = true;
						}
					}
					if (!flag3)
					{
						list10.Add(bgList2[i]);
					}
				}
				list8.AddRange(list10.ToArray());
				bgArray = list8.ToArray();
				string[] collection8 = new string[]
				{
					"dokidokifallinlove_short_inst",
					"dokidokifallinlove_short",
					"entrancetoyou_short"
				};
				string[] collection9 = new string[]
				{
					"scarlet leap_short"
				};
				string[] array2 = new string[]
				{
					"stellarmytears_short",
					"stellarmytears_short2",
					"stellarmytears_short3"
				};
				string[] collection10 = new string[]
				{
					"RhythmixToYou"
				};
				array2 = new string[]
				{
					"happy_happy_scandal1",
					"happy_happy_scandal2",
					"happy_happy_scandal3"
				};
				array2 = new string[]
				{
					"can_know_two_close"
				};
				array2 = new string[]
				{
					"sweetsweeteveryday_short1",
					"sweetsweeteveryday_short2",
					"sweetsweeteveryday_short3"
				};
				string[] collection11 = new string[]
				{
					"bloomingdreaming_short",
					"kiminiaijodelicious_short",
					"luminousmoment_short",
					"nightmagicfire_short",
					"melodyofempire_short"
				};
				List<string> list11 = new List<string>(50);
				list11.AddRange(bgmArray2);
				List<string> list12 = new List<string>();
				for (int l = 18; l < 210; l++)
				{
					if (l < 100)
					{
						using (AFileBase afileBase = GameUty.FileSystem.FileOpen("bgm0" + l + ".ogg"))
						{
							if (afileBase.IsValid())
							{
								list12.Add("bgm0" + l);
							}
						}
					}
					else
					{
						using (AFileBase afileBase = GameUty.FileSystem.FileOpen("bgm" + l + ".ogg"))
						{
							if (afileBase.IsValid())
							{
								list12.Add("bgm" + l);
							}
						}
					}
				}
				if (list12.Count > 0)
				{
					list11.AddRange(list12.ToArray());
				}
				list11.AddRange(collection8);
				list11.AddRange(collection9);
				list11.AddRange(collection10);
				list11.AddRange(collection11);
				bgmArray = list11.ToArray();
				array2 = new string[]
				{
					"OutletPark:54",
					"HoneymoonRoom:102"
				};
				List<string> list13 = new List<string>(200);
				using (AFileBase afileBase = GameUty.FileSystem.FileOpen("desk_item_detail.nei"))
				{
					using (CsvParser csvParser = new CsvParser())
					{
						bool flag4 = csvParser.Open(afileBase);
						NDebug.Assert(flag4, "desk_item_detail.nei\nopen failed.");
						for (int i = 1; i < csvParser.max_cell_y; i++)
						{
							if (csvParser.IsCellToExistData(0, i))
							{
								int cellAsInteger = csvParser.GetCellAsInteger(0, i);
								AdvancedStudioMode.ItemData2 itemData = new AdvancedStudioMode.ItemData2(csvParser, i);
								if (itemData.asset_name != "")
								{
									if (GameMain.Instance.BgMgr.CreateAssetBundle(itemData.asset_name) != null)
									{
										list13.AddRange(new string[]
										{
											itemData.name + "\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000\u3000#" + itemData.asset_name
										});
									}
								}
							}
						}
					}
				}
				parArray = list13.ToArray();
				List<string> list14 = new List<string>(50 + parArray2.Length);
				list14.AddRange(parArray2);
				list14.AddRange(parArray3);
				parArray1 = list14.ToArray();
				List<string> list15 = new List<string>(50 + doguB1Array.Length);
				list15.AddRange(doguB1Array);
				if (isCM3D2)
				{
					if (isVA)
					{
						list15.AddRange(doguB2Array);
					}
					if (isKA)
					{
						list15.AddRange(doguB3Array);
					}
					if (isKA2)
					{
						list15.AddRange(doguB4Array);
					}
				}
				doguB1Array = list15.ToArray();
				List<string>[] array3 = new List<string>[9];
				for (int l = 0; l < 9; l++)
				{
					array3[l] = new List<string>();
				}
				HashSet<int> hashSet2 = new HashSet<int>();
				CsvCommonIdManager.ReadEnabledIdList(0, true, "phot_bg_object_enabled_list", ref hashSet2);
				PhotoBGObjectData.Create();
				List<PhotoBGObjectData> data = PhotoBGObjectData.data;
				for (int l = 0; l < data.Count; l++)
				{
					string text3 = data[l].create_prefab_name;
					if (text3 == "")
					{
						text3 = data[l].create_asset_bundle_name;
					}
					string category = data[l].category;
					switch (category)
					{
						case "家具":
							array3[0].Add(text3);
							break;
						case "道具":
							array3[1].Add(text3);
							break;
						case "文房具":
							array3[2].Add(text3);
							break;
						case "グルメ":
							array3[3].Add(text3);
							break;
						case "ドリンク":
							array3[4].Add(text3);
							break;
						case "カジノアイテム":
							array3[5].Add(text3);
							break;
						case "プレイアイテム":
							array3[6].Add(text3);
							break;
						case "パーティクル":
							array3[7].Add(text3);
							break;
						case "その他":
							array3[8].Add(text3);
							break;
					}
					string name = data[l].name;
					doguNameList.Add(text3 + "," + name);
				}
				for (int l = 0; l < array3.Length; l++)
				{
					doguBArray.Add(array3[l].ToArray());
				}
				List<string> list16 = new List<string>(100 + itemBArray.Length);
				list16.AddRange(itemBArray);
				if (isCM3D2)
				{
					if (isVA)
					{
						list16.AddRange(itemB2Array);
					}
					if (isKA)
					{
						list16.AddRange(itemB3Array);
					}
					if (isKA2)
					{
						list16.AddRange(itemB4Array);
					}
				}
				itemBArray = list16.ToArray();
				List<string>[] array4 = new List<string>[9];
				for (int l = 0; l < 9; l++)
				{
					array4[l] = new List<string>();
				}
				List<int> categoryIDList = PlacementData.CategoryIDList;
				List<int> list17 = new List<int>(20);
				list17.Add(-1);
				list17.AddRange(categoryIDList.ToArray());
				myArray = list17.ToArray();
			}
		}
	}
}
