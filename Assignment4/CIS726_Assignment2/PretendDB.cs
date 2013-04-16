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
        IMessageQueueConsumer<DegreeProgram> _degreeProgramQueue;
        IMessageQueueConsumer<ElectiveCourse> _electiveCourseQueue;
        IMessageQueueConsumer<ElectiveList> _electiveListQueue;
        IMessageQueueConsumer<ElectiveListCourse> _electiveListCourseQueue;
        IMessageQueueConsumer<Plan> _planQueue;
        IMessageQueueConsumer<PlanCourse> _planCourseQueue;
        IMessageQueueConsumer<PrerequisiteCourse> _prerequisiteCourseQueue;
        IMessageQueueConsumer<RequiredCourse> _requiredCourseQueue;
        IMessageQueueConsumer<Semester> _semesterQueue;

        public PretendDB()
        {
            _context = new CourseDBContext();

            InitQueues();
        }

        public void Init()
        {
            _coursesQueue.BeginProcessing();
            _degreeProgramQueue.BeginProcessing();
            _electiveCourseQueue.BeginProcessing();
            _electiveListQueue.BeginProcessing();
            _electiveListCourseQueue.BeginProcessing();
            _planQueue.BeginProcessing();
            _planCourseQueue.BeginProcessing();
            _prerequisiteCourseQueue.BeginProcessing();
            _requiredCourseQueue.BeginProcessing();
            _semesterQueue.BeginProcessing();
        }

        public void Dispose()
        {
            _coursesQueue.Dispose();
            _degreeProgramQueue.Dispose();
            _electiveCourseQueue.Dispose();
            _electiveListQueue.Dispose();
            _electiveListCourseQueue.Dispose();
            _planQueue.Dispose();
            _planCourseQueue.Dispose();
            _prerequisiteCourseQueue.Dispose();
            _requiredCourseQueue.Dispose();
            _semesterQueue.Dispose();
        }

        private void InitQueues()
        {
            _coursesQueue = new BasicMessageQueueConsumer<Course>();
            _coursesQueue.Get += _coursesQueue_Get;
            _coursesQueue.GetAll += _coursesQueue_GetAll;
            _coursesQueue.Create += _coursesQueue_Create;
            _coursesQueue.Update += _coursesQueue_Update;
            _coursesQueue.Remove += _coursesQueue_Remove;

            _degreeProgramQueue = new BasicMessageQueueConsumer<DegreeProgram>();
            _degreeProgramQueue.Get += _degreeProgramQueue_Get;
            _degreeProgramQueue.GetAll += _degreeProgramQueue_GetAll;
            _degreeProgramQueue.Create += _degreeProgramQueue_Create;
            _degreeProgramQueue.Update += _degreeProgramQueue_Update;
            _degreeProgramQueue.Remove += _degreeProgramQueue_Remove;

            _electiveCourseQueue = new BasicMessageQueueConsumer<ElectiveCourse>();
            _electiveCourseQueue.Get += _electiveCourseQueue_Get;
            _electiveCourseQueue.GetAll += _electiveCourseQueue_GetAll;
            _electiveCourseQueue.Create += _electiveCourseQueue_Create;
            _electiveCourseQueue.Update += _electiveCourseQueue_Update;
            _electiveCourseQueue.Remove += _electiveCourseQueue_Remove;

            _electiveListQueue = new BasicMessageQueueConsumer<ElectiveList>();
            _electiveListQueue.Get += _electiveListQueue_Get;
            _electiveListQueue.GetAll += _electiveListQueue_GetAll;
            _electiveListQueue.Create += _electiveListQueue_Create;
            _electiveListQueue.Update += _electiveListQueue_Update;
            _electiveListQueue.Remove += _electiveListQueue_Remove;

            _electiveListCourseQueue = new BasicMessageQueueConsumer<ElectiveListCourse>();
            _electiveListCourseQueue.Get += _electiveListCourseQueue_Get;
            _electiveListCourseQueue.GetAll += _electiveListCourseQueue_GetAll;
            _electiveListCourseQueue.Create += _electiveListCourseQueue_Create;
            _electiveListCourseQueue.Update += _electiveListCourseQueue_Update;
            _electiveListCourseQueue.Remove += _electiveListCourseQueue_Remove;

            _planQueue = new BasicMessageQueueConsumer<Plan>();
            _planQueue.Get += _planQueue_Get;
            _planQueue.GetAll += _planQueue_GetAll;
            _planQueue.Create += _planQueue_Create;
            _planQueue.Update += _planQueue_Update;
            _planQueue.Remove += _planQueue_Remove;

            _planCourseQueue = new BasicMessageQueueConsumer<PlanCourse>();
            _planCourseQueue.Get += _planCourseQueue_Get;
            _planCourseQueue.GetAll += _planCourseQueue_GetAll;
            _planCourseQueue.Create += _planCourseQueue_Create;
            _planCourseQueue.Update += _planCourseQueue_Update;
            _planCourseQueue.Remove += _planCourseQueue_Remove;

            _prerequisiteCourseQueue = new BasicMessageQueueConsumer<PrerequisiteCourse>();
            _prerequisiteCourseQueue.Get += _prerequisiteCourseQueue_Get;
            _prerequisiteCourseQueue.GetAll += _prerequisiteCourseQueue_GetAll;
            _prerequisiteCourseQueue.Create += _prerequisiteCourseQueue_Create;
            _prerequisiteCourseQueue.Update += _prerequisiteCourseQueue_Update;
            _prerequisiteCourseQueue.Remove += _prerequisiteCourseQueue_Remove;

            _requiredCourseQueue = new BasicMessageQueueConsumer<RequiredCourse>();
            _requiredCourseQueue.Get += _requiredCourseQueue_Get;
            _requiredCourseQueue.GetAll += _requiredCourseQueue_GetAll;
            _requiredCourseQueue.Create += _requiredCourseQueue_Create;
            _requiredCourseQueue.Update += _requiredCourseQueue_Update;
            _requiredCourseQueue.Remove += _requiredCourseQueue_Remove;

            _semesterQueue = new BasicMessageQueueConsumer<Semester>();
            _semesterQueue.Get += _semesterQueue_Get;
            _semesterQueue.GetAll += _semesterQueue_GetAll;
            _semesterQueue.Create += _semesterQueue_Create;
            _semesterQueue.Update += _semesterQueue_Update;
            _semesterQueue.Remove += _semesterQueue_Remove;
        }

        #region SemesterDB

        List<Semester> _semesterQueue_GetAll()
        {
            return GetAll<Semester>();
        }

        void _semesterQueue_Remove(Semester data)
        {
            Remove(data);
        }

        void _semesterQueue_Update(Semester data)
        {
            Update(data);
        }

        Semester _semesterQueue_Create(Semester data)
        {
            return Create(data);
        }

        Semester _semesterQueue_Get(Semester data)
        {
            return Get<Semester>(data.ID);
        }

        #endregion

        #region RequiredCourseDB

        void _requiredCourseQueue_Remove(RequiredCourse data)
        {
            Remove(data);
        }

        void _requiredCourseQueue_Update(RequiredCourse data)
        {
            Update(data);
        }

        RequiredCourse _requiredCourseQueue_Create(RequiredCourse data)
        {
            return Create(data);
        }

        List<RequiredCourse> _requiredCourseQueue_GetAll()
        {
            return GetAll<RequiredCourse>();
        }

        RequiredCourse _requiredCourseQueue_Get(RequiredCourse data)
        {
            RequiredCourse requiredCourse = _context.RequiredCourses.Where(i => i.ID == data.ID)
                .Include(rc => rc.course)
                .Include(rc => rc.degreeProgram)
                .First();

            return requiredCourse;
        }

        #endregion

        #region PrerequisiteCourseDB

        void _prerequisiteCourseQueue_Remove(PrerequisiteCourse data)
        {
            Remove(data);
        }

        void _prerequisiteCourseQueue_Update(PrerequisiteCourse data)
        {
            Update(data);
        }

        PrerequisiteCourse _prerequisiteCourseQueue_Create(PrerequisiteCourse data)
        {
            return Create(data);
        }

        List<PrerequisiteCourse> _prerequisiteCourseQueue_GetAll()
        {
            return GetAll<PrerequisiteCourse>();
        }

        PrerequisiteCourse _prerequisiteCourseQueue_Get(PrerequisiteCourse data)
        {
            PrerequisiteCourse prereq = _context.PrerequisiteCourses.Where(i => i.ID == data.ID)
                .Include(pc => pc.prerequisiteCourse)
                .Include(pc => pc.prerequisiteForCourse)
                .First();

            return prereq;
        }

        #endregion

        #region PlanCourseDB

        void _planCourseQueue_Remove(PlanCourse data)
        {
            Remove(data);
        }

        void _planCourseQueue_Update(PlanCourse data)
        {
            Update(data);
        }

        PlanCourse _planCourseQueue_Create(PlanCourse data)
        {
            return Create(data);
        }

        List<PlanCourse> _planCourseQueue_GetAll()
        {
            return GetAll<PlanCourse>();
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

        #region PlanDB

        void _planQueue_Remove(Plan data)
        {
            Remove(data);
        }

        void _planQueue_Update(Plan data)
        {
            Update(data);
        }

        Plan _planQueue_Create(Plan data)
        {
            return Create(data);
        }

        Plan _planQueue_Get(Plan data)
        {
            Plan plan = _context.Plans.Where(p => p.ID == data.ID)
                .Include(pl => pl.degreeProgram)
                .Include(pl => pl.degreeProgram.requiredCourses.Select(s => s.course.prerequisites))
                .Include(pl => pl.degreeProgram.requiredCourses.Select(s => s.course.prerequisiteFor))
                .Include(pl => pl.degreeProgram.electiveCourses.Select(s => s.electiveList))
                .Include(pl => pl.user)
                .Include(pl => pl.semester)
                .Include(pl => pl.planCourses.Select(s => s.plan))
                .Include(pl => pl.planCourses.Select(s => s.course))
                .Include(pl => pl.planCourses.Select(s => s.electiveList))
                .Include(pl => pl.planCourses.Select(s => s.semester))
                .First();

            return plan;
        }

        List<Plan> _planQueue_GetAll()
        {
            List<Plan> planList = _context.Set<Plan>()
                .Include(pl => pl.degreeProgram)
                .Include(pl => pl.user)
                .Include(pl => pl.semester)
                .ToList();

            return planList;
        }

        #endregion

        #region ElectiveListCourseDB

        void _electiveListCourseQueue_Remove(ElectiveListCourse data)
        {
            Remove(data);
        }

        void _electiveListCourseQueue_Update(ElectiveListCourse data)
        {
            Update(data);
        }

        ElectiveListCourse _electiveListCourseQueue_Create(ElectiveListCourse data)
        {
            return Create(data);
        }

        List<ElectiveListCourse> _electiveListCourseQueue_GetAll()
        {
            return GetAll<ElectiveListCourse>();
        }

        ElectiveListCourse _electiveListCourseQueue_Get(ElectiveListCourse data)
        {
            ElectiveListCourse elc = _context.ElectiveListCourses.Where(i => i.ID == data.ID)
                .Include(el => el.course)
                .Include(el => el.electiveList)
                .First();

            return elc;
        }

        #endregion

        #region ElectiveListDB

        void _electiveListQueue_Remove(ElectiveList data)
        {
            Remove(data);
        }

        void _electiveListQueue_Update(ElectiveList data)
        {
            Update(data);
        }

        ElectiveList _electiveListQueue_Create(ElectiveList data)
        {
            return Create(data);
        }

        List<ElectiveList> _electiveListQueue_GetAll()
        {
            return GetAll<ElectiveList>();
        }

        ElectiveList _electiveListQueue_Get(ElectiveList data)
        {
            ElectiveList electiveList = _context.ElectiveLists.Where(i => i.ID == data.ID)
                .Include(el => el.courses.Select(s => s.course))
                .Include(el => el.courses.Select(s => s.electiveList))
                .Include(el => el.electiveCourses.Select(s => s.degreeProgram))
                .Include(el => el.electiveCourses.Select(s => s.electiveList))
                .First();

            return electiveList;
        }

        #endregion

        #region ElectiveCourseDB

        void _electiveCourseQueue_Remove(ElectiveCourse data)
        {
            Remove(data);
        }

        void _electiveCourseQueue_Update(ElectiveCourse data)
        {
            Update(data);
        }

        ElectiveCourse _electiveCourseQueue_Create(ElectiveCourse data)
        {
            return Create(data);
        }

        List<ElectiveCourse> _electiveCourseQueue_GetAll()
        {
            return GetAll<ElectiveCourse>();
        }

        ElectiveCourse _electiveCourseQueue_Get(ElectiveCourse data)
        {
            ElectiveCourse electiveCourse = _context.ElectiveCourses.Where(i => i.ID == data.ID)
                .Include(ec => ec.degreeProgram)
                .Include(ec => ec.electiveList)
                .First();

            return electiveCourse;
        }

        #endregion

        #region DegreeProgramDB

        void _degreeProgramQueue_Remove(DegreeProgram data)
        {
            Remove(data);
        }

        void _degreeProgramQueue_Update(DegreeProgram data)
        {
            Update(data);
        }

        DegreeProgram _degreeProgramQueue_Create(DegreeProgram data)
        {
            return Create(data);
        }

        List<DegreeProgram> _degreeProgramQueue_GetAll()
        {
            return GetAll<DegreeProgram>();
        }

        DegreeProgram _degreeProgramQueue_Get(DegreeProgram data)
        {
            DegreeProgram degreeProgram = _context.DegreePrograms.Where(i => i.ID == data.ID)
                .Include(dp => dp.electiveCourses.Select(s => s.degreeProgram))
                .Include(dp => dp.electiveCourses.Select(s => s.electiveList))
                .Include(dp => dp.requiredCourses.Select(s => s.course))
                .Include(dp => dp.requiredCourses.Select(s => s.degreeProgram))
                .First();

            return degreeProgram;
        }

        #endregion

        #region CourseDB

        void _coursesQueue_Remove(Course course)
        {
            Remove(course);
        }

        void _coursesQueue_Update(Course course)
        {
            Update(course);
        }

        Course _coursesQueue_Create(Course course)
        {
            return Create(course);
        }

        List<Course> _coursesQueue_GetAll()
        {
            return GetAll<Course>();
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

        private void Update<T>(T data)
            where T : IModel
        {
            T entity = _context.Set<T>().Find(data.ID);
            _context.Entry(entity).CurrentValues.SetValues(data);
            _context.SaveChanges();
        }

        private void Remove<T>(T data)
            where T : IModel
        {
            T entity = _context.Set<T>().Find(data.ID);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        private T Create<T>(T data)
            where T : IModel
        {
            _context.Set<T>().Add(data);
            _context.Entry(data).State = System.Data.EntityState.Added;
            _context.SaveChanges();

            return data;
        }

        private T Get<T>(int id)
            where T : IModel
        {
            return _context.Set<T>().Find(id);
        }

        private List<T> GetAll<T>()
            where T : IModel
        {
            return _context.Set<T>().ToList();
        }


    }
}