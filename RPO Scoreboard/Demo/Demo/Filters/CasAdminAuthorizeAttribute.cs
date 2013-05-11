using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.Models;
using Demo.Repositories;
using WebMatrix.WebData;

namespace Demo.Filters
{
    public class CasAdminAuthorizeAttribute : AuthorizeAttribute
    {
        private UserProfile _currentUser = null;
        protected UserProfile CurrentUser
        {
            get
            {
                if (_currentUser == null || _currentUser.UserName != WebSecurity.CurrentUserName)
                {
                    _currentUser = _userRepo.Get((x) => x.UserName == WebSecurity.CurrentUserName).FirstOrDefault();
                }
                return _currentUser;
            }
        }

        private IRepository<UserProfile> _userRepo;

        public CasAdminAuthorizeAttribute()
            : base()
        {
            _userRepo = new BasicRepo<UserProfile>();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (CurrentUser != null)
                return CurrentUser.IsAdmin;
            return false;
        }
    }
}