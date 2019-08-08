using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jhrino.MFPLevelEditor
{
	// Token: 0x020000E0 RID: 224
	public class LevelEditorHandler : MonoBehaviour
	{
		// Token: 0x060006AA RID: 1706
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				this.PlayMode();
			}
			if (Input.GetMouseButtonDown(0))
			{

			}
		}

		// Token: 0x060006AB RID: 1707
		public LevelEditorHandler()
		{
		}

		// Token: 0x060006AC RID: 1708
		private void Start()
		{
			this.HUD = GameObject.Find("HUD/Canvas");
			this.Player = GameObject.Find("Player");
			this.Camera = GameObject.Find("Main Camera");
			this.Player.SetActive(false);
			this.Camera.SetActive(false);
			this.HUD.GetComponentInChildren<Canvas>().enabled = false;
			GameObject gameObject = new GameObject();
			gameObject.name = "EditorCamera";
			this.EditorCamera = gameObject.AddComponent<Camera>();
			this.EditorCamera.depth = -1f;
			this.EditorCamera.clearFlags = CameraClearFlags.Depth;
			this.EditorCamera.transform.position = this.Player.transform.position;
			GameObject gameObject2 = new GameObject();
			gameObject2.transform.position = new Vector3(0f, 0f, -15f);
			CameraMovement cameraMovement = gameObject2.AddComponent<CameraMovement>();
			cameraMovement.normalSpeed = 5f;
			cameraMovement.runSpeed = 5f;
			cameraMovement.doMove = true;
			cameraMovement.crouch = false;
			cameraMovement.cam = gameObject.transform;
			gameObject.transform.parent = gameObject2.transform;
			this.SpawnEditorHud();
			LevelEditorHandler.Inst = this;
			GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject3.transform.localScale = new Vector3(gameObject3.transform.localScale.x, 4f, gameObject3.transform.localScale.x);
			gameObject3.name = "PlayerSpawn";
			this.PlayerSpawn = gameObject3.transform;
			this.PlayerSpawn.transform.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.5f, 1f);
			this.PlayerSpawn.transform.position = this.Player.transform.position;
			this.PlayMod = false;
		}

		// Token: 0x060006AD RID: 1709
		public void ChangeCameraState(LevelEditorHandler.CameraMode state, bool editorCamera)
		{
			if (state != LevelEditorHandler.CameraMode.Disabled)
			{
				if (state != LevelEditorHandler.CameraMode.Enabled)
				{
					return;
				}
				if (!this.EditorCamera)
				{
					this.Camera.GetComponent<Camera>().enabled = true;
					return;
				}
				this.EditorCamera.enabled = true;
				return;
			}
			else
			{
				if (!this.EditorCamera)
				{
					this.Camera.GetComponent<Camera>().enabled = false;
					return;
				}
				this.EditorCamera.enabled = true;
				return;
			}
		}

		// Token: 0x060006AE RID: 1710
		public void SpawnEditorHud()
		{
			GameObject gameObject = new GameObject();
			gameObject.layer = 5;
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0.5f, 0f);
			rectTransform.anchorMax = new Vector2(0.5f, 0f);
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.pixelPerfect = false;
			canvas.targetDisplay = 0;
			canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
			CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
			canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
			canvasScaler.scaleFactor = 0.5f;
			canvasScaler.referencePixelsPerUnit = 100f;
			gameObject.AddComponent<GraphicRaycaster>().ignoreReversedGraphics = true;
			GameObject gameObject4 = new GameObject();
			gameObject4.transform.parent = gameObject.transform;
			gameObject4.layer = 5;
			gameObject4.AddComponent<RectTransform>();
			gameObject4.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject4.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
			gameObject4.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
			Text text = gameObject4.AddComponent<Text>();
			Material material = new Material(Shader.Find("UI/Default"));
			text.material = material;
			text.text = "GAME OVER";
			text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
			text.fontStyle = FontStyle.Bold;
			text.fontSize = 68;
			text.lineSpacing = 1f;
			text.supportRichText = false;
			text.alignment = TextAnchor.MiddleCenter;
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.resizeTextForBestFit = false;
			text.alignByGeometry = false;
			text.GetComponent<RectTransform>().sizeDelta = new Vector2(472.9f, 100f);
			this.DebugObject = text;
			gameObject4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-45.6f, 83f);
			new GameObject();
			GameObject gameObject2 = new GameObject();
			gameObject2.name = "CoordinateField";
			gameObject2.AddComponent<RectTransform>();
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.layer = 5;
			gameObject2.AddComponent<RectTransform>();
			gameObject2.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject2.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
			gameObject2.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
			gameObject2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20f, 173.8f);
			Image image = gameObject2.AddComponent<Image>();
			image.sprite = (Resources.GetBuiltinResource(typeof(Sprite), "InputFieldBackground") as Sprite);
			image.raycastTarget = true;
			InputField inputField = gameObject2.AddComponent<InputField>();
			this.CoordinateField = inputField;
			inputField.interactable = true;
			inputField.transition = Selectable.Transition.ColorTint;
			inputField.targetGraphic = image;
			inputField.textComponent = this.DebugObject;
			inputField.contentType = InputField.ContentType.Standard;
			inputField.lineType = InputField.LineType.SingleLine;
			inputField.caretBlinkRate = 0.85f;
			inputField.caretWidth = 1;
			inputField.shouldHideMobileInput = true;
			inputField.readOnly = false;
			gameObject2.transform.parent = gameObject.transform;
			this.DebugObject.transform.parent = gameObject2.transform;
			inputField.gameObject.SetActive(false);
			GameObject gameObject3 = new GameObject();
			gameObject3.transform.parent = gameObject.transform;
			gameObject3.layer = 5;
			gameObject3.AddComponent<RectTransform>();
			gameObject3.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject3.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
			gameObject3.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			Image targetGraphic = gameObject3.AddComponent<Image>();
			gameObject3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128f, -122f);
			Button button = gameObject3.AddComponent<Button>();
			button.transition = Selectable.Transition.ColorTint;
			button.targetGraphic = targetGraphic;
			button.interactable = true;
			gameObject3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128f, -122f);
			button.onClick.AddListener(delegate()
			{
				this.ChangeEditType("position");
			});
			GameObject gameObject5 = new GameObject();
			gameObject5.transform.parent = gameObject.transform;
			gameObject5.layer = 5;
			gameObject5.AddComponent<RectTransform>();
			gameObject5.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject5.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
			gameObject5.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			Image targetGraphic2 = gameObject5.AddComponent<Image>();
			gameObject5.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128f, -255.9f);
			Button button2 = gameObject5.AddComponent<Button>();
			button2.transition = Selectable.Transition.ColorTint;
			button2.targetGraphic = targetGraphic2;
			button2.interactable = true;
			button2.onClick.AddListener(delegate()
			{
				this.ChangeEditType("rotation");
			});
			GameObject gameObject6 = new GameObject();
			gameObject6.transform.parent = gameObject.transform;
			gameObject6.layer = 5;
			gameObject6.AddComponent<RectTransform>();
			gameObject6.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject6.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
			gameObject6.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			Image targetGraphic3 = gameObject6.AddComponent<Image>();
			gameObject6.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128f, -400f);
			Button button3 = gameObject6.AddComponent<Button>();
			button3.transition = Selectable.Transition.ColorTint;
			button3.targetGraphic = targetGraphic3;
			button3.interactable = true;
			button3.onClick.AddListener(delegate()
			{
				this.ChangeEditType("scale");
			});
			GameObject gameObject7 = new GameObject();
			gameObject7.transform.parent = gameObject.transform;
			gameObject7.layer = 5;
			gameObject7.AddComponent<RectTransform>();
			gameObject7.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
			gameObject7.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
			gameObject7.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
			gameObject7.AddComponent<Image>();
			gameObject7.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128f, -550f);
			Button button4 = gameObject7.AddComponent<Button>();
			button4.transition = Selectable.Transition.ColorTint;
			button4.targetGraphic = targetGraphic3;
			button4.interactable = true;
			button4.onClick.AddListener(delegate()
			{
				this.ChangeEditType("color");
			});
		}

		// Token: 0x060006AF RID: 1711
		public void EditingMode()
		{
			if (Input.GetKeyDown(KeyCode.Space) && this.EditingObject != this.PlayerSpawn.gameObject)
			{
				UnityEngine.Object.Destroy(this.EditingObject);
				this.Editing = false;
				this.EditingObject = null;
				this.CoordinateField.DeactivateInputField();
				this.CoordinateField.gameObject.SetActive(false);
			}
			if (!this.CoordinateField.isFocused)
			{
				this.ExitEditMode();
			}
			if (this.CoordinateField.isFocused)
			{
				this.UpdateCoordinates(this.editType, this.CoordinateField.text);
			}
		}

		// Token: 0x060006B1 RID: 1713
		public void UpdateCoordinates(string type, string coordinates)
		{
			if (this.EditingObject != null)
			{
				coordinates = coordinates.Replace("(", "");
				coordinates = coordinates.Replace(")", "");
				coordinates = coordinates.Replace(",", "");
				if (type == "color")
				{
					coordinates = coordinates.Replace("RGBA", "");
				}
				float num = float.Parse(coordinates.Split(new char[]
				{
					' '
				})[0]);
				float num2 = float.Parse(coordinates.Split(new char[]
				{
					' '
				})[1]);
				float num3 = float.Parse(coordinates.Split(new char[]
				{
					' '
				})[2]);
				if (type == "position")
				{
					this.EditingObject.transform.position = new Vector3(num, num2, num3);
				}
				if (type == "rotation")
				{
					this.EditingObject.transform.eulerAngles = new Vector3(num, num2, num3);
				}
				if (type == "scale")
				{
					this.EditingObject.transform.localScale = new Vector3(num, num2, num3);
				}
				if (type == "color")
				{
					this.EditingObject.GetComponent<MeshRenderer>().material.color = new Color(num, num2, num3, 1f);
				}
			}
		}

		// Token: 0x060006B2 RID: 1714
		public void ChangeEditType(string type)
		{
			this.ExitEditMode();
			this.editType = type;
		}

		// Token: 0x060006B3 RID: 1715
		public void EnterEditMode(GameObject gobject)
		{
			this.EditingObject = gobject;
			this.CoordinateField.gameObject.SetActive(true);
			this.CoordinateField.ActivateInputField();
			if (this.editType == "position")
			{
				this.CoordinateField.text = gobject.transform.position.ToString();
			}
			if (this.editType == "rotation")
			{
				if (this.EditingObject == this.PlayerSpawn.gameObject || this.EnemySpawnpoints.Contains(this.EditingObject.transform))
				{
					this.ExitEditMode();
					this.Editing = false;
				}
				this.CoordinateField.text = gobject.transform.rotation.ToString();
			}
			if (this.editType == "scale")
			{
				if (this.EditingObject == this.PlayerSpawn.gameObject || this.EnemySpawnpoints.Contains(this.EditingObject.transform))
				{
					this.ExitEditMode();
					this.Editing = false;
				}
				this.CoordinateField.text = gobject.transform.localScale.ToString();
			}
			if (this.editType == "color")
			{
				if (this.EditingObject == this.PlayerSpawn.gameObject || this.EnemySpawnpoints.Contains(this.EditingObject.transform))
				{
					this.ExitEditMode();
					this.Editing = false;
				}
				this.CoordinateField.text = gobject.GetComponent<MeshRenderer>().material.color.ToString();
				this.CoordinateField.text = this.CoordinateField.text.Replace("RGBA", "");
			}
			this.Editing = true;
		}

		// Token: 0x060006B4 RID: 1716
		public void PlayMode()
		{
			this.Player.transform.position = this.PlayerSpawn.transform.position;
			if (!this.PlayMod)
			{
				this.PlayerSpawn.GetComponent<SphereCollider>().enabled = false;
				this.HUD.GetComponentInChildren<Canvas>().enabled = true;
				this.Player.SetActive(true);
				this.Camera.SetActive(true);
				this.CoordinateField.DeactivateInputField();
				this.CoordinateField.gameObject.SetActive(false);
				this.EditorCamera.enabled = false;
				this.EditorCamera.transform.parent.GetComponent<CameraMovement>().enabled = false;
				this.Editing = false;
				this.EditingObject = null;
				this.PlayMod = true;
				this.fart = true;
				foreach (Transform transform in this.EnemySpawnpoints)
				{
					transform.GetComponent<SphereCollider>().enabled = false;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EnemySample);
					gameObject.transform.position = transform.position;
					gameObject.SetActive(true);
				}
				return;
			}
			if (this.PlayMod)
			{
				this.PlayerSpawn.GetComponent<SphereCollider>().enabled = true;
				this.HUD.GetComponentInChildren<Canvas>().enabled = false;
				this.Player.SetActive(false);
				this.Camera.SetActive(false);
				this.EditorCamera.enabled = true;
				this.EditorCamera.transform.parent.GetComponent<CameraMovement>().enabled = true;
				this.Editing = false;
				this.PlayMod = false;
				foreach (GameObject obj in this.Enemies)
				{
					UnityEngine.Object.Destroy(obj);
					this.Enemies.Clear();
				}
			}
		}

		// Token: 0x060006B5 RID: 1717
		public void ExitEditMode()
		{
			string text = this.CoordinateField.text;
			this.CoordinateField.DeactivateInputField();
			this.CoordinateField.gameObject.SetActive(false);
			this.EditingObject = null;
			this.Editing = false;
		}

		// Token: 0x040013E5 RID: 5093
		public GameObject Player;

		// Token: 0x040013E6 RID: 5094
		public GameObject Camera;

		// Token: 0x040013E7 RID: 5095
		public GameObject HUD;

		// Token: 0x040013E8 RID: 5096
		public GameObject RootShared;

		// Token: 0x040013E9 RID: 5097
		public Camera EditorCamera;

		// Token: 0x040013EA RID: 5098
		public Text DebugObject;

		// Token: 0x040013EB RID: 5099
		public GameObject EditingObject;

		// Token: 0x040013EC RID: 5100
		public InputField CoordinateField;

		// Token: 0x040013ED RID: 5101
		public bool Editing;

		// Token: 0x040013EE RID: 5102
		private string editType = "position";

		// Token: 0x040013EF RID: 5103
		public static LevelEditorHandler Inst;

		// Token: 0x040013F0 RID: 5104
		public Transform PlayerSpawn;

		// Token: 0x040013F1 RID: 5105
		public bool PlayMod;

		// Token: 0x040013F3 RID: 5107
		public GameObject EnemySample;

		// Token: 0x040013F5 RID: 5109
		public List<GameObject> Enemies = new List<GameObject>();

		// Token: 0x040013F6 RID: 5110
		public List<Transform> EnemySpawnpoints = new List<Transform>();

		// Token: 0x040015BA RID: 5562
		public bool fart;

		// Token: 0x020000E1 RID: 225
		public enum CameraMode
		{
			// Token: 0x040013F8 RID: 5112
			Disabled,
			// Token: 0x040013F9 RID: 5113
			Enabled
		}
	}
}
