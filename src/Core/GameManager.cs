using Godot;
using TrialEngine.Assets;
using TrialEngine.UI;
using TrialEngine.Utils;

namespace TrialEngine.Core;

public partial class GameManager : Node
{
    [Export] public NodePath SceneDirectorPath { get; set; } = new("../HybridSceneDirector");
    [Export] public NodePath AudioManagerPath { get; set; } = new("../AudioManager");
    [Export] public string CharacterManifestPath { get; set; } = "res://data/characters.json";
    [Export] public string AudioManifestPath { get; set; } = "res://data/audio_manifest.json";
    [Export] public string GameConfigPath { get; set; } = "res://data/game_config.json";
    [Export] public TrialPhase InitialPhase { get; set; } = TrialPhase.VisualNovel;

    private GameContext _context = null!;
    private IGameState? _currentState;

    public override void _Ready()
    {
        var sceneDirector = NodePathGuard.GetRequired<HybridSceneDirector>(this, SceneDirectorPath);
        var audio = NodePathGuard.GetRequired<AudioManager>(this, AudioManagerPath);
        var config = JsonLoader.LoadFromGodotPath<GameConfig>(GameConfigPath) ?? new GameConfig();

        var assetLoader = new LocalAssetLoader(CharacterManifestPath, AudioManifestPath);
        audio.AssetLoader = assetLoader;

        _context = new GameContext
        {
            Tree = GetTree(),
            Root = GetTree().Root,
            SceneDirector = sceneDirector,
            AssetLoader = assetLoader,
            Audio = audio,
            Clock = new TrialClock(config.GlobalTimerSeconds),
            Focus = new FocusGauge(config.FocusMax, config.FocusDrainPerSecond, config.FocusRegenPerSecond),
            Events = new EventBus()
        };

        _context.Set("config", config);
        _context.Clock.Expired += () => ChangeState(TrialPhase.GameOver);
        _context.Events.VisualNovelCleared += () => ChangeState(TrialPhase.NonstopDebate);
        _context.Events.DebateCleared += async () => await GoToLogicDive();
        _context.Events.LogicDiveCleared += async () => await GoToDebate();
        _context.Events.DebugMessage += GD.Print;

        BindHud(sceneDirector);

        ChangeState(InitialPhase);
        _context.Clock.Start();
    }

    private void BindHud(HybridSceneDirector sceneDirector)
    {
        var hud = sceneDirector.GetHudLayer();
        hud.GetNodeOrNull<TimerHud>("TimerLabel")?.Bind(_context.Clock);
        hud.GetNodeOrNull<FocusHud>("FocusBar")?.Bind(_context.Focus);
        hud.GetNodeOrNull<DebugLogHud>("DebugLog")?.Bind(_context.Events);
    }

    public override void _Process(double delta)
    {
        if (_currentState == null)
            return;

        var d = (float)delta;
        _context.Clock.Tick(d);
        _context.Focus.Tick(d);
        _currentState.Update(_context, delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState?.PhysicsUpdate(_context, delta);
    }

    public void ChangeState(TrialPhase phase)
    {
        ChangeState(StateFactory.Create(phase));
    }

    public void ChangeState(IGameState nextState)
    {
        _currentState?.Exit(_context);
        _currentState = nextState;
        _context.Events.PublishPhaseChanged(nextState.Phase);
        _currentState.Enter(_context);
    }

    private async Task GoToLogicDive()
    {
        _currentState?.Exit(_context);
        await _context.SceneDirector.TransitionTo3D();
        _currentState = StateFactory.Create(TrialPhase.LogicDive);
        _context.Events.PublishPhaseChanged(TrialPhase.LogicDive);
        _currentState.Enter(_context);
    }

    private async Task GoToDebate()
    {
        _currentState?.Exit(_context);
        await _context.SceneDirector.TransitionTo2D();
        _currentState = StateFactory.Create(TrialPhase.NonstopDebate);
        _context.Events.PublishPhaseChanged(TrialPhase.NonstopDebate);
        _currentState.Enter(_context);
    }
}
