// GROUP MEMBERS:
// Joseane Maria da Silva - 101565390
// Rojan - 
// Eric - 

using System.Text.Json;
using System.Text.RegularExpressions;

namespace Assignment2
{
    public class Program
    {
        static string? Menu()
        {
            Console.WriteLine("\u001b[36m___________________________________");
            Console.WriteLine("\t\tMENU");
            Console.WriteLine("___________________________________");
            Console.WriteLine("1 - Add a new student");
            Console.WriteLine("2 - Add a new course");
            Console.WriteLine("3 - Register a student to a course");
            Console.WriteLine("4 - Display all students");
            Console.WriteLine("5 - Display all courses");
            Console.WriteLine("6 - Display Registrations");
            Console.WriteLine("7 - Save Data");
            Console.WriteLine("8 - Load Data");
            Console.WriteLine("9 - Exit");
            Console.WriteLine("___________________________________\u001b[0m");
            Console.WriteLine("Enter option from the menu: ");
            return Console.ReadLine();
        }

        public static void Main(string[] args)
        {
            College college = new College();
            LoadData(college);

            bool exit = false;
            while (!exit)
            {
                var userInput = Menu();
                switch (userInput)
                {
                    case "1":
                        Student newStudent = AddStudent();
                        college.AddStudent(newStudent);
                        break;
                    case "2":
                        Course newCourse = AddCourse();
                        college.AddCourse(newCourse);
                        break;
                    case "3":
                        Registration newRegistration = RegisterStudentToCourse(college.Students, college.Courses);
                        college.AddRegistration(newRegistration);
                        break;
                    case "4":
                        DisplayAllStudents(college.Students);
                        break;
                    case "5":
                        DisplayAllCourses(college.Courses);
                        break;
                    case "6":
                        DisplayRegistrations(college.Registrations, college.Students, college.Courses);
                        break;
                    case "7":
                        SaveData(college.Students, college.Courses, college.Registrations);
                        break;
                    case "8":
                        LoadData(college);
                        break;
                    case "9":
                        exit = true;
                        Console.WriteLine("\u001b[32mExiting...\u001b[0m");
                        SaveData(college.Students, college.Courses, college.Registrations);
                        break;
                    default:
                        Console.WriteLine("\u001b[31mPlease enter a valid option!\u001b[0m");
                        break;
                }
            }
        }

        private static Student AddStudent()
        {
            Student student = new Student();
            Console.WriteLine("Enter the student name: ");
            student.Name = Console.ReadLine();
            // validating
            while (string.IsNullOrWhiteSpace(student.Name))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid student name: \u001b[0m");
                student.Name = Console.ReadLine();
            }
            Console.WriteLine("Enter the student email: ");
            student.EmailAddress = Console.ReadLine();
            // validating
            while (string.IsNullOrWhiteSpace(student.EmailAddress) || !Regex.IsMatch(student.EmailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid student email (Example: name@something.com): \u001b[0m");
                student.EmailAddress = Console.ReadLine();
            }
            Console.WriteLine("\u001b[32mStudent added to the college list\u001b[0m");

            return student;
        }

        private static Course AddCourse()
        {
            Course course = new Course();
            Console.WriteLine("Enter the course name:");
            course.Name = Console.ReadLine();
            // validating
            while (string.IsNullOrWhiteSpace(course.Name))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid course name: \u001b[0m");
                course.Name = Console.ReadLine();
            }
            Console.WriteLine("Enter the course credit: ");
            string courseCreditInput = Console.ReadLine();
            double tempCredit;
            // validating
            while (!double.TryParse(courseCreditInput, out tempCredit) || tempCredit <= 0)
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid course credit (number greater than 0): \u001b[0m");
                courseCreditInput = Console.ReadLine();
            }
            course.CreditHours = tempCredit;
            Console.WriteLine("\u001b[32mCourse added to the college list\u001b[0m");

            return course;
        }

        // Prompt for StudentID and CourseID, and add the course to the specific student row as a 2D array
        private static Registration RegisterStudentToCourse(List<Student> students, List<Course> courses)
        {
            Registration registration = new Registration();
            Console.WriteLine("\u001b[32m----- Select a student from the list -----\u001b[0m");
            DisplayAllStudents(students); // Show all students to choose the ones to register
            Console.WriteLine("Enter the student id: ");
            registration.StudentId = int.Parse(Console.ReadLine());
            // validating
            while (!students.Any(s => s.StudentId == registration.StudentId))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid student id: \u001b[0m");
                registration.StudentId = int.Parse(Console.ReadLine());
            }
            Console.WriteLine($"\u001b[32m----- Select the course to register for student {registration.StudentId} ----- \u001b[0m");
            DisplayAllCourses(courses); // Show all courses to choose the ones to link to student.
            Console.WriteLine($"Enter the course id: ");
            registration.CourseId = int.Parse(Console.ReadLine());
            // validating
            while (!courses.Any(c => c.CourseId == registration.CourseId))
            {
                Console.WriteLine("\u001b[31mInvalid input. Please enter a valid course id: \u001b[0m");
                registration.CourseId = int.Parse(Console.ReadLine());
            }
            Console.WriteLine($"\u001b[32mYou registered the student id {registration.StudentId} for the course id {registration.CourseId}\u001b[0m");

            return registration;
        }

        private static void DisplayAllStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                student.StudentDiplayInfo();
            }
        }

        private static void DisplayAllCourses(List<Course> courses)
        {
            foreach (var course in courses)
            {
                course.DisplayCourseInfo();
            }
        }


        /*Show which students are registered in which courses -> To display registration, a loop will go through each
        studentId, and find in student list (assuming the information could not be ordered). For each student element,
        it will go through each column to find the courses the student is enrolled and display the information.*/
        private static void DisplayRegistrations(List<List<int>> registrations, List<Student> students, List<Course> courses)
        {
            Console.WriteLine("\u001b[32m----- REGISTRATIONS -----");

            for (int i = 0; i < registrations.Count; i++)
            {
                int studentId = i + 1;
                Student student = students.Find(student => student.StudentId == studentId);
                Console.WriteLine($"Student id: {studentId}, Name: {student.Name}");
                for (int j = 0; j < registrations[i].Count; j++)
                {
                    int courseId = registrations[i][j];
                    Course course = courses.Find(course => course.CourseId == courseId);
                    Console.WriteLine($"\t Course id: {courseId}, Name: {course.Name}");
                }

                if (registrations[i].Count == 0)
                {
                    Console.WriteLine("\tNo registrations found\u001b[0m");
                }
            }
        }


        /*Data will be saved in separated files (one for student, one for courses, and one for registrations).
         It will be saved as json files.*/
        static void SaveData(List<Student> students, List<Course> courses, List<List<int>> registrations)
        {

            string studentData = JsonSerializer.Serialize(students, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText("students.json", studentData);
            string courseData = JsonSerializer.Serialize(courses, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText("courses.json", courseData);
            string registrationData = JsonSerializer.Serialize(registrations, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText("registrations.json", registrationData);
            Console.WriteLine("\u001b[32mData saved to file!\u001b[0m");
        }

        // Calling the methods to load student, course and registration data.
        static void LoadData(College college)
        {
            LoadStudentData(college);
            LoadCourseData(college);
            LoadRegistrationData(college);
        }

        static void LoadStudentData(College college)
        {
            if (File.Exists("students.json"))
            {
                string studentData = File.ReadAllText("students.json");
                List<Student> students = JsonSerializer.Deserialize<List<Student>>(studentData);

                foreach (var student in students)
                {
                    college.AddStudent(student);
                }
            }
            else
            {
                Console.WriteLine("No student file to load!");
            }
        }

        static void LoadCourseData(College college)
        {
            if (File.Exists("courses.json"))
            {
                string courseData = File.ReadAllText("courses.json");
                List<Course> courses = JsonSerializer.Deserialize<List<Course>>(courseData);

                foreach (var course in courses)
                {
                    college.AddCourse(course, true);
                }
            }
            else
            {
                Console.WriteLine("No course file to load!");
            }
        }

        static void LoadRegistrationData(College college)
        {
            if (File.Exists("registrations.json"))
            {
                string registrationData = File.ReadAllText("registrations.json");
                List<List<int>> registrations = JsonSerializer.Deserialize<List<List<int>>>(registrationData);

                for (int i = 0; i < registrations.Count; i++)
                {
                    int studentId = i + 1;
                    for (int j = 0; j < registrations[i].Count; j++)
                    {
                        int courseId = registrations[i][j];
                        Registration registration = new Registration(studentId, courseId);
                        college.AddRegistration(registration, true);
                    }
                }
            }
            else
            {
                Console.WriteLine("No registrations file to load!");
            }
        }
    }
}