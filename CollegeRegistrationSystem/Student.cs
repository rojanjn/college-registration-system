using System.Collections;
namespace Assignment2;

internal class Student
{
    private static int idGenerator = 0;
    public int StudentId { get; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }

    public Student()
    {
        StudentId = ++idGenerator;
    }

    // Method to display student info
    public void StudentDiplayInfo()
    {
        Console.WriteLine($"\u001b[32mStudent ID: {StudentId}, Name: {Name}, Email Address: {EmailAddress}\u001b[0m");
    }
}