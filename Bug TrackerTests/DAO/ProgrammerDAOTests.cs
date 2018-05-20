using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bug_Tracker.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug_Tracker.DAO.Tests
{
    [TestClass()]
    public class ProgrammerDAOTests
    {
        [TestMethod()]
        public void IsLoginTest()
        {
            ProgrammerDAO programmerDAO = new ProgrammerDAO();
            int res = 0;
            try
            {
                res = programmerDAO.IsLogin("nishan", "nishan");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Assert.AreEqual(2010, res);
        }
    }
}