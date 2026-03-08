using System.Xml.Schema;

namespace week5.task2;

public interface IGradeStrategy
{
    string StrategyName { get; }
    double CalculateFinalGrade(List<int> assignments, int examScore);
}

public class StandardGrading : IGradeStrategy
{ 
    public string StrategyName => "Standard grade";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        ValidateGrades(assignments, examScore);
        var avg = 0;
        foreach (int grade in assignments)
        {
            avg += grade;
        }
        avg = avg/assignments.Count;
        return (avg * 0.4) + (examScore * 0.6);
    }
    private void ValidateGrades(List<int> assignments, int examSc)
    {
        foreach (int grade in assignments)
        {
            if (grade < 0 || grade > 100)
                throw new Exception("Invalid assignment grade");
        }

        if (examSc < 0 || examSc > 100)
            throw new Exception("Invalid exam grade");
    }
}

public class PracticalGrading : IGradeStrategy
{
    public string StrategyName => "Practical grade";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        ValidateGrades(assignments, examScore);
        var avg = 0;
        foreach (int grade in assignments)
        {
            avg += grade;
        }
        avg = avg/assignments.Count;
        return (avg * 0.8) + (examScore * 0.2);
    }
    private void ValidateGrades(List<int> assignments, int examSc)
    {
        foreach (int grade in assignments)
        {
            if (grade < 0 || grade > 100)
                throw new Exception("Invalid assignment grade");
        }

        if (examSc < 0 || examSc > 100)
            throw new Exception("Invalid exam grade");
    }
}

public class ExamOnlyGrading : IGradeStrategy
{
    public string StrategyName => "Exam only grade";

    public double CalculateFinalGrade(List<int> assignments, int examScore)
    {
        if (examScore < 0 || examScore > 100)
            throw new Exception("Invalid exam grade");
        return examScore;
    }
}

public class GradebookManager
{
    private Dictionary<string, IGradeStrategy> _strategies = new();

    public void RegisterStrategy(string courseCode, IGradeStrategy strategy)
    {
        if (string.IsNullOrWhiteSpace(courseCode) || strategy == null)
            throw new ArgumentNullException("Cannot be empty or null");
        _strategies[courseCode] = strategy;
    }

    public double GetStudentScore(string courseCode, List<int> grades, int exam)
    {
        if (!_strategies.ContainsKey(courseCode))
            throw new KeyNotFoundException("Invalid data");
        return _strategies[courseCode].CalculateFinalGrade(grades, exam);
    }
}
class Program
{
    static void Main()
    {
        GradebookManager manager = new GradebookManager();

        manager.RegisterStrategy("CS210", new StandardGrading());
        manager.RegisterStrategy("MATH111", new PracticalGrading());
        manager.RegisterStrategy("SCI400", new ExamOnlyGrading());

        List<int> assignments = new List<int> { 80, 90, 64 };

        var result1 = manager.GetStudentScore("CS210", assignments, 81);
        var result2 = manager.GetStudentScore("MATH111", assignments, 67);
        var result3 = manager.GetStudentScore("SCI400", assignments, 93);

        Console.WriteLine("CS101 Final Grade: " + result1);
        Console.WriteLine("CS202 Final Grade: " + result2);
        Console.WriteLine("CS303 Final Grade: " + result3);
    }
}
    


