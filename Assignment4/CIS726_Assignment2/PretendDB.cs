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
            throw new NotImplementedException();
        }

        void _semesterQueue_Remove(Semester data)
        {
            throw new NotImplementedException();
        }

        void _semesterQueue_Update(Semester data)
        {
            throw new NotImplementedException();
        }

        Semester _semesterQueue_Create(Semester data)
        {
            throw new NotImplementedException();
        }

        Semester _semesterQueue_Get(Semester data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RequiredCourseDB

        void _requiredCourseQueue_Remove(RequiredCourse data)
        {
            throw new NotImplementedException();
        }

        void _requiredCourseQueue_Update(RequiredCourse data)
        {
            throw new NotImplementedException();
        }

        RequiredCourse _requiredCourseQueue_Create(RequiredCourse data)
        {
            throw new NotImplementedException();
        }

        List<RequiredCourse> _requiredCourseQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        RequiredCourse _requiredCourseQueue_Get(RequiredCourse data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PrerequisiteCourseDB

        void _prerequisiteCourseQueue_Remove(PrerequisiteCourse data)
        {
            throw new NotImplementedException();
        }

        void _prerequisiteCourseQueue_Update(PrerequisiteCourse data)
        {
            throw new NotImplementedException();
        }

        PrerequisiteCourse _prerequisiteCourseQueue_Create(PrerequisiteCourse data)
        {
            throw new NotImplementedException();
        }

        List<PrerequisiteCourse> _prerequisiteCourseQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        PrerequisiteCourse _prerequisiteCourseQueue_Get(PrerequisiteCourse data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PlanCourseDB

        void _planCourseQueue_Remove(PlanCourse data)
        {
            throw new NotImplementedException();
        }

        void _planCourseQueue_Update(PlanCourse data)
        {
            throw new NotImplementedException();
        }

        PlanCourse _planCourseQueue_Create(PlanCourse data)
        {
            throw new NotImplementedException();
        }

        List<PlanCourse> _planCourseQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        PlanCourse _planCourseQueue_Get(PlanCourse data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PlanDB

        void _planQueue_Remove(Plan data)
        {
            throw new NotImplementedException();
        }

        void _planQueue_Update(Plan data)
        {
            throw new NotImplementedException();
        }

        Plan _planQueue_Create(Plan data)
        {
            throw new NotImplementedException();
        }

        List<Plan> _planQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        Plan _planQueue_Get(Plan data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ElectiveListCourseDB

        void _electiveListCourseQueue_Remove(ElectiveListCourse data)
        {
            throw new NotImplementedException();
        }

        void _electiveListCourseQueue_Update(ElectiveListCourse data)
        {
            throw new NotImplementedException();
        }

        ElectiveListCourse _electiveListCourseQueue_Create(ElectiveListCourse data)
        {
            throw new NotImplementedException();
        }

        List<ElectiveListCourse> _electiveListCourseQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        ElectiveListCourse _electiveListCourseQueue_Get(ElectiveListCourse data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ElectiveListDB

        void _electiveListQueue_Remove(ElectiveList data)
        {
            throw new NotImplementedException();
        }

        void _electiveListQueue_Update(ElectiveList data)
        {
            throw new NotImplementedException();
        }

        ElectiveList _electiveListQueue_Create(ElectiveList data)
        {
            throw new NotImplementedException();
        }

        List<ElectiveList> _electiveListQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        ElectiveList _electiveListQueue_Get(ElectiveList data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ElectiveCourseDB

        void _electiveCourseQueue_Remove(ElectiveCourse data)
        {
            throw new NotImplementedException();
        }

        void _electiveCourseQueue_Update(ElectiveCourse data)
        {
            throw new NotImplementedException();
        }

        ElectiveCourse _electiveCourseQueue_Create(ElectiveCourse data)
        {
            throw new NotImplementedException();
        }

        List<ElectiveCourse> _electiveCourseQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        ElectiveCourse _electiveCourseQueue_Get(ElectiveCourse data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DegreeProgramDB

        void _degreeProgramQueue_Remove(DegreeProgram data)
        {
            throw new NotImplementedException();
        }

        void _degreeProgramQueue_Update(DegreeProgram data)
        {
            throw new NotImplementedException();
        }

        DegreeProgram _degreeProgramQueue_Create(DegreeProgram data)
        {
            throw new NotImplementedException();
        }

        List<DegreeProgram> _degreeProgramQueue_GetAll()
        {
            throw new NotImplementedException();
        }

        DegreeProgram _degreeProgramQueue_Get(DegreeProgram data)
        {
            throw new NotImplementedException();
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
            return Get<Course>(partialCourse.ID);
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