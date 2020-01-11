using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<TimetableDetail> TimetableDetails { get; set; }
        public virtual DbSet<EntranceTest> EntranceTest { get; set; }
        public virtual DbSet<EntranceSubject> EntranceSubjects { get; set; }
        public virtual DbSet<LearningResult> LearningResults { get; set; }
        public virtual DbSet<Lecturer> Lectures { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<SpecializedTraining> SpecializedTrainings { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<Timetable> Timetables { get; set; }
        public virtual DbSet<University> Universitys { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Worktrack> Worktracks { get; set; }
        public virtual DbSet<EntranceTest> EntranceTests { get; set; }
        public virtual DbSet<Subject_EntranceTest> Subject_EntranceTests { get; set; }
        public virtual DbSet<Course_Training> CourseTraining { get; set; }
        public virtual DbSet<Training_Subject> TrainingSubject { get; set; }


        //public virtual DbSet<ADMIN> Admins { get; set; }

    }
}
