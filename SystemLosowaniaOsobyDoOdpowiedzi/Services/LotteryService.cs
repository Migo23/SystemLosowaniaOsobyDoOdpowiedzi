using SystemLosowaniaOsobyDoOdpowiedzi.Models;
using System.Linq;

namespace SystemLosowaniaOsobyDoOdpowiedzi.Services
{
    public class LotteryService
    {
        private Random _random = new Random();

        public Student DrawStudent(Class selectedClass)
        {
            if (selectedClass.Students == null || !selectedClass.Students.Any())
            {
                return null;
            }

            var availableStudents = selectedClass.Students
                .Where(s => s.IsPresent && !s.WasPicked && s.Number != selectedClass.LuckyNumber)
                .ToList();

            if (availableStudents.Count == 0)
            {
                foreach (var student in selectedClass.Students)
                {
                    student.WasPicked = false;
                }
                availableStudents = selectedClass.Students
                    .Where(s => s.IsPresent && s.Number != selectedClass.LuckyNumber)
                    .ToList();
            }

            if (availableStudents.Count == 0)
            {
                return null;
            }

            var selectedStudent = availableStudents[_random.Next(availableStudents.Count)];
            selectedStudent.WasPicked = true;
            return selectedStudent;
        }

        public int GenerateLuckyNumber(int maxNumber)
        {
            return _random.Next(1, maxNumber + 1);
        }
    }
}