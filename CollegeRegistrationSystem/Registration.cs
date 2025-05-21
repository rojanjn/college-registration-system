namespace Assignment2;
internal class Registration
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    public Registration() { }

    public Registration(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }
}