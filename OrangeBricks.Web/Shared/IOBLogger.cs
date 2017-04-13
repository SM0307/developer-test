using log4net;

namespace OrangeBricks.Web.Shared
{
    public interface IOBLogger
    {
        ILog Getlog4net();
    }
}