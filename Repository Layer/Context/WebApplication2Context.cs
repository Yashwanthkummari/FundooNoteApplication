using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Context
{
    
        public  class WebApplication2Context : DbContext
        {
            public WebApplication2Context(DbContextOptions options)
                : base(options)
            {
            }
            public  DbSet<UserEntity> User { get; set; }
        }
    }

