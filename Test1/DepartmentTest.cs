using Microsoft.VisualStudio.TestTools.UnitTesting;
using PMB.Models;
using PMB.Services;
using System;
using System.Collections.Generic;

namespace PMB.Tests
{
    [TestClass]
    public class ConfigurationLoadingTests
    {
        [TestMethod]
        public void LoadQuotaConfig_FromDictionary_Success()
        {
            var dict = new Dictionary<string, int>
            {
                { "CS", 30 },
                { "EE", 20 }
            };

            var quotaConfig = new QuotaConfig { CourseQuotas = dict };

            Assert.IsNotNull(quotaConfig.CourseQuotas);
            Assert.AreEqual(30, quotaConfig.CourseQuotas["CS"]);
            Assert.AreEqual(20, quotaConfig.CourseQuotas["EE"]);
        }
    }
}
