using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIS726_Assignment2.Models;
using CIS726_Assignment2.SystemBus;
using CIS726_Assignment2.Repositories;
using System.Data.Entity;

namespace CIS726_Assignment2
{
    public class PretendDB
    {
        CourseDBContext _context;
        IMessageQueueConsumer<Course> _coursesQueue;
        IMessageQueueConsumer<Plan> _planQueue;

        public PretendDB()
        {
            _context = new CourseDBContext();

            InitQueues();
        }

        public void Init()
        {
            _coursesQueue.BeginProcessing();
            _planQueue.BeginProcessing();
        }

        private void InitQueues()
        {
            _coursesQueue = new BasicMessageQueueConsumer<Course>();
            _coursesQueue.Get += _coursesQueue_Get;
            _coursesQueue.Create += _coursesQueue_Create;
            _coursesQueue.Update += _coursesQueue_Update;
            _coursesQueue.Remove += _coursesQueue_Remove;

            _planQueue = new BasicMessageQueueConsumer<Plan>();
            _planQueue.Get += _planQueue_Get;
            _planQueue.Create += _planQueue_Create;
            _planQueue.Update += _planQueue_Update;
            _planQueue.Remove += _planQueue_Remove;
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

        List<Course> _coursesQueue_Get()
        {
            List<Course> courseList = _context.Set<Course>()
                .ToList();

            return courseList;
        }

        #endregion 

        #region

        void _planQueue_Remove(Plan plan)
        {

        }

        void _planQueue_Update(Plan plan)
        {

        }

        void _planQueue_Create(Plan plan)
        {

        }

        List<Plan> _planQueue_Get()
        {
            List<Plan> planList =  _context.Set<Plan>()
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
                .ToList();

            return planList;
        }

        #endregion

    }
}