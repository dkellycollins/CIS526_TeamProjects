using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIS726_Assignment2.Models;
using CIS726_Assignment2.SystemBus;
using System.Data.Entity;

namespace CIS726_Assignment2
{
    public class PretendDB
    {
        CourseDBContext _context;
        IMessageQueueConsumer<Course> _coursesQueue;
        IMessageQueueConsumer<Plan> _planQueue;
        IMessageQueueConsumer<Semester> _semesterQueue;
        IMessageQueueConsumer<PlanCourse> _planCourseQueue;

        public PretendDB()
        {
            _context = new CourseDBContext();

            InitQueues();
        }

        public void Init()
        {
            _coursesQueue.BeginProcessing();
            _planQueue.BeginProcessing();
            _semesterQueue.BeginProcessing();
            _planCourseQueue.BeginProcessing();
        }

        private void InitQueues()
        {
            _coursesQueue = new BasicMessageQueueConsumer<Course>();
            _coursesQueue.Get += _coursesQueue_Get;
            _coursesQueue.GetAll += _coursesQueue_GetAll;
            _coursesQueue.Create += _coursesQueue_Create;
            _coursesQueue.Update += _coursesQueue_Update;
            _coursesQueue.Remove += _coursesQueue_Remove;

            _planQueue = new BasicMessageQueueConsumer<Plan>();
            _planQueue.Get += _planQueue_Get;
			_planQueue.GetAll += _planQueue_GetAll;
            _planQueue.Create += _planQueue_Create;
            _planQueue.Update += _planQueue_Update;
            _planQueue.Remove += _planQueue_Remove;

            _semesterQueue = new BasicMessageQueueConsumer<Semester>();
            _semesterQueue.Get += _semesterQueue_Get;
            _semesterQueue.GetAll += _semesterQueue_GetAll;
            _semesterQueue.Create += _semesterQueue_Create;
            _semesterQueue.Update += _semesterQueue_Update;
            _semesterQueue.Remove += _semesterQueue_Remove;

            _planCourseQueue = new BasicMessageQueueConsumer<PlanCourse>();
            _planCourseQueue.Get += _planCourseQueue_Get;
            _planCourseQueue.GetAll += _planCourseQueue_GetAll;
            _planCourseQueue.Create += _planCourseQueue_Create;
            _planCourseQueue.Update += _planCourseQueue_Update;
            _planCourseQueue.Remove += _planCourseQueue_Remove;
        }

        #region CourseDB

        void _coursesQueue_Remove(Course course)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        void _coursesQueue_Update(Course course)
        {
            _context.Entry(course).State = System.Data.EntityState.Modified;
            _context.SaveChanges();
        }

        List<Course> _coursesQueue_Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            List<Course> courseList = new List<Course>();
            courseList.Add(course);

            return courseList;
        }

        List<Course> _coursesQueue_GetAll()
        {
            List<Course> courseList = _context.Set<Course>()
                .ToList();

            return courseList;
        }

        Course _coursesQueue_Get(Course partialCourse)
        {
            Course course = _context.Courses.Where(c => c.ID == partialCourse.ID)
                .Include(c => c.degreePrograms.Select(s => s.course))
                .Include(c => c.degreePrograms.Select(s => s.degreeProgram))
                .Include(c => c.electiveLists.Select(s => s.course))
                .Include(c => c.electiveLists.Select(s => s.electiveList))
                .Include(c => c.prerequisiteFor.Select(s => s.prerequisiteCourse))
                .Include(c => c.prerequisiteFor.Select(s => s.prerequisiteForCourse))
                .Include(c => c.prerequisites.Select(s => s.prerequisiteCourse))
                .Include(c => c.prerequisites.Select(s => s.prerequisiteForCourse))
                .First();

            return course;
        }

        #endregion 

        #region PlanDB

		Plan _planQueue_Get(Plan partialPlan)
		{
            Plan plan = _context.Plans.Where(p=>p.ID == partialPlan.ID)
                .Include(pl => pl.degreeProgram)
                .Include(pl => pl.degreeProgram.requiredCourses.Select(s => s.course.prerequisites))
                .Include(pl => pl.degreeProgram.requiredCourses.Select(s => s.course.prerequisiteFor))
                .Include(pl => pl.degreeProgram.electiveCourses.Select(s => s.electiveList))
                .Include(pl => pl.user)
                .Include(pl => pl.semester)
                .Include(pl => pl.planCourses.Select(s => s.plan))
                .Include(pl=>pl.planCourses.Select(s => s.course))
                .Include(pl=>pl.planCourses.Select(s=>s.electiveList))
                .Include(pl=>pl.planCourses.Select(s=>s.semester))
                .First();
            
            return plan;
		}

        void _planQueue_Remove(Plan plan)
        {
            Plan thisPlan = _context.Plans.Where(p => p.ID == plan.ID).First();
            _context.Plans.Remove(thisPlan);
            _context.SaveChanges();
        }

        void _planQueue_Update(Plan plan)
        {
            Plan thisPlan = _context.Plans.Where(p => p.ID == plan.ID).First();
            _context.Entry(thisPlan).CurrentValues.SetValues(plan);
            _context.SaveChanges();
        }

        List<Plan> _planQueue_Create(Plan plan)
        {
            _context.Set<Plan>().Add(plan);
            _context.Entry(plan).State = System.Data.EntityState.Added;
            _context.SaveChanges();
            List<Plan> planList = new List<Plan>();
            planList.Add(plan);

            return planList;
        }

        List<Plan> _planQueue_GetAll()
        {
            List<Plan> planList =  _context.Set<Plan>()
                .Include(pl => pl.degreeProgram)
                .Include(pl => pl.user)
                .Include(pl => pl.semester)
                .ToList();

            return planList;
        }

        #endregion

        #region SemesterDB

        void _semesterQueue_Remove(Semester semester)
        {
            Semester thisSemester = _context.Semesters.Where(s => s.ID == semester.ID).First();
            _context.Semesters.Remove(thisSemester);
            _context.SaveChanges();
        }

        void _semesterQueue_Update(Semester semester)
        {
            _context.Entry(semester).State = System.Data.EntityState.Modified;
            _context.SaveChanges();
        }

        List<Semester> _semesterQueue_Create(Semester semester)
        {
            _context.Semesters.Add(semester);
            _context.SaveChanges();

            List<Semester> semesterList = new List<Semester>();
            semesterList.Add(semester);

            return semesterList;
        }

        List<Semester> _semesterQueue_GetAll()
        {
            List<Semester> semesterList = _context.Set<Semester>()
                .ToList();

            return semesterList;
        }

        Semester _semesterQueue_Get(Semester partialSemester)
        {
            Semester semester = _context.Semesters.Where(c => c.ID == partialSemester.ID)
                .First();

            return semester;
        }

        #endregion

        #region PlanCourseDB

        void _planCourseQueue_Remove(PlanCourse PlanCourse)
        {
            PlanCourse thisPlanCourse = _context.PlanCourses.Where(s => s.ID == PlanCourse.ID).First();
            _context.PlanCourses.Remove(thisPlanCourse);
            _context.SaveChanges();
        }

        void _planCourseQueue_Update(PlanCourse PlanCourse)
        {
            _context.Entry(PlanCourse).State = System.Data.EntityState.Modified;
            _context.SaveChanges();
        }

        List<PlanCourse> _planCourseQueue_Create(PlanCourse PlanCourse)
        {
            _context.PlanCourses.Add(PlanCourse);
            _context.SaveChanges();

            List<PlanCourse> PlanCourseList = new List<PlanCourse>();
            PlanCourseList.Add(PlanCourse);

            return PlanCourseList;
        }

        List<PlanCourse> _planCourseQueue_GetAll()
        {
            List<PlanCourse> PlanCourseList = _context.Set<PlanCourse>()
                //.Include(pc => pc.plan)
                //.Include(pc => pc.plan.degreeProgram)
                //.Include(pc => pc.plan.planCourses.Select(s => s.course.prerequisites))
                //.Include(pc => pc.plan.planCourses.Select(s => s.course.prerequisiteFor))
                //.Include(pc => pc.plan.semester)
                //.Include(pc => pc.plan.user)
                //.Include(pc => pc.semester)
                //.Include(pc => pc.electiveList)
                //.Include(pc => pc.course)
                .ToList();

            return PlanCourseList;
        }

        PlanCourse _planCourseQueue_Get(PlanCourse partialPlanCourse)
        {
            PlanCourse PlanCourse = _context.PlanCourses.Where(c => c.ID == partialPlanCourse.ID)
                .Include(pc => pc.plan)
                .Include(pc => pc.plan.degreeProgram)
                .Include(pc => pc.plan.planCourses.Select(s => s.course.prerequisites))
                .Include(pc => pc.plan.planCourses.Select(s => s.course.prerequisiteFor))
                .Include(pc => pc.plan.semester)
                .Include(pc => pc.plan.user)
                .Include(pc => pc.semester)
                .Include(pc => pc.electiveList)
                .Include(pc => pc.course)
                .First();

            return PlanCourse;
        }

        #endregion

    }
}