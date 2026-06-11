using Godot;

namespace TrialEngine.LogicDive;

public partial class BranchGate : Area3D
{
    [Export] public int Lane { get; set; }
    [Export] public string ProblemId { get; set; } = string.Empty;
    [Export] public string AnswerText { get; set; } = string.Empty;
    [Export] public bool IsCorrectAnswer { get; set; }

    public event Action<BranchGate>? GateResolved;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    public void Configure(EquationProblem problem, BranchAnswer answer)
    {
        ProblemId = problem.Id;
        AnswerText = answer.Text;
        IsCorrectAnswer = answer.IsCorrect;
        Lane = answer.Lane;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is not LogicDiveRunner runner)
            return;

        if (!IsCorrectAnswer)
            runner.ApplyLogicErrorPenalty();

        GateResolved?.Invoke(this);
    }
}
