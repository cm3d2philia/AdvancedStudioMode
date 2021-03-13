using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CM3D2.AdvancedStudioMode.Plugin
{
    public partial class AdvancedStudioMode
    {
		private void SetIKInit6(int k)
		{
			gHead2[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHead2[k].GetComponent<Renderer>().enabled = false;
			mHead2[k] = gHead2[k].AddComponent<MouseDrag5>();
			mHead2[k].obj = gHead2[k];
			gHead2[k].transform.localScale = new Vector3(0.2f, 0.24f, 0.2f);
			gHead2[k].layer = 8;
			gMaid2[k] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			gMaid2[k].GetComponent<Renderer>().enabled = false;
			mMaid2[k] = gMaid2[k].AddComponent<MouseDrag5>();
			mMaid2[k].obj = gMaid2[k];
			gMaid2[k].transform.localScale = new Vector3(0.2f, 0.3f, 0.24f);
			gMaid2[k].layer = 8;
		}

		private void SetIKInit7(int k)
		{
			gIKMuneL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gIKMuneL[k].GetComponent<Renderer>().enabled = false;
			mIKMuneL[k] = gIKMuneL[k].AddComponent<MouseDrag>();
			mIKMuneL[k].obj = gIKMuneL[k];
			gIKMuneL[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
			gIKMuneL[k].layer = 8;
			gIKMuneR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gIKMuneR[k].GetComponent<Renderer>().enabled = false;
			mIKMuneR[k] = gIKMuneR[k].AddComponent<MouseDrag>();
			mIKMuneR[k].obj = gIKMuneR[k];
			gIKMuneR[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
			gIKMuneR[k].layer = 8;
		}

		private void SetIKInit5(int k)
		{
			gIKHandL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gIKHandL[k].GetComponent<Renderer>().enabled = false;
			mIKHandL[k] = gIKHandL[k].AddComponent<MouseDrag4>();
			mIKHandL[k].obj = gIKHandL[k];
			gIKHandL[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
			gIKHandR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gIKHandR[k].GetComponent<Renderer>().enabled = false;
			mIKHandR[k] = gIKHandR[k].AddComponent<MouseDrag4>();
			mIKHandR[k].obj = gIKHandR[k];
			gIKHandR[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
		}

		private void SetIKInit4(int k)
		{
			gHead[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHead[k].GetComponent<Renderer>().enabled = false;
			mHead[k] = gHead[k].AddComponent<MouseDrag3>();
			mHead[k].obj = gHead[k];
			gHead[k].transform.localScale = new Vector3(0.2f, 0.24f, 0.2f);
			gJotai[k] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			gJotai[k].GetComponent<Renderer>().enabled = false;
			mJotai[k] = gJotai[k].AddComponent<MouseDrag3>();
			mJotai[k].obj = gJotai[k];
			gJotai[k].transform.localScale = new Vector3(0.2f, 0.19f, 0.24f);
			gJotai[k].layer = 8;
			gKahuku[k] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			gKahuku[k].GetComponent<Renderer>().enabled = false;
			mKahuku[k] = gKahuku[k].AddComponent<MouseDrag3>();
			mKahuku[k].obj = gKahuku[k];
			gKahuku[k].transform.localScale = new Vector3(0.2f, 0.15f, 0.24f);
			gKahuku[k].layer = 8;
		}

		private void SetIKInit3(int k)
		{
			gMaid[k] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			gMaid[k].GetComponent<Renderer>().enabled = false;
			mMaid[k] = gMaid[k].AddComponent<MouseDrag2>();
			mMaid[k].obj = gMaid[k];
			gMaid[k].transform.localScale = new Vector3(0.2f, 0.3f, 0.24f);
			gMaid[k].layer = 8;
			gMaidC[k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gMaidC[k].GetComponent<Renderer>().enabled = true;
			mMaidC[k] = gMaidC[k].AddComponent<MouseDrag2>();
			mMaidC[k].obj = gMaidC[k];
			gMaidC[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
			Material material = new Material(Shader.Find("Transparent/Diffuse"));
			material.color = new Color(0.5f, 0.5f, 1f, 0.8f);
			gMaidC[k].GetComponent<Renderer>().material = material;
			gMaidC[k].layer = 8;
		}

		private void SetIKInit2(int k)
		{
			for (int i = 0; i < 30; i++)
			{
				gFinger[k, i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				if (i % 3 == 0)
				{
				}
				mFinger[k, i] = gFinger[k, i].AddComponent<MouseDrag>();
				mFinger[k, i].obj = gFinger[k, i];
				float num = 0f;
				switch (i)
				{
					case 0:
					case 15:
						num = 0.024f;
						break;
					case 1:
					case 16:
						num = 0.018f;
						break;
					case 2:
					case 17:
						num = 0.014f;
						break;
					case 3:
					case 6:
					case 9:
					case 18:
					case 21:
					case 24:
						num = 0.017f;
						break;
					case 4:
					case 7:
					case 10:
					case 19:
					case 22:
					case 25:
						num = 0.015f;
						break;
					case 5:
					case 8:
					case 11:
					case 20:
					case 23:
					case 26:
						num = 0.013f;
						break;
					case 12:
					case 27:
						num = 0.015f;
						break;
					case 13:
					case 28:
						num = 0.013f;
						break;
					case 14:
					case 29:
						num = 0.012f;
						break;
				}
				gFinger[k, i].transform.localScale = new Vector3(num, num, num);
				Material material = new Material(Shader.Find("Transparent/Diffuse"));
				gFinger[k, i].GetComponent<Renderer>().material = material;
				MeshRenderer component = gFinger[k, i].GetComponent<MeshRenderer>();
				component.material.color = new Color(0f, 0f, 1f, 0.2f);
			}
			for (int i = 0; i < 12; i++)
			{
				gFinger2[k, i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				mFinger2[k, i] = gFinger2[k, i].AddComponent<MouseDrag>();
				mFinger2[k, i].obj = gFinger2[k, i];
				float num = 0f;
				switch (i)
				{
					case 0:
					case 6:
						num = 0.017f;
						break;
					case 1:
					case 7:
						num = 0.016f;
						break;
					case 2:
					case 8:
						num = 0.024f;
						break;
					case 3:
					case 9:
						num = 0.017f;
						break;
					case 4:
					case 10:
						num = 0.024f;
						break;
					case 5:
					case 11:
						num = 0.018f;
						break;
				}
				gFinger2[k, i].transform.localScale = new Vector3(num, num, num);
				Material material = new Material(Shader.Find("Transparent/Diffuse"));
				gFinger2[k, i].GetComponent<Renderer>().material = material;
				MeshRenderer component = gFinger2[k, i].GetComponent<MeshRenderer>();
				component.material.color = new Color(0f, 0f, 1f, 0.2f);
			}
		}

		private void SetIKInit(int k)
		{
			Material material = new Material(Shader.Find("Transparent/Diffuse"));
			material.color = new Color(0.4f, 0.4f, 1f, 0.3f);
			m_material2 = new Material(Shader.Find("Transparent/Diffuse"));
			m_material2.color = new Color(0.4f, 0.4f, 1f, 0.26f);
			m_material3 = new Material(Shader.Find("Transparent/Diffuse"));
			m_material3.color = new Color(0.4f, 0.4f, 1f, 0.36f);
			gHandL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHandL[k].GetComponent<Renderer>().enabled = false;
			gHandL[k].layer = 8;
			mHandL[k] = gHandL[k].AddComponent<MouseDrag>();
			mHandL[k].obj = gHandL[k];
			gArmL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gArmL[k].GetComponent<Renderer>().enabled = false;
			gArmL[k].layer = 8;
			mArmL[k] = gArmL[k].AddComponent<MouseDrag>();
			mArmL[k].obj = gArmL[k];
			gFootL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gFootL[k].GetComponent<Renderer>().enabled = false;
			gFootL[k].layer = 8;
			mFootL[k] = gFootL[k].AddComponent<MouseDrag>();
			mFootL[k].obj = gFootL[k];
			gHizaL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHizaL[k].GetComponent<Renderer>().enabled = false;
			gHizaL[k].layer = 8;
			mHizaL[k] = gHizaL[k].AddComponent<MouseDrag>();
			mHizaL[k].obj = gHizaL[k];
			gHandR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHandR[k].GetComponent<Renderer>().enabled = false;
			gHandR[k].layer = 8;
			mHandR[k] = gHandR[k].AddComponent<MouseDrag>();
			mHandR[k].obj = gHandR[k];
			gArmR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gArmR[k].GetComponent<Renderer>().enabled = false;
			gArmR[k].layer = 8;
			mArmR[k] = gArmR[k].AddComponent<MouseDrag>();
			mArmR[k].obj = gArmR[k];
			gFootR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gFootR[k].GetComponent<Renderer>().enabled = false;
			gFootR[k].layer = 8;
			mFootR[k] = gFootR[k].AddComponent<MouseDrag>();
			mFootR[k].obj = gFootR[k];
			gHizaR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gHizaR[k].GetComponent<Renderer>().enabled = false;
			gHizaR[k].layer = 8;
			mHizaR[k] = gHizaR[k].AddComponent<MouseDrag>();
			mHizaR[k].obj = gHizaR[k];
			gClavicleL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gClavicleL[k].GetComponent<Renderer>().enabled = false;
			gClavicleL[k].layer = 8;
			mClavicleL[k] = gClavicleL[k].AddComponent<MouseDrag>();
			mClavicleL[k].obj = gClavicleL[k];
			gClavicleR[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gClavicleR[k].GetComponent<Renderer>().enabled = false;
			gClavicleR[k].layer = 8;
			mClavicleR[k] = gClavicleR[k].AddComponent<MouseDrag>();
			mClavicleR[k].obj = gClavicleR[k];
			if (!isBone[k])
			{
				gHandL[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
				gHandR[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
				gArmL[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
				gArmR[k].transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
				gFootL[k].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				gFootR[k].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				gHizaL[k].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				gHizaR[k].transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
				gClavicleL[k].transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
				mClavicleR[k].transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
			}
			else
			{
				gHandL[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gHandR[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gArmL[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gArmR[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gFootL[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gFootR[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gHizaL[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gHizaR[k].transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
				gClavicleL[k].transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
				gClavicleR[k].transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
				gHandL[k].GetComponent<Renderer>().enabled = true;
				gHandR[k].GetComponent<Renderer>().enabled = true;
				gArmL[k].GetComponent<Renderer>().enabled = true;
				gArmR[k].GetComponent<Renderer>().enabled = true;
				gFootL[k].GetComponent<Renderer>().enabled = true;
				gFootR[k].GetComponent<Renderer>().enabled = true;
				gHizaL[k].GetComponent<Renderer>().enabled = true;
				gHizaR[k].GetComponent<Renderer>().enabled = true;
				gClavicleL[k].GetComponent<Renderer>().enabled = true;
				gClavicleR[k].GetComponent<Renderer>().enabled = true;
				gHandL[k].GetComponent<Renderer>().material = m_material2;
				gHandR[k].GetComponent<Renderer>().material = m_material2;
				gArmL[k].GetComponent<Renderer>().material = m_material2;
				gArmR[k].GetComponent<Renderer>().material = m_material2;
				gFootL[k].GetComponent<Renderer>().material = m_material2;
				gFootR[k].GetComponent<Renderer>().material = m_material2;
				gHizaL[k].GetComponent<Renderer>().material = m_material2;
				gHizaR[k].GetComponent<Renderer>().material = m_material2;
				gClavicleL[k].GetComponent<Renderer>().material = m_material2;
				gClavicleR[k].GetComponent<Renderer>().material = m_material2;
			}
			GameObject gameObject = new GameObject();
			gameObject.transform.SetParent(base.transform, false);
			gizmoHandL[k] = gameObject.AddComponent<GizmoRender>();
			gizmoHandL[k].eRotate = true;
			gizmoHandL[k].offsetScale = 0.25f;
			gizmoHandL[k].lineRSelectedThick = 0.25f;
			gizmoHandL[k].Visible = false;
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.SetParent(base.transform, false);
			gizmoHandR[k] = gameObject2.AddComponent<GizmoRender>();
			gizmoHandR[k].eRotate = true;
			gizmoHandR[k].offsetScale = 0.25f;
			gizmoHandR[k].lineRSelectedThick = 0.25f;
			gizmoHandR[k].Visible = false;
			GameObject gameObject3 = new GameObject();
			gameObject3.transform.SetParent(base.transform, false);
			gizmoFootL[k] = gameObject3.AddComponent<GizmoRender>();
			gizmoFootL[k].eRotate = true;
			gizmoFootL[k].offsetScale = 0.25f;
			gizmoFootL[k].lineRSelectedThick = 0.25f;
			gizmoFootL[k].Visible = false;
			GameObject gameObject4 = new GameObject();
			gameObject4.transform.SetParent(base.transform, false);
			gizmoFootR[k] = gameObject4.AddComponent<GizmoRender>();
			gizmoFootR[k].eRotate = true;
			gizmoFootR[k].offsetScale = 0.25f;
			gizmoFootR[k].lineRSelectedThick = 0.25f;
			gizmoFootR[k].Visible = false;
			gNeck[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gNeck[k].GetComponent<Renderer>().material = material;
			gNeck[k].layer = 8;
			mNeck[k] = gNeck[k].AddComponent<MouseDrag3>();
			mNeck[k].obj = gNeck[k];
			gNeck[k].transform.localScale = new Vector3(0.055f, 0.055f, 0.055f);
			gNeck[k].SetActive(false);
			gSpine[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gSpine[k].GetComponent<Renderer>().material = material;
			gSpine[k].layer = 8;
			mSpine[k] = gSpine[k].AddComponent<MouseDrag3>();
			mSpine[k].obj = gSpine[k];
			gSpine[k].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
			gSpine[k].SetActive(false);
			gSpine0a[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gSpine0a[k].GetComponent<Renderer>().material = material;
			gSpine0a[k].layer = 8;
			mSpine0a[k] = gSpine0a[k].AddComponent<MouseDrag3>();
			mSpine0a[k].obj = gSpine0a[k];
			gSpine0a[k].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
			gSpine0a[k].SetActive(false);
			gSpine1a[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gSpine1a[k].GetComponent<Renderer>().material = material;
			gSpine1a[k].layer = 8;
			mSpine1a[k] = gSpine1a[k].AddComponent<MouseDrag3>();
			mSpine1a[k].obj = gSpine1a[k];
			gSpine1a[k].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
			gSpine1a[k].SetActive(false);
			gSpine1[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gSpine1[k].GetComponent<Renderer>().material = material;
			gSpine1[k].layer = 8;
			mSpine1[k] = gSpine1[k].AddComponent<MouseDrag3>();
			mSpine1[k].obj = gSpine1[k];
			gSpine1[k].transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
			gSpine1[k].SetActive(false);
			gPelvis[k] = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gPelvis[k].GetComponent<Renderer>().material = m_material3;
			gPelvis[k].layer = 8;
			mPelvis[k] = gPelvis[k].AddComponent<MouseDrag3>();
			mPelvis[k].obj = gPelvis[k];
			gPelvis[k].transform.localScale = new Vector3(0.045f, 0.045f, 0.045f);
			gPelvis[k].SetActive(false);
		}

		private void SetIK(Maid maid, int i)
		{
			Head = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Neck", true);
			Head1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Neck", true);
			Head2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Head", true);
			Head3[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 HeadNub", true);
			IKHandL[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handL", true);
			IKHandR[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "_IK_handR", true);
			IKMuneL[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_L", true);
			IKMuneLSub[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_L_sub", true);
			IKMuneR[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_R", true);
			IKMuneRSub[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Mune_R_sub", true);
			Spine = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine", true);
			Spine0a = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine0a", true);
			Spine1 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1", true);
			Spine1a = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1a", true);
			Pelvis = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Pelvis", true);
			Neck[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Neck", true);
			Pelvis2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
			Spine12[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1", true);
			Spine0a2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine0a", true);
			Spine2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine", true);
			Spine1a2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 Spine1a", true);
			HandL1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Hand", true);
			UpperArmL1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L UpperArm", true);
			ForearmL1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Forearm", true);
			HandR1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Hand", true);
			UpperArmR1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R UpperArm", true);
			ForearmR1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Forearm", true);
			HandL2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Foot", true);
			UpperArmL2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Thigh", true);
			ForearmL2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Calf", true);
			HandR2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Foot", true);
			UpperArmR2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Thigh", true);
			ForearmR2[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Calf", true);
			ClavicleL1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Clavicle", true);
			ClavicleR1[i] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Clavicle", true);
			Finger[i, 0] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger0", true);
			Finger[i, 1] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger01", true);
			Finger[i, 2] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger02", true);
			Finger[i, 3] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger0Nub", true);
			Finger[i, 4] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger1", true);
			Finger[i, 5] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger11", true);
			Finger[i, 6] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger12", true);
			Finger[i, 7] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger1Nub", true);
			Finger[i, 8] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger2", true);
			Finger[i, 9] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger21", true);
			Finger[i, 10] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger22", true);
			Finger[i, 11] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger2Nub", true);
			Finger[i, 12] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger3", true);
			Finger[i, 13] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger31", true);
			Finger[i, 14] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger32", true);
			Finger[i, 15] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger3Nub", true);
			Finger[i, 16] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger4", true);
			Finger[i, 17] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger41", true);
			Finger[i, 18] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger42", true);
			Finger[i, 19] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Finger4Nub", true);
			Finger[i, 20] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger0", true);
			Finger[i, 21] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger01", true);
			Finger[i, 22] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger02", true);
			Finger[i, 23] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger0Nub", true);
			Finger[i, 24] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger1", true);
			Finger[i, 25] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger11", true);
			Finger[i, 26] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger12", true);
			Finger[i, 27] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger1Nub", true);
			Finger[i, 28] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger2", true);
			Finger[i, 29] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger21", true);
			Finger[i, 30] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger22", true);
			Finger[i, 31] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger2Nub", true);
			Finger[i, 32] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger3", true);
			Finger[i, 33] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger31", true);
			Finger[i, 34] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger32", true);
			Finger[i, 35] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger3Nub", true);
			Finger[i, 36] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger4", true);
			Finger[i, 37] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger41", true);
			Finger[i, 38] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger42", true);
			Finger[i, 39] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Finger4Nub", true);
			Finger2[i, 0] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
			Finger2[i, 1] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
			Finger2[i, 2] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
			Finger2[i, 3] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
			Finger2[i, 4] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
			Finger2[i, 5] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
			Finger2[i, 6] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
			Finger2[i, 7] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
			Finger2[i, 8] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
			Finger2[i, 9] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
			Finger2[i, 10] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
			Finger2[i, 11] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
			Finger2[i, 12] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
			Finger2[i, 13] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
			Finger2[i, 14] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
			Finger2[i, 15] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
			Finger2[i, 16] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
			Finger2[i, 17] = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
		}

		private void CopyIK(Maid maid, int i, Maid maid2, int i2)
		{
			poseIndex[i] = poseIndex[i2];
			if (maid && maid.Visible)
			{
				string[] array = poseArray[poseIndex[i]].Split(new char[]
				{
					','
				});
				isStop[i] = false;
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
				int num2;
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
						string fileName = Path.GetFileName(path);
						long num = (long)fileName.GetHashCode();
						maid.body0.CrossFade(num.ToString(), array2, false, false, false, 0f, 1f);
						Maid.AutoTwist[] array3 = new Maid.AutoTwist[]
						{
							default(Maid.AutoTwist),
							Maid.AutoTwist.ShoulderR,
							Maid.AutoTwist.WristL,
							Maid.AutoTwist.WristR,
							Maid.AutoTwist.ThighL,
							Maid.AutoTwist.ThighR
						};
						for (int j = 0; j < array3.Length; j++)
						{
							maid.SetAutoTwist(array3[j], true);
						}
					}
				}
				else if (array[0].StartsWith("p") && int.TryParse(array[0].Substring(1), out num2))
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
						Transform transform = CMT.SearchObjName(maidArray[i].body0.m_Bones.transform, "Bip01", true);
						isPoseIti[i] = true;
						poseIti[i] = maidArray[i].transform.position;
						maidArray[i].transform.position = new Vector3(100f, 100f, 100f);
					}
				}
			}
		}

		private void CopyIK2(Maid maid, int i, Maid maid2, int i2)
		{
			isStop[i] = true;
			isLock[i] = true;
			maid.transform.position = new Vector3(maid.transform.position.x, maid2.transform.position.y, maid.transform.position.z);
			SetIK(maid2, i2);
			Transform transform = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe0", true);
			Transform transform2 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe0", true);
			Transform transform3 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe01", true);
			Transform transform4 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe01", true);
			Transform transform5 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
			Transform transform6 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
			Transform transform7 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe1", true);
			Transform transform8 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe1", true);
			Transform transform9 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe11", true);
			Transform transform10 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe11", true);
			Transform transform11 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
			Transform transform12 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
			Transform transform13 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe2", true);
			Transform transform14 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe2", true);
			Transform transform15 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe21", true);
			Transform transform16 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe21", true);
			Transform transform17 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
			Transform transform18 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
			Transform transform19 = CMT.SearchObjName(maid2.body0.m_Bones.transform, "Bip01", true);
			Vector3 eulerAngles = Head.eulerAngles;
			Vector3 eulerAngles2 = Spine.eulerAngles;
			Vector3 eulerAngles3 = Spine0a.eulerAngles;
			Vector3 eulerAngles4 = Spine1.eulerAngles;
			Vector3 eulerAngles5 = Spine1a.eulerAngles;
			Vector3 eulerAngles6 = Pelvis.eulerAngles;
			Vector3 eulerAngles7 = ClavicleL1[i2].eulerAngles;
			Vector3 eulerAngles8 = ClavicleR1[i2].eulerAngles;
			Vector3 localEulerAngles = transform.localEulerAngles;
			Vector3 localEulerAngles2 = transform2.localEulerAngles;
			Vector3 localEulerAngles3 = transform3.localEulerAngles;
			Vector3 localEulerAngles4 = transform4.localEulerAngles;
			Vector3 localEulerAngles5 = transform5.localEulerAngles;
			Vector3 localEulerAngles6 = transform6.localEulerAngles;
			Vector3 localEulerAngles7 = transform7.localEulerAngles;
			Vector3 localEulerAngles8 = transform8.localEulerAngles;
			Vector3 localEulerAngles9 = transform9.localEulerAngles;
			Vector3 localEulerAngles10 = transform10.localEulerAngles;
			Vector3 localEulerAngles11 = transform11.localEulerAngles;
			Vector3 localEulerAngles12 = transform12.localEulerAngles;
			Vector3 localEulerAngles13 = transform13.localEulerAngles;
			Vector3 localEulerAngles14 = transform14.localEulerAngles;
			Vector3 localEulerAngles15 = transform15.localEulerAngles;
			Vector3 localEulerAngles16 = transform16.localEulerAngles;
			Vector3 localEulerAngles17 = transform17.localEulerAngles;
			Vector3 localEulerAngles18 = transform18.localEulerAngles;
			Vector3 eulerAngles9 = transform19.eulerAngles;
			Vector3 localEulerAngles19 = HandL1[i2].localEulerAngles;
			Vector3 localEulerAngles20 = HandR1[i2].localEulerAngles;
			Vector3 localEulerAngles21 = HandL2[i2].localEulerAngles;
			Vector3 localEulerAngles22 = HandR2[i2].localEulerAngles;
			Vector3[] array = new Vector3[40];
			for (int j = 0; j < 20; j++)
			{
				array[j] = Finger[i2, j].localEulerAngles;
			}
			for (int j = 20; j < 40; j++)
			{
				array[j] = Finger[i2, j].localEulerAngles;
			}
			Vector3 eulerAngles10 = UpperArmL1[i2].eulerAngles;
			Vector3 eulerAngles11 = ForearmL1[i2].eulerAngles;
			Vector3 eulerAngles12 = UpperArmR1[i2].eulerAngles;
			Vector3 eulerAngles13 = ForearmR1[i2].eulerAngles;
			Vector3 eulerAngles14 = UpperArmL2[i2].eulerAngles;
			Vector3 eulerAngles15 = ForearmL2[i2].eulerAngles;
			Vector3 eulerAngles16 = UpperArmR2[i2].eulerAngles;
			Vector3 eulerAngles17 = ForearmR2[i2].eulerAngles;
			maid.transform.localEulerAngles = maid2.transform.localEulerAngles;
			SetIK(maid, i);
			transform = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
			transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
			transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
			transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
			transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
			transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
			transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
			transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
			transform9 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
			transform10 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
			transform11 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
			transform12 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
			transform13 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
			transform14 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
			transform15 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
			transform16 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
			transform17 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
			transform18 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
			transform19 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
			transform19.eulerAngles = eulerAngles9;
			Pelvis.eulerAngles = eulerAngles6;
			Spine.eulerAngles = eulerAngles2;
			Spine0a.eulerAngles = eulerAngles3;
			Spine1.eulerAngles = eulerAngles4;
			Spine1a.eulerAngles = eulerAngles5;
			ClavicleL1[i].eulerAngles = eulerAngles7;
			ClavicleR1[i].eulerAngles = eulerAngles8;
			Head.eulerAngles = eulerAngles;
			HandL1[i].localEulerAngles = localEulerAngles19;
			HandR1[i].localEulerAngles = localEulerAngles20;
			HandL2[i].localEulerAngles = localEulerAngles21;
			HandR2[i].localEulerAngles = localEulerAngles22;
			for (int j = 0; j < 20; j++)
			{
				Finger[i, j].localEulerAngles = array[j];
			}
			for (int j = 20; j < 40; j++)
			{
				Finger[i, j].localEulerAngles = array[j];
			}
			transform.localEulerAngles = localEulerAngles;
			transform2.localEulerAngles = localEulerAngles2;
			transform3.localEulerAngles = localEulerAngles3;
			transform4.localEulerAngles = localEulerAngles4;
			transform5.localEulerAngles = localEulerAngles5;
			transform6.localEulerAngles = localEulerAngles6;
			transform7.localEulerAngles = localEulerAngles7;
			transform8.localEulerAngles = localEulerAngles8;
			transform9.localEulerAngles = localEulerAngles9;
			transform10.localEulerAngles = localEulerAngles10;
			transform11.localEulerAngles = localEulerAngles11;
			transform12.localEulerAngles = localEulerAngles12;
			transform13.localEulerAngles = localEulerAngles13;
			transform14.localEulerAngles = localEulerAngles14;
			transform15.localEulerAngles = localEulerAngles15;
			transform16.localEulerAngles = localEulerAngles16;
			transform17.localEulerAngles = localEulerAngles17;
			transform18.localEulerAngles = localEulerAngles18;
			UpperArmL1[i].eulerAngles = eulerAngles10;
			UpperArmR1[i].eulerAngles = eulerAngles12;
			ForearmL1[i].eulerAngles = eulerAngles11;
			ForearmR1[i].eulerAngles = eulerAngles13;
			UpperArmL2[i].eulerAngles = eulerAngles14;
			UpperArmR2[i].eulerAngles = eulerAngles16;
			ForearmL2[i].eulerAngles = eulerAngles15;
			ForearmR2[i].eulerAngles = eulerAngles17;
		}

		private void SetHanten(Maid maid, int i)
		{
			SetIK(maid, i);
			int num = pHandL[selectMaidIndex];
			pHandL[selectMaidIndex] = pHandR[selectMaidIndex];
			pHandR[selectMaidIndex] = num;
			isStop[i] = true;
			isLock[i] = true;
			Transform transform = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_L", true);
			Transform transform2 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Hip_R", true);
			Transform transform3 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0", true);
			Transform transform4 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0", true);
			Transform transform5 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe01", true);
			Transform transform6 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe01", true);
			Transform transform7 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe0Nub", true);
			Transform transform8 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe0Nub", true);
			Transform transform9 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1", true);
			Transform transform10 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1", true);
			Transform transform11 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe11", true);
			Transform transform12 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe11", true);
			Transform transform13 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe1Nub", true);
			Transform transform14 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe1Nub", true);
			Transform transform15 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2", true);
			Transform transform16 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2", true);
			Transform transform17 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe21", true);
			Transform transform18 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe21", true);
			Transform transform19 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 L Toe2Nub", true);
			Transform transform20 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01 R Toe2Nub", true);
			Transform transform21 = CMT.SearchObjName(maid.body0.m_Bones.transform, "Bip01", true);
			Vector3 eulerAngles = Head.eulerAngles;
			Vector3 eulerAngles2 = Spine.eulerAngles;
			Vector3 eulerAngles3 = Spine0a.eulerAngles;
			Vector3 eulerAngles4 = Spine1.eulerAngles;
			Vector3 eulerAngles5 = Spine1a.eulerAngles;
			Vector3 eulerAngles6 = Pelvis.eulerAngles;
			Vector3 eulerAngles7 = ClavicleL1[i].eulerAngles;
			Vector3 eulerAngles8 = ClavicleR1[i].eulerAngles;
			Vector3 eulerAngles9 = transform.eulerAngles;
			Vector3 eulerAngles10 = transform2.eulerAngles;
			Vector3 localEulerAngles = transform3.localEulerAngles;
			Vector3 localEulerAngles2 = transform4.localEulerAngles;
			Vector3 localEulerAngles3 = transform5.localEulerAngles;
			Vector3 localEulerAngles4 = transform6.localEulerAngles;
			Vector3 localEulerAngles5 = transform7.localEulerAngles;
			Vector3 localEulerAngles6 = transform8.localEulerAngles;
			Vector3 localEulerAngles7 = transform9.localEulerAngles;
			Vector3 localEulerAngles8 = transform10.localEulerAngles;
			Vector3 localEulerAngles9 = transform11.localEulerAngles;
			Vector3 localEulerAngles10 = transform12.localEulerAngles;
			Vector3 localEulerAngles11 = transform13.localEulerAngles;
			Vector3 localEulerAngles12 = transform14.localEulerAngles;
			Vector3 localEulerAngles13 = transform15.localEulerAngles;
			Vector3 localEulerAngles14 = transform16.localEulerAngles;
			Vector3 localEulerAngles15 = transform17.localEulerAngles;
			Vector3 localEulerAngles16 = transform18.localEulerAngles;
			Vector3 localEulerAngles17 = transform19.localEulerAngles;
			Vector3 localEulerAngles18 = transform20.localEulerAngles;
			Vector3 eulerAngles11 = transform21.eulerAngles;
			Vector3 localEulerAngles19 = HandL1[i].localEulerAngles;
			Vector3 localEulerAngles20 = HandR1[i].localEulerAngles;
			Vector3 localEulerAngles21 = HandL2[i].localEulerAngles;
			Vector3 localEulerAngles22 = HandR2[i].localEulerAngles;
			Vector3[] array = new Vector3[40];
			for (int j = 0; j < 20; j++)
			{
				array[j] = Finger[i, j].localEulerAngles;
			}
			for (int j = 20; j < 40; j++)
			{
				array[j] = Finger[i, j].localEulerAngles;
			}
			Vector3 eulerAngles12 = UpperArmL1[i].eulerAngles;
			Vector3 eulerAngles13 = ForearmL1[i].eulerAngles;
			Vector3 eulerAngles14 = UpperArmR1[i].eulerAngles;
			Vector3 eulerAngles15 = ForearmR1[i].eulerAngles;
			Vector3 eulerAngles16 = UpperArmL2[i].eulerAngles;
			Vector3 eulerAngles17 = ForearmL2[i].eulerAngles;
			Vector3 eulerAngles18 = UpperArmR2[i].eulerAngles;
			Vector3 eulerAngles19 = ForearmR2[i].eulerAngles;
			transform21.eulerAngles = new Vector3(360f - (eulerAngles11.x + 270f) - 270f, 360f - (eulerAngles11.y + 90f) - 90f, 360f - eulerAngles11.z);
			transform.eulerAngles = getHanten(eulerAngles10);
			transform2.eulerAngles = getHanten(eulerAngles9);
			Pelvis.eulerAngles = getHanten(eulerAngles6);
			Spine.eulerAngles = getHanten(eulerAngles2);
			Spine0a.eulerAngles = getHanten(eulerAngles3);
			Spine1.eulerAngles = getHanten(eulerAngles4);
			Spine1a.eulerAngles = getHanten(eulerAngles5);
			ClavicleL1[i].eulerAngles = getHanten(eulerAngles8);
			ClavicleR1[i].eulerAngles = getHanten(eulerAngles7);
			Head.eulerAngles = getHanten(eulerAngles);
			HandR1[i].localEulerAngles = getHanten2(localEulerAngles19);
			HandL1[i].localEulerAngles = getHanten2(localEulerAngles20);
			HandR2[i].localEulerAngles = getHanten2(localEulerAngles21);
			HandL2[i].localEulerAngles = getHanten2(localEulerAngles22);
			for (int j = 0; j < 20; j++)
			{
				Finger[i, j].localEulerAngles = getHanten2(array[j + 20]);
			}
			for (int j = 20; j < 40; j++)
			{
				Finger[i, j].localEulerAngles = getHanten2(array[j - 20]);
			}
			transform4.localEulerAngles = getHanten2(localEulerAngles);
			transform3.localEulerAngles = getHanten2(localEulerAngles2);
			transform6.localEulerAngles = getHanten2(localEulerAngles3);
			transform5.localEulerAngles = getHanten2(localEulerAngles4);
			transform8.localEulerAngles = getHanten2(localEulerAngles5);
			transform7.localEulerAngles = getHanten2(localEulerAngles6);
			transform10.localEulerAngles = getHanten2(localEulerAngles7);
			transform9.localEulerAngles = getHanten2(localEulerAngles8);
			transform12.localEulerAngles = getHanten2(localEulerAngles9);
			transform11.localEulerAngles = getHanten2(localEulerAngles10);
			transform14.localEulerAngles = getHanten2(localEulerAngles11);
			transform13.localEulerAngles = getHanten2(localEulerAngles12);
			transform16.localEulerAngles = getHanten2(localEulerAngles13);
			transform15.localEulerAngles = getHanten2(localEulerAngles14);
			transform18.localEulerAngles = getHanten2(localEulerAngles15);
			transform17.localEulerAngles = getHanten2(localEulerAngles16);
			transform20.localEulerAngles = getHanten2(localEulerAngles17);
			transform19.localEulerAngles = getHanten2(localEulerAngles18);
			UpperArmR1[i].eulerAngles = getHanten(eulerAngles12);
			UpperArmL1[i].eulerAngles = getHanten(eulerAngles14);
			ForearmR1[i].eulerAngles = getHanten(eulerAngles13);
			ForearmL1[i].eulerAngles = getHanten(eulerAngles15);
			UpperArmR2[i].eulerAngles = getHanten(eulerAngles16);
			UpperArmL2[i].eulerAngles = getHanten(eulerAngles18);
			ForearmR2[i].eulerAngles = getHanten(eulerAngles17);
			ForearmL2[i].eulerAngles = getHanten(eulerAngles19);
		}
	}
}
