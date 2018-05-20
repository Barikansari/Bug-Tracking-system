using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug_Tracker.Model;

namespace Bug_Tracker.DAO.Tests
{
    [TestClass()]
    public class BugDAOTests
    {
        
        [TestMethod()]
        public void getAllBugsTest()
        {

            BugDAO bugDAO = new BugDAO();
            List<Bug> bug = null;
            
            try
            {
                bug = bugDAO.getAllBugs();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Assert.AreEqual("",bug);
        }
    }
}