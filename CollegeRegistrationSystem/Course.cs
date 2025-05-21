namespace Assignment2;

internal class Course
{
    private static int idGenerator = 0;
    public int CourseId { get; }
    public string Name { get; set; }
    public double CreditHours { get; set; }

    public Course()
    {
        CourseId = ++idGenerator;
    }

    // Method to display course information
    public void DisplayCourseInfo()
    {
        Console.WriteLine($"\u001b[32mCourse Id: {CourseId}, Course: {Name}, Credit Hours: {CreditHours}\u001b[0m");
    }

    // Method to show courses with full credit
    public bool IsFullCreditCourse()
    {
        if (CreditHours >= 1)
        {
            return true;
        }
        return false;
    }
}