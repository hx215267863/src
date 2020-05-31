using EF.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{
    public class Class1
    {

        public void aaaa()
        {


            using (var db = new AppDbContext())
            {

                db.Blogs.Add(new Blog { Title = "http://blogs.msdn.com/adonet", CreateTime = DateTime.Now });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine(" - {0}", blog.Title);
                }

            }

        }
    }
}