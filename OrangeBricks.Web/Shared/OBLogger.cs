using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace OrangeBricks.Web.Shared
{
    public class OBLogger : IOBLogger
    {
        public OBLogger()
        {
            //Adding basic configuration. This can be moved to config file for flexibility.
            SimpleLayout layout = new SimpleLayout();
            layout.ActivateOptions();

            DebugAppender appender = new DebugAppender();
            appender.Layout = layout;
            appender.ActivateOptions();

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            Logger root = hierarchy.Root;

            root.Level = log4net.Core.Level.All;
            log4net.Config.BasicConfigurator.Configure(appender);
        }

        public ILog Getlog4net()
        {
            return log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        
    }
}
