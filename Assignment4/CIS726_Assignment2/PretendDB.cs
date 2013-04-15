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

        public PretendDB()
        {
            _context = new CourseDBContext();
            InitQueues();
        }

        public void Init()
        {
            _coursesQueue.BeginProcessing();
        }

        private void InitQueues()
        {
            _coursesQueue = new BasicMessageQueueConsumer<Course>();
            _coursesQueue.Get += _coursesQueue_Get;
            _coursesQueue.GetAll += _coursesQueue_GetAll;
            _coursesQueue.Create += _coursesQueue_Create;
            _coursesQueue.Update += _coursesQueue_Update;
            _coursesQueue.Remove += _coursesQueue_Remove;
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

        void _coursesQueue_Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        List<Course> _coursesQueue_GetAll()
        {
            return _context.Set<Course>()
                .Include("prerequisites")
                .Include("degreePrograms")
                .Include("electiveLists")
                .Include("prerequisiteFor")
                .ToList();
        }

        Course _coursesQueue_Get(Course partialCourse)
        {
            return _context.Courses.Find(partialCourse.ID);
        }

        #endregion 
    }
}