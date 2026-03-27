#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public static class PrototypeSceneBuilder
{
  const string ScenePath = "Assets/Scenes/Prototype.unity";
  const string PhysicsDir = "Assets/Generated/PhysicsMaterials";

  [MenuItem("R Swipe League V2/Generate Prototype Scene")]
  public static void Generate()
  {
    EnsureFolder("Assets/Scenes");
    EnsureFolder(PhysicsDir);

    var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

    BuildLighting();
    var directorGo = new GameObject("MatchDirector");
    var director = directorGo.AddComponent<MatchDirector>();

    var materials = CreateMaterials();
    var ballPm = CreatePhysicMaterial("BallPrototype", 0.52f, 0.22f);
    var carPm = CreatePhysicMaterial("CarPrototype", 0.05f, 0.65f);

    BuildArena(materials.floor, materials.wall);
    var (ballRb, ballSpawnTf) = BuildBall(ballPm, materials.ball);
    var (_, carCtrl, carGo) = BuildCar(carPm, materials.car);
    BuildGoal(materials.goal, director);
    BuildDunkZone();
    BuildOutOfBounds(director);
    BuildHud(director, ballRb, ballSpawnTf, carCtrl);
    BuildCamera(carGo.transform);

    EditorSceneManager.SaveScene(scene, ScenePath);
    RegisterSceneInBuildSettings(ScenePath);

    Selection.activeObject = carGo;
    Debug.Log("Prototype scene saved to " + ScenePath + ". Press Play to drive with WASD, Space jump, Shift boost.");
  }

  static void EnsureFolder(string assetPath)
  {
    if (AssetDatabase.IsValidFolder(assetPath))
      return;
    var parent = Path.GetDirectoryName(assetPath)?.Replace("\\", "/");
    var name = Path.GetFileName(assetPath);
    if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent))
      EnsureFolder(parent!);
    AssetDatabase.CreateFolder(parent ?? "Assets", name);
  }

  static void BuildLighting()
  {
    var sun = new GameObject("Directional Light");
    var light = sun.AddComponent<Light>();
    light.type = LightType.Directional;
    light.color = new Color(1f, 0.96f, 0.9f);
    light.intensity = 1.05f;
    sun.transform.rotation = Quaternion.Euler(52f, -35f, 0f);
  }

  static (Material floor, Material wall, Material ball, Material car, Material goal) CreateMaterials()
  {
    Material M(Color color, float smoothness = 0.35f)
    {
      var shader = Shader.Find("Standard");
      if (shader == null)
        shader = Shader.Find("Universal Render Pipeline/Lit");
      if (shader == null)
        shader = Shader.Find("Hidden/InternalErrorShader");
      var m = new Material(shader);
      m.color = color;
      m.SetFloat("_Glossiness", smoothness);
      return m;
    }

    return (
      M(new Color(0.12f, 0.16f, 0.22f)),
      M(new Color(0.22f, 0.28f, 0.38f), 0.5f),
      M(new Color(0.95f, 0.38f, 0.2f), 0.65f),
      M(new Color(0.2f, 0.65f, 0.85f), 0.55f),
      M(new Color(0.95f, 0.82f, 0.25f), 0.7f)
    );
  }

  static PhysicMaterial CreatePhysicMaterial(string name, float bounciness, float friction)
  {
    var pm = new PhysicMaterial(name)
    {
      bounciness = bounciness,
      dynamicFriction = friction,
      staticFriction = friction,
      frictionCombine = PhysicMaterialCombine.Minimum,
      bounceCombine = PhysicMaterialCombine.Maximum
    };

    var path = $"{PhysicsDir}/{name}.physicMaterial";
    if (AssetDatabase.LoadAssetAtPath<PhysicMaterial>(path) != null)
      AssetDatabase.DeleteAsset(path);
    AssetDatabase.CreateAsset(pm, path);
    return pm;
  }

  static void BuildArena(Material floorMat, Material wallMat)
  {
    var arena = new GameObject("Arena");

    var floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
    floor.name = "Floor";
    floor.transform.SetParent(arena.transform);
    floor.transform.localScale = new Vector3(28f, 0.6f, 20f);
    floor.transform.position = new Vector3(0f, -0.3f, 0f);
    SetLayerRecursive(floor, LayerMask.NameToLayer("Ground"));
    floor.GetComponent<Renderer>().sharedMaterial = floorMat;

    Wall(arena.transform, wallMat, new Vector3(0f, 1.25f, 10.5f), new Vector3(28f, 2.5f, 0.6f));
    Wall(arena.transform, wallMat, new Vector3(0f, 1.25f, -10.5f), new Vector3(28f, 2.5f, 0.6f));
    Wall(arena.transform, wallMat, new Vector3(14.5f, 1.25f, 0f), new Vector3(0.6f, 2.5f, 21f));
    Wall(arena.transform, wallMat, new Vector3(-14.5f, 1.25f, 0f), new Vector3(0.6f, 2.5f, 21f));
  }

  static void Wall(Transform parent, Material mat, Vector3 pos, Vector3 scale)
  {
    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
    wall.name = "Wall";
    wall.transform.SetParent(parent);
    wall.transform.position = pos;
    wall.transform.localScale = scale;
    wall.GetComponent<Renderer>().sharedMaterial = mat;
  }

  static (Rigidbody body, ArcadeCarController controller, GameObject root) BuildCar(PhysicMaterial pm, Material mat)
  {
    var root = GameObject.CreatePrimitive(PrimitiveType.Cube);
    root.name = "Car";
    root.transform.position = new Vector3(0f, 0.55f, -7.5f);

    var body = root.AddComponent<Rigidbody>();
    body.mass = 88f;
    body.interpolation = RigidbodyInterpolation.Interpolate;
    body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    root.GetComponent<BoxCollider>().sharedMaterial = pm;
    root.GetComponent<Renderer>().sharedMaterial = mat;

    var controller = root.AddComponent<ArcadeCarController>();
    return (body, controller, root);
  }

  static (Rigidbody body, Transform spawn) BuildBall(PhysicMaterial pm, Material mat)
  {
    var spawn = new GameObject("BallSpawn").transform;
    spawn.position = new Vector3(0f, 1.4f, -4f);

    var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    ball.name = "Ball";
    ball.tag = "Ball";
    ball.transform.position = spawn.position;
    ball.transform.localScale = Vector3.one * 1.15f;

    var body = ball.AddComponent<Rigidbody>();
    body.mass = 1.15f;
    body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    body.linearDamping = 0.15f;
    body.angularDamping = 0.35f;
    ball.GetComponent<SphereCollider>().sharedMaterial = pm;
    ball.GetComponent<Renderer>().sharedMaterial = mat;
    ball.AddComponent<BallLastTouch>();

    return (body, spawn);
  }

  static void BuildGoal(Material goalMat, MatchDirector director)
  {
    var goal = new GameObject("Goal");
    goal.transform.position = new Vector3(0f, 0f, 10.5f);

    Post(goal.transform, goalMat, new Vector3(-2.1f, 2.3f, 0f));
    Post(goal.transform, goalMat, new Vector3(2.1f, 2.3f, 0f));
    Cross(goal.transform, goalMat, new Vector3(0f, 3.5f, 0f), new Vector3(4.8f, 0.35f, 0.35f));
    Backboard(goal.transform, goalMat);

    var sensor = new GameObject("GoalSensor");
    sensor.transform.SetParent(goal.transform, false);
    sensor.transform.localPosition = new Vector3(0f, 2f, 0.4f);
    var box = sensor.AddComponent<BoxCollider>();
    box.isTrigger = true;
    box.size = new Vector3(4.4f, 3.2f, 1.6f);
    var scorer = sensor.AddComponent<GoalScorer>();
    AttachDirector(scorer, director);
  }

  static void Post(Transform parent, Material mat, Vector3 localPos)
  {
    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    cube.name = "GoalPost";
    cube.transform.SetParent(parent, false);
    cube.transform.localPosition = localPos;
    cube.transform.localScale = new Vector3(0.35f, 3.6f, 0.35f);
    cube.GetComponent<Renderer>().sharedMaterial = mat;
  }

  static void Cross(Transform parent, Material mat, Vector3 localPos, Vector3 scale)
  {
    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    cube.name = "GoalCross";
    cube.transform.SetParent(parent, false);
    cube.transform.localPosition = localPos;
    cube.transform.localScale = scale;
    cube.GetComponent<Renderer>().sharedMaterial = mat;
  }

  static void Backboard(Transform parent, Material mat)
  {
    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    cube.name = "Backboard";
    cube.transform.SetParent(parent, false);
    cube.transform.localPosition = new Vector3(0f, 2.4f, -0.45f);
    cube.transform.localScale = new Vector3(5f, 3.2f, 0.25f);
    cube.GetComponent<Renderer>().sharedMaterial = mat;
  }

  static void BuildDunkZone()
  {
    var dz = new GameObject("DunkZone");
    dz.transform.position = new Vector3(0f, 2.1f, 6.5f);
    var box = dz.AddComponent<BoxCollider>();
    box.isTrigger = true;
    box.size = new Vector3(9f, 4.2f, 3.4f);
    dz.AddComponent<DunkZoneVolume>();
  }

  static void BuildOutOfBounds(MatchDirector director)
  {
    var oob = new GameObject("OutOfBounds");
    oob.transform.position = new Vector3(0f, -18f, 0f);
    var box = oob.AddComponent<BoxCollider>();
    box.isTrigger = true;
    box.size = new Vector3(120f, 8f, 120f);
    oob.AddComponent<OutOfBoundsVolume>();
  }

  static void BuildHud(
    MatchDirector director,
    Rigidbody ball,
    Transform ballSpawn,
    ArcadeCarController car)
  {
    var canvasGo = new GameObject("HUD");
    var canvas = canvasGo.AddComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    var scaler = canvasGo.AddComponent<CanvasScaler>();
    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    scaler.referenceResolution = new Vector2(1920, 1080);
    canvasGo.AddComponent<GraphicRaycaster>();

    var es = new GameObject("EventSystem");
    es.AddComponent<EventSystem>();
    es.AddComponent<InputSystemUIInputModule>();

    var font = Resources.GetBuiltinResource<Font>("Arial.ttf");

    var scoreGo = new GameObject("Score");
    scoreGo.transform.SetParent(canvasGo.transform, false);
    var scoreText = scoreGo.AddComponent<Text>();
    scoreText.font = font;
    scoreText.fontSize = 32;
    scoreText.color = Color.white;
    scoreText.text = "Score: 0";
    var scoreRect = scoreText.rectTransform;
    scoreRect.anchorMin = new Vector2(0f, 1f);
    scoreRect.anchorMax = new Vector2(0f, 1f);
    scoreRect.pivot = new Vector2(0f, 1f);
    scoreRect.anchoredPosition = new Vector2(24f, -24f);

    var lastGo = new GameObject("LastGoal");
    lastGo.transform.SetParent(canvasGo.transform, false);
    var lastText = lastGo.AddComponent<Text>();
    lastText.font = font;
    lastText.fontSize = 22;
    lastText.color = new Color(0.78f, 0.92f, 1f);
    lastText.text = "Aerial +3 · Dunk +4 when touching from dunk volume";
    var lastRect = lastText.rectTransform;
    lastRect.anchorMin = new Vector2(0f, 1f);
    lastRect.anchorMax = new Vector2(1f, 1f);
    lastRect.pivot = new Vector2(0.5f, 1f);
    lastRect.anchoredPosition = new Vector2(0f, -78f);
    lastRect.sizeDelta = new Vector2(-120f, 60f);
    lastRect.alignment = TextAnchor.UpperCenter;

    var carSpawn = new GameObject("CarSpawn").transform;
    carSpawn.position = new Vector3(0f, 0.55f, -7.5f);
    carSpawn.rotation = Quaternion.identity;

    director.Configure(ball, ballSpawn, car, carSpawn, scoreText, lastText);
  }

  static void BuildCamera(Transform target)
  {
    var camGo = new GameObject("Main Camera");
    camGo.tag = "MainCamera";
    var cam = camGo.AddComponent<Camera>();
    cam.nearClipPlane = 0.1f;
    cam.farClipPlane = 250f;
    cam.clearFlags = CameraClearFlags.SolidColor;
    cam.backgroundColor = new Color(0.04f, 0.06f, 0.1f);
    camGo.AddComponent<AudioListener>();
    var follow = camGo.AddComponent<FollowCamera>();
    follow.Bind(target);
  }

  static void SetLayerRecursive(GameObject go, int layer)
  {
    foreach (var t in go.GetComponentsInChildren<Transform>(true))
      t.gameObject.layer = layer;
  }

  static void AttachDirector(GoalScorer scorer, MatchDirector director)
  {
    var so = new SerializedObject(scorer);
    so.FindProperty("director").objectReferenceValue = director;
    so.ApplyModifiedPropertiesWithoutUndo();
  }

  static void RegisterSceneInBuildSettings(string path)
  {
    var scenes = EditorBuildSettings.scenes;
    var list = new System.Collections.Generic.List<EditorBuildSettingsScene>(scenes);
    if (!list.Exists(s => s.path == path))
      list.Add(new EditorBuildSettingsScene(path, true));
    EditorBuildSettings.scenes = list.ToArray();
  }
}
#endif
