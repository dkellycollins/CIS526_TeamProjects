using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIS726_Assignment2.Models;
using CIS726_Assignment2.SystemBus;

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
            _coursesQueue.NewMessage += _coursesQueue_NewMessage;
        }

        List<Course> _coursesQueue_NewMessage(string action, List<Course> courses)
        {
            if (action == "GET")
                return (List<Course>)_context.Courses.AsQueryable().ToList();
            else
            {
                if (action == "CREATE")
                {
                    foreach (Course course in courses)
                        _context.Courses.Add(course);
                }
                else if (action == "UPDATE")
                {
                    foreach (Course course in courses)
                        _context.Entry(course).State = System.Data.EntityState.Modified;
                }
                else if (action == "REMOVE")
                {
                    foreach (Course course in courses)
                        _context.Courses.Remove(course);
                }
                _context.SaveChanges();
                return null;
            }
        }
    }
}