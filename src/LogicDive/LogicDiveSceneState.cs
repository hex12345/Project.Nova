using Godot;
using TrialEngine.Core;
using TrialEngine.Utils;

namespace TrialEngine.LogicDive;

public sealed class LogicDiveSceneState : IGameState
{
    public TrialPhase Phase => TrialPhase.LogicDive;

    private LogicDiveRunner? _runner;
    private LogicDiveProblemSet _problemSet = new();
    private int _problemIndex;
    private readonly List<BranchGate> _gates = new();

    public void Enter(GameContext context)
    {
        context.Events.PublishDebug("Entering Logic Dive.");

        _problemSet = JsonLoader.LoadFromGodotPath<LogicDiveProblemSet>("res://data/logic_dive_problems_001.json") ?? new LogicDiveProblemSet();
        _problemIndex = 0;

        _runner = context.SceneDirector.World3DRoot.GetNodeOrNull<LogicDiveRunner>("LogicDiveRunner");
        if (_runner == null)
        {
            context.Events.PublishDebug("LogicDiveRunner missing from World3DRoot.");
            return;
        }

        var config = context.Get<GameConfig>("config");
        _runner.Configure(config.LogicDive, context.Focus);
        _runner.ResetRunner(new Vector3(0, 2, 0));

        BindGates(context);
        ConfigureCurrentProblem(context);
    }

    public void Exit(GameContext context)
    {
        foreach (var gate in _gates)
            gate.GateResolved -= OnGateResolved;

        _gates.Clear();
    }

    public void Update(GameContext context, double delta)
    {
        context.Focus.SetActive(Input.IsActionPressed("focus"));
    }

    public void PhysicsUpdate(GameContext context, double delta) { }

    private void BindGates(GameContext context)
    {
        _gates.Clear();
        foreach (var gate in context.SceneDirector.World3DRoot.GetChildren().OfType<BranchGate>())
        {
            gate.GateResolved += OnGateResolved;
            _gates.Add(gate);
        }
    }

    private void ConfigureCurrentProblem(GameContext context)
    {
        if (_problemIndex >= _problemSet.Problems.Count)
        {
            context.Events.PublishLogicDiveCleared();
            return;
        }

        var problem = _problemSet.Problems[_problemIndex];
        context.Events.PublishDebug($"Logic Dive Problem: {problem.Prompt}");

        foreach (var answer in problem.Answers)
        {
            var gate = _gates.FirstOrDefault(g => g.Lane == answer.Lane);
            gate?.Configure(problem, answer);
        }
    }

    private void OnGateResolved(BranchGate gate)
    {
        if (gate.IsCorrectAnswer)
            GD.Print($"Correct branch: {gate.AnswerText}");
        else
            GD.Print($"Wrong branch: {gate.AnswerText}");

        _problemIndex++;
    }
}
