using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Base
{
    public static class EventConstants
    {
        public const int ServiceInfoMessageEventId = 7;
        public const int ServiceWarnMessageEventId = 8;
        public const int ServiceErrorMessageEventId = 9;
        public const int ServiceVerboseMessageEventId = 10;
        public const int ServiceCriticalMessageEventId = 11;
        public const int ServiceLogAlwaysMessageEventId = 12;
    }
}
