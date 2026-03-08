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

