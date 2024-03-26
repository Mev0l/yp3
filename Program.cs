using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static string connectionString = "Server=DESKTOP-5BD88QO\\SQLEXPRESS;Database=practical3; Trusted_Connection=true";

        static void Main(string[] args)
        {
            while (true)
            {
                
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить запись");
                Console.WriteLine("2. Удалить запись");
                Console.WriteLine("3. Обновить запись");
                Console.WriteLine("4. Вывести список записей");
                Console.WriteLine("5. Ввести учеников коннкертного предмета");
                Console.WriteLine("6. Ввести учеников коннкертного предмета в обратном");
                Console.WriteLine("7. Ввести учеников коннкертного в количественном исчисленнии");
                Console.WriteLine("8. Средния оценка");
                Console.WriteLine("9. Добавить оценку ученику");

                Console.WriteLine("0. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Выберите таблицу:");
                        Console.WriteLine("1. Students");
                        Console.WriteLine("2. Subjects");
                        string tableChoice = Console.ReadLine();
                        switch (tableChoice)
                        {
                            case "1":
                                AddRecord("Students");
                                break;
                            case "2":
                                AddRecord("Subjects");
                                break;
                            default:
                                Console.WriteLine("Неверный выбор таблицы.");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("Выберите таблицу:");
                        Console.WriteLine("1. Students");
                        Console.WriteLine("2. Subjects");
                        tableChoice = Console.ReadLine();
                        switch (tableChoice)
                        {
                            case "1":
                                RemoveRecord("Students");
                                break;
                            case "2":
                                RemoveRecord("Subjects");
                                break;
                            default:
                                Console.WriteLine("Неверный выбор таблицы.");
                                break;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Выберите таблицу:");
                        Console.WriteLine("1. Students");
                        Console.WriteLine("2. Subjects");
                        tableChoice = Console.ReadLine();
                        switch (tableChoice)
                        {
                            case "1":
                                UpdateRecord("Students");
                                break;
                            case "2":
                                UpdateRecord("Subjects");
                                break;
                            default:
                                Console.WriteLine("Неверный выбор таблицы.");
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Выберите таблицу:");
                        Console.WriteLine("1. Students");
                        Console.WriteLine("2. Subjects");
                        tableChoice = Console.ReadLine();
                        switch (tableChoice)
                        {
                            case "1":
                                DisplayRecords("Students");
                                break;
                            case "2":
                                DisplayRecords("Subjects");
                                break;
                            default:
                                Console.WriteLine("Неверный выбор таблицы.");
                                break;
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте ещё раз.");
                        break;
                    case "5":
                        Console.WriteLine("Введите название предмета:");
                        string subjectToSearch = Console.ReadLine();
                        DisplayStudentsBySubject(subjectToSearch);
                        break;
                    case "6":
                        Console.WriteLine("Введите название предмета:");
                        string subjectToSearch1 = Console.ReadLine();
                        DisplayStudentsBySubjectReverseOrder(subjectToSearch1);
                        break;
                    case "7":
                        Console.WriteLine("Введите название предмета:");
                        string subjectToCount = Console.ReadLine();
                        DisplayStudentCountBySubject(subjectToCount);
                        break;
                    case "8":
                        DisplayAverageGradeForAllStudents();
                        break;
                    case "9":
                        AddGrade();
                        break;
                }
            }
        }
        static void AddRecord(string tableName)
        {
            switch (tableName)
            {
                case "Students":
                    Console.WriteLine("Введите полное имя студента:");
                    string fullName = Console.ReadLine();
                    Console.WriteLine("Введите ID студента:");
                    string studentID1 = Console.ReadLine();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Students (StudentName, StudentID) VALUES (@FullName, @StudentID)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FullName", fullName);
                        command.Parameters.AddWithValue("@StudentID", studentID1);
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Студент добавлен.");
                    break;
                case "Subjects":
                    Console.WriteLine("Введите ID предмета:");
                    int subjectID = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите имя предмета:");
                    string subjectName = Console.ReadLine();
                    Console.WriteLine("Введите ID студента, для которого добавляется предмет:");
                    int studentID = int.Parse(Console.ReadLine());
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Subjects (SubjectID, SubjectName, StudentID) VALUES (@SubjectID, @SubjectName, @StudentID)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@SubjectID", subjectID);
                        command.Parameters.AddWithValue("@SubjectName", subjectName);
                        command.Parameters.AddWithValue("@StudentID", studentID);
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Предмет добавлен.");
                    break;
                default:
                    Console.WriteLine("Неверное имя таблицы.");
                    break;
            }
        }

        static void RemoveRecord(string tableName)
        {
            switch (tableName)
            {
                case "Students":
                    Console.WriteLine("Введите ID студента, которого хотите удалить:");
                    int ID = int.Parse(Console.ReadLine());
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Students WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Студент удален.");
                        }
                        else
                        {
                            Console.WriteLine("Студент с указанным ID не найден.");
                        }
                    }
                    break;
                case "Subjects":
                    Console.WriteLine("Введите ID предмета, который хотите удалить:");
                    int ID1 = int.Parse(Console.ReadLine());
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Subjects WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID1);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Предмет удален.");
                        }
                        else
                        {
                            Console.WriteLine("Предмет с указанным ID не найден.");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Неверное имя таблицы.");
                    break;
            }
        }
        static void UpdateRecord(string tableName)
        {
            switch (tableName)
            {
                case "Students":
                    Console.WriteLine("Введите ID студента, чье имя нужно обновить:");
                    int IDToUpdate1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новое имя студента:");
                    string newStudentName = Console.ReadLine();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Students SET StudentName = @NewName WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NewName", newStudentName);
                        command.Parameters.AddWithValue("@ID", IDToUpdate1);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Данные студента обновлены.");
                        }
                        else
                        {
                            Console.WriteLine("Студент с указанным ID не найден.");
                        }
                    }
                    break;
                case "Subjects":
                    Console.WriteLine("Введите ID предмета, название которого нужно обновить:");
                    int IDToUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новое название предмета:");
                    string newSubjectName = Console.ReadLine();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Subjects SET SubjectName = @NewSubjectName WHERE ID = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@NewSubjectName", newSubjectName);
                        command.Parameters.AddWithValue("@ID", IDToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Данные предмета обновлены.");
                        }
                        else
                        {
                            Console.WriteLine("Предмет с указанным ID не найден.");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Неверное имя таблицы.");
                    break;
            }
        }

        static void DisplayRecords(string tableName)
        {
            switch (tableName)
            {
                case "Students":
                    Console.WriteLine("Список студентов:");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT * FROM Students";
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["StudentID"]}, Имя: {reader["StudentName"]}");
                        }
                    }
                    break;
                case "Subjects":
                    Console.WriteLine("Список предметов:");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT * FROM Subjects";
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["SubjectID"]}, Название: {reader["SubjectName"]}, ID студента: {reader["StudentID"]}");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Неверное имя таблицы.");
                    break;
            }
        }
        
        

        

        
        static void DisplayStudentsBySubject(string subjectName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT s.StudentID, s.StudentName
                        FROM Students s
                        JOIN Subjects sub ON s.StudentID = sub.StudentID
                        WHERE sub.SubjectName = @SubjectName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"Студенты, изучающие предмет '{subjectName}':");
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["StudentID"]}, Имя: {reader["StudentName"]}");
                }
                reader.Close();
            }
        }
        static void DisplayStudentsBySubjectReverseOrder(string subjectName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT s.StudentID, s.StudentName
            FROM Students s
            JOIN Subjects sub ON s.StudentID = sub.StudentID
            WHERE sub.SubjectName = @SubjectName";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                SqlDataReader reader = command.ExecuteReader();

                List<string> students = new List<string>();

                Console.WriteLine($"Студенты, изучающие предмет '{subjectName}' в обратном порядке:");
                while (reader.Read())
                {
                    string studentInfo = $"ID: {reader["StudentID"]}, Имя: {reader["StudentName"]}";
                    students.Add(studentInfo);
                }
                reader.Close();
                for (int i = students.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(students[i]);
                }
            }
        }
        static void DisplayStudentCountBySubject(string subjectName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT COUNT(s.StudentID) AS StudentCount
            FROM Students s
            JOIN Subjects sub ON s.StudentID = sub.StudentID
            WHERE sub.SubjectName = @SubjectName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                int studentCount = (int)command.ExecuteScalar();

                Console.WriteLine($"Количество студентов, изучающих предмет '{subjectName}': {studentCount}");
            }
        }
        static void DisplayAverageGradeForAllStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT AVG(Grade) AS AverageGrade
            FROM Grade";
                SqlCommand command = new SqlCommand(query, connection);
                object result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    double averageGrade = Convert.ToDouble(result);
                    Console.WriteLine($"Средняя оценка всех студентов: {averageGrade:F2}");
                }
                else
                {
                    Console.WriteLine("Нет данных об оценках студентов.");
                }
            }
        }
        static void AddGrade()
        {
            Console.WriteLine("Введите ID студента:");
            int studentID;
            if (!int.TryParse(Console.ReadLine(), out studentID))
            {
                Console.WriteLine("Некорректный ввод ID студента.");
                return;
            }

            
            bool studentExists = CheckID(studentID);
            if (!studentExists)
            {
                Console.WriteLine("Студента с указанным ID не существует.");
                return;
            }

            Console.WriteLine("Оценка студента:");
            string grade = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Grade (StudentID, Grade) VALUES (@StudentID, @Grade)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentID", studentID);
                command.Parameters.AddWithValue("@Grade", grade);
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Оценка добавлена");
        }
        static bool CheckID(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Students WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", studentID);
                int studentCount = (int)command.ExecuteScalar();
                return studentCount > 0;
            }
        }
    }
}