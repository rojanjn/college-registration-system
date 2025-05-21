namespace Assignment2;
internal class College
{
    private List<Student> students = new List<Student>();
    private List<Course> courses = new List<Course>();
    private List<List<int>> registrations = new List<List<int>>(); // 2D list of lists for registrations. 

    public List<Student> Students { get { return students; } }
    public List<Course> Courses { get { return courses; } }
    public List<List<int>> Registrations { get { return registrations; } }


    // When adding a new student to college, it will also add on the registration list as a new row
    internal void AddStudent(Student student)
    {
        students.Add(student);
        registrations.Add(new List<int>()); // Add a student to the list of registration row
    }

    internal void AddCourse(Course course, bool isLoading = false)
    {
        courses.Add(course);
        if (!isLoading) // Will not show the message when loading, but it will show when adding a new course
        {
            if (course.IsFullCreditCourse())
            {
                Console.WriteLine($"\u001b[32mThe course {course.Name} is full credit\u001b[0m");
            }
            else
            {
                Console.WriteLine($"\u001b[32mThe course {course.Name} is not full credit\u001b[0m");
            }
        }
    }


    /* Student row will be saved as ID, and each student row will have a list of course IDS,
    connecting to the correspondent course they are enrolled.*/
    public void AddRegistration(Registration newRegistration, bool isLoading = false)
    {
        int index = newRegistration.StudentId - 1; // Mapping the index
        var row = registrations[index]; // Saving student with id information as a row in the registration list
        row.Add(newRegistration.CourseId); // Adding the correspondent course id as enrolled course to the respective student row

        if (!isLoading) // Will not show the message when loading
        {
            Console.WriteLine($"\u001b[32mTotal of Registrations: {CountTotalRegistrations()}\u001b[0m");
        }
    }

    public int CountTotalRegistrations()
    {
        int count = 0;
        foreach (var registration in registrations)
        {
            count += registration.Count;
        }
        return count;
    }
}