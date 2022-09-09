using System;


namespace Homework.NET
{
    /// <summary>
    /// 'Group' class;
    /// <br />
    /// Класс 'Группа';
    /// </summary>
    public class Group
    {


        #region FIELDS


        /// <summary>
        /// The name of the group;
        /// <br />
        /// Имя группы;
        /// </summary>
        private string _name = "Unnamed";


        /// <summary>
        /// The list of the subjects;
        /// <br />
        /// Список предметов;
        /// </summary>
        private List<Subject>? _subjects = null;


        /// <summary>
        /// The list of the students;
        /// <br />
        /// Список учеников;
        /// </summary>
        private List<Student>? _students = null;


        #endregion FIELDS



        #region PROPERTIES


        /// <summary>
        /// Property for '_name' field;
        /// <br />
        /// Свойства поля '_name';
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }


        /// <summary>
        /// Property for '_subjects' field;
        /// <br />
        /// Свойства поля '_subjects';
        /// </summary>
        public List<Subject>? Subjects
        {
            get { return _subjects; }
        }


        /// <summary>
        /// Property for '_students' field;
        /// <br />
        /// Свойства поля '_students';
        /// </summary>
        public List<Student>? Students
        {
            get { return _students; }
        }

        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// The 'Name' constructor;
        /// <br />
        /// Конструктор с именем группы;
        /// </summary>
        /// <param name="sName"></param>
        public Group(string sName)
        {
            Name = sName;
            _subjects = new List<Subject>();
            _students = new List<Student>();
        }


        #endregion CONSTRUCTION



        #region LOGIC


        /// <summary>
        /// Get group's AVG grade;
        /// <br />
        /// Получить среднюю оценку группы;
        /// </summary>
        /// <returns> Grade;<br /> Оценка;</returns>
        public int? GetAverageGroupGrade()
        {
            // Grades counter;
            int? nAvg = null;

            // Check if group has any students;
            if (_students is null) return nAvg;

            nAvg = 0;

            // Iterate through all students;
            foreach (Student student in _students)
            {
                nAvg += student?.GetAverageGrade();
            }

            // Divide grade by students quantity;
            nAvg /= _students.Count;

            // Return res;
            return nAvg;
        }


        /// <summary>
        /// Get the highest and the lowest grade for every subject;
        /// <br />
        /// Получить максимальный и минимальный балл по каждому предмету;
        /// </summary>
        /// <returns>
        /// A tuple which includes two lists with pointers to the students whose AVG results represent certain values;
        /// <br />
        /// Кортеж(?), который представляет собой два листа, содержащие указатели на учеников с соответствующим средним баллом по предмету;
        /// </returns>
        public (List<Student> Highest, List<Student> Lowest) GetMinMaxGradesForAllSubjects()
        {
            // Create result lists;
            List<Student> resultLow = new List<Student>();
            List<Student> resultHigh = new List<Student>();

            // Create pointers to the highest and lowest student;
            Student highest_student_pointer = _students[0];
            Student lowest_student_pointer = _students[0]; ;

            // Counters for comparison;
            int? nLowestValue = Grade.MAX_VALUE;
            int? nHighestValue = Grade.MIN_VALUE;

            // Methods invokation economy;
            int tempResult;

            // Iterate through subjects;
            for (int i = 0, subj_size = _subjects.Count; i < subj_size; i++)
            {
                // Refresh comporation counters;
                nLowestValue = 100;
                nHighestValue = 0;

                // Iterate through students;
                for (int j = 0, stud_size = _students.Count; j < stud_size; j++)
                {
                    tempResult = _students[j].GetAverageGradesForAllSubjects()[i];

                    if (tempResult < nLowestValue) // The lowest;
                    {
                        lowest_student_pointer = _students[j];
                        nLowestValue = tempResult;
                    }
                    if (tempResult > nHighestValue) // The highest;
                    {
                        highest_student_pointer = _students[j];
                        nHighestValue = tempResult;
                    }
                }

                resultHigh.Add(highest_student_pointer);
                resultLow.Add(lowest_student_pointer);
            }

            return (resultHigh, resultLow);
        }


        /// <summary>
        /// Add a subject;
        /// <br />
        /// Добавить предмет;
        /// </summary>
        /// <param name="newSubject">Указатель на предмет</param>
        public void AddSubject(Subject newSubject)
        {
            if (newSubject != null)
            {
                // If list hasn't been created yet - create;
                if (_subjects == null) _subjects = new List<Subject>();

                // In case we already have it;
                if (_subjects.Contains(newSubject)) return;

                // Actually add a subject;
                _subjects.Add(newSubject);

                // Sort the list to have all grades ready to be printed out;
                _subjects.Sort(
                    delegate (Subject sbj1, Subject sbj2)
                    {
                        return string.Compare(sbj1.Name, sbj2.Name);
                    }
                );
            }
        }


        /// <summary>
        /// Add a Student;
        /// <br />
        /// Добавить ученика;
        /// </summary>
        /// <param name="student">Указатель на ученика</param>
        public void AddStudent(Student student)
        {
            // If list hasn't been created yet - create;
            if (_students == null) _students = new List<Student>();

            // In case we already have them in group;
            if (_students.Contains(student)) return;

            // Actually add them;
            _students.Add(student);

            // Make 'Group' reference point to 'this';
            student.Group = this;

            // Sort the group in Alphabet order;
            _students.Sort(
                delegate (Student std1, Student std2)
                {
                    return string.Compare(std1.LastName, std2.LastName);
                }
            );

        }


        #endregion LOGIC


    }
}